namespace ConsoleToolBox
{
    public class Cursor
    {
        /// <summary>
        /// Gets or sets the column position of the <see cref="Cursor"/> within the buffer area.
        /// </summary>
        /// <value>
        /// The left.
        /// </value>
        public int Left { get; set; }
        /// <summary>
        /// Gets or sets the height of the <see cref="Cursor"/> within a character cell.
        /// </summary>
        /// <value>
        /// The size.
        /// </value>
        public int Size { get; set; }
        /// <summary>
        /// Gets or sets the row position of the <see cref="Cursor"/> within the buffer area.
        /// </summary>
        /// <value>
        /// The top.
        /// </value>
        public int Top { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether the <see cref="Cursor"/> is visible.
        /// </summary>
        /// <value>
        ///   <c>true</c> if visible; otherwise, <c>false</c>.
        /// </value>
        public bool Visible { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Cursor"/> class.
        /// </summary>
        public Cursor()
        {
            Left = 0;
            Top = 0;
            Size = 25;
            Visible = true;
        }
    }
}