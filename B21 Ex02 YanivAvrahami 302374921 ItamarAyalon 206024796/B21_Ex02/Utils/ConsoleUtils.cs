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

        public static void ReportInvalid(string i_SameMessage)
        {
            Console.SetCursorPosition(0, Console.CursorTop - 1);
            ClearLine(0);
            Console.WriteLine(i_SameMessage);
            Console.Write("Invalid input");
            Console.CursorTop--;
            Console.CursorLeft = i_SameMessage.Length;
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

        public static void WriteUnderline(int i_Length)
        {
            for (int i = 0; i < i_Length; i++)
            {
                Console.Write('-');
            }

            Console.WriteLine("");
        }
    }
}
