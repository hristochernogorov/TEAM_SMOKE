//The first team project.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.IO;
using System.Media;


namespace PingPong
{
    class PingPong
    {

        private static Dictionary<string, int> database; // Todor Dimitrov
        private static string currentUsername; // Todor Dimitrov

        static int width = 70;
        static int height = 45;
        static ConsoleColor ColorOfPaddle = ConsoleColor.White;
        static char symbolPaddle = '_';
        static char symbolPaddle2 = '*';
        static int PaddleLength = 20;
        static int PaddlePositionX = width / 2 - PaddleLength / 2;
        static int PaddlePositionY = height - 4;
        static int BallPosX = width / 2 - 5;
        static int BallPosY = height / 2 + 5;
        static bool BallDirectionUp = true;
        static bool BallDirectionRight = true;

        private static void MoveBall()   // Nikk-Dzhurov
        {
            if (BallPosX == Console.WindowWidth - 4)
            {
                BallDirectionRight = false;

            }
            if (BallPosX == 2)
            {
                BallDirectionRight = true;


            }
            if (BallPosY == 1)
            {
                BallDirectionUp = false;


            }
            if (BallPosY == Console.WindowHeight - 3)
            {
                BallDirectionUp = true;


            }

            if (BallDirectionUp)
            {
                BallPosY--;


            }
            else
            {
                BallPosY++;
                SoundOfJumpBall(); // Chernogorov
            }

            if (BallDirectionRight)
            {
                BallPosX++;


            }
            else
            {
                BallPosX--;


            }
        }
        static void PrintAtPosition(int x, int y, char symbol, ConsoleColor color)  // Nikk-Dzhurov
        {
            Console.SetCursorPosition(x, y);
            Console.ForegroundColor = color;
            Console.Write(symbol);
        }
        static void Logo(int posX, int posY)   // Nikk-Dzhurov
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.CursorVisible = false;
            char[,] matrix ={   {' ', ' ', ' ', '§', ' ', ' ', ' ', ' '},                        
                                {' ', ' ', '§', ' ', '§', ' ', ' ', ' '},
                                {' ', '§', ' ', ' ', ' ', '§', ' ', ' '},
                                {' ', ' ', '§', ' ', ' ', '§', '§', ' '},
                                {' ', ' ', ' ', '§', ' ', ' ', '§', '§'},
                                {' ', ' ', '§', '§', ' ', ' ', ' ', '§'},
                                {' ', ' ', ' ', '§', ' ', ' ', '§', ' '},
                                {' ', ' ', ' ', ' ', '§', ' ', '§', ' '},
                                {' ', ' ', ' ', ' ', ' ', '§', ' ', ' '},
                                {' ', ' ', ' ', ' ', ' ', ' ', ' ', ' '},
                                {' ', ' ', ' ', ' ', ' ', '*', ' ', ' '},
                                {' ', ' ', ' ', ' ', ' ', '*', '*', ' '},
                                {' ', ' ', ' ', '*', '*', 'X', 'X', '*'},
                                {' ', ' ', '*', '$', '$', 'X', 'X', '*'},
                                {' ', '*', '$', 'X', 'X', 'X', '*', ' '},
                                {'*', '$', 'X', '$', '$', 'X', '$', '*'},
                                {'*', '$', 'X', '$', '$', 'X', '$', '*'},
                                {' ', '*', '$', 'X', 'X', '$', '*', ' '},
                                {' ', ' ', '*', '$', '$', '*', ' ', ' '},
                                {'-', '-', '-', '-', '-', '-', '-', '-'},
                                {'\\','S', 'M', 'O', 'K', 'E', '\u00AE', '/'},
                                {' ', '\\', '_', '_', '_', '_', '/', ' '},

                           };
            int tmp = 0;
            while (tmp < 5)
            {
                tmp++;
                Console.SetCursorPosition(posX, posY + 11);
                for (int i = 9; i < matrix.GetLength(0); i++)
                {
                    for (int j = 0; j < matrix.GetLength(1); j++)
                    {
                        if (matrix[i, j] == 'X')
                        {
                            Console.ForegroundColor = ConsoleColor.Yellow;
                        }
                        else if (matrix[i, j] == '-' || matrix[i, j] == '_' || matrix[i, j] == '\\' || matrix[i, j] == '/')
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                        }
                        Console.Write(matrix[i, j]);
                    }
                    Console.SetCursorPosition(posX, posY + i + 2);
                }


