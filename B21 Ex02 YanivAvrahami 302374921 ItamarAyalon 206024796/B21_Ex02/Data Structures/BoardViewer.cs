using System;
using System.Text;

namespace B21_Ex02
{
    public class BoardViewer
    {
        public static void DisplayOnConsole(Board i_Board)
        {
            StringBuilder boardStrBulder = new StringBuilder();

            buildFirstRowColumns(boardStrBulder, i_Board.Width);

            for (int row = 0; row < i_Board.Height; row++)
            {
                boardStrBulder.Append($"{row + 1} |");
                buildCellsInRow(i_Board, boardStrBulder, row);
                buildNewLine(boardStrBulder, i_Board.Width);
            }

            Console.WriteLine(boardStrBulder);
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
                    i_BoardStrBuiler.Append($" {i_Board.GetItem(i_Row, column)} |");
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
