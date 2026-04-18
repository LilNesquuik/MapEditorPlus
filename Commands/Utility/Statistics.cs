using System.Text;
using AdminToys;
using CommandSystem;
using LabApi.Features.Permissions;
using LabApi.Features.Wrappers;
using NorthwoodLib.Pools;
using ProjectMER.Features.Objects;
using UnityEngine;
using UnityEngine.Pool;
using Object = UnityEngine.Object;

namespace ProjectMER.Commands.Utility;

public class Statistics : ICommand
{
    /// <inheritdoc/>
    public string Command => "statistics";

    /// <inheritdoc/>
    public string[] Aliases { get; } = [ "stats", "sts" ];

    /// <inheritdoc/>
    public string Description => "Gives statistics about schematics spawned.";

    public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
    {
        if (!sender.HasAnyPermission($"mpr.{Command}"))
        {
            response = $"You don't have permission to execute this command. Required permission: mpr.{Command}";
            return false;
        }

        StringBuilder stringBuilder = StringBuilderPool.Shared.Rent();
        stringBuilder.AppendLine("Schematics:");
        
        Dictionary<Type, int> entries = new();
        Dictionary<Type, int> staticEntries = new();
        
        foreach (SchematicObject schematic in Object.FindObjectsOfType<SchematicObject>())
        {
            entries.Clear();
            staticEntries.Clear();
            
            stringBuilder.Append("<b>");
            stringBuilder.Append(schematic.Name);
            stringBuilder.Append("</b>");
            
            foreach (AdminToyBase adminToy in schematic.AdminToyBases)
            {
                Type adminToyType = adminToy.GetType();
                
                entries.TryGetValue(adminToyType, out int count);
                entries[adminToyType] = count + 1;

                staticEntries.TryGetValue(adminToyType, out int staticCount);
                if (adminToy.IsStatic)
                    staticEntries[adminToyType] = staticCount + 1;
            }

            foreach (KeyValuePair<Type, int> entry in entries)
            {
                stringBuilder.Append("└─<b>");
                stringBuilder.Append(entry.Key.Name);
                stringBuilder.Append(":</b> <u>");
                stringBuilder.Append(entry.Value);
                stringBuilder.Append("</u>");
                stringBuilder.Append(" (Static: ");
                stringBuilder.Append(staticEntries.TryGetValue(entry.Key, out int count) ? count : 0);
                stringBuilder.AppendLine(")");
            }
            
            stringBuilder.AppendLine();
        }

        response = StringBuilderPool.Shared.ToStringReturn(stringBuilder);
        return true;
    }
}