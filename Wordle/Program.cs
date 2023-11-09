using System;
using System.IO;

class Program
{
    static void Main()
    {
        Random rnd = new Random();

        string secretWord = null;
        string filePath = "Words.txt";

        try
        {
            string wordsString = File.ReadAllText(filePath);

            // Тут типо между словами пробел и мы создаем массив
            string[] wordsArray = wordsString.Split(' ');

                int wordIndex = rnd.Next(wordsArray.Length);
                secretWord = wordsArray[wordIndex];
        }
        catch (IOException ex) { Console.WriteLine("Words.txt error"); }

        int maxAttempts = 6; // Максимальное количество попыток
        int attempts = 0; // Счетчик попыток

        Console.WriteLine("Добро пожаловать в игру Wordle!");
        Console.WriteLine("Угадайте загаданное слово из 5 букв.");

        while (attempts < maxAttempts)
        {
            Console.Write("Попытка " + (attempts + 1) + ": ");
            string guess = Console.ReadLine().ToLower(); ;

            if (guess.Length != secretWord.Length)
            {
                Console.WriteLine("Слово должно содержать " + secretWord.Length + " букв.");
                continue;
            }

            attempts++;

            if (guess == secretWord)
            {
                Console.WriteLine("Поздравляем! Вы угадали слово: " + secretWord);
                break;
            }
            else
            {
                string feedback = GenerateFeedback(secretWord, guess);
                PrintColoredFeedback(feedback);
            }
        }

        if (attempts >= maxAttempts)
        {
            Console.WriteLine("Игра окончена. Загаданное слово: " + secretWord);
        }
    }

    static void PrintColoredFeedback(string feedback)
    {
        foreach (char c in feedback)
        {
            switch (c)
            {
                case '+':
                    Console.ForegroundColor = ConsoleColor.Green;
                    break;
                case '-':
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    break;
                default:
                    Console.ResetColor();
                    break;
            }

            Console.Write(c);
        }

        Console.ResetColor(); // Вернуть цвет обратно в стандартный
        Console.WriteLine(); // Перейти на новую строку
    }

    static string GenerateFeedback(string secretWord, string guess)
    {
        char[] feedback = new char[secretWord.Length];

        for (int i = 0; i < secretWord.Length; i++)
        {
            if (secretWord[i] == guess[i])
            {
                feedback[i] = '+';
            }
            else if (secretWord.Contains(guess[i].ToString()))
            {
                feedback[i] = '-';
            }
            else
            {
                feedback[i] = '.';
            }
        }

        return new string(feedback);
    }
}
