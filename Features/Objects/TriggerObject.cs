using CustomPlayerEffects;
using LabApi.Features.Wrappers;
using ProjectMER.Events.Arguments;
using ProjectMER.Events.Handlers;
using ProjectMER.Features.Enums;
using UnityEngine;

namespace ProjectMER.Features.Objects;

public sealed class TriggerObject : MonoBehaviour
{
    public TriggerType triggerType = TriggerType.OnEnter;
    public string? effectName;
    public float duration;
    public byte intensity;
    public bool addDuration;

    private BoxCollider _collider;
    
    public void Start()
    {
        _collider = gameObject.AddComponent<BoxCollider>();
        _collider.isTrigger = true;
        _collider.size = Vector3.one;
        _collider.center = Vector3.zero;
    }

    public void OnDestroy()
    {
        Destroy(_collider);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (triggerType is not TriggerType.OnEnter) 
            return;
        
        Player? player = Player.Get(other.transform.root.gameObject);
        if (player != null)
            OnTriggered(player);
    }

    private void OnTriggerStay(Collider other)
    {
        if (triggerType is not TriggerType.OnStay) 
            return;
    
        Player? player = Player.Get(other.transform.root.gameObject);
        if (player != null)
            OnTriggered(player);
    }

    private void OnTriggerExit(Collider other)
    {
        if (triggerType is not TriggerType.OnExit) 
            return;

        Player? player = Player.Get(other.transform.root.gameObject);
        if (player != null)
            OnTriggered(player);
    }

    private void OnTriggered(Player player)
    {
        Schematic.OnPlayerTrigger(new PlayerTriggerEventArgs(player, this));
        
        if (effectName is null)
            return;

        if (player.TryGetEffect(effectName, out StatusEffectBase? statusEffectBase))
            player.EnableEffect(statusEffectBase, intensity, duration, addDuration);
    }
}