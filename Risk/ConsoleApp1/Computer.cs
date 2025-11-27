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
    public override void Fortify(Territory fromTerritory, Territory toTerritory, int numArmies)
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
        // AI board analysis logic
    }
}
