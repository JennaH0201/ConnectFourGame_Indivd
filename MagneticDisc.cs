using System;

namespace ConnectFour
{
    public class MagneticDisc : Disc
    {
        public MagneticDisc(string symbol) : base(symbol)
        {}

        // Magnetic disc changes to the ordinary disc
        public override int NextDropRow(Board board, int col)
        {
            return board.GetDropRow(col);
        }

        public override bool DropDisc(Board board, int col)
        {
            if (col < 0 || col >= board.Cols)
            {
                return false;
            }

            int row = board.GetDropRow(col);

            if (row == -1)
            {
                return false;
            }

            char playerSymbol = Symbol[0];

            board.PlaceDisc(row, col, Symbol[0]);
            board.SetCell(row, col, playerSymbol);
            int targetRow = -1;

            for(int r = row -1; r >= 0; r--)
            {
                if (board.GetCell(r, col) == playerSymbol)
                {
                    targetRow = r;
                    break;
                }
            }

            if (targetRow == -1 || targetRow == row -1)
            {
                return true;
            }

            char targetDisc = board.GetCell(targetRow, col);

            for(int r = targetRow; r < row -1; r++)
            {
                board.SetCell(r, col, board.GetCell(r+1, col));
            }

            board.SetCell(row -1, col, targetDisc);
            return true;
        }
    }


}