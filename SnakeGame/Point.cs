using System;
using System.Collections.Generic;
using System.Threading;

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
}
