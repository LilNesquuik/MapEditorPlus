using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using ProjectMER.Features.Objects;

namespace ProjectMER.Events.Arguments;

/// <summary>
/// Triggered when a player enters a <see cref="TriggerObject"/>
/// </summary>
public class PlayerTriggerEventArgs : EventArgs, IPlayerEvent
{
    public Player Player { get; }
    public TriggerObject Trigger { get; }
    public PlayerTriggerEventArgs(Player player, TriggerObject trigger)
    {
        Player = player;
        Trigger = trigger;
    }
}