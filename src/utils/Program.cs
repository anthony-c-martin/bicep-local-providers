// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using Bicep.Local.Extension;
using Bicep.Local.Extension.Rpc;
using Bicep.Local.Extension.Protocol;
using Bicep.Local.Extension.Utils.Handlers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.DependencyInjection;

namespace Bicep.Local.Extension.Utils;

public class Program
{
    public static async Task Main(string[] args)
    {
        var extension = new KestrelProviderExtension();

        await ProviderExtension.Run(new KestrelProviderExtension(), RegisterHandlers, args);
    }

    public static void RegisterHandlers(ResourceDispatcherBuilder builder) => builder
        .AddHandler(new AssertResourceHandler())
        .AddHandler(new ScriptResourceHandler())
        .AddHandler(new WaitResourceHandler());
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