using System;
using System.Runtime.InteropServices;
using System.Text;

namespace ConsoleToolBox
{
    /// <summary>
    /// Kernel32 holds the imported Kernel32.dll functions and data structures needed to directly access 
    /// and modify the console that are not provided from the .net managed platform. Yup we have to reach outside of managed 
    /// code to do some of  the fancy console work. I want to keep this all some what seperate since this is platform 
    /// dependant and may change in future version of window. 
    /// 
    /// TODO: This should be refactored a bit at some point but for right now it will work fine
    /// </summary>
    internal class Kernel32
    {
        #region Data Structures & Enums

        public enum StdHandle
        {
            Stdin = -10, 
            Stdout = -11, 
            Stderr = -12
        };

        internal struct COORD
        {
            public short X;
            public short Y;

        }

        internal struct SMALL_RECT
        {
            public short Left;
            public short Top;
            public short Right;
            public short Bottom;
        }

        [StructLayout(LayoutKind.Explicit)]
        internal struct CHAR_INFO
        {
            [FieldOffset(0)]
            char UnicodeChar;
            [FieldOffset(0)]
            char AsciiChar;
            [FieldOffset(2)] //2 bytes seems to work properly
            UInt16 Attributes;
        }

        //internal struct CONSOLE_SCREEN_BUFFER_INFO
        //{
        //    public COORD dwSize;
        //    public COORD dwCursorPosition;
        //    public short wAttributes;
        //    public SMALL_RECT srWindow;
        //    public COORD dwMaximumWindowSize;
        //}

        //[StructLayout(LayoutKind.Sequential)]
        //internal struct CONSOLE_SCREEN_BUFFER_INFO_EX
        //{
        //    public uint cbSize;
        //    public COORD dwSize;
        //    public COORD dwCursorPosition;
        //    public short wAttributes;
        //    public SMALL_RECT srWindow;
        //    public COORD dwMaximumWindowSize;

        //    public ushort wPopupAttributes;
        //    public bool bFullscreenSupported;
        //    public COLORREF color0;
        //    public COLORREF color1;
        //    public COLORREF color2;
        //    public COLORREF color3;

        //    public COLORREF color4;
        //    public COLORREF color5;
        //    public COLORREF color6;
        //    public COLORREF color7;

        //    public COLORREF color8;
        //    public COLORREF color9;
        //    public COLORREF colorA;
        //    public COLORREF colorB;

        //    public COLORREF colorC;
        //    public COLORREF colorD;
        //    public COLORREF colorE;
        //    public COLORREF colorF;
        //}

        //[StructLayout(LayoutKind.Sequential)]
        //public struct COLORREF
        //{
        //    public uint ColorDWORD;

        //    public COLORREF(System.Drawing.Color color)
        //    {
        //        ColorDWORD = (uint)color.R + (((uint)color.G) << 8) + (((uint)color.B) << 16);
        //    }

        //    public System.Drawing.Color GetColor()
        //    {
        //        return System.Drawing.Color.FromArgb((int)(0x000000FFU & ColorDWORD),
        //           (int)(0x0000FF00U & ColorDWORD) >> 8, (int)(0x00FF0000U & ColorDWORD) >> 16);
        //    }

        //    public void SetColor(System.Drawing.Color color)
        //    {
        //        ColorDWORD = (uint)color.R + (((uint)color.G) << 8) + (((uint)color.B) << 16);
        //    }
        //}

        //[StructLayout(LayoutKind.Sequential)]
        //internal struct CONSOLE_FONT_INFO
        //{
        //    public int nFont;
        //    public COORD dwFontSize;
        //}

        //[StructLayout(LayoutKind.Sequential)]
        //public struct CONSOLE_FONT_INFO_EX
        //{
        //    public uint cbSize;
        //    public uint nFont;
        //    public COORD dwFontSize;
        //    public ushort FontFamily;
        //    public ushort FontWeight;
        //    fixed char FaceName[LF_FACESIZE];

        //    const uint LF_FACESIZE = 32;
        //}

        //[StructLayout(LayoutKind.Explicit)]
        //internal struct INPUT_RECORD
        //{
        //    [FieldOffset(0)]
        //    public ushort EventType;
        //    [FieldOffset(4)]
        //    public KEY_EVENT_RECORD KeyEvent;
        //    [FieldOffset(4)]
        //    public MOUSE_EVENT_RECORD MouseEvent;
        //    [FieldOffset(4)]
        //    public WINDOW_BUFFER_SIZE_RECORD WindowBufferSizeEvent;
        //    [FieldOffset(4)]
        //    public MENU_EVENT_RECORD MenuEvent;
        //    [FieldOffset(4)]
        //    public FOCUS_EVENT_RECORD FocusEvent;
        //};

