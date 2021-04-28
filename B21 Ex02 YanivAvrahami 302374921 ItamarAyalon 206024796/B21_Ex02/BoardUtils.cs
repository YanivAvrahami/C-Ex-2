using System;
using System.Collections.Generic;
using System.Text;

namespace B21_Ex02
{
    public class BoardUtils
    {
        public static bool IsBoardFull(Board i_Board)
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

        public static bool IsBoardHaveFullSequence(Board i_Board, int row, int column)
        {
            eSymbol symbol = i_Board.GetItem(row, column);

            return checkRow(symbol, i_Board, row) ||
                   checkColumn(symbol, i_Board, column) ||
                   checkDiagonalAscending(symbol, i_Board) ||
                   checkDiagonalDecending(symbol, i_Board);
        }

        private static bool checkRow(eSymbol i_Symbol, Board i_Board, int row)
        {
            bool isWon = true;
            for (int i = 0; i < i_Board.Width; i++)
            {
                if (i_Board.GetItem(row, i) != i_Symbol)
                {
                    isWon = false;
                    break;
                }
            }

            return isWon;
        }

        private static bool checkColumn(eSymbol i_Symbol, Board i_Board, int column)
        {
            bool isWon = true;
            for (int i = 0; i < i_Board.Height; i++)
            {
                if (i_Board.GetItem(i, column) != i_Symbol)
                {
                    isWon = false;
                    break;
                }
            }

            return isWon;
        }

        private static bool checkDiagonalDecending(eSymbol i_Symbol, Board i_Board)
        {
            bool isWon = true;
            for (int i = 0; i < i_Board.Height; i++)
            {
                if (i_Board.GetItem(i, i) != i_Symbol)
                {
                    isWon = false;
                    break;
                }
            }

            return isWon;
        }

        private static bool checkDiagonalAscending(eSymbol i_Symbol, Board i_Board)
        {
            bool isWon = true;
            for (int i = 0; i < i_Board.Height; i++)
            {
                if (i_Board.GetItem(i_Board.Height - 1 - i, i) != i_Symbol)
                {
                    isWon = false;
                    break;
                }
            }

            return isWon;
        }
    }
}
