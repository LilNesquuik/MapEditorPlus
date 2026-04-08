using ProjectMER.Events.Arguments.Interfaces;
using ProjectMER.Features.Objects;
using ProjectMER.Features.Serializable.Schematics;
using UnityEngine;

namespace ProjectMER.Events.Arguments;

public sealed class ScriptBlockSpawnedEventArgs(
    SerializableScript script,
    Transform transform,
    SchematicObject schematic)
    : EventArgs, ISchematicEvent
{
    public SerializableScript Script { get; } = script;
    public Transform Transform { get; } = transform;
    public SchematicObject Schematic { get; } = schematic;
    
    /// <summary>
    /// Get the name of the spawned script
    /// </summary>
    public string Name => Script.ScriptName;
    
    /// <summary>
    /// Get the properties of the spawned script as a <see cref="Dictionary{TKey,TValue}"/>
    /// </summary>
    public Dictionary<string, string> Properties => Script.Properties;
}