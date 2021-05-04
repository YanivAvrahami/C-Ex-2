using System.Collections.Generic;

namespace B21_Ex02
{
    public class XMixDrixReverseEngine
    {
        private bool m_IsGameRunning;   // was m_Running
        private int m_CurrentTurn;      // was m_Turn
        private Board m_Board;
        private readonly int r_MaxNumberOfPlayers;
        private readonly List<BasePlayer> r_PlayersInGame;
        private BasePlayer m_CurrentPlayerTurn;
        private ePlayMode m_PlayMode;
        private eGameState m_GameState;


        #region Public Properties

        public Board Board
        { 
            get { return m_Board; }
            private set
            {
                if (!m_IsGameRunning)
                {
                    m_Board = value;
                }
            }
        }

        public BasePlayer CurrentPlayerTurn
        {
            get { return m_CurrentPlayerTurn; }
            private set { m_CurrentPlayerTurn = value; }
        }

        public int MaxNumberOfPlayers
        {
            get { return r_MaxNumberOfPlayers; }
        }

        public List<BasePlayer> PlayersInGame
        {
            get { return r_PlayersInGame; }
        }

        public ePlayMode PlayMode
        {
            get { return m_PlayMode; }
            private set 
            {
                if (!m_IsGameRunning)
                {
                    m_PlayMode = value;
                }
            }
        }

        public eGameState GameState
        {
            get { return m_GameState; }
            private set { m_GameState = value; }
        }

        public bool IsGameRunning
        {
            get { return m_IsGameRunning; }
            private set { m_IsGameRunning = value; }
        }

        public int CurrentTurn
        {
            get { return m_CurrentTurn; }
            private set { m_CurrentTurn = value; }
        }

        #endregion

        #region Constructor

        public XMixDrixReverseEngine(int i_MaxPlayers)
        {
            r_MaxNumberOfPlayers = i_MaxPlayers;
            r_PlayersInGame = new List<BasePlayer>(MaxNumberOfPlayers);
        }

        #endregion

        public void SetBoardSize(int i_NumberOfRows, int i_NumberOfCols)
        {
            Board = new Board(i_NumberOfRows, i_NumberOfCols);
        }

        public void SetPlayMode(int i_PlayMode)
        {
            if (i_PlayMode == 1)
            {
                PlayMode = ePlayMode.SinglePlayer;
            }
            else if (i_PlayMode == 2)
            {
                PlayMode = ePlayMode.MultiPlayer;
            }
        }

        public void AddPlayer(BasePlayer i_Player)
        {
            if (!IsGameRunning)
            {
                if (PlayersInGame.Count < MaxNumberOfPlayers)
                {
                    PlayersInGame.Add(i_Player);
                }
            }
        }

        public void StartNewGame()
        {
            if (PlayersInGame.Count == 0)
            {
                // throw exception
            }

            Board.Clear();
            m_IsGameRunning = true;
            m_CurrentTurn = 0;
            m_CurrentPlayerTurn = r_PlayersInGame[0];
            m_GameState = eGameState.InProgress;
        }

        public bool PlayTurnByInput(string i_userInput)
        {
            string[] words = i_userInput.Split(' ');
            bool isValid = false;

            if (words.Length == 2)
            {
                if (int.TryParse(words[0], out int row) && int.TryParse(words[1], out int col))
                {
                    if (isValidPosition(row - 1, col - 1))
                    {
                        isValid = true;
                    }
                }
            }
            else if (i_userInput.ToLower() == "q")
            {
                isValid = true;
            }

            if (isValid)
            {
                nextActionOnPlayerInput(i_userInput);
                if (CurrentPlayerTurn is NPC npcTurn)
                {
                    playMove(npcTurn.RandomNextMove());
                    changeTurn();
                    handleGameState();
                }
            }

            return isValid;
        }

        private void nextActionOnPlayerInput(string i_PlayerInputStr)
        {
            if (i_PlayerInputStr.ToLower() == "q")
            {
                m_GameState = eGameState.Quit;
            }
            else
            {
                playMove(Position.Parse(i_PlayerInputStr));
            }

            changeTurn();
            handleGameState();
        }

        private void playMove(Position i_Position)
        {
            Board.SetItem(CurrentPlayerTurn.Symbol, i_Position);
            bool hasWon = BoardUtils.HasCompleteSymbolSequence(Board, CurrentPlayerTurn.Symbol, i_Position);

            if (hasWon)
            {
                GameState = eGameState.Win;
            }
            else if (BoardUtils.IsFull(m_Board))
            {
                GameState = eGameState.Draw;
            }
        }

        private void changeTurn()
        {
            CurrentTurn++;
            int playerTurn = CurrentTurn % PlayersInGame.Count;

            CurrentPlayerTurn = PlayersInGame[playerTurn];
        }

        private void handleGameState()
        {
            if (GameState != eGameState.InProgress)
            {
                if (GameState == eGameState.Win || GameState == eGameState.Quit)
                {
                    CurrentPlayerTurn.Score++;
                }

                CurrentTurn = 0;
                CurrentPlayerTurn = PlayersInGame[0];
                IsGameRunning = false;
            }
        }

        private bool isValidPosition(int i_Row, int i_Col)
        {
            return ((0 <= i_Row && i_Row < Board.Height) &&
                (0 <= i_Col && i_Col < Board.Width) &&
                !Board.IsOccupied(i_Row, i_Col));
        }

        public bool IsValidBoardChoice(int i_Size)
        {
            return (3 <= i_Size && i_Size <= 9);
        }

        public bool IsValidPlayModeChoice(int i_Mode)
        {
            return (i_Mode == 1 || i_Mode == 2);
        }
    }
}
