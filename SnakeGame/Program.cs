using System;
using System.Collections.Generic;
using System.Threading;

namespace SnakeGame
{
    class Program
    {
        static void Main(string[] args)
        {
            int width = 40;
            int height = 20;
            Console.CursorVisible = false;
            Console.SetWindowSize(width, height);
            Snake sss = new Snake(3, width / 2, height / 2);

            Random rnd = new Random();
            Point fruit = new Point() { x = rnd.Next(0, width), y = rnd.Next(0, height), symbol = '@' };
            fruit.Draw();

            while (true)
            {
                if (Console.KeyAvailable)
                {
                    ConsoleKey consoleKey = Console.ReadKey(true).Key;
                    sss.ChangeSpeed(consoleKey);
                }

                Thread.Sleep(200);
                sss.Move();
            }
        }
    }

    /**********************************************/

    class Point
    {
        public int x;
        public int y;
        public char symbol;

        public void Draw()
        {
            Console.SetCursorPosition(x, y);
            Console.Write(symbol);
        }
    }

    /**********************************************/

    class Snake
    {
        LinkedList<Point> body = new LinkedList<Point>();
        char bodyPart = '*';
        public bool eaten = false;

        // Velocity, initially set to (1, 0)
        public int speedX = 0;
        public int speedY = 1;

        public Snake(int length, int startX, int startY)
        {
            for (int i = 0; i < length; i++)
            {
                body.AddLast(new Point() { x = startX, y = startY, symbol = bodyPart });
                startX--;
            }

            foreach (Point p in body)
                p.Draw();
        }

        public void ChangeSpeed(ConsoleKey key)
        {
            switch (key)
            {
                case ConsoleKey.UpArrow:
                    if (speedY != 1)
                    {
                        speedX = 0;
                        speedY = -1;
                    }
                    break;
                case ConsoleKey.DownArrow:
                    if (speedY != -1)
                    {
                        speedX = 0;
                        speedY = 1;
                    }
                    break;
                case ConsoleKey.RightArrow:
                    if (speedX != -1)
                    {
                        speedX = 1;
                        speedY = 0;
                    }
                    break;
                case ConsoleKey.LeftArrow:
                    if (speedX != 1)
                    {
                        speedX = -1;
                        speedY = 0;
                    }
                    break;
            }
        }

        public void Move()
        {
            // If snake didn't eat, remove the last element
            if (!eaten)
            {
                Point last = body.Last.Value;
                // last.Draw();
                Console.SetCursorPosition(last.x, last.y);
                Console.Write(' ');
                body.RemoveLast();
            }

            // Move the head to new position
            Point first = body.First.Value;
            int newX = (first.x + speedX + Console.WindowWidth) % Console.WindowWidth;
            int newY = (first.y + speedY + Console.WindowHeight) % Console.WindowHeight;
            body.AddFirst(new Point() { x = newX, y = newY, symbol = bodyPart });
            body.First.Value.Draw();
        }
    }
}
