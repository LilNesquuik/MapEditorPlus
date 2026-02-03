namespace ProjectMER.Features.Serializable.Schematics;

public class SerializableScript
{
    public string ScriptId { get; set; }
    public IReadOnlyDictionary<string, string> Properties { get; set; }
}