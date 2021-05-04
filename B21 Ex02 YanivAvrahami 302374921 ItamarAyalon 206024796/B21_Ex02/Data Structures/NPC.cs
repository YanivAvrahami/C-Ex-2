using System;

namespace B21_Ex02
{
    public class NPC : BasePlayer
    {
        private readonly Board r_Board;

        public NPC(eSymbol i_Symbol, string i_Name, Board i_Board)
            : base(i_Symbol, i_Name)
        {
            r_Board = i_Board;
        }

        public Position RandomNextMove()
        {
            Random rnd = new Random();
            int row;
            int col;

            do
            {
                row = rnd.Next(r_Board.Height);
                col = rnd.Next(r_Board.Width);
            }
            while (r_Board.IsOccupied(row, col));

            return new Position(row, col);
        }
    }
}
