// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.
using System.Text.Json;
using System.Text.Json.Nodes;
using Bicep.Extension.Rpc;
using Newtonsoft.Json.Linq;
using JsonConvert = Newtonsoft.Json.JsonConvert;

namespace Bicep.Extension.Utils.Handlers;

public record WaitRequest(
    int durationMs);

public record WaitResponse();

public class WaitResourceHandler : IResourceHandler
{
    public string ResourceType => "Wait";

    public Task<ExtensibilityOperationResponse> Delete(ExtensibilityOperationRequest request, CancellationToken cancellationToken)
        => throw new NotImplementedException();

    public Task<ExtensibilityOperationResponse> Get(ExtensibilityOperationRequest request, CancellationToken cancellationToken)
        => throw new NotImplementedException();

    public Task<ExtensibilityOperationResponse> PreviewSave(ExtensibilityOperationRequest request, CancellationToken cancellationToken)
        => throw new NotImplementedException();

    public async Task<ExtensibilityOperationResponse> Save(ExtensibilityOperationRequest request, CancellationToken cancellationToken)
    {
        var body = JsonSerializer.Deserialize(JsonConvert.SerializeObject(request.Resource.Properties), SerializationContext.Default.WaitRequest)
            ?? throw new InvalidOperationException("Failed to deserialize request body");

        await Task.Delay(body.durationMs, cancellationToken);

        return new ExtensibilityOperationResponse(
            new ExtensibleResourceData(request.Resource.Type, new JObject()),
            null,
            null);
    }
}
