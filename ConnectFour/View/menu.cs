#region DRAW

        /// <summary>
        /// Draw a single character
        /// </summary>
        /// <param name="x">x position</param>
        /// <param name="y">y position</param>
        /// <param name="ct">char</param>
        /// <param name="fColor"></param>
        /// <param name="bColor"></param>
        protected void DrawChar(int x, int y, char ct, ConsoleColor fColor, ConsoleColor bColor)
        {
            ChangeColors(fColor, bColor);

            WriteAt(x, y, ct);

            ChangeColors(_foreColor, _backColor);
        }

        /// <summary>
        /// Draw Lines
        /// </summary>
        protected void DrawLine(int x, int y, int l, bool dir, char c)
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
        protected void DrawRectangle(int x, int y, int w, int h)
        {
            w--;
            h--;

            //Making the corners
            WriteAt(x, y, '/');
            WriteAt(x + w, y, '\\');
            WriteAt(x, y + h, '\\');
            WriteAt(x + w, y + h, '/');

            //Make the sides
            DrawLine(x + 1, y, w, true, '-');
            DrawLine(x + 1, y + h, w, true, '-');
            DrawLine(x, y + 1, h, false, '|');
            DrawLine(x + w, y + 1, h, false, '|');
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
        protected void DrawRectangle(int x, int y, int w, int h, ConsoleColor fColor, ConsoleColor bColor)
        {
            ChangeColors(fColor, bColor);

            DrawRectangle(x, y, w, h);

            ChangeColors(_foreColor, _backColor);
        }
        
        /// <summary>
        /// Draw a menu from a list
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="w"></param>
        /// <param name="h"></param>
        /// <param name="menu"></param>
        protected void DrawMenu(int x, int y, int w, int h, List<string> menu)
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
        protected void DrawMenu(int x, int y, int w, int h, ConsoleColor fColor, ConsoleColor bColor, List<string> menu)
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
        protected void DrawTextBox(int x, int y, int w, int h, string s)
        {
            DrawRectangle(x, y, w, h);

            Console.SetCursorPosition(x + w / 8, y + h / 2);
            Console.Write(s);
        }

        /// <summary>
        /// Simpler DrawTextBox override
        /// </summary>
        /// <param name="s"></param>
        protected void DrawTextBox(string s)
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
        protected void DrawTextBox(string s, bool v)
        {
            if (v) Console.Clear();

            DrawTextBox(s);
        }

        /// <summary>
        /// Draw a specialized text box
        /// </summary>
        protected void DrawPromptBox(string s)
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