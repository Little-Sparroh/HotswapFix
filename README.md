# HotswapFix

A BepInEx client-side mod for Mycopunk that fixes the **Hotswap** upgrade on the Scout Laser Rifle (DMLR) so Laser
remains the default mode after opening menus or entering missions.

## The Bug

With Hotswap equipped, the weapon correctly defaults to Laser mode — until you open a menu or enter a mission. At that
point the game reapplies upgrades and the weapon flips back to DMR as the default.

## The Fix

Hotswap (`DMLRUpgradeFlags.SwapModes`) forces Laser mode when upgrades are applied. On reapply, the game temporarily
disables upgrades, remembers that Laser was active, then re-enables upgrades (setting Laser again) and **toggles mode
once more** — ending on DMR.

This mod prevents that final toggle when Hotswap is equipped, so Laser stays the default after menus and mission entry.

## Dependencies

- Mycopunk
- [BepInEx Pack for Mycopunk](https://thunderstore.io/c/mycopunk/p/BepInEx/BepInExPack_Mycopunk/) 5.4.2403 or compatible

## Installing

**Via Thunderstore (recommended):**

1. Install with a Thunderstore mod manager (e.g. r2modman or the Thunderstore App).

**Manual installation:**

1. Install BepInEx for Mycopunk if you have not already.
2. Copy `HotswapFix.dll` into `<Mycopunk Directory>/BepInEx/plugins/`.

## Building

```bash
dotnet build --configuration Release
```

The built assembly is written to `bin/Release/netstandard2.1/HotswapFix.dll`.

## Authors

- Sparroh

## Links

- [GitHub](https://github.com/Little-Sparroh/HotswapFix)
- [Thunderstore](https://thunderstore.io/c/mycopunk/p/Sparroh/HotswapFix/)

## License

This project is licensed under the MIT License — see the [LICENSE](LICENSE) file for details.
