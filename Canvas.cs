using System;
using System.Text;
using FarseerPhysics.Dynamics;

namespace AsciiBirds
{
    class Canvas
    {
        public void DrawSquare(int x, int y, ConsoleColor color, char symbol)
        {
            x = Math.Min( Math.Max(x, 0), Console.BufferWidth-1);
            y = Math.Min( Math.Max(y, 0), Console.BufferHeight-1);

            Console.SetCursorPosition(x, y);
            
            //Console.Write("{0}({1},{2})", '@', x, y);
            Console.ForegroundColor = color;
            Console.Write(symbol);
            Console.ResetColor();
        }

        public void DrawGround(Fixture fixture, int width, int angle, int power)
        {
            Console.BackgroundColor = ConsoleColor.Green;

            Console.SetCursorPosition(0, (int) fixture.Body.Position.Y-1);
            var str = new StringBuilder();
            for(int i=0; i<width-1; i++) str.Append(' ');
            
            Console.WriteLine(str);
            Console.ResetColor();
            
            Console.Write(" Angle=" + angle + ", Power=" + power);
        }

        public void Clear()
        {
            Console.Clear();
        }
    }
}