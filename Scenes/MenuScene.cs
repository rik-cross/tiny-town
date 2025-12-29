//   Tiny Town, By Rik Cross
//   -- Code: github.com/rik-cross/tiny-town
//   -- Shared under the MIT licence
//   Uses the milk MonoGame ECS engine
//   -- Docs: rik-cross.github.io/monogame-milk

using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using milk.Core;
using milk.UI;
using milk.Transitions;
using MonoGame.Extended.Screens.Transitions;

public class MenuScene : Scene
{

    Text titleText;
    Text versionText;
    Text milkText;
    Image milkImage;

    public override void Init()
    {

        titleText = new Text(
            caption: "Tiny Town",
            anchor: Anchor.TopCenter,
            font: game._engineResources.FontLarge,
            position: new Vector2(Middle.X, Middle.Y - 320),
            outlineWidth: 6,
            color: GameSettings.primaryTextColor,
            outlineColor: Color.Black
        );

        versionText = new Text(
            caption: "Tiny Town v0.1",
            anchor: Anchor.BottomLeft,
            position: new Vector2(20, Size.Y - 5),
            outlineWidth: 3,
            color: GameSettings.primaryTextColor,
            outlineColor: Color.Black
        );

        milkText = new Text(
            caption: "Made with milk",
            anchor: Anchor.BottomCenter,
            position: new Vector2(Size.X - 95, Size.Y - 5),
            outlineWidth: 3,
            color: GameSettings.primaryTextColor,
            outlineColor: Color.Black
        );

        milkImage = new Image(
            texture: game._engineResources.ImgMilk,
            size: new Vector2(205 / 8, 265 / 8),
            position: new Vector2(Size.X - 200, Size.Y - 13),
            anchor: Anchor.BottomCenter
        );

        // test
        Button newGame = new Button(
            caption: "Play",
            anchor: Anchor.TopCenter,
            position: new Vector2(Middle.X, Middle.Y + 120),
            size: GameSettings.ButtonSize,
            foregroundColor: GameSettings.primaryTextColor,
            customDrawMethod: GameUI.DrawButton,
            onSelected: (UIElement element, Scene scene) =>
            {
                GameAssets.villageScene.GetCameraByName("main camera").SetZoom(4.5f, 2.0f);
                game.RemoveScene(transition: new TransitionFadeIn(duration: 500));
            }
        );

        // test
        Button info = new Button(
            caption: "Info",
            anchor: Anchor.TopCenter,
            position: new Vector2(0, newGame.Height + 10),
            size: GameSettings.ButtonSize,
            foregroundColor: GameSettings.primaryTextColor,
            parent: newGame,
            customDrawMethod: GameUI.DrawButton,
            onSelected: (UIElement element, Scene scene) =>
            {
                game.SetScene(
                    GameAssets.infoScene,
                    transition: new TransitionFadeToBlack(duration: 500),
                    keepExistingScenes: true
                );
            }
        );

        // test
        Button quit = new Button(
            caption: "Quit",
            anchor: Anchor.TopCenter,
            position: new Vector2(0, info.Height + 10),
            size: GameSettings.ButtonSize,
            foregroundColor: GameSettings.primaryTextColor,
            parent: info,
            customDrawMethod: GameUI.DrawButton,
            onSelected: (UIElement element, Scene scene) => { EngineGlobals.game.Exit(); }
        );

        newGame.ElementBelow = info;
        info.ElementBelow = quit;

        AddUIElement(newGame);
        AddUIElement(info);
        AddUIElement(quit);

    }

    public override void Input(GameTime gameTime)
    {
        // Press [Esc] to quit
        if (game.inputManager.IsKeyPressed(Keys.Escape))
            game.Quit();
    }

    public override void Draw()
    {
        titleText.Draw();
        versionText.Draw();
        milkText.Draw();
        milkImage.Draw();
    }

}