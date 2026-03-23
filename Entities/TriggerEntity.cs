//   Tiny Town, By Rik Cross
//   -- Code: github.com/rik-cross/tiny-town
//   -- Shared under the MIT licence
//   Uses the milk MonoGame ECS engine
//   -- Docs: rik-cross.github.io/monogame-milk

using System;
using Microsoft.Xna.Framework;
using milk.Core;
using milk.Components;

/// <summary>
/// Note that a TriggerEntity is just a utility class;
/// a prototype cannot be added via Milk.Entities.AddPrototype()
/// </summary>
public static class TriggerEntity
{

    public static Entity Create(
        Vector2 position,
        Vector2 size,
        Action<Entity, Entity, float> onCollisionEnter = null,
        Action<Entity, Entity, float> onCollide = null,
        Action<Entity, Entity, float> onCollisionExit = null
    )
    {
        Entity triggerEntity = new Entity(name: "trigger");

        triggerEntity.AddComponent(
            new TransformComponent(
                position: position
            )
        );

        triggerEntity.AddComponent(
            new TriggerComponent(
                size: size,
                offset: Vector2.Zero,
                onCollisionEnter: onCollisionEnter,
                onCollide: onCollide,
                onCollisionExit: onCollisionExit
            )
        );

        return triggerEntity;
    }

}