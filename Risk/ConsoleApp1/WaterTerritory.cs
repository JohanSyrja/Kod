using System;

namespace ConsoleApp1;

/// <summary>
/// Represents a water-based territory with naval-specific rules.
/// </summary>
/// <remarks>
/// Initializes a new instance of the WaterTerritory class.
/// </remarks>
public class WaterTerritory(string name, int row, int col, string waterType = "Water") : Territory(name, "Water", row, col)
{
    private string Type = waterType ?? "Water";

    /// <summary>
    /// Gets or sets the water type of the territory.
    /// </summary>
    public string WaterType
    {
        get => Type;
        set => Type = value ?? "Water";
    }

    /// <summary>
    /// Determines if the territory can be attacked.
    /// </summary>
    /// <returns>True if the territory can be attacked. otherwise, false.</returns>
    public override bool CanBeAttacked()
    {
        return true;
    }

    /// <summary>
    /// Applies army attrition rules specific to water territories.
    /// </summary>
    public void ArmyAttrition()
    {
        if (Armies > 1)
        {
            RemoveArmy(Armies / 2); 
        }
    }
    /// <summary>
    /// Returns a string representation of the water territory. 
    /// </summary>
    /// <returns>A string describing the water territory.</returns>
    public override string ToString()
    {
        return $"WaterTerritory: {Name}, Type: {Type}, Owner: {Owner?.GetName() ?? "None"}, Armies: {Armies}";
    }
}
