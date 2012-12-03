using System;
using System.ComponentModel;
using System.Reflection;
using System.Text;

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
namespace ConsoleToolBox
{
    public class ConsoleToolBox
    {
        #region Generic Functions

        /// <summary>
        /// Returns the version number of the ConsoleToolBox library.
        /// </summary>
        /// <returns></returns>
        public static string GetVersion()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            return assembly.GetName().Version.ToString();
        }

        #endregion

        #region Low Level Screen Character Functions

        /// <summary>
        /// Returns a single <see cref="Char"/> value that matches the current character at the specified screen location.
        /// </summary>
        /// <param name="positionLeft">The position counting from the left (starts at position 0).</param>
        /// <param name="positionTop">The position counting from the top (starts at position 0).</param>
        /// <returns>Char</returns>
        public static char PeekChar(int positionLeft, int positionTop)
        {
            IntPtr stdout = Kernel32ConsoleFunctions.GetStdHandle(Kernel32ConsoleFunctions.StdHandle.Stdout);

            if (stdout == IntPtr.Zero)
                throw new Win32Exception();

            Kernel32ConsoleFunctions.COORD dwReadCoord;
            dwReadCoord.X = (short) positionLeft;
            dwReadCoord.Y = (short) positionTop;
            
            uint nLength = 1;
            StringBuilder lpCharacter = new StringBuilder((int)nLength);

            uint lpNumberOfCharsRead = 1;

            if (!Kernel32ConsoleFunctions.ReadConsoleOutputCharacter(stdout, lpCharacter, nLength, dwReadCoord, out lpNumberOfCharsRead))
                throw new Win32Exception();

            return lpCharacter[0];
        }

        /// <summary>
        /// Returns a single <see cref="CharEx"/> that matches the current character and attributes at the specified screen location.
        /// </summary>
        /// <param name="positionLeft">The position counting from the left (starts at position 0).</param>
        /// <param name="positionTop">The position counting from the top (starts at position 0).</param>
        /// <returns>CharEx</returns>
        public static CharEx PeekCharEx(int positionLeft, int positionTop)
        {
            IntPtr stdout = Kernel32ConsoleFunctions.GetStdHandle(Kernel32ConsoleFunctions.StdHandle.Stdout);

            if (stdout == IntPtr.Zero)
                throw new Win32Exception();

            Kernel32ConsoleFunctions.COORD dwReadCoord;
            dwReadCoord.X = (short)positionLeft;
            dwReadCoord.Y = (short)positionTop;

            uint nLength = 1;
            ushort[] lpAttribute = new ushort[1];

            uint lpNumberOfAttrsRead = 1;

            if (!Kernel32ConsoleFunctions.ReadConsoleOutputAttribute(stdout,lpAttribute,nLength,dwReadCoord,out lpNumberOfAttrsRead))
                throw new Win32Exception();            
            
            char _asciiChar = PeekChar(positionLeft, positionTop);

            return new CharEx { AsciiChar = _asciiChar, Attributes = lpAttribute[0] };
        }

        /// <summary>
        /// Places a single character value at the specified screen location.
        /// </summary>
        /// <param name="positionLeft">The position counting from the left (starts at position 0).</param>
        /// <param name="positionTop">The position counting from the top (starts at position 0).</param>
        /// <param name="value">The character to be written to the screen.</param>
        public static void PokeChar(int positionLeft, int positionTop, char value)
        {
            IntPtr stdout = Kernel32ConsoleFunctions.GetStdHandle(Kernel32ConsoleFunctions.StdHandle.Stdout);

            if (stdout == IntPtr.Zero)
                throw new Win32Exception();

            Kernel32ConsoleFunctions.COORD dwWriteCoord;
            dwWriteCoord.X = (short) positionLeft;
            dwWriteCoord.Y = (short) positionTop;

            uint nLength = 1;
            string lpCharacter = value.ToString();
            uint lpNumberOfCharsWritten = 1;

            if (!Kernel32ConsoleFunctions.WriteConsoleOutputCharacter(stdout, lpCharacter, nLength, dwWriteCoord, out lpNumberOfCharsWritten))
                throw new Win32Exception();
        }

        /// <summary>
        /// Places a single character value and set attributes (fg/bg colors) at the specified screen location.
        /// </summary>
        /// <param name="positionLeft">The position counting from the left (starts at position 0).</param>
        /// <param name="positionTop">The position counting from the top (starts at position 0).</param>
        /// <param name="value">The character and atributes to be written to the screen.</param>
        public static void PokeCharEx(int positionLeft, int positionTop, CharEx value)
        {
            IntPtr stdout = Kernel32ConsoleFunctions.GetStdHandle(Kernel32ConsoleFunctions.StdHandle.Stdout);

            if (stdout == IntPtr.Zero)
                throw new Win32Exception();

            Kernel32ConsoleFunctions.COORD dwWriteCoord;
            dwWriteCoord.X = (short)positionLeft;
            dwWriteCoord.Y = (short)positionTop;

            uint nLength = 1;
            ushort[] lpAttributes = new[] {value.Attributes};
            uint lpNumberOfAttrsWritten = 1;

            if (!Kernel32ConsoleFunctions.WriteConsoleOutputAttribute(stdout, lpAttributes, nLength, dwWriteCoord, out lpNumberOfAttrsWritten))
                throw new Win32Exception();
            
            PokeChar(positionLeft, positionTop, value.AsciiChar);
        }

        #endregion

        #region Title Bar Functions (Get / Set)
        /// <summary>
        /// Gets the console window current title.
        /// </summary>
        /// <returns></returns>
        public static string GetConsoleTitle()
        {
            uint nSize = 1024;
            StringBuilder lpConsoleTitle = new StringBuilder((int)nSize);

            if (Kernel32ConsoleFunctions.GetConsoleTitle(lpConsoleTitle, nSize) <= 0)
                throw new Win32Exception();

            return lpConsoleTitle.ToString();
        }

        /// <summary>
        /// Gets the console window original title when the console was first created.
        /// </summary>
        /// <returns></returns>
        public static string GetConsoleOriginalTitle()
        {
            uint nSize = 1024;
            StringBuilder lpConsoleTitle = new StringBuilder((int)nSize);

            if (Kernel32ConsoleFunctions.GetConsoleOriginalTitle(lpConsoleTitle, nSize) <= 0)
                throw new Win32Exception();

            return lpConsoleTitle.ToString();
        }

        /// <summary>
        /// Sets the console window title.
        /// </summary>
        /// <param name="lpConsoleTitle">The title to be set.</param>
        public static void SetConsoleTitle(string lpConsoleTitle)
        {
            if (!Kernel32ConsoleFunctions.SetConsoleTitle(lpConsoleTitle))
                throw new Win32Exception();
        }
        #endregion

    }
}
