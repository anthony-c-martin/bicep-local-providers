// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.
using Bicep.Local.Extension.Protocol;
using Octokit;

namespace Bicep.Local.Extension.Github.Handlers;

public class CollaboratorResourceHandler : ResourceHandlerBase
{
    public override string ResourceType => "Collaborator";

    protected override Task<ExtensibilityOperationResponse> Delete(GitHubClient client, ExtensibilityOperationRequest request, CancellationToken cancellationToken)
        => throw new NotImplementedException();

    protected override async Task<ExtensibilityOperationResponse> Get(GitHubClient client, ExtensibilityOperationRequest request, CancellationToken cancellationToken)
    {
        var owner = request.Resource.Properties!["owner"]!.GetValue<string>();
        var name = request.Resource.Properties!["name"]!.GetValue<string>();
        var user = request.Resource.Properties!["user"]!.GetValue<string>();

        var response = await client.Connection.Get<object>(ApiUrls.RepoCollaborator(owner, name, user), null);

        return new ExtensibilityOperationResponse(
            new(request.Resource.Type, request.Resource.Properties),
            null,
            null);
    }

    protected override Task<ExtensibilityOperationResponse> PreviewSave(GitHubClient client, ExtensibilityOperationRequest request, CancellationToken cancellationToken)
        => throw new NotImplementedException();

    protected override async Task<ExtensibilityOperationResponse> Save(GitHubClient client, ExtensibilityOperationRequest request, CancellationToken cancellationToken)
    {
        var owner = request.Resource.Properties!["owner"]!.GetValue<string>();
        var name = request.Resource.Properties!["name"]!.GetValue<string>();
        var user = request.Resource.Properties!["user"]!.GetValue<string>();

        var response = await client.Connection.Put<object>(ApiUrls.RepoCollaborator(owner, name, user), null);

        return new ExtensibilityOperationResponse(
            new(request.Resource.Type, request.Resource.Properties),
            null,
            null);
    }
}
