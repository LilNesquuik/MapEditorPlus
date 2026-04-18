using Interactables.Interobjects.DoorUtils;
using LabApi.Features.Wrappers;
using MapGeneration.Distributors;
using Mirror;
using ProjectMER.Features.Extensions;
using UnityEngine;

namespace ProjectMER.Features.Serializable;

public sealed class SerializableGenerator : SerializableObject
{
	/// <summary>
	/// The time it takes to activate the generator.
	/// </summary>
	public float ActivationTime { get; set; } = -1;
	
	/// <summary>
	/// The time it takes to activate the generator.
	/// </summary>
	public float DeactivationTime { get; set; } = -1;
	
	/// <summary>
	/// Whether the generator is open by default.
	/// </summary>
	public bool IsOpen { get; set; } = false;
	
	/// <summary>
	/// Whether the generator is unlocked by default.
	/// </summary>
	public bool IsUnlocked { get; set; } = false;
	
	/// <summary>
	/// The permissions required to access unlock this generator.
	/// </summary>
	public DoorPermissionFlags RequiredPermissions { get; set; } = DoorPermissionFlags.ArmoryLevelTwo;

	public override GameObject SpawnOrUpdateObject(Room? room = null, GameObject? instance = null)
	{
		Scp079Generator scp079Generator = instance == null ? UnityEngine.Object.Instantiate(PrefabManager.Generator) : instance.GetComponent<Scp079Generator>();
		Vector3 position = room.GetAbsolutePosition(Position);
		Quaternion rotation = room.GetAbsoluteRotation(Rotation);
		_prevIndex = Index;
		
		Transform transform = scp079Generator.transform;
		transform.SetPositionAndRotation(position, rotation);
		transform.localScale = Scale;

		if (ActivationTime > 0)
			scp079Generator.TotalActivationTime = ActivationTime; 
		
		if (DeactivationTime > 0)
			scp079Generator.TotalDeactivationTime = DeactivationTime;

		scp079Generator.IsOpen = IsOpen;
		scp079Generator.IsUnlocked = IsUnlocked;
		scp079Generator.RequiredPermissions = RequiredPermissions; 
		
		scp079Generator.RegisterRoom();
            
		if (scp079Generator.TryGetComponent(out StructurePositionSync structurePositionSync))
		{
			transform.GetPositionAndRotation(out position, out rotation);
			
			structurePositionSync.Network_position = position;
			structurePositionSync.Network_rotationY = (sbyte)Mathf.RoundToInt(rotation.eulerAngles.y / 5.625f);
		}
		
		if (instance != null)
			NetworkServer.UnSpawn(scp079Generator.gameObject);

		NetworkServer.Spawn(scp079Generator.gameObject);

		return scp079Generator.gameObject;
	}
}
