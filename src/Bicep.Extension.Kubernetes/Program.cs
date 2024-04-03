// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.
using Bicep.Extension.Kubernetes.Handlers;

namespace Bicep.Extension.Kubernetes;

public class Program
{
    public static async Task Main(string[] args)
        => await ProviderExtension.Run(RegisterHandlers, args);

    public static void RegisterHandlers(ResourceDispatcherBuilder builder) => builder
        .AddGenericHandler(new KubernetesResourceHandler());
}