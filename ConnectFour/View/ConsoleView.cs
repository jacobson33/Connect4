﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectFour
{
    public class ConsoleView
    {
        #region ENUM

        public enum ViewState { None, Active }

        #endregion

        #region FIELD

        private const int GAMEBOARD_VERTICAL_LOCATION = 4;
        private const int GAMEBOARD_VERTICAL_CURSOR = 3;

        private ConsoleMenu _consoleMenu;

        private Gameboard _gameboard;
        private ViewState _currentViewState;

        private int _WIDTH = 120;
        private int _HEIGHT = 40;

        //Grid Variables
        private int _gridRowNum;
        private int _gridColNum;
        private int _gridCellWidth;
        private int _gridCellHeight;
        private int _gridX;
        private int _gridY;

        #endregion

        #region PROPERTY

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

        #endregion

        #region CONSTRUCTOR

        public void InitializeConsole()
        {
            Console.Title = "Connect 4";
            Console.WindowWidth = _WIDTH;
            Console.WindowHeight = _HEIGHT;
            Console.BufferWidth = _WIDTH;
            Console.BufferHeight = _HEIGHT;

            _consoleMenu = new ConsoleMenu(_WIDTH, _HEIGHT);

            _gridRowNum = 6;
            _gridColNum = 7;
            _gridCellWidth = 5;
            _gridCellHeight = 3;
            _gridX = _WIDTH / 2 - ((_gridColNum * _gridCellWidth) + _gridColNum + 1) / 2;
            _gridY = _HEIGHT / 2 - ((_gridRowNum * _gridCellHeight) + _gridRowNum + 1) / 2;
        }

        #endregion

        #region METHOD

        public GameboardPosition GetPlayerPositionChoice()
        {
            //
            // Initialize gameboardPosition with -1 values
            //
            GameboardPosition gameboardPosition = new GameboardPosition(-1, -1);

            return gameboardPosition;

        }

        /// <summary>
        /// Draws the game board
        /// </summary>
        /// <param name="_gameboard"></param>
        /// <param name="column"></param>
        public void DisplayGameArea(Gameboard _gameboard, int column = 0)
        {
            Console.Clear();

            _consoleMenu.DrawGrid(_gridX, _gridY, _gridRowNum, _gridColNum, _gridCellWidth, _gridCellHeight);

            _consoleMenu.DrawPlayerPieces(_gridX, _gridY, _gridCellWidth, _gridCellHeight, _gameboard);
            _consoleMenu.DrawCursor(_gridX, _gridY, _gridCellWidth, _gridCellHeight, column, _gameboard);
        }

        /// <summary>
        /// Update the game board
        /// </summary>
        /// <param name="_gameboard"></param>
        /// <param name="column"></param>
        public void UpdateGameArea(Gameboard _gameboard, int column = 0)
        {
            _consoleMenu.DrawPlayerPieces(_gridX, _gridY, _gridCellWidth, _gridCellHeight, _gameboard);
            _consoleMenu.DrawCursor(_gridX, _gridY, _gridCellWidth, _gridCellHeight, column, _gameboard);
        }

        /// <summary>
        /// Displays the main menu to the screen
        /// </summary>
        public void DisplayMainMenu(bool error, string errorMessage)
        {
            List<string> options = new List<string> { "Connect 4: Main Menu", " ", "1) New Game", "2) Load Game", "3) Exit" };

            _consoleMenu.DrawMenu(25, 16, options);

            if (error) DisplayErrorMessage(errorMessage);
        }

        /// <summary>
        /// Displays in-game menu
        /// </summary>
        public void DisplaySubMenu()
        {
            List<string> options = new List<string> { "Options", " ", "1) Main Menu", "2) Save Game", "3) Back" };

            _consoleMenu.DrawMenu(25, 16, options);
        }

        /// <summary>
        /// Displays the Rule when the game is started
        /// </summary>
        public void DisplayRules()
        {
            _consoleMenu.DrawRectangle(1, 1, _WIDTH - 2, _HEIGHT - 2);

            _consoleMenu.WriteAt(_WIDTH / 2 - 5, 1, " Connect 4 ");

            _consoleMenu.DrawTextBox(15, 10, 10, 5, "  ESC");

            _consoleMenu.DrawTextBox(75, 20, 10, 5, "   <-");
            _consoleMenu.DrawTextBox(90, 20, 10, 5, "   ->");

            _consoleMenu.DrawTextBox(15, 20, 50, 5, "\t\t      SPACE");

            _consoleMenu.WriteAt(15, 30, "Connect 4 of your pieces to win the game!");

            _consoleMenu.WriteAt(_WIDTH - 32, _HEIGHT - 3, "Press Any Key To Continue -->");

            Console.ReadKey(true);
        }

        /// <summary>
        /// Displays an exit message
        /// </summary>
        public void DisplayExitMessage()
        {
            string endm = "Thanks for playing!";

            _consoleMenu.DrawTextBox(endm, true);
        }

        /// <summary>
        /// Displays an Error Message to the main menu screen
        /// </summary>
        public void DisplayErrorMessage()
        {
            string message = "Loading Failed";
            int x = 48 - (message.Length / 3);
            _consoleMenu.DrawTextBox(x, 30, message);
        }

        /// <summary>
        /// Displays an Error with custom message
        /// </summary>
        public void DisplayErrorMessage(string message)
        {
            int x = 48 - (message.Length / 3);

            //shorten long messages
            message = message.Length > 80 ? message.Substring(0, 75) + "..." : message;
            x = message.Length > 80 ? x - 5 : x;
            
            //draw error message
            _consoleMenu.DrawTextBox(x, 30, message);
        }

        /// <summary>
        /// Displays who the winner is
        /// </summary>
        /// <param name="winner"></param>
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
            Console.ReadKey(true);
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

        #endregion
    }
}
