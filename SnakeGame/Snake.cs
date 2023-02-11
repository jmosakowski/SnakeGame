using System;
using System.Collections.Generic;
using System.Threading;

namespace SnakeGame
{
    class Snake
    {
        LinkedList<Point> body = new LinkedList<Point>();
        char bodySymbol;
        int vX = 1, vY = 0;         // snake velocity, initially set towards right

        // Constructor which puts snake head at the starting position and body to the left of head
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
                {
                    Console.SetCursorPosition(newX, newY);
                    Console.Write('x');
                    return true;    // return true if collision found
                }

            return false;           // return false if no collisions
        }

        public bool MoveSnake(Point fruit)
        {
            // Move the head to new position
            Point head = body.First.Value;
            int newX = (head.X + vX + Console.WindowWidth) % Console.WindowWidth;
            int newY = (head.Y + vY + Console.WindowHeight) % Console.WindowHeight;
            if (DetectCollision(newX, newY))
                return false;       // game over
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

            return true;
        }
    }
}
