int sticksInStack = 5;
int[] stack = new int[3];
string[,] stacks = new string[3, 5];
string name1 = "";
string name2 = "";

bool checkIfEmpty()
{
    return stack[0] == 0 && stack[1] == 0 && stack[2] == 0;
}
string getInput()
{
    string? input = Console.ReadLine();
    return input ?? ""; 
}
void welcomeMessage()
{
    Console.WriteLine("Welcome to the program!, type rules for more info");
    if (getInput() == "rules")
    {
        Console.WriteLine("rules");
    }
    else { Console.WriteLine("continue"); }
}
string getName()
{
    Console.WriteLine("Type your name");
    return getInput();
}
void repopulateStacks()
{
    stack[0] = sticksInStack;
    stack[1] = sticksInStack;
    stack[2] = sticksInStack;
}

void removeSticks(int stackNumber, int amount)
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
void checkCurrentPlayer()
{
    // Kolla nuvarande spelare
}
void playerMove()
{   
    /*
    kolla om lagligt drag 
    om lagligt drag, ta bort pinnar
    checkIfEmpty(); kolla om spelaren vann
    if inte vunnit byt spelare
    */
}
string playerWin()
{
    return "Player wins!";
}

void printStacks(int[] stack)
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

repopulateStacks();
printStacks(stack);
Console.WriteLine(checkIfEmpty());
removeSticks(1, 2);
printStacks(stack);
Console.WriteLine(checkIfEmpty());
removeSticks(2, 5);
removeSticks(1, 3);
removeSticks(3, 5);
printStacks(stack);
Console.WriteLine(checkIfEmpty());
