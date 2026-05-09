# U009_Kill_Them_All_2D

[English](README.en.md) | [Español](README.md)

## Summary

Playable roguelite prototype developed in Unity with C#. **Kill Them All!** is a top-down/isometric survival game based on auto-attacks, enemy waves, selectable characters, upgradeable skills, experience, gems and progressive difficulty.

The experience takes place in the Abyss, a dark and dangerous place connected to Eldoria and the Dark Warrior. The player must survive, defeat enemy hordes, collect gems, level up and improve skills to last longer and longer.

## Collaboration

Project developed as a team together with **Hugo** and **Sergio**. My contribution focused on core programming, mechanic implementation, gameplay systems, integration, technical support, testing and development coordination.

## Documentation

- [`GDD_Kill them All!.pdf`](./GDD_Kill%20them%20All!.pdf)

## Technologies

- Unity
- C#
- Unity 2D physics system
- Collider2D / Rigidbody2D
- Unity UI system
- Canvas
- EventSystem
- ScriptableObjects
- Animator
- Particle System
- AudioSource
- URP
- ShaderLab
- HLSL
- Photoshop
- Spine
- Git LFS
- GitHub Releases

## Implemented features

- Top-down / isometric movement.
- Auto-attack.
- Enemy waves.
- Enemy spawning.
- Basic chase AI.
- Health system.
- Damage system.
- Experience gems.
- Leveling system.
- Upgrade selection.
- Skill upgrades.
- Timer.
- Difficulty increase over time.
- Selectable characters.
- Main menu.
- Character selection menu.
- Options menu.
- Pause menu.
- Health HUD.
- Experience/level HUD.
- Skills HUD.
- Game Over.
- Summary screen.
- Sound.
- Music.
- VFX.
- Playable Windows build.

## Implemented characters and abilities

- **Assassin** — `Dagger Shot`, `Poison Flasks`, `Multi-Cut`, `ShadowStep`.
- **Cleric** — `Basic Light Pulse`, `Sacred Sword`, `Solar Beam`, `Sacred Floor`.
- **Warrior** — `Base Atk`, `Twisting Slash / Spin`, `Rageful Blow / Ground Crack`, `Earthshake`.
- **Sorceress** — `Arcane Ball`, `Twister`, `Thunder`, `Inferno`.
- **Hunter** — `Arrow Shot`, `Penetration Arrow`, `Arrow Fall`, `Flying Traps`.

## Implemented enemies

- Tribal Tribes.
- Mummies.
- Skeletons.
- Dracomancers.
- Dark Warrior.
- Werewolves.

## Designed / planned

In addition to the implemented content, the design and future improvements include:

- Sixth playable character.
- Achievements.
- Progress saving.
- Chests.
- Random events.
- More enemies and variants.
- More biomes or visual variations.
- More complete dynamic music.
- Greater variety of VFX and impact feedback.
- Advanced balancing of waves, skills and difficulty curve.
- Narrative expansion of the Abyss, Eldoria and the Dark Warrior.

## Screenshots

> Final screenshots pending.

Planned path:

![Gameplay](./Media/screenshots/gameplay-01.png)

## Architecture

The main logic is organized inside `PRJ_KillThemAll/Assets/_Root/Resources/Scripts/` into several areas:

- **Enemy** — enemy movement, stats, spawner, waves and enemy data.
- **General** — game manager, character selection, scenes, damage popups, drops and utilities.
- **Map** — map control, chunks, tiles and world scrolling.
- **Player** — movement, stats, inventory, collection and player animation.
- **UI** — stats, upgrades, inventory icons and virtual joystick.
- **Weapons** — base weapons, melee weapons, projectiles, skills, VFX and timers.
- **Passive Items** — items, passives and related data.
- **Pick-ups** — collectibles.
- **MiniFix** — auxiliary skill tracking.

Highlighted scripts:

- `GameManager`
- `CharacterSelector`
- `ControllerScene`
- `DropRateManager`
- `EnemySpawner`
- `SpawnManager`
- `WaveData`
- `EnemyMovement`
- `EnemyStats`
- `EnemyScriptableObject`
- `PlayerMovement`
- `PlayerStats`
- `PlayerCollector`
- `PlayerInventory`
- `CharacterData`
- `MapController`
- `WorldScrolling`
- `ChunkOptimizer`
- `Weapon`
- `WeaponData`
- `ProjectileWeapon`
- `MeleeWeapon`
- `SpawnSkill`
- `VFXWeapon`
- `UISkillTimers`
- `UIUpgradeWindow`
- `UIStatDisplay`
- `UIInventoryIconsDisplay`
- `Pickup`
- `Passive`
- `PassiveData`
- `SkillTracking`

