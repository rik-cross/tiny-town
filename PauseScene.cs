using Microsoft.Xna.Framework;
using milk.Core;
using milk.Transitions;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

public class PauseScene : Scene
{

    private Text instructionText;

    public override void Init()
    {
        // Semi-transparent black background
        BackgroundColor = Color.Black * 0.75f;

        instructionText = new Text(
            caption: "Paused! Press [P]",
            font: content.Load<SpriteFont>("Fonts/Medium"),
            position: Middle,
            anchor: Anchor.MiddleCenter,
            color: Color.White,
            outlineWidth: 3
        );

        InputSceneBelow = false;
        UpdateSceneBelow = false;

    }

    public override void Input(GameTime gameTime)
    {
        
        // Press [P] to move back to the previous scene
        if (game.inputManager.IsKeyPressed(Keys.P))
            game.RemoveScene(new TransitionFadeIn(duration: 100));

        // Press [Q] to quit
        if (game.inputManager.IsKeyPressed(Keys.Escape))
            game.Quit();

    }

    public override void Draw()
    {
        instructionText.Draw();
    }

}
