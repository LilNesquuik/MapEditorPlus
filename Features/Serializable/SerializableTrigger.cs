using AdminToys;
using CustomPlayerEffects;
using Interactables.Interobjects.DoorUtils;
using LabApi.Features.Wrappers;
using Mirror;
using ProjectMER.Features.Enums;
using ProjectMER.Features.Extensions;
using ProjectMER.Features.Interfaces;
using ProjectMER.Features.Objects;
using UnityEngine;
using PrimitiveObjectToy = AdminToys.PrimitiveObjectToy;

namespace ProjectMER.Features.Serializable;

public class SerializableTrigger : SerializableObject, IIndicatorDefinition
{
	public TriggerType TriggerType { get; set; } = TriggerType.OnEnter;
	public string EffectName { get; set; } = nameof(PitDeath);
	
	public byte Intensity { get; set; } = byte.MaxValue;
	public float Duration { get; set; } = 1f;
	public bool AddDuration { get; set; } = false;

	public override GameObject SpawnOrUpdateObject(Room? room = null, GameObject? instance = null)
	{
		GameObject gameObject = instance ?? new GameObject("Trigger");
		Vector3 position = room.GetAbsolutePosition(Position);
		Quaternion rotation = room.GetAbsoluteRotation(Rotation);
		_prevIndex = Index;
		gameObject.transform.SetLocalPositionAndRotation(position, rotation);

		TriggerObject triggerObject = instance == null ? 
			gameObject.AddComponent<TriggerObject>() : instance.GetComponent<TriggerObject>();
		
		triggerObject.triggerType = TriggerType;
		triggerObject.effectName = EffectName;
		triggerObject.intensity = Intensity;
		triggerObject.duration = Duration;
		triggerObject.addDuration = AddDuration;
		
		return gameObject.gameObject;
	}

	public GameObject SpawnOrUpdateIndicator(Room room, GameObject? instance = null)
	{
		PrimitiveObjectToy root;
		
		Vector3 position = room.GetAbsolutePosition(Position);
		Quaternion rotation = room.GetAbsoluteRotation(Rotation);

		if (instance == null)
		{
			root = UnityEngine.Object.Instantiate(PrefabManager.PrimitiveObject);
			root.NetworkPrimitiveFlags = PrimitiveFlags.Visible;
			root.name = "Indicator";
			root.NetworkPrimitiveType = PrimitiveType.Cube;
			root.transform.localScale = Scale;
			root.transform.position = position;
			root.transform.position = position;
		}
		else
		{
			root = instance.GetComponent<PrimitiveObjectToy>();
		}
		
		root.transform.position = position;
		room.Transform.rotation = rotation;
		
		root.NetworkMaterialColor = new Color(1.0f, 0f, 0f, 0.5f);
		
		return root.gameObject;
	}
}
