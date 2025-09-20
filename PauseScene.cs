using System;
using System.Data;
using Microsoft.Xna.Framework;
using MonoGameECS;
using MonoGameECS.Transitions;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

public class PauseScene : Scene
{

    private Text instructionText;

    public override void Init()
    {
        // Semi-transparent black background
        backgroundColor = Color.Black * 0.75f;

        instructionText = new Text(
            caption: "Paused! Press [Esc]",
            font: content.Load<SpriteFont>("Fonts/Medium"),
            position: Middle,
            anchor: Anchor.MiddleCenter,
            color: Color.White,
            outlineWidth: 3
        );

        UpdateSceneBelow = false;

    }

    public override void Input(GameTime gameTime) {
        // Press [Esc] to move back to the previous scene
        if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            game.RemoveScene(new TransitionFadeIn(duration: 100));
    }

    public override void Draw() {
        instructionText.Draw();
    }

}
