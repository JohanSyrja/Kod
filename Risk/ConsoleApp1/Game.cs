using System;

namespace ConsoleApp1;

public class Game
{
    /// game start logic 
    /// manage turns
    /// implement game loop
    private List<Player> players = [];
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
            board.GetTerritory(row, col).SetLandType("Water");
            
        }
        Player player1 = new("Alice", board.GetTerritory(0, 0));
        Player player2 = new("Bob", board.GetTerritory(rows - 1, cols - 1));
        players.Add(player1);
        players.Add(player2);
        board.GetTerritory(0, 0).SetOwner(player1);
        board.GetTerritory(0, 0).AddArmy(5);
        board.GetTerritory(rows - 1, cols - 1).SetOwner(player2);
        board.GetTerritory(rows - 1, cols - 1).AddArmy(5);
        player1.SetPlayerTurn(true);
        while (!players.Any(p => p.HasWon()))
        {
            for (int i = 0; i < players.Count; i++)
            {
                if (players[i].GetPlayerTurn())
                {
                    ReinforcePhase();
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
                }
                else
                {
                    players[i + 1].SetPlayerTurn(true);
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
        Console.WriteLine("Enter your fortify command (e.g., 'fortify, 1a1,  2a2, numArmies'):");
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
                for (int r = 0; r < rows; r++)
                {
                    for (int c = 0; c < cols; c++)
                    {
                        if (board?.GetTerritory(r, c).GetName() == fromTerritoryName)
                        {
                            for (int r2 = 0; r2 < rows; r2++)
                            {
                                for (int c2 = 0; c2 < cols; c2++)
                                {
                                    if (board.GetTerritory(r2, c2).GetName() == toTerritoryName)
                                    {
                                        if (board.GetTerritory(r2, c2).GetOwner() == players[i])
                                        {
                                            Console.WriteLine("Cannot attack the same territory. Try again.");
                                            AttackPhase(GetAttackInput());
                                        }
                                        // Ensure target is a neighboring cell (including diagonals)
                                        int dr = Math.Abs(r - r2);
                                        int dc = Math.Abs(c - c2);
                                        if ((dr == 0 && dc == 0) || dr > 1 || dc > 1)
                                        {
                                            Console.WriteLine("Target territory is not adjacent. You can only attack neighboring territories.");
                                            return;
                                        }

                                        players[i].Attack(board.GetTerritory(r, c), board.GetTerritory(r2, c2), numArmies);
                                    }
                                }
                            }
                        }
                    }
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
        string fromTerritoryName = input[1].Trim();
        string toTerritoryName = input[2].Trim();
        int numArmies = int.Parse(input[3].Trim());
        for (int i = 0; i < players.Count; i++)
        {
            if (players[i].GetPlayerTurn())
            {
                for (int r = 0; r < rows; r++)
                {
                    for (int c = 0; c < cols; c++)
                    {
                        if (board?.GetTerritory(r, c).GetName() == fromTerritoryName)
                        {
                            for (int r2 = 0; r2 < rows; r2++)
                            {
                                for (int c2 = 0; c2 < cols; c2++)
                                {
                                    if (board.GetTerritory(r2, c2).GetName() == toTerritoryName)
                                    {
                                        players[i].Fortify(board.GetTerritory(r, c), board.GetTerritory(r2, c2), numArmies);
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }


    public static void Main(string[] args)
    {
        new Game().GameLoop();

    }

}
