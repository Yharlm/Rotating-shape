using System.Runtime.CompilerServices;

namespace Rotating_shape
{
    internal class Program
    {

        class point
        {
            public double x = 0;
            public double y = 0;

            public point(double x, double y)
            {
                this.x = x;
                this.y = y;
            }
            public point(double x, double y,int size)
            {
                this.x = x*size;
                this.y = y*size;
            }
        }

        class shape
        {
            public point a, b, c, d;
            public int x, y;
            public int length = 1;
        }

        static void Rotate(float angle, shape shape)
        {
            float radians = angle * (float)(Math.PI / 180.0);
            //Matrix for rotation
            float cos = (float)Math.Cos(radians);
            float sin = (float)Math.Sin(radians);
            // Rotate each point
            shape.a.x *= cos + sin;

        }
        static void Main(string[] args)
        {
            int a = 0;
            int s = 6;
            shape square = new shape();
            square.a = new point(-1,-1, s);
            square.b = new point(1, -1, s);
            square.c = new point(1, 1, s);
            square.d = new point(-1, 1, s);
            square.x = 10;
            square.y = 10;
            while(true)
            {
                Rotate(a, square);
                WriteAt("#", square);
                Console.ReadLine();
                a += 10;
            }
           

        }

        static void WriteAt(string text, int x, int y)
        {
            Console.SetCursorPosition(x, y);
            Console.Write(text);
        }
        static void WriteAt(string text, shape shape)
        {
            int lenght = shape.length;
            WriteAt(text, 2*((int)shape.a.x + shape.x), (int)shape.a.y+shape.y);
            WriteAt(text, 2*((int)shape.b.x + shape.x), (int)shape.b.y+shape.y);
            
            WriteAt(text, 2*((int)shape.c.x + shape.x), (int)shape.c.y+shape.y);
            WriteAt(text, 2*((int)shape.d.x + shape.x), (int)shape.d.y+shape.y);
        }
    }
}
