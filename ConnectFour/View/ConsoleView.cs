using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectFour
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

        public void DisplayGameArea(Gameboard _gameboard, int column = 0)
        {
            Console.Clear();
            //_consoleMenu.DrawGrid(_WIDTH / 2 - 14, _HEIGHT / 2 - 7);
            _consoleMenu.DrawGrid(_WIDTH / 2 - 14, _HEIGHT / 2 - 7, 6, 7, 3, 1);
            _consoleMenu.DrawPlayerPieces(_WIDTH / 2 - 14, _HEIGHT / 2 - 7, _gameboard);
            _consoleMenu.DrawCursor(_WIDTH / 2 - 14, _HEIGHT / 2 - 7, _gameboard, column);
        }

        //
        //Displays the main menu to the screen
        //
        public void DisplayMainMenu()
        {
            List<string> options = new List<string> { "Connect 4: Main Menu", " ", "1) Start Game", "2) Exit" };

            _consoleMenu.DrawMenu(25, 16, options);
        }

        public void DisplayExitMessage()
        {
            string endm = "Thanks for playing!";

            _consoleMenu.DrawTextBox(endm, true);
        }

        public void DisplayWinner(Gameboard.PlayerColor winner)
        {
            string message = "";

            switch (winner)
            {
                case Gameboard.PlayerColor.None:
                    //draw game message
                    message = "Tie Game!";
                    break;
                case Gameboard.PlayerColor.Red:
                    //player one wins message
                    message = "Player One Wins!";
                    break;
                case Gameboard.PlayerColor.Blue:
                    //player two wins message
                    message = "Player Two Wins!";
                    break;
            }
            message += " Press any key to continue.";
            _consoleMenu.DrawTextBox(message, true);
            Console.ReadKey();
        }

        /// <summary>
        /// Prompt for a char
        /// </summary>
        /// <returns>char</returns>
        public char PromptChar()
        {
            string input;
            char result;

            while (true)
            {
                input = Char.ToString(Console.ReadKey(true).KeyChar);

                if (char.TryParse(input, out result)) break;
            }

            return result;
        }
    }
}
