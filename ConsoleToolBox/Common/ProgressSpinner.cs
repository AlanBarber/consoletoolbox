using System;

namespace ConsoleToolBox.Common
{
    public class ProgressSpinner
    {
        #region Internal Data & Constructor

        private int _positionLeft;
        private int _positionTop;
        private int _spinnerState;

        public ProgressSpinner(int positionLeft, int positionTop)
        {
            SetupProgressSpinner(positionLeft, positionTop);
        }

        private void SetupProgressSpinner(int positionLeft, int positionTop)
        {
            if (positionLeft < 0 || positionLeft > Console.WindowWidth)
                throw new ArgumentOutOfRangeException("positionLeft");

            _positionLeft = positionLeft;

            if (positionTop < 0 || positionTop > Console.WindowHeight)
                throw new ArgumentOutOfRangeException("positionTop");

            _positionTop = positionTop;

            _spinnerState = 0;

        }

        #endregion

        #region Public Functions

        public void Update()
        {
            DrawSpinner();
        }

        #endregion

        #region Private functions

        private void DrawSpinner()
        {
            // First thing we do is save the current cursor position and visibility state to reset once we finish drawing
            bool _cursorVisible = Console.CursorVisible;
            int _cursorLeft = Console.CursorLeft;
            int _cursorTop = Console.CursorTop;

            // Now hide the cursor
            Console.CursorVisible = false;

            // Draw the current spinner
            Console.SetCursorPosition(_positionLeft, _positionTop);
            switch(_spinnerState)
            {
                case 0:
                    Console.Write(@"|");
                    _spinnerState = 1;
                    break;
                case 1:
                    Console.Write(@"/");
                    _spinnerState = 2;
                    break;
                case 2:
                    Console.Write(@"-");
                    _spinnerState = 3;
                    break;
                case 3:
                    Console.Write(@"\");
                    _spinnerState = 0;
                    break;
            }

            // All done so let's clean up and put our cursor back to the original location and visibility state
            Console.SetCursorPosition(_cursorLeft, _cursorTop);
            Console.CursorVisible = _cursorVisible;
        }

        #endregion
    }
}