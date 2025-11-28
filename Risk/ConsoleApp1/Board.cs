using System;


namespace ConsoleApp1;

public class Board(int rows, int cols)
{
    private readonly int height = rows;
    private readonly int width = cols;
    public int Height => height;
    public int Width => width;

    private Territory[,] territories = new Territory[rows, cols];

    public Territory GetTerritory(int row, int col)
    {
        return territories[row, col];
    }
    public void SetTerritory(int row, int col, Territory territory)
    {
        territories[row, col] = territory;
    }
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

<<<<<<< HEAD
                    // Set background color based on territory type
                    if (territory is WaterTerritory)
                    {
                        Console.BackgroundColor = ConsoleColor.Blue;
                    }
                    else if (territory is LandTerritory)
                    {
                        Console.BackgroundColor = ConsoleColor.Green;
                    }

                    // Set foreground color based on owner
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
=======
                    Console.Write($"[{ownerChar} {territory.Name} {armies,2} {territory.LandType} ]");
>>>>>>> 8d6222cbc86c69b2278804e4b4bd871229ad454f
                }
                else
                {
                    Console.Write("[   ]");
                }
            }
            Console.WriteLine();
        }
    }
<<<<<<< HEAD

    // Överdrivet ta bort.
    private ConsoleColor GetColorForPlayer(string playerName)
    {
        if (string.IsNullOrEmpty(playerName)) return ConsoleColor.White;
        
        // Assign consistent colors based on player name
        int hash = Math.Abs(playerName.GetHashCode());
        var colors = new[] 
        { 
            ConsoleColor.Magenta, 
            ConsoleColor.Red,
            ConsoleColor.Cyan, 
            ConsoleColor.Yellow, 
            ConsoleColor.White
        };
        return colors[hash % colors.Length];
    }
=======
>>>>>>> 8d6222cbc86c69b2278804e4b4bd871229ad454f
    
    // Lookup territory by name (e.g., "0a0")
    public Territory? GetTerritoryByName(string name)
    {
        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                var territory = territories[i, j];
                if (territory != null && territory.Name == name)
                {
                    return territory;
                }
            }
        }
        return null;
    }
    
    
}
