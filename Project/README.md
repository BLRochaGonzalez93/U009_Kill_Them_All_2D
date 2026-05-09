# U009_Kill_Them_All_2D

[English](README.en.md) | [Español](README.md)

## Resumen

Prototipo roguelite jugable desarrollado en Unity con C#. **Kill Them All!** es un juego de supervivencia top-down/isométrico basado en auto-ataque, oleadas de enemigos, personajes seleccionables, habilidades mejorables, experiencia, gemas y dificultad progresiva.

La experiencia se desarrolla en el Abismo, un lugar oscuro y peligroso conectado con Eldoria y el Guerrero Oscuro. El jugador debe sobrevivir, derrotar hordas de enemigos, recoger gemas, subir de nivel y mejorar habilidades para aguantar cada vez más tiempo.

## Colaboración

Proyecto desarrollado en equipo junto a **Hugo** y **Sergio**. Mi contribución se centró en programación principal, implementación de mecánicas, sistemas de gameplay, integración, soporte técnico, pruebas y coordinación del desarrollo.

## Documentación

- [`GDD_Kill them All!.pdf`](../Media/Diagrams/GDD_Kill%20them%20All!.pdf)

## Tecnologías

- Unity
- C#
- Sistema de físicas 2D de Unity
- Collider2D / Rigidbody2D
- Sistema de UI de Unity
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

## Características implementadas

- Movimiento top-down / isométrico.
- Auto-ataque.
- Oleadas de enemigos.
- Spawn de enemigos.
- IA básica de persecución.
- Sistema de vida.
- Sistema de daño.
- Gemas de experiencia.
- Subida de nivel.
- Selección de mejoras.
- Mejora de habilidades.
- Cronómetro.
- Aumento de dificultad con el tiempo.
- Personajes seleccionables.
- Menú principal.
- Menú de selección de personaje.
- Menú de opciones.
- Menú de pausa.
- HUD de vida.
- HUD de experiencia/nivel.
- HUD de habilidades.
- Game Over.
- Pantalla de resumen.
- Sonido.
- Música.
- VFX.
- Build jugable para Windows.

## Personajes y habilidades implementadas

- **Assassin** — `Dagger Shot`, `Poison Flasks`, `Multi-Cut`, `ShadowStep`.
- **Cleric** — `Basic Light Pulse`, `Sacred Sword`, `Solar Beam`, `Sacred Floor`.
- **Warrior** — `Base Atk`, `Twisting Slash / Spin`, `Rageful Blow / Ground Crack`, `Earthshake`.
- **Sorceress** — `Arcane Ball`, `Twister`, `Thunder`, `Inferno`.
- **Hunter** — `Arrow Shot`, `Penetration Arrow`, `Arrow Fall`, `Flying Traps`.

## Enemigos implementados

- Tribus Tribales.
- Momias.
- Esqueletos.
- Dracomantes.
- Guerrero Oscuro.
- Hombres lobo.

## Diseñado / previsto

Además de lo implementado, el diseño y las mejoras futuras contemplan:

- Sexto personaje jugable.
- Logros.
- Guardado de progreso.
- Cofres.
- Eventos aleatorios.
- Más enemigos y variantes.
- Más biomas o variaciones visuales.
- Música dinámica más completa.
- Mayor variedad de VFX y feedback de impacto.
- Balance avanzado de oleadas, habilidades y curva de dificultad.
- Expansión narrativa del Abismo, Eldoria y el Guerrero Oscuro.

## Capturas

> Pendiente de añadir capturas finales.

Ruta prevista:

![Gameplay](./Media/screenshots/gameplay-01.png)

## Arquitectura

La lógica principal se organiza dentro de `PRJ_KillThemAll/Assets/_Root/Resources/Scripts/` en varias áreas:

- **Enemy** — movimiento enemigo, stats, spawner, waves y datos de enemigos.
- **General** — game manager, selección de personaje, escenas, popups de daño, drops y utilidades.
- **Map** — control de mapa, chunks, tiles y desplazamiento del mundo.
- **Player** — movimiento, stats, inventario, recolección y animación del jugador.
- **UI** — estadísticas, mejoras, iconos de inventario y joystick virtual.
- **Weapons** — armas base, armas cuerpo a cuerpo, proyectiles, skills, VFX y timers.
- **Passive Items** — objetos, pasivas y datos asociados.
- **Pick-ups** — recogibles.
- **MiniFix** — tracking auxiliar de habilidades.

Scripts destacados:

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

## Código recomendado para revisar

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

La build está disponible en GitHub Releases.

[`Releases/Download.md`](./Releases/Download.md)

[Descargar build U009-v1.0.0](https://github.com/BLRochaGonzalez93/U009_Kill_Them_All_2D/releases/tag/U009-v1.0.0)

## Estado

**Prototipo roguelite jugable.**

El proyecto incluye una base funcional de supervivencia roguelite con movimiento top-down/isométrico, auto-ataque, personajes seleccionables, habilidades, gemas, experiencia, subida de nivel, oleadas, spawn de enemigos, dificultad progresiva, HUD, menús, Game Over, pantalla de resumen, sonido, música y VFX.

Pendiente de posibles mejoras:

- Completar todos los personajes jugables.
- Completar y balancear habilidades.
- Añadir más enemigos y variantes.
- Mejorar el sistema de oleadas.
- Mejorar la curva de dificultad.
- Añadir más VFX y feedback de impacto.
- Añadir más sonidos y música dinámica.
- Añadir logros.
- Añadir guardado de progreso.
- Añadir más biomas o variaciones visuales.
- Optimizar rendimiento.

## Aprendizajes

Este proyecto me permitió trabajar el diseño de un roguelite top-down inspirado en **Vampire Survivors**, centrado en supervivencia, auto-ataque, progresión y oleadas de enemigos.

También me sirvió para implementar auto-ataque y supervivencia contra oleadas, sistema de experiencia, niveles, gemas y mejora de habilidades.

Además, pude diseñar personajes con habilidades diferenciadas, gestionar spawn, dificultad progresiva y comportamiento básico de enemigos.

El proyecto también me ayudó a crear un HUD para vida, experiencia, habilidades y cronómetro, además de practicar trabajo en equipo con roles de programación, VFX y arte.
