//   Tiny Town, By Rik Cross
//   -- Code: github.com/rik-cross/tiny-town
//   -- Shared under the MIT licence
//   Uses the milk MonoGame ECS engine
//   -- Docs: rik-cross.github.io/monogame-milk

using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using milk.Core;
using milk.Components;

public static class HouseEntity
{

    public static Texture2D houseSpriteTexture = EngineGlobals.game.Content.Load<Texture2D>("images/house");
    //public static List<List<Texture2D>> houseImagesList = milk.Utilities.SplitTexture(houseSpriteSheet, new Vector2(48, 48));

    public static Entity Create(Vector2 position)
    {
        Entity houseEntity = new Entity();

        houseEntity.AddComponent(
            new TransformComponent(
                position: position,
                size: new Vector2(92, 71)
            )
        );

        houseEntity.AddComponent(
            new ColliderComponent(
                size: new Vector2(90, 71 - 32),
                offset: new Vector2(1, 32)
            )
        );

        houseEntity.AddComponent(new SpriteComponent());

        houseEntity.GetComponent<SpriteComponent>().AddSprite(
            sprite: new Sprite(
                textureList: new List<Texture2D>() {
                    houseSpriteTexture
                },
                resizeToEntity: false,
                offset: new Vector2(-11, -9)
            ),
            state: "default"
        );

        houseEntity.AddComponent(
            new TriggerComponent(
                size: new Vector2(10, 5),
                offset: new Vector2(40, 71 - 10),
                onCollisionEnter: (Entity entity1, Entity entity2, float distance) =>
                {
                    if (entity2.Name != "player")
                        return;

                    GameUtils.ChangePlayerScene(
                        fromScene: GameAssets.villageScene,
                        toScene: GameAssets.houseScene,
                        playerPosition: new Vector2(6 * 16 + 1, 7 * 16),
                        playerState: "idle_up"
                    );
                }
            )
        );

        houseEntity.State = "default";

        return houseEntity;
    }

}