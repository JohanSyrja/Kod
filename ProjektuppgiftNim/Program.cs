using System.Configuration.Assemblies;
using System.Drawing;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;

class Nim
{
    static int sticksInStack = 5;
    static int[] result = new int[2];
    static int[] stack = new int[3];
    static string[,] stacks = new string[3, 5];
    static string player1 = "";
    static string player2 = "";
    static string computerPlayer = "computer";
    static string currentPlayer = "";
    static bool playerwon = false;
    static int gameMode = 0;
    static int whichStack = 0;
    static int amountOfSticks = 0;
    /// <summary>
    /// Kollar om alla högar är tomma
    /// </summary>
    /// <returns>boolean true om alla högar är tomma</returns>
    static bool checkIfEmpty()
    {
        return stack[0] == 0 && stack[1] == 0 && stack[2] == 0;
    }
    /// <summary>
    /// Tar emot användar input
    /// </summary>
    /// <returns>retunerar användar input</returns>
    static string getInput()
    {
        string? input = Console.ReadLine();
        return input ?? "";
    }
    /// <summary>
    /// Skriver ut välkomstmedelande, där användare kan välja att läsa regler
    /// </summary>
    static void welcomeMessage()
    {
        Console.WriteLine("Welcome to the program!, type rules for more info");
        Console.WriteLine("To play, type play");
        if (getInput() == "rules")
        {
            Console.WriteLine("There are three piles with five sticks each. The goal is to take the last stick from the last pile. Each player may remove as many sticks as they want from a specific pile.");
        }
        else return;
    }
    /// <summary>
    /// Tar emot namn
    /// </summary>
    /// <returns>namn</returns>
    static string getName()
    {
        Console.WriteLine("Type your name");
        return getInput();
    }
    /// <summary>
    /// fyller alla högar
    /// </summary>
    static void repopulateStacks()
    {
        stack[0] = sticksInStack;
        stack[1] = sticksInStack;
        stack[2] = sticksInStack;
    }
    /// <summary>
    /// tar bort antal pinnar u en specifik hög
    /// </summary>
    /// <param name="stackNumber">Från vilken hög den ska ta bort</param>
    /// <param name="amount">Hur mycket den ska ta bort</param>
    static void removeSticks(int stackNumber, int amount)
    {
        if (amount < 0) amount = 1;

        if (stack[stackNumber - 1] - amount < 0)
        {
            stack[stackNumber - 1] = 0;
        }
        else
        {
            stack[stackNumber - 1] -= amount;
        }
    }
    /// <summary>
    /// Koller current player
    /// </summary>
    /// <param name="player1">spelare 1</param>
    /// <param name="player2">spelare 2</param>
    /// <returns>retunerar den spelare vars tur det är</returns>
    static string checkCurrentPlayer(string player1, string player2)
    {
        if (currentPlayer == player1) return player1;
        else return player2;
    }
    /// <summary>
    /// tar användarinput för vilken stack
    /// </summary>
    /// <param name="input">Användarens input</param>
    /// <returns>Den stack som valts</returns>
    static int chooseStack(string input)
    {
        int temp;
        if (Int32.TryParse(input, out int result)) temp = result;
        else
        {
            Console.WriteLine("Please choose a valid stack");
            return chooseStack(getInput());
        }

        if (temp - 1 >= 0 && temp - 1 <= stack.Length - 1 && stack[temp - 1] != 0)
        {
            return temp;
        }
        else
        {
            return chooseStack(getInput());

        }
    }
    /// <summary>
    /// Användare väljer hur många pinnar som ska plockas bort
    /// </summary>
    /// <param name="input">användares input</param>
    /// <returns>antalet pinnar som ska plockas bort</returns>
    static int chooseSticks(string input)
    {
        int temp;
        if (Int32.TryParse(input, out int result)) temp = result;
        else
        {
            Console.WriteLine("Please choose a valid amount of sticks");
            return chooseSticks(getInput());
        }
        if (temp >= 0 && temp <= stack[whichStack - 1])
        {
            Console.WriteLine($"You have chosen to remove {temp} sticks from stack {whichStack}");
            return temp;
        }
        else
        {
            return chooseSticks(getInput());
        }
    }
    /// <summary>
    /// Tar hand om allt en spelare gör under sin omgång och kollar om spelaren vunnit
    /// </summary>
    static void playerMove()
    {
        Console.WriteLine("Chose which stack to remove from:");
        whichStack = chooseStack(getInput());

        Console.WriteLine($"The stack has {amountInStack(whichStack)} amount of sticks");
        Console.WriteLine("How many sticks do you want to remove");
        amountOfSticks = chooseSticks(getInput());

        removeSticks(whichStack, amountOfSticks);
        printStacks(stack);
        playerwon = checkIfEmpty();

    }
    /// <summary>
    /// Skriver ut när splearen har vunnit
    /// </summary>
    /// <returns>Vilken spelare som har vunnit och hur många poäng som varje spelare har</returns>
    static string playerWin()
    {

        return $"Player {currentPlayer} wins! Current scores: \n {player1} has {result[0]} \n {player2} has {result[1]}";
    }
    /// <summary>
    /// Kollar antalet pinnar i en hög
    /// </summary>
    /// <param name="pile">Vilken hög som kollas</param>
    /// <returns>antalet pinnar i högen</returns>
    static string amountInStack(int pile)
    {
        return stack[pile - 1].ToString();
    }
    /// <summary>
    /// skriver ut högarna 
    /// </summary>
    /// <param name="stack">alla högar</param>

