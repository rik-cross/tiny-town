using Microsoft.Xna.Framework;
using MonoGameECS;

// Create a new game object
using var game = new MonoGameECS.Game(
    title: "MonoGame ECS Example",
    size: new Vector2(1280, 720)
);

// Add a scene to the game via an Init() method
game.Init = () => { game.SetScene(scene: new GameScene()); };

// Run the game
game.Run();

