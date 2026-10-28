using System;
using ConnectFour;

namespace ConnectFour
{
    internal class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("====================================");
                Console.WriteLine("\n Let's play Connect Four!\n");
                Console.WriteLine("Developed by: Jaeeun Heo\n");
                Console.WriteLine("Student ID: n11705191");
                Console.WriteLine("====================================");
                Console.WriteLine("1. New Game");
                Console.WriteLine("2. Load Game");
                Console.WriteLine("3. Help");
                Console.WriteLine("4. Test Mode");
                Console.WriteLine("5. Exit");

                int mode = PromptForInt("Select the option (1-5): ", 1, 5);

                switch (mode)
                {
                    case 1:
                        StartNewGame();
                        break;
                    case 2:
                        LoadGame();
                        break;
                    case 3:
                        ShowHelpPage();
                        break;
                    case 4:
                        RunTestMode();
                        break;
                    case 5:
                        Console.WriteLine("Exiting the game. Goodbye!");
                        return;
                }

                Console.Clear();
            }
        }

        static void StartNewGame()
        {
            Console.Clear();

            Console.WriteLine("Starting a new game!\n");
            Console.WriteLine("1. Basic Grid (6x7)");
            Console.WriteLine("2. Custom Grid");
            Console.Write("Select the option (1-2): ");
            string? gridSize = Console.ReadLine();

            int rows;
            int cols;

            if (gridSize == "2")
            {
                Console.Clear();
                Console.WriteLine("Starting a customized game!");
                rows = PromptForInt("Enter the rows number minimum 6: ", 6, 100);
                cols = rows + 1;
            }
            else
            {
                Console.WriteLine("Starting a basic game!");
                rows = 6;
                cols = 7;
            }

            Console.WriteLine();
            Console.WriteLine("Select the game mode!");
            Console.WriteLine("1. Human vs Human");
            Console.WriteLine("2. Human vs Computer");

            int gameMode = PromptForInt("Select the option (1-2): ", 1, 2);
            bool vsComputer = (gameMode == 2);

            Board board = new Board(rows, cols);
            int moveCounter = 1;

            RunGame(board, rows, cols, moveCounter, vsComputer);
        }

        static void RunGame(Board board, int rows, int cols, int moveCounter, bool vsComputer)
        {
            ComputerPlayer computer = new ComputerPlayer();
            board.Print();

            while (true)
            {
                Console.WriteLine();
                char currentSymbol = (moveCounter % 2 == 1) ? '@' : '#';
                int currentPlayer = (moveCounter % 2 == 1) ? 1 : 2;

                Disc disc;
                int colBased;

                if (vsComputer && currentPlayer == 2)
                {
                    Console.WriteLine("Computer's turn");
                    (disc, colBased) = computer.MakeMove(board);
                    Console.WriteLine($"Computer choose column {colBased + 1}");
                }
                else
                {
                    Console.WriteLine($"Player {currentPlayer}'s turn ({currentSymbol}): ");
                    Console.WriteLine("Enter your move");
                    Console.WriteLine("You can go to the main page, press c");
                    string? input = Console.ReadLine();

                    if (string.IsNullOrWhiteSpace(input))
                    {
                        Console.WriteLine("Invalid input. Please enter the correct move.");
                        continue;
                    }

                    if (input.Equals("c", StringComparison.OrdinalIgnoreCase))
                    {
                        return;
                    }

                    if (input.Length < 2)
                    {
                        Console.WriteLine("Invalid disc type. Try again");
                        continue;
                    }

                    char discType = char.ToLowerInvariant(input[0]);

                    if (discType != 'o' && discType != 'b' && discType != 'm')
                    {
                        Console.WriteLine("Invalid disc type. Try again");
                        continue;
                    }

                    string colStr = input.Substring(1);

                    if (!int.TryParse(colStr, out int colInput) || colInput < 1 || colInput > cols)
                    {
                        Console.WriteLine("Invalid column. Please enter the correct number.");
                        continue;
                    }

                    colBased = colInput - 1;
                    disc = CreateDisc(discType, currentSymbol);
                }

                bool succeed = disc.DropDisc(board, colBased);
                if (!succeed)
                {
                    Console.WriteLine("Column is full. Try again.");
                    continue;
                }

                SaveData save = new SaveData();

                SerializableDataField data = new SerializableDataField
                {
                    Rows = rows,
                    Cols = cols,
                    Grid = board.ExportBoard(),
                    Turn = moveCounter,
                    Player1DiscType = "Ordinary",
                    Player2DiscType = "Ordinary",
                    Player1Symbol = "@",
                    Player2Symbol = "#"
                };

                save.SaveGame(data);

                Console.Clear();
                board.Print();

                if (board.CheckForConnectFour(currentSymbol))
                {
                    Console.WriteLine();
                    if (vsComputer && currentPlayer == 2)
                    {
                        Console.WriteLine("Computer (#) wins!");
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.WriteLine($"Player {currentPlayer} ({currentSymbol}) wins!");
                        Console.ResetColor();
                    }
                    Console.WriteLine("Press Enter to go to the main page");
                    Console.ReadLine();
                    break;
                }

                if (board.IsFull())
                {
                    Console.WriteLine();
                    Console.WriteLine("It's the draw!");
                    Console.WriteLine("Press Enter to go to the main page");
                    Console.ReadLine();
                    break;
                }

                moveCounter++;
            }
        }

        static Disc CreateDisc(char discType, char symbol)
        {
            if (discType == 'b')
            {
                return new BoringDisc(symbol.ToString());
            }
            else if (discType == 'm')
            {
                return new MagneticDisc(symbol.ToString());
            }

            return new OrdinaryDisc(symbol.ToString());
        }

        static int PromptForInt(string message, int min, int max)
        {
            while (true)
            {
                Console.Write(message);
                string? input = Console.ReadLine();

                if (int.TryParse(input, out int number))
                {
                    if (number >= min && number <= max)
                    {
                        return number;
                    }
                    else
                    {
                        Console.WriteLine($"Error: Enter a number between {min} and {max}.");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid number. Please enter the correct number.");
                }
            }
        }

       
       static void LoadGame()
        {
            
            Console.Clear();
        }
    
        

        static void RunTestMode()
        {
            Console.Clear();
            Console.WriteLine("This is the test mode");

            string? input = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(input))
            {
                Console.WriteLine("Invalid input");
                Console.ReadLine();
                return;
            }
        
            Board board = new Board(6, 7);
            TestRunner.RunTest(input, board);
            Console.WriteLine("Press enter to go to the main page");
            Console.ReadLine();
        }




        static void ShowHelpPage()
        {
            Console.Clear();
            Console.WriteLine("This is help page");
            Console.WriteLine("o3 is Ordinary disc in column 3");
            Console.WriteLine("b3 is Boring disc in column 3");
            Console.WriteLine("Press enter to go to the main page");
            Console.ReadLine();
        }
    }
}