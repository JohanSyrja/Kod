
namespace ConsoleApp1;

public class Computer : Player, IPlayer
{
    private int desicionScoreAttack;
    private int desicionScoreMove;

    /// <summary>
    /// Initializes a new instance of the Computer player with a specified starting territory.
    /// </summary>
	public Computer(Territory startingTerritory)
        : base("Computer", startingTerritory)
    {
        desicionScoreAttack = 0;
        desicionScoreMove = 0;
        isComputer = true;
    }

    /// <summary>
    /// Analyzes the board and updates decision scores for attacking and moving based on neighboring territories.
    /// </summary>
    /// <param name="territory"></param>
    /// <param name="board"></param>
    public void AnalyzeBoard(Territory territory, Board board)
    {
        foreach (var neighbor in territory.GetNeighbors(board))
        {
            if (neighbor.Owner != this)
            {
                if (territory.Armies >= neighbor.Armies)
                {
                    desicionScoreAttack += 8;
                }
                if (territory.Armies < neighbor.Armies)
                {
                    desicionScoreAttack -= 10;
                    desicionScoreMove += 10;
                }
            }
            else
            {
                desicionScoreMove += 5;
            }
        }


        if (StartingTerritory.Armies > 1)
        {
            desicionScoreAttack += 5;
        }
        if (territory.Armies < 3)
        {
            desicionScoreMove += 5;
        }

    }
    /// <summary>
    /// Chooses a territory to attack from the given territory on the board.
    /// </summary>
    /// <param name="board">The game board.</param>
    /// <param name="fromTerritory">The territory from which to attack.</param>
    /// <returns>The chosen territory to attack.</returns>
    public Territory? ChooseTerritoryToAttack(Board board, Territory fromTerritory)
    {
        List<Territory> neighbors = [];

        foreach (var neighbor in fromTerritory.GetNeighbors(board))
        {
            if (neighbor.Owner != this && neighbor.CanBeAttacked())
            {
                neighbors.Add(neighbor);
            }
        }

        if (neighbors.Count == 0)
        {
            return null;
        }

        Random rand = new();
        return neighbors[rand.Next(neighbors.Count)];
    }
    /// <summary>
    /// Chooses a territory to move to from the given territory on the board.
    /// </summary>
    /// <param name="board">The game board.</param>
    /// <param name="fromTerritory">The territory from which to move.</param>
    /// <returns>The chosen territory to move to.</returns>
    public Territory? ChooseTerritoryToMove(Board board, Territory fromTerritory)
    {
        List<Territory> ownedNeighbors = [];

        foreach (var neighbor in fromTerritory.GetNeighbors(board))
        {
            if (neighbor.Owner == this)
            {
                ownedNeighbors.Add(neighbor);
            }
        }
        if (ownedNeighbors.Count == 0)
        {
            return null;
        }

        Random rand = new();
        return ownedNeighbors[rand.Next(ownedNeighbors.Count)];
    }

    /// <summary>
    /// Retrieves the move decision score.  
    /// </summary>
    /// <returns>Move Decision Score</returns>
    public int GetMoveValue()
    {
        return desicionScoreMove;
    }

    /// <summary>
    /// Resets the attack and move decision scores to zero. 
    /// </summary>

    public void ResetScores()
    {
        desicionScoreAttack = 0;
        desicionScoreMove = 0;
    }
    /// <summary>
    /// Retrieves the attack decision score.
    /// </summary>
    /// <returns>Attack Decision Score</returns>
    public int GetAttackValue()
    {
        return desicionScoreAttack;
    }
    public override void Reinforce(Territory territory)
    {
        int reinforcement = territory.Armies < 3 ? 5 : 2;
        territory.AddArmy(reinforcement);
    }

    public override void Attack(Territory fromTerritory, Territory toTerritory, int numArmies)
    {
        if (fromTerritory.Owner != this || toTerritory.Owner == this || !fromTerritory.IsNeighbor(toTerritory))
        {
            return;
        }

        int maxAttack = fromTerritory.Armies - 1;
        if (maxAttack <= 0)
        {
            return;
        }

        int armiesToUse = toTerritory.Armies >= fromTerritory.Armies ? Math.Max(1, maxAttack / 2) : maxAttack;
        base.Attack(fromTerritory, toTerritory, armiesToUse);
        Console.WriteLine($"Datorn attackerar {toTerritory.Name} från {fromTerritory.Name} med {armiesToUse} arméer.");
    }

    public override void Move(Territory fromTerritory, Territory toTerritory, int numArmies)
    {
        if (fromTerritory.Owner != this || toTerritory.Owner != this)
        {
            return;
        }

        int movable = fromTerritory.Armies - 1;
        if (movable <= 0)
        {
            return;
        }

        int armiesToMove = toTerritory.Armies < fromTerritory.Armies ? Math.Max(1, movable / 2) : 1;
        base.Move(fromTerritory, toTerritory, armiesToMove);
        Console.WriteLine($"Datorn flyttar {armiesToMove} arméer från {fromTerritory.Name} till {toTerritory.Name}.");
    }
}
