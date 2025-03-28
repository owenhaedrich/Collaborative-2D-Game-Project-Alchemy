﻿// Include the namespaces (code libraries) you need below.
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
        GameOver
    }

    gameState state = gameState.Menu;



    // Physics
    Vector2 gravity = Vector2.UnitY * 10;

    // Game Objects
    Interactable[] bottles = new Interactable[50];
    ItemHolder[] shelves = new ItemHolder[shelfWidth * shelfHeight * 2];
    Material[] potions = new Material[Material.craftableMaterials.Length];
    
    // Shelves
    const int shelfWidth = 4;
    const int shelfHeight = 5;

    // Cauldron
    Vector2 cauldronPourPosition = new Vector2(400, 300);
    Vector2 cauldronPosition = new Vector2(400, 500);
    float cauldronRadius = 50;

    // Respawner
    Vector2 respawnerPosition = new Vector2(400, 100);

    // Trash
    Vector2 trashPosition = new Vector2(400, 500);
    float trashRadius = 50;

    // Backgrounds
    Texture2D gameBackground = Graphics.LoadTexture("../../../assets/Screens/Game_Background.png");
    Texture2D cauldron = Graphics.LoadTexture("../../../assets/graphics/Cauldron.png");
    Texture2D cauldronLit = Graphics.LoadTexture("../../../assets/graphics/CauldronLit.png");

    public void Setup()
    {
        Window.SetSize(800, 600);

        // Generate Shelves
        int shelfPosition = 0;
        for (int x = 0; x < shelfWidth; x++)
        {
            for (int y = 0; y < shelfHeight; y++)
            {
                shelves[shelfPosition] = (new ItemHolder(new Vector2(50 + x * 75, 100 + y * 100)));
                shelves[shelfPosition + shelves.Length/2] = (new ItemHolder(new Vector2(510 + x * 75, 100 + y * 100)));
                shelfPosition++;
            }
        }

        // Fill Bottle Array with Free Bottles
        Array.Fill(bottles, new Interactable());

        // Add bottles of the basic materials to the shelves
        for (int i = 0; i < Material.materials.Length; i++)
        {
            bottles[i] = new Interactable(Interactable.EmptyBottle, shelves[i].position - Interactable.bottleSize/2, Material.materials[i]);
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
                break;
            case gameState.GameOver:
                GameOver();
                if (Input.IsKeyboardKeyPressed(KeyboardInput.Enter) || Input.IsMouseButtonPressed(MouseInput.Left))
                {
                    state = gameState.Menu;
                }
                break;
        }
    }

    public void Menu()
    {
        Window.ClearBackground(Color.OffWhite);
    }

    public void Play()
    {
        Window.ClearBackground(Color.OffWhite);

        Graphics.Draw(gameBackground, 0, 0);


        ManageCauldron();
        ManageInteractables();
        ManageItemHolders();
        Graphics.Draw(cauldron, cauldronPosition - new Vector2(cauldron.Width, cauldron.Height) / 2);
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
        //Draw.FillColor = Color.Black;
        //Draw.Circle(cauldronPosition, cauldronRadius);
        Draw.FillColor = Color.Blue;
        Draw.Circle(cauldronPourPosition, cauldronRadius);
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

            // Spawn the combination in a free bottle slot
            for (int i = 0; i < bottles.Length; i++)
            {
                if (bottles[i].free)
                {
                    bottles[i] = CombineBottles(bottlesInCauldron);
                    break;
                }
            }
        }

        // Respawn Bottles with Consumed Materials
        foreach (Material? consumedMaterial in consumedMaterials)
        {
            if (consumedMaterial is not null)
            {
                for (int i = 0; i < bottles.Length; i++)
                {
                    if (bottles[i].free)
                    {
                        bottles[i] = new Interactable(Interactable.EmptyBottle, respawnerPosition - Interactable.bottleSize/2, consumedMaterial);
                        break;
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
        if (newMaterial.name != "Junk")
        {
            for (int i = 0; i < potions.Length; i++)
            {
                if (potions is null)
                {
                    potions[i] = newMaterial;
                }
            }
        }


        Interactable combinedBottle = new Interactable(Interactable.EmptyBottle, cauldronPosition - Interactable.bottleSize / 2, newMaterial);
        combinedBottle.homePosition = cauldronPourPosition;
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
                        float newX = float.Lerp(interactable.position.X, interactable.homePosition.X - interactableSize.X / 2, 0.03f);
                        float newY = float.Lerp(interactable.position.Y, interactable.homePosition.Y - interactableSize.Y / 2, 1/distanceToHome * 3.9f);
                        interactable.position = new Vector2(newX, newY);
                    }
                }
            }

            // Set item holder to not full when its item gets a new home


            // Check if the interactable is near an item holder when released. Add the interactable to the item holder if it is not full.
            if (Input.IsMouseButtonReleased(MouseInput.Left))
            {
                ItemHolder closestItemHolder = null;
                foreach (ItemHolder itemHolder in shelves)
                {
                    Vector2 itemHolderSize = new Vector2(itemHolder.texture.Width, itemHolder.texture.Height);
                    bool closeToItemHolder = Vector2.Distance(interactable.position + interactableSize / 2, itemHolder.position + itemHolderSize / 2) < 50;
                    if (closeToItemHolder && itemHolder.item is null)
                    {
                        closestItemHolder = itemHolder;
                    }
                }

                if (closestItemHolder != null)
                {
                    interactable.homePosition = closestItemHolder.position;
                    closestItemHolder.item = interactable;
                }
            }

            // Check if the interactable is released over the cauldron - ignore if the home position is tha cauldron
            if (Vector2.Distance(interactable.position + interactableSize / 2, cauldronPourPosition) < cauldronRadius && Input.IsMouseButtonReleased(MouseInput.Left) && interactable.homePosition != cauldronPosition)
            {
                interactable.homePosition = cauldronPourPosition;
            }

            // Check if the interactable is released over the trash
            if (Vector2.Distance(interactable.position + interactableSize / 2, trashPosition) < trashRadius && Input.IsMouseButtonReleased(MouseInput.Left) && interactable.material == Material.Junk)
            {
                interactable.homePosition = trashPosition;
                interactable.Free();
            }

            // Interactables don't overlap, push them apart if they do
            foreach (Interactable otherInteractable in bottles)
            {
                if (interactable != otherInteractable)
                {
                    if (Vector2.Distance(interactable.position + interactableSize / 2, otherInteractable.position + interactableSize / 2) < 50)
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

            // Interactables don't go off screen, push them back if they do
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

    public void GameOver()
    {
    }

}

