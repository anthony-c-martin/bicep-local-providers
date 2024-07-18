// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.
using System.Diagnostics;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using Bicep.Local.Extension.Protocol;

namespace Bicep.Local.Extension.Utils.Handlers;

[JsonConverter(typeof(JsonStringEnumConverter<ScriptType>))]
public enum ScriptType
{
    Bash,
    PowerShell,
}

public record RunScriptRequest(
    ScriptType type,
    string script);

public record RunScriptResponse(
    int exitCode,
    string stdout,
    string stderr);

public class ScriptResourceHandler : IResourceHandler
{
    public string ResourceType => "Script";

    public Task<LocalExtensibilityOperationResponse> Delete(ResourceReference request, CancellationToken cancellationToken)
        => throw new NotImplementedException();

    public Task<LocalExtensibilityOperationResponse> Get(ResourceReference request, CancellationToken cancellationToken)
        => throw new NotImplementedException();

    public Task<LocalExtensibilityOperationResponse> Preview(ResourceSpecification request, CancellationToken cancellationToken)
        => throw new NotImplementedException();

    public async Task<LocalExtensibilityOperationResponse> CreateOrUpdate(ResourceSpecification request, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        var body = JsonSerializer.Deserialize(request.Properties, SerializationContext.Default.RunScriptRequest)
            ?? throw new InvalidOperationException("Failed to deserialize request body");
        
        var scriptOutput = body.type switch {
            ScriptType.Bash => RunBashScript(body.script),
            ScriptType.PowerShell => RunPowerShellScript(body.script),
            _ => throw new InvalidOperationException($"Unknown script type '{body.type}'"),
        };

        var responseBody = JsonSerializer.SerializeToNode(scriptOutput, SerializationContext.Default.RunScriptResponse) as JsonObject;

        return new(
            new(request.Type, request.ApiVersion, "Succeeded", new(), request.Config, responseBody!),
            null);
    }

    private static RunScriptResponse RunBashScript(string script)
    {
        var proc = new Process
        {
            StartInfo = new ProcessStartInfo
            {
                FileName = "bash",
                Arguments = $"-c \"{script.Replace("\"", "\\\"")}\"",
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                StandardOutputEncoding = Encoding.UTF8,
                StandardErrorEncoding = Encoding.UTF8,
                CreateNoWindow = true,
            }
        };

        proc.Start();
        var stdout = proc.StandardOutput.ReadToEnd();
        var stderr = proc.StandardError.ReadToEnd();
        proc.WaitForExit();

        return new(proc.ExitCode, stdout, stderr);
    }

    private static RunScriptResponse RunPowerShellScript(string script)
    {
        var proc = new Process
        {
            StartInfo = new ProcessStartInfo
            {
                FileName = "pwsh",
                Arguments = $"-c \"{script.Replace("\"", "\\\"")}\"",
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                StandardOutputEncoding = Encoding.UTF8,
                StandardErrorEncoding = Encoding.UTF8,
                CreateNoWindow = true,
            }
        };

        proc.Start();
        var stdout = proc.StandardOutput.ReadToEnd();
        var stderr = proc.StandardError.ReadToEnd();
        proc.WaitForExit();

        return new(proc.ExitCode, stdout, stderr);
    }
}
