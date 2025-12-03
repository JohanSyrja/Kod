using System;

namespace ConsoleApp1;

/// <summary>
/// Initializes a new instance of the Player class with a specified name and starting territory.
/// </summary>
public class Player(string name, Territory startingTerritory) : IPlayer
{
    private readonly string name = name ?? throw new ArgumentNullException(nameof(name));
    private bool isTurn;
    private bool hasLost = false;
    public bool isComputer = false;
    protected readonly Territory StartingTerritory = startingTerritory ?? throw new ArgumentNullException(nameof(startingTerritory));

    
    public string GetName()
    {
        return name;
    }

    
    public Territory GetStartingTerritory()
    {
        return StartingTerritory;
    }

    
    public bool IsComputer() 
    {
        return isComputer;
    }
    /// <summary>
    /// Simulates combat between attacking armies and a defending territory.
    /// </summary>
    /// <param name="attackingArmies"></param>
    /// <param name="defendingTerritory"></param>
    /// <returns>The number of remaining attacking armies after combat.</returns>
    protected int Combat(int attackingArmies, Territory defendingTerritory)
    {
    
        Random rand = new();
        while (defendingTerritory.Armies > 0 && attackingArmies > 0)
        {
            int attackRoll = rand.Next(1, 7);
            int defendRoll = rand.Next(1, 7);
            
            if (attackRoll > defendRoll )
            {
                defendingTerritory.RemoveArmy(1);
            }
            else
            {
                attackingArmies--;
            }
        }
        if (defendingTerritory.Armies == 0 && attackingArmies > 0)
        {
            defendingTerritory.AddArmy(attackingArmies);
            defendingTerritory.Owner = this;
        }
        return attackingArmies;
    }

    public virtual void Reinforce(Territory Territory)
    {
        Territory.AddArmy(3);
    }
   
    public virtual void Attack(Territory fromTerritory, Territory toTerritory, int numArmies)
    {
        int remainingArmies = Combat(numArmies, toTerritory);
        int armiesToRemove = (toTerritory.Owner == this) ? numArmies : (numArmies - remainingArmies);
        if (armiesToRemove > 0 && armiesToRemove <= fromTerritory.Armies)
        {
            fromTerritory.RemoveArmy(armiesToRemove);
        }
    }

  
    public virtual void Move(Territory fromTerritory, Territory toTerritory, int numArmies)
    {
        fromTerritory.RemoveArmy(numArmies);
        toTerritory.AddArmy(numArmies);
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

}
