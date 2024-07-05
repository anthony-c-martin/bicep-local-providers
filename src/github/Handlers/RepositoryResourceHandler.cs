// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using Bicep.Local.Extension.Protocol;
using Octokit;

namespace Bicep.Local.Extension.Github.Handlers;

public class RepositoryResourceHandler : ResourceHandlerBase
{
    public override string ResourceType => "Repository";

    private static (string owner, string name) GetIdentifiers(JsonObject properties)
    {
        return new(
            properties["owner"]!.GetValue<string>(),
            properties["name"]!.GetValue<string>());
    }

    private static JsonObject GetIdentifiersObject(string owner, string name)
        => new()
        {
            ["owner"] = owner,
            ["name"] = name
        };

    protected override Task<LocalExtensibilityOperationResponse> Delete(GitHubClient client, ResourceReference request, CancellationToken cancellationToken)
        => throw new NotImplementedException();

    protected override async Task<LocalExtensibilityOperationResponse> Get(GitHubClient client, ResourceReference request, CancellationToken cancellationToken)
    {
        var identifiers = GetIdentifiers(request.Identifiers);

        var response = await client.Connection.Get<object>(ApiUrls.Repository(identifiers.owner, identifiers.name), null);
        var body = JsonNode.Parse(response.Body.ToString()!) as JsonObject;

        return new(
            new(request.Type, request.ApiVersion, "Succeeded", request.Identifiers, request.Config, body!),
            null);
    }

    protected override async Task<LocalExtensibilityOperationResponse> Preview(GitHubClient client, ResourceSpecification request, CancellationToken cancellationToken)
    {
        await Task.Yield();
        var (owner, name) = GetIdentifiers(request.Properties);

        return new(
            new(request.Type, request.ApiVersion, "Succeeded", GetIdentifiersObject(owner, name), request.Config, GetIdentifiersObject(owner, name)),
            null);
    }

    protected override async Task<LocalExtensibilityOperationResponse> CreateOrUpdate(GitHubClient client, ResourceSpecification request, CancellationToken cancellationToken)
    {
        var (owner, name) = GetIdentifiers(request.Properties);

        var response = await client.Connection.Get<object>(ApiUrls.Repository(owner, name), null);
        if (response.HttpResponse.StatusCode == System.Net.HttpStatusCode.OK)
        {
            response = await client.Connection.Patch<object>(ApiUrls.Repository(owner, name), null);
        }
        else
        {
            response = await client.Connection.Put<object>(ApiUrls.Repository(owner, name), null);
        }
        var body = JsonNode.Parse(response.Body.ToString()!) as JsonObject;

        return new(
            new(request.Type, request.ApiVersion, "Succeeded", GetIdentifiersObject(owner, name), request.Config, body!),
            null);
    }
}