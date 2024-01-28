using System.Reflection;
using BepInEx;
using BepInEx.Logging;
using CommonAPI.Systems;
using CommonAPI;
using HarmonyLib;

namespace DysonSphereProgram.Modding.SwizzlesDarkStormExpansion;

//[BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
[BepInAutoPlugin("dev.swizzlewizzle.dsp.SwizzlesDarkStormExpansion", "SwizzlesDarkStormExpansion")]
[BepInProcess("DSPGAME.exe")]
[BepInDependency(CommonAPIPlugin.GUID)]
[CommonAPISubmoduleDependency(nameof(ProtoRegistry), nameof(CustomKeyBindSystem))]
public partial class Plugin : BaseUnityPlugin
{
    internal new static ManualLogSource Logger;
        
    private void Awake()
    {
        // Plugin startup logic
        Logger = base.Logger;
        //Logger.LogInfo($"Plugin {MyPluginInfo.PLUGIN_GUID} is loaded!");
        Logger.LogInfo($"Plugin wonkytonk is loaded!");

        // Load Harmony patches in this assembly
        Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly());
    }
}
