namespace B21_Ex02
{
    public class BoardUtils
    {
        public static bool IsFull(Board i_Board)
        {
            bool isFull = true;

            for (int i = 0; i < i_Board.Height; i++)
            {
                for (int j = 0; j < i_Board.Width; j++)
                {
                    if (!i_Board.IsOccupied(i, j))
                    {
                        isFull = false;
                    }
                }
            }

            return isFull;
        }

        public static bool HasCompleteSymbolSequence(Board i_Board, eSymbol i_Symbol, Position i_Pos)
        {
            bool result = hasCompleteSymbolSequenceByOrientation(eOrientation.Horizontal, i_Board, i_Symbol, i_Pos) ||
                          hasCompleteSymbolSequenceByOrientation(eOrientation.Vertical, i_Board, i_Symbol, i_Pos) ||
                          hasCompleteSymbolSequenceByOrientation(eOrientation.Ascending, i_Board, i_Symbol, i_Pos) ||
                          hasCompleteSymbolSequenceByOrientation(eOrientation.Decending, i_Board, i_Symbol, i_Pos);

            return result;
        }

        private static bool hasCompleteSymbolSequenceByOrientation(eOrientation i_Orientation, Board i_Board, eSymbol i_Symbol, Position i_Pos)
        {
            bool result = true;
            int row, col;

            for (int i = 0; i < i_Board.Width; i++)
            {
                if (i_Orientation == eOrientation.Horizontal)
                {
                    row = i_Pos.Row;
                    col = i;
                }
                else if (i_Orientation == eOrientation.Vertical)
                {
                    row = i;
                    col = i_Pos.Column;
                }
                else if (i_Orientation == eOrientation.Ascending)
                {
                    row = i_Board.Height - 1 - i;
                    col = i;
                }
                else
                {
                    row = i;
                    col = i;
                }

                if (!i_Board.IsOccupied(row, col) || i_Board.GetItem(row, col) != i_Symbol)
                {
                    result = false;
                    break;
                }
            }

            return result;
        }
    }
}
