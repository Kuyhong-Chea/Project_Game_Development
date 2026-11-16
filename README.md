
# Horrified

Horrified is a first-person 3D horror game inspired by classic maze-chasing gameplay. Set on Halloween night, the player enters an abandoned house on a dare to collect 20 candies, only to discover that the family who disappeared there still haunts the property.

- Gameplay video: [Watch Gameplay](https://drive.google.com/file/d/1rXgNvN6jkZ5QfJ2Cw41Lkd_8lCgyuJdQ/view?usp=sharing)
- Unity version: 6000.3.8f1 (Unity 6.3 LTS)

## Overview

The objective is to collect all 20 candies while avoiding the ghost, which patrols the house and aggressively pursues the player when it spots them. Each failed encounter costs a life, and the player must survive the night to win.

## Gameplay

- Candies spawn in random locations throughout the house.
- Walking into a candy collects it and contributes to the total.
- The ghost roams the environment using waypoints and chases the player when detected.
- If the ghost catches the player, a jumpscare triggers, one life is lost, and the player respawns at the entrance.
- Spider webs reduce movement speed temporarily, creating additional risk while navigating the house.
- Sprinting consumes stamina; once depleted, the player must wait for it to recharge before sprinting again.
- Winning requires collecting all 20 candies.
- Losing occurs after all 3 lives are exhausted.
- Players can restart the round from the end-screen menu or quit the game at any time using the escape key.

## Controls

| Action | Control |
| --- | --- |
| Move | W, A, S, D |
| Look | Mouse |
| Sprint | Left Shift or hold Left Mouse |
| Quit | Esc |

## How to Play

1. Download the project ZIP and extract it to a local folder.
2. Open the extracted folder and run `Horrified.exe`.
3. If Windows displays a security warning, select “More info” and then “Run anyway”.

## Opening in Unity

1. Clone this repository to your local machine.
2. Open Unity Hub.
3. Select “Add” → “Add project from disk” and choose the project folder.
4. Open the project using Unity 6000.3.8f1.
5. Navigate to the scene in `Assets/Scenes` and press Play to test the game.

## Project Structure

| Script | Purpose |
| --- | --- |
| PlayerController | Handles movement, camera look, sprinting, and stamina management |
| GameManager | Oversees game flow, candy collection, lives, jumpscares, messages, and win/lose conditions |
| GhostAI | Controls ghost patrol behavior and chase logic using NavMesh navigation |
| CandySpawner | Randomly places collectible candies across the map |
| CandyPickup | Detects and collects candy when the player makes contact |
| SlowZone | Applies temporary slowdown effects when the player enters spider-web areas |
| MenuController | Manages the restart and quit menu actions |

## Credits

This project makes use of assets and content from the following sources:

- Kenney for building and furniture kits (kenney.nl)
- Ghost asset source: [ghost asset]
- Spider web texture: [spider web texture]
- Floor texture: [floor texture]

## License

This project is intended for educational and personal use. Please respect the licensing terms of the included third-party assets and content.

