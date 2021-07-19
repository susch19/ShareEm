using HarmonyLib;

namespace ShareEmRebalanced.Patches
{
    [HarmonyPatch(typeof(SteamLobby))]
    class SteamLobbyPatches
    {


        internal static int multiplier = 1;
        [HarmonyPatch(nameof(SteamLobby.StartGame)), HarmonyPostfix]
        static void GetMemberCount(SteamLobby __instance)
        {
            //Main.MainLogger?.LogMessage("Multiply Chest prices by " + __instance.currentLobby.MemberCount);
            multiplier = Main.PlayersInLobby;
        }
    }
}
