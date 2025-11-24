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
    private Player? owner;
    private int armies;
    private string landtype;
    private string name;

    public Territory(string name, string landtype)
    {
        this.name = name ?? throw new ArgumentNullException(nameof(name));
        this.landtype = landtype ?? string.Empty;
        this.owner = null;
        this.armies = 0;
    }

    public string GetName()
    {
        return name;
    }

    public string GetLandType()
    {
        return landtype;
    }

    public string SetLandType(string newLandType)
    {
        landtype = newLandType ?? string.Empty;
        return landtype;
    }

    public Player? GetOwner()
    {
        return owner;
    }

    public void SetOwner(Player? newOwner)
    {
        owner = newOwner;
    }

    public int GetArmies()
    {
        return armies;
    }

    public void RemoveArmy(int num)
    {
        if (num < 0) throw new ArgumentOutOfRangeException(nameof(num));
        if (num > armies)
        {
            throw new ArgumentException("Cannot remove more armies than are present.");
        }
        armies -= num;
    }

    public void AddArmy(int num)
    {
        if (num < 0) throw new ArgumentOutOfRangeException(nameof(num));
        armies += num;
    }

    public override string ToString()
    {
        return $"Territory: {name}, Owner: {owner?.GetName() ?? "None"}, Armies: {armies}";
    }
}