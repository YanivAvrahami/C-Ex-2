using System;

namespace B21_Ex02
{
    class ConsoleUtils
    {
        public static void ClearLine(int i_OffsetInLine)
        {
            Console.CursorTop += i_OffsetInLine;
            int currentLineCursor = Console.CursorTop;

            Console.SetCursorPosition(0, Console.CursorTop);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, currentLineCursor);
            Console.CursorTop -= i_OffsetInLine;
        }

        public static void ReportInvalid(string i_MessageBeforeCursor)
        {
            Console.SetCursorPosition(0, Console.CursorTop - 1);
            ClearLine(0);
            Console.WriteLine(i_MessageBeforeCursor);
            Console.Write("Invalid input");
            Console.CursorTop--;
            Console.CursorLeft = i_MessageBeforeCursor.Length;
        }

        public static void ReportInvalid()
        {
            Console.SetCursorPosition(0, Console.CursorTop - 1);
            ClearLine(0);
            Console.WriteLine();
            Console.Write("Invalid input");
            Console.CursorTop--;
            Console.CursorLeft = 0;
        }

        public static void WriteUnderline(int i_UnderlineLength)
        {
            for (int i = 0; i < i_UnderlineLength; i++)
            {
                Console.Write('-');
            }

            Console.WriteLine("");
        }
    }
}
