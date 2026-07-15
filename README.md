# HotswapFix

A BepInEx mod for Mycopunk that fixes the **Hotswap** upgrade on the Scout Laser Rifle (DMLR).

## The Bug

With Hotswap equipped, the weapon correctly defaults to Laser mode — until you open a menu or enter a mission. At that point the game reapplies upgrades and the weapon flips back to DMR as the default, which is annoying in play.

## The Fix

Hotswap (`DMLRUpgradeFlags.SwapModes`) forces Laser mode when upgrades are applied. On reapply, the game temporarily disables upgrades, remembers that Laser was active, then re-enables upgrades (setting Laser again) and **toggles mode once more** — ending on DMR.

This mod prevents that final toggle when Hotswap is equipped, so Laser stays the default after menus and mission entry.

## Dependencies

* Mycopunk (base game)
* [BepInEx](https://github.com/BepInEx/BepInEx) - Version 5.4.2403 or compatible
* .NET Framework 4.8
* HarmonyLib (included via NuGet / BepInEx)

## Building

```bash
dotnet build --configuration Release
```

## Installing

**Via Thunderstore (Recommended):**
1. Download and install via Thunderstore Mod Manager

**Manual Installation:**
1. Place `HotswapFix.dll` in `<Mycopunk Directory>/BepInEx/plugins/`

## Authors

- Sparroh

## License

This project is licensed under the MIT License - see the LICENSE file for details
