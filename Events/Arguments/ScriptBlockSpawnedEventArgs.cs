using ProjectMER.Events.Arguments.Interfaces;
using ProjectMER.Features.Objects;
using ProjectMER.Features.Serializable.Schematics;
using UnityEngine;

namespace ProjectMER.Events.Arguments;

public sealed class ScriptBlockSpawnedEventArgs : EventArgs, ISchematicEvent
{
    public ScriptBlockSpawnedEventArgs(SerializableScript script, Transform transform, SchematicObject schematic)
    {
        Script = script;
        Transform = transform;
        Schematic = schematic;
    }

    public SerializableScript Script { get; set; }

    public Transform Transform { get; set; }
    
    public SchematicObject Schematic { get; }
}