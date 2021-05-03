namespace B21_Ex02
{
    public struct Position
    {
        public int Row { get; set; }
        public int Column { get; set; }

        public Position(int i_Row, int i_Col)
        {
            Row = i_Row;
            Column = i_Col;
        }

        public static Position Parse(string i_PositionStr)
        {
            string[] words = i_PositionStr.Split(' ', ',');

            return new Position(int.Parse(words[0]) - 1, int.Parse(words[1]) - 1);
        }
    }
}
