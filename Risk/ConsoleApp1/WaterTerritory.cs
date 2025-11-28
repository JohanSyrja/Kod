using System;

namespace ConsoleApp1;

/// <summary>
/// Represents a water-based territory with naval-specific rules.
/// Examples: Ocean, Sea, River.
/// </summary>
public class WaterTerritory : Territory
{
    private string waterType; // 
    public WaterTerritory(string name, string waterType = "Water")
        : base(name, "Water")
    {
        this.waterType = waterType ?? "Water";
    }

    public string WaterType
    {
        get => waterType;
        set => waterType = value ?? "Water";
    }

    
    public override bool CanBeAttacked()
    {
        return false;
    }
    private void ArmyAttrition()
    {
        if (Armies > 1)
        {
            RemoveArmy(Armies%2); 
        }
    }

    public override string ToString()
    {
        return $"WaterTerritory: {Name}, Type: {waterType}, Owner: {Owner?.GetName() ?? "None"}, Armies: {Armies}";
    }
}
