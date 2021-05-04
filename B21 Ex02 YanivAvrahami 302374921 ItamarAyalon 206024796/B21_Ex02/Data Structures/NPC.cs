using System;

namespace B21_Ex02
{
    public class NPC : BasePlayer
    {
        private readonly Board m_Board;

        public NPC(eSymbol i_Symbol, string i_Name, Board i_Board)
            : base(i_Symbol, i_Name)
        {
            m_Board = i_Board;
        }

        public Position RandomNextMove()
        {
            Random rnd = new Random();
            int row;
            int col;

            do
            {
                row = rnd.Next(m_Board.Height);
                col = rnd.Next(m_Board.Width);
            }
            while (m_Board.IsOccupied(row, col));

            return new Position(row, col);
        }
    }
}
