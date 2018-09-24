using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextAdventureV2
{
    class Puzzle
    {
        Random rnd = new Random();
        private int rndNumber;
        int attempts = 1;
        public void RunPuzzle(ref bool gameIsRunning, ref bool gameComplete)
        {

            
            string guess = "0";
            string correctNumber = "461" + rndNumber.ToString();

            while (guess != correctNumber && attempts <= 10)
            {
                Console.WriteLine("\nTries left:  {0}", (11 - attempts));
                Console.WriteLine("ENTER CODE:");
                Console.Write(">");

                guess = Console.ReadLine();

                if (guess == correctNumber)
                {
                    Console.WriteLine("**CORRECT**");
                    gameComplete = true;
                    gameIsRunning = false;
                    return;
                }
                else
                {
                    Console.WriteLine("**INCORRECT**");
                    attempts++;
                }
                if (attempts < 10)
                {
                    Console.WriteLine("Do you want to try again? (Type Y to try again)");
                    Console.Write(">");
                    string answer = Console.ReadLine().ToUpper();
                    if (answer != "Y")
                    {
                        Console.WriteLine("Leaving the keypad.");
                        return;
                    }
                }

            }
            Console.WriteLine("**UNAUTHORIZED PERSONEL DETECTED**");
            Console.WriteLine("You hear doors lock all around the hospital and hear sounds of growling and slowly moving people around you.");
            Console.WriteLine("In your terrified state your heart stops and you die.");
            Console.ReadLine();
            gameIsRunning = false;
            return;
        }

        public void GenerateRandomNumber()
        {
            rndNumber = rnd.Next(0, 10);
        }
    }
}
