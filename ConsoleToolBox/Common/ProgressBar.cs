using System;

namespace ConsoleToolBox.Common
{
    public class ProgressBar
    {
        #region Internal Data & Constructor
        
        private int _positionLeft;
        private int _positionTop;
        private int _barLength;
        private char _fillCharacter;
        private float _currentPercentage;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProgressBar"/> class.
        /// </summary>
        /// <param name="positionLeft">The position left.</param>
        /// <param name="positionTop">The position top.</param>
        /// <param name="barLength">Length of the bar.</param>
        /// <param name="fillCharacter">The fill character.</param>
        public ProgressBar(int positionLeft, int positionTop, int barLength, char fillCharacter)
        {
            SetupProgressBar(positionLeft, positionTop, barLength, fillCharacter, 0);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ProgressBar"/> class.
        /// </summary>
        /// <param name="positionLeft">The position left.</param>
        /// <param name="positionTop">The position top.</param>
        /// <param name="barLength">Length of the bar.</param>
        public ProgressBar(int positionLeft, int positionTop, int barLength)
        {
            SetupProgressBar(positionLeft, positionTop, barLength, '*', 0);
        }

        /// <summary>
        /// Sets up the progress bar.
        /// </summary>
        /// <param name="positionLeft">The position left.</param>
        /// <param name="positionTop">The position top.</param>
        /// <param name="barLength">Length of the bar.</param>
        /// <param name="fillCharacter">The fill character.</param>
        /// <param name="percentage">The percentage value.</param>
        private void SetupProgressBar(int positionLeft, int positionTop, int barLength, char fillCharacter, float percentage)
        {
            if (positionLeft < 0 || positionLeft > Console.WindowWidth)
                throw new ArgumentOutOfRangeException("positionLeft");

            _positionLeft = positionLeft;

            if (positionTop < 0 || positionTop > Console.WindowHeight)
                throw new ArgumentOutOfRangeException("positionTop");

            _positionTop = positionTop;

            if (barLength <= 0)
                throw new ArgumentOutOfRangeException("barLength");

            _barLength = barLength;

            if (fillCharacter.ToString() == string.Empty)
                throw new ArgumentOutOfRangeException("fillCharacter");

            _fillCharacter = fillCharacter;

            if (percentage < 0 || percentage > 100)
                throw new ArgumentOutOfRangeException("percentage");

            _currentPercentage = percentage;
        }
        #endregion

        #region Public functions

        /// <summary>
        /// Sets the percentage.
        /// </summary>
        /// <param name="percentage">The new percentage value.</param>
        public void SetPercentage(float percentage)
        {
            if (percentage < 0)
                percentage = 0;

            if (percentage > 100)
                percentage = 100;

            _currentPercentage = percentage;
        }

        /// <summary>
        /// Updates the ProgressBar display.
        /// </summary>
        public void Update()
        {
            DrawProgressBar();
        }

        /// <summary>
        /// Updates the ProgressBar display with a new percentage.
        /// </summary>
        /// <param name="percentage">The new percentage value.</param>
        public void Update(float percentage)
        {
            SetPercentage(percentage);
            Update();
        }

        #endregion

        #region Private functions

        /// <summary>
        /// Draws the progress bar.
        /// </summary>
        private void DrawProgressBar()
        {
            // First thing we do is save the current cursor position and visibility state to reset once we finish drawing
            bool _cursorVisible = Console.CursorVisible;
            int _cursorLeft = Console.CursorLeft;
            int _cursorTop = Console.CursorTop;

            // Now hide the cursor
            Console.CursorVisible = false;

            string progressBarContent =
                "["
                + new string(_fillCharacter, (int)((_currentPercentage / 100.0) * _barLength))
                + new string(' ', _barLength - (int)((_currentPercentage / 100.0) * _barLength)) 
                + "]";

            Console.SetCursorPosition(_positionLeft,_positionTop);
            Console.Write(progressBarContent);
           
            // All done so let's clean up and put our cursor back to the original location and visibility state
            Console.SetCursorPosition(_cursorLeft,_cursorTop);
            Console.CursorVisible = _cursorVisible;
        }

        #endregion
    }
}