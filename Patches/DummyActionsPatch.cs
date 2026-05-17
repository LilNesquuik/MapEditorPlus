using AdminToys;
using DrawableLine;
using HarmonyLib;
using Interactables;
using Interactables.Verification;
using InventorySystem;
using InventorySystem.Searching;
using LabApi.Features.Wrappers;
using NetworkManagerUtils.Dummies;
using PlayerRoles.FirstPersonControl;
using UnityEngine;
using Utils.Networking;

namespace ProjectMER.Patches;

[HarmonyPatch(typeof(Inventory))]
internal static class DummyActionsPatch 
{
    [HarmonyPatch(nameof(Inventory.PopulateDummyActions))]
    [HarmonyPostfix]
    private static void Postfix(Action<DummyAction> actionAdder, Action<string> categoryAdder, Inventory __instance)
    {
        Vector3 hubPosition = __instance._hub.GetPosition();

        foreach (InteractableToy interactableToy in InteractableToy.List)
        {
            Vector3 interactPosition = interactableToy.Transform.position;

            if (Vector3.Distance(hubPosition, interactPosition) > StandardDistanceVerification.DefaultMaxDistance)
                continue;

            InvisibleInteractableToy baseToy = interactableToy.Base;

            categoryAdder($"{baseToy.name} (#{baseToy.netId})");
            actionAdder(new DummyAction("Mark", () =>
                new DrawableLineMessage(5, Color.blue, [interactPosition, interactPosition + Vector3.up * 15])
                    .SendToAuthenticated()));

            if (interactableToy.InteractionDuration > 0)
            {
                actionAdder(new DummyAction("Search", () =>
                {
                    ISearchCompletor searchCompletor = baseToy.GetSearchCompletor(__instance._hub.searchCoordinator, float.MaxValue);
                    bool success = searchCompletor.ValidateStart();
                    if (success)
                        searchCompletor.Complete();

                    new DrawableLineMessage(3, success ? Color.green : Color.red, [interactPosition, interactPosition + Vector3.up * 15])
                        .SendToAuthenticated();
                }));
            }
            else
            {
                actionAdder(new DummyAction("Interact", () =>
                    baseToy.ServerInteract(__instance._hub, baseToy.GetComponent<InteractableCollider>().ColliderId)));
            }
        }
    }
}