// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.
using System.Text.Json;
using System.Text.Json.Nodes;
using Bicep.Extension.Rpc;
using Newtonsoft.Json.Linq;
using JsonConvert = Newtonsoft.Json.JsonConvert;

namespace Bicep.Extension.Utils.Handlers;

public record AssertRequest(
    string name,
    bool condition);

public record AssertResponse();

public class AssertResourceHandler : IResourceHandler
{
    public string ResourceType => "Assert";

    public Task<ExtensibilityOperationResponse> Delete(ExtensibilityOperationRequest request, CancellationToken cancellationToken)
        => throw new NotImplementedException();

    public Task<ExtensibilityOperationResponse> Get(ExtensibilityOperationRequest request, CancellationToken cancellationToken)
        => throw new NotImplementedException();

    public Task<ExtensibilityOperationResponse> PreviewSave(ExtensibilityOperationRequest request, CancellationToken cancellationToken)
        => throw new NotImplementedException();

    public async Task<ExtensibilityOperationResponse> Save(ExtensibilityOperationRequest request, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        var body = JsonSerializer.Deserialize(JsonConvert.SerializeObject(request.Resource.Properties), SerializationContext.Default.AssertRequest)
            ?? throw new InvalidOperationException("Failed to deserialize request body");

        if (!body.condition)
        {
            return new ExtensibilityOperationResponse(
                null,
                null,
                [new("AssertionFailed", $"Assertion '{body.name}' failed!", "")]);
        }

        return new ExtensibilityOperationResponse(
            new ExtensibleResourceData(request.Resource.Type, new JObject()),
            null,
            null);
    }
}
