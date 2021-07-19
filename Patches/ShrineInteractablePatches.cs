using HarmonyLib;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;

using static UnityEngine.AudioSettings;

using Random = UnityEngine.Random;

namespace ShareEmRebalanced.Patches
{
    [HarmonyPatch(typeof(ShrineInteractable))]
    class ShrineInteractablePatches
    {
        [HarmonyPatch(nameof(ShrineInteractable.ServerExecute)), HarmonyPrefix]
        static bool StartShrine(ShrineInteractable __instance, int fromClient)
        {
            if (__instance.started)
            {
                return true;
            }
            var mobAmount = 3 * Main.PlayersInLobby;
            __instance.mobIds = new int[mobAmount];
            MobType mobType = GameLoop.Instance.SelectMobToSpawn(true);
            int num = mobAmount;
            for (int i = 0; i < num; i++)
            {
                int nextId = MobManager.Instance.GetNextId();
                int mobType2 = mobType.id;
                RaycastHit raycastHit;
                if (Physics.Raycast(__instance.transform.position + new Vector3(Random.Range(-1f, 1f) * 10f, 100f, Random.Range(-1f, 1f) * 10f), Vector3.down, out raycastHit, 200f, __instance.whatIsGround))
                {
                    MobSpawner.Instance.ServerSpawnNewMob(nextId, mobType2, raycastHit.point, 1.75f, 1f, Mob.BossType.None, -1);
                    __instance.mobIds[i] = nextId;
                }
            }
            __instance.StartShrine(__instance.mobIds);
            ServerSend.ShrineStart(__instance.mobIds, __instance.id);

            return false;
        }

        [HarmonyPatch(nameof(ShrineInteractable.CheckLights)), HarmonyPrefix]
        static bool CheckLights(ShrineInteractable __instance)
        {
            int num = 0;
            var players = Main.PlayersInLobby;
            foreach (int key in __instance.mobIds)
            {
                if (!MobManager.Instance.mobs.ContainsKey(key))
                {
                    num++;
                }
            }
            for (int j = 0; j < num / players; j++)
            {
                __instance.lights[j].material = __instance.lightMat;
            }
            if (num >= 3 * players)
            {
                __instance.CancelInvoke("CheckLights");
                if (LocalClient.serverOwner)
                {
                    __instance.Invoke("DropPowerup", 1.33f);
                }
                UnityEngine.Object.Instantiate<GameObject>(__instance.destroyShrineFx, __instance.transform.position, __instance.destroyShrineFx.transform.rotation);
                __instance.Invoke("DestroyShrine", 1.33f);
            }
            return false;
        }
    }
}
