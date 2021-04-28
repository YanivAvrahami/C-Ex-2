namespace B21_Ex02
{
    public class Board
    {
        private struct BoardItem
        {
            private bool isOccupied;
            private eSymbol m_Symbol;

            public eSymbol Symbol
            {
                get
                {
                    return m_Symbol;
                }
                set
                {
                    m_Symbol = value;
                }
            }

            public bool IsOccupied
            {
                get
                {
                    return isOccupied;
                }
                set
                {
                    isOccupied = true;
                }
            }

            public BoardItem(eSymbol i_Symbol)
            {
                isOccupied = true;
                m_Symbol = i_Symbol;
            }
        }

        private readonly BoardItem[,] m_Board;

        public int Size
        {
            get
            {
                return m_Board.Length;
            }
        }

        public Board(int i_Rows, int i_Columns)
        {
            m_Board = new BoardItem[i_Rows,i_Columns];
        }

        public void SetItem(eSymbol i_Symbol, int i_Row, int i_Column)
        {
            m_Board[i_Row, i_Column] = new BoardItem(i_Symbol);
        }

        public eSymbol GetItem(int i_Row, int i_Column)
        {
            return m_Board[i_Row, i_Column].Symbol;
        }

        public void Delete(int i_Row, int i_Column)
        {
            m_Board[i_Row, i_Column].IsOccupied = false;
        }

        public void Clear()
        {
            for (int i = 0; i < m_Board.Length; i++)
            {
                for (int j = 0; j < m_Board.Length; j++)
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
