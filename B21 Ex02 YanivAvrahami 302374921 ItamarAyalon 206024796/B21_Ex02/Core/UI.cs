using System;
using System.Text;
using Ex02.ConsoleUtils;

namespace B21_Ex02
{
    public class UI
    {
        private readonly XMixDrixReverseEngine r_GameEngine;
        private bool m_playing;

        public UI()
        {
            m_playing = false;
            r_GameEngine = new XMixDrixReverseEngine(2);
        }

        public void Run()
        {
            m_playing = true;
            startMenu();
            startGame();
        }

        private void startMenu()
        {
            getBoardChoice();
            getPlayModeChoice();
            createPlayers();
        }

        private void startGame()
        {
            r_GameEngine.StartNewGame();
            render();

            while (m_playing)
            {
                update();
                render();
            }
        }

        private void render()
        {
            Screen.Clear();
            Console.WriteLine(BoardViewer.GetBoardAsString(r_GameEngine.Board));

            if (r_GameEngine.GameState == eGameState.InProgress)
            {
                string underboardMsg = r_GameEngine.CurrentPlayerTurn.Name + " turn:";
                Console.WriteLine(underboardMsg);
                ConsoleUtils.WriteUnderline(underboardMsg.Length);
            }
            else if (r_GameEngine.GameState != eGameState.InProgress)
            {
                displayScoreBoard();
            }
        }

        private void update()
        {
            if (r_GameEngine.IsGameRunning)
            {
                Console.WriteLine("Enter row and col (R C): ");
                string userInput;
                bool isValidInput;

                do
                {
                    userInput = Console.ReadLine();

                    isValidInput = r_GameEngine.PlayTurnByInput(userInput);
                    if (!isValidInput)
                    {
                        ConsoleUtils.ReportInvalid();
                    }
                }
                while (!isValidInput);
            }
            else
            {
                if (getExtraRoundChoice())
                {
                    r_GameEngine.StartNewGame();
                }
                else
                {
                    m_playing = false;
                }
            }
        }

        private bool getExtraRoundChoice()
        {
            Console.WriteLine("Want to play more? (y/n): ");
            bool isExtraRound = false;
            bool isValidInput = false;
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

        private void getBoardChoice()
        {
            string msg = "Enter board size between 3 - 9: ";
            Console.Write(msg);
            int boardSize;
            string inputString;
            bool isValidInput = false;

            do
            {
                inputString = Console.ReadLine();

                if (int.TryParse(inputString, out boardSize))
                {
                    isValidInput = r_GameEngine.IsValidBoardChoice(boardSize);
                }

                if (!isValidInput)
                {
                    ConsoleUtils.ReportInvalid(msg);
                }
            }
            while (!isValidInput);

            r_GameEngine.SetBoardSize(boardSize, boardSize);
        }

        private void getPlayModeChoice()
        {
            string msg = "Enter play mode (1-singleplayer, 2-multiplayer): ";
            Console.Write(msg);
            int playMode;
            string inputString;
            bool isValidInput = false;

            do
            {
                inputString = Console.ReadLine();

                if (int.TryParse(inputString, out playMode))
                {
                    isValidInput = r_GameEngine.IsValidPlayModeChoice(playMode);
                }

                if (!isValidInput)
                {
                    ConsoleUtils.ReportInvalid(msg);
                }
            }
            while (!isValidInput);

            r_GameEngine.SetPlayMode(playMode);
        }

        private bool isYesOrNo(string i_String)
        {
            return i_String.ToLower() == "yes" || i_String.ToLower() == "y" ||
                   i_String.ToLower() == "no" || i_String.ToLower() == "n";
        }

        private void createPlayers()
        {
            r_GameEngine.AddPlayer(new Player(eSymbol.X, "Player 1"));

            if (r_GameEngine.PlayMode == ePlayMode.SinglePlayer)
            {
                r_GameEngine.AddPlayer(new NPC(eSymbol.O, "Computer", r_GameEngine.Board));
            }
            else
            {
                r_GameEngine.AddPlayer(new Player(eSymbol.O, "Player 2"));
            }
        }

        private void displayScoreBoard()
        {
            StringBuilder stringToDisplay = new StringBuilder();

            stringToDisplay.AppendLine("Score Board");
            stringToDisplay.AppendLine("-----------");
            foreach (var player in r_GameEngine.PlayersInGame)
            {
                stringToDisplay.AppendLine($"Name: {player.Name}  Score: {player.Score}");
            }

            Console.WriteLine(stringToDisplay);
        }
    }
}