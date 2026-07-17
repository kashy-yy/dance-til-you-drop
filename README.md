# dance-til-you-drop
A rhythm-based boss fight game built in Godot using C#.

## About

Dance Till You Drop is a rhythm-action boss battle game where players must memorise and perform dance patterns while surviving a constantly changing dance floor.

Players defeat the boss by correctly following movement sequences. Successful patterns deal damage, while incorrect moves and dangerous tiles punish mistakes.

The project focuses on object-oriented game architecture, procedural generation, event-driven systems, and gameplay programming using Godot and C#.

This project is being developed to explore object-oriented programming, data structures, and game development using Godot and C#.

## Features

- 9 × 10 procedurally generated dance floor
- Dynamic colour-changing dance floor with tile state system
- Tile-based player movement
- Randomised dance pattern generation
- Visual dance pattern previews using generated tiles and directional arrows
- Event-driven player input validation system
- Modular architecture using separate gameplay systems:
  - DanceFloor
  - Player
  - DancePattern
  - DanceManager
  - DanceController
- Built using Godot 4 and C#

## Planned Features

- Boss AI and attack patterns
- Combat system
- Health and damage system
- More complex dance patterns
- Pattern difficulty progression
- Dangerous floor mechanics
- Music synchronisation
- Visual effects and polish
- Boss animations

## Technical Highlights

### Event-driven input system

Player movement is handled separately from dance validation using C# events. The Player class broadcasts successful movements, allowing the DanceController to validate patterns without tightly coupling gameplay systems.

### Procedural pattern generation

Dance patterns are represented as movement sequences and converted into visual previews dynamically at runtime.

### Component-based architecture

Gameplay responsibilities are separated across dedicated systems, allowing individual features to be modified without affecting unrelated parts of the game.

## Technologies

- Godot 4
- C#
- Git & GitHub

## Team

Developed by:
- Kashmira Hassan
- Leena Osman

## Screenshots

<img width="1600" height="1386" alt="image" src="https://github.com/user-attachments/assets/3a02588c-9959-4ce1-b62a-57f4ffa3b220" />

https://github.com/user-attachments/assets/dbb12382-437d-4ec8-9015-b6be12b689df

<img width="1518" height="1432" alt="image" src="https://github.com/user-attachments/assets/b5f97788-0278-4196-a012-de68a1fee206" />

https://github.com/user-attachments/assets/d21bddf6-7acd-4e06-8699-b00cd302bb1a






