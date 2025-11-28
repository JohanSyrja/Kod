using System;

namespace ConsoleApp1;

/// <summary>
/// Abstract base class for all territory types (Land, Water, etc.).
/// Provides shared properties and methods for armies, ownership, and display.
/// </summary>
public abstract class Territory
{
    //Name
    //owner
    //armies
    //neighbors
    //removearmy
    //addArmy
    public IPlayer? Owner { get; set; }
    public int Armies { get; private set; }
<<<<<<< HEAD
    public string LandType { get; protected set; }
=======
    public string LandType { get; set; }
>>>>>>> 8d6222cbc86c69b2278804e4b4bd871229ad454f
    public string Name { get; }
    public string OwnerInitial
    {
        get
        {
            var ownerName = Owner?.GetName();
            return !string.IsNullOrEmpty(ownerName) ? ownerName[0].ToString() : " ";
        }
<<<<<<< HEAD
    }

    protected Territory(string name, string type)
    {
        Name = name ?? throw new ArgumentNullException(nameof(name));
        LandType = type ?? string.Empty;
        Owner = null;
        Armies = 0;
    }
=======
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
>>>>>>> 8d6222cbc86c69b2278804e4b4bd871229ad454f

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
<<<<<<< HEAD
    }
    

    public virtual bool CanBeAttacked()
    {
        return true;
=======
>>>>>>> 8d6222cbc86c69b2278804e4b4bd871229ad454f
    }

    public override string ToString()
    {
<<<<<<< HEAD
        return $"Territory: {Name}, Owner: {Owner?.GetName() ?? "None"}, Armies: {Armies}, Type: {LandType}";
=======
        return $"Territory: {Name}, Owner: {Owner?.GetName() ?? "None"}, Armies: {Armies}";
>>>>>>> 8d6222cbc86c69b2278804e4b4bd871229ad454f
    }

    // Computed convenience properties
    

    
}