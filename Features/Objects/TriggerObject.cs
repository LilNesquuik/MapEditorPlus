using CustomPlayerEffects;
using LabApi.Features.Wrappers;
using ProjectMER.Features.Enums;
using UnityEngine;

namespace ProjectMER.Features.Objects;

public class TriggerObject : MonoBehaviour
{
    public TriggerType triggerType = TriggerType.OnEnter;
    public string effectName = nameof(PitDeath);
    public float duration;
    public byte intensity;
    public bool addDuration;

    public event Action<Player> OnTrigger;
    
    private BoxCollider _collider;
    private CachedLayerMask _detectionMask;

    private void Awake()
    {
        _detectionMask = new CachedLayerMask("Player");
        
        _collider = gameObject.AddComponent<BoxCollider>();
        _collider.isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer != _detectionMask) 
            return;
    
        if (triggerType is not TriggerType.OnEnter) 
            return;
    
        Player? player = Player.Get(other.gameObject);
        if (player != null)
            ServerEnableEffect(player);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer != _detectionMask) 
            return;
        
        if (triggerType is not TriggerType.OnStay) 
            return;
    
        Player? player = Player.Get(other.gameObject);
        if (player != null)
            ServerEnableEffect(player);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer != _detectionMask) 
            return;
        
        if (triggerType is not TriggerType.OnExit) 
            return;
    
        Player? player = Player.Get(other.gameObject);
        if (player != null)
            ServerEnableEffect(player);
    }

    private void ServerEnableEffect(Player player)
    {
        OnTrigger(player);

        if (player.TryGetEffect(effectName, out StatusEffectBase? statusEffectBase))
            player.EnableEffect(statusEffectBase, intensity, duration, addDuration);
    }
}