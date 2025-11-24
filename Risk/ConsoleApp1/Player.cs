using System;

namespace ConsoleApp1;

public class Player
{
    private readonly string name;

    public Player(string name)
    {
        this.name = name ?? throw new ArgumentNullException(nameof(name));
    }

    public string GetName()
    {
        return name;
    }
    public void combat(int attackingArmies, Territory defendingTerritory)
    {
        Random rand = new Random();
        while (defendingTerritory.GetArmies() > 0 || attackingArmies > 0)
        {
            int attackRoll = rand.Next(1, 7);
            int defendRoll = rand.Next(1, 7);

            if (attackRoll > defendRoll)
            {
                defendingTerritory.RemoveArmy(1);
            }
            else
            {
                attackingArmies--;
            }
        }
        if (defendingTerritory.GetArmies() == 0)
        {
            defendingTerritory.AddArmy(attackingArmies);
            defendingTerritory.SetOwner(this);
        }
        else
        {
            // Attacker lost all armies
        }
    }
    public void Reinforce(Territory territory, int numArmies)
    {
        if (territory.GetOwner() != this)
        {
            throw new InvalidOperationException("You do not own the territory.");
        }
        territory.AddArmy(numArmies);
    }
    public void Attack(Territory fromTerritory, Territory toTerritory, int numArmies)
    {
        if (fromTerritory.GetOwner() != this)
        {
            throw new InvalidOperationException("You do not own the attacking territory.");
        }
        if (numArmies >= fromTerritory.GetArmies())
        {
            throw new InvalidOperationException("Not enough armies to attack.");
        }
        combat(numArmies, toTerritory);
        fromTerritory.RemoveArmy(numArmies);
    }


    public void Fortify(Territory fromTerritory, Territory toTerritory, int numArmies)
    {
        if (fromTerritory.GetOwner() != this || toTerritory.GetOwner() != this)
        {
            throw new InvalidOperationException("You do not own both territories.");
        }
        if (numArmies >= fromTerritory.GetArmies())
        {
            throw new InvalidOperationException("Not enough armies to fortify.");
        }
        fromTerritory.RemoveArmy(numArmies);
        toTerritory.AddArmy(numArmies);
    }
    public void EndTurn()
    {
        // logic to end turn
    }

    public override string ToString()
    {
        return $"Player: {name}";
    }


}
