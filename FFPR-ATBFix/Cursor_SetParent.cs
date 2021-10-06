using HarmonyLib;
using Last.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace FFPR_ATBFix
{
    [HarmonyPatch(typeof(Last.UI.Cursor),nameof(Last.UI.Cursor.SetParent))]
    public sealed class Cursor_SetParent : Il2CppSystem.Object
    {
        public Cursor_SetParent(IntPtr ptr) : base(ptr)
        {

        }
        public static void Postfix(GameObject parent, bool isZeroPostion, Last.UI.Cursor __instance)
        {
            List<GameObject> go = FunctionUtil.GetAllChildren(__instance.gameObject);
            foreach(GameObject gob in go)
            {
                if(gob.transform.localScale.z == 0)
                {
                    gob.transform.localScale = new Vector3(gob.transform.localScale.x, gob.transform.localScale.y, 1);
                }
            }
        }


    }
}
