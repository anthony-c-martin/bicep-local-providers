// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.
using System.Text.Json;
using System.Text.Json.Nodes;
using Bicep.Local.Extension.Protocol;

namespace Bicep.Local.Extension.Utils.Handlers;

public record WaitRequest(
    int durationMs);

public record WaitResponse();

public class WaitResourceHandler : IResourceHandler
{
    public string ResourceType => "Wait";

    public Task<LocalExtensibilityOperationResponse> Delete(ResourceReference request, CancellationToken cancellationToken)
        => throw new NotImplementedException();

    public Task<LocalExtensibilityOperationResponse> Get(ResourceReference request, CancellationToken cancellationToken)
        => throw new NotImplementedException();

    public Task<LocalExtensibilityOperationResponse> Preview(ResourceSpecification request, CancellationToken cancellationToken)
        => throw new NotImplementedException();

    public async Task<LocalExtensibilityOperationResponse> CreateOrUpdate(ResourceSpecification request, CancellationToken cancellationToken)
    {
        var body = JsonSerializer.Deserialize(request.Properties, SerializationContext.Default.WaitRequest)
            ?? throw new InvalidOperationException("Failed to deserialize request body");

        await Task.Delay(body.durationMs, cancellationToken);

        return new(
            new(request.Type, request.ApiVersion, "Succeeded", new(), request.Config, new()),
            null);
    }
}
