using System;

namespace ConnectFour
{
    public class BoringDisc : Disc
    {
        public BoringDisc(string symbol) : base(symbol)
        {
        }

        // This is becuase boring disc is fixed at the bottom
        public override int NextDropRow(Board board, int col)
        {
            return 0;
        }

        // boring disc implementation
        public override bool DropDisc(Board board, int col)
        {
            if (col < 0 || col >= board.Cols)
            {
                return false;
            }

            // Clear the discs
            board.ClearColumn(col);

            board.PlaceDisc(0, col, Symbol[0]);

            return true;
        }
    }
}