//   Tiny Town, By Rik Cross
//   -- Code: github.com/rik-cross/tiny-town
//   -- Shared under the MIT licence
//   Uses the milk MonoGame ECS engine
//   -- Docs: rik-cross.github.io/monogame-milk

using System;
using Microsoft.Xna.Framework;
using milk.Core;
using milk.Components;
using milk.Transitions;
using milk.UI;
using MonoGame.Extended;
using Microsoft.Xna.Framework.Graphics;

public static class GameUI
{
    
    // Button elements

    public static Texture2D buttonTopLeft = Utilities.GetSubTexture(
        GameAssets.buttonSheet,
        59, 71, 5, 5
    );
    public static Texture2D buttonTopMiddle = Utilities.GetSubTexture(
        GameAssets.buttonSheet,
        59 + 5, 71, 1, 5
    );
    public static Texture2D buttonTopRight = Utilities.GetSubTexture(
        GameAssets.buttonSheet,
        80, 71, 5, 5
    );
    public static Texture2D buttonMiddleLeft = Utilities.GetSubTexture(
        GameAssets.buttonSheet,
        59, 71 + 5, 5, 1
    );
    public static Texture2D buttonMiddleRight = Utilities.GetSubTexture(
        GameAssets.buttonSheet,
        80, 71 + 5, 5, 1
    );
    public static Texture2D buttonBottomLeft = Utilities.GetSubTexture(
        GameAssets.buttonSheet,
        59, 84, 5, 5
    );
    public static Texture2D buttonBottomRight = Utilities.GetSubTexture(
        GameAssets.buttonSheet,
        80, 84, 5, 5
    );
    public static Texture2D buttonBottomMiddle = Utilities.GetSubTexture(
        GameAssets.buttonSheet,
        59 + 5, 84, 1, 5
    );
    public static Texture2D buttonCenter = Utilities.GetSubTexture(
        GameAssets.buttonSheet,
        59 + 5, 71 + 5, 1, 1
    );

    // Indicator elements
    public static Texture2D indicatorTopLeft = Utilities.GetSubTexture(
        GameAssets.indicators,
        0, 0, 8, 9
    );  
    public static Texture2D indicatorTopRight = Utilities.GetSubTexture(
        GameAssets.indicators,
        16, 0, 8, 9
    );  
    public static Texture2D indicatorBottomLeft = Utilities.GetSubTexture(
        GameAssets.indicators,
        0, 16, 8, 9
    );  
    public static Texture2D indicatorBottomRight = Utilities.GetSubTexture(
        GameAssets.indicators,
        16, 16, 8, 9
    );  


    public static void DrawButton(UIElement element, bool selected = false)
    {

        Color fg = selected == true ? element.SelectedForegroundColor : element.ForegroundColor;

        Vector2 pp = element.GetPositionIncludingParent(element.Position);
        Vector2 position = element.CalculateTopLeftPositionFromAnchor(pp);

        DrawButtonDown(position, element.Size, borderScale: 3, alpha: element.Active == true ? element.Alpha : element.Alpha * 0.5f);

        // Button center - half text size
        Vector2 stringSize = element.Font.MeasureString(element.Caption);
        Vector2 textPos = new Vector2(
            position.X + (element.Size.X / 2) - (stringSize.X / 2),
            position.Y + (element.Size.Y / 2) - (stringSize.Y / 2)
        );

        Milk.Graphics.DrawString(
            element.Font,
            element.Caption,
            textPos,
            element.Active == true ? fg * element.Alpha : fg * element.Alpha * 0.5f
        );

        if (selected == true)
            DrawButtonIndicators(element, position, borderScale: 3);
    }

