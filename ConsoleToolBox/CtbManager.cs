/*
Copyright (c) 2012 Alan Barber

Permission is hereby granted, free of charge, to any person obtaining a copy 
of this software and associated documentation files (the "Software"), to deal 
in the Software without restriction, including without limitation the rights 
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell 
copies of the Software, and to permit persons to whom the Software is 
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in 
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR 
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, 
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE 
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER 
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, 
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS 
IN THE SOFTWARE.
*/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Reflection;
using System.Text;
using ConsoleToolBox.Containers;

namespace ConsoleToolBox
{
    public sealed class CtbManager
    {
        #region Setup for Singleton Pattern
        private static readonly Lazy<CtbManager> instance = new Lazy<CtbManager>(() => new CtbManager());
        private CtbManager() {}
        public static CtbManager Instance { get { return instance.Value; } }
        #endregion

        #region Public Variables

        /// <summary>
        /// Gets the height of the console.
        /// </summary>
        /// <value>
        /// The height of the console.
        /// </value>
        public int ConsoleHeight
        {
            get { return _consoleHeight; }
            //set { _consoleHeight = value; }
        }

        /// <summary>
        /// Gets the width of the console.
        /// </summary>
        /// <value>
        /// The width of the console.
        /// </value>
        public int ConsoleWidth
        {
            get { return _consoleWidth; }
            //set { _consoleWidth = value; }
        }

        /// <summary>
        /// Gets or sets the console title.
        /// </summary>
        /// <value>
        /// The console title.
        /// </value>
        public string ConsoleTitle
        {
            get { return _consoleTitle; }
            set { _consoleTitle = value; }
        }

        public List<Panel> Panels
        {
            get { return _panels; }
            //set { }
        } 

        #endregion

        #region Private Variables

        private static readonly log4net.ILog _log =
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);


        private Cursor _cursor = null;
        private CharEx[,] _frameBuffer = null;
        private List<Panel> _panels = null;
        private int _consoleHeight;
        private int _consoleWidth;
        private string _consoleTitle;
        private bool _frameBufferIsDirty;
        private bool _disableStandardOutput = true;

        private int _originalWindowHeight;
        private int _originalWindowWidth;
        private int _originalBufferHeight;
        private int _originalBufferWidth;
        private string _originalConsoleTitle;
        private TextWriter _originalConsoleOut;

        #endregion

        #region Public Functions

