namespace ConsoleToolBox
{
    /// <summary>
    /// Console character data structure that stores a single Char character and color Attributes.
    /// </summary>
    public class CharEx
    {
        /// <summary>
        /// Gets or sets the ASCII char value.
        /// </summary>
        /// <value></value>
        public char AsciiChar { get; set; }
        /// <summary>
        /// Gets or sets the color of the foreground.
        /// </summary>
        /// <value></value>
        public ushort ForegroundColor { get; set; }
        /// <summary>
        /// Gets or sets the color of the background.
        /// </summary>
        /// <value></value>
        public ushort BackgroundColor { get; set; }
        /// <summary>
        /// Gets or sets the caracter Attributes (background & foreground color).
        /// </summary>
        /// <value></value>
        public ushort Attributes
        {
            get
            {
                return (ushort)((BackgroundColor << 4) + ForegroundColor);
            }
            set
            {
                ForegroundColor = (ushort)(value & 15);
                BackgroundColor = (ushort)((value & 240) >> 4);
            }
        }

        /// <summary>
        /// Returns a <see cref="System.String"/> of the Char value.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return AsciiChar.ToString();
        }
    }
}