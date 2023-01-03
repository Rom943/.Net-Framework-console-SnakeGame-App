using snake_project_Roma_A;
using System;
using System.Diagnostics;
using static System.Console;

namespace snakeProjectRomaA
{
    class ProgramCopy
    {
        private const int MapHeight = 40;
        private const int MapWidth = 60;

        private const int ScreenHeight = MapHeight*3;
        private const int ScreenWidth = MapWidth*3;

        private const ConsoleColor BorderColor = ConsoleColor.Gray;
        private const ConsoleColor HeadColor = ConsoleColor.Red;
        private const ConsoleColor BodyColor = ConsoleColor.DarkGreen;
        private const ConsoleColor FoodColor = ConsoleColor.Magenta;
        private static readonly Random Random = new Random();

        private const int FrameMs = 60;

        
        
        
        
        
        static void Main()
        {
            SetWindowSize(ScreenWidth, ScreenHeight);
            SetBufferSize(ScreenWidth, ScreenHeight); 
            CursorVisible = false;

            while (true)
            {
                StartGame();
                Thread.Sleep(1000);
                ReadKey();
            }
        }

        static void StartGame()
        {
            Clear();

            DrawBorder();

            Direction currentMovment = Direction.Right;

            var snake = new Snake(10, 5, HeadColor, BodyColor);

            Pixel food = GenFood(snake);

            food.Draw();

            int score = 3;

            Stopwatch sw = new Stopwatch();


            while (true)
            {
                sw.Restart();
                Direction oldMovment = currentMovment;
                while (sw.ElapsedMilliseconds <= FrameMs)
                {
                    if (currentMovment == oldMovment)
                    {
                        currentMovment = ReadMovement(currentMovment);
                    }
                }
                if (snake.Head.X == food.X && snake.Head.Y == food.Y)
                {
                    //snake.Move(currentMovment, true );
                    break;
                    //food = GenFood(snake);
                    //food.Draw();

                    score++;
                    Task.Run(() => Beep(1200, 200));
                }
                else
                {
                    snake.Move(currentMovment,false);
                }

                if (
                    snake.Head.X == MapWidth - 1
                    || snake.Head.X == 0
                    || snake.Head.Y == MapHeight - 1
                    || snake.Head.Y == 0
                    || snake.Body.Any(b => b.X == snake.Head.X && b.Y == snake.Head.Y))
                    break;
            }
            //snake.Clear();
            SetCursorPosition(ScreenWidth / 3, ScreenHeight / 2);
            Console.WriteLine($"Game Over,score:{score}");
            for (int i = 0; i < 3; i++)
            {
                Task.Run(() => Beep(500, 150));
            }
            


        }

        static void GenShapes(int num)
        {
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
            List<Pos> pos = new List<Pos>(num);
            Pos temp;
            for (int i = 0; i < num; i++)
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



        static Pixel GenFood(Snake snake)
        {
            Pixel food;
            do
            {
                food = new Pixel(Random.Next(1, MapWidth - 2), Random.Next(1, MapHeight - 2), FoodColor);
            } while (snake.Head.X == food.X && snake.Head.Y == food.Y
            ||snake.Body.Any(b=>b.X == food.X && b.Y == food.Y));

            return food;
        }



        static Direction ReadMovement (Direction currentDirection)
        {
            if (!KeyAvailable)
                return currentDirection;

                ConsoleKey key = ReadKey(true).Key;

                currentDirection = key switch
                {
                    ConsoleKey.UpArrow when currentDirection != Direction.Down => Direction.Up,
                    ConsoleKey.DownArrow when currentDirection != Direction.Up => Direction.Down,
                    ConsoleKey.LeftArrow when currentDirection != Direction.Right => Direction.Left,
                    ConsoleKey.RightArrow when currentDirection != Direction.Left => Direction.Right,
                    _ => currentDirection
                };
            return currentDirection;

        }
        
        static void DrawBorder()
        {
            for (int i = 0; i < MapWidth; i++)
            {
                new Pixel(i,0, BorderColor).Draw();
                new Pixel(i,MapHeight-1,BorderColor).Draw();
            }
            for (int i = 1; i < MapHeight ; i++)
            {
                new Pixel(0, i, BorderColor).Draw();
                new Pixel(MapWidth - 1, i, BorderColor).Draw();
            }

            
        }
    }
}