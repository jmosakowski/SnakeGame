using System;
using System.Threading;

namespace SnakeGame
{
    class Program
    {
        static void Main(string[] args)
        {
            // Settings
            char snakeSymbol = '*', fruitSymbol = '@';
            int snakeLength = 3;
            int gameSpeed = 5;
            int width = 40, height = 20;

            Console.SetWindowSize(width, height);   // set console size
            Console.CursorVisible = false;          // hide the cursor

            // Create the snake at some initial position
            int snakePosX = width / 2;
            int snakePosY = height / 2;
            Snake harry = new Snake(snakeSymbol, snakePosX, snakePosY, snakeLength);

            // Create the fruit at some initial position
            int fruitPosX = 3 * width / 4;
            int fruitPosY = height / 2;
            Point cherry = new Point(fruitSymbol, fruitPosX, fruitPosY);

            // Game loop
            bool gameOn = true;
            while (gameOn)
            {
                if (Console.KeyAvailable)
                {
                    ConsoleKey key = Console.ReadKey(true).Key; // true so the key is not printed
                    harry.ChangeVelocity(key);
                }

                Thread.Sleep(1000 / gameSpeed);                 // pause between each move
                gameOn = harry.MoveSnake(cherry);
            }

            // Game end - display message below the game board
            Console.SetWindowSize(width, height + 10);
            Console.SetCursorPosition(0, height + 1);
            Console.WriteLine("Game over! Press any key to quit.");
            Console.ReadKey();
        }
    }
}
