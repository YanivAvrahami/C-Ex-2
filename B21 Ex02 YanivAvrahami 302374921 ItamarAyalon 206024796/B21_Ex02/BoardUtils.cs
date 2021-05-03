using System;
using System.Collections.Generic;
using System.Text;

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
            BoardViewer.DisplayOnConsole(i_Board);
            int size = (i_Orientation == eOrientation.Horizontal) ? i_Board.Width : i_Board.Height;

            for (int i = 0; i < i_Board.Width; i++)
            {
                Board.BoardItem currentBoardItem;

                if (i_Orientation == eOrientation.Horizontal)
                {
                    currentBoardItem = i_Board.GetItem(i_Pos.Row, i);
                }
                else if(i_Orientation == eOrientation.Vertical)
                {
                    currentBoardItem = i_Board.GetItem(i, i_Pos.Column);
                }
                else if(i_Orientation == eOrientation.Ascending)
                {
                    currentBoardItem = i_Board.GetItem(i_Board.Height - 1 - i, i);
                }
                else
                {
                    currentBoardItem = i_Board.GetItem(i, i);
                }

                if (!currentBoardItem.IsOccupied || currentBoardItem.Symbol != i_Symbol)
                {
                    result = false;
                    break;
                }
            }

            return result;
        }
    }
}
