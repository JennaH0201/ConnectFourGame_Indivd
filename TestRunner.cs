using System;

namespace ConnectFour
{
    public class TestRunner
    {
        public static void RunTest(string input, Board board)
        {
            // Check if the board exists
            if (board == null)
            {
                throw new ArgumentNullException(nameof(board));
            }

            // Check if the input is empty
            if (string.IsNullOrWhiteSpace(input))
            {
                throw new ArgumentException(nameof(input));
            }

            
            string[] moves = input.Split(',', StringSplitOptions.RemoveEmptyEntries);

            bool isPlayer1Turn = true;

            for (int i = 0; i < moves.Length; i++)
            {
                string move = moves[i].Trim();

                
                if (move.Length < 2)
                {
                    Console.WriteLine($"Invalid input. Please enter the correct input.");
                    continue;
                }

                char discType = char.ToLowerInvariant(move[0]);
                string columnPart = move.Substring(1);

                // ordinary and boring discs are available only
                if (discType != 'o' && discType != 'b' && discType != 'm')
                {
                    Console.WriteLine($"Invalid disc type. Please enter the correct move.");
                    continue;
                }

                if (!int.TryParse(columnPart, out int colInput))
                {
                    Console.WriteLine($"Invalid column. Please enter the correct column.");
                    continue;
                }

                int colBased = colInput - 1;
 
                if (colBased < 0 || colBased >= board.Cols)
                {
                    Console.WriteLine($"Invalid column. Please enter the correct column");
                    continue;
                }

                string symbol = isPlayer1Turn ? "@" : "#";
                Disc currentDisc;

             
                if (discType == 'b')
                {
                    currentDisc = new BoringDisc(symbol);
                }
                else if (discType == 'm')
                {
                    currentDisc = new MagneticDisc(symbol);
                }
                else
                {
                    currentDisc = new OrdinaryDisc(symbol);
                }

                bool succeed = currentDisc.DropDisc(board, colBased);

                if (!succeed)
                {
                    Console.WriteLine($"Invalid column. Please enter the correct column");
                    continue;
                }

                Console.Clear();
                board.Print();

                char currentSymbol = isPlayer1Turn ? '@' : '#';

              
                if (board.CheckForConnectFour(currentSymbol))
                {
                    Console.WriteLine($" Wow! Player {(isPlayer1Turn ? 1 : 2)} win!.");
                    break;
                }
                
                if (board.IsFull())
                {
                    Console.WriteLine("The board is full. Test is finished.");
                    break;
                }

                isPlayer1Turn = !isPlayer1Turn;
            }

            Console.WriteLine("Test completed.");
        }
    }
}