using System;

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

        // TODO: set zoom to a global value?
        // Track the new player
        toScene.GetCameraByName("main camera").TrackedEntity = playerEntity;

        // Set player state
        playerEntity.State = playerState;

        // Initiate the scene transition
        fromScene.game.SetScene(
            toScene,
            new TransitionFadeToBlack(400)
        );

    }
}