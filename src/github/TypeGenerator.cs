// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text;
using Azure.Bicep.Types;
using Azure.Bicep.Types.Concrete;
using Azure.Bicep.Types.Index;
using Azure.Bicep.Types.Serialization;

namespace Bicep.Local.Extension.Github;

public static class TypeGenerator
{
    public static string GetString(Action<Stream> streamWriteFunc)
    {
        using var memoryStream = new MemoryStream();
        streamWriteFunc(memoryStream);

        return Encoding.UTF8.GetString(memoryStream.ToArray());
    }

    public static Dictionary<string, string> GenerateTypes()
    {
        var factory = new TypeFactory([]);

        var secureStringType = factory.Create(() => new StringType(sensitive: true));
        var stringType = factory.Create(() => new StringType());

        var repositoryBodyType = factory.Create(() => new ObjectType("body", new Dictionary<string, ObjectTypeProperty>
        {
            ["owner"] = new(factory.GetReference(stringType), ObjectTypePropertyFlags.Required | ObjectTypePropertyFlags.Identifier, null),
            ["name"] = new(factory.GetReference(stringType), ObjectTypePropertyFlags.Required | ObjectTypePropertyFlags.Identifier, null),
        }, null));

        var repositoryType = factory.Create(() => new ResourceType(
            "Repository",
            ScopeType.Unknown,
            null,
            factory.GetReference(repositoryBodyType),
            ResourceFlags.None,
            null));

        var collaboratorBodyType = factory.Create(() => new ObjectType("body", new Dictionary<string, ObjectTypeProperty>
        {
            ["owner"] = new(factory.GetReference(stringType), ObjectTypePropertyFlags.Required | ObjectTypePropertyFlags.Identifier, null),
            ["name"] = new(factory.GetReference(stringType), ObjectTypePropertyFlags.Required | ObjectTypePropertyFlags.Identifier, null),
            ["user"] = new(factory.GetReference(stringType), ObjectTypePropertyFlags.Required | ObjectTypePropertyFlags.Identifier, null),
        }, null));

        var collaboratorType = factory.Create(() => new ResourceType(
            "Collaborator",
            ScopeType.Unknown,
            null,
            factory.GetReference(collaboratorBodyType),
            ResourceFlags.None,
            null));

        var configurationType = factory.Create(() => new ObjectType("configuration", new Dictionary<string, ObjectTypeProperty>
        {
            ["token"] = new(factory.GetReference(secureStringType), ObjectTypePropertyFlags.Required, null),
        }, null));

        var settings = new TypeSettings(
            name: "Github",
            version: "0.0.1",
            isSingleton: true,
            configurationType: new CrossFileTypeReference("types.json", factory.GetIndex(configurationType)));

        var resourceTypes = new[] {
            repositoryType,
            collaboratorType,
        };

        var index = new TypeIndex(
            resourceTypes.ToDictionary(x => x.Name, x => new CrossFileTypeReference("types.json", factory.GetIndex(x))),
            new Dictionary<string, IReadOnlyDictionary<string, IReadOnlyList<CrossFileTypeReference>>>(),
            settings,
            null);

        return new Dictionary<string, string>{
            ["index.json"] = GetString(stream => TypeSerializer.SerializeIndex(stream, index)),
            ["types.json"] = GetString(stream => TypeSerializer.Serialize(stream, factory.GetTypes())),
        };
    }
}