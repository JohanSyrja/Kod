using System;

namespace ConsoleApp1;

public interface IPlayer
{
    string GetName();
    Territory GetStartingTerritory();
    void Reinforce(Territory territory);
    void Attack(Territory fromTerritory, Territory toTerritory, int numArmies);
    void Move(Territory fromTerritory, Territory toTerritory, int numArmies);
    bool GetPlayerTurn();
    bool SetPlayerTurn(bool isTurn);
    void SetLost(Board board);
    bool HasLost();
    int GetTotalArmies(Board board);
    void EndTurn();
}
