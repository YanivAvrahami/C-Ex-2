namespace B21_Ex02
{
    public class Board
    {
        private struct BoardItem
        {
            private eSymbol m_Symbol;

            public eSymbol Symbol
            {
                get { return m_Symbol; }
                set { m_Symbol = value; }
            }

            public BoardItem(eSymbol i_Symbol)
            {
                m_Symbol = i_Symbol;
            }
        }

        private readonly BoardItem?[,] r_Board;
        private readonly int r_Width;
        private readonly int r_Height;

        public int Width
        {
            get { return r_Width; }
        }

        public int Height
        {
            get { return r_Height; }
        }

        public Board(int i_Rows, int i_Columns)
        {
            r_Width = i_Columns;
            r_Height = i_Rows;
            r_Board = new BoardItem?[i_Rows, i_Columns];
        }

        public void SetItem(eSymbol i_Symbol, int i_Row, int i_Col)
        {
            r_Board[i_Row, i_Col] = new BoardItem(i_Symbol);
        }

        public eSymbol GetItem(int i_Row, int i_Col)
        {
            return r_Board[i_Row, i_Col].Value.Symbol;
        }

        public void Delete(int i_Row, int i_Col)
        {
            r_Board[i_Row, i_Col] = null;
        }

        public void Clear()
        {
            for (int i = 0; i < r_Width; i++)
            {
                for (int j = 0; j < r_Height; j++)
                {
                    Delete(i, j);
                }
            }
        }

        public bool IsOccupied(int i_Row, int i_Column)
        {
            return r_Board[i_Row, i_Column] != null;
        }
    }
}
