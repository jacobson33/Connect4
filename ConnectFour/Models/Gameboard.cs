using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectFour.Models
{
    public class Gameboard
    {
        public enum PlayerColor
        {
            None,
            Red,
            Blue
        }

        public enum GameboardState
        {
            NewRound,
            PlayerOneTurn,
            PlayerTwoTurn,
            PlayerOneWin,
            PlayerTwoWin,
            PlayerDraw
        }

        private const int MAX_ROWS = 6;
        private const int MAX_COLS = 7;

        private PlayerColor[,] _positionState;
        private GameboardState _currentRoundState;

        public int MaxRows
        {
            get { return MAX_ROWS; }
        }
        public int MaxCols
        {
            get { return MAX_COLS; }
        }
        public PlayerColor[,] PositionState
        {
            get { return _positionState; }
            set { _positionState = value; }
        }
        public GameboardState CurrentRoundState
        {
            get { return _currentRoundState; }
            set { _currentRoundState = value; }
        }

        public Gameboard()
        {
            _positionState = new PlayerColor[MAX_COLS, MAX_ROWS];
            InitializeGameboard();
        }


        public void InitializeGameboard()
        {
            _currentRoundState = GameboardState.NewRound;

            //set all positions to None
            for (int row = 0; row < MAX_ROWS; row++)
            {
                for (int col = 0; col < MAX_COLS; col++)
                {
                    _positionState[row, col] = PlayerColor.None;
                }
            }
        }

        public bool GameboardPositionAvailable(int column)
        {
            //check each row in the column, starting from the top
            //return true if "None" is found
            for (int row = 0; row < MAX_ROWS; row++)
            {
                if (_positionState[row, column] == PlayerColor.None)
                    return true;
            }                
            return false;
        }

        public void UpdateGameboardState()
        {
            //check for win conditions
            if (FourInARow(PlayerColor.Red))
                _currentRoundState = GameboardState.PlayerOneWin;
            else if (FourInARow(PlayerColor.Blue))
                _currentRoundState = GameboardState.PlayerTwoWin;
            else if (IsDrawGame())
                _currentRoundState = GameboardState.PlayerDraw;
        }

        public bool IsDrawGame()
        {
            for (int row = 0; row < MAX_ROWS; row++)
            {
                for (int col = 0; col < MAX_COLS; col++)
                    if (_positionState[row, col] == PlayerColor.None)
                        return false;
            }
            return true;
        }

        public bool FourInARow(PlayerColor playerColorToCheck)
        {
            //check for 4 pieces in a row



            return false;
        }

        public void SetPlayerPiece(int column, PlayerColor playerColor)
        {
            //check game board from bottom row and insert player color into first open position
            for (int row = MAX_ROWS - 1; row >= 0; row--)
            {
                if (_positionState[row, column] == PlayerColor.None)
                    _positionState[row, column] = playerColor;
            }
            //change player
            SetNextPlayer();
        }

        private void SetNextPlayer()
        {
            if (_currentRoundState == GameboardState.PlayerOneTurn)
                _currentRoundState = GameboardState.PlayerTwoTurn;
            else
                _currentRoundState = GameboardState.PlayerOneTurn;
        }
    }
}
