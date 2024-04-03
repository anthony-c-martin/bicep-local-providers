// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using Bicep.Extension.Utils.Handlers;

namespace Bicep.Extension.Utils;

public class Program
{
    public static Task Main(string[] args)
        => ProviderExtension.Run(RegisterHandlers, args);

    public static void RegisterHandlers(ResourceDispatcherBuilder builder) => builder
        .AddHandler(new AssertResourceHandler())
        .AddHandler(new ScriptResourceHandler())
        .AddHandler(new WaitResourceHandler());
}