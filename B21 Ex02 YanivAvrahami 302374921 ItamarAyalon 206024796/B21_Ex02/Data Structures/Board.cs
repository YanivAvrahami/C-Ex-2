namespace B21_Ex02
{
    public class Board
    {
        public struct BoardItem
        {
            private bool m_IsOccupied;
            private eSymbol m_Symbol;

            public eSymbol Symbol
            {
                get { return m_Symbol; }
                set { m_Symbol = value; }
            }

            public bool IsOccupied
            {
                get { return m_IsOccupied; }
                set { m_IsOccupied = value; }
            }

            public BoardItem(eSymbol i_Symbol)
            {
                m_IsOccupied = true;
                m_Symbol = i_Symbol;
            }
        }

        private readonly BoardItem[,] m_Board;
        private readonly int m_Width;
        private readonly int m_Height;

        public int Width
        {
            get
            {
                return m_Width;
            }
        }

        public int Height
        {
            get
            {
                return m_Height;
            }
        }

        public Board(int i_Rows, int i_Columns)
        {
            m_Width = i_Columns;
            m_Height = i_Rows;
            m_Board = new BoardItem[i_Rows, i_Columns];
        }

        public void SetItem(eSymbol i_Symbol, Position i_Pos)
        {
            m_Board[i_Pos.Row, i_Pos.Column] = new BoardItem(i_Symbol);
        }

        public void SetItem(eSymbol i_Symbol, int i_Row, int i_Col)
        {
            m_Board[i_Row, i_Col] = new BoardItem(i_Symbol);
        }


        public eSymbol GetItem(Position i_Pos)
        {
            return m_Board[i_Pos.Row, i_Pos.Column].Symbol;
        }

        public BoardItem GetItem(int i_Row, int i_Col)
        {
            return m_Board[i_Row, i_Col];
        }


        public void Delete(Position i_Pos)
        {
            m_Board[i_Pos.Row, i_Pos.Column].IsOccupied = false;
        }

        public void Delete(int i_Row, int i_Col)
        {
            m_Board[i_Row, i_Col].IsOccupied = false;
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
            return m_Board[i_Row, i_Column].IsOccupied;
        }
    }
}
