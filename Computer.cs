using System;

namespace ConnectFour
{
    public class ComputerPlayer
    {
        private Random random = new Random();

        // Computer finds a winning move
        public int FindWinningMove(Board board)
        {
            for (int col = 0; col < board.Cols; col++)
            {
                int row= board.GetDropRow(col);

                if (row == -1) continue;
                board.PlaceDisc(row, col, '#');
                bool isWin = board.CheckForConnectFour('#');
                board.PlaceDisc(row, col, ' ');

                if (isWin)
                {
                    return col;
                }
            }
            return -1;
        }
    

        public int ChooseColumn(Board board)
        {
            int winningCol = FindWinningMove(board);

            if(winningCol != -1)
            {
                return winningCol;
            }

            return random.Next(board.Cols);
        }

        public (Disc, int) MakeMove(Board board)
        {
            int column = ChooseColumn(board);

            if (column == -1)
            {
                throw new InvalidOperationException("Invalid move.");
            }

            Disc disc = new OrdinaryDisc("#");
            return (disc, column);
        }
    }  
}