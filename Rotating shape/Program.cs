using System.Numerics;

namespace Rotating_shape
{
    internal class Program
    {
        class point
        {
            public Vector3 position;
            public point(float x, float y, float z)
            {
                position = new Vector3(x, y, z);
            }

        }
        static void Draw(Vector3 pos, Vector2 WorldPos, float Fov)
        {
            Vector2 final = new Vector2(
               WorldPos.X + pos.X * pos.Z * 2,
               WorldPos.Y + pos.Y * pos.Z
            );
            Console.SetCursorPosition((int)(final.X), (int)final.Y);
            Console.Write($"[{pos.X},{pos.Y},{pos.Z}]");
        }

        class Connection : shape
        {
            public Connection(point a, point b)
            {
                A = a;
                B = b;
            }
            public point A;
            public point B;
        }
        class shape
        {
            public List<point> points = new List<point>();
            public List<Connection> Edges = new List<Connection>();
            public float Fov = 1.3f; // Field of view, used to scale the Z coordinate for perspective effect
            public void addPoint(float x, float y, float z)
            {
                points.Add(new point(x, y, z));
            }

            public void addEdge(int a, int b)
            {
                Edges.Add(new Connection(points[a], points[b]));
            }

            public void Rotate(float angleX, float angleY, float angleZ)
            {
                Matrix4x4 rotationX = Matrix4x4.CreateRotationX(angleX);
                Matrix4x4 rotationY = Matrix4x4.CreateRotationY(angleY);
                Matrix4x4 rotationZ = Matrix4x4.CreateRotationZ(angleZ);

                foreach (point p in points)
                {
                    Vector3 rotatedPosition = Vector3.Transform(p.position, rotationX);
                    rotatedPosition = Vector3.Transform(rotatedPosition, rotationY);
                    rotatedPosition = Vector3.Transform(rotatedPosition, rotationZ);
                    p.position = rotatedPosition;
                }
            }

            public void drawLine(int B, int A, Vector2 Offset)
            {
                int a = A;
                int b = B;
                DrawLine((int)(points[a].position.X + Offset.X), (int)(points[a].position.Y + Offset.Y), (int)(points[b].position.X + Offset.X), (int)(points[b].position.Y + Offset.Y));

            }
            public void drawLine(Connection C, Vector2 Offset)
            {
                var a = C.A.position;
                var b = C.B.position;
                float Za = 1;
                float Zb = 1;


                if (a.Z < 0)
                {
                    Za = Fov;
                }
                if (b.Z < 0)
                {

                    Zb = Fov;
                }
                if (a.Z < 0)
                {
                    Za = Fov;
                }
                if (b.Z < 0)
                {

                    Zb = Fov;
                }

                DrawLine((int)(a.X * Za + Offset.X), (int)(a.Y * Za + Offset.Y), (int)(b.X * Zb + Offset.X), (int)(b.Y * Zb + Offset.Y));

            }


        }

        /*
         
          
         
        */

        static void Main(string[] args)
        {
            int lenght = 20;
            shape cube = new shape();
            cube.addPoint(-lenght, -lenght, -lenght);
            cube.addPoint(lenght, -lenght, -lenght);
            cube.addPoint(lenght, lenght, -lenght);
            cube.addPoint(-lenght, lenght, -lenght);
            cube.addPoint(-lenght, -lenght, lenght);
            cube.addPoint(lenght, -lenght, lenght);
            cube.addPoint(lenght, lenght, lenght);
            cube.addPoint(-lenght, lenght, lenght);


            cube.addEdge(0, 1);
            cube.addEdge(1, 2);
            cube.addEdge(2, 3);
            cube.addEdge(3, 0);
            cube.addEdge(4, 5);
            cube.addEdge(5, 6);
            cube.addEdge(6, 7);
            cube.addEdge(7, 4);
            cube.addEdge(0, 4);
            cube.addEdge(1, 5);
            cube.addEdge(2, 6);
            cube.addEdge(3, 7);

            Vector3 Orientation = new Vector3(0, 0, 0);
            Vector2 Offset = new Vector2(40, 10);
            while (true)
            {
                foreach (point p in cube.points)
                {
                    Draw(p.position, Offset, 1);
                }
                //Console.WriteLine($"Point: {p.position}");
            }
            //foreach (Connection C in cube.Edges.FindAll(x => float.Max(x.A.position.Z, x.B.position.Z) > 0))
            //{
            //    int index = cube.Edges.IndexOf(C);
            //    if (index > 0 && index < cube.Edges.FindAll(x => float.Max(x.A.position.Z, x.B.position.Z) > 0).Count())
            //    {
            //        cube.drawLine(index - 1, index, Offset);
            //    }

            //}
            cube.Rotate(Orientation.Y, Orientation.X, Orientation.Z);

            foreach (Connection C in cube.Edges)
            {
                //if (C.A.position.Z > -lenght && C.B.position.Z > -lenght / 0.905)
                cube.drawLine(C, Offset);

            }
            //cube.drawLine(0, 1, Offset);
            //cube.drawLine(1, 2, Offset);
            //cube.drawLine(2, 3, Offset);
            //cube.drawLine(3, 0, Offset);
            //cube.drawLine(4, 5, Offset);
            //cube.drawLine(5, 6, Offset);
            //cube.drawLine(6, 7, Offset);
            //cube.drawLine(7, 4, Offset);
            //cube.drawLine(0, 4, Offset);
            //cube.drawLine(1, 5, Offset);
            //cube.drawLine(2, 6, Offset);
            //cube.drawLine(3, 7, Offset);
            var ListToDisplay = cube.Edges;
            //var ListToDisplay = cube.Edges.FindAll(x => float.Max(x.A.position.Z, x.B.position.Z) > 0);


            // This line is not necessary, but it can be used to force the evaluation of the points if needed.

            if (Console.KeyAvailable)
            {
                float speed = 0.001f;
                ConsoleKeyInfo key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.D)
                {
                    Orientation.X += speed;
                }
                else if (key.Key == ConsoleKey.A)
                {
                    Orientation.X -= speed;
                }
                else if (key.Key == ConsoleKey.W)
                {
                    Orientation.Y -= speed;
                }
                else if (key.Key == ConsoleKey.S)
                {
                    Orientation.Y += speed;
                }
                else if (key.Key == ConsoleKey.Q)
                {
                    Orientation.Z += speed;
                }
                else if (key.Key == ConsoleKey.E)
                {
                    Orientation.Z -= speed;
                }
                else if (key.Key == ConsoleKey.Escape)
                {
                    Orientation = new Vector3(0, 0, 0);
                }
            }






            Thread.Sleep(10);
            //Console.ReadLine();
            Console.Clear();
        }





        static void WriteAt(float x, float y)
        {
            try
            {
                Console.SetCursorPosition((int)x * 2, (int)y);
                Console.Write("██");
            }
            catch { }

        }


        static void DrawLine(int x1, int y1, int x2, int y2)
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
                WriteAt((int)x, (int)y);
                x += xIncrement;
                y += yIncrement;
            }
        }



    }
}
