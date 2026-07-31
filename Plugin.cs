using BepInEx;
using BepInEx.Logging;
using HarmonyLib;

[BepInPlugin(PluginGUID, PluginName, PluginVersion)]
[MycoMod(null, ModFlags.IsClientSide)]
public class HotswapFixPlugin : BaseUnityPlugin
{
    public const string PluginGUID = "sparroh.hotswapfix";
    public const string PluginName = "HotswapFix";
    public const string PluginVersion = "1.0.1";

    internal static ManualLogSource Log;

    private Harmony _harmony;

    private void Awake()
    {
        Log = Logger;
        _harmony = new Harmony(PluginGUID);
        _harmony.PatchAll(typeof(ScoutLaserRiflePatches));
        Log.LogInfo(
            $"{PluginName} v{PluginVersion} loaded — Hotswap will keep Laser as the default mode after menus/missions.");
    }

    private void OnDestroy()
    {
        _harmony?.UnpatchSelf();
    }
}