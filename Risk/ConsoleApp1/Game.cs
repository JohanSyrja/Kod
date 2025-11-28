using System;
using System.ComponentModel;
using System.IO.Compression;

namespace ConsoleApp1;

public class Game
{
<<<<<<< HEAD
    private readonly Board board;
    private readonly List<IPlayer> players;
    private bool gameOver = false;
    

    public Game(Board board, List<IPlayer> players)
=======
    /// game start logic 
    /// manage turns
    /// implement game loop
    private List<IPlayer> players = [];
    private Board? board;
    private int rows;
    private int cols;
    public void GameLoop()
>>>>>>> 8d6222cbc86c69b2278804e4b4bd871229ad454f
    {
        this.board = board ?? throw new ArgumentNullException(nameof(board));
        this.players = players ?? throw new ArgumentNullException(nameof(players));
    }

    public void GameLoop()
    {
        // Start the game with the first player's turn
        if (players.Count > 0)
        {
<<<<<<< HEAD
            players[0].SetPlayerTurn(true);
        }
        while (!gameOver)
=======
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
>>>>>>> 8d6222cbc86c69b2278804e4b4bd871229ad454f
        {

            for (int i = 0; i < players.Count; i++)
            {
                if (players[i].GetPlayerTurn())
                {
                    board.DisplayBoard();
                    Console.WriteLine($"{players[i].GetName()}'s turn.");
                    Console.WriteLine("Choose an action: Attack, Move, EndTurn");
                    string action = GetPlayerInput();
                    if (action != null)
                    {
                        switch (action.ToLower())
                        {
                            case "attack":
                                AttackPhase(GetAttackInput());
                                board.DisplayBoard();
                                break;
                            case "move":
                                MovePhase(GetMoveInput());
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

        players.RemoveAll(p => p.GetTotalArmies(board) == 0);
        if(players.Count <= 1)
        {
            gameOver = true;
            Console.WriteLine("Game Over!");
            return;
        }
        
        {
        foreach (var player in players)
            {
                player.SetLost(board);
            }
        }
        if (players.Count(p => !p.HasLost()) == 1)
        {
            gameOver = true;
            Console.WriteLine("Game Over!");
            return;
        }
        Console.WriteLine(players.Count(p => !p.HasLost()));
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
    public List<string> GetAttackInput()
    {
        Console.WriteLine("Enter your attack command (e.g., '1a1,  2a2, numArmies'):");
        string? input = Console.ReadLine() ?? string.Empty;
        List<string> inputList = [.. input.Split(',')];
        if (inputList.Count != 3)
        {
            Console.WriteLine("Invalid input. Please try again.");
            GetAttackInput();
        }
        if (inputList[0].Trim() == inputList[1].Trim())
        {
            Console.WriteLine("You cannot attack the same territory. Please try again.");
            GetAttackInput();
        }
        List<string> result = [.. input.Split([','])];
        return result;
    }
    private List<string> GetMoveInput()
    {
<<<<<<< HEAD
        Console.WriteLine("Enter your Move command (e.g. 1a1,  2a2, numArmies'):");
=======
        Console.WriteLine("Enter your fortify command (e.g. 1a1,  2a2, numArmies'):");
>>>>>>> 8d6222cbc86c69b2278804e4b4bd871229ad454f
        string? input = Console.ReadLine() ?? string.Empty;
        List<string> inputList = [.. input.Split(',')];
        if (inputList.Count != 3)
        {
            Console.WriteLine("Invalid input. Please try again.");
            GetMoveInput();
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
<<<<<<< HEAD
                if(toTerritory.CanBeAttacked() == false)
                {
                    Console.WriteLine("You cannot attack this territory. Try again.");
                    AttackPhase(GetAttackInput());
                    return;
                }
=======
>>>>>>> 8d6222cbc86c69b2278804e4b4bd871229ad454f
                if (toTerritory.Owner == players[i])
                {
                    Console.WriteLine("Cannot attack the same territory. Try again.");
                    AttackPhase(GetAttackInput());
                    return;
                }
<<<<<<< HEAD
                if (fromTerritory.Owner != players[i])
                {
                    Console.WriteLine("You do not own the attacking territory. Try again.");
                    AttackPhase(GetAttackInput());
                    return;
                }
        
                // Check adjacency (8-connected: vertical, horizontal, or diagonal neighbors)
                int dr = Math.Abs(GetRow(fromTerritoryName) - GetRow(toTerritoryName));
                int dc = Math.Abs(GetCol(fromTerritoryName) - GetCol(toTerritoryName));
                
                // Both dr and dc must be 0 or 1, and at least one must be non-zero
                if (dr > 1 || dc > 1 || (dr == 0 && dc == 0))
=======
                // Ensure target is a neighboring cell (including diagonals)
                int dr = Math.Abs(GetRow(fromTerritoryName) - GetRow(toTerritoryName));
                int dc = Math.Abs(GetCol(fromTerritoryName) - GetCol(toTerritoryName));
                if ((dr == 0 && dc == 0) || dr > 1 || dc > 1)
>>>>>>> 8d6222cbc86c69b2278804e4b4bd871229ad454f
                {
                    Console.WriteLine("Target territory is not adjacent. You can only attack neighboring territories.");
                    AttackPhase(GetAttackInput());
                    return;
                }
<<<<<<< HEAD
              
=======
                // Only call Attack if both territories are not null
>>>>>>> 8d6222cbc86c69b2278804e4b4bd871229ad454f
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
                if(players[i].GetStartingTerritory().Owner != players[i])
                {
                    Console.WriteLine("Your starting territory has been captured. You cannot reinforce.");
                    return;
                }
                players[i].Reinforce(players[i].GetStartingTerritory());
            }
        }
    }
    private void MovePhase(List<string> input)
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
<<<<<<< HEAD
                    MovePhase(GetMoveInput());
                    return;
                }
                if (board?.GetTerritoryByName(fromTerritoryName)?.Owner != players[i] || board?.GetTerritoryByName(toTerritoryName)?.Owner != players[i])
                {
                    Console.WriteLine("You dont own both territories, please try again");
                    MovePhase(GetMoveInput());
                    return;
                }
                players[i].Move(fromTerritory, toTerritory, numArmies);
=======
                    FortifyPhase(GetFortifyInput());
                    return;
                }
                if(board?.GetTerritoryByName(fromTerritoryName)?.Owner != players[i] || board?.GetTerritoryByName(toTerritoryName)?.Owner != players[i] ){
                    Console.WriteLine("You dont own both territories, please try again");
                    FortifyPhase(GetFortifyInput());
                    return;
                }
                players[i].Fortify(fromTerritory, toTerritory, numArmies);
>>>>>>> 8d6222cbc86c69b2278804e4b4bd871229ad454f
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
<<<<<<< HEAD
        // Setup: Create and configure the board and players (Dependency Injection)
        var board = SetupBoard(3, 3);
        var players = SetupPlayers(board);

        // Inject dependencies into Game and run
        var game = new Game(board, players);
        game.GameLoop();
    }

    /// <summary>
    /// set up the board with territories.
    /// </summary>
    private static Board SetupBoard(int rows, int cols)
    {
        var board = new Board(rows, cols);
        List<string> countries = new List<string> { "Sweden", "Norway", "Denmark", "Finland", "Iceland", "Estonia", "Latvia", "Lithuania", "Poland"};
        // Initialize board with Land
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                board.SetTerritory(i, j, new LandTerritory($"{i}a{j}", "Land"));
            }
        }

        // Create water
        
        board.SetTerritory(rows/2, cols/2, new WaterTerritory($"Baltic", "Water"));

        return board;
    }


    private static List<IPlayer> SetupPlayers(Board board)
    {
        int startingArmies = 5;
        var players = new List<IPlayer>
        {
            new Player("Alice", board.GetTerritory(0, 0)),
            new Player("Bob", board.GetTerritory(board.Height - 1, board.Width - 1))
        };

        // Initialize starting territories
        players[0].GetStartingTerritory().Owner = players[0];
        players[0].GetStartingTerritory().AddArmy(startingArmies);

        players[1].GetStartingTerritory().Owner = players[1];
        players[1].GetStartingTerritory().AddArmy(startingArmies);

        return players;
    }
=======
        new Game().GameLoop();
    }
>>>>>>> 8d6222cbc86c69b2278804e4b4bd871229ad454f
}
