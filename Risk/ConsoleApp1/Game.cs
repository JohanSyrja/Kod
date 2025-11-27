using System;
using System.ComponentModel;
using System.IO.Compression;

namespace ConsoleApp1;

public class Game
{
    /// game start logic 
    /// manage turns
    /// implement game loop
    private List<IPlayer> players = [];
    private Board? board;
    private int rows;
    private int cols;
    public void GameLoop()
    {

        rows = 5;
        cols = 5;
        board = new(rows, cols);
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                board.SetTerritory(i, j, new Territory($"{i}a{j}"));
            }
            
        }
        for (int i = 0; i < rows; i++)
        {
            Random rand = new Random();
            int row = rand.Next(1, rows);
            int col = rand.Next(0, cols -1);
            board.GetTerritory(row, col).LandType = "Water";
            
        }

        IPlayer player1 = new Player("Alice", board.GetTerritory(0, 0));
        IPlayer player2 = new Player("Bob", board.GetTerritory(rows - 1, cols - 1));
        players.Add(player1);
        players.Add(player2);
        board.GetTerritory(0, 0).Owner = player1;
        board.GetTerritory(0, 0).AddArmy(5);
        board.GetTerritory(rows - 1, cols - 1).Owner = player2;
        board.GetTerritory(rows - 1, cols - 1).AddArmy(5);
        player1.SetPlayerTurn(true);

        while (!players.Any(p => p.HasWon()))
        {
            for (int i = 0; i < players.Count; i++)
            {
                if (players[i].GetPlayerTurn())
                {
                    board.DisplayBoard();
                    Console.WriteLine($"{players[i].GetName()}'s turn.");
                    Console.WriteLine("Choose an action: Attack, Fortify, EndTurn");
                    string action = GetPlayerInput();
                    if (action != null)
                    {
                        switch (action.ToLower())
                        {
                            case "attack":
                                AttackPhase(GetAttackInput());
                                board.DisplayBoard();
                                break;
                            case "fortify":
                                FortifyPhase(GetFortifyInput());
                                board.DisplayBoard();
                                break;
                            case "endturn":
                                EndTurn();
                                break;
                            default:
                                Console.WriteLine("Invalid action. Try again.");
                                break;
                        }
                    }
                }
            }
        }

    }
    private string GetPlayerInput()
    {
        Console.WriteLine("Enter your command:");
        string? input = Console.ReadLine() ?? GetPlayerInput();
        return input;
    }
    private void EndTurn()
    {
        for (int i = 0; i < players.Count; i++)
        {
            if (players[i].GetPlayerTurn())
            {
                players[i].SetPlayerTurn(false);
                if (i == players.Count - 1)
                {
                    players[0].SetPlayerTurn(true);
                    ReinforcePhase();
                }
                else
                {
                    players[i + 1].SetPlayerTurn(true);
                    ReinforcePhase();
                }
                break;
            }
        }
    }
    private List<string> GetAttackInput()
    {
        Console.WriteLine("Enter your attack command (e.g., '1a1,  2a2, numArmies'):");
        string? input = Console.ReadLine() ?? string.Empty;
        List<string> inputList = [.. input.Split(',')];
        if (inputList.Count != 3)
        {
            Console.WriteLine("Invalid input. Please try again.");
            GetAttackInput();
        }
        List<string> result = [.. input.Split([','])];
        return result;
    }
    private List<string> GetFortifyInput()
    {
        Console.WriteLine("Enter your fortify command (e.g. 1a1,  2a2, numArmies'):");
        string? input = Console.ReadLine() ?? string.Empty;
        List<string> inputList = [.. input.Split(',')];
        if (inputList.Count != 3)
        {
            Console.WriteLine("Invalid input. Please try again.");
            GetFortifyInput();
        }
        List<string> result = [.. input.Split([','])];
        return result;
    }
    private void AttackPhase(List<string> input)
    {
        string fromTerritoryName = input[0].Trim();
        string toTerritoryName = input[1].Trim();
        int numArmies = int.Parse(input[2].Trim());
        for (int i = 0; i < players.Count; i++)
        {
            if (players[i].GetPlayerTurn())
            {
                var fromTerritory = board?.GetTerritoryByName(fromTerritoryName);
                var toTerritory = board?.GetTerritoryByName(toTerritoryName);
                if (fromTerritory == null || toTerritory == null)
                {
                    Console.WriteLine("Invalid territory name(s). Try again.");
                    AttackPhase(GetAttackInput());
                    return;
                }
                if (toTerritory.Owner == players[i])
                {
                    Console.WriteLine("Cannot attack the same territory. Try again.");
                    AttackPhase(GetAttackInput());
                    return;
                }
                // Ensure target is a neighboring cell (including diagonals)
                int dr = Math.Abs(GetRow(fromTerritoryName) - GetRow(toTerritoryName));
                int dc = Math.Abs(GetCol(fromTerritoryName) - GetCol(toTerritoryName));
                if ((dr == 0 && dc == 0) || dr > 1 || dc > 1)
                {
                    Console.WriteLine("Target territory is not adjacent. You can only attack neighboring territories.");
                    AttackPhase(GetAttackInput());
                    return;
                }
                // Only call Attack if both territories are not null
                if (fromTerritory != null && toTerritory != null)
                {
                    players[i].Attack(fromTerritory, toTerritory, numArmies);
                }
            }
        }


    }
    private void ReinforcePhase()
    {
        for (int i = 0; i < players.Count; i++)
        {
            if (players[i].GetPlayerTurn())
            {
                players[i].Reinforce(players[i].GetStartingTerritory());
            }
        }
    }
    private void FortifyPhase(List<string> input)
    {
        string fromTerritoryName = input[0].Trim();
        string toTerritoryName = input[1].Trim();
        int numArmies = int.Parse(input[2].Trim());
        for (int i = 0; i < players.Count; i++)
        {
            if (players[i].GetPlayerTurn())
            {
                var fromTerritory = board?.GetTerritoryByName(fromTerritoryName);
                var toTerritory = board?.GetTerritoryByName(toTerritoryName);
                if (fromTerritory == null || toTerritory == null)
                {
                    Console.WriteLine("Invalid territory name(s). Try again.");
                    FortifyPhase(GetFortifyInput());
                    return;
                }
                if(board?.GetTerritoryByName(fromTerritoryName)?.Owner != players[i] || board?.GetTerritoryByName(toTerritoryName)?.Owner != players[i] ){
                    Console.WriteLine("You dont own both territories, please try again");
                    FortifyPhase(GetFortifyInput());
                    return;
                }
                players[i].Fortify(fromTerritory, toTerritory, numArmies);
            }
        }
    }

    // Helper methods to extract row/col from territory name (e.g., "2a3" -> 2, 3)
    private int GetRow(string name)
    {
        var idx = name.IndexOf('a');
        if (idx > 0 && int.TryParse(name.Substring(0, idx), out int row))
            return row;
        return -1;
    }
    private int GetCol(string name)
    {
        var idx = name.IndexOf('a');
        if (idx >= 0 && int.TryParse(name.Substring(idx + 1), out int col))
            return col;
        return -1;
    }

    public static void Main(string[] args)
    {
        new Game().GameLoop();
    }
}
