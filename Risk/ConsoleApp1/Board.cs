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

                    Console.Write($"[{ownerChar} {territory.Name} {armies,2} {territory.LandType} ]");
                }
                else
                {
                    Console.Write("[   ]");
                }
            }
            Console.WriteLine();
        }
    }
    
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
