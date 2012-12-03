using System;
using System.Text;

namespace ConsoleToolBox.Containers
{
    /// <summary>
    /// The Panel is a low level object. It provides a two dimensional area to read and write characters to. You can set the 
    /// position and selectively choose to show or hide the contents of the panel on the screen. It also automatically saves
    /// the content of the screen that it overwrites so that when it is hidden it will places the original content of the 
    /// screen back. 
    /// 
    /// Known Issues:
    ///     1) Overlapping panels will have major drawing issues (need to come up with some way of doing Z index tracking)
    ///     2) ????
    /// 
    /// </summary>
    public class Panel
    {
        #region Properties & Constructor

        /// <summary>
        /// Gets or sets a value indicating whether the control will refresh the screen upon changes.
        /// The default value is <c>true</c>
        /// </summary>
        /// <value>
        ///   <c>true</c> if [auto refresh]; otherwise, <c>false</c>.
        /// </value>
        public bool AutoRefresh { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether the control will scroll rows up when the cursor reaches the last row.
        /// The default value is <c>true</c>. If set to <c>false</c>, once the cursor has reached the bottom right corner
        /// of the control it will stop writing text to the control until the cursor position is moved.
        /// </summary>
        /// <value>
        ///   <c>true</c> if [auto scroll]; otherwise, <c>false</c>.
        /// </value>
        public bool AutoScroll { get; set; }
        /// <summary>
        /// Gets or sets the color of the background with the Write() and WriteLine() functions.
        /// </summary>
        /// <value>
        /// The color of the background.
        /// </value>
        public ushort BackgroundColor { get; set; }
        /// <summary>
        /// Gets or set the column position of the cursor within the control area.
        /// </summary>
        /// <value>
        /// The cursor left.
        /// </value>
        public int CursorLeft { get; set; }
        /// <summary>
        /// Gets or set the row position of the cursor within the control area.
        /// </summary>
        /// <value>
        /// The cursor top.
        /// </value>
        public int CursorTop { get; set; }
        /// <summary>
        /// Gets or sets the color of the foreground with the Write() and WriteLine() functions.
        /// </summary>
        /// <value>
        /// The color of the foreground.
        /// </value>
        public ushort ForegroundColor { get; set; }
        /// <summary>
        /// Gets or sets the height, in characters, of the control.
        /// </summary>
        /// <value></value>
        public int Height
        {
            get { return _height; }
            set { SetPanelSize(value, _width); }
        }
        /// <summary>
        /// Gets or set the distance, in characters, between the left edge of the control and the left edge of the screen.
        /// </summary>
        /// <value></value>
        public int Left
        {
            get { return _positionLeft; }
            set { MovePanel(value, _positionTop); }
        }
        /// <summary>
        /// Gets or set the distance, in characters, between the top edge of the control and the top edge of the screen.
        /// </summary>
        /// <value></value>
        public int Top
        {
            get { return _positionTop; }
            set { MovePanel(_positionLeft, value); }
        }
        /// <summary>
        /// Gets or sets the width, in characters, of the control.
        /// </summary>
        /// <value></value>
        public int Width
        {
            get { return _width; }
            set { SetPanelSize(_height, value); }
        }
        /// <summary>
        /// Gets or sets whether this control is visible.
        /// </summary>
        /// <value>
        ///   <c>true</c> if visible; otherwise, <c>false</c>.
        /// </value>
        public bool Visible
        {
            get { return _visible; }
            set { _visible = value; DrawBufferOnScreen(_visible ? _panelContentBuffer : _originalScreenContentBuffer); }
        }

        private int _height;
        private CharEx[,] _originalScreenContentBuffer;
        private CharEx[,] _panelContentBuffer;
        private int _positionLeft;
        private int _positionTop;
        private int _width;
        private bool _visible;

        /// <summary>
        /// Initializes a new instance of the <see cref="Panel"/> class.
        /// </summary>
        public Panel(int positionLeft, int positionTop, int width, int height)
        {
            SetupPanel(positionLeft,positionTop,width,height, (ushort)Console.ForegroundColor, (ushort)Console.BackgroundColor, false);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Panel"/> class.
        /// </summary>
        public Panel(int positionLeft, int positionTop, int width, int height, ushort foregroundColor, ushort backgroundColor)
        {
            SetupPanel(positionLeft, positionTop, width, height, foregroundColor, backgroundColor, false);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Panel"/> class.
        /// </summary>
        public Panel(int positionLeft, int positionTop, int width, int height, bool copyConsole)
        {
            SetupPanel(positionLeft, positionTop, width, height, (ushort)Console.ForegroundColor, (ushort)Console.BackgroundColor, copyConsole);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Panel"/> class.
        /// </summary>
        public Panel(int positionLeft, int positionTop, int width, int height, ushort foregroundColor, ushort backgroundColor, bool copyConsole)
        {
            SetupPanel(positionLeft, positionTop, width, height, foregroundColor, backgroundColor, copyConsole);
        }

        /// <summary>
        /// Sets up the panel.
        /// </summary>
        /// <param name="positionLeft">The position left.</param>
        /// <param name="positionTop">The position top.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <param name="copyConsole">Indicate to have panel copy current contents of screen to panel buffer</param>
        private void SetupPanel(int positionLeft, int positionTop, int width, int height, ushort foregroundColor, ushort backgroundColor, bool copyConsole)
        {
            if (positionLeft < 0 || positionLeft > Console.WindowWidth)
                throw new ArgumentOutOfRangeException("positionLeft");

            _positionLeft = positionLeft;

            if (positionTop < 0 || positionTop > Console.WindowHeight)
                throw new ArgumentOutOfRangeException("positionTop");

            _positionTop = positionTop;

            if (width <= 0)
                throw new ArgumentOutOfRangeException("width");

            _width = width;

            if (height <= 0)
                throw new ArgumentOutOfRangeException("height");

            _height = height;

            ForegroundColor = foregroundColor;
            BackgroundColor = backgroundColor;

            _originalScreenContentBuffer = GetBufferFromScreen(_positionLeft, _positionTop, _width, _height);

            _panelContentBuffer = copyConsole ? _originalScreenContentBuffer : GetEmptyBuffer(_height, _width);

            AutoRefresh = true;
            AutoScroll = true;

        }

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents the text content of the control.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder((_height * _width));

            for (int x = 0; x < _height; x++)
            {
                for (int y = 0; y < _width; y++)
                {
                    stringBuilder.Append(_panelContentBuffer[x, y].AsciiChar);
                }
            }
            return stringBuilder.ToString();
        }

        #endregion

        #region Public Functions

        /// <summary>
        /// Clears the contents of the panel.
        /// </summary>
        public void Clear()
        {
            _panelContentBuffer = GetEmptyBuffer(_height, _width);
            CursorLeft = 0;
            CursorTop = 0;
            Refresh();
        }

        /// <summary>
        /// Copies the current screen content to the panel.
        /// </summary>
        public void CopyScreenToPanel()
        {
            if (!_visible)
                _originalScreenContentBuffer = GetBufferFromScreen(_positionLeft, _positionTop, _width, _height);
            
            _panelContentBuffer = _originalScreenContentBuffer;

            Refresh();
        }

        /// <summary>
        /// Moves the panel.
        /// </summary>
        /// <param name="positionLeft">The position left.</param>
        /// <param name="positionTop">The position top.</param>
        public void MovePanel(int positionLeft, int positionTop)
        {
            if(_visible)
                DrawBufferOnScreen(_originalScreenContentBuffer);

            _positionLeft = positionLeft;
            _positionTop = positionTop;

            _originalScreenContentBuffer = GetBufferFromScreen(_positionLeft, _positionTop, _width, _height);

            if (AutoRefresh)
                Refresh();
        }

        /// <summary>
        /// Returns a single Char value for the contents of the specified panel location.
        /// </summary>
        /// <param name="positionLeft">The position left.</param>
        /// <param name="positionTop">The position top.</param>
        /// <returns>Char</returns>
        public Char PeekChar(int positionLeft, int positionTop)
        {
            return _panelContentBuffer[positionLeft, positionTop].AsciiChar;
        }

        /// <summary>
        /// Returns a single CharEx value for the contents of the specified panel location.
        /// </summary>
        /// <param name="positionLeft">The position left.</param>
        /// <param name="positionTop">The position top.</param>
        /// <returns>CharEx</returns>
        public CharEx PeekCharEx(int positionLeft, int positionTop)
        {
            return _panelContentBuffer[positionLeft, positionTop];
        }

        /// <summary>
        /// Places a single Char value into the specified panel location.
        /// </summary>
        /// <param name="positionLeft">The position left.</param>
        /// <param name="positionTop">The position top.</param>
        /// <param name="value">The Char value to be written.</param>
        public void PokeChar(int positionLeft, int positionTop, Char value)
        {
            _panelContentBuffer[positionLeft, positionTop].AsciiChar = value;
            if (AutoRefresh)
                Refresh();
        }

        /// <summary>
        /// Places a single CharEx value into the specified panel location.
        /// </summary>
        /// <param name="positionLeft">The position left.</param>
        /// <param name="positionTop">The position top.</param>
        /// <param name="value">The CharEx value to be written.</param>
        public void PokeCharEx(int positionLeft, int positionTop, CharEx value)
        {
            _panelContentBuffer[positionLeft, positionTop] = value;
            if (AutoRefresh)
                Refresh();
        }

        /// <summary>
        /// Causes the panel to redraw itself if it is currently visible.
        /// </summary>
        public void Refresh()
        {
            if (_visible)
                DrawBufferOnScreen(_panelContentBuffer);
        }

        /// <summary>
        /// Sets the background color of all characters on the panel.
        /// </summary>
        /// <param name="color">The color.</param>
        public void SetPanelBackgroundColor(ushort color)
        {
            for (int x = 0; x < _height; x++)
            {
                for (int y = 0; y < _width; y++)
                {
                    _panelContentBuffer[x, y].BackgroundColor = color;
                }
            }
            if (AutoRefresh)
                Refresh();
        }

        /// <summary>
        /// Sets the foreground color of all characters on the panel.
        /// </summary>
        /// <param name="color">The color.</param>
        public void SetPanelForegroundColor(ushort color)
        {
            for (int x = 0; x < _height; x++)
            {
                for (int y = 0; y < _width; y++)
                {
                    _panelContentBuffer[x, y].ForegroundColor = color;
                }
            }
            if (AutoRefresh)
                Refresh();
        }

        /// <summary>
        /// Sets the size of the panel.
        /// </summary>
        /// <param name="height">The height.</param>
        /// <param name="width">The width.</param>
        public void SetPanelSize(int height, int width)
        {
            //throw new NotImplementedException();

            if (height == _height && width == _width)
                return;

            CharEx[,] pb = GetEmptyBuffer(height, width);

            for (int x = 0; x < _height; x++)
            {
                for (int y = 0; y < _width; y++)
                {
                    pb[x, y] = _panelContentBuffer[x, y];
                }
            }
            _panelContentBuffer = pb;

            if (_visible)
                DrawBufferOnScreen(_originalScreenContentBuffer);

            _originalScreenContentBuffer = GetBufferFromScreen(_positionLeft, _positionTop, width, height);

            _height = height;
            _width = width;
            
            if(_visible)
                DrawBufferOnScreen(_panelContentBuffer);
        }

        /// <summary>
        /// Writes the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        public void Write(string value)
        {
            while(value.Length > 0)
            {
                char c = value.ToCharArray(0, 1)[0];
                value = value.Remove(0, 1);

                switch(c)
                {
                    case '\r':
                        CursorLeft = 0;
                        break;
                    case '\n':
                        CursorLeft = 0;
                        CursorTop++;
                        break;
                    default:
                        _panelContentBuffer[CursorTop, CursorLeft] = new CharEx { AsciiChar = c, ForegroundColor = ForegroundColor, BackgroundColor = BackgroundColor };
                        CursorLeft++;
                        break;
                }
                
                if(CursorLeft >= _width)
                {
                    CursorLeft = 0;
                    CursorTop++;
                }
                if (CursorTop >= _height)
                {
                    CursorTop = _height - 1;
                    if (AutoScroll)
                    {
                        if (_height > 1)
                        {
                            for (int x = 0; x < _height - 1; x++)
                            {
                                for (int y = 0; y < _width; y++)
                                {
                                    _panelContentBuffer[x, y] = _panelContentBuffer[x + 1, y];
                                }
                            }
                        }
                        for (int y = 0; y < _width; y++)
                        {
                            _panelContentBuffer[_height - 1, y] = new CharEx() { AsciiChar = ' ', BackgroundColor = BackgroundColor, ForegroundColor = ForegroundColor };
                        }
                    }
                    else
                    {
                        value = string.Empty;
                    }
                }
            }
            if(AutoRefresh)
                Refresh();
        }

        /// <summary>
        /// Writes the line to the panel.
        /// </summary>
        /// <param name="value">The value.</param>
        public void WriteLine(string value)
        {
            Write(value + Environment.NewLine);
        }

       
        #endregion

        #region Private Functions
        /// <summary>
        /// Gets an empty buffer.
        /// </summary>
        /// <param name="height">The height.</param>
        /// <param name="width">The width.</param>
        /// <returns></returns>
        private CharEx[,] GetEmptyBuffer(int height, int width)
        {
            CharEx[,] array = new CharEx[height,width];
            for (int x = 0; x < height; x++)
            {
                for (int y = 0; y < width; y++)
                {
                    array[x, y] = new CharEx { AsciiChar = ' ', ForegroundColor = ForegroundColor, BackgroundColor = BackgroundColor };
                }
            }
            return array;
        }

        /// <summary>
        /// Gets the buffer from screen.
        /// </summary>
        /// <param name="positionLeft">The position left.</param>
        /// <param name="positionTop">The position top.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <returns></returns>
        private CharEx[,] GetBufferFromScreen(int positionLeft, int positionTop, int width, int height)
        {   
            // create a new buffer and load the contents of the screen onto it
            CharEx[,] array = new CharEx[height, width];

            for (int x = 0; x < height; x++)
            {
                for (int y = 0; y < width; y++)
                {
                    array[x, y] = ConsoleToolBox.PeekCharEx(positionLeft + y, positionTop + x);
                }
            }
            return array;
        }

        /// <summary>
        /// Draws the buffer on screen.
        /// </summary>
        /// <param name="buffer">The buffer.</param>
        private void DrawBufferOnScreen(CharEx[,] buffer)
        {
            // loop through buffer and drop characters on the screen
            for (int x = 0; x < _height; x++)
            {
                for (int y = 0; y < _width; y++)
                {
                    ConsoleToolBox.PokeCharEx(_positionLeft+y,_positionTop+x,buffer[x,y]);
                }
            }
        }

        #endregion

    }
}
/*
** Properties **
[X] - AutoRefresh
[X] - AutoScroll
[X] - BackgroundColor
[X] - ForegroundColor
[X] - CursorLeft
[X] - CursorTop
[X] - PositionLeft
[X] - PositionTop
[X] - Height
[X] - Width
[X] - Visible

** Methods **
[X] - Clear()
[X] - CopyScreenToPanel()
[ ] - MovePanel(left,top)
[X] - PeekChar(left,top)
[X] - PeekCharEx(left,top)
[X] - PokeChar(left,top,char)
[X] - PokeCharEx(left,top,charex)
[X] - Refresh()
[X] - SetPanelSize(height, width)
[X] - SetPanelBackgroundColor(color)
[ ] - Write(string)
[X] - WriteLine(string)

** Events **
None

*/
