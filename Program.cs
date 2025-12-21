using Microsoft.Xna.Framework;
using milk.Core;

// Create a new game object
using var game = new milk.Core.Game(
    title: "Tiny Town",
    size: new Vector2(1280, 720),
    debug: false
);

// Add a scene to the game via an Init() method
game.Init = () => {
    game.SetScene(GameAssets.villageScene);
};

// Run the game
game.Run();
