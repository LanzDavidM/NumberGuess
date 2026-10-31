const int MinNumber = 1;

do
{
    PlayGame(ChooseDifficulty());
}
while (AskPlayAgain());

Console.WriteLine("\nThank you for playing.");

static (int MaxNumber, int Chances) GetSettings(Difficulty difficulty) => difficulty switch
{
    Difficulty.Easy => (50, 10),
    Difficulty.Medium => (100, 7),
    Difficulty.Hard => (200, 8),
    _ => throw new ArgumentOutOfRangeException(nameof(difficulty))
};

static Difficulty ChooseDifficulty()
{
    Console.Clear();
    Console.WriteLine("Select Difficulty\n");
    Console.WriteLine("1. Easy");
    Console.WriteLine("2. Medium");
    Console.WriteLine("3. Hard");
    Console.Write("Press 1, 2, or 3: ");

    while (true)
    {
        switch (Console.ReadKey(true).KeyChar)
        {
            case '1':
                return Difficulty.Easy;
            case '2':
                return Difficulty.Medium;
            case '3':
                return Difficulty.Hard;
        }
    }
}

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

static void PlayGame(Difficulty gameDifficulty)
{
    var (chances, maxNumber) = GetSettings(gameDifficulty);
    int secretNumber = Random.Shared.Next(MinNumber, maxNumber + 1);
    int guessCount = 0;
    int guess;

    Console.Clear();
    Console.WriteLine("Welcome to Guess the Number\n");
    Console.WriteLine($"I have picked a number between {MinNumber} and {maxNumber}");
    Console.WriteLine($"You have {chances} chances to guess my number.");

    do
    {
        guess = GetGuess(guessCount, chances, maxNumber);
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
    while (guess != secretNumber && guessCount < chances);

    if (guess != secretNumber)
    {
        Console.WriteLine($"\nMy number was {secretNumber}.");
        Console.WriteLine($"You failed to guess my number in {chances} tries.");
        Console.WriteLine("You LOSE.");
        return;
    }

    string tryWord = guessCount == 1 ? "try" : "tries";
    Console.WriteLine($"\nYou guessed my number in {guessCount} {tryWord}.");
    Console.WriteLine("You WIN!");
}

static int GetGuess(int guessCount, int chances, int maxNumber)
{
    while (true)
    {
        Console.Write($"\nPlease enter your guess {guessCount + 1}/{chances}: ");

        if (int.TryParse(Console.ReadLine(), out int guess)
            && guess >= MinNumber
            && guess <= maxNumber)
        {
            return guess;
        }

        Console.WriteLine($"That was an invalid guess. Enter a whole number from {MinNumber} to {maxNumber}.");
    }
}

enum Difficulty
{
    Easy,
    Medium,
    Hard
}
