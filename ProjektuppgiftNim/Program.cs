using System.Configuration.Assemblies;
using System.Runtime.CompilerServices;

class Nim
{
    static int sticksInStack = 5;
    static int[] stack = new int[3];
    static string[,] stacks = new string[3, 5];
    static string player1 = "";
    static string player2 = "";
    static string computerPlayer = "computer";
    static string currentPlayer = "";
    static bool playerwon = false;
    static int gameMode = 0;

    static bool checkIfEmpty()
    {
        return stack[0] == 0 && stack[1] == 0 && stack[2] == 0;
    }
    static string getInput()
    {
        string? input = Console.ReadLine();
        return input ?? "";
    }
    static void welcomeMessage()
    {
        Console.WriteLine("Welcome to the program!, type rules for more info otherwise type 1");
        if (getInput() == "rules")
        {
            Console.WriteLine("There are three piles with five sticks each. The goal is to take the last stick from the last pile. Each player may remove as many sticks as they want from a specific pile.");
        }
    }
    static string getName()
    {
        Console.WriteLine("Type your name");
        return getInput();
    }
    static void repopulateStacks()
    {
        stack[0] = sticksInStack;
        stack[1] = sticksInStack;
        stack[2] = sticksInStack;
    }

    static void removeSticks(int stackNumber, int amount)
    {
        if (stack[stackNumber - 1] - amount < 0)
        {
            Console.WriteLine("Not enough sticks in stack");
        }
        else
        {
            stack[stackNumber - 1] -= amount;
        }
    }
    static string checkCurrentPlayer(string player1, string player2)
    {
        if (currentPlayer == player1) return player1;
        else return player2;
    }
    static void playerMove()
    {
        Console.WriteLine("Chose which stack to remove from:");
        int whichStack = Int32.Parse(getInput());
        Console.WriteLine($"The stack has {amountInStack(whichStack)} amount of sticks");
        Console.WriteLine("How many sticks do you want to remove");
        int amountOfSticks = Int32.TryParse(getInput(), out int result) ? result : 0;
        if (stack[whichStack - 1] >= amountOfSticks)
        {
            removeSticks(whichStack, amountOfSticks);
            printStacks(stack);
        }
        else
        {
            Console.WriteLine("Please make a valid move");
            amountOfSticks = Int32.TryParse(getInput(), out int res) ? res : 0;
        }
        playerwon = checkIfEmpty();
        /*
        kolla om lagligt drag 
        om lagligt drag, ta bort pinnar
        checkIfEmpty(); kolla om spelaren vann
        if inte vunnit byt spelare
        */
    }
    static string playerWin()
    {
        return "Player wins!";
    }

    static string amountInStack(int pile)
    {
        return (stack[pile]).ToString();
    }

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
    static void checkGameMode()
    {
        Console.WriteLine("Choose game mode: 1. Player vs Player 2. Player vs Computer");
        int mode = int.TryParse(getInput(), out int res) ? res : 0;
        gameMode = mode;
    }
    static void ComputerMove()
    {
        Random random = new Random();
        int whichstack = random.Next(1, 4);
        int amountToRemove = random.Next(1, stack[whichstack - 1] + 1);
        removeSticks(whichstack, amountToRemove);
    }
    static void restartGame()
    {
        repopulateStacks();
        playerwon = checkIfEmpty();
    }

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
                    Console.WriteLine(playerWin());
                    break;
                }
                currentPlayer = checkCurrentPlayer(player1, player2) == player1 ? player2 : player1;
            }
        }
        if (gameMode == 2)
        {
            while (!playerwon)
            {

            }
        }

    }

    static void game()
    {
        welcomeMessage();
        player1 = getName();
        player2 = getName();
        currentPlayer = player1;
        repopulateStacks();
        printStacks(stack);
        gameLoop();
    }

    static void Main(string[] Args){
        welcomeMessage();
        game();
       


    }

}
