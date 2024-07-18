// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.
using System.Text.Json;
using System.Text.Json.Nodes;
using Bicep.Local.Extension.Protocol;

namespace Bicep.Local.Extension.Utils.Handlers;

public record AssertRequest(
    string name,
    bool condition);

public record AssertResponse();

public class AssertResourceHandler : IResourceHandler
{
    public string ResourceType => "Assert";

    public Task<LocalExtensibilityOperationResponse> Delete(ResourceReference request, CancellationToken cancellationToken)
        => throw new NotImplementedException();

    public Task<LocalExtensibilityOperationResponse> Get(ResourceReference request, CancellationToken cancellationToken)
        => throw new NotImplementedException();

    public Task<LocalExtensibilityOperationResponse> Preview(ResourceSpecification request, CancellationToken cancellationToken)
        => throw new NotImplementedException();

    public async Task<LocalExtensibilityOperationResponse> CreateOrUpdate(ResourceSpecification request, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        var body = JsonSerializer.Deserialize(request.Properties, SerializationContext.Default.AssertRequest)
            ?? throw new InvalidOperationException("Failed to deserialize request body");

        if (!body.condition)
        {
            return new(
                null,
                new(new("AssertionFailed", "", $"Assertion '{body.name}' failed!", null, null)));
        }

        return new(
            new(request.Type, request.ApiVersion, "Succeeded", new(), request.Config, new()),
            null);
    }
}
