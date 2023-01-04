using snake_project_Roma_A;
using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Metadata;
using System.Xml.Linq;
using static System.Console;
using static System.Formats.Asn1.AsnWriter;

namespace snakeProjectRomaA
{
    class Program
    {
        private const int MapHeight = 38;
        private const int MapWidth = 62;

        private const int ScreenHeight = MapHeight;
        private const int ScreenWidth = MapWidth;

        private const ConsoleColor BorderColor = ConsoleColor.Gray;
        private const ConsoleColor HeadColor = ConsoleColor.Red;
        private const ConsoleColor BodyColor = ConsoleColor.DarkGreen;
        private static readonly Random Random = new Random();
        private static readonly Stopwatch stopwatch = new Stopwatch();
        public const int FrameMs = 80;
        

        
        
        
        
        
        static void Main()
        {
            SetWindowSize(ScreenWidth, ScreenHeight);
            SetBufferSize(ScreenWidth + 2, ScreenHeight + 5); 
            
           
            while (true)
            {


                Clear();
                SetCursorPosition(10, 10);
                Console.ForegroundColor = HeadColor;
                Console.WriteLine("Welcome to Roma Alexeichick Project");
                for (int i = 0; i < 3; i++)
                {
                    Task.Run(() => Beep(500, 150));
                }
                Thread.Sleep(500);
                Console.ForegroundColor = ConsoleColor.Green;
                SetCursorPosition(17, 12);
                Console.WriteLine("Snake with Shapes");
                SetCursorPosition(15, 14);
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("the rules of the game:");
                SetCursorPosition(1, 17);
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("* you need to keep crawling as much time as you can!");
                Console.WriteLine("* each time you collide with the snakes head:");
                Console.WriteLine("  the game will restart");
                Console.WriteLine("  but each time will be add + 1 shape until 15 shapes");
                Console.WriteLine(" Enter how many shaps you want to start?:(1-14)");
                int score;
                int.TryParse(Console.ReadLine(),out score);
                while(score == 0||score>14)
                {
                    Console.WriteLine("Illegal action,only numbers between 1 to 14");
                    Console.WriteLine("try again");
                    int.TryParse(Console.ReadLine(), out score);
                }
                Console.WriteLine("Press any key to start");
                ReadKey();
                stopwatch.Restart();
                CursorVisible = false;
                StartGame(score);
                Console.WriteLine(stopwatch.Elapsed.TotalSeconds);
                Thread.Sleep(1000);
                ReadKey();
            }
        }

        static void StartGame(int score)
        {
            
            Clear();

            DrawBorder();
            var shape = new Shapes(score);
            Direction currentMovment = Direction.Right;
            var snake = new Snake(13, 13, HeadColor, BodyColor);
            int lagMs = 0;
            Stopwatch sw = new Stopwatch();


            while (true)
            {
                if (score >= 15) break;
                sw.Restart();
                Direction oldMovment = currentMovment;
                while (sw.ElapsedMilliseconds <= FrameMs-lagMs)
                {
                    
                    if (currentMovment == oldMovment)
                    {
                        currentMovment = ReadMovement(currentMovment);
                    }

                }
                sw.Restart();
                if (shape.Shape.Any(b => b.X == snake.Head.X && b.Y == snake.Head.Y) 
                    || snake.Head.X == MapWidth - 1
                    || snake.Head.X == 0
                    || snake.Head.Y == MapHeight - 1
                    || snake.Head.Y == 0
                    || snake.Body.Any(b => b.X == snake.Head.X && b.Y == snake.Head.Y))
                {
                    if (score < 15) { ++score; };
                    Task.Run(() => Beep(1200, 200));
                    Clear();
                    DrawBorder();
                    shape = new Shapes(score);
                    snake = new Snake(13, 13, HeadColor, BodyColor);
                    currentMovment = Direction.Right;

                }
                else
                {
                    snake.Move(currentMovment);
                }

                lagMs = (int)sw.ElapsedMilliseconds;

            }
            stopwatch.Stop();
            Clear();
            SetCursorPosition(ScreenWidth / 3-9, ScreenHeight / 2);
            Console.WriteLine("You have maneged to stay in the game for:" );
            SetCursorPosition(ScreenWidth / 3-8, ScreenHeight / 2+1);
            Console.Write("Elapsed time: {0:0.00} seconds ", stopwatch.Elapsed.TotalSeconds);
 
            
            for (int i = 0; i < 3; i++)
            {
                Task.Run(() => Beep(500, 150));
            }

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