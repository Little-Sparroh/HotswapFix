using System.Reflection;
using HarmonyLib;

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
            HotswapFixPlugin.Log.LogWarning(
                "Could not find toggleLaserOnUpgradesEnabled field; Hotswap fix may not apply.");
            return;
        }

        ToggleLaserOnUpgradesEnabledField.SetValue(__instance, false);
    }
}