//   Tiny Town, By Rik Cross
//   -- Code: github.com/rik-cross/tiny-town
//   -- Shared under the MIT licence
//   Uses the milk MonoGame ECS engine
//   -- Docs: rik-cross.github.io/monogame-milk

using Microsoft.Xna.Framework;
using milk.Core;

// Create a new game object
using var game = new milk.Core.Game(
    title: "Tiny Town",
    size: new Vector2(1280, 720)
);

// Add scenes to the game via an Init() method
game.Init = () => {
    Milk.Scenes.SetScene([GameAssets.menuScene, GameAssets.villageScene]);
};

// Run the game
game.Run();
