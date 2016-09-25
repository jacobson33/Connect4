using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConnectFour.Models;

namespace ConnectFour.View
{
    public class ConsoleView
    {
        public enum ViewState { None, Active }

        private const int GAMEBOARD_VERTICAL_LOCATION = 4;
        private const int GAMEBOARD_VERTICAL_CURSOR = 3;

        private ConsoleMenu _consoleMenu;

        private Gameboard _gameboard;
        private ViewState _currentViewState;

        private int _WIDTH = 120;
        private int _HEIGHT = 40;

        public ViewState CurrentViewState
        {
            get { return _currentViewState; }
            set { _currentViewState = value; }
        }

        public ConsoleView(Gameboard gameboard)
        {
            _gameboard = gameboard;
            InitializeView();
        }

        public void InitializeView()
        {
            _currentViewState = ViewState.Active;
            InitializeConsole();
        }

        public void InitializeConsole()
        {
            Console.WindowWidth = _WIDTH;
            Console.WindowHeight = _HEIGHT;
            Console.BufferWidth = _WIDTH;
            Console.BufferHeight = _HEIGHT;

            _consoleMenu = new ConsoleMenu(_WIDTH, _HEIGHT);
        }

        //menu items
        public GameboardPosition GetPlayerPositionChoice()
        {
            //
            // Initialize gameboardPosition with -1 values
            //
            GameboardPosition gameboardPosition = new GameboardPosition(-1, -1);

            //
            // Get row number from player.
            //
            //gameboardPosition.Row = PlayerCoordinateChoice("Row");

            //
            // Get column number.
            //
            //if (CurrentViewState != ViewState.PlayerUsedMaxAttempts)
            //{
            //    gameboardPosition.Column = PlayerCoordinateChoice("Column");
            //}

            return gameboardPosition;

        }

        //
        //Displays the main menu to the screen
        //
        public void DisplayMainMenu()
        {
            List<string> options = new List<string> { "Connect 4", " ", "1) Start Game", "2) Exit" };

            _consoleMenu.DrawMenu(19, 16, options);
        }
    }
}
