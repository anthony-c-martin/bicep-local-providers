// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.
using System.Collections.Immutable;
using Newtonsoft.Json;
using Azure.Deployments.Extensibility.Core.Exceptions;
using Azure.Deployments.Extensibility.Providers.Kubernetes;
using Bicep.Extension.Rpc;
using System.Diagnostics;
using Azure.Deployments.Extensibility.Core.Json;
using Newtonsoft.Json.Serialization;

namespace Bicep.Extension.Kubernetes.Handlers;

public partial class KubernetesResourceHandler : IGenericResourceHandler
{
    private static readonly JsonSerializerSettings JsonSerializerSettings = new()
    {
        ContractResolver = new CamelCasePropertyNamesContractResolver(),
    };

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

    private static Azure.Deployments.Extensibility.Core.ExtensibilityOperationRequest Convert(ExtensibilityOperationRequest request)
    {
        var serialized = JsonConvert.SerializeObject(request, JsonSerializerSettings);

        return ExtensibilityJsonSerializer.Default.Deserialize<Azure.Deployments.Extensibility.Core.ExtensibilityOperationRequest>(serialized) 
            ?? throw new InvalidOperationException($"Failed to deserialize {nameof(ExtensibilityOperationRequest)}");
    }

    private static ExtensibilityOperationResponse Convert(Azure.Deployments.Extensibility.Core.ExtensibilityOperationResponse response)
    {
        var serialized = ExtensibilityJsonSerializer.Default.Serialize(response);

        return JsonConvert.DeserializeObject<ExtensibilityOperationResponse>(serialized, JsonSerializerSettings)
            ?? throw new InvalidOperationException($"Failed to deserialize {nameof(ExtensibilityOperationResponse)}");
    }
}