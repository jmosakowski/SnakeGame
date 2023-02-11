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

            // Set console to the desired size and 
            Console.SetWindowSize(width, height);
            Console.CursorVisible = false;

            // Create the snake and fruit, at some them at some starting positions
            Snake harry = new Snake(snakeSymbol, width / 2, height / 2, snakeLength);
            Point cherry = new Point(fruitSymbol, width / 4, height / 4);

            // Game loop
            while (true)
            {
                if (Console.KeyAvailable)
                {
                    ConsoleKey consoleKey = Console.ReadKey(true).Key;  // true so the pressed key is not displayed
                    harry.ChangeVelocity(consoleKey);
                }

                Thread.Sleep(1000 / gameSpeed);
                harry.MoveSnake(cherry);
            }
        }
    }

    /**************************************************************/

    class Point
    {
        char Symbol { get; }
        public int X { get; private set; }
        public int Y { get; private set; }

        // Constructor with three arguments
        public Point(char s, int a, int b)
        {
            Symbol = s;
            X = a;
            Y = b;
            Draw();
        } 

        public void Draw()
        {
            Console.SetCursorPosition(X, Y);
            Console.Write(Symbol);
        }

        public void SetRandomPositionAndDraw()
        {
            Random rnd = new Random();
            X = rnd.Next(0, Console.WindowWidth);
            Y = rnd.Next(0, Console.WindowHeight);
            Draw();
        }
    }

    /**************************************************************/

    class Snake
    {
        LinkedList<Point> body = new LinkedList<Point>();   // snake body
        char bodySymbol;
        int vX = 1, vY = 0;                                 // snake velocity, initially set to the right

        // Constructor which puts snake's head at the starting position and the body to the left of the head
        public Snake(char snakeBody, int startX, int startY, int snakeLength)
        {
            bodySymbol = snakeBody;
            for (int i = 0; i < snakeLength; i++)
            {
                Point p = new Point(bodySymbol, startX, startY);
                body.AddLast(p);
                startX--;
            }
        }

        public void ChangeVelocity(ConsoleKey key)
        {
            switch (key)
            {
                case ConsoleKey.UpArrow:
                    if (vY != 1)
                        (vX, vY) = (0, -1);
                    break;
                case ConsoleKey.DownArrow:
                    if (vY != -1)
                        (vX, vY) = (0, 1);
                    break;
                case ConsoleKey.LeftArrow:
                    if (vX != 1)
                        (vX, vY) = (-1, 0);
                    break;
                case ConsoleKey.RightArrow:
                    if (vX != -1)
                        (vX, vY) = (1, 0);
                    break;
            }
        }

        public bool DetectCollision(int newX, int newY)
        {
            // Check if the new head position collides with any of the snake elements
            foreach (Point p in body)
                if (p.X == newX && p.Y == newY)
                    return true;    // return true if collision found

            return false;           // return false if no collisions
        }

        public void MoveSnake(Point fruit)
        {
            // Move the head to new position
            Point head = body.First.Value;
            int newX = (head.X + vX + Console.WindowWidth) % Console.WindowWidth;
            int newY = (head.Y + vY + Console.WindowHeight) % Console.WindowHeight;
            if (DetectCollision(newX, newY))
                return;             // game over
            Point newHead = new Point(bodySymbol, newX, newY);
            body.AddFirst(newHead);

            // If snake ate the fruit, create new fruit
            if (newX == fruit.X && newY == fruit.Y)
                fruit.SetRandomPositionAndDraw();
            // If snake didn't eat, remove snake's last element
            else
            {
                Point last = body.Last.Value;
                Console.SetCursorPosition(last.X, last.Y);
                Console.Write(' '); // draw empty space where the last point was
                body.RemoveLast();
            }
        }
    }
}