        //[StructLayout(LayoutKind.Explicit, CharSet = CharSet.Unicode)]
        //internal struct KEY_EVENT_RECORD
        //{
        //    [FieldOffset(0), MarshalAs(UnmanagedType.Bool)]
        //    public bool bKeyDown;
        //    [FieldOffset(4), MarshalAs(UnmanagedType.U2)]
        //    public ushort wRepeatCount;
        //    [FieldOffset(6), MarshalAs(UnmanagedType.U2)]
        //    //public VirtualKeys wVirtualKeyCode;
        //    public ushort wVirtualKeyCode;
        //    [FieldOffset(8), MarshalAs(UnmanagedType.U2)]
        //    public ushort wVirtualScanCode;
        //    [FieldOffset(10)]
        //    public char UnicodeChar;
        //    [FieldOffset(12), MarshalAs(UnmanagedType.U4)]
        //    //public ControlKeyState dwControlKeyState;
        //    public uint dwControlKeyState;
        //}

        //[StructLayout(LayoutKind.Sequential)]
        //internal struct MOUSE_EVENT_RECORD
        //{
        //    public COORD dwMousePosition;
        //    public uint dwButtonState;
        //    public uint dwControlKeyState;
        //    public uint dwEventFlags;
        //}

        //internal struct WINDOW_BUFFER_SIZE_RECORD
        //{
        //    public COORD dwSize;

        //    public WINDOW_BUFFER_SIZE_RECORD(short x, short y)
        //    {
        //        dwSize = new COORD();
        //        dwSize.X = x;
        //        dwSize.Y = y;
        //    }
        //}

        //[StructLayout(LayoutKind.Sequential)]
        //internal struct MENU_EVENT_RECORD
        //{
        //    public uint dwCommandId;
        //}

        //[StructLayout(LayoutKind.Sequential)]
        //internal struct FOCUS_EVENT_RECORD
        //{
        //    public uint bSetFocus;
        //}

        //[StructLayout(LayoutKind.Sequential)]
        //internal struct CONSOLE_CURSOR_INFO
        //{
        //    uint Size;
        //    bool Visible;
        //}

        //[StructLayout(LayoutKind.Sequential)]
        //internal struct CONSOLE_HISTORY_INFO
        //{
        //    ushort cbSize;
        //    ushort HistoryBufferSize;
        //    ushort NumberOfHistoryBuffers;
        //    uint dwFlags;
        //}

        //[StructLayout(LayoutKind.Sequential)]
        //internal struct CONSOLE_SELECTION_INFO
        //{
        //    uint Flags;
        //    COORD SelectionAnchor;
        //    SMALL_RECT Selection;

        //    // Flags values:
        //    const uint CONSOLE_MOUSE_DOWN = 0x0008; // Mouse is down
        //    const uint CONSOLE_MOUSE_SELECTION = 0x0004; //Selecting with the mouse
        //    const uint CONSOLE_NO_SELECTION = 0x0000; //No selection
        //    const uint CONSOLE_SELECTION_IN_PROGRESS = 0x0001; //Selection has begun
        //    const uint CONSOLE_SELECTION_NOT_EMPTY = 0x0002; //Selection rectangle is not empty
        //}

        //// Enumerated type for the control messages sent to the handler routine
        //internal enum CtrlTypes : uint
        //{
        //    CTRL_C_EVENT = 0,
        //    CTRL_BREAK_EVENT,
        //    CTRL_CLOSE_EVENT,
        //    CTRL_LOGOFF_EVENT = 5,
        //    CTRL_SHUTDOWN_EVENT
        //}
        #endregion

        #region Kernel32 Console Functions

        [DllImport("kernel32.dll", SetLastError = true)]
        internal static extern IntPtr GetStdHandle(
            StdHandle std
            );

        //[DllImport("kernel32.dll", SetLastError = true)]
        //static extern bool SetStdHandle(
        //    uint nStdHandle,
        //    IntPtr hHandle
        //    );

        //[DllImport("kernel32.dll", SetLastError = true)]
        //static extern bool GetConsoleCursorInfo(
        //    IntPtr hConsoleOutput,
        //    out CONSOLE_CURSOR_INFO lpConsoleCursorInfo
        //    );

