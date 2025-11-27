using System;

namespace ConsoleApp1;

public interface IPlayer
{
    string GetName();
    Territory GetStartingTerritory();
    void Reinforce(Territory territory);
    void Attack(Territory fromTerritory, Territory toTerritory, int numArmies);
    void Fortify(Territory fromTerritory, Territory toTerritory, int numArmies);
    bool GetPlayerTurn();
    bool SetPlayerTurn(bool isTurn);
    bool SetWon();
    bool HasWon();
    int GetTotalArmies(Board board);
    void EndTurn();
}
