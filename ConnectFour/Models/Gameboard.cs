using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectFour
{
    public class Gameboard
    {
        #region Enums
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
        #endregion  

        #region Fields
        private const int MAX_ROWS = 6;
        private const int MAX_COLS = 7;
        private const int WIN_CONDITION = 4;

        private PlayerColor[,] _positionState;
        private GameboardState _currentRoundState;
        #endregion

        #region Properties
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
        #endregion

        #region Constructors
        public Gameboard()
        {
            _positionState = new PlayerColor[MAX_ROWS, MAX_COLS];

            InitializeGameboard();
        }
        #endregion

        #region Methods
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
            int piecesInARow;

            //Check Horizontal Lines
            for (int row = 0; row < MAX_ROWS; row++)
            {
                //reset pieces for each row
                piecesInARow = 0;

                for (int col = 0; col < MAX_COLS; col++)
                {
                    if (_positionState[row,col] == playerColorToCheck)
                        piecesInARow++;

                    if (piecesInARow >= WIN_CONDITION)
                        return true;
                }
            }

            //Check Vertical Lines
            for (int col = 0; col < MAX_COLS; col++)
            {
                //reset pieces for each column
                piecesInARow = 0;

                for (int row = 0; row < MAX_ROWS; row++)
                {
                    if (_positionState[row, col] == playerColorToCheck)
                        piecesInARow++;

                    if (piecesInARow >= WIN_CONDITION)
                        return true;
                }
            }

            //Check Left Diagonal Lines
            //Only check positions that wouldn't check positions outside of _positionState bounds
            for (int row = 0; row <= (MAX_ROWS - WIN_CONDITION); row++)
            {
                for (int col = (WIN_CONDITION - 1); col < MAX_COLS; col++)
                {
                    if (_positionState[row, col] == playerColorToCheck)
                    {
                        //check diagonal (down/left) if this piece is match
                        if (_positionState[row + 1, col - 1] == playerColorToCheck &&
                            _positionState[row + 2, col - 2] == playerColorToCheck &&
                            _positionState[row + 3, col - 3] == playerColorToCheck)
                        {
                            return true;
                        }
                    }
                }
            }

            //Check Right Diagonal Lines
            //Only check positions that wouldn't check positions outside of _positionState bounds
            for (int row = 0; row <= (MAX_ROWS - WIN_CONDITION); row++)
            {
                for (int col = 0; col <= (WIN_CONDITION - 1); col++)
                {
                    if (_positionState[row, col] == playerColorToCheck)
                    {
                        //check diagonal (down/right) if this piece is match
                        if (_positionState[row + 1, col + 1] == playerColorToCheck &&
                            _positionState[row + 2, col + 2] == playerColorToCheck &&
                            _positionState[row + 3, col + 3] == playerColorToCheck)
                        {
                            return true;
                        }
                    }
                }
            }
            //return false if none of win conditions have returned true
            return false;
        }

        public void SetPlayerPiece(int column, PlayerColor playerColor)
        {
            //check game board from bottom row and insert player color into first open position
            for (int row = MAX_ROWS - 1; row >= 0; row--)
            {
                if (_positionState[row, column] == PlayerColor.None)
                {
                    _positionState[row, column] = playerColor;
                    break;
                }
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
        #endregion
    }
}
