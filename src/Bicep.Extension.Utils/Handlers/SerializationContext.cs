// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.
using System.Text.Json.Serialization;

namespace Bicep.Extension.Utils.Handlers;

[JsonSerializable(typeof(WaitRequest))]
[JsonSerializable(typeof(WaitResponse))]
[JsonSerializable(typeof(AssertRequest))]
[JsonSerializable(typeof(AssertResponse))]
[JsonSerializable(typeof(RunScriptRequest))]
[JsonSerializable(typeof(RunScriptResponse))]
[JsonSourceGenerationOptions(PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase)]
internal partial class SerializationContext : JsonSerializerContext { }
