// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.
using System.Collections.Immutable;
using Azure.Deployments.Extensibility.Extensions.Kubernetes;
using Bicep.Local.Extension.Protocol;
using System.Diagnostics;
using Azure.Deployments.Extensibility.Core.Json;
using System.Text.Json.Nodes;
using System.Text.Json;
using Json.More;
using System.Formats.Asn1;
using Microsoft.Extensions.DependencyInjection;
using Azure.Deployments.Extensibility.AspNetCore;
using Azure.Deployments.Extensibility.Extensions.Kubernetes.DependencyInjection;
using V2Models = Azure.Deployments.Extensibility.Core.V2.Models;
using Microsoft.AspNetCore.Http;

namespace Bicep.Local.Extension.Kubernetes.Handlers;

public partial class KubernetesResourceHandler : IGenericResourceHandler
{
    private static IExtension GetKubernetesExtension()
    {
        var serviceProvider = new ServiceCollection().AddKubernetesExtensionDispatcher().BuildServiceProvider();
        var extensionDispatcher = serviceProvider.GetRequiredKeyedService<IExtensionDispatcher>("Kubernetes");

        return extensionDispatcher.DispatchExtension("1.0.0");
    }

    public async Task<LocalExtensibilityOperationResponse> Delete(Protocol.ResourceReference request, CancellationToken cancellationToken)
        => Convert(await GetKubernetesExtension().DeleteResourceAsync(new(), Convert(request), cancellationToken), request.Type, request.ApiVersion);

    public async Task<LocalExtensibilityOperationResponse> Get(Protocol.ResourceReference request, CancellationToken cancellationToken)
        => Convert(await GetKubernetesExtension().GetResourceAsync(new(), Convert(request), cancellationToken), request.Type, request.ApiVersion);

    public async Task<LocalExtensibilityOperationResponse> Preview(Protocol.ResourceSpecification request, CancellationToken cancellationToken)
        => Convert(await GetKubernetesExtension().PreviewResourceCreateOrUpdateAsync(new(), Convert(request), cancellationToken), request.Type, request.ApiVersion);

    public async Task<LocalExtensibilityOperationResponse> CreateOrUpdate(Protocol.ResourceSpecification request, CancellationToken cancellationToken)
        => Convert(await GetKubernetesExtension().CreateOrUpdateResourceAsync(new(), Convert(request), cancellationToken), request.Type, request.ApiVersion);

    private static V2Models.ResourceSpecification Convert(Protocol.ResourceSpecification request)
        => new(
            request.Type,
            request.ApiVersion,
            request.Properties,
            request.Config);

    private static V2Models.ResourceReference Convert(Protocol.ResourceReference request)
        => new(
            request.Type,
            request.ApiVersion,
            request.Identifiers,
            request.Config);

    private static Protocol.LocalExtensibilityOperationResponse Convert(IResult response, string type, string? apiVersion)
    {
        switch (response)
        {
            case ExtCore.ExtensibilityOperationErrorResponse errorResponse:
                var errors = errorResponse.Errors.ToArray();
                if (errors.Length > 1)
                {
                    return new(
                        null,
                        new(new("MultipleErrorsOccurred", "", "Multiple errors occurred", errorResponse.Errors.Select(x => new ErrorDetail(x.Code, x.Target.ToString(), x.Message)).ToArray(), null)));
                }
                
                var error = errors.First();
                return new(
                    null,
                    new(new(error.Code, error.Target.ToString(), error.Message, null, null)));
            case ExtCore.ExtensibilityOperationSuccessResponse successResponse:
                return new(
                    new(type, apiVersion, "Succeeded", JsonObject.Create(successResponse.Resource.Properties)!, null, JsonObject.Create(successResponse.Resource.Properties)!),
                    null);
            default:
                throw new InvalidOperationException($"Unexpected response type: {response.GetType()}");
        }
    }
}