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
    public string Description => "Gives statistics about the admintoys spawned.";

    public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
    {
        if (!sender.HasAnyPermission($"mpr.{Command}"))
        {
            response = $"You don't have permission to execute this command. Required permission: mpr.{Command}";
            return false;
        }

        StringBuilder stringBuilder = StringBuilderPool.Shared.Rent();
        stringBuilder.AppendLine("Schematics:");
        
        foreach (SchematicObject schematic in Object.FindObjectsOfType<SchematicObject>())
        {
            Dictionary<Type, int> dictionaryPool = DictionaryPool<Type, int>.Get();
            
            stringBuilder.AppendLine($"<b>{schematic.Name}</b>");
            foreach (AdminToyBase adminToy in schematic.AdminToyBases)
            {
                dictionaryPool.TryGetValue(adminToy.GetType(), out int count);
                dictionaryPool[adminToy.GetType()] = count + 1;
            }

            foreach (KeyValuePair<Type, int> entry in dictionaryPool)
                stringBuilder.AppendLine($"└─<b>{entry.Key.Name}:</b> <u>{entry.Value}</u>");
            
            stringBuilder.AppendLine();
            
            DictionaryPool<Type, int>.Release(dictionaryPool);
        }

        response = StringBuilderPool.Shared.ToStringReturn(stringBuilder);
        return true;
    }
}