using System;
using System.Collections.Generic;
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

            // Set console to the desired size and hide the cursor
            Console.SetWindowSize(width, height);
            Console.CursorVisible = false;

            // Create the snake and fruit at some starting positions
            Snake harry = new Snake(snakeSymbol, width / 2, height / 2, snakeLength);
            Point cherry = new Point(fruitSymbol, width / 2, height / 3);

            // Game loop
            bool gameOn = true;
            while (gameOn)
            {
                if (Console.KeyAvailable)
                {
                    ConsoleKey consoleKey = Console.ReadKey(true).Key;  // true so the pressed key is not displayed
                    harry.ChangeVelocity(consoleKey);
                }

                Thread.Sleep(1000 / gameSpeed);
                gameOn = harry.MoveSnake(cherry);
            }

            // Game end - display message below the game board
            Console.SetWindowSize(width, height + 10);
            Console.SetCursorPosition(0, height + 1);
            Console.WriteLine("Game over!");
        }
    }
}
