namespace ProjectMER.Features.Serializable.Schematics;

[Serializable]
public sealed class SerializableScript
{
    public string ScriptName { get; init; }
    public Dictionary<string, string> Properties { get; init; }
}