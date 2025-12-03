using System;


namespace ConsoleApp1;

public class Board(int rows, int cols)
{
    private readonly int height = rows;
    private readonly int width = cols;
    public int Height => height;
    public int Width => width;

    private Territory[,] territories = new Territory[rows, cols];

    /// <summary>
    /// Retrieves a territory at the specified row and column.
    /// </summary>
    public Territory GetTerritory(int row, int col)
    {
        return territories[row, col];
    }

    /// <summary>
    /// Sets a territory at the specified row and column.   
    /// </summary>
    public void SetTerritory(int row, int col, Territory territory)
    {
        territories[row, col] = territory;
    }

    /// <summary>
    /// Retrieves all territories owned by a specific player.
    /// </summary>

    public List<Territory> GetOwnedTerritory(IPlayer player)
    {
        var ownedTerritories = new List<Territory>();
        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                var territory = territories[i, j];
                if (territory.Owner == player)
                {
                    ownedTerritories.Add(territory);
                }
            }
        }
        return ownedTerritories;
    }

    /// <summary>
    /// Displays the board in the console with color coding for different territory types and owners.
    /// </summary>
    public void DisplayBoard()
    {
        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                Territory territory = territories[i, j];
                if (territory != null)
                {
                    string ownerChar = territory.OwnerInitial;
                    int armies = territory.Armies;

                   
                   
                    if (territory is WaterTerritory)
                    {
                        Console.BackgroundColor = ConsoleColor.Blue;
                    }
                    else if (territory is LandTerritory)
                    {
                        Console.BackgroundColor = ConsoleColor.Green;
                    }

                   
                   
                    if (territory.Owner != null)
                    {
                        Console.ForegroundColor = GetColorForPlayer(territory.Owner.GetName());
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Gray;
                    }

                    Console.Write($"[{ownerChar} {territory.Name} {armies,2}]");
                    Console.ResetColor();
                }
                else
                {
                    Console.Write("[   ]");
                }
            }
            Console.WriteLine();
        }
    }

    /// <summary>
    /// Determines a console color based on the player's name for consistent coloring.  
    /// </summary>
    private ConsoleColor GetColorForPlayer(string playerName)
    {
        if (string.IsNullOrEmpty(playerName)) return ConsoleColor.White;
    
        int hash = Math.Abs(playerName.GetHashCode());
        var colors = new[] 
        { 
            ConsoleColor.Magenta, 
            ConsoleColor.Red,
        };
        return colors[hash % colors.Length];
    }
    
    /// <summary>
    /// Retrieves a territory by its name, ignoring case.   
    /// </summary>
    public Territory? GetTerritoryByName(string name)
    {
        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                var territory = territories[i, j];
                if (territory != null && territory.Name.Equals(name, StringComparison.OrdinalIgnoreCase))
                {
                    return territory;
                }
            }
        }
        return null;
    }
    
    
}
