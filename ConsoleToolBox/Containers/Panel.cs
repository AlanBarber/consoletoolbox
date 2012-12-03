using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleToolBox.Containers
{
    public class Panel : IComparable<Panel>
    {
        #region Public Properties & Constructor

        public int ZIndex { get; set; }
        
        public int Height
        {
            get { return _height; } 
            set { ResizeFrameBuffer(value,_width); }
        }

        public int Width { 
            get { return _width; } 
            set { ResizeFrameBuffer(_height,value); } 
        }

        public int Left { get; set; }
        public int Top { get; set; }

        public bool Visible { get; set; }
        public ushort ForegroundColor { get; set; }
        public ushort BackgroundColor { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Panel"/> class.
        /// </summary>
        public Panel()
        {
            if(_log.IsDebugEnabled)
                _log.Debug("");


            _height = 0;
            _width = 0;

            // Create the correct size frameBuffer for the ConsoleToolBox to use
            _frameBuffer = null;

            // Setup Cursor
            _cursor = new Cursor();

            // Set Dirty Frame Buffer flag
            _frameBufferIsDirty = true;
        }
        #endregion


        #region Private Variables
        private static readonly log4net.ILog _log =
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private Cursor _cursor = null;
        private CharEx[,] _frameBuffer = null;
        private int _height;
        private int _width;
        private bool _frameBufferIsDirty;


        #endregion

        #region Public Functions
        #endregion

        #region Private Functions
        /// <summary>
        /// Resizes the frame buffer.
        /// </summary>
        /// <param name="height">The new height.</param>
        /// <param name="width">The new width.</param>
        private void ResizeFrameBuffer(int height, int width)
        {
            // Create temp frame buffer
            CharEx[,] tempBuffer = GetEmptyBuffer(height,width);

            // Copy current frame buffer to temp
            for (int h = 0; h < _height; h++ )
            {
                for(int w = 0; w < _width; w++)
                {
                    tempBuffer[h, w] = _frameBuffer[h, w];
                }
            }

                // Replace with temp
                _frameBuffer = tempBuffer;

            // Set new size
            _height = height;
            _width = width;

            _frameBufferIsDirty = true;
        }

        private CharEx[,] GetEmptyBuffer(int height, int width)
        {
            CharEx[,] array = new CharEx[height, width];
            for (int x = 0; x < height; x++)
            {
                for (int y = 0; y < width; y++)
                {
                    array[x, y] = new CharEx { AsciiChar = ' ', ForegroundColor = ForegroundColor, BackgroundColor = BackgroundColor };
                }
            }
            return array;            
        }
        #endregion

        /// <summary>
        /// Compares the current object with another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        /// A 32-bit signed integer that indicates the relative order of the objects being compared. The return value has the following meanings:
        /// Value
        /// Meaning
        /// Less than zero
        /// This object is less than the <paramref name="other"/> parameter.
        /// Zero
        /// This object is equal to <paramref name="other"/>.
        /// Greater than zero
        /// This object is greater than <paramref name="other"/>.
        /// </returns>
        public int CompareTo(Panel other)
        {
            return this.ZIndex.CompareTo(other.ZIndex);
        }
    }
}
