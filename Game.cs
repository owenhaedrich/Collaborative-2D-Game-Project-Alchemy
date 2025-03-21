// Include the namespaces (code libraries) you need below.
using System;
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

    // Shelves
    const int shelfWidth = 4;
    const int shelfHeight = 5;

    // Cauldron
    Vector2 cauldronPourPosition = new Vector2(400, 200);
    Vector2 cauldronPosition = new Vector2(400, 300);
    float cauldronRadius = 50;
    Material[] cauldronMaterials = new Material[4];

    // Physics
    Vector2 gravity = Vector2.UnitY * 10;

    // Game Objects
    Interactable[] bottles = [new Interactable(Interactable.EmptyBottle, new Vector2(100,100)), new Interactable(Interactable.EmptyBottle, new Vector2(100, 300)), new Interactable(Interactable.EmptyBottle, new Vector2(100, 500))];
    ItemHolder[] shelves = new ItemHolder[shelfWidth * shelfHeight * 2];

    public void Setup()
    {
        Window.SetSize(800, 600);

        // Generate Shelves
        int i = 0;
        for (int x = 0; x < shelfWidth; x++)
        {
            for (int y = 0; y < shelfHeight; y++)
            {
                shelves[i] = (new ItemHolder(new Vector2(50 + x * 75, 100 + y * 100)));
                shelves[i + shelves.Length/2] = (new ItemHolder(new Vector2(510 + x * 75, 100 + y * 100)));
                i++;
            }
        }
    }

    public void Update()
    {
        switch (state)
        {
            case gameState.Menu:
                Menu();
                if (Input.IsKeyboardKeyPressed(KeyboardInput.Enter))
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

        MoveInteractables();
        ManageItemHolders();
        ManageCauldron();
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
        Draw.FillColor = Color.Black;
        Draw.Circle(cauldronPosition, cauldronRadius);

        // Add materials above the cauldron up to the max capacity
        for (int i = 0; i < cauldronMaterials.Length; i++)
        {
            if (cauldronMaterials[i] is null)
            {
                foreach (Interactable bottle in bottles)
                {
                    if (bottle.homePosition == cauldronPourPosition)
                    {
                        cauldronMaterials[i] = bottle.material;
                        bottle.homePosition = Vector2.NegativeInfinity;
                        break;
                    }
                }
            }
        }

        if (Input.IsMouseButtonPressed(MouseInput.Left) && Vector2.Distance(Input.GetMousePosition(), cauldronPosition) < cauldronRadius)
        {
            Console.WriteLine(Material.Combine(cauldronMaterials).name);
        }

    }

    public void MoveInteractables()
    {
        foreach (Interactable interactable in bottles)
        {
            Vector2 interactableSize = new Vector2(interactable.texture.Width, interactable.texture.Height);
            bool closeToInteractable = Vector2.Distance(Input.GetMousePosition(), interactable.position + interactableSize / 2) < 50;
            interactable.Render();
            if (Input.IsMouseButtonPressed(MouseInput.Left) && closeToInteractable)
            {
                interactable.Interact();
                break;
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
                        float newY = float.Lerp(interactable.position.Y, interactable.homePosition.Y - interactableSize.Y / 2, 1/distanceToHome * 2.7f);
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

            // Check if the interactable is released over the cauldron
            if (Vector2.Distance(interactable.position + interactableSize / 2, cauldronPourPosition) < cauldronRadius && Input.IsMouseButtonReleased(MouseInput.Left))
            {
                interactable.homePosition = cauldronPourPosition;
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
                        Console.WriteLine(direction);
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

