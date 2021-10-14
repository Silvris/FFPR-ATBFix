using HarmonyLib;
using Last.Battle;
using Last.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace FFPR_ATBFix
{
    [HarmonyPatch(typeof(DamageViewUIManager), nameof(DamageViewUIManager.CreateDamgeView))]
    public sealed class DamageViewUIManager_CreateDamgeView : Il2CppSystem.Object
    {
        public DamageViewUIManager_CreateDamgeView(IntPtr ptr) : base(ptr)
        {

        }
        public static void Postfix(int damage, bool isRecovery, bool isMiss, Transform transform, DamageViewUIManager __instance)
        {
            //ModComponent.Log.LogInfo(isMiss);
            List<GameObject> go = FunctionUtil.GetAllChildren(__instance.gameObject);
            foreach (GameObject gob in go)
            {
                //ModComponent.Log.LogInfo(gob.name);
                if (gob.transform.localScale.z == 0)
                {
                    gob.transform.localScale = new Vector3(gob.transform.localScale.x, gob.transform.localScale.y, 1);
                }
            }
        }
    }
}
