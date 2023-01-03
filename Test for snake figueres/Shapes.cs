using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using TestFigures;

namespace Test_for_snake_figueres
{
    public class Shapes
    {
        
        private static readonly Random Random = new Random();
        public Queue<Pixel> Shape { get; } = new Queue<Pixel>();
        private const ConsoleColor LineColor = ConsoleColor.Blue;
        private const ConsoleColor RectangleColor = ConsoleColor.Green;
        private const ConsoleColor TriangularColor = ConsoleColor.Red;



        public Shapes
            (
                int initialX,
                int initialY
             )
        {
            
            int x = initialX;
            int y = initialY;
            

            DrawShapes(x,y);
        }

        public void DrawShapes(int x,int y)
        {

            int r = Random.Next(1, 4);
            Shape.Clear();
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
            int randomX = Random.Next(x + 2, x + 10);
            for (int w = x; w <= randomX; w++)
            {
               Shape.Enqueue( new Pixel(w, y, color));
                Draw();
            }
            
        }

        public void Rectangle(int x, int y, ConsoleColor color)
        {
            
            int randomY = Random.Next(y + 1, y + 10);
            int randomX = Random.Next(x + 1, x + 10);
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
            int val = 9;
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
    }
}


