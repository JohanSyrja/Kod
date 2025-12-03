using System;
using System.ComponentModel;
using System.IO.Compression;
using System.IO.Pipelines;
using System.Runtime;

namespace ConsoleApp1;

/// <summary>
/// Initializes a new instance of the Game class with the specified board and players.
/// </summary>
public class Game(Board board, List<IPlayer> players)
{
    private readonly Board board = board ?? throw new ArgumentNullException(nameof(board));
    private readonly List<IPlayer> players = players ?? throw new ArgumentNullException(nameof(players));
    private bool gameOver = false;

    /// <summary>
    /// Main game loop handling player turns and game progression.
    /// </summary>

    public void GameLoop()
    {
        if (players.Count > 0)
        {
            players[0].SetPlayerTurn(true);
        }
        while (!gameOver)
        {

            for (int i = 0; i < players.Count; i++)
            {
                if (players[i].GetPlayerTurn() && !players[i].IsComputer())
                {
                    board.DisplayBoard();
                    Console.WriteLine($"{players[i].GetName()}'s turn.");
                    Console.WriteLine("Choose an action: Attack, Move, EndTurn");
                    ChooseMovePhase();
                }
                else if (players[i].GetPlayerTurn() && players[i].IsComputer())
                {
                    ComputerTurn((Computer)players[i]);
                    EndTurn();
                }
            }
        }

    }

    /// <summary>
    /// Displays the starting screen and instructions.
    /// </summary>
    private static void StartingScreen()
    {
        Console.WriteLine("Welcome to Risk");
        Console.WriteLine("Players take turns to attack, and move armies.");
        Console.WriteLine("The last player with territories remaining wins!");
        Console.WriteLine("Water has attrition - armies lose units each turn. down to 1 army.");
        Console.WriteLine("Please Enter your name:");
    }

