//   Tiny Town, By Rik Cross
//   -- Code: github.com/rik-cross/tiny-town
//   -- Shared under the MIT licence
//   Uses the milk MonoGame ECS engine
//   -- Docs: rik-cross.github.io/monogame-milk

using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using milk.Core;
using milk.Components;
using milk.Transitions;

public static class GameUtils
{
    public static void ChangePlayerScene(
        Scene fromScene,
        Scene toScene,
        Vector2 playerPosition,
        string playerState)
    {

        // Get the player entity
        Entity playerEntity = GameAssets.playerEntity;
        
        // Remove the player from the old scene
        fromScene.RemoveEntity(playerEntity);

        // Add the player to the new scene in the new position
        toScene.AddEntity(playerEntity);
        playerEntity.GetComponent<TransformComponent>().Position = playerPosition;
        // Set the camera to the correct position (instantly)
        toScene.GetCameraByName("main camera").SetWorldPosition(
            new Vector2(
                playerEntity.GetComponent<TransformComponent>().Center,
                playerEntity.GetComponent<TransformComponent>().Middle
            ),
            instant: true
        );

        // Stop the player moving by setting the velocity and acceleration to zero
        playerEntity.GetComponent<PhysicsComponent>().Velocity = Vector2.Zero;
        playerEntity.GetComponent<PhysicsComponent>().Acceleration = Vector2.Zero;

        // Track the new player
        toScene.GetCameraByName("main camera").TrackedEntity = playerEntity;

        // Set player state
        playerEntity.State = playerState;

        // Initiate the scene transition
        Milk.Scenes.SetScene(
            toScene,
            new TransitionFadeToBlack(duration: GameSettings.sceneTransitionDuration)
        );

    }

    public static void SetOverlappingEntityAlpha(Scene scene)
    {
        
        // Get player entity information
        
        Entity? player = scene.GetEntityByName("player");
        if (player == null)
            return;
        
        TransformComponent playerTransform = player.GetComponent<TransformComponent>();
        ColliderComponent playerCollider = player.GetComponent<ColliderComponent>();

        Rectangle playerRect = new Rectangle(
            (int)(playerTransform.X + playerCollider.Offset.X),
            (int)(playerTransform.Y + playerCollider.Offset.Y),
            (int)playerCollider.Size.X,
            (int)playerCollider.Size.Y
        );

        // Loop through all entities
        foreach (Entity entity in scene.GetAllEntities()) {

            if (
                entity != player &&
                (entity.HasTag("building") || entity.HasTag("tree")) &&
                entity.HasComponent<TransformComponent>() &&
                entity.HasComponent<SpriteComponent>()
            )
            {

                TransformComponent transform = entity.GetComponent<TransformComponent>();
                SpriteComponent sprite = entity.GetComponent<SpriteComponent>();

                Rectangle entityRect = new Rectangle(
                    (int)transform.X,
                    (int)transform.Y,
                    (int)transform.Width - 1,
                    (int)transform.Height
                );

                if (sprite.GetSpriteForState(entity.State) != null)
                {
                    if (playerRect.Intersects(entityRect))
                        sprite.GetSpriteForState(entity.State).Alpha = 0.5f;
                    else 
                        sprite.GetSpriteForState(entity.State).Alpha = 1.0f;
                }

            }
        }
    }

}