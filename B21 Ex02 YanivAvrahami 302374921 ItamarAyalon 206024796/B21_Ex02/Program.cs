namespace B21_Ex02
{
    class Program
    {
        public static void Main()
        {
            Board b = new Board(6, 6);
            b.SetItem(eSymbol.O, 2, 2);
            b.SetItem(eSymbol.X, 1, 0);
            BoardViewer.DisplayOnConsole(b);
            b.Delete(1, 0);
            b.Clear();
            BoardViewer.DisplayOnConsole(b);
        }
    }
}
