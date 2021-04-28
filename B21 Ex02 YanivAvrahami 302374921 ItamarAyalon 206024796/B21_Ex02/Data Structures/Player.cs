namespace B21_Ex02
{
    public class Player
    {
        private ePlayerTypeEnum m_PlayerType;
        private int m_Score;
        private eSymbol m_Symbol;

        public ePlayerTypeEnum PlayerType
        {
            get
            {
                return m_PlayerType;
            }
            set
            {
                m_PlayerType = value;
            }
        }

        public int Score
        {
            get
            {
                return m_Score;
            }
            set
            {
                m_Score = value;
            }
        }

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

        public Player(eSymbol i_Symbol, ePlayerTypeEnum i_PlayerType)
        {
            m_Symbol = i_Symbol;
            m_Score = 0;
            m_PlayerType = i_PlayerType;
        }
    }
}
