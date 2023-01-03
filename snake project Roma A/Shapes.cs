using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using snake_project_Roma_A;

namespace snakeProjectRomaA
{
    public class Shapes
    {
        
        private static readonly Random Random = new Random();
        public Queue<Pixel> Shape { get; } = new Queue<Pixel>();
        private const ConsoleColor LineColor = ConsoleColor.Blue;
        private const ConsoleColor RectangleColor = ConsoleColor.Green;
        private const ConsoleColor TriangularColor = ConsoleColor.Red;
         List<Pos> pos = new List<Pos>();





        public Shapes( int num )
        {
            Clear();
            List<Pos> pos = GenShapes(num);
            foreach (Pos p in pos)
            {
                DrawShapes(p.X,p.Y);
            }
            

            

            
        }
        static List<Pos> GenShapes(int num)
        {
            List<Pos> pos = new List<Pos>();
            Pos[,] xyArry = new Pos[3, 5];
            int x = 2;
            int y = 2;
            for (int i = 0; i <= 2; i++)
            {
                if (i > 0)
                { y = y + 12; }
                for (int j = 0; j <= 4; j++)
                {
                    if (j > 0) { x = x + 12; }
                    else if (x == 50) { x = 2; }
                    xyArry[i, j] = new Pos(x, y);
                }
            }
            
            Pos temp;
            for (int i = 0; i < num; i++)
            {
                do
                {
                    temp = xyArry[Random.Next(0, 3), Random.Next(0, 5)];
                }
                while (pos.Contains(temp));
                pos.Add(new Pos(temp.X, temp.Y));
            }
            return pos;
        }

        public void DrawShapes(int x,int y)
        {

            int r = Random.Next(1, 4);
            Clear();
            void Rshape()
            {
                switch (r)
                {
                    case 1: Line(x, y, LineColor); break;
                    case 2: Rectangle(x, y, RectangleColor); break;
                    case 3: Triangular(x, y, TriangularColor); break;
                }
            }
            Rshape();
            


        }


        public void Line(int x, int y, ConsoleColor color)
        {
            int randomX = Random.Next(x + 2, x + 6);
            for (int w = x; w <= randomX; w++)
            {
               Shape.Enqueue( new Pixel(w, y, color));
                Draw();
            }
            
        }

        public void Rectangle(int x, int y, ConsoleColor color)
        {
            
            int randomY = Random.Next(y + 1, y + 6);
            int randomX = Random.Next(x + 1, x + 6);
            for (int h = y; h <= randomY; h++)
            {
                for (int w = x; w <= randomX; w++)
                {
                    Shape.Enqueue(new Pixel(w, h, color));
                    Draw();
                }
            }
            
            
        }

        public void Triangular(int x, int y, ConsoleColor color)
        {
            int val = Random.Next(2,6);
            for (int i = 0; i <= val; i++)
            {
                for(int j = 0; j <= i; j++) //row
                {
                    Shape.Enqueue(new Pixel(x + j, y + i, color));
                    Draw();
                }

            }
        }

        public void Draw()
        {
            foreach (Pixel p in Shape)
            {
                p.Draw();
            }
        }
        public void Clear()
        {
            foreach (Pixel p in Shape)
            {
                p.Clear();
            }
        }
    }
}


