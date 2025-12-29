//   Tiny Town, By Rik Cross
//   -- Code: github.com/rik-cross/tiny-town
//   -- Shared under the MIT licence
//   Uses the milk MonoGame ECS engine
//   -- Docs: rik-cross.github.io/monogame-milk

using Microsoft.Xna.Framework;
using milk.Core;
using milk.Transitions;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.Tiled;
using milk.UI;
using System;
using System.Linq.Expressions;
using milk.Components;


public class GameScene : Scene
{
    public override void Init()
    {

        BackgroundColor = Color.CornflowerBlue;
        Map = content.Load<TiledMap>("images/Maps/Village");
        EntitySortMethod = EntitySortMethods.SortBottom;

        // Add player
        AddEntity(GameAssets.playerEntity);

        // Add a house
        AddEntity(HouseEntity.Create(new Vector2(107, 104)));

        // Add trees
        AddEntity(TreeEntity.Create(new Vector2(210, 140)));
        AddEntity(TreeEntity.Create(new Vector2(230, 120)));

        //
        // Add cameras
        //

        AddCamera(
            new Camera(
                screenSize: Size,
                backgroundColor: new Color(155, 212, 195),
                clampToMap: true,
                //worldPosition: new Vector2(50, 350), -- // TODO, doesn't work
                name: "main camera",
                zoom: 1.3f,
                trackedEntity: GameAssets.playerEntity,
                followPercentage: 0.05f
            )
        );
        
    }

    public override void Input(GameTime gameTime)
    {
        
        //
        // Main camera controls
        //

        // Number keys to set the main camera zoom level
        if (game.inputManager.IsKeyPressed(Keys.D1))
            GetCameraByName("main camera").SetZoom(1.0f, duration: 0.5f);
        if (game.inputManager.IsKeyPressed(Keys.D2))
            GetCameraByName("main camera").SetZoom(2.0f, duration: 0.5f);
        if (game.inputManager.IsKeyPressed(Keys.D3))
            GetCameraByName("main camera").SetZoom(3.0f, duration: 0.5f);
        if (game.inputManager.IsKeyPressed(Keys.D4))
            GetCameraByName("main camera").SetZoom(4.0f, duration: 0.5f);
        if (game.inputManager.IsKeyPressed(Keys.D5))
            GetCameraByName("main camera").SetZoom(5.0f, duration: 0.5f);

        //
        // Scene controls
        //

        // Press [Esc] to 'pause'
        if (game.inputManager.IsKeyPressed(Keys.Escape))
        {
            game.SetScene(
                new PauseScene(),
                new TransitionFadeIn(duration: 100),
                keepExistingScenes: true
            );
        }

    }

}
