using System;
using System.Text;

namespace B21_Ex02
{
    class XMixDrix
    {
        private Board m_Board;
        private Player m_Player1;
        private Player m_Player2;
        private Player m_CurrentPlayer;
        private Player m_Winner;
        private bool m_IsGameRunning;
        private ePlayMode ePlayMode;
        private eGameState eGameState;

        public XMixDrix()
        {
            m_Board = null;
            m_Player1 = null;
            m_Player2 = null;
            m_IsGameRunning = true;
        }

        public void Run()
        {
            initializeSettings();
            BoardViewer.DisplayOnConsole(m_Board);

            while (m_IsGameRunning)
            {
                playTurnAndUpdateGameState();
                Console.Clear();
                BoardViewer.DisplayOnConsole(m_Board);
                
                if(isEndOfRound())
                {
                    switch (eGameState)
                    {
                        case eGameState.Win:
                            m_Winner.Score++;
                            displayScoreBoard();
                            break;
                        case eGameState.Draw:
                            displayScoreBoard();
                            break;
                        case eGameState.Left:
                            m_IsGameRunning = false;
                            break;
                    }

                    bool isExtraRound = getExtraRoundChoiceFromPlayer();
                    if(isExtraRound == false)
                    {
                        m_IsGameRunning = false;
                    }
                    else
                    {
                        m_Board.Clear();
                    }
                }
            }
        }

        private void initializeSettings()
        {
            getBoardFromUser();
            getPlayModeFromUser();
            initializePlayers();
            eGameState = eGameState.InProgress;
        }

        private bool isEndOfRound()
        {
            return eGameState == eGameState.Draw || eGameState == eGameState.Left || eGameState == eGameState.Win;
        }

        private bool getExtraRoundChoiceFromPlayer()
        {
            bool isExtraRound = false;
            Console.WriteLine("Want to play more? (y/n): ");
            string stringInput = Console.ReadLine();
            char answer = stringInput[0];

            if(answer == 'y')
            {
                isExtraRound = true;
            }

            return isExtraRound;
        }

        private void initializePlayers()
        {
            m_Player1 = new Player(eSymbol.O, ePlayerTypeEnum.Human);

            if (ePlayMode == ePlayMode.SinglePlayer)
            {
                m_Player2 = new Player(eSymbol.X, ePlayerTypeEnum.Human);
            }
            else
            {
                m_Player2 = new Player(eSymbol.X, ePlayerTypeEnum.Computer);
            }

            m_CurrentPlayer = m_Player1;
        }

        private void displayScoreBoard()
        {
            StringBuilder stringToDisplay = new StringBuilder();
            stringToDisplay.Append("Score Board");
            stringToDisplay.AppendLine();
            stringToDisplay.Append("___________");
            stringToDisplay.AppendLine();
            stringToDisplay.Append($"Player 1 score: {m_Player1.Score}");
            stringToDisplay.AppendLine();
            stringToDisplay.Append($"Player 2 score: {m_Player2.Score}");
            stringToDisplay.AppendLine();
        }

        private void playTurnAndUpdateGameState()
        {
            if(m_CurrentPlayer.PlayerType == ePlayerTypeEnum.Computer)
            {
                // Computer turn logic
            }
            else
            {
                // Human turn logic
                getValidItemPositionFromPlayer(out int row, out int col);
                m_Board.SetItem(m_CurrentPlayer.Symbol, row, col);

                if (BoardUtils.IsBoardHaveFullSequence(m_Board, row, col) == true)
                {
                    eGameState = eGameState.Win;
                    m_Winner = m_CurrentPlayer;
                }
                else if (BoardUtils.IsBoardFull(m_Board) == true)
                {
                    eGameState = eGameState.Draw;
                }
            }

            swapTurns();
        }

        private void swapTurns()
        {
            if (m_CurrentPlayer == m_Player1)
            {
                m_CurrentPlayer = m_Player2;
            }
            else
            {
                m_CurrentPlayer = m_Player1;
            }
        }

        private void getValidItemPositionFromPlayer(out int o_Row, out int o_Col)
        {
            getPlayerItemPositionChoice(out o_Row, out o_Col);

            while (m_Board.IsOccupied(o_Row, o_Col) == true)
            {
                Console.WriteLine();
                Console.WriteLine("Invalid placement, please try again..");
                Console.WriteLine();
                getPlayerItemPositionChoice(out o_Row, out o_Col);
            }
        }

        private void getPlayerItemPositionChoice(out int o_Row, out int o_Col)
        {
            Console.WriteLine("Enter row: ");
            o_Row = int.Parse(Console.ReadLine());

            Console.WriteLine("Enter col: ");
            o_Col = int.Parse(Console.ReadLine());
        }

        private void getPlayModeFromUser()
        {
            Console.WriteLine("Enter play mode (1-singleplayer, 2-multiplayer): ");

            string userInput = Console.ReadLine();

            int.TryParse(userInput, out int playModeInput);

            switch (playModeInput)
            {
                case 1:
                    ePlayMode = ePlayMode.SinglePlayer;
                    break;
                case 2:
                    ePlayMode = ePlayMode.MultiPlayer;
                    break;
            }
            Console.Clear();
        }

        private void getBoardFromUser()
        {
            Console.WriteLine("Enter board size: ");
            int size = int.Parse(Console.ReadLine());
            m_Board = new Board(size, size);
            Console.Clear();
        }
    }
}
