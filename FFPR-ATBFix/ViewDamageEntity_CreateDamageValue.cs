﻿using HarmonyLib;
using Last.Battle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace FFPR_ATBFix
{
    //[HarmonyPatch(typeof(ViewDamageEntity), nameof(ViewDamageEntity.CreateDamageValue))]
    public sealed class ViewDamageEntity_CreateDamageValue : Il2CppSystem.Object
    {
        public ViewDamageEntity_CreateDamageValue(IntPtr ptr) : base(ptr)
        {

        }
        public static void Postfix(ViewDamageEntity __instance)
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
