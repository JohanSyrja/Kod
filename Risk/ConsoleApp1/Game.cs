using System;

namespace ConsoleApp1;

public class Game
{
    /// game start logic 
    /// manage turns
    /// implement game loop

    public void StartGame()
    {
        Board board = new(5, 5);
        Player player1 = new("Alice");
        Player player2 = new("Bob");
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                board.SetTerritory(i, j, new Territory($"Territory_{i}_{j}", "Plains"));
            }
        }
        board.GetTerritory(0, 0).SetOwner(player1);
        board.GetTerritory(0, 0).AddArmy(5);
        board.GetTerritory(4, 4).SetOwner(player2);
        board.GetTerritory(4, 4).AddArmy(3);
        Console.WriteLine(board.GetTerritory(0, 0).GetName() + " owned by " + board.GetTerritory(0, 0).GetOwner()?.GetName() + " with " + board.GetTerritory(0, 0).GetArmies() + " armies.");
        Console.WriteLine(board.GetTerritory(3, 3).GetName() + " owned by " + board.GetTerritory(3, 3).GetOwner()?.GetName() + " with " + board.GetTerritory(3, 3).GetArmies() + " armies.");
        board.DisplayBoard();

    }

    public static void Main(string[] args)
    {
        new Game().StartGame();

    }

}
