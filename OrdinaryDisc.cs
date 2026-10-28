using System;

namespace ConnectFour
{
    // Ordinary disc drops to the lowest empty row in the column
    public class OrdinaryDisc : Disc
    {
        public OrdinaryDisc(string symbol) : base(symbol)
        {
        }

        public override int NextDropRow(Board board, int col)
        {
            for (int r = 0; r < board.Rows; r++)
            {
                if (board.IsCellEmpty(r, col))
                {
                    return r;
                }
            }

            return -1;
        }
    }
}