        //[DllImport("kernel32.dll", SetLastError = true)]
        //static extern bool GetConsoleDisplayMode(
        //    out uint ModeFlags
        //    );

        [DllImport("kernel32.dll", SetLastError = true)]
        internal static extern uint GetConsoleTitle(
            [Out] StringBuilder lpConsoleTitle,
            uint nSize
            );


        [DllImport("kernel32.dll", SetLastError = true)]
        internal static extern uint GetConsoleOriginalTitle(
            [Out] StringBuilder lpConsoleTitle,
            uint nSize
            );

        //[DllImport("kernel32.dll", SetLastError = true)]
        //internal static extern bool GetConsoleScreenBufferInfo(
        //    IntPtr hConsoleOutput, 
        //    [Out] CONSOLE_SCREEN_BUFFER_INFO lpConsoleScreenBufferInfo
        //    );
        
        //[DllImport("kernel32.dll", SetLastError = true)]
        //static extern bool GetNumberOfConsoleMouseButtons(
        //    ref uint lpNumberOfMouseButtons
        //    );
        
        //[DllImport("kernel32.dll", SetLastError = true)]
        //static extern bool PeekConsoleInput(
        //    IntPtr hConsoleInput,
        //    [Out] INPUT_RECORD[] lpBuffer,
        //    uint nLength,
        //    out uint lpNumberOfEventsRead
        //    );

        //[DllImport("kernel32.dll", SetLastError = true)]
        //static extern bool ReadConsole(
        //    IntPtr hConsoleInput,
        //    [Out] StringBuilder lpBuffer,
        //    uint nNumberOfCharsToRead,
        //    out uint lpNumberOfCharsRead,
        //    IntPtr lpReserved
        //    );

        //[DllImport("kernel32.dll", EntryPoint = "ReadConsoleInputW", CharSet = CharSet.Unicode)]
        //static extern bool ReadConsoleInput(
        //    IntPtr hConsoleInput,
        //    [Out] INPUT_RECORD[] lpBuffer,
        //    uint nLength,
        //    out uint lpNumberOfEventsRead
        //    );

        //[DllImport("kernel32.dll", SetLastError = true)]
        //static extern bool ReadConsoleOutput(
        //    IntPtr hConsoleOutput,
        //    [Out] CHAR_INFO[] lpBuffer,
        //    COORD dwBufferSize,
        //    COORD dwBufferCoord,
        //    ref SMALL_RECT lpReadRegion
        //    );

        [DllImport("kernel32.dll", SetLastError = true)]
        internal static extern bool ReadConsoleOutputAttribute(
            IntPtr hConsoleOutput,
            [Out] ushort[] lpAttribute,
            uint nLength,
            COORD dwReadCoord,
            out uint lpNumberOfAttrsRead
            );

        [DllImport("kernel32.dll", SetLastError = true)]
        internal static extern bool ReadConsoleOutputCharacter(
            IntPtr hConsoleOutput,
            [Out] StringBuilder lpCharacter,
            uint nLength,
            COORD dwReadCoord,
            out uint lpNumberOfCharsRead
            );

        //[DllImport("kernel32.dll", SetLastError = true)]
        //static extern bool SetConsoleCursorInfo(
        //    IntPtr hConsoleOutput,
        //    [In] ref CONSOLE_CURSOR_INFO lpConsoleCursorInfo
        //    );

        //[DllImport("kernel32.dll", SetLastError = true)]
        //internal static extern bool SetConsoleCursorPosition(
        //    IntPtr hConsoleOutput,
        //   COORD dwCursorPosition
        //    );


        [DllImport("kernel32.dll", SetLastError = true)]
        internal static extern bool SetConsoleTitle(
            string lpConsoleTitle
            );

        [DllImport("kernel32.dll")]
        internal static extern bool WriteConsoleOutputAttribute(
            IntPtr hConsoleOutput,
            ushort[] lpAttribute, 
            uint nLength, 
            COORD dwWriteCoord,
            out uint lpNumberOfAttrsWritten
            );


        [DllImport("kernel32.dll", SetLastError = true)]
        internal static extern bool WriteConsoleOutputCharacter(
            IntPtr hConsoleOutput,
            string lpCharacter,
            uint nLength,
            COORD dwWriteCoord,
            out uint lpNumberOfCharsWritten
            );

        #endregion
    }
}