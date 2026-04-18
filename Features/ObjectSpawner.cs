using AdminToys;
using ProjectMER.Features.Objects;
using ProjectMER.Features.Serializable;
using ProjectMER.Features.Serializable.Schematics;
using UnityEngine;

namespace ProjectMER.Features;

public static class ObjectSpawner
{
	public static PrimitiveObjectToy SpawnPrimitive(SerializablePrimitive serializablePrimitive)
	{
		GameObject gameObject = serializablePrimitive.SpawnOrUpdateObject();
		return gameObject.GetComponent<PrimitiveObjectToy>();
	}

	/// <summary>
	/// Spawns a schematic from the provided serializable schematic.
	/// </summary>
	/// <param name="serializableSchematic">The serializable schematic to spawn.</param>
	/// <returns>The spawned schematic object.</returns>
	public static SchematicObject SpawnSchematic(SerializableSchematic serializableSchematic)
	{
		GameObject? gameObject = serializableSchematic.SpawnOrUpdateObject();
		return gameObject == null ? null! : gameObject.GetComponent<SchematicObject>();
	}

	/// <summary>
	/// Spawns a schematic at the specified position.
	/// </summary>
	/// <param name="schematicName">The name of the schematic to spawn.</param>
	/// <param name="position">The position to spawn the schematic at.</param>
	/// <returns>The spawned schematic object.</returns>
	public static SchematicObject SpawnSchematic(string schematicName, Vector3 position) =>
		SpawnSchematic(new SerializableSchematic { SchematicName = schematicName, Position = position });

	/// <summary>
	/// Spawns a schematic at the specified position.
	/// </summary>
	/// <param name="schematicName">The name of the schematic to spawn.</param>
	/// <param name="position">The position to spawn the schematic at.</param>
	/// <param name="rotation">The rotation to apply to the schematic.</param>
	/// <returns>The spawned schematic object.</returns>
	public static SchematicObject SpawnSchematic(string schematicName, Vector3 position, Quaternion rotation) =>
		SpawnSchematic(new SerializableSchematic { SchematicName = schematicName, Position = position, Rotation = rotation.eulerAngles });

	/// <summary>
	/// Spawns a schematic at the specified position.
	/// </summary>
	/// <param name="schematicName">The name of the schematic to spawn.</param>
	/// <param name="position">The position to spawn the schematic at.</param>
	/// <param name="eulerAngles">The rotation to apply to the schematic.</param>
	/// <returns>The spawned schematic object.</returns>
	public static SchematicObject SpawnSchematic(string schematicName, Vector3 position, Vector3 eulerAngles) =>
		SpawnSchematic(new SerializableSchematic { SchematicName = schematicName, Position = position, Rotation = eulerAngles });

	/// <summary>
	/// Spawns a schematic at the specified position.
	/// </summary>
	/// <param name="schematicName">The name of the schematic to spawn.</param>
	/// <param name="position">The position to spawn the schematic at.</param>
	/// <param name="rotation">The rotation to apply to the schematic.</param>
	/// <param name="scale">The scale to apply to the schematic.</param>
	/// <returns>The spawned schematic object.</returns>
	public static SchematicObject SpawnSchematic(string schematicName, Vector3 position, Quaternion rotation, Vector3 scale) =>
		SpawnSchematic(new SerializableSchematic { SchematicName = schematicName, Position = position, Rotation = rotation.eulerAngles, Scale = scale });

	/// <summary>
	/// Spawns a schematic at the specified position.
	/// </summary>
	/// <param name="schematicName">The name of the schematic to spawn.</param>
	/// <param name="position">The position to spawn the schematic at.</param>
	/// <param name="eulerAngles">The rotation to apply to the schematic.</param>
	/// <param name="scale">The scale to apply to the schematic.</param>
	/// <returns>The spawned schematic object.</returns>
	public static SchematicObject SpawnSchematic(string schematicName, Vector3 position, Vector3 eulerAngles, Vector3 scale) =>
		SpawnSchematic(new SerializableSchematic { SchematicName = schematicName, Position = position, Rotation = eulerAngles, Scale = scale });

	/// <summary>
	/// Attempts to spawn a schematic from the provided serializable schematic.
	/// </summary>
	/// <param name="serializableSchematic">The serializable schematic to spawn.</param>
	/// <param name="schematic">When this method returns, contains the spawned schematic object if successful; otherwise, <c>null</c>.</param>
	/// <returns><c>true</c> if the schematic was spawned successfully; otherwise, <c>false</c>.</returns>
	public static bool TrySpawnSchematic(SerializableSchematic serializableSchematic, out SchematicObject schematic)
	{
		try
		{
			schematic = SpawnSchematic(serializableSchematic);
			return schematic != null;
		}
		catch (Exception)
		{
			schematic = null!;
			return false;
		}
	}
	
