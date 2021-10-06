using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace FFPR_ATBFix
{
    [HarmonyPatch(typeof(Last.UI.Cursor), nameof(Last.UI.Cursor.SetScale))]
    public sealed class Cursor_SetScale : Il2CppSystem.Object
    {
        public Cursor_SetScale(IntPtr ptr) : base(ptr)
        {

        }
        public static void Postfix(float scale, Last.UI.Cursor __instance)
        {
            List<GameObject> go = new List<GameObject>();
            try
            {
                if (__instance != null)
                {
                    go = FunctionUtil.GetAllChildren(__instance.gameObject);
                }
            }catch(Exception ex)
            {
                ModComponent.Log.LogError(ex);
            }

            foreach (GameObject gob in go)
            {
                if (gob.transform.localScale.z == 0)
                {
                    gob.transform.localScale = new Vector3(gob.transform.localScale.x, gob.transform.localScale.y, 1);
                }
            }
        }


    }
}
