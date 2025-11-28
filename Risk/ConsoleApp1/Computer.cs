using System;

namespace ConsoleApp1;

public class Computer : Player, IPlayer
{
	public Computer(Territory startingTerritory)
		: base("Computer", startingTerritory)
	{
	}
    private void MakeDecision()
    {
        
    }
    public override void Attack(Territory fromTerritory, Territory toTerritory, int numArmies)
    {
       
    }
<<<<<<< HEAD
    public override void Move(Territory fromTerritory, Territory toTerritory, int numArmies)
=======
    public override void Fortify(Territory fromTerritory, Territory toTerritory, int numArmies)
>>>>>>> 8d6222cbc86c69b2278804e4b4bd871229ad454f
    {

    }
    public override void EndTurn()
    {
        base.EndTurn();
    }
    private void TakeTurn()
    {
        MakeDecision();
        EndTurn();
    }
    private void AnalyzeBoard()
    {
<<<<<<< HEAD
        
=======
        // AI board analysis logic
>>>>>>> 8d6222cbc86c69b2278804e4b4bd871229ad454f
    }
}
