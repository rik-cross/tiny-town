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


public static class TreeEntity
{

    public static Texture2D treeSpriteSheet = Milk.Content.Load<Texture2D>("images/tree");
    public static List<List<Texture2D>> treeImagesList = Utilities.SplitTexture(treeSpriteSheet, new Vector2(48, 48));

    public static Entity Create(Vector2 position)
    {
        Entity treeEntity = new Entity();

        treeEntity.AddComponent(
            new TransformComponent(
                position: position,
                size: new Vector2(24, 30)
            )
        );

        treeEntity.AddComponent(
            new ColliderComponent(
                size: new Vector2(10, 5),
                offset: new Vector2(7, 25)
            )
        );

        treeEntity.AddComponent(new SpriteComponent(
            treeImagesList[0][0],
            offset: new Vector2(12, 13)
        ));

        /*treeEntity.GetComponent<SpriteComponent>().AddSprite(
            sprite: new Sprite(
                textureList: new List<Texture2D>() {
                    treeImagesList[0][0]
                },
                resizeToEntity: false,
                offset: new Vector2(-12, -13)
            ),
            state: "default"
        );*/

        treeEntity.State = "default";

        return treeEntity;
    }

}