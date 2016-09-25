using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectFour
{
    public class GameController
    {
        #region Fields
        private bool _playingGame;
        private bool _playingRound;
        private int _roundNumber;
        private int _playerOneWins;
        private int _playerTwoWins;
        private int _playerDraws;

        private static Gameboard _gameboard = new Gameboard();
        private static ConsoleView _gameView = new ConsoleView(_gameboard);
        #endregion

        #region Constructors
        public GameController()
        {
            InitializeGame();
            PlayGame();
        }
        #endregion

        #region Methods
        public void InitializeGame()
        {
            // set variables
            _playingGame = true;
            _playingRound = false;
            _roundNumber = 0;

            _playerOneWins = 0;
            _playerTwoWins = 0;
            _playerDraws = 0;

            //initialize game board
           // _gameboard.InitializeGameboard();
        }

        public void PlayGame()
        {
            //display welcome screen
            _gameView.DisplayMainMenu();

            //game loop
            while (_playingGame)
            {
                //display main menu
                _gameView.DisplayMainMenu();

                switch (_gameView.PromptChar())
                {
                    case '1':

                        break;
                    case '2':

                        break;
                }


                while (_playingRound)
                {
                    //do stuff
                    ManageGameStateTasks();
                    //update game board
                    //_gameboard.UpdateGameboardState();
                }

                //handle round complete - display score screen

            }
            //handle game over

        }

        public void ManageGameStateTasks()
        {
            if (_gameView.CurrentViewState == ConsoleView.ViewState.Active)
            {
                //display game area
                //_gameView.DisplayGameArea();

                //check round state
                switch (_gameboard.CurrentRoundState)
                {
                    case Gameboard.GameboardState.NewRound:
                        _roundNumber++;
                        _gameboard.CurrentRoundState = Gameboard.GameboardState.PlayerOneTurn;
                        break;

                    case Gameboard.GameboardState.PlayerOneTurn:
                        ManagePlayerTurn(Gameboard.PlayerColor.Red);
                        break;

                    case Gameboard.GameboardState.PlayerTwoTurn:
                        ManagePlayerTurn(Gameboard.PlayerColor.Blue);
                        break;

                    case Gameboard.GameboardState.PlayerOneWin:
                        _playerOneWins++;
                        _playingRound = false;
                        break;

                    case Gameboard.GameboardState.PlayerTwoWin:
                        _playerTwoWins++;
                        _playingRound = false;
                        break;

                    case Gameboard.GameboardState.PlayerDraw:
                        _playerDraws++;
                        _playingRound = false;
                        break;

                    default:
                        break;
                }
            }
        }
   

        private void ManagePlayerTurn(Gameboard.PlayerColor playerColor)
        {
            //GameboardPosition gameboardPosition = _gameView.GetPlayerPositionChoice();
            /*
            if (_gameboard.GameboardPositionAvailable(gameboardPosition))
                   _gameboard.SetPlayerPiece(gameboardPosition, playerColor);
                else
                   _gameView.DisplayGamePositionChoiceNotAvailableScreen();
            */
        }
        #endregion
    }
}

