// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.
using System.Text.Json.Nodes;
using Bicep.Local.Extension.Protocol;
using Octokit;
using Octokit.Internal;

namespace Bicep.Local.Extension.Github.Handlers;

public abstract class ResourceHandlerBase : IResourceHandler
{
    public abstract string ResourceType { get; }

    private async Task<LocalExtensibilityOperationResponse> HandleRequest(JsonObject? config, Func<GitHubClient, Task<LocalExtensibilityOperationResponse>> onExecuteFunc)
    {
        var credentials = new Credentials(config!["token"]!.GetValue<string>());
        var client = new GitHubClient(new ProductHeaderValue("Bicep.LocalDeploy"), new InMemoryCredentialStore(credentials));

        try
        {
            return await onExecuteFunc(client);
        }
        catch (Exception exception)
        {
            return new LocalExtensibilityOperationResponse(
                null,
                new(new("UnhandledError", "", exception.Message, null, null)));
        }
    }

    protected abstract Task<LocalExtensibilityOperationResponse> CreateOrUpdate(GitHubClient client, ResourceSpecification request, CancellationToken cancellationToken);

    public Task<LocalExtensibilityOperationResponse> CreateOrUpdate(ResourceSpecification request, CancellationToken cancellationToken)
        => HandleRequest(request.Config, client => CreateOrUpdate(client, request, cancellationToken));

    protected abstract Task<LocalExtensibilityOperationResponse> Preview(GitHubClient client, ResourceSpecification request, CancellationToken cancellationToken);

    public Task<LocalExtensibilityOperationResponse> Preview(ResourceSpecification request, CancellationToken cancellationToken)
        => HandleRequest(request.Config, client => Preview(client, request, cancellationToken));

    protected abstract Task<LocalExtensibilityOperationResponse> Get(GitHubClient client, ResourceReference request, CancellationToken cancellationToken);

    public Task<LocalExtensibilityOperationResponse> Get(ResourceReference request, CancellationToken cancellationToken)
        => HandleRequest(request.Config, client => Get(client, request, cancellationToken));

    protected abstract Task<LocalExtensibilityOperationResponse> Delete(GitHubClient client, ResourceReference request, CancellationToken cancellationToken);

    public Task<LocalExtensibilityOperationResponse> Delete(ResourceReference request, CancellationToken cancellationToken)
        => HandleRequest(request.Config, client => Delete(client, request, cancellationToken));
}
