//   Tiny Town, By Rik Cross
//   -- Code: github.com/rik-cross/tiny-town
//   -- Shared under the MIT licence
//   Uses the milk MonoGame ECS engine
//   -- Docs: rik-cross.github.io/monogame-milk

// The info scene displays some 'credits', inculding
// the MonoGame milk engine and various assets used

using Microsoft.Xna.Framework;
using milk.Core;
using milk.UI;
using milk.Transitions;

public class InfoScene : Scene
{

    // The text displayed is split into lines
    Text infoLine1;
    Text infoLine2;
    Text infoLine3;
    Text infoLine4;
    Text infoLine5;

    public override void Init()
    {

        BackgroundColor = GameSettings.darkBrown;

        infoLine1 = new Text(
            caption: "Tiny Town, by Rik Cross",
            anchor: Anchor.MiddleCenter,
            position: new Vector2(Middle.X, 150),
            color: GameSettings.primaryTextColor
        );

        infoLine2 = new Text(
            caption: "This game is made using the milk MonoGame engine",
            anchor: Anchor.MiddleCenter,
            position: new Vector2(Middle.X, 250),
            color: GameSettings.primaryTextColor
        );

        infoLine3 = new Text(
            caption: "rik-cross.github.io/monogame-milk",
            anchor: Anchor.MiddleCenter,
            position: new Vector2(Middle.X, 280),
            color: GameSettings.primaryTextColor
        );

        infoLine4 = new Text(
            caption: "Graphics: Sprout Lands, by Cup Nooble",
            anchor: Anchor.MiddleCenter,
            position: new Vector2(Middle.X, 340),
            color: GameSettings.primaryTextColor
        );

        infoLine5 = new Text(
            caption: "cupnooble.itch.io",
            anchor: Anchor.MiddleCenter,
            position: new Vector2(Middle.X, 370),
            color: GameSettings.primaryTextColor
        );

        Button back = new Button(
            caption: "Back",
            anchor: Anchor.TopCenter,
            position: new Vector2(Middle.X, Middle.Y + 250),
            size: GameSettings.ButtonSize,
            foregroundColor: GameSettings.primaryTextColor,
            customDrawMethod: GameUI.DrawButton,
            onSelected: (UIElement element, Scene scene) =>
            {
                Scenes.RemoveScene(
                    transition: new TransitionFadeToBlack(
                        duration: GameSettings.sceneTransitionDuration
                    )
                );
            }
        );

        AddUIElement(back);

    }

    public override void Draw()
    {
        // Draw the lines of text
        infoLine1.Draw();
        infoLine2.Draw();
        infoLine3.Draw();
        infoLine4.Draw();
        infoLine5.Draw();
    }

}