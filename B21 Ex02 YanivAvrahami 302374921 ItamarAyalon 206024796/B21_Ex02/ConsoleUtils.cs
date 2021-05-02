using System;

namespace B21_Ex02
{
    class ConsoleUtils
    {
        public static void ClearCurrentLine()
        {
            int currentLineCursor = Console.CursorTop;
            Console.SetCursorPosition(0, Console.CursorTop);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, currentLineCursor);
        }

        public static void OverWrite(string msg)
        {
            Console.SetCursorPosition(0, Console.CursorTop - 1);
            ClearCurrentLine();
            Console.WriteLine(msg);
            Console.CursorTop--;
            Console.CursorLeft = msg.Length;
        }
    }
}
