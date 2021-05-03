using System;
using System.Text;

namespace B21_Ex02
{
    public class BoardViewer
    {
        public static string GetBoardAsString(Board i_Board)
        {
            StringBuilder boardStrBuilder = new StringBuilder();

            buildFirstRowColumns(boardStrBuilder, i_Board.Width);

            for (int row = 0; row < i_Board.Height; row++)
            {
                boardStrBuilder.Append($"{row + 1} |");
                buildCellsInRow(i_Board, boardStrBuilder, row);
                buildNewLine(boardStrBuilder, i_Board.Width);
            }

            return boardStrBuilder.ToString();
        }

        private static void buildFirstRowColumns(StringBuilder i_BoardStrBuiler, int i_Columns)
        {
            i_BoardStrBuiler.Append("   ");
            for (int i = 0; i < i_Columns; i++)
            {
                i_BoardStrBuiler.Append($" {i + 1}  ");
            }

            i_BoardStrBuiler.Append(Environment.NewLine);
        }

        private static void buildCellsInRow(Board i_Board, StringBuilder i_BoardStrBuiler, int i_Row)
        {
            for (int column = 0; column < i_Board.Width; column++)
            {
                if (i_Board.IsOccupied(i_Row, column) == true)
                {
                    i_BoardStrBuiler.Append($" {i_Board.GetItem(i_Row, column).Symbol} |");
                }
                else
                {
                    i_BoardStrBuiler.Append($"   |");
                }
            }

            i_BoardStrBuiler.Append(Environment.NewLine);
        }

        private static void buildNewLine(StringBuilder i_BoardStrBuiler, int i_Columns)
        {
            i_BoardStrBuiler.Append("===");
            for (int i = 0; i < i_Columns; i++)
            {
                i_BoardStrBuiler.Append("====");
            }

            i_BoardStrBuiler.Append(Environment.NewLine);
        }
    }
}
