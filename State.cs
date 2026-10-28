using System;

namespace ConnectFour
{
    //save the game state
    public class State
    {
        public int Rows { get; set; }
        public int Cols { get; set; }
        public int MoveCounter { get; set; }
        public string[] BoardData { get; set; }
        public DateTime SaveTime { get; set; } = DateTime.Now;

        public State()
        {
            BoardData = Array.Empty<string>();
        }


        public State(Board board, int rows, int cols, int moveCounter)
        {
            Rows = rows;
            Cols = cols;
            MoveCounter = moveCounter;
            BoardData = board.ExportBoard();
            SaveTime = DateTime.Now;
        }
    }
}