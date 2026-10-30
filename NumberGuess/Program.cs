const int MinNumber = 1;
const int MaxNumber = 100;
const int Chances = 7;

do
{
    PlayGame();
}
while (AskPlayAgain());

Console.WriteLine("Thank you for playing.");

static bool AskPlayAgain()
{
    Console.Write("\nWould you like to play again (y/n)? ");

    while (true)
    {
        char key = char.ToLower(Console.ReadKey(true).KeyChar);

        if (key == 'y')
        {
            return true;
        }

        if (key == 'n')
        {
            return false;
        }
    }
}

static void PlayGame()
{
    int secretNumber = Random.Shared.Next(MinNumber, MaxNumber + 1);
    int guessCount = 0;
    int guess;

    Console.Clear();
    Console.WriteLine("Welcome to Guess the Number\n");
    Console.WriteLine($"I have picked a number between {MinNumber} and {MaxNumber}.");
    Console.WriteLine($"You have {Chances} chances to guess my number.");

    do
    {
        guess = GetGuess(guessCount);
        guessCount++;

        if (guess < secretNumber)
        {
            Console.WriteLine($"You guessed {guess}. Too LOW!");
        }
        else if (guess > secretNumber)
        {
            Console.WriteLine($"You guessed {guess}. Too HIGH!");
        }
        else
        {
            Console.WriteLine($"You guessed {guess}. Correct!");
        }
    }
    while (guess != secretNumber && guessCount < Chances);

    if (guess != secretNumber)
    {
        Console.WriteLine($"\nMy number was {secretNumber}.");
        Console.WriteLine($"You failed to guess my number in {Chances} tries.");
        Console.WriteLine("You LOSE.");
        return;
    }

    string tryWord = guessCount == 1 ? "try" : "tries";
    Console.WriteLine($"\nYou guessed my number in {guessCount} {tryWord}.");
    Console.WriteLine("You WIN!");
}

static int GetGuess(int guessCount)
{
    while (true)
    {
        Console.Write($"\nPlease enter your guess {guessCount + 1}/{Chances}: ");

        if (int.TryParse(Console.ReadLine(), out int guess)
            && guess >= MinNumber
            && guess <= MaxNumber)
        {
            return guess;
        }

        Console.WriteLine($"That was an invalid guess. Enter a whole number from {MinNumber} to {MaxNumber}.");
    }
}