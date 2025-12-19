using AdminToys;
using LabApi.Features.Wrappers;
using Mirror;
using ProjectMER.Features.Extensions;
using ProjectMER.Features.Interfaces;
using UnityEngine;
using PrimitiveObjectToy = AdminToys.PrimitiveObjectToy;
using WaypointToy = AdminToys.WaypointToy;

namespace ProjectMER.Features.Serializable;

public class SerializableWaypoint : SerializableObject, IIndicatorDefinition
{
	public const float ScaleMultiplier = 1 / 256f;
	
	public Vector3 Bounds { get; set; } = Vector3.one;
	public float Priority { get; set; } = 0;

	public override GameObject? SpawnOrUpdateObject(Room? room = null, GameObject? instance = null)
	{
		WaypointToy waypoint = instance == null ? GameObject.Instantiate(PrefabManager.Waypoint) : instance.GetComponent<WaypointToy>();
		Vector3 position = room.GetAbsolutePosition(Position);
		Quaternion rotation = room.GetAbsoluteRotation(Rotation);
		_prevIndex = Index;

		waypoint.transform.SetPositionAndRotation(position, rotation);
		waypoint.transform.localScale = Scale;
		
		waypoint.NetworkBoundsSize = Bounds;
		waypoint.NetworkPriority = Priority;
		waypoint.NetworkMovementSmoothing = 60;
		waypoint.NetworkVisualizeBounds = true;

		if (instance == null)
			NetworkServer.Spawn(waypoint.gameObject);

		return waypoint.gameObject;
	}

	public GameObject SpawnOrUpdateIndicator(Room room, GameObject? instance = null)
	{
		PrimitiveObjectToy primitive = instance == null ? UnityEngine.Object.Instantiate(PrefabManager.PrimitiveObject) : instance.GetComponent<PrimitiveObjectToy>();
		Vector3 position = room.GetAbsolutePosition(Position);
		Quaternion rotation = room.GetAbsoluteRotation(Rotation);

		primitive.NetworkPrimitiveFlags = PrimitiveFlags.None;
		primitive.NetworkPrimitiveType = PrimitiveType.Cube;
		primitive.transform.localScale = Scale;
		primitive.transform.SetPositionAndRotation(position, rotation);

		return primitive.gameObject;
	}
}
