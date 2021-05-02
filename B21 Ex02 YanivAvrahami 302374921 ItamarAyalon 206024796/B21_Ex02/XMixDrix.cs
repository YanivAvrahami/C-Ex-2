using System;
using System.Collections.Generic;
using System.Text;

namespace B21_Ex02
{
    class XMixDrix
    {
        private bool m_Running;
        private Board m_Board;
        private Player m_PlayerOne;
        private Player m_PlayerTwo;
        private Player m_CurrentPlayerTurn;
        private ePlayMode ePlayMode;
        private eGameState eGameState;

        public XMixDrix()
        {
            m_PlayerOne = null;
            m_PlayerTwo = null;
            m_CurrentPlayerTurn = null;
            m_Running = true;
            m_Board = null;
        }

        public void Start()
        {
            startMenu();
            startGame();
        }

        private void startGame()
        {
            eGameState = eGameState.InProgress;
            BoardViewer.DisplayOnConsole(m_Board);

            while (m_Running)
            {
                playTurn();
                BoardViewer.DisplayOnConsole(m_Board);

                if (eGameState != eGameState.InProgress)
                {
                    if(eGameState == eGameState.Win)
                    {
                        m_CurrentPlayerTurn.Score++;
                        displayScoreBoard();
                    }
                    else if (eGameState == eGameState.Draw)
                    {
                        displayScoreBoard();
                    }
                    else
                    {
                        m_Running = false;
                    }
                   
                    bool extraRound = getExtraRoundDecisionFromPlayer();
                    if (extraRound == true)
                    {
                        m_Board.Clear();
                    }
                    else
                    {
                        m_Running = false;
                    }
                }
            }
        }

        private void startMenu()
        {
            setBoardByUser();
            setPlayModeByUser();
            initializePlayers();
        }

        private bool getExtraRoundDecisionFromPlayer()
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
            m_PlayerOne = new Player(eSymbol.O, ePlayerTypeEnum.Human);

            if (ePlayMode == ePlayMode.SinglePlayer)
            {
                m_PlayerTwo = new Player(eSymbol.X, ePlayerTypeEnum.Human);
            }
            else
            {
                m_PlayerTwo = new Player(eSymbol.X, ePlayerTypeEnum.Computer);
            }

            m_CurrentPlayerTurn = m_PlayerOne;
        }

        private void displayScoreBoard()
        {
            StringBuilder stringToDisplay = new StringBuilder();
            stringToDisplay.Append("Score Board");
            stringToDisplay.AppendLine();
            stringToDisplay.Append("___________");
            stringToDisplay.AppendLine();
            stringToDisplay.Append($"Player 1 score: {m_PlayerOne.Score}");
            stringToDisplay.AppendLine();
            stringToDisplay.Append($"Player 2 score: {m_PlayerTwo.Score}");
            stringToDisplay.AppendLine();
        }

        private void playTurn()
        {
            if(m_CurrentPlayerTurn.PlayerType == ePlayerTypeEnum.Computer)
            {
                // Computer turn logic
            }
            else
            {
                // Human turn logic
                getFreePositionFromPlayer(out int row, out int col);
                m_Board.SetItem(m_CurrentPlayerTurn.Symbol, row, col);

                if (BoardUtils.IsBoardHaveFullSequence(m_Board, row, col))
                {
                    eGameState = eGameState.Win;
                }
                else if (BoardUtils.IsBoardFull(m_Board))
                {
                    eGameState = eGameState.Draw;
                }
            }

            setNextPlayerTurn();
        }

        private void setNextPlayerTurn()
        {
            m_CurrentPlayerTurn = (m_CurrentPlayerTurn == m_PlayerOne) ? m_PlayerTwo : m_PlayerOne;
        }

        private void getFreePositionFromPlayer(out int o_Row, out int o_Col)
        {
            int row, col;
            getPositionFromUser(out row, out col);

            while (m_Board.IsOccupied(row, col))
            {
                Console.WriteLine("\nInvalid position, try again: \n");
                getPositionFromUser(out row, out col);
            }

            o_Row = row;
            o_Col = col;
        }

        private void getPositionFromUser(out int o_Row, out int o_Col)
        {
            int row = 0, col = 0;

            Console.Write("Row: ");
            bool good = false;
            while (!good)
            {
                string inputStr = Console.ReadLine();
                good = int.TryParse(inputStr, out row);
                Console.CursorTop--;
                int k_Space = 8;
                Console.CursorLeft += k_Space;
                if (!good)
                {
                    Console.Write("Invalid input, try again: ");
                }
            }

            Console.Write("Col: ");
            good = false;
            while (!good)
            {
                good = int.TryParse(Console.ReadLine(), out col);
                if (!good)
                {
                    Console.Write("Invalid input, try again: ");
                }
            }

            o_Row = row - 1;
            o_Col = col - 1;
        }

        private void setPlayModeByUser()
        {
            string msg = "Enter play mode (1-singleplayer, 2-multiplayer): ";
            Console.Write(msg);

            int playMode;
            while (!int.TryParse(Console.ReadLine(), out playMode) || playMode != 1 && playMode != 2)
            {
                Console.SetCursorPosition(0, Console.CursorTop - 1);
                ConsoleUtils.ClearCurrentLine();
                Console.WriteLine(msg);
                Console.Write("Invalid input");
                Console.CursorTop--;
                Console.CursorLeft = msg.Length;
            }
            
            ePlayMode = (playMode == 1) ? ePlayMode.SinglePlayer : ePlayMode.MultiPlayer;
        }

        private void setBoardByUser()
        {
            string msg = "Enter board size between 3 - 9: ";
            Console.Write(msg);

            int boardSize;
            while(!int.TryParse(Console.ReadLine(), out boardSize) || boardSize > 9 || boardSize < 3)
            {
                Console.SetCursorPosition(0, Console.CursorTop - 1);
                ConsoleUtils.ClearCurrentLine();
                Console.WriteLine(msg);
                Console.Write("Invalid input");
                Console.CursorTop--;
                Console.CursorLeft = msg.Length;
            }

            m_Board = new Board(boardSize, boardSize);
        }
    }
}
