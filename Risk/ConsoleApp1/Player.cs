using System;

namespace ConsoleApp1;

public class Player : IPlayer
{
    private readonly string name;
    private bool isTurn;
<<<<<<< HEAD
    private bool hasLost;
=======
    private bool hasWon;
>>>>>>> 8d6222cbc86c69b2278804e4b4bd871229ad454f
    protected readonly Territory StartingTerritory;
    public Player(string name, Territory startingTerritory)
    {
        this.name = name ?? throw new ArgumentNullException(nameof(name));
        this.StartingTerritory = startingTerritory ?? throw new ArgumentNullException(nameof(startingTerritory));
        hasLost = false;
    }

    public string GetName()
    {
        return name;
    }
    public Territory GetStartingTerritory()
    {
        return StartingTerritory;
    }
    protected void Combat(int attackingArmies, Territory defendingTerritory)
    {
        Random rand = new Random();
        while (defendingTerritory.Armies > 0 || attackingArmies > 0)
        {
            int attackRoll = rand.Next(1, 7);
            int defendRoll = rand.Next(1, 7);
            if(defendingTerritory.Armies == 0 || attackingArmies == 0)
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
        if (defendingTerritory.Armies == 0)
        {
            defendingTerritory.AddArmy(attackingArmies);
            defendingTerritory.Owner = this;
        }
        else
        {
            // Attacker lost all armies
        }
    }
    public virtual void Reinforce(Territory Territory)
    {
        Territory.AddArmy(3);
    }
    public virtual void Attack(Territory fromTerritory, Territory toTerritory, int numArmies)
    {
<<<<<<< HEAD
=======
        if (fromTerritory.Owner != this)
        {
            throw new InvalidOperationException("You do not own the attacking territory.");
        }
>>>>>>> 8d6222cbc86c69b2278804e4b4bd871229ad454f
        Combat(numArmies, toTerritory);
        fromTerritory.RemoveArmy(numArmies);
    }


<<<<<<< HEAD
    public virtual void Move(Territory fromTerritory, Territory toTerritory, int numArmies)
=======
    public virtual void Fortify(Territory fromTerritory, Territory toTerritory, int numArmies)
>>>>>>> 8d6222cbc86c69b2278804e4b4bd871229ad454f
    {
        fromTerritory.RemoveArmy(numArmies);
        toTerritory.AddArmy(numArmies);
    }
    public virtual void EndTurn()
    {
        // Default no-op; subclasses may override to perform end-of-turn actions
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
    public void SetLost(Board board)
    {
        if( GetTotalArmies(board) == 0)
        {
            hasLost = true;
            return;
        }
        hasLost = false;
    }
    public bool HasLost()
    {
        return hasLost;
    }

    public int GetTotalArmies(Board board)
    {
        int totalArmies = 0;
        for (int i = 0; i < board.Height; i++)
        {
            for (int j = 0; j < board.Width; j++)
            {
                Territory territory = board.GetTerritory(i, j);
                if (territory != null && territory.Owner == this)
                {
                    totalArmies += territory.Armies;
                }
            }
        }
        return totalArmies;
    }

    public int GetTotalArmies(Board board)
    {
        int totalArmies = 0;
        for (int i = 0; i < board.Height; i++)
        {
            for (int j = 0; j < board.Width; j++)
            {
                Territory territory = board.GetTerritory(i, j);
                if (territory != null && territory.Owner == this)
                {
                    totalArmies += territory.Armies;
                }
            }
        }
        return totalArmies;
    }

    public override string ToString()
    {
        return $"Player: {name}";
    }
}
