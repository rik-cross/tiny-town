//   Tiny Town, By Rik Cross
//   -- Code: github.com/rik-cross/tiny-town
//   -- Shared under the MIT licence
//   Uses the milk MonoGame ECS engine
//   -- Docs: rik-cross.github.io/monogame-milk

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Tiled;
using milk.Core;
using milk.Components;
using milk.Transitions;
using System;

public class GameScene : Scene
{
    public override void Init()
    {

        BackgroundColor = Color.CornflowerBlue;
        Map = Milk.Content.Load<TiledMap>("images/Maps/Village");
        EntitySortMethod = EntitySortMethods.SortBottom;

        // Add player
        AddEntity(GameAssets.playerEntity);

        // Add a house
        AddEntity(HouseEntity.Create(new Vector2(107, 104)));

        // Add trees
        AddEntity(TreeEntity.Create(new Vector2(210, 140)));
        AddEntity(TreeEntity.Create(new Vector2(230, 120)));

        //
        // Add camera
        //

        AddCamera(
            new Camera(
                screenSize: Size,
                backgroundColor: new Color(155, 212, 195),
                clampToMap: true,
                name: "main camera",
                zoom: 1.3f,
                trackedEntity: GameAssets.playerEntity,
                followPercentage: 0.05f
            )
        );
        
    }

    public override void Input(GameTime gameTime)
    {

        // Press [Esc] to 'pause'
        if (game.inputManager.IsKeyPressed(Keys.Escape))
        {
            Scenes.SetScene(
                new PauseScene(),
                new TransitionFadeIn(duration: GameSettings.scenePauseDuration),
                keepExistingScenes: true
            );
        }

    }

}
