// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.
using System.Text.Json.Nodes;
using Bicep.Local.Extension.Protocol;
using Octokit;

namespace Bicep.Local.Extension.Github.Handlers;

public class CollaboratorResourceHandler : ResourceHandlerBase
{
    public override string ResourceType => "Collaborator";

    private static (string owner, string name, string user) GetIdentifiers(JsonObject properties)
    {
        return new(
            properties["owner"]!.GetValue<string>(),
            properties["name"]!.GetValue<string>(),
            properties["user"]!.GetValue<string>());
    }

    private static JsonObject GetIdentifiersObject(string owner, string name, string user)
        => new()
        {
            ["owner"] = owner,
            ["name"] = name,
            ["user"] = user
        };

    protected override Task<LocalExtensibilityOperationResponse> Delete(GitHubClient client, ResourceReference request, CancellationToken cancellationToken)
        => throw new NotImplementedException();

    protected override async Task<LocalExtensibilityOperationResponse> Get(GitHubClient client, ResourceReference request, CancellationToken cancellationToken)
    {
        var (owner, name, user) = GetIdentifiers(request.Identifiers);

        var response = await client.Connection.Get<object>(ApiUrls.RepoCollaborator(owner, name, user), null);
        var body = JsonNode.Parse(response.Body.ToString()!) as JsonObject;

        return new(
            new(request.Type, request.ApiVersion, "Succeeded", request.Identifiers, request.Config, body!),
            null);
    }

    protected override async Task<LocalExtensibilityOperationResponse> Preview(GitHubClient client, ResourceSpecification request, CancellationToken cancellationToken)
    {
        await Task.Yield();
        var (owner, name, user) = GetIdentifiers(request.Properties);

        return new(
            new(request.Type, request.ApiVersion, "Succeeded", GetIdentifiersObject(owner, name, user), request.Config, GetIdentifiersObject(owner, name, user)),
            null);
    }

    protected override async Task<LocalExtensibilityOperationResponse> CreateOrUpdate(GitHubClient client, ResourceSpecification request, CancellationToken cancellationToken)
    {
        var (owner, name, user) = GetIdentifiers(request.Properties);

        var response = await client.Connection.Put<object>(ApiUrls.RepoCollaborator(owner, name, user), null);
        var body = JsonNode.Parse(response.Body.ToString()!) as JsonObject;

        return new(
            new(request.Type, request.ApiVersion, "Succeeded", GetIdentifiersObject(owner, name, user), request.Config, body!),
            null);
    }
}