    static void printStacks(int[] stack)
    {
        for (int i = 0; i < stack.Length; i++)
        {
            for (int j = 0; j < sticksInStack; j++)
            {
                stacks[i, j] = "|";
            }
        }
        Console.WriteLine("Current stacks:");
        for (int i = 0; i < stack.Length; i++)
        {
            Console.Write($"Stack {i + 1}: ");
            for (int j = 0; j < stack[i]; j++)
            {
                Console.Write(stacks[i, j]);
            }
            Console.WriteLine();
        }

    }
    /// <summary>
    /// updaterar resultaten för spelares vinster
    /// </summary>
    static void updateResult()
    {
        if (currentPlayer == player1)
        {
            result[0] += 1;
        }
        else result[1] += 1;

    }
    /// <summary>
    /// kollar om spelaren vill splea mot dator eller annan spelare
    /// </summary>
    static void checkGameMode()
    {

        Console.WriteLine("Choose game mode: 1. Player vs Player 2. Player vs Computer");
        if (Int32.TryParse(getInput(), out int res))
        {
            gameMode = res;

        }
        else
        {
            Console.WriteLine("Please choose a valid gamemode '1' or '2'");
            checkGameMode();
        }
        if (gameMode == 1)
        {
            player1 = getName();
            player2 = getName();
            currentPlayer = player1;
        }
        if (gameMode == 2)
        {
            player1 = getName();
            player2 = computerPlayer;
            currentPlayer = player1;
        }
        else
        {
            checkGameMode();
        }
        return;



    }
    /// <summary>
    /// tar fram datorns giltiga drag
    /// </summary>
    static void getValidComputerMove()
    {
        Random random = new Random();
        whichStack = random.Next(1, 4);
        if (stack[whichStack - 1] == 0 && whichStack == 1) whichStack = 2;
        if (stack[whichStack - 1] == 0 && whichStack == 3) whichStack = 2;
        if (stack[whichStack - 1] == 0 && whichStack == 2)
        {
            if (stack[0] == 0) whichStack = 3;
            if (stack[2] == 0) whichStack = 1;
        }
        amountOfSticks = random.Next(1, 6);
        Console.WriteLine(amountOfSticks);


    }
    /// <summary>
    /// Det drag som görs när användare spelar mot datorn
    /// </summary>
    static void ComputerMove()
    {
        if (currentPlayer == player2)
        {
            getValidComputerMove();
            removeSticks(whichStack, amountOfSticks);
            printStacks(stack);
            playerwon = checkIfEmpty();
        }
        if (currentPlayer == player1)
        {
            Console.WriteLine("Chose which stack to remove from:");
            whichStack = chooseStack(getInput());

            Console.WriteLine($"The stack has {amountInStack(whichStack)} amount of sticks");
            Console.WriteLine("How many sticks do you want to remove");
            amountOfSticks = chooseSticks(getInput());

            removeSticks(whichStack, amountOfSticks);
            printStacks(stack);
            playerwon = checkIfEmpty();
        }
    }
    /// <summary>
    /// startar om spelet 
    /// </summary>

    static void restartGame()
    {
        repopulateStacks();
        if (currentPlayer == player1) currentPlayer = player2;
        else currentPlayer = player1;
        playerwon = checkIfEmpty();
        printStacks(stack);
        gameLoop();
    }
    /// <summary>
    /// Spelloopen som körs medans spelet pågår
    /// </summary>

    static void gameLoop()
    {
        if (gameMode == 1)
        {
            while (!playerwon)
            {
                Console.WriteLine($"{currentPlayer}'s turn");
                playerMove();
                if (playerwon)
                {
                    updateResult();
                    Console.WriteLine(playerWin());
                    break;
                }
                currentPlayer = checkCurrentPlayer(player1, player2) == player1 ? player2 : player1;
            }
            if (playerwon)
            {
                Console.WriteLine("Would you like to play again? Type 'yes' to contine, type 'no' to end game");
                if (getInput().Equals("yes"))
                {
                    restartGame();
                }
                else return;
            }
        }
        if (gameMode == 2)
        {
            while (!playerwon)
            {
                ComputerMove();
                if (playerwon)
                {
                    updateResult();
                    Console.WriteLine(playerWin());
                    break;
                }
                currentPlayer = checkCurrentPlayer(player1, player2) == player1 ? player2 : player1;
            }
            if (playerwon)
            {
                Console.WriteLine("Would you like to play again? Type 'yes' to contine, type 'no' to end game");
                if (getInput().Equals("yes"))
                {
                    restartGame();
                }
                else
                {
                    Console.WriteLine("Game is now closing");
                    Thread.Sleep(5000);
                    return;
                }
            }

        }
        /// <summary>
        /// alla metoder för att starta spelet
        /// </summary>

        static void game()
        {
            welcomeMessage();
            checkGameMode();
            repopulateStacks();
            printStacks(stack);
            gameLoop();
        }
        /// <summary>
        /// Fixar färgen för bakgrund och text
        /// </summary>
        static void setColor()
        {
            Console.BackgroundColor = ConsoleColor.DarkCyan;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Clear();

        }
        /// <summary>
        /// Main metod som anropar spelet och färg
        /// </summary>

        static void Main(string[] Args)
        {
            setColor();
            game();



        }

    }
}
/// Johan Syrja, Elias Fredin, 3/11 2025, Visual studio code verision: 1.105.1