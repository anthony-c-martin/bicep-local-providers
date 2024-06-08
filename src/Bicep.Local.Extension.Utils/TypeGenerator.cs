// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text;
using Azure.Bicep.Types;
using Azure.Bicep.Types.Concrete;
using Azure.Bicep.Types.Index;
using Azure.Bicep.Types.Serialization;

namespace Bicep.Local.Extension.Utils;

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

        var stringType = factory.Create(() => new StringType());
        var intType = factory.Create(() => new IntegerType());
        var boolType = factory.Create(() => new BooleanType());

        var bashOptions = factory.Create(() => new ObjectType("BashScript", new Dictionary<string, ObjectTypeProperty>
        {
            ["type"] = new(factory.GetReference(factory.Create(() => new StringLiteralType("bash"))), ObjectTypePropertyFlags.None, null),
        }, null));

        var pwshOptions = factory.Create(() => new ObjectType("PowerShellScript", new Dictionary<string, ObjectTypeProperty>
        {
            ["type"] = new(factory.GetReference(factory.Create(() => new StringLiteralType("powershell"))), ObjectTypePropertyFlags.None, null),
        }, null));

        var scriptBodyType = factory.Create(() => new DiscriminatedObjectType("body", "type", new Dictionary<string, ObjectTypeProperty>
        {
            ["script"] = new(factory.GetReference(stringType), ObjectTypePropertyFlags.WriteOnly | ObjectTypePropertyFlags.Required, null),
            ["exitCode"] = new(factory.GetReference(intType), ObjectTypePropertyFlags.ReadOnly, null),
            ["stdout"] = new(factory.GetReference(stringType), ObjectTypePropertyFlags.ReadOnly, null),
            ["stderr"] = new(factory.GetReference(stringType), ObjectTypePropertyFlags.ReadOnly, null),
        }, new Dictionary<string, ITypeReference> {
            ["bash"] = factory.GetReference(bashOptions),
            ["powershell"] = factory.GetReference(pwshOptions),
        }));

        var scriptType = factory.Create(() => new ResourceType(
            "Script",
            ScopeType.Unknown,
            null,
            factory.GetReference(scriptBodyType),
            ResourceFlags.None,
            null));

        var waitBodyType = factory.Create(() => new ObjectType("body", new Dictionary<string, ObjectTypeProperty>
        {
            ["durationMs"] = new(factory.GetReference(intType), ObjectTypePropertyFlags.WriteOnly, null),
        }, null));

        var waitType = factory.Create(() => new ResourceType(
            "Wait",
            ScopeType.Unknown,
            null,
            factory.GetReference(waitBodyType),
            ResourceFlags.None,
            null));

        var assertBodyType = factory.Create(() => new ObjectType("body", new Dictionary<string, ObjectTypeProperty>
        {
            ["name"] = new(factory.GetReference(stringType), ObjectTypePropertyFlags.WriteOnly, null),
            ["condition"] = new(factory.GetReference(boolType), ObjectTypePropertyFlags.WriteOnly, null),
        }, null));

        var assertType = factory.Create(() => new ResourceType(
            "Assert",
            ScopeType.Unknown,
            null,
            factory.GetReference(assertBodyType),
            ResourceFlags.None,
            null));

        var settings = new TypeSettings(name: "Utils", version: "0.0.1", isSingleton: false, configurationType: null!);

        var resourceTypes = new[] {
            scriptType,
            waitType,
            assertType,
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