                Console.SetCursorPosition(posX, posY + 9);
                for (int i = 8; i >= 0; i--)
                {
                    for (int j = 0; j < matrix.GetLength(1); j++)
                    {

                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write(matrix[i, j]);
                    }
                    Console.SetCursorPosition(posX, posY + i);
                    Thread.Sleep(150);
                }
                Console.Clear();
            }
            Console.SetCursorPosition(0, 0);
        }


        static void ConsoleView() //Niya Keranova
        {

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.BackgroundColor = ConsoleColor.DarkCyan;


            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.DarkGreen;
           

            int height = Console.BufferHeight;
            int width = Console.BufferWidth;
            for (int i = 0; i < width; i++)
            {
                Console.Write("_");
            }
            for (int i = 1; i < height - 1; i++)
            {
                Console.SetCursorPosition(0, i);
                Console.Write("-|");
                Console.SetCursorPosition(width - 2, i);
                Console.Write("|-");
                Console.SetCursorPosition(0, i);
            }

            Console.OutputEncoding = System.Text.Encoding.UTF8;
            char symbol = '\u00AF';
            for (int i = 0; i < width; i++)
            {
                Console.Write(symbol);
            }
        }

        static void Startup() //Niya Keranova
        {
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            string title = "PING-PONG GAME";
            Console.CursorLeft = Console.BufferWidth / 2 - title.Length / 2;
            Console.WriteLine(title);

            string longestString = "Rightarow (->) - Right";
            int cursorLeft = Console.BufferWidth - longestString.Length * 2 - 1;

            Console.CursorTop = 5;
            Console.CursorLeft = cursorLeft;
            string oneRow = "Player's controls:";
            Console.WriteLine(oneRow);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.CursorLeft = cursorLeft;
            string twoRow = "Leftarow (<-) - Left";
            Console.WriteLine(twoRow);
            Console.CursorLeft = cursorLeft;
            string threeRow = "Rightarow (->) - Right";
            Console.WriteLine(threeRow);

            Console.ReadKey();
            Console.Clear();
        }
        static void Greatings() //Niya Keranova
        {
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            string greatings = "TEAM SMOKE: PING-PONG GAME";
            int y = Console.WindowHeight / 2;
            int x = (Console.WindowWidth / 2 + 10) - greatings.Length;
            Console.SetCursorPosition(x, y);

            for (int i = 0; i < greatings.Length; i++)
            {
                Console.Write(greatings[i]);
                Thread.Sleep(100);
            }
            Thread.Sleep(1500);
        }
        static void Loading() //Niya Keranova
        {
            int i = 0;
            while (i != 7)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Loading : (|)");
                Thread.Sleep(50);
                Console.Clear();
                Console.WriteLine("Loading : (/)");
                Thread.Sleep(50);
                Console.Clear();
                Console.WriteLine("Loading : (~)");
                Thread.Sleep(50);
                Console.Clear();
                Console.WriteLine("Loading : (\\)");
                Thread.Sleep(50);
                Console.Clear();
                i++;
            }
        }


        static void PrintPaddlePossition(int x, int y, char symbolPaddle, int length, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.SetCursorPosition(x, y);
            Console.Write(new string(symbolPaddle2, length));
            Console.SetCursorPosition(x, y - 1);
            Console.Write(new string(symbolPaddle, length));
        }

        static void ClearBox()    // Nikk-Dzhurov
        {
            for (int i = 1; i < Console.WindowHeight - 2; i++)
            {
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.SetCursorPosition(2, i);
                Console.Write(new string(' ', Console.WindowWidth - 4));
            }
        }


        static void PlayerMoveLeft(int padPositionX)
        {
            if (PaddlePositionX < Console.WindowWidth - PaddleLength - 2)
            {
                PaddlePositionX++;
            }
            PrintPaddlePossition(PaddlePositionX, PaddlePositionY, symbolPaddle, PaddleLength, ColorOfPaddle);
        }
        static void PlayerMoveRigth(int padPositionX)
        {
            if (PaddlePositionX > 2)
            {
                PaddlePositionX--;
            }
            PrintPaddlePossition(PaddlePositionX, PaddlePositionY, symbolPaddle, PaddleLength, ColorOfPaddle);
        }
        static void MovePaddle()
        {
            if (Console.KeyAvailable)
            {
                ConsoleKeyInfo keyInfo = Console.ReadKey();
                if (keyInfo.Key == ConsoleKey.RightArrow)
                {
                    PlayerMoveLeft(PaddlePositionX);
                }
                if (keyInfo.Key == ConsoleKey.LeftArrow)
                {
                    PlayerMoveRigth(PaddlePositionX);
                }
            }
        }
        private static void RegisterPlayer() // Todor Dimitrov
        {
            Console.Write("Enter your username: ");
            currentUsername = Console.ReadLine();
            if (!database.Keys.Any(username => username == currentUsername))
            {
                database.Add(currentUsername, 0);
                SaveChanges();
            }
            Console.Clear();
        }
        private static void SaveChanges() // Todor Dimitrov
        {
            StreamWriter writer = new StreamWriter("..\\..\\..\\results.txt");
            using (writer)
            {
                foreach (var item in database)
                {
                    string line = String.Format("{0}-{1}", item.Key, item.Value);
                    writer.WriteLine(line);
                }
            }
        }
        private static void LoadResults() // Todor Dimitrov
        {
            StreamReader reader = new StreamReader("..\\..\\..\\results.txt");
            using (reader)
            {
                string line = reader.ReadLine();
                while (line != null)
                {
                    if (!string.IsNullOrEmpty(line))
                    {
                        string[] splitLine = line.Split(new char[] { '-' }, StringSplitOptions.RemoveEmptyEntries);
                        string username = splitLine[0];
                        int score = int.Parse(splitLine[1]);
                        database.Add(username, score);
                    }
                    line = reader.ReadLine();
                }
            }
        }


        private static void SoundOfGameNewLive() // Chernogorov
        {
            SoundPlayer soundLive = new SoundPlayer();
            soundLive.SoundLocation = @"F:\GIT\C#\TEAM_SMOKE\pingpong2\PingPong\newLive.wav";
            soundLive.Play();
        }

        private static void SoundOfGameOver() // Chernogorov
        {
            SoundPlayer soundGameOver = new SoundPlayer();
            soundGameOver.SoundLocation = @"F:\GIT\C#\TEAM_SMOKE\pingpong2\PingPong\gameOver.wav";
            soundGameOver.Play();
            Console.Read();

        }

        private static void SoundOfJumpBall() //Chernogorov 
        {
            SoundPlayer soundJumpBall = new SoundPlayer();
            soundJumpBall.SoundLocation = @"F:\GIT\C#\TEAM_SMOKE\pingpong2\PingPong\jumpBall.wav";
            soundJumpBall.Play();


        }

        static void Main()
        {
            int stateOfColor = 1;
            ConsoleColor[] colours = { ConsoleColor.White, ConsoleColor.Green, ConsoleColor.Red };
            Console.BufferHeight = Console.WindowHeight = 45; //Niya Keranova
            Console.BufferWidth = Console.WindowWidth = 70;    //Niya Keranova
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.Title = "TEAM SMOKE - PING-PONG GAME";   // Nikk-Dzhurov

            //Logo((Console.WindowWidth-8)/2, (Console.WindowHeight-22)/2);    // Nikk-Dzhurov
            //Greatings();    //Niya Keranova
            //Loading();      //Niya Keranova
            //Startup();      //Niya Keranova
            //Loading();      //Niya Keranova


            Logo((Console.WindowWidth-8)/2, (Console.WindowHeight-22)/2);    // Nikk-Dzhurov
            Greatings();    //Niya Keranova
            Loading();      //Niya Keranova
            Startup();      //Niya Keranova
            Loading();      //Niya Keranova
            

            //database = new Dictionary<string, int>(); // Todor Dimitrov
            //LoadResults(); // Todor Dimitrov
            //RegisterPlayer(); // Todor Dimitrov
            Console.CursorVisible = false;

            ConsoleView();  //Niya Keranova
            Console.ForegroundColor = ConsoleColor.Red;                                                  // Nikk-Dzhurov
            Console.BackgroundColor = ConsoleColor.Black;                                                // Nikk-Dzhurov
            PrintPaddlePossition(PaddlePositionX, PaddlePositionY, symbolPaddle, PaddleLength, ColorOfPaddle);    // Nikk-Dzhurov
            PrintAtPosition(BallPosX, BallPosY, '\u00A9', ConsoleColor.Green);                           // Nikk-Dzhurov
            Console.SetCursorPosition((Console.WindowWidth - 23) / 2, Console.WindowHeight / 2);         // Nikk-Dzhurov
            Console.Write("Press any key to start!");                                                    // Nikk-Dzhurov
            Console.ReadKey();


            while (true)
            {

                MoveBall(); // Nicola
                MovePaddle(); // Nicola
                while (Console.KeyAvailable)
                    Console.ReadKey(true);


                ClearBox();                                                                                      // Nikk-Dzhurov


                PrintPaddlePossition(PaddlePositionX, PaddlePositionY, symbolPaddle, PaddleLength, ColorOfPaddle);  // Nikk-Dzhurov
                PrintAtPosition(BallPosX, BallPosY, '\u00A9', ConsoleColor.Green);                               // Nikk-Dzhurov


                if (BallPosY == PaddlePositionY - 1)                                                                  // Nikk-Dzhurov
                {                                                                                                // Nikk-Dzhurov
                    if (BallPosX >= PaddlePositionX && BallPosX <= PaddlePositionX + PaddleLength)               // Nikk-Dzhurov
                    {                                                                                            // Nikk-Dzhurov
                        ColorOfPaddle = colours[stateOfColor++];                                                 // Nikk-Dzhurov
                        if (stateOfColor > 2)                                                                    // Nikk-Dzhurov
                        {                                                                                        // Nikk-Dzhurov
                            stateOfColor = 0;                                                                    // Nikk-Dzhurov
                        }                                                                                        // Nikk-Dzhurov
                        BallDirectionUp = true;                                                                  // Nikk-Dzhurov
                    }                                                                                            // Nikk-Dzhurov
                }                                                                                                // Nikk-Dzhurov


                if (BallPosY > PaddlePositionY)                                                                    // Nikk-Dzhurov
                {                                                                                                // Nikk-Dzhurov
                    Console.SetCursorPosition((Console.WindowWidth - 9) / 2, Console.WindowHeight / 2);          // Nikk-Dzhurov
                    Console.Write("Game Over!");                                                                 // Nikk-Dzhurov 
                    SoundOfGameOver();                                                                           // Chernogorov
                    Console.SetCursorPosition((Console.WindowWidth - 30) / 2, Console.WindowHeight / 2 + 2);     // Nikk-Dzhurov
                    break;                                                                                       // Nikk-Dzhurov
                }

                Thread.Sleep(30);

            }
        }
    }
}