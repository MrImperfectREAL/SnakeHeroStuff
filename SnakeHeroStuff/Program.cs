using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SnakeGame
{
    class Program
    {
        const ConsoleColor HERO_COLOR = ConsoleColor.DarkBlue;
        const ConsoleColor BACKGROUND_COLOR = ConsoleColor.Green;
        public static Coordinate Hero { get; set; } //Will represent our here that's moving around :P/>
        static void Main(string[] args)
        {

            //todo
            //lage meny - må gå helt til bruker avslutter
            //
            Menu();
        }


        static void Menu()
        {
            Console.WriteLine("What do you want to do? Input s for Snake, h for Hero Game, l to leave.");
            string input = Console.ReadLine();
            Console.Clear();


            if (input.Contains("s") == true)
            {
                Snake();
            }

            else if (input.Contains("h") == true)
            {
                Console.WriteLine("Budget Game Console TM");
                Console.WriteLine("------------------------------------------------------------------------------------------");
                HeroGame();
            }

            else if (input.Contains("l") == true)
            {
                Console.WriteLine("Now Closing Console.");
                Thread.Sleep(1000);
                Environment.Exit(0);
            }

            else
            {
                Console.WriteLine("Your input was invalid, please try again.");
                Thread.Sleep(1000);
                Environment.Exit(0);
            }

        }

        static void Exit()
        {
            Environment.Exit(0);
        }
        static void Snake()//hva skjer om du endrer dette metodenavnet?
        {
            //lar deg velge mellom forskjellige speeds
            Console.WriteLine("Select speed Slow / Medium / Fast");
            int delayInMillisecs = 0;
            string Speed = Console.ReadLine();
            if (Speed.ToLower() == "slow")
            {
                delayInMillisecs += 70;
            }
            else if (Speed.ToLower() == "medium")
            {
                delayInMillisecs += 50;
            }
            else if (Speed.ToLower() == "fast")
            {
                delayInMillisecs += 20;
            }
            else
            {
                Console.WriteLine("Your input is invalid, please try again");
                Thread.Sleep(1000);
                Environment.Exit(0);
            }

            // start game
            Console.WriteLine("Press to start game");
            Console.ReadKey();

            // display this char on the console during the game
            char ch = '*';
            bool gameLive = true;
            ConsoleKeyInfo consoleKey; // holds whatever key is pressed

            // location info & display
            int x = 0, y = 5; // y is 5 to allow the top row for directions & space
            int dx = 1, dy = 0;
            int consoleWidthLimit = 79;
            int consoleHeightLimit = 24;

            // clear to color
            Console.BackgroundColor = ConsoleColor.DarkGray;
            Console.Clear();

            // whether to keep trails
            bool trail = true;

            do // until escape
            {
                // print directions at top, then restore position
                // save then restore current color
                ConsoleColor cc = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.SetCursorPosition(0, 0);
                Console.WriteLine("Budget Game Console TM\nArrows move up/down/right/left. 't' trail.  'c' clear  'esc' quit.\n--------------------------------------------------------------------------------------------------------");
                Console.SetCursorPosition(x, y);
                Console.ForegroundColor = cc;

                // see if a key has been pressed
                if (Console.KeyAvailable)
                {
                    // get key and use it to set options
                    consoleKey = Console.ReadKey(true);
                    switch (consoleKey.Key)
                    {
                        case ConsoleKey.T:
                            trail = true;
                            break;
                        case ConsoleKey.C:
                            Console.BackgroundColor = ConsoleColor.DarkGray;
                            trail = true;
                            Console.Clear();
                            break;
                        case ConsoleKey.UpArrow: //UP
                            dx = 0;
                            dy = -1;
                            Console.ForegroundColor = ConsoleColor.Green;
                            break;
                        case ConsoleKey.DownArrow: // DOWN
                            dx = 0;
                            dy = 1;
                            Console.ForegroundColor = ConsoleColor.Green;
                            break;
                        case ConsoleKey.LeftArrow: //LEFT
                            dx = -1;
                            dy = 0;
                            Console.ForegroundColor = ConsoleColor.Green;
                            break;
                        case ConsoleKey.RightArrow: //RIGHT
                            dx = 1;
                            dy = 0;
                            Console.ForegroundColor = ConsoleColor.Green;
                            break;
                        case ConsoleKey.Escape: //END
                            gameLive = false;
                            Console.Clear();
                            Console.WriteLine("The game is now closing.");
                            Thread.Sleep(1000);
                            Environment.Exit(0);
                            break;
                    }
                }

                // find the current position in the console grid & erase the character there if don't want to see the trail
                Console.SetCursorPosition(x, y);
                if (trail == false)
                    Console.Write(' ');

                // calculate the new position
                // note x set to 0 because we use the whole width, but y set to 1 because we use top row for instructions
                x += dx;
                if (x > consoleWidthLimit)
                    x = 0;
                if (x < 0)
                    x = consoleWidthLimit;

                y += dy;
                if (y > consoleHeightLimit)
                    y = 3; // 3 due to top spaces used for directions + console
                if (y < 3)
                    y = consoleHeightLimit;

                // write the character in the new position
                Console.SetCursorPosition(x, y);
                Console.Write(ch);

                // pause to allow eyeballs to keep up
                System.Threading.Thread.Sleep(delayInMillisecs);

            } while (gameLive);

        }// end method snake

        //new section for the game Hero *******************************************************

        static void HeroGame()
        {

            InitGame();
            ConsoleKeyInfo keyInfo;
            while ((keyInfo = Console.ReadKey(true)).Key != ConsoleKey.Escape)
            {
                switch (keyInfo.Key)
                {
                    case ConsoleKey.UpArrow:
                        MoveHero(0, -1);
                        break;

                    case ConsoleKey.RightArrow:
                        MoveHero(1, 0);
                        break;

                    case ConsoleKey.DownArrow:
                        MoveHero(0, 1);
                        break;

                    case ConsoleKey.LeftArrow:
                        MoveHero(-1, 0);
                        break;
                }
            }
        }

        /// <summary>
        /// Paint the new hero
        /// </summary>
        static void MoveHero(int x, int y)
        {
            Coordinate newHero = new Coordinate()
            {
                X = Hero.X + x,
                Y = Hero.Y + y
            };

            if (CanMove(newHero))
            {
                RemoveHero();

                Console.BackgroundColor = HERO_COLOR;
                Console.SetCursorPosition(newHero.X, newHero.Y);
                Console.Write(" ");

                Hero = newHero;
            }
        }

        /// <summary>
        /// Overpaint the old hero
        /// </summary>
        static void RemoveHero()
        {
            Console.BackgroundColor = BACKGROUND_COLOR;
            Console.SetCursorPosition(Hero.X, Hero.Y);
            Console.Write(" ");
        }

        /// <summary>
        /// Make sure that the new coordinate is not placed outside the
        /// console window (since that will cause a runtime crash
        /// </summary>
        static bool CanMove(Coordinate c)
        {
            if (c.X < 0 || c.X >= Console.WindowWidth)
                return false;

            if (c.Y < 0 || c.Y >= Console.WindowHeight)
                return false;

            return true;
        }

        /// <summary>
        /// Paint a background color
        /// </summary>
        /// <remarks>
        /// It is very important that you run the Clear() method after
        /// changing the background color since this causes a repaint of the background
        /// </remarks>
        static void SetBackgroundColor()
        {
            Console.BackgroundColor = BACKGROUND_COLOR;
            Console.Clear(); //Important!
        }

        /// <summary>
        /// Initiates the game by painting the background
        /// and initiating the hero
        /// </summary>
        static void InitGame()
        {
            SetBackgroundColor();

            Hero = new Coordinate()
            {
                X = 0,
                Y = 0
            };

            MoveHero(0, 0);

        }

    }

    /// <summary>
    /// Represents a map coordinate
    /// </summary>
    class Coordinate
    {
        public int X { get; set; } //Left
        public int Y { get; set; } //Top
    }
}



