//   Tiny Town, By Rik Cross
//   -- Code: github.com/rik-cross/tiny-town
//   -- Shared under the MIT licence
//   Uses the milk MonoGame ECS engine
//   -- Docs: rik-cross.github.io/monogame-milk

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Tiled;
using milk.Core;
using milk.Components;
using milk.Transitions;


public class HouseScene : Scene
{

    public override void Init()
    {

        BackgroundColor = Color.CornflowerBlue;
        Map = Milk.Content.Load<TiledMap>("images/Maps/House");
        EntitySortMethod = EntitySortMethods.SortBottom;

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
                zoom: 4.0f
            )
        );

        AddEntity(
            TriggerEntity.Create(
                position: new Vector2(16 * 6, 16 * 8 + 8),
                size: new Vector2(16, 8),
                onCollisionEnter: (Entity entity1, Entity entity2, float distance) =>
                {
                    GameUtils.ChangePlayerScene(
                        fromScene: this,
                        toScene: GameAssets.villageScene,
                        playerPosition: new Vector2(16 * 9 + 2, 16 * 11),
                        playerState: "idle_down"
                    );
                }
            )
        );

    }

    public override void Input(GameTime gameTime)
    {

        // Press [Esc] to 'pause'
        if (Controls.IsKeyPressed(Keys.Escape))
        {
            Scenes.SetScene(
                new PauseScene(),
                new TransitionFadeIn(duration: GameSettings.scenePauseDuration),
                keepExistingScenes: true
            );
        }

    }

    public override void OnEnter()
    {
        GameAssets.playerEntity.GetComponent<InventoryComponent>().Visible = true;
    }

}