        /// <summary>
        /// Returns the version number of the ConsoleToolBox library.
        /// </summary>
        /// <returns></returns>
        public string GetVersion()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            return assembly.GetName().Version.ToString();
        }

        /// <summary>
        /// Initializes the ConsoleToolBox Manager with default of current console.
        /// </summary>
        public void Initialize()
        {
            Initialize(Console.WindowHeight,Console.WindowWidth);
        }

        /// <summary>
        /// Initializes the specified height.
        /// </summary>
        /// <param name="height">The height.</param>
        /// <param name="width">The width.</param>
        public void Initialize(int height, int width)
        {
            if(_log.IsDebugEnabled)
                _log.Debug(String.Format("Initialize({0},{1})",height,width));

            if (_log.IsInfoEnabled)
                _log.Info(string.Format("Current console settings: Console.WindowHeight={0}, Console.WindowWidth={1}, Console.BufferHeight={2}, Console.BufferWidth={3}, Console.LargestWindowHeight={4}, Console.LargestWindowWidth={5}",
                    Console.WindowHeight, Console.WindowWidth, Console.BufferHeight, Console.BufferWidth, Console.LargestWindowHeight, Console.LargestWindowWidth));
            
            // Validate height/width are within valid range and set
            if(height <= 0 || height > Console.LargestWindowHeight)
                throw new ArgumentOutOfRangeException("height");

            if(width <= 0 || width > Console.LargestWindowWidth)
                throw new ArgumentOutOfRangeException("width");

            _consoleHeight = height;
            _consoleWidth = width;

            // Store original values
            _originalWindowHeight = Console.WindowHeight;
            _originalWindowWidth = Console.WindowWidth;
            _originalBufferHeight = Console.BufferHeight;
            _originalBufferWidth = Console.BufferWidth;
            _originalConsoleTitle = GetConsoleTitle();
            _consoleTitle = GetConsoleTitle();
            _originalConsoleOut = Console.Out;

            // Trap standard output
            if(_disableStandardOutput)
                Console.SetOut(new ConsoleOutputTrapper(Console.Out));

            // Set the Console to the specified height/width
            Console.WindowHeight = height;
            Console.WindowWidth = width;
            Console.BufferHeight = height;
            Console.BufferWidth = width;

            // Create the correct size frameBuffer for the ConsoleToolBox to use
            _frameBuffer = new CharEx[height,width];

            // Setup Cursor
            _cursor = new Cursor();

            // setup Panel list
            _panels = new List<Panel>();
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Cleanup()
        {
            if (_log.IsDebugEnabled)
                _log.Debug("Cleanup()");

            _frameBuffer = null;

            SetConsoleTitle(_originalConsoleTitle);
            Console.WindowHeight = _originalWindowHeight;
            Console.WindowWidth = _originalWindowWidth;
            Console.BufferHeight = _originalBufferHeight;
            Console.BufferWidth = _originalBufferWidth;
            Console.SetOut(_originalConsoleOut);
            Console.Clear();
        }


        /// <summary>
        /// Refreshes the screen and draws the current contents of the screen buffer.
        /// </summary>
        public void Refresh()
        {
            if(_log.IsDebugEnabled)
                _log.Debug("Refresh()");

            if(_log.IsInfoEnabled)
                _log.Info(string.Format("setting console title to \"{0}\"",_consoleTitle));

            SetConsoleTitle(_consoleTitle);

            // Draw buffer to screen
            for (int x = 0; x < _consoleHeight; x++)
            {
                for (int y = 0; y < _consoleWidth; y++)
                {
                    PokeScreenCharEx(y,x,_frameBuffer[x,y]);
                }
            }

            _frameBufferIsDirty = false;

        }

        public void Write(string value)
        {
            throw new NotImplementedException();
        }

        public void WriteLine(string value)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Private Functions

        /// <summary>
        /// Gets the console window current title.
        /// </summary>
        /// <returns></returns>
        private string GetConsoleTitle()
        {
            uint nSize = 1024;
            StringBuilder lpConsoleTitle = new StringBuilder((int)nSize);

            if (Kernel32.GetConsoleTitle(lpConsoleTitle, nSize) <= 0)
                throw new Win32Exception();

            return lpConsoleTitle.ToString();
        }

        /// <summary>
        /// Gets the console window original title when the console was first created.
        /// </summary>
        /// <returns></returns>
        private string GetConsoleOriginalTitle()
        {
            uint nSize = 1024;
            StringBuilder lpConsoleTitle = new StringBuilder((int)nSize);

            if (Kernel32.GetConsoleOriginalTitle(lpConsoleTitle, nSize) <= 0)
                throw new Win32Exception();

            return lpConsoleTitle.ToString();
        }

        /// <summary>
        /// Sets the console window title.
        /// </summary>
        /// <param name="lpConsoleTitle">The title to be set.</param>
        private void SetConsoleTitle(string lpConsoleTitle)
        {
            if (!Kernel32.SetConsoleTitle(lpConsoleTitle))
                throw new Win32Exception();
        }

        #region Low Level Screen Character Functions

        /// <summary>
        /// Returns a single <see cref="Char"/> value that matches the current character at the specified screen location.
        /// </summary>
        /// <param name="positionLeft">The position counting from the left (starts at position 0).</param>
        /// <param name="positionTop">The position counting from the top (starts at position 0).</param>
        /// <returns>Char</returns>
        private char PeekScreenChar(int positionLeft, int positionTop)
        {
            IntPtr stdout = Kernel32.GetStdHandle(Kernel32.StdHandle.Stdout);

            if (stdout == IntPtr.Zero)
                throw new Win32Exception();

            Kernel32.COORD dwReadCoord;
            dwReadCoord.X = (short)positionLeft;
            dwReadCoord.Y = (short)positionTop;

            uint nLength = 1;
            StringBuilder lpCharacter = new StringBuilder((int)nLength);

            uint lpNumberOfCharsRead = 1;

            if (!Kernel32.ReadConsoleOutputCharacter(stdout, lpCharacter, nLength, dwReadCoord, out lpNumberOfCharsRead))
                throw new Win32Exception();

            return lpCharacter[0];
        }

        /// <summary>
        /// Returns a single <see cref="CharEx"/> that matches the current character and attributes at the specified screen location.
        /// </summary>
        /// <param name="positionLeft">The position counting from the left (starts at position 0).</param>
        /// <param name="positionTop">The position counting from the top (starts at position 0).</param>
        /// <returns>CharEx</returns>
        private CharEx PeekScreenCharEx(int positionLeft, int positionTop)
        {
            IntPtr stdout = Kernel32.GetStdHandle(Kernel32.StdHandle.Stdout);

            if (stdout == IntPtr.Zero)
                throw new Win32Exception();

            Kernel32.COORD dwReadCoord;
            dwReadCoord.X = (short)positionLeft;
            dwReadCoord.Y = (short)positionTop;

            uint nLength = 1;
            ushort[] lpAttribute = new ushort[1];

            uint lpNumberOfAttrsRead = 1;

            if (!Kernel32.ReadConsoleOutputAttribute(stdout, lpAttribute, nLength, dwReadCoord, out lpNumberOfAttrsRead))
                throw new Win32Exception();

            char _asciiChar = PeekScreenChar(positionLeft, positionTop);

            return new CharEx { AsciiChar = _asciiChar, Attributes = lpAttribute[0] };
        }

        /// <summary>
        /// Places a single character value at the specified screen location.
        /// </summary>
        /// <param name="positionLeft">The position counting from the left (starts at position 0).</param>
        /// <param name="positionTop">The position counting from the top (starts at position 0).</param>
        /// <param name="value">The character to be written to the screen.</param>
        private void PokeScreenChar(int positionLeft, int positionTop, char value)
        {
            IntPtr stdout = Kernel32.GetStdHandle(Kernel32.StdHandle.Stdout);

            if (stdout == IntPtr.Zero)
                throw new Win32Exception();

            Kernel32.COORD dwWriteCoord;
            dwWriteCoord.X = (short)positionLeft;
            dwWriteCoord.Y = (short)positionTop;

            uint nLength = 1;
            string lpCharacter = value.ToString();
            uint lpNumberOfCharsWritten = 1;

            if (!Kernel32.WriteConsoleOutputCharacter(stdout, lpCharacter, nLength, dwWriteCoord, out lpNumberOfCharsWritten))
                throw new Win32Exception();
        }

        /// <summary>
        /// Places a single character value and set attributes (fg/bg colors) at the specified screen location.
        /// </summary>
        /// <param name="positionLeft">The position counting from the left (starts at position 0).</param>
        /// <param name="positionTop">The position counting from the top (starts at position 0).</param>
        /// <param name="value">The character and atributes to be written to the screen.</param>
        private void PokeScreenCharEx(int positionLeft, int positionTop, CharEx value)
        {
            IntPtr stdout = Kernel32.GetStdHandle(Kernel32.StdHandle.Stdout);

            if (stdout == IntPtr.Zero)
                throw new Win32Exception();

            Kernel32.COORD dwWriteCoord;
            dwWriteCoord.X = (short)positionLeft;
            dwWriteCoord.Y = (short)positionTop;

            uint nLength = 1;
            ushort[] lpAttributes = new[] { value.Attributes };
            uint lpNumberOfAttrsWritten = 1;

            if (!Kernel32.WriteConsoleOutputAttribute(stdout, lpAttributes, nLength, dwWriteCoord, out lpNumberOfAttrsWritten))
                throw new Win32Exception();

            PokeScreenChar(positionLeft, positionTop, value.AsciiChar);
        }

        #endregion

        #endregion
    }
}
