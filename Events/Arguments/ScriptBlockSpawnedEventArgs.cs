using ProjectMER.Events.Arguments.Interfaces;
using ProjectMER.Features.Objects;
using ProjectMER.Features.Serializable.Schematics;
using UnityEngine;

namespace ProjectMER.Events.Arguments;

public sealed class ScriptBlockSpawnedEventArgs : EventArgs, ISchematicEvent
{
    public ScriptBlockSpawnedEventArgs(SerializableScript script, GameObject gameObject, SchematicObject schematic)
    {
        Script = script;
        GameObject = gameObject;
        Schematic = schematic;
    }

    public SerializableScript Script { get; set; }

    public GameObject GameObject { get; set; }
    
    public SchematicObject Schematic { get; }
}