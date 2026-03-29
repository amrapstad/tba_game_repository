# Project TBA

## Overview
Project TBA is a 3D First-Person Unity game integrating a card-based mechanic to handle abilities/effects along with traditional first-person movement and enemy interactions.

<video src="github_demo/demo_unity_game.gif" controls width="600"></video>

## What Has Been Done
Currently, the following core features are implemented:
* **First-Person Player Movement**: Includes walking, sprinting, jumping, crouching, dynamic head bobbing, and leaning while strafing.
* **NPC AI System**: Includes state-driven AI (Patrolling, Chasing, and Attacking). NPCs can shoot projectiles when the player is within attack range and patrol when out of range.
* **Card & Effect System**: Includes robust `ScriptableObject`-based data representations for cards that contain uses, descriptions, and lists of effects (e.g., Attack/Heal/Buff) and subtypes (e.g., Physical/Magical damage, Temporary/Permanent healing).
* **Event System Framework**: Includes customizable trigger events (such as `GameEvents.current.onNPCTouch`).

## How to Run the Game
1. **Prerequisites**: Make sure you have **Unity Hub** installed.
2. **Install Unity Version**: Install Unity Editor **6000.3.0f1**.
3. **Open Project**: From Unity Hub, click **Add project from disk** and select the `tba_game_unity` folder located inside the repository.
4. **Play**: Open the project, navigate to `Assets/Scenes/LVL1/` and click on '`Cave1`' in the Project window, and press the **Play** button at the top middle of the editor.
