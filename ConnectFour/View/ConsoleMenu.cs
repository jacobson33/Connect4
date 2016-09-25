using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectFour
{
    public class ConsoleMenu
    {
        #region FIELD

        private string _name;
        private int _WIDTH;
        private int _HEIGHT;
        private ConsoleColor _foreColor;
        private ConsoleColor _backColor;

        #endregion

        #region PROPERTY

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public int WIDTH
        {
            get { return _WIDTH; }
            set { _WIDTH = value; }
        }

        public int HEIGHT
        {
            get { return _HEIGHT; }
            set { _HEIGHT = value; }
        }

        public ConsoleColor ForegroundColor
        {
            get { return _foreColor; }
            set { _foreColor = value; }
        }

        public ConsoleColor BackgroundColor
        {
            get { return _backColor; }
            set { _backColor = value; }
        }

        #endregion

        #region CONSTRUCTOR

        public ConsoleMenu()
        {
            Console.CursorVisible = false;
            Console.ForegroundColor = ConsoleColor.White;
            _foreColor = ConsoleColor.White;
            _backColor = ConsoleColor.Black;
        }

        public ConsoleMenu(int w, int h) : this()
        {
            _WIDTH = w;
            _HEIGHT = h;
        }

        #endregion

        #region DRAW

        /// <summary>
        /// Draw a single character
        /// </summary>
        /// <param name="x">x position</param>
        /// <param name="y">y position</param>
        /// <param name="ct">char</param>
        /// <param name="fColor"></param>
        /// <param name="bColor"></param>
        public void DrawChar(int x, int y, char ct, ConsoleColor fColor, ConsoleColor bColor)
        {
            ChangeColors(fColor, bColor);

            WriteAt(x, y, ct);

            ChangeColors(_foreColor, _backColor);
        }

        /// <summary>
        /// Draw Lines
        /// </summary>
        public void DrawLine(int x, int y, int l, bool dir, char c)
        {
            Console.SetCursorPosition(x, y);

            l--;

            if (dir) //Horizontal
                for (int i = 0; i < l; i++)
                    WriteAt(x + i, y, c);
            else    //Vertical
                for (int i = 0; i < l; i++)
                    WriteAt(x, y + i, c);
        }

        /// <summary>
        /// Draw a rectangle
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="w"></param>
        /// <param name="h"></param>
        public void DrawRectangle(int x, int y, int w, int h)
        {
            w--;
            h--;

            //Making the corners
            WriteAt(x, y, '╔');
            WriteAt(x + w, y, '╗');
            WriteAt(x, y + h, '╚');
            WriteAt(x + w, y + h, '╝');

            //Make the sides
            DrawLine(x + 1, y, w, true, '═');
            DrawLine(x + 1, y + h, w, true, '═');
            DrawLine(x, y + 1, h, false, '║');
            DrawLine(x + w, y + 1, h, false, '║');
        }

        /// <summary>
        /// Overload of DrawRectangle
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="w"></param>
        /// <param name="h"></param>
        /// <param name="fColor"></param>
        /// <param name="bColor"></param>
        public void DrawRectangle(int x, int y, int w, int h, ConsoleColor fColor, ConsoleColor bColor)
        {
            ChangeColors(fColor, bColor);

            DrawRectangle(x, y, w, h);

            ChangeColors(_foreColor, _backColor);
        }

        //public void DrawGrid(int x, int y, int w, int h, int rowNum, int colNum)
        //{
        //    List<char> walls = new List<char>() { '█', '═', '║', '╩', '╦', '╠', '╣', '╔', '╗', '╚', '╝', '╬' };

        //    DrawRectangle(x, y, w, h);

        //    int row_interval = h / rowNum;
        //    int col_interval = w / colNum;

        //    for (int i = h; i > 0; i -= row_interval) DrawLine(x+1, y+i, w-1, true, '═');
        //    for (int j = w; j > 0; j -= col_interval) DrawLine(x+j, y+1, h-1, false, '║');

        //    Console.ReadKey(true);
        //}

        public void DrawGrid(int x, int y)
        {
            string[] game_grid = new string[] { @"╔═══╦═══╦═══╦═══╦═══╦═══╦═══╗",
                                                @"║   ║   ║   ║   ║   ║   ║   ║",
                                                @"╠═══╬═══╬═══╬═══╬═══╬═══╬═══╣",
                                                @"║   ║   ║   ║   ║   ║   ║   ║",
                                                @"╠═══╬═══╬═══╬═══╬═══╬═══╬═══╣",
                                                @"║   ║   ║   ║   ║   ║   ║   ║",
                                                @"╠═══╬═══╬═══╬═══╬═══╬═══╬═══╣",
                                                @"║   ║   ║   ║   ║   ║   ║   ║",
                                                @"╠═══╬═══╬═══╬═══╬═══╬═══╬═══╣",
                                                @"║   ║   ║   ║   ║   ║   ║   ║",
                                                @"╠═══╬═══╬═══╬═══╬═══╬═══╬═══╣",
                                                @"║   ║   ║   ║   ║   ║   ║   ║",
                                                @"╚═══╩═══╩═══╩═══╩═══╩═══╩═══╝"};
            int i = 0;
            foreach (string s in game_grid)
            {
                WriteAt(x, y + i, s);
                i++;
            }
        }

        public void DrawPlayerPieces()
        {
            char c = '█';


        }

        /// <summary>
        /// Draw a menu from a list
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="w"></param>
        /// <param name="h"></param>
        /// <param name="menu"></param>
        public void DrawMenu(int x, int y, int w, int h, List<string> menu)
        {
            Console.Clear();
            int i = 2;

            DrawRectangle(x, y, w, h);

            foreach (string s in menu)
            {
                Console.SetCursorPosition(x + 2, y + i);
                Console.Write(s);

                i += 2;
            }
        }

        public void DrawMenu(int w, int h, List<string> menu)
        {
            Console.Clear();
            int i = 2;

            int x = _WIDTH / 2 - 7;
            int y = _HEIGHT / 2 - 8;

            DrawRectangle(x, y, w, h);

            foreach (string s in menu)
            {
                Console.SetCursorPosition(x + 2, y + i);
                Console.Write(s);

                i += 2;
            }
        }

        /// <summary>
        /// Overload of DrawMenu
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="w"></param>
        /// <param name="h"></param>
        /// <param name="fColor"></param>
        /// <param name="bColor"></param>
        /// <param name="menu"></param>
        public void DrawMenu(int x, int y, int w, int h, ConsoleColor fColor, ConsoleColor bColor, List<string> menu)
        {
            ChangeColors(fColor, bColor);

            DrawMenu(x, y, w, h, menu);

            ChangeColors(_foreColor, _backColor);
        }

        /// <summary>
        /// Draw a single ligned textbox
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="w"></param>
        /// <param name="h"></param>
        /// <param name="s"></param>
        public void DrawTextBox(int x, int y, int w, int h, string s)
        {
            DrawRectangle(x, y, w, h);

            Console.SetCursorPosition(x + w / 8, y + h / 2);
            Console.Write(s);
        }

        /// <summary>
        /// Simpler DrawTextBox override
        /// </summary>
        /// <param name="s"></param>
        public void DrawTextBox(string s)
        {
            int w = s.Length + 8;
            int h = 5;
            int x = _WIDTH / 2 - w / 2;
            int y = _HEIGHT / 2 - h / 2;

            DrawTextBox(x, y, w, h, s);
        }

        /// <summary>
        /// DrawTextBox override that can clear the screen
        /// </summary>
        /// <param name="s"></param>
        /// <param name="v"></param>
        public void DrawTextBox(string s, bool v)
        {
            if (v) Console.Clear();

            DrawTextBox(s);
        }

        /// <summary>
        /// Draw a specialized text box
        /// </summary>
        public void DrawPromptBox(string s)
        {
            Console.Clear();

            int w = s.Length + 21;
            int h = 7;
            int x = _WIDTH / 2 - w / 2;
            int y = _HEIGHT / 2 - h / 2;

            DrawRectangle(x, y, w, h);

            Console.SetCursorPosition(x + w / 8, y + 2);
            Console.Write(s);
            Console.SetCursorPosition(x + w / 8, y + 4);
            Console.CursorVisible = true;
        }

        #endregion

        #region OTHER

        /// <summary>
        /// Change Front and Back color of console
        /// </summary>
        /// <param name="fColor"></param>
        /// <param name="bColor"></param>
        public void ChangeColors(ConsoleColor fColor, ConsoleColor bColor)
        {
            Console.ForegroundColor = fColor;
            Console.BackgroundColor = bColor;
        }

        /// <summary>
        /// Write Single Character
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="c"></param>
        public void WriteAt(int x, int y, char c)
        {
            Console.SetCursorPosition(x, y);
            Console.Write(c);
        }

        /// <summary>
        /// Write Full sentence
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="s"></param>
        public void WriteAt(int x, int y, string s)
        {
            Console.SetCursorPosition(x, y);
            Console.Write(@s);
        }

        /// <summary>
        /// Makes the first letter Upper, while making the other lower
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public string Title(string s)
        {
            s = s.ToLower();

            char[] a = s.ToCharArray();
            a[0] = char.ToUpper(a[0]);

            return new string(a);
        }
        #endregion
    }
}
