// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using Bicep.Extension.Github.Handlers;
using Bicep.Extension.Protocol;

namespace Bicep.Extension.Github;

public static class Program
{
    public static Task Main(string[] args)
        => ProviderExtension.Run(RegisterHandlers, args);

    public static void RegisterHandlers(ResourceDispatcherBuilder builder) => builder
        .AddHandler(new CollaboratorResourceHandler())
        .AddHandler(new RepositoryResourceHandler());
}