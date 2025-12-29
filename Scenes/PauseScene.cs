//   Tiny Town, By Rik Cross
//   -- Code: github.com/rik-cross/tiny-town
//   -- Shared under the MIT licence
//   Uses the milk MonoGame ECS engine
//   -- Docs: rik-cross.github.io/monogame-milk

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using milk.Core;
using milk.UI;
using milk.Transitions;

public class PauseScene : Scene
{

    private Text instructionText;

    public override void Init()
    {
        // Semi-transparent black background
        BackgroundColor = Color.Black * 0.75f;

        instructionText = new Text(
            caption: "Paused",
            anchor: Anchor.BottomLeft,
            position: new Vector2(20, Size.Y - 5),
            color: GameSettings.primaryTextColor,
            outlineWidth: 3
        );

        InputSceneBelow = false;
        UpdateSceneBelow = false;

        Button btnReturn = new Button(
            caption: "Back",
            anchor: Anchor.BottomCenter,
            position: new Vector2(Middle.X, Middle.Y - 5),
            size: GameSettings.ButtonSize,
            foregroundColor: GameSettings.primaryTextColor,
            customDrawMethod: GameUI.DrawButton,
            onSelected: (UIElement element, Scene scene) => { EngineGlobals.game.RemoveScene(transition: new TransitionFadeIn(duration: 100)); }
        );

        Button btnMenu = new Button(
            caption: "Menu",
            anchor: Anchor.TopCenter,
            position: new Vector2(Middle.X, Middle.Y + 5),
            size: GameSettings.ButtonSize,
            customDrawMethod: GameUI.DrawButton,
            foregroundColor: GameSettings.primaryTextColor,
            onSelected: (UIElement element, Scene scene) => {
                EngineGlobals.game.SetScene(
                    [
                        GameAssets.menuScene,
                        GetSceneBelow()
                    ],
                    new TransitionFadeToBlack(duration: 500)
                );
            }
        );

        btnReturn.ElementBelow = btnMenu;

        AddUIElement(btnReturn);
        AddUIElement(btnMenu);

    }

    public override void Input(GameTime gameTime)
    {
        
        // Press [P] to move back to the previous scene
        //if (game.inputManager.IsKeyPressed(Keys.P))
        //    game.RemoveScene(new TransitionFadeIn(duration: 100));

        // Press [Esc] to return
        if (game.inputManager.IsKeyPressed(Keys.Escape))
            game.RemoveScene(transition: new TransitionFadeIn(duration: 100));

    }

    public override void Draw()
    {
        instructionText.Draw();
    }

}
