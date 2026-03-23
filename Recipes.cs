//   Tiny Town, By Rik Cross
//   -- Code: github.com/rik-cross/tiny-town
//   -- Shared under the MIT licence
//   Uses the milk MonoGame ECS engine
//   -- Docs: rik-cross.github.io/monogame-milk

using System.Collections.Generic;
using milk.Core;

public static class Recipes
{
    
    public static CraftingRecipe TestRecipe = new CraftingRecipe(
        entityTypeCreated: "apple",
        new Dictionary<string, int>()
        {
            { "apple", 3 }
        },
        duration: 2
    );
    
}