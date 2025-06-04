using System.ComponentModel;
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

            public shape(int size)
            {
                
                int s = size;
                
                a = new point(-1, -1, s);
                b = new point(0.5, -0.5, s);
                c = new point(1, 1, s);
                d = new point(-0.5, 0.5, s);
            }
        }
        class Polygon
        {
            public List<point> Points= new List<point>();
            public int x, y;
            public int length = 1;

            public Polygon(int size)
            {

                int s = size;

                Points.Add( new point(-1, -1, s));
                Points.Add(new point(1, -1, s));
                Points.Add(new point(1, 1, s));
                Points.Add(new point(-1, 2, s));
            }
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
            float time = 0;
            Thread deltatime = new Thread(() =>
            {
                while (true)
                {
                    Thread.Sleep(1);
                    time += 0.001f; // Increment time by 1 millisecond
                }
            });
            Console.ForegroundColor = ConsoleColor.Blue;

            shape square = new shape(21);
            shape square2 = new shape(17);
            square.x = 70;
            square.y = 60;
            square2.x = 70;
            square2.y = 60;
            shape square3 = new shape(10);
            shape square4 = new shape(13);
            square3.x = 70;
            square3.y = 60;
            square4.x = 70;
            square4.y = 60;
            Console.ReadLine();
            float speed = 0f;
            var input = Console.ReadKey().Key;
            while (true)
            {
                
                if (Console.KeyAvailable)
                {
                    input = Console.ReadKey(true).Key;
                }


                Rotate(speed, square);
                if (input == ConsoleKey.E)
                {
                    speed += 0.1f;
                    
                }
                else if (input == ConsoleKey.Q)
                {
                    speed -= 0.1f;
                }
                else
                {
                    if (speed > 0)
                    {
                        speed -= 0.1f;
                    }
                    else if(speed < 0)
                    {
                        speed += 0.1f;
                    }
                }
                input = ConsoleKey.None;
                Thread.Sleep(10);
                Console.Clear();
                
               
                WriteAt("██", square);
                



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
            drawLine(((int)shape.a.x + shape.x), (int)shape.a.y + shape.y,((int)shape.b.x + shape.x), (int)shape.b.y + shape.y);
            drawLine(((int)shape.b.x + shape.x), (int)shape.b.y + shape.y, ((int)shape.c.x + shape.x), (int)shape.c.y + shape.y);
            drawLine(((int)shape.c.x + shape.x), (int)shape.c.y + shape.y, ((int)shape.d.x + shape.x), (int)shape.d.y + shape.y);
            drawLine(((int)shape.d.x + shape.x), (int)shape.d.y + shape.y, ((int)shape.a.x + shape.x), (int)shape.a.y + shape.y);


        }

        static void drawLine(int x1, int y1, int x2, int y2)
        {
            int dx = x2 - x1;
            int dy = y2 - y1;
            int steps = Math.Max(Math.Abs(dx), Math.Abs(dy));
            float xIncrement = (float)dx / steps;
            float yIncrement = (float)dy / steps;

            float x = x1;
            float y = y1;

            for (int i = 0; i <= steps; i++)
            {
                WriteAt("██", (int)x*2, (int)y);
                x += xIncrement;
                y += yIncrement;
            }
        }
    }
}
