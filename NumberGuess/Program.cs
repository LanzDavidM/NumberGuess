const int MinNumber = 1;

static (int MaxNumber, int Chances) GetSettings(Difficulty difficulty) => difficulty switch
{
    Difficulty.Easy => (50, 10),
    Difficulty.Medium => (100, 7),
    Difficulty.Hard => (200, 8),
    _ => throw new ArgumentOutOfRangeException(nameof(difficulty))
};

do
{
    PlayGame(ChooseDifficulty());
}
while (AskPlayAgain());

Console.WriteLine("Thank you for playing.");

static Difficulty ChooseDifficulty()
{
    //print a menu
    //read a key 1, 2, or 3 (AskPlayAgain) 
    //return difficulty
    return Difficulty.Medium;
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
    var gameMode = GetSettings(gameDifficulty);
    int secretNumber = Random.Shared.Next(MinNumber, gameMode.MaxNumber + 1);
    int guessCount = 0;
    int guess;

    Console.Clear();
    Console.WriteLine("Welcome to Guess the Number\n");
    Console.WriteLine($"I have picked a number between {MinNumber} and {gameMode.MaxNumber}. {secretNumber}");
    Console.WriteLine($"You have {gameMode.Chances} chances to guess my number.");

    do
    {
        guess = GetGuess(guessCount, gameDifficulty);
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
    while (guess != secretNumber && guessCount < gameMode.Chances);

    if (guess != secretNumber)
    {
        Console.WriteLine($"\nMy number was {secretNumber}.");
        Console.WriteLine($"You failed to guess my number in {gameMode.Chances} tries.");
        Console.WriteLine("You LOSE.");
        return;
    }

    string tryWord = guessCount == 1 ? "try" : "tries";
    Console.WriteLine($"\nYou guessed my number in {guessCount} {tryWord}.");
    Console.WriteLine("You WIN!");
}

static int GetGuess(int guessCount, Difficulty gameDifficulty)
{
    var gameMode = GetSettings(gameDifficulty);

    while (true)
    {
        Console.Write($"\nPlease enter your guess {guessCount + 1}/{gameMode.Chances}: ");

        if (int.TryParse(Console.ReadLine(), out int guess)
            && guess >= MinNumber
            && guess <= gameMode.MaxNumber)
        {
            return guess;
        }

        Console.WriteLine($"That was an invalid guess. Enter a whole number from {MinNumber} to {gameMode.MaxNumber}.");
    }
}

enum Difficulty
{
    Easy,
    Medium,
    Hard
}
