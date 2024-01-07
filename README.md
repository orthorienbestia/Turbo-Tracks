# README

[Click here for APK](https://github.com/orthorienbestia/Turbo-Tracks/releases/tag/submission)

## Notable features added

- Navmesh Agent for AI Kart
- Cinemachine for player camera tracking
- URP
    - Bloom, motion blur and vignette
    - Shader graph for coin magnet effect
- Car controller
    - adding traction control
    - adding anti roll force in opposite direction
    - adding down force
- Camera
    - changing field of view based on current speed
    - tilting camera according to gyroscope

## Important Classes

- `KartMovementController` : Handles complete working of the player kart.
- `AIKartHandler` : Handles working of AI Kart.
- `PlayerInputHandler` : Takes input from Keyboard / Touch and forwards it to KartMovementController
- `CarColorModifier` : Handles player kart’s color changes in home scene
- `Spawner` : Used to spawn coins and power ups
- `Collectable` : Base abstract class which handles interaction with Player.
- `GameplayManager` : Handles all UI, effects, lap system, etc of game scene.

## Task Breakdown Structure (Git branch wise)

- Phase 1: Base Setup
- Phase 2: Important features
- Phase 3: Integration of features, Remaining features, Bug fixes
- Final Phase: Finalisation, Assets/Code CleanUp & Restructure, Documentation, Task Breakdown structure, Extra features

### Phase 1: Base Setup

- Basic Scene setup
- Input System
- Gyroscope
- Car controller

### Phase 2: Important features

- Power Ups
- Coins
- Collectable spawner
- Kart trail and collectable effects
- Start game countdown
- Race complete pop up
- Kart customisation panel

### Phase 3: Integration of features, Remaining features, Bug fixes

- Integration of all done features in scene
- Laps’ system
- AI Bot working using Navmesh Agent
- Improve car controller
- Improve camera follow
- Updating graphics
- Adding lighting and other effects
- Bug fixes

### Final Phase: Finalisation, Assets/Code CleanUp & Restructure, Documentation, Task Breakdown structure, Extra features

- Assets restructure
- Code clean up
- Code restructure
- Testing and bug fixes
- Adding extra features, surrounding, effects, etc
- Create Task Breakdown Structure
- Create Documentation / README
