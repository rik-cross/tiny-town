//   Tiny Town, By Rik Cross
//   -- Code: github.com/rik-cross/tiny-town
//   -- Shared under the MIT licence
//   Uses the milk MonoGame ECS engine
//   -- Docs: rik-cross.github.io/monogame-milk

// The PauseScene acts as a game pause. When this scene
// is added to the scene stack on top of the current game
// scene, it stops the scene below from updating and
// allowing input, and has menu buttons for returning to
// the game or exiting to the main menu

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using milk.Core;
using milk.UI;
using milk.Transitions;

public class PauseScene : Scene
{

    private Text pausedText;

    public override void Init()
    {

        // 'Pause' the game scene below this one
        InputSceneBelow = false;
        UpdateSceneBelow = false;        

        // Semi-transparent black background
        BackgroundColor = Color.Black * 0.75f;

        // Create a 'Paused' indicator,
        // to display in the bottom-left of the scene
        pausedText = new Text(
            caption: "Paused",
            anchor: Anchor.BottomLeft,
            position: new Vector2(20, Size.Y - 5),
            color: GameSettings.primaryTextColor,
            outlineWidth: 3
        );

        // Create a back button, to return to the game
        Button btnReturn = new Button(
            caption: "Back",
            anchor: Anchor.BottomCenter,
            position: new Vector2(Middle.X, Middle.Y - (GameSettings.buttonSpacing / 2)),
            size: GameSettings.ButtonSize,
            foregroundColor: GameSettings.primaryTextColor,
            customDrawMethod: GameUI.DrawButton,
            onSelected: (UIElement element, Scene scene) =>
            {
                EngineGlobals.game.RemoveScene(
                    transition: new TransitionFadeIn(
                        duration: GameSettings.scenePauseDuration
                    )
                );
            }
        );

        // Create a button to return back to the main menu
        Button btnMenu = new Button(
            caption: "Menu",
            anchor: Anchor.TopCenter,
            // Button is positioned relative to the btnReturn button above
            parent: btnReturn,
            position: new Vector2(0, GameSettings.buttonSpacing),
            size: GameSettings.ButtonSize,
            customDrawMethod: GameUI.DrawButton,
            foregroundColor: GameSettings.primaryTextColor,
            onSelected: (UIElement element, Scene scene) =>
            {
                EngineGlobals.game.SetScene(
                    [
                        GameAssets.menuScene,
                        GetSceneBelow()
                    ],
                    new TransitionFadeToBlack(duration: GameSettings.sceneTransitionDuration)
                );
            }
        );

        // Link the buttons together (one above / below the other)
        btnReturn.ElementBelow = btnMenu;

        // Add the buttons to the menu
        AddUIElement(btnReturn);
        AddUIElement(btnMenu);

    }

    public override void Input(GameTime gameTime)
    {

        // The [Esc] key also returns to the game
        if (game.inputManager.IsKeyPressed(Keys.Escape))
            game.RemoveScene(transition: new TransitionFadeIn(duration: 100));
    
    }

    public override void Draw()
    {
    
        // Draw the 'Paused' text
        pausedText.Draw();
    
    }

}
