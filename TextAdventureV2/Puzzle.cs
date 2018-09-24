using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextAdventureV2
{
    class Puzzle
    {
        private void PuzzleGame()
        {
            Random random = new Random();
            Console.WriteLine("Seems like the lock needs a number between 1 and 10 to be unlocked. You have 3 attempts.");

            Console.WriteLine("\nGuess a number between 1 and 10.");
            int number = random.Next(1, 11);
            int attempts = 1;
            int guess = 0;

            while (guess != number || attempts == 3)
            {
                Console.Write("\nAttempt number {0}:  ", attempts);

                if (Int32.TryParse(Console.ReadLine(), out guess))
                {
                    Console.WriteLine("\nYou picked " + guess);

                    if (guess == number)
                    {
                        Console.WriteLine("You opened the lock.");
                        break;
                    }
                    else
                    {
                        attempts++;

                        if (guess < number)
                        {
                            Console.WriteLine("Seems like you need a bigger number.");
                        }
                        else
                        {
                            Console.WriteLine("Seems like you need a smaller number.");
                        }
                    }
                }
                else
                {
                    Console.WriteLine("That's not a number.");
                }
            }
        }
    }
}
