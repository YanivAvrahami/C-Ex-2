using System;
using System.Collections.Generic;
using System.Text;
using Ex02.ConsoleUtils;

namespace B21_Ex02
{
    class XMixDrix
    {
        private bool m_Running;
        private int m_Turn;
        private Board m_Board;
        private readonly List<BasePlayer> m_PlayersInGame;
        private BasePlayer m_CurrentPlayerTurn;
        private ePlayMode m_PlayMode;
        private eGameState m_GameState;

        public XMixDrix()
        {
            m_PlayersInGame = new List<BasePlayer>(2);
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
            m_CurrentPlayerTurn = m_PlayersInGame[0];
        }

        private void startGame()
        {
            while (m_Running)
            {
                render();
                update();
                render();
            }
        }

        private void update()
        {
            handleCurrentTurn();
            setNextTurn();
            handleGameState();
        }

        private void render()
        {
            Screen.Clear();
            Console.WriteLine(BoardViewer.GetBoardAsString(m_Board));

            if (m_GameState == eGameState.InProgress)
            {
                string underboardMsg = m_CurrentPlayerTurn.Name + " turn:";
                Console.WriteLine(underboardMsg);
                ConsoleUtils.WriteUnderline(underboardMsg.Length);
            }
            else if (m_GameState != eGameState.InProgress)
            {
                displayScoreBoard();
                handleAnotherGameChoice();
            }
        }

        private void createPlayers()
        {
            m_PlayersInGame.Add(new Player(eSymbol.X, "Player 1"));

            if (m_PlayMode == ePlayMode.SinglePlayer)
            {
                m_PlayersInGame.Add(new NPC(eSymbol.O, "Computer", m_Board));
            }
            else
            {
                m_PlayersInGame.Add(new Player(eSymbol.O, "Player 2"));
            }
        }

        private void handleCurrentTurn()
        {
            if (m_CurrentPlayerTurn is Player currentPlayer)
            {
                Console.WriteLine("Enter row and col (R C): ");
                string userInput;

                do
                {
                    userInput = Console.ReadLine();

                    if (isValidPositionStringInput(userInput))
                    {
                        break;
                    }

                    ConsoleUtils.ReportInvalid();
                }
                while (true);

                if (userInput.ToLower() == "q")
                {
                    m_GameState = eGameState.Quit;
                }
                else
                {
                    Position positionToSetItem = Position.Parse(userInput);

                    m_Board.SetItem(currentPlayer.Symbol, positionToSetItem);

                    bool hasWon = BoardUtils.HasCompleteSymbolSequence(m_Board, currentPlayer.Symbol, positionToSetItem);

                    if (hasWon)
                    {
                        m_GameState = eGameState.Win;
                    }
                    else if (BoardUtils.IsFull(m_Board))
                    {
                        m_GameState = eGameState.Draw;
                    }
                }
            }
            else if (m_CurrentPlayerTurn is NPC computer)
            {
                Position positionToSetItem = computer.RandomNextMove();

                m_Board.SetItem(m_CurrentPlayerTurn.Symbol, positionToSetItem);
                bool hasWon = BoardUtils.HasCompleteSymbolSequence(m_Board, m_CurrentPlayerTurn.Symbol, positionToSetItem);

                if (hasWon)
                {
                    m_GameState = eGameState.Win;
                }
                else if (BoardUtils.IsFull(m_Board))
                {
                    m_GameState = eGameState.Draw;
                }
            }
        }

        private void handleGameState()
        {
            if (m_GameState != eGameState.InProgress)
            {
                if (m_GameState == eGameState.Win || m_GameState == eGameState.Quit)
                {
                    m_CurrentPlayerTurn.Score++;
                }

                m_Turn = 0;
                m_CurrentPlayerTurn = m_PlayersInGame[0];
            }
        }

        private void handleAnotherGameChoice()
        {
            if (m_GameState != eGameState.InProgress)
            {
                if (m_Running)
                {
                    bool wantExtraRound = getExtraRoundChoice();
                    if (wantExtraRound)
                    {
                        m_Board.Clear();
                        m_GameState = eGameState.InProgress;
                    }
                    else
                    {
                        m_Running = false;
                    }
                }
            }
        }

        private void setNextTurn()
        {
            m_Turn++;
            int playerTurn = m_Turn % m_PlayersInGame.Count;

            m_CurrentPlayerTurn = m_PlayersInGame[playerTurn];
        }

        private bool isValidPositionStringInput(string i_userInput)
        {
            string[] words = i_userInput.Split(' ');
            bool isValid = false;

            if (words.Length == 2)
            {
                if (int.TryParse(words[0], out int row) && int.TryParse(words[1], out int col))
                {
                    if (isValidPositionInput(row - 1, col - 1))
                    {
                        isValid = true;
                    }
                }
            }
            else if (i_userInput.ToLower() == "q")
            {
                isValid = true;
            }

            return isValid;
        }

        private bool isValidPositionInput(int i_Row, int i_Col)
        {
            return ((0 <= i_Row && i_Row < m_Board.Height) &&
                (0 <= i_Col && i_Col < m_Board.Width) &&
                !m_Board.IsOccupied(i_Row, i_Col));
        }

        private bool getExtraRoundChoice()
        {
            Console.WriteLine("Want to play more? (y/n): ");
            string stringInput = Console.ReadLine();
            char answer = stringInput[0];
            bool isExtraRound = false;

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

            int playMode;
            string inputString = Console.ReadLine();

            while (!(isValidPlayModeChoice(inputString) || inputString == "Q"))
            {
                ConsoleUtils.ReportInvalid(msg);
                inputString = Console.ReadLine();
            }

            if (inputString == "Q")
            {
                m_GameState = eGameState.Quit;
            }
            else
            {
                int.TryParse(inputString, out playMode);
                m_PlayMode = (playMode == 1) ? ePlayMode.SinglePlayer : ePlayMode.MultiPlayer;
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
                m_GameState = eGameState.Quit;
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

            stringToDisplay.AppendLine("Score Board");
            stringToDisplay.AppendLine("-----------");
            foreach (var player in m_PlayersInGame)
            {
                stringToDisplay.AppendLine($"Name: {player.Name}  Score: {player.Score}");
            }

            Console.WriteLine(stringToDisplay);
        }
    }
}
