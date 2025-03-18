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

    public void Setup()
    {

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
        Console.WriteLine("Click to play");
    }

    public void Play()
    {
        Console.WriteLine(Material.Combine([Material.Water, Material.Earth]).name);
    }

    public void GameOver()
    {
    }

}

