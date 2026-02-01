//   Tiny Town, By Rik Cross
//   -- Code: github.com/rik-cross/tiny-town
//   -- Shared under the MIT licence
//   Uses the milk MonoGame ECS engine
//   -- Docs: rik-cross.github.io/monogame-milk

using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using milk.Core;
using milk.Components;

public static class PlayerEntity
{

    public static void PlayerInputController(Scene scene, Entity entity, bool isSuspended)
    {

        //
        // Position
        //

        PhysicsComponent physicsComponent = entity.GetComponent<PhysicsComponent>();

        float dx = 0;
        float dy = 0;

        float speed = 50.0f;

        if (isSuspended == false) {

            if (scene.game.inputManager.IsKeyDown(Keys.W))
                dy -= speed;
            if (scene.game.inputManager.IsKeyDown(Keys.A))
                dx -= speed;
            if (scene.game.inputManager.IsKeyDown(Keys.S))
                dy += speed;
            if (scene.game.inputManager.IsKeyDown(Keys.D))
                dx += speed;

        }

        physicsComponent.Velocity = new Vector2(dx, dy);

        //
        // State
        //

        string state = entity.State;

        if (dy < 0)
            entity.State = "walk_up";
        if (dy > 0)
            entity.State = "walk_down";
        if (dx < 0)
            entity.State = "walk_left";
        if (dx > 0)
            entity.State = "walk_right";
            
        if (state == "walk_up" && dx == 0 && dy == 0)
            entity.State = "idle_up";
        if (state == "walk_down" && dx == 0 && dy == 0)
            entity.State = "idle_down";
        if (state == "walk_left" && dx == 0 && dy == 0)
            entity.State = "idle_left";
        if (state == "walk_right" && dx == 0 && dy == 0)
            entity.State = "idle_right";

    }

    public static Texture2D playerSpriteSheet = Milk.Content.Load<Texture2D>("images/player");
    public static List<List<Texture2D>> playerImagesList = Utilities.SplitTexture(playerSpriteSheet, new Vector2(48, 48));

    public static Entity Create()
    {
        Entity playerEntity = new Entity(name: "player");

        playerEntity.AddComponent(
            new TransformComponent(
                position: new Vector2(16 * 16 - 7, 16 * 16 - 8),
                size: new Vector2(14, 16)
            )
        );

        playerEntity.AddComponent(
            new ColliderComponent(
                size: new Vector2(10, 4),
                offset: new Vector2(2, 12)
            )
        );

        playerEntity.AddComponent(
            new TriggerComponent(
                size: new Vector2(14, 16),
                offset: new Vector2(0, 0)
            )
        );

        playerEntity.AddComponent(new SpriteComponent());

        playerEntity.GetComponent<SpriteComponent>().AddTextures(
            textureList: new List<Texture2D>() {
                playerImagesList[1][0],
                playerImagesList[1][1],
                playerImagesList[1][2],
                playerImagesList[1][3],
                playerImagesList[1][4],
                playerImagesList[1][5],
                playerImagesList[1][6],
                playerImagesList[1][7]
            },
            state: "idle_up",
            duration: 0.6f,
            offset: new Vector2(17, 16)
        );

        playerEntity.GetComponent<SpriteComponent>().AddTextures(
            textureList: new List<Texture2D>() {
                playerImagesList[0][0],
                playerImagesList[0][1],
                playerImagesList[0][2],
                playerImagesList[0][3],
                playerImagesList[0][4],
                playerImagesList[0][5],
                playerImagesList[0][6],
                playerImagesList[0][7]
            },
            state: "idle_down",
            duration: 0.6f,
            offset: new Vector2(17, 16)
        );

        playerEntity.GetComponent<SpriteComponent>().AddTextures(
            textureList: new List<Texture2D>() {
                playerImagesList[3][0],
                playerImagesList[3][1],
                playerImagesList[3][2],
                playerImagesList[3][3],
                playerImagesList[3][4],
                playerImagesList[3][5],
                playerImagesList[3][6],
                playerImagesList[3][7]
            },
            state: "idle_left",
            duration: 0.6f,
            offset: new Vector2(17, 16)
        );

        playerEntity.GetComponent<SpriteComponent>().AddTextures(
            textureList: new List<Texture2D>() {
                playerImagesList[2][0],
                playerImagesList[2][1],
                playerImagesList[2][2],
                playerImagesList[2][3],
                playerImagesList[2][4],
                playerImagesList[2][5],
                playerImagesList[2][6],
                playerImagesList[2][7]
            },
            state: "idle_right",
            duration: 0.6f,
            offset: new Vector2(17, 16)
        );

        playerEntity.GetComponent<SpriteComponent>().AddTextures(
            textureList: new List<Texture2D>() {
                playerImagesList[5][0],
                playerImagesList[5][1],
                playerImagesList[5][2],
                playerImagesList[5][3],
                playerImagesList[5][4],
                playerImagesList[5][5],
                playerImagesList[5][6],
                playerImagesList[5][7]
            },
            state: "walk_up",
            duration: 0.6f,
            offset: new Vector2(17, 16)
        );

        playerEntity.GetComponent<SpriteComponent>().AddTextures(
            textureList: new List<Texture2D>() {
                playerImagesList[4][0],
                playerImagesList[4][1],
                playerImagesList[4][2],
                playerImagesList[4][3],
                playerImagesList[4][4],
                playerImagesList[4][5],
                playerImagesList[4][6],
                playerImagesList[4][7]
            },
            state: "walk_down",
            duration: 0.6f,
            offset: new Vector2(17, 16)
        );


        playerEntity.GetComponent<SpriteComponent>().AddTextures(
            textureList: new List<Texture2D>() {
                playerImagesList[7][0],
                playerImagesList[7][1],
                playerImagesList[7][2],
                playerImagesList[7][3],
                playerImagesList[7][4],
                playerImagesList[7][5],
                playerImagesList[7][6],
                playerImagesList[7][7]
            },
            state: "walk_left",
            duration: 0.6f,
            offset: new Vector2(17, 16)
        );


        playerEntity.GetComponent<SpriteComponent>().AddTextures(
            textureList: new List<Texture2D>() {
                playerImagesList[6][0],
                playerImagesList[6][1],
                playerImagesList[6][2],
                playerImagesList[6][3],
                playerImagesList[6][4],
                playerImagesList[6][5],
                playerImagesList[6][6],
                playerImagesList[6][7]
            },
            state: "walk_right",
            duration: 0.6f,
            offset: new Vector2(17, 16)
        );

        playerEntity.AddComponent(new PhysicsComponent());

        playerEntity.AddComponent(new InputComponent(PlayerInputController));

        playerEntity.State = "idle_down";

        return playerEntity;
    }

}