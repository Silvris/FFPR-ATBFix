using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace FFPR_ATBFix
{
    static class FunctionUtil
    {
        public static List<GameObject> GetAllChildren(GameObject obj)
        {
            List<GameObject> children = new List<GameObject>();

            if(obj != null)
            {
                for (int i = 0; i < obj.transform.childCount; i++)
                {
                    Transform child = obj.transform.GetChild(i);
                    if (child != null)
                    {
                        if (child.gameObject != null)
                        {
                            children.Add(child.gameObject);
                            if(child.childCount != 0)
                            {
                                children.AddRange(GetAllChildren(child.gameObject));
                            }
                        }
                    }


                }
            }
            else
            {
                ModComponent.Log.LogWarning("Root object is null!");
            }

            return children;
        }
    }
}
