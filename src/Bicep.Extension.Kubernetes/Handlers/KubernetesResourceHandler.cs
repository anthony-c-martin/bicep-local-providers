// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.
using System.Collections.Immutable;
using Azure.Deployments.Extensibility.Core.Exceptions;
using Azure.Deployments.Extensibility.Providers.Kubernetes;
using Bicep.Extension.Protocol;
using System.Diagnostics;
using Azure.Deployments.Extensibility.Core.Json;
using System.Text.Json.Nodes;
using System.Text.Json;
using Core = Azure.Deployments.Extensibility.Core;
using Json.More;

namespace Bicep.Extension.Kubernetes.Handlers;

public partial class KubernetesResourceHandler : IGenericResourceHandler
{
    public Task<ExtensibilityOperationResponse> Delete(ExtensibilityOperationRequest request, CancellationToken cancellationToken)
        => ExecuteRequest(request, (coreRequest) => new KubernetesProvider().DeleteAsync(coreRequest, cancellationToken));

    public Task<ExtensibilityOperationResponse> Get(ExtensibilityOperationRequest request, CancellationToken cancellationToken)
        => ExecuteRequest(request, (coreRequest) => new KubernetesProvider().GetAsync(coreRequest, cancellationToken));

    public Task<ExtensibilityOperationResponse> PreviewSave(ExtensibilityOperationRequest request, CancellationToken cancellationToken)
        => ExecuteRequest(request, (coreRequest) => new KubernetesProvider().PreviewSaveAsync(coreRequest, cancellationToken));

    public Task<ExtensibilityOperationResponse> Save(ExtensibilityOperationRequest request, CancellationToken cancellationToken)
        => ExecuteRequest(request, (coreRequest) => new KubernetesProvider().SaveAsync(coreRequest, cancellationToken));

    private static async Task<ExtensibilityOperationResponse> ExecuteRequest(ExtensibilityOperationRequest request, Func<Azure.Deployments.Extensibility.Core.ExtensibilityOperationRequest, Task<Azure.Deployments.Extensibility.Core.ExtensibilityOperationResponse>> func)
    {
        try 
        {
            var coreRequest = Convert(request);
            var coreResponse = await func(coreRequest);

            return Convert(coreResponse);
        }
        catch (ExtensibilityException ex) when (ex.Errors.Any())
        {
            return new(
                null,
                null,
                ex.Errors.Select(x => new ExtensibilityError(x.Code, x.Message, x.Target.ToString())).ToImmutableArray());
        }
    }

    private static Core.ExtensibilityOperationRequest Convert(ExtensibilityOperationRequest request)
    {
        return new(
            new(request.Import.Provider, request.Import.Version, JsonSerializer.Deserialize<JsonElement>(request.Import.Config)),
            new(request.Resource.Type, JsonSerializer.Deserialize<JsonElement>(request.Resource.Properties)));
    }

    private static ExtensibilityOperationResponse Convert(Core.ExtensibilityOperationResponse response)
    {
        switch (response)
        {
            case Core.ExtensibilityOperationErrorResponse errorResponse:
                return new(
                    null,
                    null,
                    errorResponse.Errors.Select(x => new ExtensibilityError(x.Code, x.Message, x.Target.ToString())).ToImmutableArray());
            case Core.ExtensibilityOperationSuccessResponse successResponse:
                return new(
                    new(successResponse.Resource.Type, successResponse.Resource.Properties.AsNode() as JsonObject),
                    successResponse.ResourceMetadata is {} metadata ? Convert(metadata) : null,
                    ImmutableArray<ExtensibilityError>.Empty);
            default:
                throw new InvalidOperationException($"Unexpected response type: {response.GetType()}");
        }
    }

    private static ExtensibleResourceMetadata Convert(Core.ExtensibleResourceMetadata metadata)
    {
        return new(
            metadata.ReadOnlyProperties?.Select(x => x.ToString()).ToImmutableArray(),
            metadata.ImmutableProperties?.Select(x => x.ToString()).ToImmutableArray(),
            metadata.DynamicProperties?.Select(x => x.ToString()).ToImmutableArray());
    }
}