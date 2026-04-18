using ProjectMER.Features.Objects;

namespace ProjectMER.Events.Arguments.Interfaces;

public interface ISchematicEvent
{
	/// <summary>
	/// Gets the schematic associated with this event.
	/// </summary>
	public SchematicObject Schematic { get; }
}
