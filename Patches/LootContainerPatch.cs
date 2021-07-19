using HarmonyLib;

namespace ShareEmRebalanced.Patches
{
    [HarmonyPatch(typeof(LootContainerInteract))]
    class LootContainerGetNamePatch
    {
        [HarmonyPatch(nameof(LootContainerInteract.GetName)), HarmonyPostfix]
        static void GetNamePostfix(LootContainerInteract __instance, ref string __result)
        {
            __instance.price *= Main.PlayersInLobby;
            //Main.MainLogger?.LogMessage($"The base price {__instance.basePrice} is of the charts and results in {__instance.price}");
            __result = __instance.price < 1 ? "Open chest" : string.Format("{0} Gold\n<size=75%>open chest", __instance.price);

        }

    }
}
