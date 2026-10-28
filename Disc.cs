using System;

namespace ConnectFour
{
    public abstract class Disc
    {
        public string Symbol { get; }

        public Disc(string symbol)
        {
            Symbol = symbol;
        }

        public abstract int NextDropRow(Board board, int col);

        public virtual bool DropDisc(Board board, int col)
        {
            int row = NextDropRow(board, col);

            if (row >= 0)
            {
                board.PlaceDisc(row, col, Symbol[0]);
                return true;
            }

            return false;
        }
    }
}