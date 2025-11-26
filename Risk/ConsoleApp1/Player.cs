using System;

namespace ConsoleApp1;

public class Player
{
    private readonly string name;
    private bool isTurn;
    private bool hasWon;
    private readonly Territory StartingTerritory;
    public Player(string name, Territory startingTerritory)
    {
        this.name = name ?? throw new ArgumentNullException(nameof(name));
        this.StartingTerritory = startingTerritory ?? throw new ArgumentNullException(nameof(startingTerritory));
        hasWon = false;
    }

    public string GetName()
    {
        return name;
    }
    public Territory GetStartingTerritory()
    {
        return StartingTerritory;
    }
    public void combat(int attackingArmies, Territory defendingTerritory)
    {
        Random rand = new Random();
        while (defendingTerritory.GetArmies() > 0 || attackingArmies > 0)
        {
            int attackRoll = rand.Next(1, 7);
            int defendRoll = rand.Next(1, 7);
            if(defendingTerritory.GetArmies() == 0 || attackingArmies == 0)
            {
                break;
            }
            if (attackRoll > defendRoll )
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
    public void Reinforce(Territory Territory)
    {
        Territory.AddArmy(3);
    }
    public void Attack(Territory fromTerritory, Territory toTerritory, int numArmies)
    {
        if (fromTerritory.GetOwner() != this)
        {
            throw new InvalidOperationException("You do not own the attacking territory.");
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
    public bool GetPlayerTurn()
    {
        return isTurn;
    }
    public bool SetPlayerTurn(bool isTurn)
    {
        this.isTurn = isTurn;
        return isTurn;
    }
    public bool SetWon()
    {
        hasWon = true;
        return hasWon;
    }
    public bool HasWon()
    {
        return hasWon;
    }

    public override string ToString()
    {
        return $"Player: {name}";
    }
}
