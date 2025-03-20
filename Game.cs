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
    enum gameState
    {
        Menu,
        Play,
        GameOver
    }

    gameState state = gameState.Menu;
    Interactable[] interactables = [new Interactable(Interactable.EmptyBottle), new Interactable(Interactable.EmptyBottle)];

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

        foreach (Interactable interactable in interactables)
        {
            Vector2 interactableSize = new Vector2(interactable.texture.Width, interactable.texture.Height);
            bool closeToInteractable = Vector2.Distance(Input.GetMousePosition(), interactable.position + interactableSize/2) < 50;
            interactable.Render();
            if (Input.IsMouseButtonPressed(MouseInput.Left) && closeToInteractable)
            { 
                interactable.Interact();
                break;
            }
            if (interactable.moving)
            {
                interactable.position = Input.GetMousePosition() - interactableSize/2;
                if (!Input.IsMouseButtonDown(MouseInput.Left))
                {
                    interactable.moving = false;
                }
            }
        }
    }

    public void GameOver()
    {
    }

}

