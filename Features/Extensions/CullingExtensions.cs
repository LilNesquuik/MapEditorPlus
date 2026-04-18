using UnityEngine;
using Object = UnityEngine.Object;

namespace ProjectMER.Features.Extensions;

using LabApi.Features.Wrappers;
using Mirror;
using Objects;
using SpawnableCullingParent = AdminToys.SpawnableCullingParent;

/// <summary>
/// A set of useful extensions to easily interact with culling features.
/// </summary>
public static class CullingExtensions
{
	/// <summary>
	/// Spawns the given <paramref name="schematic"/> for the specified <paramref name="player"/>.
	/// </summary>
	/// <param name="player">The target.</param>
	/// <param name="schematic">The schematic to spawn.</param>
	public static void SpawnSchematic(this Player player, SchematicObject schematic)
	{
		foreach (NetworkIdentity networkIdentity in schematic.NetworkIdentities)
			player.SpawnNetworkIdentity(networkIdentity);
	}

	/// <summary>
	/// Destroys the given <paramref name="schematic"/> for the specified <paramref name="player"/>.
	/// </summary>
	/// <param name="player">The target.</param>
	/// <param name="schematic">The schematic to destroy.</param>
	public static void DestroySchematic(this Player player, SchematicObject schematic)
	{
		foreach (NetworkIdentity networkIdentity in schematic.NetworkIdentities)
			player.DestroyNetworkIdentity(networkIdentity);
	}

	/// <summary>
	/// Spawns the given <paramref name="networkIdentity"/> for the specified <paramref name="player"/>.
	/// </summary>
	/// <param name="player">The target.</param>
	/// <param name="networkIdentity">The network identity to spawn.</param>
	public static void SpawnNetworkIdentity(this Player player, NetworkIdentity networkIdentity) =>
		NetworkServer.SendSpawnMessage(networkIdentity, player.Connection);

	/// <summary>
	/// Destroys the given <paramref name="networkIdentity"/> for the specified <paramref name="player"/>.
	/// </summary>
	/// <param name="player">The target.</param>
	/// <param name="networkIdentity">The network identity to destroy.</param>
	public static void DestroyNetworkIdentity(this Player player, NetworkIdentity networkIdentity) =>
		player.Connection.Send(new ObjectDestroyMessage { netId = networkIdentity.netId });
	
	/// <summary>
	/// Wraps the specified <paramref name="schematic"/> inside a culling parent.
	/// </summary>
	/// <param name="schematic">The schematic to wrap.</param>
	/// <param name="center">The center position of the culling bounds.</param>
	/// <param name="size">The size of the culling bounds.</param>
	/// <param name="showDebug">If true, draws the debug bounds for the culling area.</param>
	/// <returns>The created <see cref="SpawnableCullingParent"/>.</returns>
	/// <remarks>
	/// Your schematic will be parented to the <see cref="SpawnableCullingParent">culling parent</see> but <see cref="SpawnableCullingParent"/> can't be moved or rotated.
	/// </remarks>
	public static SpawnableCullingParent WrapWithCullingParent(this SchematicObject schematic, 
		Vector3 center, 
		Vector3 size,
		bool showDebug = false)
	{
		SpawnableCullingParent cullingParent = Object.Instantiate(PrefabManager.CullingParent);
		cullingParent.name = "CullingParent-" + schematic.Name;
		cullingParent.NetworkBoundsPosition = center;
		cullingParent.NetworkBoundsSize = size;

		if (showDebug)
			cullingParent.DrawDebugBounds(Color.green, float.MaxValue);
		
		schematic.transform.SetParent(cullingParent.transform);
		return cullingParent;
	}
}