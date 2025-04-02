// Include the namespaces (code libraries) you need below.
using System;
using System.Linq;
using System.Numerics;
using Collaborative_2D_Game_Project;

// The namespace your code is in.
namespace MohawkGame2D;

/// <summary>
///     Your game code goes inside this class!
/// </summary>
public class Game
{
    // Game State
    enum gameState
    {
        Menu,
        Play,
        Recipes
    }

    gameState state = gameState.Menu;

    // Recipe View Variables
    Vector2 scrollOffset = Vector2.Zero;

    // Game Objects
    Interactable[] bottles = new Interactable[50];
    ItemHolder[] shelves = new ItemHolder[shelfWidth * shelfHeight * 2];
    Material[] discoveredPotions = new Material[Material.potions.Length];
    Vector2 speedToHome = new Vector2(1.5f, 1.9f);
    Vector2 speedToLeave = new Vector2(1.5f, 0.7f);

    // Shelves
    const int shelfWidth = 3;
    const int shelfHeight = 3;
    Vector2 catPosition = new Vector2(200, 550);
    Vector2 moonPosition = new Vector2(350, 100);

    // Cauldron
    Vector2 cauldronPourPosition = new Vector2(400, 300);
    Vector2 cauldronPosition = new Vector2(400, 500);
    float cauldronRadius = 50;
    Vector2 finishedPotionPosition = new Vector2(400, -100);

    // Respawner
    Vector2 respawnerPosition = new Vector2(-300, 100);

    // Backgrounds
    Texture2D gameBackground = Graphics.LoadTexture("../../../assets/Screens/Game_Background.png");
    Texture2D cauldron = Graphics.LoadTexture("../../../assets/graphics/Cauldron.png");
    Texture2D cauldronLit = Graphics.LoadTexture("../../../assets/graphics/CauldronLit.png");
    Texture2D StartScreen = Graphics.LoadTexture("../../../assets/Screens/StartScreen.png");
    Texture2D IngredientScroll = Graphics.LoadTexture("../../../assets/Screens/IngredientsScroll.png");
    Texture2D RecipeScroll = Graphics.LoadTexture("../../../assets/Screens/RecipeScroll.png");
    Texture2D bubbleSprite = Graphics.LoadTexture("../../../assets/graphics/Bubble.png");

    public void Setup()
    {
        Window.SetSize(800, 600);
        // Generate Shelves
        int shelfPosition = 0;
        for (int x = 0; x < shelfWidth; x++)
        {
            for (int y = 0; y < shelfHeight; y++)
            {
                shelves[shelfPosition] = (new ItemHolder(new Vector2(75 + x * 75, 100 + y * 150)));
                shelves[shelfPosition + shelves.Length/2] = (new ItemHolder(new Vector2(580 + x * 75, 100 + y * 150)));
                shelfPosition++;
            }
        }

        // Fill Bottle Array with Free Bottles
        Array.Fill(bottles, new Interactable());

        // Add bottles of the basic materials to the shelves
        for (int i = 1; i < Material.materials.Length; i++)
        {
            Interactable newBottle = new Interactable(Interactable.EmptyBottle, respawnerPosition, Material.materials[i]);
            if (Material.materials[i].ID == 18)
            {
                newBottle.homePosition = moonPosition;

            }
            else if (Material.materials[i].ID == 19)
            {
                newBottle.homePosition = catPosition;

            }
            else
            {
                newBottle.homePosition = shelves[Material.materials[i].ID].position;
            }
            bottles[i] = newBottle;
        }
    }

    public void Update()
    {
        switch (state)
        {
            case gameState.Menu:
                Menu();
                if (Input.IsKeyboardKeyPressed(KeyboardInput.Enter) || Input.IsMouseButtonPressed(MouseInput.Left))
                {
                    state = gameState.Play;
                }
                break;
            case gameState.Play:
                Play();
                if (Input.IsKeyboardKeyPressed(KeyboardInput.Tab))
                    state = gameState.Recipes;
                break;
            case gameState.Recipes:
                RecipeView();
                if (Input.IsKeyboardKeyPressed(KeyboardInput.Tab))
                    state = gameState.Play;
                break;
        }
    }

    public void Menu()
    {
        Window.ClearBackground(Color.OffWhite);
        Graphics.Draw(StartScreen, 0, 0);
    }

    public void Play()
    {
        Window.ClearBackground(Color.OffWhite);

        Graphics.Draw(gameBackground, 0, 0);

        ManageCauldron();
        ManageInteractables();
        ManageItemHolders();
        Graphics.Draw(bubbleSprite, cauldronPourPosition - new Vector2(bubbleSprite.Width, bubbleSprite.Height) / 2);
        if (Vector2.Distance(Input.GetMousePosition(), cauldronPosition) < cauldronRadius)
        {
            Graphics.Draw(cauldronLit, cauldronPosition - new Vector2(cauldron.Width, cauldron.Height) / 2);
        }
        else
        {
            Graphics.Draw(cauldron, cauldronPosition - new Vector2(cauldron.Width, cauldron.Height) / 2);
        }
    }

