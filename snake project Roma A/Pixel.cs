﻿using System;


namespace snake_project_Roma_A
{
    public readonly struct Pixel
    {
        private const char PixelChar = '█';

        //█
        public Pixel(int x,int y,ConsoleColor color,int pixelSize = 1)
        {
            X = x;
            Y = y;
            Color = color;
            PixelSize = pixelSize;
        }
        public int X { get; }
        public int Y { get; }
        public ConsoleColor Color { get; }
        public int PixelSize { get; }


        public void Draw()
        {
            Console.ForegroundColor = Color;
            for(int x = 0; x < PixelSize; x++)
            {
                for (int y = 0; y < PixelSize; y++)
                {
                    Console.SetCursorPosition(X*PixelSize+x, Y*PixelSize+y);
                    Console.Write(PixelChar);
                }
            }

        }

        public void Clear()
        {
            for (int x = 0; x < PixelSize; x++)
            {
                for (int y = 0; y < PixelSize; y++)
                {
                    Console.SetCursorPosition(X * PixelSize + x, Y * PixelSize + y);
                    Console.Write(' ');
                }
            }
        }
    }
}
