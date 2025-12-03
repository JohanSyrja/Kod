using System;

namespace ConsoleApp1;

/// <summary>
/// Represents a land-based territory with standard rules.
/// Examples: Plains, Forest, Mountain, Desert.
/// </summary>
/// <remarks>
/// Initializes a new instance of the LandTerritory class.
/// </remarks>
public class LandTerritory(string name, string terrainType, int row, int col) : Territory(name, "Land", row, col)
{
    private string Type = terrainType ?? "Land";

    /// <summary>
    /// Gets or sets the terrain type of the land territory.
    /// </summary>
    /// <returns> The terrain type as a string. </returns>
    public string TerrainType
    {
        get => Type;
        set => Type = value ?? "Land";
    }

}