	/// <summary>
	/// Attempts to spawn a schematic at the specified position.
	/// </summary>
	/// <param name="schematicName">The name of the schematic to spawn.</param>
	/// <param name="position">The position to spawn the schematic at.</param>
	/// <param name="schematic">When this method returns, contains the spawned schematic object if successful; otherwise, <c>null</c>.</param>
	/// <returns><c>true</c> if the schematic was spawned successfully; otherwise, <c>false</c>.</returns>
	public static bool TrySpawnSchematic(string schematicName, Vector3 position, out SchematicObject schematic) =>
		TrySpawnSchematic(new SerializableSchematic { SchematicName = schematicName, Position = position }, out schematic);

	/// <summary>
	/// Attempts to spawn a schematic at the specified position with the given rotation.
	/// </summary>
	/// <param name="schematicName">The name of the schematic to spawn.</param>
	/// <param name="position">The position to spawn the schematic at.</param>
	/// <param name="rotation">The rotation to apply to the schematic.</param>
	/// <param name="schematic">When this method returns, contains the spawned schematic object if successful; otherwise, <c>null</c>.</param>
	/// <returns><c>true</c> if the schematic was spawned successfully; otherwise, <c>false</c>.</returns>
	public static bool TrySpawnSchematic(string schematicName, Vector3 position, Quaternion rotation, out SchematicObject schematic) =>
		TrySpawnSchematic(new SerializableSchematic { SchematicName = schematicName, Position = position, Rotation = rotation.eulerAngles }, out schematic);

	/// <summary>
	/// Attempts to spawn a schematic at the specified position with the given rotation.
	/// </summary>
	/// <param name="schematicName">The name of the schematic to spawn.</param>
	/// <param name="position">The position to spawn the schematic at.</param>
	/// <param name="eulerAngles">The rotation to apply to the schematic.</param>
	/// <param name="schematic">When this method returns, contains the spawned schematic object if successful; otherwise, <c>null</c>.</param>
	/// <returns><c>true</c> if the schematic was spawned successfully; otherwise, <c>false</c>.</returns>
	public static bool TrySpawnSchematic(string schematicName, Vector3 position, Vector3 eulerAngles, out SchematicObject schematic) =>
		TrySpawnSchematic(new SerializableSchematic { SchematicName = schematicName, Position = position, Rotation = eulerAngles }, out schematic);

	/// <summary>
	/// Attempts to spawn a schematic at the specified position with the given rotation and scale.
	/// </summary>
	/// <param name="schematicName">The name of the schematic to spawn.</param>
	/// <param name="position">The position to spawn the schematic at.</param>
	/// <param name="rotation">The rotation to apply to the schematic.</param>
	/// <param name="scale">The scale to apply to the schematic.</param>
	/// <param name="schematic">When this method returns, contains the spawned schematic object if successful; otherwise, <c>null</c>.</param>
	/// <returns><c>true</c> if the schematic was spawned successfully; otherwise, <c>false</c>.</returns>
	public static bool TrySpawnSchematic(string schematicName, Vector3 position, Quaternion rotation, Vector3 scale, out SchematicObject schematic) =>
		TrySpawnSchematic(new SerializableSchematic { SchematicName = schematicName, Position = position, Rotation = rotation.eulerAngles, Scale = scale }, out schematic);

	/// <summary>
	/// Attempts to spawn a schematic at the specified position with the given rotation and scale.
	/// </summary>
	/// <param name="schematicName">The name of the schematic to spawn.</param>
	/// <param name="position">The position to spawn the schematic at.</param>
	/// <param name="eulerAngles">The rotation to apply to the schematic.</param>
	/// <param name="scale">The scale to apply to the schematic.</param>
	/// <param name="schematic">When this method returns, contains the spawned schematic object if successful; otherwise, <c>null</c>.</param>
	/// <returns><c>true</c> if the schematic was spawned successfully; otherwise, <c>false</c>.</returns>
	public static bool TrySpawnSchematic(string schematicName, Vector3 position, Vector3 eulerAngles, Vector3 scale, out SchematicObject schematic) =>
		TrySpawnSchematic(new SerializableSchematic { SchematicName = schematicName, Position = position, Rotation = eulerAngles, Scale = scale }, out schematic);
}