## Recommended code to review

- [`PRJ_KillThemAll/Assets/_Root/Resources/Scripts/General/GameManager.cs`](./PRJ_KillThemAll/Assets/_Root/Resources/Scripts/General/GameManager.cs)
- [`PRJ_KillThemAll/Assets/_Root/Resources/Scripts/General/CharacterSelector.cs`](./PRJ_KillThemAll/Assets/_Root/Resources/Scripts/General/CharacterSelector.cs)
- [`PRJ_KillThemAll/Assets/_Root/Resources/Scripts/Enemy/EnemySpawner.cs`](./PRJ_KillThemAll/Assets/_Root/Resources/Scripts/Enemy/EnemySpawner.cs)
- [`PRJ_KillThemAll/Assets/_Root/Resources/Scripts/Enemy/SpawnManager.cs`](./PRJ_KillThemAll/Assets/_Root/Resources/Scripts/Enemy/SpawnManager.cs)
- [`PRJ_KillThemAll/Assets/_Root/Resources/Scripts/Enemy/WaveData.cs`](./PRJ_KillThemAll/Assets/_Root/Resources/Scripts/Enemy/WaveData.cs)
- [`PRJ_KillThemAll/Assets/_Root/Resources/Scripts/Enemy/EnemyMovement.cs`](./PRJ_KillThemAll/Assets/_Root/Resources/Scripts/Enemy/EnemyMovement.cs)
- [`PRJ_KillThemAll/Assets/_Root/Resources/Scripts/Player/PlayerMovement.cs`](./PRJ_KillThemAll/Assets/_Root/Resources/Scripts/Player/PlayerMovement.cs)
- [`PRJ_KillThemAll/Assets/_Root/Resources/Scripts/Player/PlayerStats.cs`](./PRJ_KillThemAll/Assets/_Root/Resources/Scripts/Player/PlayerStats.cs)
- [`PRJ_KillThemAll/Assets/_Root/Resources/Scripts/Weapons/Weapon.cs`](./PRJ_KillThemAll/Assets/_Root/Resources/Scripts/Weapons/Weapon.cs)
- [`PRJ_KillThemAll/Assets/_Root/Resources/Scripts/Weapons/ProjectileWeapon.cs`](./PRJ_KillThemAll/Assets/_Root/Resources/Scripts/Weapons/ProjectileWeapon.cs)
- [`PRJ_KillThemAll/Assets/_Root/Resources/Scripts/UI/UIUpgradeWindow.cs`](./PRJ_KillThemAll/Assets/_Root/Resources/Scripts/UI/UIUpgradeWindow.cs)
- [`PRJ_KillThemAll/Assets/_Root/Resources/Scripts/Map/MapController.cs`](./PRJ_KillThemAll/Assets/_Root/Resources/Scripts/Map/MapController.cs)

## Build

The build is available through GitHub Releases.

[`Releases/Download.md`](./Releases/Download.md)

[Download build U009-v1.0.0](https://github.com/BLRochaGonzalez93/U009_Kill_Them_All_2D/releases/tag/U009-v1.0.0)

## Status

**Playable roguelite prototype.**

The project includes a functional roguelite survival base with top-down/isometric movement, auto-attacks, selectable characters, skills, gems, experience, leveling, waves, enemy spawning, progressive difficulty, HUD, menus, Game Over, summary screen, sound, music and VFX.

Possible pending improvements:

- Complete all playable characters.
- Complete and balance skills.
- Add more enemies and variants.
- Improve the wave system.
- Improve the difficulty curve.
- Add more VFX and impact feedback.
- Add more sounds and dynamic music.
- Add achievements.
- Add progress saving.
- Add more biomes or visual variations.
- Optimize performance.

## Learnings

This project allowed me to work on the design of a top-down roguelite inspired by **Vampire Survivors**, focused on survival, auto-attacks, progression and enemy waves.

It also helped me implement auto-attacks and survival against waves, experience, levels, gems and skill upgrade systems.

In addition, I designed characters with differentiated abilities and managed spawning, progressive difficulty and basic enemy behavior.

The project also helped me create a HUD for health, experience, skills and timer, as well as practice teamwork with programming, VFX and art roles.
