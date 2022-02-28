using HarmonyLib;
using Last.Management;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace FFPR_ATBFix
{
    //[HarmonyPatch(typeof(ResourceManager), nameof(ResourceManager.IsLoadAssetCompleted), new Type[] { typeof(string)})]
    public sealed class ResourceManager_IsAssetLoadCompleted
    {
        public static string Target = "Assets/GameAssets/Serial/Res/UI/Key/Battle/Prefabs/player_info_content";
        public static bool hasRun = false;
        //apparently this gets called multiple times?
        //a perfectly even amount too, so the goal actually fails
        public static void Postfix(string addressName, ResourceManager __instance, bool __result)
        {
            ModComponent.Log.LogInfo(addressName);
            if (!hasRun)
            {
                if (addressName == Target)
                {
                    if (__result == true)
                    {
                        GameObject playerInfo = __instance.completeAssetDic[addressName].Cast<GameObject>();
                        if (playerInfo != null)
                        {
                            GameObject r1 = FunctionUtil.GetDirectChild(playerInfo, "root");
                            if (r1 != null)
                            {
                                GameObject r2 = FunctionUtil.GetDirectChild(r1, "root");
                                if (r2 != null)
                                {
                                    GameObject rA = FunctionUtil.GetDirectChild(r2, "root_atb");
                                    if (rA != null)
                                    {
                                        List<GameObject> gobs = FunctionUtil.GetAllChildren(rA);
                                        foreach (GameObject gob in gobs)
                                        {
                                            if(gob.name == "gauge" || gob.name == "frame")
                                            {
                                                Vector3 local = gob.transform.localPosition;
                                                local.y = Mathf.Round(local.y);
                                                gob.transform.localPosition = local;
                                            }
                                        }
                                        hasRun = true;
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        ModComponent.Log.LogInfo("result = false");
                    }
                }
            }
        }
    }
}
