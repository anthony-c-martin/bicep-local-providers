// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using Bicep.Extension.Protocol;
using Octokit;

namespace Bicep.Extension.Github.Handlers;

public class RepositoryResourceHandler : ResourceHandlerBase
{
    public override string ResourceType => "Repository";

    protected override Task<ExtensibilityOperationResponse> Delete(GitHubClient client, ExtensibilityOperationRequest request, CancellationToken cancellationToken)
        => throw new NotImplementedException();

    protected override async Task<ExtensibilityOperationResponse> Get(GitHubClient client, ExtensibilityOperationRequest request, CancellationToken cancellationToken)
    {
        var owner = request.Resource.Properties!["owner"]!.GetValue<string>();
        var name = request.Resource.Properties!["name"]!.GetValue<string>();

        var response = await client.Connection.Get<object>(ApiUrls.Repository(owner, name), null);
        var body = JsonNode.Parse(response.Body.ToString()!) as JsonObject;

        return new ExtensibilityOperationResponse(
            new(request.Resource.Type, body),
            null,
            null);
    }

    protected override Task<ExtensibilityOperationResponse> PreviewSave(GitHubClient client, ExtensibilityOperationRequest request, CancellationToken cancellationToken)
        => throw new NotImplementedException();

    protected override async Task<ExtensibilityOperationResponse> Save(GitHubClient client, ExtensibilityOperationRequest request, CancellationToken cancellationToken)
    {
        var owner = request.Resource.Properties!["owner"]!.GetValue<string>();
        var name = request.Resource.Properties!["name"]!.GetValue<string>();

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

        return new ExtensibilityOperationResponse(
            new(request.Resource.Type, body),
            null,
            null);
    }
}