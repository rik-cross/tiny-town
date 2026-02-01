//   Tiny Town, By Rik Cross
//   -- Code: github.com/rik-cross/tiny-town
//   -- Shared under the MIT licence
//   Uses the milk MonoGame ECS engine
//   -- Docs: rik-cross.github.io/monogame-milk

// The menu scene is designed to be overlayed onto a game
// scene, and includes a title, ome info text and buttons
// for starting a game, displaying some credits and exiting.

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using milk.Core;
using milk.Transitions;
using milk.UI;

public class MenuScene : Scene
{

    // 'Tiny Town' title
    Text txtTitle;

    // Game version text
    Text txtVersion;

    // The 'made with milk' text and logo
    Text txtMilk;
    Image imgMilk;

    public override void Init()
    {

        txtTitle = new Text(
            caption: "Tiny Town",
            anchor: Anchor.TopCenter,
            font: game._engineResources.FontLarge,
            position: new Vector2(Middle.X, Middle.Y - 320),
            outlineWidth: 6,
            color: GameSettings.primaryTextColor,
            outlineColor: Color.Black
        );

        txtVersion = new Text(
            caption: "Tiny Town v0.1.4",
            anchor: Anchor.BottomLeft,
            position: new Vector2(20, Size.Y - 5),
            outlineWidth: 3,
            color: GameSettings.primaryTextColor,
            outlineColor: Color.Black
        );

        txtMilk = new Text(
            caption: "Made with milk",
            anchor: Anchor.BottomCenter,
            position: new Vector2(Size.X - 95, Size.Y - 5),
            outlineWidth: 3,
            color: GameSettings.primaryTextColor,
            outlineColor: Color.Black
        );

        imgMilk = new Image(
            texture: game._engineResources.ImgMilk,
            size: new Vector2(205 / 8, 265 / 8),
            position: new Vector2(Size.X - 200, Size.Y - 13),
            anchor: Anchor.BottomCenter
        );

        // Start the game
        Button btnPlay = new Button(
            caption: "Play",
            anchor: Anchor.TopCenter,
            position: new Vector2(Middle.X, Middle.Y + 120),
            size: GameSettings.ButtonSize,
            foregroundColor: GameSettings.primaryTextColor,
            customDrawMethod: GameUI.DrawButton,
            onSelected: (UIElement element, Scene scene) =>
            {
                GameAssets.villageScene.GetCameraByName("main camera").SetZoom(4.5f, 2.0f);
                Scenes.RemoveScene(transition: new TransitionFadeIn(
                    duration: GameSettings.sceneTransitionDuration)
                );
            }
        );

        // Display some game credits
        Button btnCredits = new Button(
            caption: "Credits",
            anchor: Anchor.TopCenter,
            position: new Vector2(0, btnPlay.Height + GameSettings.buttonSpacing),
            size: GameSettings.ButtonSize,
            foregroundColor: GameSettings.primaryTextColor,
            parent: btnPlay,
            customDrawMethod: GameUI.DrawButton,
            onSelected: (UIElement element, Scene scene) =>
            {
                Scenes.SetScene(
                    GameAssets.infoScene,
                    transition: new TransitionFadeToBlack(
                        duration: GameSettings.sceneTransitionDuration
                    ),
                    keepExistingScenes: true
                );
            }
        );

        // Exit the game
        Button btnQuit = new Button(
            caption: "Quit",
            anchor: Anchor.TopCenter,
            position: new Vector2(0, btnCredits.Height + GameSettings.buttonSpacing),
            size: GameSettings.ButtonSize,
            foregroundColor: GameSettings.primaryTextColor,
            parent: btnCredits,
            customDrawMethod: GameUI.DrawButton,
            onSelected: (UIElement element, Scene scene) => { Quit(); }
        );

        // Link the buttons in the menu
        btnPlay.ElementBelow = btnCredits;
        btnCredits.ElementBelow = btnQuit;

        // Add the buttons to the menu
        AddUIElement(btnPlay);
        AddUIElement(btnCredits);
        AddUIElement(btnQuit);

    }

    public override void Input(GameTime gameTime)
    {

        // Pressing [Esc] also quits the game
        if (Controls.IsKeyPressed(Keys.Escape))
            Quit();
    
    }

    public override void Draw()
    {
    
        // Draw the scene title, text and image
        txtTitle.Draw();
        txtVersion.Draw();
        txtMilk.Draw();
        imgMilk.Draw();
    
    }

}