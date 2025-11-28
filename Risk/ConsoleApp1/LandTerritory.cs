using System;

namespace ConsoleApp1;

/// <summary>
/// Represents a land-based territory with standard rules.
/// Examples: Plains, Forest, Mountain, Desert.
/// </summary>
public class LandTerritory : Territory
{
    private string terrainType;

    public LandTerritory(string name, string terrainType = "Land")
        : base(name, "Land")
    {
        this.terrainType = terrainType ?? "Land";
    }

    public string TerrainType
    {
        get => terrainType;
        set => terrainType = value ?? "Land";
    }

    public override string ToString()
    {
        return $"LandTerritory: {Name}, Terrain: {terrainType}, Owner: {Owner?.GetName() ?? "None"}, Armies: {Armies}";
    }
}
