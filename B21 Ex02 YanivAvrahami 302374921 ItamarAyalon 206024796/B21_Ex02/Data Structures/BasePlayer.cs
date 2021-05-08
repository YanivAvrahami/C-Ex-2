namespace B21_Ex02
{
    public abstract class BasePlayer
    {
        private string m_Name;
        private int m_Score;
        private eSymbol m_Symbol;

        public string Name
        {
            get { return m_Name; }
            set { m_Name = value; }
        }

        public int Score
        {
            get { return m_Score; }
            set { m_Score = value; }
        }

        public eSymbol Symbol
        {
            get { return m_Symbol; }
            set { m_Symbol = value; }
        }

        public BasePlayer(eSymbol i_Symbol, string i_Name)
        {
            m_Name = i_Name;
            m_Symbol = i_Symbol;
            m_Score = 0;
        }
    }
}
