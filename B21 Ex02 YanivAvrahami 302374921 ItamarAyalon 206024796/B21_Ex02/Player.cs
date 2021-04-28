namespace B21_Ex02
{
    public class Player
    {
        private eSymbol m_Symbol;

        private int m_Score;

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
    }
}
