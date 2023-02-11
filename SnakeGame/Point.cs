using System;
using System.Collections.Generic;

namespace SnakeGame
{
    class Point
    {
        char Symbol { get; }
        public int X { get; private set; }
        public int Y { get; private set; }

        // Constructor
        public Point(char s, int a, int b)
        {
            Symbol = s;
            X = a;
            Y = b;
            Draw();
        }

        /**************************************************************/

        public void Draw()
        {
            Console.SetCursorPosition(X, Y);
            Console.Write(Symbol);
        }

        /**************************************************************/

        public void SetRandomPositionAndDraw(LinkedList<Point> pointsToAvoid)
        {
            bool pickedForbiddenPoint;
            Random rnd = new Random();

            // Repeat the loop until an empty point is picked
            do
            {
                pickedForbiddenPoint = false;
                X = rnd.Next(0, Console.WindowWidth);
                Y = rnd.Next(0, Console.WindowHeight);

                // Check if the point is not occupied
                foreach (Point p in pointsToAvoid)
                    if (p.X == X && p.Y == Y)
                    {
                        pickedForbiddenPoint = true;
                        break;
                    }
            }
            while (pickedForbiddenPoint);

            Draw();
        }
    }
}
