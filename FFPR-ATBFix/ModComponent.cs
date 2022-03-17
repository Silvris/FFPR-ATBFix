using BepInEx.Logging;
using Last.Management;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace FFPR_ATBFix
{
    public sealed class ModComponent : MonoBehaviour
    {
        public static ModComponent Instance { get; private set; }
        public static ManualLogSource Log { get; private set; }
        public static ResourceManager resourceManager { get; set; }
        public static string Target = "Assets/GameAssets/Serial/Res/UI/Key/Battle/Prefabs/player_info_content";
        public static bool hasRun = false;
        private Boolean _isDisabled;
        public ModComponent(IntPtr ptr) : base(ptr)
        {
        }
        public void Awake()
        {
            Log = BepInEx.Logging.Logger.CreateLogSource("FFPR_ATBFix");
            try
            {
                Instance = this;
                Log.LogMessage($"[{nameof(ModComponent)}].{nameof(Awake)}: Processed successfully.");
            }
            catch (Exception ex)
            {
                _isDisabled = true;
                Log.LogError($"[{nameof(ModComponent)}].{nameof(Awake)}(): {ex}");
                throw;
            }

        }
        public static void ApplyFix()
        {
            GameObject playerInfo = resourceManager.completeAssetDic[Target].Cast<GameObject>();
            if (playerInfo != null)
            {
                GameObject r1 = FunctionUtil.GetDirectChild(playerInfo, "root");
                if (r1 != null)
                {
                    GameObject r2 = FunctionUtil.GetDirectChild(r1, "root");
                    if (r2 != null)
                    {
                        foreach (GameObject gob in FunctionUtil.GetAllChildren(r2))
                        {
                            if (gob.name == "gauge" || gob.name == "frame")
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
        public void Update()
        {
            try
            {
                if (_isDisabled)
                {
                    return;
                }
                if(resourceManager == null)
                {
                    resourceManager = ResourceManager.Instance;
                    if (resourceManager == null) return;
                }
                if (!hasRun)
                {
                    if(resourceManager.completeAssetDic.ContainsKey(Target)) ApplyFix();
                }
            }
            catch (Exception ex)
            {
                _isDisabled = true;
                Log.LogError($"[{nameof(ModComponent)}].{nameof(Update)}(): {ex}");
                throw;
            }

        }
    }
}