    /// <summary>
    /// Handles the move phase for the current player.
    /// </summary>
    private void ChooseMovePhase()
    {
       
        string action = GetPlayerInput();
        if (action != null)
        {
            switch (action.ToLower())
            {
                case "attack":
                    AttackPhase(GetAttackInput());
                    board.DisplayBoard();
                    EndTurn();
                    break;
                case "move":
                    MovePhase(GetMoveInput());
                    board.DisplayBoard();
                    EndTurn();
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

    /// <summary>
    /// Gets player input for actions.
    /// </summary>
    /// <returns>Player input as a string</returns>
    private string GetPlayerInput()
    {
        Console.WriteLine("Enter your command:");
        string? input = Console.ReadLine() ?? GetPlayerInput();
        return input;
    }
    /// <summary>
    /// Ends the current player's turn and checks for game over conditions.
    /// </summary>
    private void EndTurn()
    {

        players.RemoveAll(p => p.GetTotalArmies(board) == 0);
        if(players.Count <= 1)
        {
            gameOver = true;
            Console.WriteLine("Game Over!");
            Console.WriteLine($"{players[0].GetName()} wins!");
            GameLoop();
        }
             
        {
        foreach (var player in players)
            {
                player.SetLost(board);
            }
        }
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

    /// <summary>
    /// Handles army attrition for water territories.
    /// </summary>
    private void ArmyAttrition()
    {
        for (int i = 0; i < board.Height; i++)
        {
            for (int j = 0; j < board.Width; j++)
            {
                var territory = board.GetTerritory(i, j);
                if (territory is WaterTerritory waterTerritory)
                {
                    waterTerritory.ArmyAttrition();
                }
            }
        }
    }

    /// <summary>
    /// Handles the attack phase for the current player.
    /// </summary>
    /// <returns>List of strings with player attack input</returns>
    public List<string> GetAttackInput()
    {
        Console.WriteLine("Enter from where you want to attack, to where you want to attack, and how many armies you want to use. \n You can only attack neighboring territories.");
        Console.WriteLine("To move back type back");
        Console.WriteLine("Format: fromTerritory, toTerritory, numArmies");
        string? input = Console.ReadLine() ?? string.Empty;
        
        if( input.ToLower().Equals("back"))
        { 
            ChooseMovePhase();
            return [];
        }
        
        List<string> inputList = [.. input.Split(',')];
        if (inputList.Count != 3)
        {
            Console.WriteLine("Invalid input. Please try again.");
            return GetAttackInput();
        }
        if (inputList.Count != 3)
        {
            Console.WriteLine("Invalid input. Please try again.");
            GetAttackInput();
            return GetAttackInput();
        }
        if (inputList[0].Trim() == inputList[1].Trim())
        {
            Console.WriteLine("You cannot attack the same territory. Please try again.");
            GetAttackInput();
            return GetAttackInput();
        }
        return inputList;
    }

    /// <summary>
    /// Handles the move phase for the current player.
    /// </summary>
    /// <returns>List of strings with player move input</returns>
    private List<string> GetMoveInput()
    {
        Console.WriteLine("Enter from where you want to move, to where you want to move and how many armies you want to move. \n You can only move to and from territories you own.");
        Console.WriteLine("Format: fromTerritory, toTerritory, numArmies");
        Console.WriteLine("To move back type back");
        string? input = Console.ReadLine() ?? string.Empty;
        List<string> inputList = [.. input.Split(',')];
    if( input.ToLower().Equals("back"))
        {
            ChooseMovePhase();
        }
    if (inputList.Count != 3)
        {
            Console.WriteLine("Invalid input. Please try again.");
            GetMoveInput();
        }
    if (inputList[0].Trim() == inputList[1].Trim())
        {
            Console.WriteLine("You cannot attack the same territory. Please try again.");
            GetMoveInput();
        }
        return inputList;
    }

    /// <summary>
    /// Handles the attack phase for the current player.
    /// </summary>
    private void AttackPhase(List<string> input)
    {
        string fromTerritoryName = input[0].Trim().ToLower();
        string toTerritoryName = input[1].Trim().ToLower();
        int numArmies  = int.TryParse(input[2].Trim(), out int result) ? result : 0;
        if (numArmies <= 0)
        {
            Console.WriteLine("Number of armies must be greater than zero. Try again.");
            AttackPhase(GetAttackInput());
            return;
        }
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
                if(toTerritory.CanBeAttacked() == false)
                {
                    Console.WriteLine("You cannot attack this territory. Try again.");
                    AttackPhase(GetAttackInput());
                    return;
                }
                if (toTerritory.Owner == players[i])
                {
                    Console.WriteLine("Cannot attack the same territory. Try again.");
                    AttackPhase(GetAttackInput());
                    return;
                }
                if (fromTerritory.Owner != players[i])
                {
                    Console.WriteLine("You do not own the attacking territory. Try again.");
                    AttackPhase(GetAttackInput());
                    return;
                }

                if (!fromTerritory.IsNeighbor(toTerritory))
                {
                    Console.WriteLine("Territories are not neighbors. Try again.");
                    AttackPhase(GetAttackInput());
                    return;
                }
        
                if (fromTerritory != null && toTerritory != null)
                {
                    players[i].Attack(fromTerritory, toTerritory, numArmies);
                }
            }
              
               
        }


    }

    /// <summary>
    /// Handles the reinforce phase for all players.    
    /// </summary>
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
                if (board.GetTerritory(board.Height/2, board.Width/2).Owner == players[i])
                {
                    ArmyAttrition();
                }
                for (int j = 0; j < board.GetOwnedTerritory(players[i]).Count; j++)
                {
                    players[i].Reinforce(players[i].GetStartingTerritory());    
                }                
                
            }
        }
    }

    /// <summary>
    /// Handles the move phase for the current player.
    /// </summary>
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
                    MovePhase(GetMoveInput());
                    return;
                }
                if (board?.GetTerritoryByName(fromTerritoryName)?.Owner != players[i])
                {
                    Console.WriteLine("You dont own both territories, please try again");
                    MovePhase(GetMoveInput());
                    return;
                }

                
                if (board?.GetTerritoryByName(toTerritoryName)?.Owner != players[i])
                {
                    Console.WriteLine("You dont own both territories, please try again");
                    MovePhase(GetMoveInput());
                    return;
                }
                players[i].Move(fromTerritory, toTerritory, numArmies);
            }
            
        }
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
                board.SetTerritory(i, j, new LandTerritory(countries[i * cols + j], "Land", i, j));
            }
        }

        // Create water
        
        board.SetTerritory(rows/2, cols/2, new WaterTerritory("Baltic", rows/2, cols/2));

        return board;
    }

    /// <summary>
    /// set up the players. 
    /// </summary>

    private static List<IPlayer> SetupPlayers(Board board)
    {
        int startingArmies = 5;
        var players = new List<IPlayer>
        {
            new Player(Console.ReadLine() ?? "Player1", board.GetTerritory(0, 0)),
            new Computer(board.GetTerritory(board.Height - 1, board.Width - 1))
        };

       
        players[0].GetStartingTerritory().Owner = players[0];
        players[0].GetStartingTerritory().AddArmy(startingArmies);

        players[1].GetStartingTerritory().Owner = players[1];
        players[1].GetStartingTerritory().AddArmy(startingArmies);

        return players;
    }

    /// <summary>
    /// Handles the computer player's turn.
    /// </summary>
    private void ComputerTurn(Computer computer)
    {
        Random rand = new();
        Console.WriteLine("Computer is making its move...");
        System.Threading.Thread.Sleep(1000); 
        
        var ownedTerritories = board.GetOwnedTerritory(computer);
        foreach (var territory in ownedTerritories)
        {
            computer.AnalyzeBoard(territory, board);
        }
        
        int attackValue = computer.GetAttackValue();
        int moveValue = computer.GetMoveValue();
        
    
        if (attackValue > moveValue)
        {
            Console.WriteLine("Computer attacks.");
            Territory? target = GetRandomOwnedTerritory(computer);
            if (target != null && target.Armies > 1)
            {
                Territory? toAttack = computer.ChooseTerritoryToAttack(board, target);
                if (toAttack != null && toAttack != target && toAttack.Owner != computer && target.IsNeighbor(toAttack))
                {
                    {
                        computer.Attack(target, toAttack, target.Armies -1);
                        Console.WriteLine("Computer attacks from " + target.Name + " to " + toAttack.Name);
                    }
                }
                else 
                {
                    Console.WriteLine("Computer could not find a valid attack.");
                    Console.WriteLine("Computer tries again");
                    ComputerTurn(computer);
                }
            }
        }
        else
        {
            Console.WriteLine("Computer moves its troops.");
            Territory? fromTerritory = GetRandomOwnedTerritory(computer);
            if (fromTerritory != null && fromTerritory.Armies > 1)
            {
                Territory? toMove = computer.ChooseTerritoryToMove(board, fromTerritory);
                if (toMove != null && toMove != fromTerritory &&  toMove.Owner == computer)
                {
                    {   
                        computer.Move(fromTerritory, toMove, fromTerritory.Armies -1);
                        Console.WriteLine("Computer moves from " + fromTerritory.Name + " to " + toMove.Name);
                    }
                }
                else {
                    Console.WriteLine("Computer could not find a valid move.");
                    Console.WriteLine("Computer tries again");
                    ComputerTurn(computer);
                }
            }
        }
        
        computer.ResetScores();
    }
    private Territory? GetRandomOwnedTerritory(Computer player)
    {
        List<Territory> ownedTerritories = board.GetOwnedTerritory(player);
        if (ownedTerritories.Count == 0) return null;
        Random rand = new();
        return ownedTerritories[rand.Next(ownedTerritories.Count)];
    }


    public static void Main(string[] args)
    {
        StartingScreen();
        var board = SetupBoard(3, 3);
        var players = SetupPlayers(board);

        var game = new Game(board, players);
        game.GameLoop();
    }
}
