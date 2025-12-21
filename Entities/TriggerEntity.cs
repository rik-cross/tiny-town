using System;
using Microsoft.Xna.Framework;

using milk.Core;
using milk.Components;

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