
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
                if(territory.Armies < neighbor.Armies)
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
}