    public static void DrawButtonIndicators(UIElement element, Vector2 position, int borderScale = 1)
    {

        int currentActiveBorder = (int)(Math.Sin(element.parentScene.elapsedTime * 6) * 2 * borderScale);

        Milk.Graphics.Draw(
            indicatorTopLeft,
            new Rectangle(
                (int)position.X - currentActiveBorder - 4 * borderScale,
                (int)position.Y - currentActiveBorder - 5 * borderScale,
                8 * borderScale,
                9 * borderScale
            ),
            Color.White
        );

        Milk.Graphics.Draw(
            indicatorTopRight,
            new Rectangle(
                (int)(position.X + element.Size.X + currentActiveBorder - 4 * borderScale),
                (int)(position.Y - 9 * borderScale - currentActiveBorder + 5 * borderScale),
                8 * borderScale,
                9 * borderScale
            ),
            Color.White
        );

        Milk.Graphics.Draw(
            indicatorBottomLeft,
            new Rectangle(
                (int)position.X - currentActiveBorder - 4 * borderScale,
                (int)(position.Y + element.Size.Y + currentActiveBorder - 4 * borderScale),
                8 * borderScale,
                9 * borderScale
            ),
            Color.White
        );

        Milk.Graphics.Draw(
            indicatorBottomRight,
            new Rectangle(
                (int)(position.X + element.Size.X + currentActiveBorder - 4 * borderScale),
                (int)(position.Y + element.Size.Y + currentActiveBorder - 4 * borderScale),
                8 * borderScale,
                9 * borderScale
            ),
            Color.White
        );

    }

    public static void DrawButtonDown(Vector2 position, Vector2 size, int borderScale = 1, float alpha = 1.0f)
    {

        Milk.Graphics.Draw(
            buttonTopLeft,
            new Rectangle((int)position.X, (int)position.Y, 5 * borderScale, 5 * borderScale),
            Color.White * alpha
        );        
        
        Milk.Graphics.Draw(
            buttonTopMiddle,
            new Rectangle(
                (int)position.X + (5 * borderScale),
                (int)position.Y,
                (int)(size.X - 2 * 5 * borderScale),
                5 * borderScale
            ),
            Color.White * alpha
        );

        Milk.Graphics.Draw(
            buttonTopRight,
            new Rectangle(
                (int)(position.X + (size.X - (5 * borderScale))),
                (int)position.Y,
                5 * borderScale,
                5 * borderScale
            ),
            Color.White * alpha
        );

        Milk.Graphics.Draw(
            buttonMiddleLeft,
            new Rectangle(
                (int)position.X,
                (int)(position.Y + 5 * borderScale),
                5 * borderScale,
                (int)(size.Y - 2 * 5 * borderScale)
            ),
            Color.White * alpha
        );

        Milk.Graphics.Draw(
            buttonMiddleRight,
            new Rectangle(
                (int)(position.X + (size.X - (5 * borderScale))),
                (int)(position.Y + 5 * borderScale),
                5 * borderScale,
                (int)(size.Y - 2 * 5 * borderScale)
            ),
            Color.White * alpha
        );

        Milk.Graphics.Draw(
            buttonBottomLeft,
            new Rectangle(
                (int)position.X,
                (int)(position.Y + (size.Y - (5 * borderScale))),
                5 * borderScale,
                5 * borderScale
            ),
            Color.White * alpha
        );

        Milk.Graphics.Draw(
            buttonBottomRight,
            new Rectangle(
                (int)(position.X + (size.X - (5 * borderScale))),
                (int)(position.Y + (size.Y - (5 * borderScale))),
                5 * borderScale,
                5 * borderScale
            ),
            Color.White * alpha
        );

        Milk.Graphics.Draw(
            buttonBottomMiddle,
            new Rectangle(
                (int)(position.X + (5 * borderScale)),
                (int)(position.Y + (size.Y - (5 * borderScale))),
                (int)(size.X - 2 * 5 * borderScale),
                5 * borderScale
            ),
            Color.White * alpha
        );

        Milk.Graphics.Draw(
            buttonCenter,
            new Rectangle(
                (int)(position.X + (5 * borderScale)),
                (int)(position.Y + (5 * borderScale)),
                (int)(size.X - 2 * 5 * borderScale),
                (int)(size.Y - 2 * 5 * borderScale)
            ),
            Color.White * alpha
        );

    }

}