using CommandSystem;
using LabApi.Features.Permissions;
using LabApi.Features.Wrappers;

namespace ProjectMER.Commands.Utility;

public class Statistics : ICommand
{
    /// <inheritdoc/>
    public string Command => "statistics";

    /// <inheritdoc/>
    public string[] Aliases { get; } = [ "stats", "sts" ];

    /// <inheritdoc/>
    public string Description => "Gives statistics about the admintoys spawned.";

    public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
    {
        if (!sender.HasAnyPermission($"mpr.{Command}"))
        {
            response = $"You don't have permission to execute this command. Required permission: mpr.{Command}";
            return false;
        }

        int adminToyCount = AdminToy.List.Count;
        int staticToyCount = AdminToy.List.Count(x => x.IsStatic);
        int diff = adminToyCount - staticToyCount;

        response = $"\nAdmin toys: {adminToyCount}\nStatic toys: {staticToyCount}\nDiff: {diff}";
        return true;
    }
}