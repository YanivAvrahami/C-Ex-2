using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
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
            render();
            while (m_Running)
            {
                MakeAMove();

                if (eGameState != eGameState.InProgress)
                {
                    if(eGameState == eGameState.Win)
                    {
                        m_CurrentPlayerTurn.Score++;
                        displayScoreBoard();
                        Console.WriteLine("asdas\nasdsa");
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

                setNextPlayerTurn();
                render();
            }
        }


        private void render()
        {
            BoardViewer.DisplayOnConsole(m_Board);
            Console.WriteLine(m_CurrentPlayerTurn.Name + " turn:");
            Console.WriteLine("--------------");
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
            m_PlayerOne = new Player(eSymbol.O, ePlayerType.Human, "Player 1");

            if (ePlayMode == ePlayMode.SinglePlayer)
            {
                m_PlayerTwo = new Player(eSymbol.X, ePlayerType.Human, "Player 2");
            }
            else
            {
                m_PlayerTwo = new Player(eSymbol.X, ePlayerType.Computer, "Computer");
            }

            m_CurrentPlayerTurn = m_PlayerOne;
        }

        private void displayScoreBoard()
        {
            StringBuilder stringToDisplay = new StringBuilder();
            stringToDisplay.Append("Score Board");
            stringToDisplay.AppendLine();
            stringToDisplay.Append("_____________________");
            stringToDisplay.AppendLine();
            stringToDisplay.Append($"Name: {m_PlayerOne.Name}  Score: {m_PlayerOne.Score}");
            stringToDisplay.AppendLine();
            stringToDisplay.Append($"Name: {m_PlayerTwo.Name}  Score: {m_PlayerTwo.Score}");
            stringToDisplay.AppendLine();
        }

        private void MakeAMove()
        {
            if(m_CurrentPlayerTurn.PlayerType == ePlayerType.Computer)
            {
                // Computer turn logic
            }
            else
            {
                // Human turn logic
                Position pos = getFreePositionFromUser();
                m_Board.SetItem(m_CurrentPlayerTurn.Symbol, pos);

                bool hasWon = BoardUtils.HasCompleteSymbolSequence(m_Board, m_CurrentPlayerTurn.Symbol, pos);
                if (hasWon)
                {
                    eGameState = eGameState.Win;
                }
                else if (BoardUtils.IsFull(m_Board))
                {
                    eGameState = eGameState.Draw;
                }
            }
        }

        private void setNextPlayerTurn()
        {
            if(ePlayMode == ePlayMode.SinglePlayer)
            {
                m_CurrentPlayerTurn = (m_CurrentPlayerTurn == m_PlayerOne) ? m_PlayerTwo : m_PlayerOne;
            }
        }
        
        private Position getFreePositionFromUser()
        {
            Position pos = new Position();

            do
            {
                string msg1 = "Row: ";
                Console.Write(msg1);

                bool goodParse = int.TryParse(Console.ReadLine(), out int row);
                pos.Row = row;
                pos.Row--;
                
                while(!goodParse || pos.Row > m_Board.Height || pos.Row < 0)
                {
                    ConsoleUtils.ReportInvalid(msg1);
                }

                ConsoleUtils.ClearLine(0);
                Console.SetCursorPosition(msg1.Length + 3, Console.CursorTop - 1);

                string msg2 = "Col: ";
                Console.Write(msg2);

                goodParse = int.TryParse(Console.ReadLine(), out int col);
                pos.Column = col;
                pos.Column--;

                while (!goodParse || pos.Column > m_Board.Width || pos.Column < 0)
                {
                    ConsoleUtils.ReportInvalid(msg1 + pos.Row + "  " + msg2);
                }

                if(m_Board.IsOccupied(pos.Row, pos.Column))
                {
                    ConsoleUtils.ReportInvalid(msg1 + pos.Row + "  " + msg2 + pos.Column);
                    ConsoleUtils.ClearLine(0);
                }
            }
            while(m_Board.IsOccupied(pos.Row, pos.Column));

            return pos;
        }

        private void setPlayModeByUser()
        {
            string msg = "Enter play mode (1-singleplayer, 2-multiplayer): ";
            Console.Write(msg);

            int playMode;
            while (!int.TryParse(Console.ReadLine(), out playMode) || playMode != 1 && playMode != 2)
            {
                ConsoleUtils.ReportInvalid(msg);
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
                ConsoleUtils.ReportInvalid(msg);
            }

            m_Board = new Board(boardSize, boardSize);
        }
    }
}
