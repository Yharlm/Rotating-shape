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
            public Vector3 Position; // Position of the shape in 3D space
            public List<point> points = new List<point>();
            public List<Connection> Edges = new List<Connection>();
            public float Fov = 80f; // Field of view, used to scale the Z coordinate for perspective effect
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
            public void Cube(Vector3 Size)
            {
                addPoint(-Size.X, -Size.Y, -Size.Z);
                addPoint(Size.X, -Size.Y, -Size.Z);
                addPoint(Size.X, Size.Y, -Size.Z);
                addPoint(-Size.X, Size.Y, -Size.Z);
                addPoint(-Size.X, -Size.Y, Size.Z);
                addPoint(Size.X, -Size.Y, Size.Z);
                addPoint(Size.X, Size.Y, Size.Z);
                addPoint(-Size.X, Size.Y, Size.Z);

                addEdge(0, 1);
                addEdge(1, 2);
                addEdge(2, 3);
                addEdge(3, 0);
                addEdge(4, 5);
                addEdge(5, 6);
                addEdge(6, 7);
                addEdge(7, 4);
                addEdge(0, 4);
                addEdge(1, 5);
                addEdge(2, 6);
                addEdge(3, 7);
            }

            public void Sphere(Vector3 Size)
            {
                int sides = 10; // Number of segments in the sphere
                for (int i = 0; i < sides; i++)
                {
                    for (int j = 0; j < sides; j++)
                    {
                        float theta = (float)(i * Math.PI / 10);
                        float phi = (float)(j * 2 * Math.PI / 10);

                        float x = Size.X * (float)(Math.Sin(theta) * Math.Cos(phi));
                        float y = Size.Y * (float)(Math.Sin(theta) * Math.Sin(phi));
                        float z = Size.Z * (float)(Math.Cos(theta));

                        addPoint(x, y, z);
                    }
                }
                for (int i = 0; i < sides; i++)
                {
                    for (int j = 0; j < sides; j++)
                    {
                        int a = i * 10 + j;
                        int b = i * 10 + (j + 1) % 10;
                        int c = ((i + 1) % 10) * 10 + j;
                        int d = ((i + 1) % 10) * 10 + (j + 1) % 10;

                        addEdge(a, b);
                        addEdge(b, d);
                        addEdge(d, c);
                        addEdge(c, a);
                    }
                }

            }
            public void Cube(float lenght)
            {
                addPoint(-lenght, -lenght, -lenght);
                addPoint(lenght, -lenght, -lenght);
                addPoint(lenght, lenght, -lenght);
                addPoint(-lenght, lenght, -lenght);
                addPoint(-lenght, -lenght, lenght);
                addPoint(lenght, -lenght, lenght);
                addPoint(lenght, lenght, lenght);
                addPoint(-lenght, lenght, lenght);

                addEdge(0, 1);
                addEdge(1, 2);
                addEdge(2, 3);
                addEdge(3, 0);
                addEdge(4, 5);
                addEdge(5, 6);
                addEdge(6, 7);
                addEdge(7, 4);
                addEdge(0, 4);
                addEdge(1, 5);
                addEdge(2, 6);
                addEdge(3, 7);
            }

            public void drawLine(int B, int A, Vector2 Offset)
            {
                int a = A;
                int b = B;
                DrawLine((int)(points[a].position.X + Offset.X), (int)(points[a].position.Y + Offset.Y), (int)(points[b].position.X + Offset.X), (int)(points[b].position.Y + Offset.Y));

            }
            public void drawLine(Connection C, Vector2 Offset)
            {
                var a = C.A.position + Position;
                var b = C.B.position + Position;
                float Za = 1 + a.Z / Fov;
                float Zb = 1 + b.Z / Fov;



                DrawLine((int)(a.X * Za + Offset.X), (int)(a.Y * Za + Offset.Y), (int)(b.X * Zb + Offset.X), (int)(b.Y * Zb + Offset.Y));

            }


        }

        /*
         
          
         
        */


        float Pithagoras(float a, float b)
        {
            return (float)Math.Sqrt(a * a + b * b);
        }
        static void Main(string[] args)
        {
            int lenght = 5;




            List<shape> shapes = new List<shape>();

            shape Cube = new shape();
            Cube.Cube(new Vector3(40, 30, 40));
            
            shapes.Add(Cube);
            Cube = new shape();
            Cube.Position = new Vector3(0, 50, 0);
            Cube.Cube(new Vector3(10, 30, 10));
            shapes.Add(Cube);






            //cube.addPoint(lenght/2,,,)

            //Console.ReadLine();
            Vector3 Orientation = new Vector3(0, 0, 0);
            Vector2 Offset = new Vector2(100, 60);
            ConsoleKeyInfo key = new ConsoleKeyInfo();
            int mode = 2;

            foreach (var s in shapes)
            {
                Thread Print = new Thread(() =>
                {
                    while (true)
                    {

                        foreach (Connection C in s.Edges)
                        {
                            s.drawLine(C, Offset);
                        }
                    }
                }
                );
                Print.Start();

            }
            float speed = 0.001f;
            float MoveSpeed = 3f;
            while (true)
            {
                if (Console.KeyAvailable)
                {
                    key = Console.ReadKey(true);
                    foreach (shape cube in shapes)
                    {
                        



                        var ListToDisplay = cube.Edges;
                        //var ListToDisplay = cube.Edges.FindAll(x => float.Max(x.A.position.Z, x.B.position.Z) > 0);
                        cube.Rotate(Orientation.Y, Orientation.X, Orientation.Z);

                        //foreach (Connection C in cube.Edges)
                        //{
                        //    cube.drawLine(C, Offset);
                        //}





                        if (key.Key == ConsoleKey.D1)
                        {
                            mode = 1;
                        }
                        if (key.Key == ConsoleKey.D2)
                        {
                            mode = 2;
                        }
                        else if (key.Key == ConsoleKey.F)
                        {
                            cube.Fov += 1;
                        }
                        else if (key.Key == ConsoleKey.G)
                        {
                            cube.Fov -= 2;
                        }

                        if (mode == 1)
                        {
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
                        if (mode == 2)
                        {

                            if (key.Key == ConsoleKey.D)
                            {
                                cube.Position.X += MoveSpeed;
                            }
                            else if (key.Key == ConsoleKey.A)
                            {
                                cube.Position.X -= MoveSpeed;
                            }
                            else if (key.Key == ConsoleKey.W)
                            {
                                cube.Position.Y -= MoveSpeed;
                            }
                            else if (key.Key == ConsoleKey.S)
                            {
                                cube.Position.Y += MoveSpeed;
                            }
                            else if (key.Key == ConsoleKey.E)
                            {
                                cube.Position.Z += MoveSpeed;
                            }
                            else if (key.Key == ConsoleKey.Q)
                            {
                                cube.Position.Z -= MoveSpeed;
                            }
                        }

                        


                    }

                    Console.Clear();
                }
                else
                {
                    
                }




                Thread.Sleep(10);
                //Console.ReadLine();
                
            }

        }





        static void WriteAt(float x, float y)
        {
            if (x > 0 && y > 0 && x * 2 < Console.BufferWidth && y < Console.BufferHeight)
            {
                Console.SetCursorPosition((int)x * 2, (int)y);
                Console.Write("██");
            }


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
