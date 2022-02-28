using BepInEx;
using BepInEx.IL2CPP;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnhollowerRuntimeLib;
using UnityEngine;

namespace FFPR_ATBFix
{
    [BepInPlugin("silvris.ffpr.atb_fix", "ATB Fix", "2.0.0.0")]
    [BepInProcess("FINAL FANTASY IV.exe")]
    [BepInProcess("FINAL FANTASY V.exe")]
    [BepInProcess("FINAL FANTASY VI.exe")]
    public class EntryPoint : BasePlugin
    {
        public static EntryPoint Instance { get; private set; }
        public override void Load()
        {
            Instance = this;
            Log.LogInfo("Loading...");
            ClassInjector.RegisterTypeInIl2Cpp<ModComponent>();
            String name = typeof(ModComponent).FullName;
            Log.LogInfo($"Initializing in-game singleton: {name}");
            GameObject singleton = new GameObject(name);
            singleton.hideFlags = HideFlags.HideAndDontSave;
            GameObject.DontDestroyOnLoad(singleton);
            Log.LogInfo("Adding ModComponent to singleton...");
            ModComponent component = singleton.AddComponent<ModComponent>();
            if (component is null)
            {
                GameObject.Destroy(singleton);
                throw new Exception($"The object is missing the required component: {name}");
            }
            PatchMethods();
        }
        private void PatchMethods()
        {
            try
            {
                Log.LogInfo("Patching methods...");
                Harmony harmony = new Harmony("silvris.ffpr.atb_fix");
                harmony.PatchAll(Assembly.GetExecutingAssembly());
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to patch methods.", ex);
            }
        }
    }
}
