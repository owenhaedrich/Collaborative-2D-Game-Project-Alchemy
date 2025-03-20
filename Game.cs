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

    // Game Objects
    Interactable[] interactables = [new Interactable(Interactable.EmptyBottle), new Interactable(Interactable.EmptyBottle)];
    ItemHolder[] itemHolders = [new ItemHolder(new Vector2(100, 100)), new ItemHolder(new Vector2(500, 100))];

    // Cauldron
    Vector2 cauldronPosition = new Vector2(400, 300);
    float cauldronRadius = 50;

    public void Setup()
    {
        Window.SetSize(800, 600);
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
                if (Input.IsKeyboardKeyPressed(KeyboardInput.Enter))
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

        foreach (ItemHolder itemHolder in itemHolders)
        {
            itemHolder.Render();
        }

        MoveInteractables();
        ManageCauldron();
    }

    public void ManageCauldron()
    {
        Draw.FillColor = Color.Black;
        Draw.Circle(cauldronPosition, cauldronRadius);
    }

    public void MoveInteractables()
    {
        foreach (Interactable interactable in interactables)
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
                    if (Vector2.Distance(interactable.position + interactableSize / 2, interactable.homePosition) > 10)
                    {
                        interactable.position = Vector2.Lerp(interactable.position, interactable.homePosition - interactableSize / 2, 0.1f);
                    }
                }
            }

            // Check if the interactable is near an item holder
            foreach (ItemHolder itemHolder in itemHolders)
            {
                Vector2 itemHolderSize = new Vector2(itemHolder.texture.Width, itemHolder.texture.Height);
                bool closeToItemHolder = Vector2.Distance(interactable.position + interactableSize/2, itemHolder.position + itemHolderSize / 2) < 15;
                if (closeToItemHolder)
                {
                    interactable.homePosition = itemHolder.position;
                }
            }
        }
    }

    public void GameOver()
    {
    }

}

