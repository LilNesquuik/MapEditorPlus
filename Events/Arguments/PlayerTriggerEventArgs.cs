using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using ProjectMER.Features.Objects;

namespace ProjectMER.Events.Arguments;

/// <summary>
/// Triggered when a player enters a <see cref="TriggerObject"/>
/// </summary>
public class PlayerTriggerEventArgs(Player player, TriggerObject trigger) : EventArgs, IPlayerEvent
{
    public Player Player { get; } = player;
    public TriggerObject Trigger { get; } = trigger;
}