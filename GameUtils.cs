//   Tiny Town, By Rik Cross
//   -- Code: github.com/rik-cross/tiny-town
//   -- Shared under the MIT licence
//   Uses the milk MonoGame ECS engine
//   -- Docs: rik-cross.github.io/monogame-milk

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

        // Track the player in the new scene
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

    public static Vector2 CalculateEntityDropPosition(Scene scene, Entity entity, Entity removedEntity)
    {
        string st = entity.State.Split("_")[1].ToLower();

        if (st == "left")
        {
            float left = entity.GetComponent<TransformComponent>().Left - removedEntity.GetComponent<TransformComponent>().Width - 2;
            float top = entity.GetComponent<TransformComponent>().Bottom - removedEntity.GetComponent<TransformComponent>().Height;
            return new Vector2(left, top);
        }
        if (st == "right")
        {
            float left = entity.GetComponent<TransformComponent>().Left + removedEntity.GetComponent<TransformComponent>().Width + 2;
            float top = entity.GetComponent<TransformComponent>().Bottom - removedEntity.GetComponent<TransformComponent>().Height;
            return new Vector2(left, top);
        }
        if (st == "up")
        {
            float left = entity.GetComponent<TransformComponent>().Center - removedEntity.GetComponent<TransformComponent>().Width / 2;
            float top = entity.GetComponent<TransformComponent>().Top - removedEntity.GetComponent<TransformComponent>().Height - 2;
            return new Vector2(left, top);
        }
        if (st == "down")
        {
            float left = entity.GetComponent<TransformComponent>().Center - removedEntity.GetComponent<TransformComponent>().Width / 2;
            float top = entity.GetComponent<TransformComponent>().Bottom - 2;
            if (removedEntity.HasComponent<ColliderComponent>() == true)
                top -= removedEntity.GetComponent<ColliderComponent>().Size.Y;
            else
                top -= 3;
            return new Vector2(left, top);
        }
        return Vector2.Zero;
    }

    public static void DrawInventory(InventoryComponent inventoryComponent)
    {

        Vector2 adjustedPosition = inventoryComponent.CalculateTopLeftPositionFromAnchor();

        GameUI.DrawBox(
            adjustedPosition,
            inventoryComponent.Size,
            3,
            inventoryComponent.Alpha
        );

        // Draw each slot
        for (int i = 0; i < inventoryComponent.NumberOfSlots; i++)
        {

            // Calculate slot top-left position
            Vector2 pos = new Vector2(
                adjustedPosition.X + inventoryComponent.Margin + ( (i % inventoryComponent.SlotsPerRow) * (inventoryComponent.SlotSize.X + inventoryComponent.Margin)),
                (int)(adjustedPosition.Y + inventoryComponent.Margin + ( (inventoryComponent.Margin + inventoryComponent.SlotSize.Y) * (Math.Floor((double)(i / inventoryComponent.SlotsPerRow)))))
            );

            // Draw slot background
            GameUI.DrawButtonDown(
                pos,
                inventoryComponent.SlotSize,
                3,
                inventoryComponent.Alpha,
                i == inventoryComponent.SelectedSlot ? new Color(215, 215, 215) : Color.White
            );

            // Draw the texture of the entity in the slot
            // (if the slot is being used)

            InventorySystem inventorySystem = Milk.Systems.GetSystem<InventorySystem>();

            if (
                inventoryComponent.GetSlot(i).Items.Count > 0 &&
                inventorySystem.GetTexture(inventoryComponent.GetSlot(i).Type) != null  
            )
            {

                // Pulse if item has been recently added
                double timeSinceLastUpdate = Milk.TotalGameTime - inventoryComponent.GetSlot(i).LastUpdated;
                Rectangle r;
                if (timeSinceLastUpdate <= 0.25 && inventoryComponent.GetSlot(i).LastUpdateType == SlotChangeType.Added)
                {
                    double x = Math.Clamp(1 - (timeSinceLastUpdate * 4), 0, 1);
                    //Console.WriteLine(x);
                    r = new Rectangle(
                        (int)pos.X + 8 - (int)(x * 16),
                        (int)pos.Y + 8 - (int)(x * 16),
                        (int)inventoryComponent.SlotSize.X - 16 + (int)(x * 32),
                        (int)inventoryComponent.SlotSize.Y - 16 + (int)(x * 32)
                    );
                }
                else
                {
                    r = new Rectangle(
                        (int)pos.X + 8,
                        (int)pos.Y + 8,
                        (int)inventoryComponent.SlotSize.X - 16,
                        (int)inventoryComponent.SlotSize.Y - 16
                    );
                }

                // Fit the texture to the available slot size
                Utilities.DrawTextureToContainerSize(
                    inventorySystem.GetTexture(inventoryComponent.GetSlot(i).Type),
                    r,
                    Color.White
                );
            }

            // Draw inventory numnber
            // Draw the number of entities stored in the slot
            // (if the slot is being used)
            if (inventoryComponent.GetSlot(i).Items.Count > 0)
            {
                string numberOfEntities = inventoryComponent.GetSlot(i).Items.Count.ToString();
                Milk.Graphics.DrawString(
                    inventoryComponent.Font,
                    numberOfEntities,
                    new Vector2(
                        pos.X + 7,
                        pos.Y
                    ),
                    Color.White
                );
            }

        }
    }

}