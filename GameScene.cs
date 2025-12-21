using Microsoft.Xna.Framework;
using milk.Core;
using milk.Transitions;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Tiled;


public class GameScene : Scene
{
    public override void Init()
    {

        BackgroundColor = Color.CornflowerBlue;
        Map = content.Load<TiledMap>("images/Maps/Village");
        EntitySortMethod = EntitySortMethods.SortBottom;
        
        // Add player
        AddEntity(GameAssets.playerEntity);

        // Add a house
        AddEntity(HouseEntity.Create(new Vector2(107, 104)));

        // Add trees
        AddEntity(TreeEntity.Create(new Vector2(210, 140)));
        AddEntity(TreeEntity.Create(new Vector2(230, 120)));

        //
        // Add cameras
        //

        AddCamera(
            new Camera(
                screenSize: Size,
                backgroundColor: new Color(155, 212, 195),
                clampToMap: true,
                worldPosition: new Vector2(16 * 16, 16 * 16),
                name: "main camera",
                zoom: 4.0f,
                trackedEntity: GameAssets.playerEntity,
                followPercentage: 0.05f
            )
        );

        AddCamera(
            new Camera(
                screenPosition: new Vector2(Size.X - 220, Size.Y - 220),
                screenSize: new Vector2(200, 200),
                clampToMap: true,
                backgroundColor: new Color(155, 212, 195),
                borderWidth: 3,
                borderColor: Color.Black,
                zoom: 0.35f,
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
        if (game.inputManager.IsKeyPressed(Keys.D1))
            GetCameraByName("main camera").SetZoom(1.0f, duration: 0.5f);
        if (game.inputManager.IsKeyPressed(Keys.D2))
            GetCameraByName("main camera").SetZoom(2.0f, duration: 0.5f);
        if (game.inputManager.IsKeyPressed(Keys.D3))
            GetCameraByName("main camera").SetZoom(3.0f, duration: 0.5f);
        if (game.inputManager.IsKeyPressed(Keys.D4))
            GetCameraByName("main camera").SetZoom(4.0f, duration: 0.5f);
        if (game.inputManager.IsKeyPressed(Keys.D5))
            GetCameraByName("main camera").SetZoom(5.0f, duration: 0.5f);

        //
        // Scene controls
        //

        // Press [P] to 'pause'
        // TODO: OnKeyPress not OnKeyDown, or this fires too many times
        if (game.inputManager.IsKeyPressed(Keys.P))
        {
            game.SetScene(
                new PauseScene(),
                new TransitionFadeIn(duration: 100),
                keepExistingScenes: true
            );
        }

        // Press [Q] to quit
        if (game.inputManager.IsKeyPressed(Keys.Escape))
            game.Quit();

    }

}
