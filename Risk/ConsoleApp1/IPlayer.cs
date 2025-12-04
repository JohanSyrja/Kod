using System;

namespace ConsoleApp1;

/// <summary>
/// Interface defining the contract for player actions and properties.  
/// </summary>

/// KRAV #7 
/// Här änveder vi kravet if form av ett interface för att definiera de grundläggande funktionerna och egenskaperna som varje spelare måste implementera
/// och används för att möjliggöra polymorfism så att olika typer av spelare (mänskliga eller datorstyrda) kan hanteras på samma sätt i spelet
/// 
/// Används för att säkerställa att alla spelare har samma grundläggande funktionalitet och kan interagera med spelet på ett konsekvent sätt
public interface IPlayer
{
    /// <summary>
    /// Gets the name of the player.
    /// </summary>
    /// <returns>The name of the player as a string.</returns>
    string GetName();
    /// <summary>
    /// Gets the starting territory of the player.
    /// </summary>
    /// <returns>The starting territory.</returns>
    Territory GetStartingTerritory();
    /// <summary>
    /// Reinforces a territory by adding armies.
    /// </summary>
    /// <param name="territory"></param>
    void Reinforce(Territory territory);

    /// <summary>
    /// Attacks a territory from another territory with a specified number of armies.
    /// </summary>
    /// <param name="fromTerritory"></param>
    /// <param name="toTerritory"></param>
    /// <param name="numArmies"></param>
    void Attack(Territory fromTerritory, Territory toTerritory, int numArmies);

    /// <summary>
    /// Moves armies from one territory to another.
    /// </summary>
    /// <param name="fromTerritory"></param>
    /// <param name="toTerritory"></param>
    /// <param name="numArmies"></param>
    void Move(Territory fromTerritory, Territory toTerritory, int numArmies);

    /// <summary>
    /// Gets whether it is the player's turn.
    /// </summary>
    /// <returns>True if it is the player's turn</returns>
    bool GetPlayerTurn();

    /// <summary>
    /// Sets whether it is the player's turn.
    /// </summary>
    /// <param name="isTurn"></param>
    /// <returns></returns>
    bool SetPlayerTurn(bool isTurn);

    /// <summary>
    /// Sets the player's lost status based if they have any armies.
    /// </summary>
    /// <param name="board"></param>
    void SetLost(Board board);

    /// <summary>
    /// Determines if the player has lost the game.
    /// </summary>
    /// <returns>True if the player has lost the game</returns>
    bool HasLost();

    /// <summary>
    /// Calculates the total number of armies the player has across all territories on the board.
    /// </summary>
    /// <param name="board"></param>
    /// <returns>Total number of armies the player has</returns>
    int GetTotalArmies(Board board);

    /// <summary>
    /// Determines if the player is a computer-controlled player.
    /// </summary>
    /// <returns>True if the player is a computer-controlled player, otherwise false.</returns>
    bool IsComputer ();
}
