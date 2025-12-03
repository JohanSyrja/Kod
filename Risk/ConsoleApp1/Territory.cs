using System;

namespace ConsoleApp1;

/// <summary>
/// Abstract base class for all territory types (Land, Water, etc.).
/// </summary>
public abstract class Territory(string name, string type, int row, int col)
{

    public IPlayer? Owner { get; set; } = null;
    public int Armies { get; private set; } = 0;
    public string LandType { get; protected set; } = type ?? string.Empty;
    public string Name { get; } = name ?? throw new ArgumentNullException(nameof(name));
    public int Row { get; } = row;
    public int Col { get; } = col;

    /// <summary>
    /// Gets the initial of the owner's name for display purposes.
    /// </summary>
    /// <returns>String representing the owner's initial, or a space if unowned.</returns>
    public string OwnerInitial
    {
        get
        {
            var ownerName = Owner?.GetName();
            return !string.IsNullOrEmpty(ownerName) ? ownerName[0].ToString() : " ";
        }
    }

    /// <summary>
    /// Gets a list of neighboring territories on the board.
    /// </summary>
    /// <param name="board">The game board containing the territories.</param>
    /// <returns>List of neighboring Territory objects.</returns>
    public List<Territory> GetNeighbors(Board board)
    {
        var neighbors = new List<Territory>();
        
        for (int dr = -1; dr <= 1; dr++)
        {
            for (int dc = -1; dc <= 1; dc++)
            {
                
                if (dr == 0 && dc == 0) continue;
                int newRow = Row + dr;
                int newCol = Col + dc; 
                if (newRow >= 0 && newRow < board.Height && 
                    newCol >= 0 && newCol < board.Width)
                {
                    var neighboring = board.GetTerritory(newRow, newCol);
                    if (neighboring != null)
                    {
                        neighbors.Add(neighboring);
                    }
                }
            }
        }
        
        return neighbors;
    }
    
    /// <summary>
    /// Determines if another territory is a neighbor of this territory.
    /// </summary>
    /// <param name="other">The other territory to check.</param>
    /// <returns>True if the other territory is a neighbor; otherwise, false.</returns
    public bool IsNeighbor(Territory other)
    {
        if (other == null) return false;
        
        int dr = Math.Abs(Row - other.Row);
        int dc = Math.Abs(Col - other.Col);
        
        return dr <= 1 && dc <= 1 && (dr != 0 || dc != 0);
    }
    
    /// <summary>
    /// Removes a specified number of armies from the territory.
    /// </summary>
    /// <param name="num"></param>
    public void RemoveArmy(int num)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(num);
        if (num > Armies)
        {
            throw new ArgumentException("Cannot remove more armies than are present.");
        }
        Armies -= num;
    }

    /// <summary>
    /// Adds a specified number of armies to the territory.
    /// </summary>
    /// <param name="num"></param>
    public void AddArmy(int num)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(num);
        Armies += num;
    }
    
    /// <summary>
    /// Determines if the territory can be attacked.
    /// </summary>
    /// <returns>True if the territory can be attacked; otherwise, false.</returns>
    public virtual bool CanBeAttacked()
    {
        return true;
    }

    /// <summary>
    /// Returns a string representation of the territory.
    /// </summary>
    public override string ToString()
    {
        return $"Territory: {Name}, Owner: {Owner?.GetName() ?? "None"}, Armies: {Armies}, Type: {LandType}";
    }

    

    
}