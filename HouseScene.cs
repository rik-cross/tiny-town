using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using milk.Core;
using milk.Transitions;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

using MonoGame.Extended;
using MonoGame.Extended.Tiled;
using MonoGame.Extended.Tiled.Renderers;

public class HouseScene : Scene
{

    public override void Init()
    {

        BackgroundColor = Color.CornflowerBlue;
        Map = content.Load<TiledMap>("images/Maps/House");
        EntitySortMethod = EntitySortMethods.SortBottom;

        // Add player
        //milk.Entity player = PlayerEntity.Create();
        // TODO: move the player between scenes
        //AddEntity(GameAssets.playerEntity);

        //
        // Add cameras
        //

        AddCamera(
            new Camera(
                screenSize: Size,
                backgroundColor: new Color(155, 212, 195),
                clampToMap: true,
                worldPosition: new Vector2(16 * 16, 16 * 16),
                name: "main camera",
                zoom: 4.0f
            )
        );

        AddEntity(
            TriggerEntity.Create(
                position: new Vector2(16 * 6, 16 * 8 + 8),
                size: new Vector2(16, 8),
                onCollisionEnter: (Entity entity1, Entity entity2, float distance) =>
                {
                    GameUtils.ChangePlayerScene(
                        fromScene: this,
                        toScene: GameAssets.villageScene,
                        playerPosition: new Vector2(16 * 9 + 1, 16 * 11),
                        playerState: "idle_down"
                    );
                }
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

        // Press [P] to 'pause'
        if (game.inputManager.IsKeyPressed(Keys.P))
        {
            game.SetScene(
                new PauseScene(),
                new TransitionFadeIn(duration: 100),
                keepExistingScenes: true
            );
        }

        // Press [Escape] to quit
        if (game.inputManager.IsKeyPressed(Keys.Escape))
            game.Quit();

    }

}
