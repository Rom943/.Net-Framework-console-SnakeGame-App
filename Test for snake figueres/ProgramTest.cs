
using System;
using System.Diagnostics;
using Test_for_snake_figueres;
using static System.Console;

namespace TestFigures
{
    class TestProgram
    {
        private const int MapHeight = 38;
        private const int MapWidth = 62;

        private const int ScreenHeight = MapHeight ;
        private const int ScreenWidth = MapWidth ;
        private const ConsoleColor BorderColor = ConsoleColor.Gray;
        private static readonly Random Random = new Random();







        static void Main()
        {

            SetWindowSize(ScreenWidth, ScreenHeight);
            SetBufferSize(ScreenWidth, ScreenHeight);
            CursorVisible = false;
            int num = 3;

            while (true)
            {
                
                DrawBorder();
                
                Console.ReadKey();
                num++;
            }

        }






        static void GenShapes(int num)
        {
            Pos [,] xyArry = new Pos [3,5];
            int x = 2;
            int y = 2;

            for(int i = 0; i <= 2; i++)
            {
                if (i > 0) 
                { y = y + 12; }
                

                for (int j = 0; j <= 4; j++)
                {
                   
                    if (j > 0) { x = x + 12; }
                    else if(x == 50) { x = 2; }
                    
                    xyArry[i,j] = new Pos(x,y);
                }
            }
            List<Pos> pos = new List<Pos>(num);
            Pos temp;
            for (int i = 0; i< num; i++)
            {
                do
                {
                    temp = xyArry[Random.Next(0, 3), Random.Next(0, 5)];
                }
                while (pos.Contains(temp));
                pos.Add(new Pos(temp.X, temp.Y));
                var shape = new Shapes(pos[i].X, pos[i].Y);
            }
            num++;
        }


        static void DrawBorder()
        {
            for (int i = 0; i < MapWidth; i++)
            {
                new Pixel(i, 0, BorderColor).Draw();
                new Pixel(i, MapHeight - 1, BorderColor).Draw();
            }
            for (int i = 1; i < MapHeight; i++)
            {
                new Pixel(0, i, BorderColor).Draw();
                new Pixel(MapWidth - 1, i, BorderColor).Draw();
            }


        }
    }
}
