using System;

namespace B21_Ex02
{
    class XMixDrix
    {
        private Board m_Board;
        private Player m_Player1;
        private Player m_Player2;
        private Player m_PlayerTurn;
        private ePlayMode m_PlayMode;
        private eEndGameState m_EndGameState;
        private bool m_Running;

        public XMixDrix()
        {
            m_Board = null;

            m_Player1 = new Player();
            m_Player1.Symbol = eSymbol.O;
            m_Player2 = new Player();
            m_Player2.Symbol = eSymbol.X;
            
            m_PlayerTurn = m_Player1;
            
            m_Running = true;
        }

        public void Run()
        {
            getBoardFromUser();
            getPlayModeFromUser();
            BoardViewer.DisplayOnConsole(m_Board);

            while (m_Running == true)
            {
                getValidPlayerPlacementChoice(out int validRow, out int validCol);
                m_Board.SetItem(m_PlayerTurn.Symbol, validRow, validCol);
                BoardViewer.DisplayOnConsole(m_Board);
                


                //      if there is a sequence then the player without the sequence win
                //      he gains a point
                //      DisplayPlayersScore
                //      if the board is full then there is a draw and we DisplayPlayersScore
                //      if endGameState is not 'none' then ask the user if he wants another round
                //      we will just reset the board and start a new game
            }
        }

        private void getValidPlayerPlacementChoice(out int o_Row, out int o_Col)
        {
            bool isValidPlacement = true;
            
            getPlayerPlacementChoice(out o_Row, out o_Col);

            while (m_Board.IsOccupied(o_Row, o_Col) == true)
            {
                Console.WriteLine("Invalid placement, please try again..");
                getPlayerPlacementChoice(out o_Row, out o_Col);
            }
        }

        private void getPlayerPlacementChoice(out int o_Row, out int o_Col)
        {
            Console.WriteLine("Enter row: ");
            o_Row = Console.Read();

            Console.WriteLine("Enter col: ");
            o_Col = Console.Read();
        }

        private void startGame()
        {

        }

        private void getPlayModeFromUser()
        {
            Console.WriteLine("Enter play mode (1-singleplayer, 2-multiplayer): ");

            int playModeInput;

            string userInput = Console.ReadLine();

            int.TryParse(userInput, out playModeInput);

            switch (playModeInput)
            {
                case 1:
                    m_PlayMode = ePlayMode.SinglePlayer;
                    break;
                case 2:
                    m_PlayMode = ePlayMode.MultiPlayer;
                    break;
            }
        }

        private void getBoardFromUser()
        {
            Console.WriteLine("Enter board size: ");
            int size = Console.Read();
            m_Board = new Board(size, size);
        }
    }
}
