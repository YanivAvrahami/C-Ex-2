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
                // TODO: add userInput to get all the information from the user, without any logic.;
                update(); // TODO: At first check the input from the user and than decide how to act (update or skip the update)
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
                bool isValidInput;

                do
                {
                    userInput = Console.ReadLine();

                    isValidInput = isValidPositionStringInput(userInput);
                    if (!isValidInput)
                    {
                        ConsoleUtils.ReportInvalid();
                    }
                }
                while (!isValidInput);

                if (userInput.ToLower() == "q")
                {
                    m_GameState = eGameState.Quit;
                }
                else
                {
                    PlayerMove(Position.Parse(userInput));
                }
            }
            else if (m_CurrentPlayerTurn is NPC computer)
            {
                PlayerMove(computer.RandomNextMove());
            }
        }

        private void PlayerMove(Position i_Position)
        {
            m_Board.SetItem(m_CurrentPlayerTurn.Symbol, i_Position);
            bool hasWon = BoardUtils.HasCompleteSymbolSequence(m_Board, m_CurrentPlayerTurn.Symbol, i_Position);

            if (hasWon)
            {
                m_GameState = eGameState.Win;
            }
            else if (BoardUtils.IsFull(m_Board))
            {
                m_GameState = eGameState.Draw;
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

        private bool isYesOrNo(string i_String)
        {
            return i_String.ToLower() == "yes" || i_String.ToLower() == "y" ||
                   i_String.ToLower() == "no" || i_String.ToLower() == "n";
        }

        private bool getExtraRoundChoice()
        {
            Console.WriteLine("Want to play more? (y/n): ");
            bool isExtraRound = false;
            bool isValidInput;
            string userInput;

            do
            {
                userInput = Console.ReadLine();

                isValidInput = isYesOrNo(userInput);
                if (!isValidInput)
                {
                    ConsoleUtils.ReportInvalid();
                }
            }
            while (!isValidInput);

            if (userInput.ToLower() == "yes" || userInput.ToLower() == "y")
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

            while (!(isValidPlayModeChoice(inputString)))
            {
                ConsoleUtils.ReportInvalid(msg);
                inputString = Console.ReadLine();
            }

            int.TryParse(inputString, out playMode);
            m_PlayMode = (playMode == 1) ? ePlayMode.SinglePlayer : ePlayMode.MultiPlayer;
        }

        private void getBoardChoice()
        {
            string msg = "Enter board size between 3 - 9: ";
            Console.Write(msg);

            string inputString = Console.ReadLine();

            while (!(isValidBoardChoice(inputString)))
            {
                ConsoleUtils.ReportInvalid(msg);
                inputString = Console.ReadLine();
            }

            int boardSize = int.Parse(inputString);
            m_Board = new Board(boardSize, boardSize);
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