    public void RecipeView()
    {
        Window.ClearBackground(Color.OffWhite);

        //Handle Scroll
        scrollOffset += Input.GetMouseWheel() * 25;

        for (int i = 0; i < Material.potions.Length; i++)
        {
            //Graphics.Draw(Material.potions[i].texture, recipeStartPosition + i * recipeOffset + scrollOffset);
            //Text.Draw(Material.potions[i].name, recipeStartPosition + i * recipeOffset + scrollOffset);
            Graphics.Draw(IngredientScroll, scrollOffset);
            Graphics.Draw(RecipeScroll, scrollOffset + new Vector2(0, 560));
            Draw.FillColor = Color.Black;
            Draw.Rectangle(scrollOffset + new Vector2(0, 550), new Vector2(800, 10));
        }
    }

    public void ManageItemHolders()
    {
        foreach (ItemHolder itemHolder in shelves)
        {
            if (itemHolder.item is not null)
            {
                if (itemHolder.item.homePosition != itemHolder.position)
                {
                    itemHolder.item = null;
                }
            }
            itemHolder.Render();
        }
    }

    public void ManageCauldron()
    {
        Interactable[] bottlesAboveCauldron = new Interactable[4];
        Interactable[] bottlesInCauldron = new Interactable[4];

        // Add materials above the cauldron up to the max capacity
        for (int i = 0; i < bottlesAboveCauldron.Length; i++)
        {
            if (bottlesAboveCauldron[i] is null)
            {
                foreach (Interactable bottle in bottles)
                {
                    if (bottle is not null)
                    {
                        if (bottle.homePosition == cauldronPourPosition && !bottlesAboveCauldron.Contains(bottle))
                        {
                            bottlesAboveCauldron[i] = bottle;
                            break;
                        }
                        if (bottle.homePosition == cauldronPosition && !bottlesInCauldron.Contains(bottle))
                        {
                            bottlesInCauldron[i] = bottle;
                            break;
                        }
                    }
                }
            }
        }

        // Combine bottles in the cauldron if the mouse is pressed over the cauldron
        if (Input.IsMouseButtonPressed(MouseInput.Left) && Vector2.Distance(Input.GetMousePosition(), cauldronPosition) < cauldronRadius)
        {
            foreach (Interactable bottle in bottlesAboveCauldron)
            {
                if (bottle is not null)
                {
                    bottle.homePosition = cauldronPosition;
                }
            }
        }

        // Do not perform a combination if any bottle are outside the cauldron or if all bottles are free/unused
        bool allInCauldron = true;
        int freeCount = 0;
        foreach (Interactable bottle in bottlesInCauldron)
        {
            if (bottle is null)
            {
                freeCount++; // A null bottle is considered free
            }
            else
            {
                if (bottle.free)
                {
                    freeCount++;
                }
                else
                {
                    if (Vector2.Distance(bottle.position + Interactable.bottleSize / 2, cauldronPosition) > cauldronRadius)
                    {
                        allInCauldron = false;
                        break;
                    }
                }
            }
        }

        // Combine bottles in the cauldron once they all move inside and track the consumed materials
        Material?[] consumedMaterials = new Material?[4];
        int consumedMaterialCount = 0;
        if (allInCauldron && freeCount < bottlesInCauldron.Length)
        {
            foreach (Interactable bottle in bottlesInCauldron)
            {
                if (bottle is not null)
                {
                    consumedMaterials[consumedMaterialCount] = bottle.material;
                    consumedMaterialCount++;
                    bottle.Free();
                }
            }

            Interactable combination = CombineBottles(bottlesInCauldron);

            // Spawn the combination in a free bottle slot
            if (combination.material.name != "Junk")
            {
                for (int i = 0; i < bottles.Length; i++)
                {
                    if (bottles[i].free)
                    {
                        bottles[i] = combination;
                        break;
                    }
                }
            }
        }

        // Respawn Bottles with Consumed Materials
        foreach (Material? consumedMaterial in consumedMaterials)
        {
            if (consumedMaterial is not null)
            {
                if (consumedMaterial.ID == -1) break;

                // Determine the home position based on the material's ID
                Vector2 homePos;
                if (consumedMaterial.ID == 18)
                {
                    homePos = moonPosition;
                }
                else if (consumedMaterial.ID == 19)
                {
                    homePos = catPosition;
                }
                else
                {
                    homePos = shelves[consumedMaterial.ID].position;
                }

                bool foundExisting = false;
                // First, check if there's an existing free bottle with the same home position
                for (int i = 0; i < bottles.Length; i++)
                {
                    if (bottles[i] != null && bottles[i].free && bottles[i].homePosition == homePos)
                    {
                        // Reset the existing bottle
                        bottles[i].material = consumedMaterial;
                        bottles[i].position = respawnerPosition - Interactable.bottleSize / 2;
                        bottles[i].free = false;
                        foundExisting = true;
                        break;
                    }
                }

                if (!foundExisting)
                {
                    // If no existing bottle found, look for any free slot to create a new one
                    for (int i = 0; i < bottles.Length; i++)
                    {
                        if (bottles[i].free)
                        {
                            Interactable newBottle = new Interactable(Interactable.EmptyBottle, respawnerPosition - Interactable.bottleSize / 2, consumedMaterial);
                            newBottle.homePosition = homePos;
                            bottles[i] = newBottle;
                            break;
                        }
                    }
                }
            }
        }
    }

