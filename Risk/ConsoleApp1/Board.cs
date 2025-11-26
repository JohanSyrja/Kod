using System;


namespace ConsoleApp1;

public class Board(int rows, int cols)
{
    private readonly int height = rows;
    private readonly int width = cols;

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
                    var owner = territory.GetOwner();
                    string ownerChar = owner != null && !string.IsNullOrEmpty(owner.GetName()) ? owner.GetName()[0].ToString() : " ";
                    int armies = territory.GetArmies();

                    Console.Write($"[{ownerChar} {territory.GetName()} {armies,2} {territory.GetLandType()} ]");
                }
                else
                {
                    Console.Write("[   ]");
                }
            }
            Console.WriteLine();
        }
    }


}
