using CustomPlayerEffects;
using LabApi.Features.Wrappers;
using Mirror;
using ProjectMER.Features.Enums;
using ProjectMER.Features.Serializable;
using UnityEngine;

namespace ProjectMER.Features.Objects;

public class TriggerObject : MonoBehaviour
{
    private const float Interval = 0.1f;
    
    public TriggerType triggerType = TriggerType.OnEnter;
    public string effectName = nameof(PitDeath);
    public float duration;
    public byte intensity;
    public bool addDuration;

    public event Action<Player> OnTrigger;

    private Bounds _bounds;
    private HashSet<Player> _affectedPlayers;
    private float _nextCheck;
    
    private MapEditorObject _mapEditorObject;
    public SerializableTeleport Base;

    private void Awake()
    {
        _affectedPlayers = [];
        _bounds = new Bounds(transform.position, transform.lossyScale);
    }
    
    private void Start()
    {
        _mapEditorObject = GetComponent<MapEditorObject>();
        Base = (SerializableTeleport)_mapEditorObject.Base;
    }
    
    public void FixedUpdate()
    {
        if (Time.time < _nextCheck) 
            return;
        
        _nextCheck = Time.time + Interval;
        
        _bounds.center = transform.position;
        _bounds.size = transform.lossyScale;
        
        foreach (Player player in Player.List)
        {
            bool flag = _affectedPlayers.Contains(player);
            if (_bounds.Contains(player.Position))
            {
                if (!flag)
                {
                    _affectedPlayers.Add(player);
                    
                    if (triggerType is TriggerType.OnEnter)
                        ServerEnableEffect(player);
                }
                else
                    if (triggerType is TriggerType.OnStay)
                        ServerEnableEffect(player);
            }
            else if (flag)
            {
                _affectedPlayers.Remove(player);
                
                if (triggerType is TriggerType.OnExit)
                    ServerEnableEffect(player);
            }
        }
    }

    private void ServerEnableEffect(Player player)
    {
        OnTrigger(player);
        
        if (player.TryGetEffect(effectName, out StatusEffectBase? statusEffectBase))
            player.EnableEffect(statusEffectBase, intensity, duration, addDuration);
    }
}