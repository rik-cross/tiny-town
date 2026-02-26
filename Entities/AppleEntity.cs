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


public static class AppleEntity
{

    public static Texture2D itemSpriteSheet = Milk.Content.Load<Texture2D>("images/items");
    public static Texture2D appleTexture = Utilities.GetSubTexture(itemSpriteSheet, 16, 64, 16, 16);
    public static Texture2D itemShadowTexture = Milk.Content.Load<Texture2D>("images/item-shadow");

    public static Entity Create(Vector2? position = null)
    {
        Entity appleEntity = new Entity(
            type: "apple",
            tags: new List<string>() {"apple"}
        );

        appleEntity.AddComponent(
            new TransformComponent(
                position: position ?? Vector2.Zero,
                size: new Vector2(16, 14)
            )
        );

        appleEntity.AddComponent(
            new TriggerComponent(
                size: new Vector2(16, 14)
            )
        );

        appleEntity.AddComponent(new SpriteComponent(appleTexture));
        appleEntity.GetComponent<SpriteComponent>().AddTextures(itemShadowTexture);

        // Set apple inventory data
        Milk.Systems.GetSystem<InventorySystem>().SetStackSize("apple", 10);
        Milk.Systems.GetSystem<InventorySystem>().SetTexture("apple", appleTexture);

        appleEntity.AddComponent(new CollectableComponent());

        appleEntity.State = "default";

        return appleEntity;
    }

}