# The Room That Breathes

A single-player VR psychological horror game built in Unity 6 for the Meta Quest 2.

There are no enemies. No jumpscares. Just a room that slowly breaks down around you.

---

## Overview

The Room That Breathes is a first-person VR exploration game built around environmental dread. The player is trapped in a single room that gradually shrinks over time. Objects shift and move when outside the player's field of view, but freeze the instant they are looked at directly. The only way out is to solve the room before it closes in completely.

The game was developed as a minor project in Semester VI of a B.Tech program in Robotics and AI.

---

## Core Mechanics

**Shrinking Room**
The walls, floor, and ceiling continuously scale inward using transform lerp over time. The rate of shrinkage escalates as the game progresses. Implemented in `RoomShrink.cs`.

**FOV-Based Object Shifting**
Objects in the environment move when they fall outside the player's field of view and freeze the moment they enter it. This creates a constant sense of unease without any active threat. Detection is handled using the Meta XR SDK's camera data. Implemented in `ObjectShift.cs`.

**Environmental Puzzle**
The room contains a two-stage puzzle that the player must solve to trigger the exit sequence. The correct code is derived from clues placed around the environment. Keypad interaction and validation are handled in `KeypadManager.cs`.

**Dual Endings**
The game has two possible endings depending on how the player progresses through the puzzle and interacts with the environment.

**Dynamic Audio Escalation**
Background audio intensifies in response to room scale and player proximity to walls, reinforcing the sense of confinement without relying on visual jump scares.

---

## Technical Details

| Field | Value |
|---|---|
| Engine | Unity 6 (6000.3.11f1) |
| Target Platform | Meta Quest 2 (Android / ARM64) |
| Scripting Backend | IL2CPP |
| Color Space | Linear |
| XR Integration | Meta XR SDK, OVRCameraRig |
| Language | C# |

---

## Scripts

| Script | Responsibility |
|---|---|
| `RoomShrink.cs` | Controls wall scaling over time using transform lerp |
| `ObjectShift.cs` | FOV detection logic; moves objects outside view, freezes them in view |
| `GameManager.cs` | Central state management; handles game flow, endings, and scene transitions |
| `KeypadManager.cs` | Puzzle input handling, code validation, and unlock logic |

---

## Project Structure
Assets/
Scripts/
RoomShrink.cs
ObjectShift.cs
GameManager.cs
KeypadManager.cs
Scenes/
Prefabs/
Audio/

---

## Build

The project targets Meta Quest 2 via the Android build pipeline.

- Build Target: Android
- Architecture: ARM64
- Scripting Backend: IL2CPP
- Minimum API Level: Android 10 (API 29)
- XR Plugin: Oculus / Meta XR

To build, open the project in Unity 6 (6000.3.11f1), switch the build target to Android, and build using the standard Unity build pipeline with IL2CPP and ARM64 selected.

---

## Requirements

- Unity 6 (version 6000.3.11f1 or compatible)
- Meta XR SDK (installed via Unity Package Manager)
- Android Build Support module
- A Meta Quest 2 headset for deployment and testing

---

## Notes

This project was tested and deployed on a Meta Quest 2. A demo video will be added once recording equipment is available.
