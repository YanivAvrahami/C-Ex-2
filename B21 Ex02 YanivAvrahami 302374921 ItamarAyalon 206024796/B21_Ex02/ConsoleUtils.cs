using System;

namespace B21_Ex02
{
    class ConsoleUtils
    {
        public static void ClearLine(int i_Offset)
        {
            Console.CursorTop += i_Offset;

            int currentLineCursor = Console.CursorTop;
            Console.SetCursorPosition(0, Console.CursorTop);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, currentLineCursor);

            Console.CursorTop -= i_Offset;
        }

        public static void ReportInvalid(string msg)
        {
            Console.SetCursorPosition(0, Console.CursorTop - 1);
            ConsoleUtils.ClearLine(0);
            Console.WriteLine(msg);
            Console.Write("Invalid input");
            Console.CursorTop--;
            Console.CursorLeft = msg.Length;
        }

        public static void RemoveReportInvalid()
        {
            ClearLine(1);
        }

        public static void WriteUnderline(int length)
        {
            for(int i = 0; i < length; i++)
            {
                Console.Write('-');
            }
            Console.WriteLine("");
        }
    }
}
