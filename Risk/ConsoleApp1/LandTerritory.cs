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

    /// <summary>
    /// Determines if the territory can be attacked.
    /// Land can always be attacked when unowned, otherwise requires at least one army.
    /// </summary>
    /// <returns>True if the territory can be attacked; otherwise, false.</returns>
    public override bool CanBeAttacked()
    {
        return Owner == null || Armies > 0;
    }

    /// <summary>
    /// Returns a string representation of the land territory.
    /// </summary>
    /// <returns>A string describing the land territory.</returns>
    public override string ToString()
    {
        return $"LandTerritory: {Name}, Terrain: {TerrainType}, Owner: {Owner?.GetName() ?? "None"}, Armies: {Armies}";
    }

}
