using System.Reflection;
using BepInEx;
using BepInEx.Logging;
using HarmonyLib;

[BepInPlugin(PluginGUID, PluginName, PluginVersion)]
[MycoMod(null, ModFlags.IsClientSide)]
public class HotswapFixPlugin : BaseUnityPlugin
{
    public const string PluginGUID = "sparroh.hotswapfix";
    public const string PluginName = "HotswapFix";
    public const string PluginVersion = "1.0.0";

    internal static ManualLogSource Log;

    private Harmony _harmony;

    private void Awake()
    {
        Log = Logger;
        _harmony = new Harmony(PluginGUID);
        _harmony.PatchAll(typeof(ScoutLaserRiflePatches));
        Log.LogInfo($"{PluginName} v{PluginVersion} loaded — Hotswap will keep Laser as the default mode after menus/missions.");
    }

    private void OnDestroy()
    {
        _harmony?.UnpatchSelf();
    }
}

/// <summary>
/// Hotswap (DMLRUpgradeFlags.SwapModes) forces laser mode in OnUpgradesEnabled.
/// On upgrade reapply (menus, mission entry), OnUpgradesDisabled saves the current
/// laser mode into toggleLaserOnUpgradesEnabled, then AfterUpgradesEnabled toggles
/// it back — undoing Hotswap's laser default. This patch clears that restore flag
/// when Hotswap is equipped so laser remains the default.
/// </summary>
[HarmonyPatch(typeof(ScoutLaserRifle))]
internal static class ScoutLaserRiflePatches
{
    private static readonly FieldInfo ToggleLaserOnUpgradesEnabledField =
        AccessTools.Field(typeof(ScoutLaserRifle), "toggleLaserOnUpgradesEnabled");

    [HarmonyPatch(nameof(ScoutLaserRifle.AfterUpgradesEnabled))]
    [HarmonyPrefix]
    private static void AfterUpgradesEnabled_Prefix(ScoutLaserRifle __instance)
    {
        if (!__instance.UpgradeFlags.IsEnabled(DMLRUpgradeFlags.SwapModes))
            return;

        if (ToggleLaserOnUpgradesEnabledField == null)
        {
            HotswapFixPlugin.Log.LogWarning("Could not find toggleLaserOnUpgradesEnabled field; Hotswap fix may not apply.");
            return;
        }

        // Prevent AfterUpgradesEnabled from calling ToggleLaserMode() and flipping
        // Hotswap's forced laser default back to DMR.
        ToggleLaserOnUpgradesEnabledField.SetValue(__instance, false);
    }
}
