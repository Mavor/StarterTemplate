using System.Reflection;
using BepInEx;
using BepInEx.Logging;
using CommonAPI.Systems;
using CommonAPI;
using HarmonyLib;
using System;

namespace DysonSphereProgram.Modding.SwizzlesDarkStormExpansion;

//[BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
[BepInAutoPlugin("dev.swizzlewizzle.dsp.SwizzlesDarkStormExpansion", "SwizzlesDarkStormExpansion")]
[BepInProcess("DSPGAME.exe")]
[BepInDependency(CommonAPIPlugin.GUID)]
[CommonAPISubmoduleDependency(nameof(ProtoRegistry), nameof(CustomKeyBindSystem))]
public partial class Plugin : BaseUnityPlugin
{
    internal static ManualLogSource Log;
    private Harmony _harmony;

    private void Awake()
    {
        Plugin.Log = Logger;
        _harmony = new Harmony(Plugin.Id);
        _harmony.PatchAll(typeof(EnemyRescalingPatch));
        _harmony.PatchAll(typeof(VegRescalingPatch));
        Logger.LogInfo($"Swizzle's Dark Storm Expansion has been loaded. Welcome to the tempest.");


        // Load Harmony patches in this assembly
        Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly());
    }
}
