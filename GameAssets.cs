//   Tiny Town, By Rik Cross
//   -- Code: github.com/rik-cross/tiny-town
//   -- Shared under the MIT licence
//   Uses the milk MonoGame ECS engine
//   -- Docs: rik-cross.github.io/monogame-milk

using Microsoft.Xna.Framework.Graphics;
using milk.Core;

public static class GameAssets
{

    // Entities
    public static Entity playerEntity = PlayerEntity.Create();

    // Scenes
    public static Scene menuScene = new MenuScene();
    public static Scene infoScene = new InfoScene();
    public static Scene villageScene = new GameScene();
    public static Scene houseScene = new HouseScene();

    // Images
    public static Texture2D buttonSheet = Milk.Content.Load<Texture2D>("images/UI/buttons");
    public static Texture2D indicators = Milk.Content.Load<Texture2D>("images/UI/indicators");

}