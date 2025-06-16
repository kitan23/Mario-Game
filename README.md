# Mario-Game

A fully-featured, from-scratch 2D Mario platformer built in Unity and C#. This project demonstrates advanced game development skills, including custom physics, animation, level design, and interactive gameplay mechanics inspired by the classic Super Mario Bros.

## 🎮 Features

- **Custom Player Controller:**  
  - Responsive movement, jumping, and collision detection.
  - Smooth camera follow system with level bounds.
  - State-driven animation (idle, running, jumping, bouncing).

- **Classic Mario Mechanics:**  
  - Interactive question blocks that spawn coins with bounce and sound effects.
  - Enemies with patrol, gravity, and defeat logic (including player-enemy interactions).
  - Win condition via castle trigger and loss condition via enemy or hole collision.

- **Level Design:**  
  - Multiple scenes: Main Menu, Level 1, Game Over, and Win screens.
  - Modular level built with prefabs for blocks, pipes, enemies, and environmental decorations.
  - Pixel-perfect graphics and authentic Mario-style assets and animations.

- **Audio & Visual Polish:**  
  - Custom sound effects for coins and interactions.
  - Smooth, frame-based animations for player, enemies, and items.
  - Parallax backgrounds and environmental details for immersion.

- **Extensible Architecture:**  
  - Organized C# scripts for player, enemies, items, and game management.
  - Easily add new levels, enemies, or power-ups by extending existing prefabs and scripts.

## 🛠️ Technologies Used

- **Unity Engine** (2D)
- **C#** for all gameplay logic
- **Custom pixel art** and sprite animations
- **Audio integration** for interactive feedback

## 🚀 How to Run

1. **Clone the repository:**
   ```bash
   git clone https://github.com/yourusername/Mario-Game.git
   ```
2. **Open in Unity:**  
   Use Unity Hub to open the project folder.

3. **Play:**  
   Open `Assets/Scenes/MainMenu.unity` or `Level1.unity` and press Play.

## 📂 Project Structure

- `Assets/Scripts/` — Core gameplay scripts (Player, Enemy, QuestionBlock, Camera, etc.)
- `Assets/Scenes/` — MainMenu, Level1, GameOver, Win
- `Assets/Graphics/` — Pixel art sprites and tiles
- `Assets/Animations/` — Animator controllers and animation clips
- `Assets/Sound/` — Sound effects (e.g., coin collection)
- `Assets/Resources/` — Prefabs and runtime assets

## 🧩 Key Code Highlights

- **Player.cs:**  
  Custom physics, state machine, and animation control for Mario.
- **Enemy.cs:**  
  AI for enemy movement, collision, and defeat logic.
- **QuestionBlock.cs:**  
  Interactive blocks with coin spawning and bounce animation.
- **CameraFollow.cs:**  
  Smooth camera tracking with level bounds.

## 🏆 What I Learned

- Advanced Unity 2D development and C# scripting
- Implementing classic platformer mechanics from scratch
- Game architecture, modularity, and extensibility
- Pixel art animation and audio integration
- Scene management and user experience polish

## 📸 Screenshots

*(Add gameplay screenshots here for extra impact!)*

## 🎥 Gameplay Video

<iframe width="560" height="315" src="https://www.youtube.com/embed/tI_VpxxEOI8?si=dRWnAnd8EZMp_8yI&controls=0" title="YouTube video player" frameborder="0" allow="accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture; web-share" referrerpolicy="strict-origin-when-cross-origin" allowfullscreen></iframe>

## 📜 License

This project is for educational and portfolio purposes.
