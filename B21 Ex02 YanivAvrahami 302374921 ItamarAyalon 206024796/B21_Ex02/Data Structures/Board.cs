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

        private readonly BoardItem?[,] m_Board;
        private readonly int m_Width; // TODO: CHANGE ALL readonly NAMEING SYNTAX
        private readonly int m_Height;


        public int Width
        {
            get { return m_Width; }
        }

        public int Height
        {
            get { return m_Height; }
        }

        public Board(int i_Rows, int i_Columns)
        {
            m_Width = i_Columns;
            m_Height = i_Rows;
            m_Board = new BoardItem?[i_Rows, i_Columns];
        }

        public void SetItem(eSymbol i_Symbol, Position i_Pos)
        {
            SetItem(i_Symbol, i_Pos.Row, i_Pos.Column);
        }

        public void SetItem(eSymbol i_Symbol, int i_Row, int i_Col)
        {
            m_Board[i_Row, i_Col] = new BoardItem(i_Symbol);
        }

        public eSymbol GetItem(Position i_Pos)
        {
            return GetItem(i_Pos.Row, i_Pos.Column);
        }

        public eSymbol GetItem(int i_Row, int i_Col)
        {
            return m_Board[i_Row, i_Col].Value.Symbol;
        }

        public void Delete(Position i_Pos)
        {
            Delete(i_Pos.Row, i_Pos.Column);
        }

        public void Delete(int i_Row, int i_Col)
        {
            m_Board[i_Row, i_Col] = null;
        }


        public void Clear()
        {
            for (int i = 0; i < m_Width; i++)
            {
                for (int j = 0; j < m_Height; j++)
                {
                    Delete(i, j);
                }
            }
        }

        public bool IsOccupied(int i_Row, int i_Column)
        {
            return m_Board[i_Row, i_Column] != null;
        }
    }
}
