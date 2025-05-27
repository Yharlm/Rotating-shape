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
            point tempA = new point(shape.a.x, shape.a.y);
            point tempB = new point(shape.b.x, shape.b.y);
            point tempC = new point(shape.c.x, shape.c.y);
            point tempD = new point(shape.d.x, shape.d.y);

            tempA.x = shape.a.x * cos - shape.a.y * sin;
            tempA.y = shape.a.x * sin + shape.a.y * cos;
            tempB.x = shape.b.x * cos - shape.b.y * sin;
            tempB.y = shape.b.x * sin + shape.b.y * cos;
            tempC.x = shape.c.x * cos - shape.c.y * sin;
            tempC.y = shape.c.x * sin + shape.c.y * cos;
            tempD.x = shape.d.x * cos - shape.d.y * sin;
            tempD.y = shape.d.x * sin + shape.d.y * cos;

            // Update the shape points
            shape.a = tempA;
            shape.b = tempB;
            shape.c = tempC;
                shape.d = tempD;

            

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
                Console.Clear();
                a += 1;
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
