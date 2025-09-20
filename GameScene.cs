using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameECS;
using MonoGameECS.Transitions;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

public class GameScene : Scene
{

    private Text instructionText;
    private Text instructionText2;

    public override void Init()
    {

        // Set a map Rect
        mapSize = new Vector2(700, 700);

        // Set the scene background colour
        backgroundColor = Color.CornflowerBlue;

        //
        // Player entity
        // 

        Texture2D playerSpriteSheet = content.Load<Texture2D>("images/character");
        List<List<Texture2D>> playerImagesList = Utilities.SplitTexture(playerSpriteSheet, new Vector2(48, 48));

        Entity playerEntity = new Entity(
            components: [
                new TransformComponent(
                    position: new Vector2(350-28, 350-32),
                    size: new Vector2(14*4, 16*4)
                ),
                new SpriteComponent()
            ]
        );

        playerEntity.GetComponent<SpriteComponent>().AddSprite(
            sprite: new Sprite(
                textureList: new List<Texture2D>() { playerImagesList[0][0], playerImagesList[0][1] },
                resizeToEntity: false,
                duration: 0.3f,
                scale: new Vector2(4, 4),
                offset: new Vector2(17 * 4, 16 * 4)
            ),
            state: "idle"
        );

        playerEntity.State = "idle";
        AddEntity(playerEntity);

        //
        // Chest entity
        // 

        Texture2D chestSpriteSheet = content.Load<Texture2D>("images/chest");
        List<List<Texture2D>> chestImagesList = Utilities.SplitTexture(chestSpriteSheet, new Vector2(48, 48));

        Entity chestEntity = new Entity(
            components: [
                new TransformComponent(
                    position: new Vector2(400, 340),
                    size: new Vector2(16 * 3, 14 * 3)
                ),
                new SpriteComponent()
            ]
        );

        chestEntity.GetComponent<SpriteComponent>().AddSprite(
            sprite: new Sprite(
                textureList: new List<Texture2D>() {
                    chestImagesList[0][0],
                    chestImagesList[0][1],
                    chestImagesList[0][2],
                    chestImagesList[0][3]
                },
                resizeToEntity: false,
                duration: 0.6f,
                scale: new Vector2(3, 3),
                offset: new Vector2(16 * 3, 18 * 3),
                loop: false
            ),
            state: "open"
        );

        chestEntity.State = "open";
        AddEntity(chestEntity);

        instructionText = new Text(
            caption: "WASD = change camera world center, 1/2/3 = zoom level",
            font: content.Load<SpriteFont>("Fonts/Medium"),
            position: new Vector2(20, Size.Y - 20),
            anchor: Anchor.BottomLeft,
            color: Color.White,
            outlineWidth: 3
        );

        instructionText2 = new Text(
            caption: "P = pause, Q = quit",
            font: content.Load<SpriteFont>("Fonts/Medium"),
            position: new Vector2(20, Size.Y),
            anchor: Anchor.BottomLeft,
            color: Color.White,
            outlineWidth: 3
        );

        AddCamera(
            new Camera(
                size: Size,
                clampToMap: true,
                worldPosition: new Vector2(350, 350),
                name: "main camera",
                zoom: 2.0f
            )
        );

        AddCamera(
            new Camera(
                screenPosition: new Vector2(Size.X - 220, Size.Y - 220),
                size: new Vector2(200, 200),
                clampToMap: true,
                backgroundColor: Color.CornflowerBlue,
                borderWidth: 3,
                borderColor: Color.Black,
                worldPosition: new Vector2(350, 350),
                zoom: 0.25f,
                name: "minimap"
            )
        );

    }

    public override void Input(GameTime gameTime)
    {

        //
        // Camera controls
        //

        // Press [WASD] to change the camera world position

        if (Keyboard.GetState().IsKeyDown(Keys.W))
        {
            GetCameraByName("main camera").SetWorldPosition(
                new Vector2(
                    GetCameraByName("main camera").GetWorldPosition().X,
                    GetCameraByName("main camera").GetWorldPosition().Y - 5
                )
            );
        }

        if (Keyboard.GetState().IsKeyDown(Keys.A))
        {
            GetCameraByName("main camera").SetWorldPosition(
                new Vector2(
                    GetCameraByName("main camera").GetWorldPosition().X - 5,
                    GetCameraByName("main camera").GetWorldPosition().Y
                )
            );
        }

        if (Keyboard.GetState().IsKeyDown(Keys.S))
        {
            GetCameraByName("main camera").SetWorldPosition(
                new Vector2(
                    GetCameraByName("main camera").GetWorldPosition().X,
                    GetCameraByName("main camera").GetWorldPosition().Y + 5
                )
            );
        }

        if (Keyboard.GetState().IsKeyDown(Keys.D))
        {
            GetCameraByName("main camera").SetWorldPosition(
                new Vector2(
                    GetCameraByName("main camera").GetWorldPosition().X + 5,
                    GetCameraByName("main camera").GetWorldPosition().Y
                )
            );
        }

        // Number keys 1-3 set the main camera zoom level
        if (Keyboard.GetState().IsKeyDown(Keys.D1))
            GetCameraByName("main camera").SetZoom(1.0f, duration: 0.5f);
        if (Keyboard.GetState().IsKeyDown(Keys.D2))
            GetCameraByName("main camera").SetZoom(2.0f, duration: 0.5f);
        if (Keyboard.GetState().IsKeyDown(Keys.D3))
            GetCameraByName("main camera").SetZoom(3.0f, duration: 0.5f);

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

    public override void Draw()
    {
        instructionText.Draw();
        instructionText2.Draw();
    }

}
