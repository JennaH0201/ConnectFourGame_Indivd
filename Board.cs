using System;

namespace ConnectFour
{

    //6*7 grid and customized grid
    public class Board
    {
        public int Rows { get; }
        public int Cols { get; }
        private char[,] grid;

        public int WinningDiscs => 4;

        public Board(int rows, int cols)
        {
            Rows= rows;
            Cols = cols;
            grid = new char[Rows, Cols];

            for (int r = 0; r< Rows; r++)
            {
                for (int c = 0; c < Cols; c++)
                {
                    grid[r, c] = ' ';
                }
            }
        }

        public bool IsCellEmpty(int row, int col)
        {
            return grid[row, col] == ' ';
        }

        public void PlaceDisc(int row, int col, char symbol)
        {
            grid[row, col] = symbol;
        }

        public int GetDropRow(int col)
        {
            if (col < 0 || col >= Cols)
            {
                return -1;
            }
            for (int r = 0; r< Rows; r++)
            {
                if(grid[r, col] == ' ')
                {
                    return r;
                }
            }
            return -1;
        }

        public void Print()
        {
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("Move your disc, Player 1 (@) and Player 2 (#) ");
            Console.WriteLine("You can enter o4, b1");
            Console.WriteLine("ex) o3 is ordinary disc at column 3");
            Console.WriteLine("You can go to the main page, press c");
            Console.WriteLine();
            Console.ResetColor();

            // rows=0 is bottom
            for (int r = Rows - 1; r >= 0; r--)
            {
                Console.Write((r + 1).ToString().PadLeft(2));
                Console.Write(" ");

                for (int c = 0; c < Cols; c++)
                {
                    Console.Write($"| {grid[r, c]} ");
                }
                Console.WriteLine("|");

            }
            //column numbers
            Console.Write("   ");
            for (int c = 0; c < Cols; c++)
            {
                Console.Write($"  {c + 1} ");
            }
        }

        public bool CheckForConnectFour(char symbol)
        {
            if(symbol == ' ')
            {
                return false;
            }

            for(int r = 0; r<Rows; r++)
            {
                for (int c =0; c < Cols; c++)
                {
                    if (grid[r,c] != symbol)
                    {
                        continue;
                    }
                    if(CheckDirection(r, c, symbol, 0, 1) ||    //horizontal
                    CheckDirection(r, c, symbol, 1, 0) ||       //vertical
                    CheckDirection(r, c, symbol, 1, 1) ||       // diagonal \
                    CheckDirection(r, c, symbol, 1, -1)         // diagonal /
                    )

                    {
                        return true;
                    }
                }
            }
            return false;
        }
        
        public bool CheckDirection(int row, int col, char symbol, int rowDelta, int colDelta)
        {
            int count = 0;

            for (int i = 0; i < WinningDiscs; i++)
            {
                int newRow = row + i*rowDelta;
                int newCol = col + i*colDelta;

                if(newRow < 0 || newRow >= Rows || newCol < 0 || newCol >= Cols)
                {
                    return false;
                }

                if(grid[newRow, newCol] != symbol)
                {
                    return false;
                }

                count++;
            }
            return count == WinningDiscs;
        }
        
        public void ClearColumn(int col)
        {
            for (int r = 0; r < Rows; r++)
            {
                grid[r, col] = ' ';
            }
        }

        public bool IsFull()
        {
            for (int c =0; c < Cols; c++)
            {
                if(grid[Rows -1, c] == ' ')
                {
                    return false;
                }
            }

            //no more available play
            return true;
        }

        // This is for load the saved game
        public void ImportBoard(string[] loadedGrid)
        {
            for(int r = 0; r < Rows; r++)
            {
                for (int c =0; c < Cols; c++)
                {
                    grid[r, c] = loadedGrid[r][c];
                }
            }
        }

        //This used when the game is saved
        public string[] ExportBoard()
        {
            string[] rowsData = new string[Rows];

            for(int r = 0; r < Rows; r++)
            {
                char[] rowChars = new char [Cols];
                for (int c = 0; c < Cols; c++)
                {
                    rowChars[c] = grid[r, c];

                }
                rowsData[r] = new string(rowChars);
            }
            return rowsData;
        }

        public char GetCell(int row, int col)
        {
            return grid[row, col];

        }
        
        public void SetCell(int row, int col, char symbol)
        {
            grid[row, col] = symbol;
        }
    }
}

            