    public Interactable CombineBottles(Interactable?[] combinationBottles)
    {
        Material?[] cauldronMaterials = new Material?[4];
        for (int i = 0; i < combinationBottles.Length; i++)
        {
            if (combinationBottles[i] is not null)
            {
                cauldronMaterials[i] = combinationBottles[i]!.material;
            }
        }

        Material newMaterial = Material.Combine(cauldronMaterials);
        Console.WriteLine(newMaterial.name);
        if (newMaterial.name != "Junk")
        {
            for (int i = 0; i < discoveredPotions.Length; i++)
            {
                if (discoveredPotions[i] is null)
                {
                    discoveredPotions[i] = newMaterial;
                    break;
                }
            }
        }


        Interactable combinedBottle = new Interactable(Interactable.EmptyBottle, cauldronPosition - Interactable.bottleSize / 2, newMaterial);
        combinedBottle.homePosition = finishedPotionPosition;
        return combinedBottle;
    }

    public void ManageInteractables()
    {
        foreach (Interactable interactable in bottles)
        {
            Vector2 interactableSize = new Vector2(interactable.texture.Width, interactable.texture.Height);
            bool closeToInteractable = Vector2.Distance(Input.GetMousePosition(), interactable.position + interactableSize / 2) < 50;
            interactable.Render();
            if (Input.IsMouseButtonPressed(MouseInput.Left) && closeToInteractable)
            {
                interactable.Interact();
            }

            // Moving interactables move with the mouse
            if (interactable.moving)
            {
                if (!Input.IsMouseButtonDown(MouseInput.Left))
                {
                    interactable.moving = false;
                }
                else
                {
                    interactable.position = Input.GetMousePosition() - interactableSize / 2;
                }
            }
            else
            {
                // Interactables move back to their home position if they are not being moved
                if (interactable.homePosition != Vector2.NegativeInfinity)
                {
                    float distanceToHome = Vector2.Distance(interactable.position + interactableSize / 2, interactable.homePosition);
                    if (distanceToHome > 3)
                    {
                        float newX = float.Lerp(interactable.position.X, interactable.homePosition.X - interactableSize.X / 2, speedToHome.X / 100);
                        float newY = float.Lerp(interactable.position.Y, interactable.homePosition.Y - interactableSize.Y / 2, speedToHome.Y / 100);
                        if (interactable.homePosition == finishedPotionPosition)
                        {
                            newY = float.Lerp(interactable.position.Y, interactable.homePosition.Y - interactableSize.Y / 2, speedToLeave.Y / 100);
                        }
                        interactable.position = new Vector2(newX, newY);
                    }
                }
            }

            // Check if the interactable is released over the cauldron - ignore if the home position is tha cauldron
            if (Vector2.Distance(interactable.position + interactableSize / 2, cauldronPourPosition) < cauldronRadius && Input.IsMouseButtonReleased(MouseInput.Left) && interactable.homePosition != cauldronPosition)
            {
                interactable.homePosition = cauldronPourPosition;
            }

            // Interactables don't overlap, push them apart if they do
            foreach (Interactable otherInteractable in bottles)
            {
                if (interactable != otherInteractable)
                {
                    if (Vector2.Distance(interactable.position + interactableSize, otherInteractable.position + interactableSize / 2) < 30)
                    {
                        Vector2 direction = interactable.position - otherInteractable.position;
                        if (direction == Vector2.Zero)
                        {
                            interactable.position += Vector2.One;
                        }
                        else
                        {
                            interactable.position += Vector2.Normalize(direction) * 1.1f;
                        }                        
                    }
                }
            }

            // Interactables with potions can fly off screen, push other interactables back to the viewable area
            if (!Material.potions.Contains(interactable.material))
            {
                if (interactable.position.X < 0)
                {
                    interactable.position.X = 0;
                }
                if (interactable.position.X > Window.Width - interactableSize.X)
                {
                    interactable.position.X = Window.Width - interactableSize.X;
                }
                if (interactable.position.Y < 0)
                {
                    interactable.position.Y = 0;
                }
                if (interactable.position.Y > Window.Height - interactableSize.Y)
                {
                    interactable.position.Y = Window.Height - interactableSize.Y;
                }
            }

        }
    }
}

