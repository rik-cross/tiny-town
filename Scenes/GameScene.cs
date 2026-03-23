//   Tiny Town, By Rik Cross
//   -- Code: github.com/rik-cross/tiny-town
//   -- Shared under the MIT licence
//   Uses the milk MonoGame ECS engine
//   -- Docs: rik-cross.github.io/monogame-milk

using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Tiled;
using milk.Core;
using milk.Components;
using milk.Transitions;

public class GameScene : Scene
{
    public override void Init()
    {

        BackgroundColor = Color.CornflowerBlue;
        Map = Milk.Content.Load<TiledMap>("images/Maps/Village");
        EntitySortMethod = EntitySortMethods.SortBottom;

        // Add player
        AddEntity(GameAssets.playerEntity);

        // Add a house
        AddEntity(HouseEntity.Create(new Vector2(107, 104)));

        // Add trees
        AddEntity(Milk.Entities.CreateFromPrototype("tree", new Vector2(210, 140)));
        AddEntity(Milk.Entities.CreateFromPrototype("tree", new Vector2(230, 120)));

        // Add apples
        AddEntity(Milk.Entities.CreateFromPrototype("apple", new Vector2(250, 180)));
        AddEntity(Milk.Entities.CreateFromPrototype("apple", new Vector2(260, 195)));
        AddEntity(Milk.Entities.CreateFromPrototype("apple", new Vector2(255, 160)));

        // TODO: Remove this, and create a LOAD() method that's called early
        GameAssets.POIMarker = Milk.Content.Load<Texture2D>("images/UI/POIMarker");

        POIMarkers.Add(
            new POIMarker(
                targetPosition: new Vector2(107 + 46, 104 + 55),
                size: new Vector2(21, 27),
                texture: GameAssets.POIMarker,
                name: "house",
                text: "House",
                visible: false
            )
        );

        //
        // Add camera
        //

        AddCamera(
            new Camera(
                screenPosition: new Vector2(0, 0),
                screenSize: Milk.Size,
                backgroundColor: new Color(155, 212, 195),
                clampToMap: true,
                name: "main camera",
                zoom: 4.5f,
                trackedEntity: GameAssets.playerEntity,
                followPercentage: 0.05f
            )
        );
        
    }

    public override void Update(GameTime gameTime)
    {
        GameUtils.SetOverlappingEntityAlpha(this);
    }

    public override void Input(GameTime gameTime)
    {     

        // Press [Esc] to 'pause'
        if (game.inputManager.IsKeyPressed(Keys.Escape))
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
        GameAssets.playerEntity.GetComponent<CraftingComponent>().Visible = true;
        GameAssets.villageScene.POIMarkers.Show();
    }

}
