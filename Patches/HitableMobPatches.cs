using HarmonyLib;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShareEmRebalanced.Patches
{
    [HarmonyPatch(typeof(HitableMob))]
    class HitableMobPatches
    {
        [HarmonyPostfix, HarmonyPatch(nameof(HitableMob.Start))]
        static void Start(HitableMob __instance)
        {
            __instance.maxHp *= __instance.mob.bossType switch
            {
                Mob.BossType.BossNight => Main.PlayersInLobby,
                Mob.BossType.BossShrine => Main.PlayersInLobby,
                _ => 1
            };
            __instance.hp = __instance.maxHp;
        }

    }
}
