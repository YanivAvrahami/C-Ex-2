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

        private void startMenu()
        {
            getBoardChoice();
            getPlayModeChoice();
            createPlayers();
            m_CurrentPlayerTurn = m_PlayerOne;
        }

        private void startGame()
        {
            render();
            while (m_Running)
            {
                update();
                render();
            }
        }

        private void update()
        {
            eGameState = eGameState.InProgress;
            handleCurrentTurn();
            setNextTurn();
            handleGameState();
        }

        private void render()
        {
            BoardViewer.DisplayOnConsole(m_Board);

            if (eGameState == eGameState.InProgress)
            {
                string underboardMsg = m_CurrentPlayerTurn.Name + " turn:";
                Console.WriteLine(underboardMsg);
                ConsoleUtils.WriteUnderline(underboardMsg.Length);
            }
            else if (eGameState == eGameState.Win)
            {
                displayScoreBoard();
            }
        }

        private void createPlayers()
        {
            if (ePlayMode == ePlayMode.SinglePlayer)
            {
                m_PlayerOne = new Player(eSymbol.O, ePlayerType.Human, "Player 1");
                m_PlayerTwo = new Player(eSymbol.X, ePlayerType.Human, "Player 2");
            }
            else
            {
                m_PlayerOne = new Player(eSymbol.O, ePlayerType.Human, "Player 1");
                m_PlayerTwo = new Player(eSymbol.X, ePlayerType.Computer, "Computer");
            }
        }

        private void handleCurrentTurn()
        {
            if (m_CurrentPlayerTurn.PlayerType == ePlayerType.Human)
            {
                Position positionToSetItem = getPositionChoice();

                m_Board.SetItem(m_CurrentPlayerTurn.Symbol, positionToSetItem);
                
                bool hasWon = BoardUtils.HasCompleteSymbolSequence(m_Board, m_CurrentPlayerTurn.Symbol, positionToSetItem);

                if (hasWon)
                {
                    eGameState = eGameState.Win;
                }
                else if (BoardUtils.IsFull(m_Board))
                {
                    eGameState = eGameState.Draw;
                }
            }
            else
            {
                
            }
        }

        private void handleGameState()
        {
            if (eGameState != eGameState.InProgress)
            {
                if (eGameState == eGameState.Win)
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

                bool wantExtraRound = getExtraRoundChoice();
                if (wantExtraRound)
                {
                    m_Board.Clear();
                    Console.Clear();
                    eGameState = eGameState.InProgress;
                }
                else
                {
                    m_Running = false;
                }
            }
        }

        private void setNextTurn()
        {
            if(ePlayMode == ePlayMode.SinglePlayer)
            {
                m_CurrentPlayerTurn = (m_CurrentPlayerTurn == m_PlayerOne) ? m_PlayerTwo : m_PlayerOne;
            }
        }

        private Position getPositionChoice()
        {
            Position pos = new Position();

            do
            {
                string msg1 = "Row: ";
                Console.Write(msg1);
                int row;
                while(!int.TryParse(Console.ReadLine(), out row) || pos.Row > m_Board.Height || pos.Row < 0)
                {
                    ConsoleUtils.ReportInvalid(msg1);
                }
                pos.Row = row;
                pos.Row--;


                ConsoleUtils.ClearLine(0);
                Console.SetCursorPosition(msg1.Length + 3, Console.CursorTop - 1);


                string msg2 = "Col: ";
                Console.Write(msg2);
                int col;
                while (!int.TryParse(Console.ReadLine(), out col) || pos.Column > m_Board.Width || pos.Column < 0)
                {
                    ConsoleUtils.ReportInvalid(msg1 + row + "  " + msg2);
                }
                pos.Column = col;
                pos.Column--;


                if (m_Board.IsOccupied(pos.Row, pos.Column))
                {
                    ConsoleUtils.ReportInvalid(msg1 + row + "  " + msg2 + col);
                    ConsoleUtils.ClearLine(0);
                }
            }
            while(m_Board.IsOccupied(pos.Row, pos.Column));

            return pos;
        }

        private bool getExtraRoundChoice()
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

        private void getPlayModeChoice()
        {
            string msg = "Enter play mode (1-singleplayer, 2-multiplayer): ";
            Console.Write(msg);

            int playMode = 0;

            string inputString = Console.ReadLine();

            while (!(isValidPlayModeChoice(inputString) || inputString == "Q"))
            {
                ConsoleUtils.ReportInvalid(msg);
                inputString = Console.ReadLine();
            }

            if (inputString == "Q")
            {
                eGameState = eGameState.Quit;
            }
            else
            {
                ePlayMode = (playMode == 1) ? ePlayMode.SinglePlayer : ePlayMode.MultiPlayer;
            }

        }

        private void getBoardChoice()
        {
            string msg = "Enter board size between 3 - 9: ";
            Console.Write(msg);

            string inputString = Console.ReadLine();

            while (!(isValidBoardChoice(inputString) || inputString == "Q"))
            {
                ConsoleUtils.ReportInvalid(msg);
                inputString = Console.ReadLine();
            }

            if (inputString == "Q")
            {
                eGameState = eGameState.Quit;
            }
            else
            {
                int boardSize = int.Parse(inputString);
                m_Board = new Board(boardSize, boardSize);
            }
        }

        private bool isValidPlayModeChoice(string i_String)
        {
            return int.TryParse(i_String, out int num) && (num == 1 || num == 2);
        }

        private bool isValidBoardChoice(string i_String)
        {
            return int.TryParse(i_String, out int num) && num <= 9 && num >= 3;
        }

        private void displayScoreBoard()
        {
            StringBuilder stringToDisplay = new StringBuilder();
            stringToDisplay.Append("Score Board");
            stringToDisplay.AppendLine();
            stringToDisplay.Append("-----------");
            stringToDisplay.AppendLine();
            stringToDisplay.Append($"Name: {m_PlayerOne.Name}  Score: {m_PlayerOne.Score}");
            stringToDisplay.AppendLine();
            stringToDisplay.Append($"Name: {m_PlayerTwo.Name}  Score: {m_PlayerTwo.Score}");
            stringToDisplay.AppendLine();
            Console.WriteLine(stringToDisplay);
        }
    }
}
