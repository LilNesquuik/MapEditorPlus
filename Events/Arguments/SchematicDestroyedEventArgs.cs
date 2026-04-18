using ProjectMER.Events.Arguments.Interfaces;
using ProjectMER.Features.Objects;

namespace ProjectMER.Events.Arguments;

/// <summary>
/// Fired when a schematic is destroyed.
/// </summary>
public class SchematicDestroyedEventArgs : EventArgs, ISchematicEvent
{
	public SchematicDestroyedEventArgs(SchematicObject schematic, string name)
	{
		Schematic = schematic;
		Name = name;
	}

	public SchematicObject Schematic { get; }

	public string Name { get; }
}
