using System;

namespace ConsoleApp1;

public class Territory
{
    //Name
    //owner
    //armies
    //neighbors
    //removearmy
    //addArmy
    public IPlayer? Owner { get; set; }
    public int Armies { get; private set; }
    public string LandType { get; set; }
    public string Name { get; }
    public string OwnerInitial
    {
        get
        {
            var ownerName = Owner?.GetName();
            return !string.IsNullOrEmpty(ownerName) ? ownerName[0].ToString() : " ";
        }
    }
    
    public bool IsWater => string.Equals(LandType, "Water", StringComparison.OrdinalIgnoreCase);

    public Territory(string name, string type)
    {
        Name = name ?? throw new ArgumentNullException(nameof(name));
        LandType = type ?? string.Empty;
        Owner = null;
        Armies = 0;
    }

    public Territory(string name) : this(name, "Land") { }

    public void RemoveArmy(int num)
    {
        if (num < 0) throw new ArgumentOutOfRangeException(nameof(num));
        if (num > Armies)
        {
            throw new ArgumentException("Cannot remove more armies than are present.");
        }
        Armies -= num;
    }

    public void AddArmy(int num)
    {
        if (num < 0) throw new ArgumentOutOfRangeException(nameof(num));
        Armies += num;
    }

    public override string ToString()
    {
        return $"Territory: {Name}, Owner: {Owner?.GetName() ?? "None"}, Armies: {Armies}";
    }

    // Computed convenience properties
    

    
}