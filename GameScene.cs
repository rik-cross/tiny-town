using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using milk;
using milk.Transitions;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

using MonoGame.Extended;
using MonoGame.Extended.Tiled;
using MonoGame.Extended.Tiled.Renderers;

public class GameScene : Scene
{

    public override void Init()
    {

        BackgroundColor = Color.CornflowerBlue;
        Map = content.Load<TiledMap>("images/Maps/Village");
        
        // Add player
        milk.Entity player = PlayerEntity.Create();
        AddEntity(player);

        //
        // Add cameras
        //

        AddCamera(
            new Camera(
                size: Size,
                backgroundColor: new Color(155, 212, 195),
                clampToMap: true,
                worldPosition: new Vector2(16 * 16, 16 * 16),
                name: "main camera",
                zoom: 2.0f,
                trackedEntity: player
            )
        );

        AddCamera(
            new Camera(
                screenPosition: new Vector2(Size.X - 220, Size.Y - 220),
                size: new Vector2(200, 200),
                clampToMap: true,
                backgroundColor: new Color(155, 212, 195),
                borderWidth: 3,
                borderColor: Color.Black,
                worldPosition: new Vector2(24 * 16, 24 * 16),
                zoom: 0.24f,
                name: "minimap"
            )
        );

    }

    public override void Input(GameTime gameTime)
    {

        //
        // Main camera controls
        //

        // Number keys to set the main camera zoom level
        if (Keyboard.GetState().IsKeyDown(Keys.D1))
            GetCameraByName("main camera").SetZoom(1.0f, duration: 0.5f);
        if (Keyboard.GetState().IsKeyDown(Keys.D2))
            GetCameraByName("main camera").SetZoom(2.0f, duration: 0.5f);
        if (Keyboard.GetState().IsKeyDown(Keys.D3))
            GetCameraByName("main camera").SetZoom(3.0f, duration: 0.5f);
        if (Keyboard.GetState().IsKeyDown(Keys.D4))
            GetCameraByName("main camera").SetZoom(4.0f, duration: 0.5f);
        if (Keyboard.GetState().IsKeyDown(Keys.D5))
            GetCameraByName("main camera").SetZoom(5.0f, duration: 0.5f);

        //
        // Scene controls
        //

        // Press [P] to 'pause'
        if (Keyboard.GetState().IsKeyDown(Keys.P))
        {
            game.SetScene(
                new PauseScene(),
                new TransitionFadeIn(duration: 100),
                keepExistingScenes: true
            );
        }

        // Press [Q] to quit
        if (Keyboard.GetState().IsKeyDown(Keys.Q))
            game.Quit();

    }

}
