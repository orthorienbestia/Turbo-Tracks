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
- Tutorial added at first launch and homescreen

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
- Adding Tutorial
- Add APK


## Screenshots
### Home Screen
![Screenshot 1](Screenshots/Screenshot%201.png "Screenshot 1")
### Game Tutorials
![Screenshot 2](Screenshots/Screenshot%202.png "Screenshot 2")
![Screenshot 3](Screenshots/Screenshot%203.png "Screenshot 3")
![Screenshot 4](Screenshots/Screenshot%204.png "Screenshot 4")
![Screenshot 5](Screenshots/Screenshot%205.png "Screenshot 5")
![Screenshot 6](Screenshots/Screenshot%206.png "Screenshot 6")
![Screenshot 7](Screenshots/Screenshot%207.png "Screenshot 7")
### Kart Customisation
![Screenshot 8](Screenshots/Screenshot%208.png "Screenshot 8")
### Game Play
![Screenshot 9](Screenshots/Screenshot%209.png "Screenshot 9")
![Screenshot 10](Screenshots/Screenshot%2010.png "Screenshot 10")
