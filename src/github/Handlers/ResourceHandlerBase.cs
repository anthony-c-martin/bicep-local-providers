// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.
using Bicep.Local.Extension.Protocol;
using Octokit;
using Octokit.Internal;

namespace Bicep.Local.Extension.Github.Handlers;

public abstract class ResourceHandlerBase : IResourceHandler
{
    public abstract string ResourceType { get; }

    private async Task<ExtensibilityOperationResponse> HandleRequest(ExtensibilityOperationRequest request, Func<GitHubClient, Task<ExtensibilityOperationResponse>> onExecuteFunc)
    {
        var credentials = new Credentials(request.Import.Config!["token"]!.GetValue<string>());
        var client = new GitHubClient(new ProductHeaderValue("Bicep.LocalDeploy"), new InMemoryCredentialStore(credentials));

        try
        {
            return await onExecuteFunc(client);
        }
        catch (Exception exception)
        {
            return new ExtensibilityOperationResponse(
                null,
                null,
                [new(request.Resource.Type, exception.Message, "")]);
        }
    }

    protected abstract Task<ExtensibilityOperationResponse> Delete(GitHubClient client, ExtensibilityOperationRequest request, CancellationToken cancellationToken);

    public Task<ExtensibilityOperationResponse> Delete(ExtensibilityOperationRequest request, CancellationToken cancellationToken)
        => HandleRequest(request, client => Delete(client, request, cancellationToken));

    protected abstract Task<ExtensibilityOperationResponse> Get(GitHubClient client, ExtensibilityOperationRequest request, CancellationToken cancellationToken);

    public Task<ExtensibilityOperationResponse> Get(ExtensibilityOperationRequest request, CancellationToken cancellationToken)
        => HandleRequest(request, client => Get(client, request, cancellationToken));

    protected abstract Task<ExtensibilityOperationResponse> PreviewSave(GitHubClient client, ExtensibilityOperationRequest request, CancellationToken cancellationToken);

    public Task<ExtensibilityOperationResponse> PreviewSave(ExtensibilityOperationRequest request, CancellationToken cancellationToken)
        => HandleRequest(request, client => PreviewSave(client, request, cancellationToken));

    protected abstract Task<ExtensibilityOperationResponse> Save(GitHubClient client, ExtensibilityOperationRequest request, CancellationToken cancellationToken);

    public Task<ExtensibilityOperationResponse> Save(ExtensibilityOperationRequest request, CancellationToken cancellationToken)
        => HandleRequest(request, client => Save(client, request, cancellationToken));
}
