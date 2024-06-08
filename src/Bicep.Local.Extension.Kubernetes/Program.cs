// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using Bicep.Local.Extension.Kubernetes.Handlers;
using Bicep.Local.Extension.Protocol;
using Bicep.Local.Extension.Rpc;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.DependencyInjection;

namespace Bicep.Local.Extension.Kubernetes;

public class Program
{
    public static async Task Main(string[] args)
    {
        var extension = new KestrelProviderExtension();

        await ProviderExtension.Run(new KestrelProviderExtension(), RegisterHandlers, args);
    }

    public static void RegisterHandlers(ResourceDispatcherBuilder builder) => builder
        .AddGenericHandler(new KubernetesResourceHandler());
}

public class KestrelProviderExtension : ProviderExtension
{
    protected override async Task RunServer(string socketPath, ResourceDispatcher dispatcher, CancellationToken cancellationToken)
    {
        var builder = WebApplication.CreateBuilder();
        builder.WebHost.ConfigureKestrel(options =>
        {
            options.ListenUnixSocket(socketPath, listenOptions =>
            {
                listenOptions.Protocols = HttpProtocols.Http2;
            });
        });

        builder.Services.AddGrpc();
        builder.Services.AddSingleton(dispatcher);
        var app = builder.Build();
        app.MapGrpcService<BicepExtensionImpl>();

        await app.RunAsync();
    }
}