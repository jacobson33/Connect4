using System;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

//
// Auhtors: Sam Lebel & Taylor Jacobson
//
// Date: 4th October, 2016
//
// Project: Connect 4
//

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

        private bool _error;
        private string _errorMessage;

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

        /// <summary>
        /// Init the Game
        /// </summary>
        public void InitializeGame()
        {
            // set variables
            _playingGame = true;
            _playingRound = false;
            _roundNumber = 0;

            _playerOneWins = 0;
            _playerTwoWins = 0;
            _playerDraws = 0;

            _error = false;
            _errorMessage = "";

            //initialize game board
            _gameboard.InitializeGameboard();
        }

        /// <summary>
        /// Main Loop
        /// </summary>
        public void PlayGame()
        {
            //display welcome screen
            _gameView.DisplayRules();

            //game loop
            while (_playingGame)
            {
                //display main menu
                MainMenu();

                while (_playingRound)
                {
                    _error = false;
                    //do stuff
                    ManageGameStateTasks();

                    //update game board
                    _gameboard.UpdateGameboardState();
                }
            }
        }

        /// <summary>
        /// Manages the input game stats and other...
        /// </summary>
        public void ManageGameStateTasks()
        {
            if (_gameView.CurrentViewState == ConsoleView.ViewState.Active)
            {
                //display game area
                _gameView.UpdateGameArea(_gameboard);

                //check round state
                switch (_gameboard.CurrentRoundState)
                {
                    case Gameboard.GameboardState.NewRound:
                        _roundNumber++;
                        _gameView.DisplayGameArea(_gameboard);
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
                        _gameView.DisplayWinner(Gameboard.PlayerColor.Red);
                        _playingRound = false;
                        break;

                    case Gameboard.GameboardState.PlayerTwoWin:
                        _playerTwoWins++;
                        _gameView.DisplayWinner(Gameboard.PlayerColor.Blue);
                        _playingRound = false;
                        break;

                    case Gameboard.GameboardState.PlayerDraw:
                        _playerDraws++;
                        _gameView.DisplayWinner(Gameboard.PlayerColor.None);
                        _playingRound = false;
                        break;
                }
            }
        }
   
        /// <summary>
        /// Manages the player's turns
        /// </summary>
        /// <param name="playerColor"></param>
        private void ManagePlayerTurn(Gameboard.PlayerColor playerColor)
        {
            int column = 0;
            bool validChoice = false;

            while (!validChoice)
            {
                ConsoleKey key = Console.ReadKey(true).Key;
                switch (key)
                {
                    case ConsoleKey.LeftArrow:
                        column--;
                        column = column < 0 ? _gameboard.MaxCols - 1 : column;
                        break;
                    case ConsoleKey.RightArrow:
                        column++;
                        column = column > _gameboard.MaxCols - 1 ? 0 : column;
                        break;
                    case ConsoleKey.Spacebar:
                        //attempt to place piece in selected column
                        if (_gameboard.GameboardPositionAvailable(column))
                        {
                            validChoice = true;
                            _gameboard.SetPlayerPiece(column, playerColor);
                        }
                        break;
                    case ConsoleKey.Escape:
                        InGameMenu();
                        break;
                }

                //Check if Still Playing
                if (_playingRound)
                    _gameView.UpdateGameArea(_gameboard, column);
                else
                    break;
            }
        }

        /// <summary>
        /// Main menu choices
        /// </summary>
        private void MainMenu()
        {
            _gameView.DisplayMainMenu(_error, _errorMessage);

            switch (_gameView.PromptChar())
            {
                case '1':
                    _playingRound = true;
                    _gameboard.InitializeGameboard();
                    break;
                case '2':
                    try
                    {
                        LoadGame();
                        _playingRound = true;
                    }
                    catch (DataCorruptException e)
                    {
                        _error = true;
                        _errorMessage = e.Message;
                    }
                    catch (FileNotFoundException e)
                    {
                        _error = true;
                        _errorMessage = e.Message;
                    }
                    catch (Exception)
                    {
                        _error = true;
                    }
                    break;
                case '3':
                    _gameView.DisplayExitMessage();
                    Thread.Sleep(2000);
                    Environment.Exit(0);
                    break;
            }
        }
        
        /// <summary>
        /// Option menu choices
        /// </summary>
        private void InGameMenu()
        {
            _gameView.DisplaySubMenu();

            switch (_gameView.PromptChar())
            {
                case '1':
                    _playingRound = false;
                    break;
                case '2':
                    SaveGame();
                    _gameView.DisplayGameArea(_gameboard);
                    break;
                case '3':
                    _gameView.DisplayGameArea(_gameboard);
                    break;
                default:
                    _gameView.DisplayGameArea(_gameboard);
                    break;
            }
        }

        #endregion

        #region Save / Load
        
        /// <summary>
        /// Save Game
        /// </summary>
        private void SaveGame()
        {
            string filepath = AppDomain.CurrentDomain.BaseDirectory + "/data.txt"; //base directory of application
            char delimeter = '|';

            try
            {
                //Instantiate new StreamWriter
                StreamWriter sw = new StreamWriter(filepath);
                using (sw)
                {
                    for (int row = 0; row < _gameboard.MaxRows; row++)
                    {
                        for (int col = 0; col < _gameboard.MaxCols; col++)
                        {
                            //build string for each position on gameboard and write to save file
                            string line = _gameboard.PositionState[row, col].ToString() + delimeter + row + delimeter + col;
                            sw.WriteLine(line);
                        }
                    }
                }
            }
             catch (Exception e)
            {
                
            }

        }

        /// <summary>
        /// Load Game
        /// </summary>
        private void LoadGame()
        {
            string filepath = AppDomain.CurrentDomain.BaseDirectory + "/data.txt"; //base directory of application
            char delimeter = '|';
            List<string> saveData = new List<string>();

            try
            {
                //Instantiate reader
                StreamReader sr = new StreamReader(filepath);
                using (sr)
                {
                    while (!sr.EndOfStream)
                    {
                        saveData.Add(sr.ReadLine());
                    }
                }
            }
            catch (FileNotFoundException e)
            {
                throw new FileNotFoundException("Save Game file is missing");
            }

            foreach (string s in saveData)
            {
                string[] line = s.Split(delimeter);
                try
                {
                    LoadPosition(line);
                }
                catch (Exception e)
                {
                    throw new DataCorruptException("Data file has been corrupted. Game cannot be loaded. ");
                }
            }
        }

        /// <summary>
        /// Loads a single position of the gameboard
        /// </summary>
        /// <param name="line">Line in text file with Player coordinates</param>
        private void LoadPosition(string[] line)
        {
            Gameboard.PlayerColor pc;

            try
            {
                int row = Int32.Parse(line[1]);
                int col = Int32.Parse(line[2]);

                if (Enum.TryParse(line[0], out pc)) { }
                else { pc = Gameboard.PlayerColor.None; }

                _gameboard.PositionState[row, col] = pc;
            }
            catch (IndexOutOfRangeException e)
            {
                throw;
            }
        }
        #endregion
    }
}

