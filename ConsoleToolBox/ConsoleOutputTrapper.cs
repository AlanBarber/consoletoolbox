using System;
using System.IO;
using System.Security;
using System.Text;

namespace ConsoleToolBox
{
    /// <summary>
    /// ConsoleOutputTrapper is a class that inherrits from TextWriter designed to catch and handle all 
    /// Console.Write() and Console.WriteLine() calls while using the CtbManager to have content written
    /// to the CtbManager framebuffer instead of directly to the screen.
    /// </summary>
    internal class ConsoleOutputTrapper : TextWriter
    {
        private TextWriter _textWritter;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConsoleOutputTrapper"/> class.
        /// </summary>
        /// <param name="inWriter">The TextWriter.</param>
        public ConsoleOutputTrapper(TextWriter inWriter)
        {
            _textWritter = inWriter;
        }
        
        public override Encoding Encoding
        {
            get { return _textWritter.Encoding; }
        }

        /// <summary>
        /// Writes a character to the text stream.
        /// </summary>
        /// <param name="value">The character to write to the text stream. </param><exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.TextWriter"/> is closed. </exception><exception cref="T:System.IO.IOException">An I/O error occurs. </exception><filterpriority>1</filterpriority>
        public override void Write(char value) { throw new NotImplementedException(); }

        /// <summary>
        /// Writes a character array to the text stream.
        /// </summary>
        /// <param name="buffer">The character array to write to the text stream. </param><exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.TextWriter"/> is closed. </exception><exception cref="T:System.IO.IOException">An I/O error occurs. </exception><filterpriority>1</filterpriority>
        public override void Write(char[] buffer) { throw new NotImplementedException(); }

        /// <summary>
        /// Writes a subarray of characters to the text stream.
        /// </summary>
        /// <param name="buffer">The character array to write data from. </param><param name="index">Starting index in the buffer. </param><param name="count">The number of characters to write. </param><exception cref="T:System.ArgumentException">The buffer length minus <paramref name="index"/> is less than <paramref name="count"/>. </exception><exception cref="T:System.ArgumentNullException">The <paramref name="buffer"/> parameter is null. </exception><exception cref="T:System.ArgumentOutOfRangeException"><paramref name="index"/> or <paramref name="count"/> is negative. </exception><exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.TextWriter"/> is closed. </exception><exception cref="T:System.IO.IOException">An I/O error occurs. </exception><filterpriority>1</filterpriority>
        public override void Write(char[] buffer, int index, int count) { throw new NotImplementedException(); }

        /// <summary>
        /// Writes the text representation of a Boolean value to the text stream.
        /// </summary>
        /// <param name="value">The Boolean to write. </param><exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.TextWriter"/> is closed. </exception><exception cref="T:System.IO.IOException">An I/O error occurs. </exception><filterpriority>1</filterpriority>
        public override void Write(bool value) { throw new NotImplementedException(); }

        /// <summary>
        /// Writes the text representation of a 4-byte signed integer to the text stream.
        /// </summary>
        /// <param name="value">The 4-byte signed integer to write. </param><exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.TextWriter"/> is closed. </exception><exception cref="T:System.IO.IOException">An I/O error occurs. </exception><filterpriority>1</filterpriority>
        public override void Write(int value) { throw new NotImplementedException(); }

        /// <summary>
        /// Writes the text representation of a 4-byte unsigned integer to the text stream.
        /// </summary>
        /// <param name="value">The 4-byte unsigned integer to write. </param><exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.TextWriter"/> is closed. </exception><exception cref="T:System.IO.IOException">An I/O error occurs. </exception><filterpriority>1</filterpriority>
        [CLSCompliant(false)]
        public override void Write(uint value) { throw new NotImplementedException(); }

        /// <summary>
        /// Writes the text representation of an 8-byte signed integer to the text stream.
        /// </summary>
        /// <param name="value">The 8-byte signed integer to write. </param><exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.TextWriter"/> is closed. </exception><exception cref="T:System.IO.IOException">An I/O error occurs. </exception><filterpriority>1</filterpriority>
        public override void Write(long value) { throw new NotImplementedException(); }

        /// <summary>
        /// Writes the text representation of an 8-byte unsigned integer to the text stream.
        /// </summary>
        /// <param name="value">The 8-byte unsigned integer to write. </param><exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.TextWriter"/> is closed. </exception><exception cref="T:System.IO.IOException">An I/O error occurs. </exception><filterpriority>1</filterpriority>
        [CLSCompliant(false)]
        public override void Write(ulong value) { throw new NotImplementedException(); }

        /// <summary>
        /// Writes the text representation of a 4-byte floating-point value to the text stream.
        /// </summary>
        /// <param name="value">The 4-byte floating-point value to write. </param><exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.TextWriter"/> is closed. </exception><exception cref="T:System.IO.IOException">An I/O error occurs. </exception><filterpriority>1</filterpriority>
        public override void Write(float value) { throw new NotImplementedException(); }

        /// <summary>
        /// Writes the text representation of an 8-byte floating-point value to the text stream.
        /// </summary>
        /// <param name="value">The 8-byte floating-point value to write. </param><exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.TextWriter"/> is closed. </exception><exception cref="T:System.IO.IOException">An I/O error occurs. </exception><filterpriority>1</filterpriority>
        public override void Write(double value) { throw new NotImplementedException(); }

        /// <summary>
        /// Writes the text representation of a decimal value to the text stream.
        /// </summary>
        /// <param name="value">The decimal value to write. </param><exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.TextWriter"/> is closed. </exception><exception cref="T:System.IO.IOException">An I/O error occurs. </exception><filterpriority>1</filterpriority>
        public override void Write(decimal value) { throw new NotImplementedException(); }

        /// <summary>
        /// Writes a string to the text stream.
        /// </summary>
        /// <param name="value">The string to write. </param><exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.TextWriter"/> is closed. </exception><exception cref="T:System.IO.IOException">An I/O error occurs. </exception><filterpriority>1</filterpriority>
        public override void Write(string value) { throw new NotImplementedException(); }

        /// <summary>
        /// Writes the text representation of an object to the text stream by calling ToString on that object.
        /// </summary>
        /// <param name="value">The object to write. </param><exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.TextWriter"/> is closed. </exception><exception cref="T:System.IO.IOException">An I/O error occurs. </exception><filterpriority>1</filterpriority>
        public override void Write(object value) { throw new NotImplementedException(); }

        /// <summary>
        /// Writes out a formatted string, using the same semantics as <see cref="M:System.String.Format(System.String,System.Object)"/>.
        /// </summary>
        /// <param name="format">The formatting string. </param><param name="arg0">An object to write into the formatted string. </param><exception cref="T:System.ArgumentNullException"><paramref name="format"/> is null. </exception><exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.TextWriter"/> is closed. </exception><exception cref="T:System.IO.IOException">An I/O error occurs. </exception><exception cref="T:System.FormatException">The format specification in format is invalid.-or- The number indicating an argument to be formatted is less than zero, or larger than or equal to the number of provided objects to be formatted. </exception><filterpriority>1</filterpriority>
        public override void Write(string format, object arg0) { throw new NotImplementedException(); }

        /// <summary>
        /// Writes out a formatted string, using the same semantics as <see cref="M:System.String.Format(System.String,System.Object)"/>.
        /// </summary>
        /// <param name="format">The formatting string. </param><param name="arg0">An object to write into the formatted string. </param><param name="arg1">An object to write into the formatted string. </param><exception cref="T:System.ArgumentNullException"><paramref name="format"/> is null. </exception><exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.TextWriter"/> is closed. </exception><exception cref="T:System.IO.IOException">An I/O error occurs. </exception><exception cref="T:System.FormatException">The format specification in format is invalid.-or- The number indicating an argument to be formatted is less than zero, or larger than or equal to the number of provided objects to be formatted. </exception><filterpriority>1</filterpriority>
        public override void Write(string format, object arg0, object arg1) { throw new NotImplementedException(); }

        /// <summary>
        /// Writes out a formatted string, using the same semantics as <see cref="M:System.String.Format(System.String,System.Object)"/>.
        /// </summary>
        /// <param name="format">The formatting string. </param><param name="arg0">An object to write into the formatted string. </param><param name="arg1">An object to write into the formatted string. </param><param name="arg2">An object to write into the formatted string. </param><exception cref="T:System.ArgumentNullException"><paramref name="format"/> is null. </exception><exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.TextWriter"/> is closed. </exception><exception cref="T:System.IO.IOException">An I/O error occurs. </exception><exception cref="T:System.FormatException">The format specification in format is invalid.-or- The number indicating an argument to be formatted is less than zero, or larger than or equal to the number of provided objects to be formatted. </exception><filterpriority>1</filterpriority>
        public override void Write(string format, object arg0, object arg1, object arg2) { throw new NotImplementedException(); }

        /// <summary>
        /// Writes out a formatted string, using the same semantics as <see cref="M:System.String.Format(System.String,System.Object)"/>.
        /// </summary>
        /// <param name="format">The formatting string. </param><param name="arg">The object array to write into the formatted string. </param><exception cref="T:System.ArgumentNullException"><paramref name="format"/> or <paramref name="arg"/> is null. </exception><exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.TextWriter"/> is closed. </exception><exception cref="T:System.IO.IOException">An I/O error occurs. </exception><exception cref="T:System.FormatException">The format specification in format is invalid.-or- The number indicating an argument to be formatted is less than zero, or larger than or equal to <paramref name="arg"/>. Length. </exception><filterpriority>1</filterpriority>
        public override void Write(string format, params object[] arg) { throw new NotImplementedException(); }

        /// <summary>
        /// Writes a line terminator to the text stream.
        /// </summary>
        /// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.TextWriter"/> is closed. </exception><exception cref="T:System.IO.IOException">An I/O error occurs. </exception><filterpriority>1</filterpriority>
        public override void WriteLine() { throw new NotImplementedException(); }

        /// <summary>
        /// Writes a character followed by a line terminator to the text stream.
        /// </summary>
        /// <param name="value">The character to write to the text stream. </param><exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.TextWriter"/> is closed. </exception><exception cref="T:System.IO.IOException">An I/O error occurs. </exception><filterpriority>1</filterpriority>
        public override void WriteLine(char value) { throw new NotImplementedException(); }

        /// <summary>
        /// Writes an array of characters followed by a line terminator to the text stream.
        /// </summary>
        /// <param name="buffer">The character array from which data is read. </param><exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.TextWriter"/> is closed. </exception><exception cref="T:System.IO.IOException">An I/O error occurs. </exception><filterpriority>1</filterpriority>
        public override void WriteLine(char[] buffer) { throw new NotImplementedException(); }

        /// <summary>
        /// Writes a subarray of characters followed by a line terminator to the text stream.
        /// </summary>
        /// <param name="buffer">The character array from which data is read. </param><param name="index">The index into <paramref name="buffer"/> at which to begin reading. </param><param name="count">The maximum number of characters to write. </param><exception cref="T:System.ArgumentException">The buffer length minus <paramref name="index"/> is less than <paramref name="count"/>. </exception><exception cref="T:System.ArgumentNullException">The <paramref name="buffer"/> parameter is null. </exception><exception cref="T:System.ArgumentOutOfRangeException"><paramref name="index"/> or <paramref name="count"/> is negative. </exception><exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.TextWriter"/> is closed. </exception><exception cref="T:System.IO.IOException">An I/O error occurs. </exception><filterpriority>1</filterpriority>
        public override void WriteLine(char[] buffer, int index, int count) { throw new NotImplementedException(); }

        /// <summary>
        /// Writes the text representation of a Boolean followed by a line terminator to the text stream.
        /// </summary>
        /// <param name="value">The Boolean to write. </param><exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.TextWriter"/> is closed. </exception><exception cref="T:System.IO.IOException">An I/O error occurs. </exception><filterpriority>1</filterpriority>
        public override void WriteLine(bool value) { throw new NotImplementedException(); }

        /// <summary>
        /// Writes the text representation of a 4-byte signed integer followed by a line terminator to the text stream.
        /// </summary>
        /// <param name="value">The 4-byte signed integer to write. </param><exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.TextWriter"/> is closed. </exception><exception cref="T:System.IO.IOException">An I/O error occurs. </exception><filterpriority>1</filterpriority>
        public override void WriteLine(int value) { throw new NotImplementedException(); }

        /// <summary>
        /// Writes the text representation of a 4-byte unsigned integer followed by a line terminator to the text stream.
        /// </summary>
        /// <param name="value">The 4-byte unsigned integer to write. </param><exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.TextWriter"/> is closed. </exception><exception cref="T:System.IO.IOException">An I/O error occurs. </exception><filterpriority>1</filterpriority>
        [CLSCompliant(false)]
        public override void WriteLine(uint value) { throw new NotImplementedException(); }

        /// <summary>
        /// Writes the text representation of an 8-byte signed integer followed by a line terminator to the text stream.
        /// </summary>
        /// <param name="value">The 8-byte signed integer to write. </param><exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.TextWriter"/> is closed. </exception><exception cref="T:System.IO.IOException">An I/O error occurs. </exception><filterpriority>1</filterpriority>
        public override void WriteLine(long value) { throw new NotImplementedException(); }

        /// <summary>
        /// Writes the text representation of an 8-byte unsigned integer followed by a line terminator to the text stream.
        /// </summary>
        /// <param name="value">The 8-byte unsigned integer to write. </param><exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.TextWriter"/> is closed. </exception><exception cref="T:System.IO.IOException">An I/O error occurs. </exception><filterpriority>1</filterpriority>
        [CLSCompliant(false)]
        public override void WriteLine(ulong value) { throw new NotImplementedException(); }

        /// <summary>
        /// Writes the text representation of a 4-byte floating-point value followed by a line terminator to the text stream.
        /// </summary>
        /// <param name="value">The 4-byte floating-point value to write. </param><exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.TextWriter"/> is closed. </exception><exception cref="T:System.IO.IOException">An I/O error occurs. </exception><filterpriority>1</filterpriority>
        public override void WriteLine(float value) { throw new NotImplementedException(); }

        /// <summary>
        /// Writes the text representation of a 8-byte floating-point value followed by a line terminator to the text stream.
        /// </summary>
        /// <param name="value">The 8-byte floating-point value to write. </param><exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.TextWriter"/> is closed. </exception><exception cref="T:System.IO.IOException">An I/O error occurs. </exception><filterpriority>1</filterpriority>
        public override void WriteLine(double value) { throw new NotImplementedException(); }

        /// <summary>
        /// Writes the text representation of a decimal value followed by a line terminator to the text stream.
        /// </summary>
        /// <param name="value">The decimal value to write. </param><exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.TextWriter"/> is closed. </exception><exception cref="T:System.IO.IOException">An I/O error occurs. </exception><filterpriority>1</filterpriority>
        public override void WriteLine(decimal value) { throw new NotImplementedException(); }

        /// <summary>
        /// Writes a string followed by a line terminator to the text stream.
        /// </summary>
        /// <param name="value">The string to write. If <paramref name="value"/> is null, only the line termination characters are written. </param><exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.TextWriter"/> is closed. </exception><exception cref="T:System.IO.IOException">An I/O error occurs. </exception><filterpriority>1</filterpriority>
        [SecuritySafeCritical]
        public override void WriteLine(string value) { throw new NotImplementedException(); }

        /// <summary>
        /// Writes the text representation of an object by calling ToString on this object, followed by a line terminator to the text stream.
        /// </summary>
        /// <param name="value">The object to write. If <paramref name="value"/> is null, only the line termination characters are written. </param><exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.TextWriter"/> is closed. </exception><exception cref="T:System.IO.IOException">An I/O error occurs. </exception><filterpriority>1</filterpriority>
        public override void WriteLine(object value) { throw new NotImplementedException(); }

        /// <summary>
        /// Writes out a formatted string and a new line, using the same semantics as <see cref="M:System.String.Format(System.String,System.Object)"/>.
        /// </summary>
        /// <param name="format">The formatted string. </param><param name="arg0">The object to write into the formatted string. </param><exception cref="T:System.ArgumentNullException"><paramref name="format"/> is null. </exception><exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.TextWriter"/> is closed. </exception><exception cref="T:System.IO.IOException">An I/O error occurs. </exception><exception cref="T:System.FormatException">The format specification in format is invalid.-or- The number indicating an argument to be formatted is less than zero, or larger than or equal to the number of provided objects to be formatted. </exception><filterpriority>1</filterpriority>
        public override void WriteLine(string format, object arg0) { throw new NotImplementedException(); }

        /// <summary>
        /// Writes out a formatted string and a new line, using the same semantics as <see cref="M:System.String.Format(System.String,System.Object)"/>.
        /// </summary>
        /// <param name="format">The formatting string. </param><param name="arg0">The object to write into the format string. </param><param name="arg1">The object to write into the format string. </param><exception cref="T:System.ArgumentNullException"><paramref name="format"/> is null. </exception><exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.TextWriter"/> is closed. </exception><exception cref="T:System.IO.IOException">An I/O error occurs. </exception><exception cref="T:System.FormatException">The format specification in format is invalid.-or- The number indicating an argument to be formatted is less than zero, or larger than or equal to the number of provided objects to be formatted. </exception><filterpriority>1</filterpriority>
        public override void WriteLine(string format, object arg0, object arg1) { throw new NotImplementedException(); }

        /// <summary>
        /// Writes out a formatted string and a new line, using the same semantics as <see cref="M:System.String.Format(System.String,System.Object)"/>.
        /// </summary>
        /// <param name="format">The formatting string. </param><param name="arg0">The object to write into the format string. </param><param name="arg1">The object to write into the format string. </param><param name="arg2">The object to write into the format string. </param><exception cref="T:System.ArgumentNullException"><paramref name="format"/> is null. </exception><exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.TextWriter"/> is closed. </exception><exception cref="T:System.IO.IOException">An I/O error occurs. </exception><exception cref="T:System.FormatException">The format specification in format is invalid.-or- The number indicating an argument to be formatted is less than zero, or larger than or equal to the number of provided objects to be formatted. </exception><filterpriority>1</filterpriority>
        public override void WriteLine(string format, object arg0, object arg1, object arg2) { throw new NotImplementedException(); }

        /// <summary>
        /// Writes out a formatted string and a new line, using the same semantics as <see cref="M:System.String.Format(System.String,System.Object)"/>.
        /// </summary>
        /// <param name="format">The formatting string. </param><param name="arg">The object array to write into format string. </param><exception cref="T:System.ArgumentNullException">A string or object is passed in as null. </exception><exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.TextWriter"/> is closed. </exception><exception cref="T:System.IO.IOException">An I/O error occurs. </exception><exception cref="T:System.FormatException">The format specification in format is invalid.-or- The number indicating an argument to be formatted is less than zero, or larger than or equal to arg.Length. </exception><filterpriority>1</filterpriority>
        public override void WriteLine(string format, params object[] arg) { throw new NotImplementedException(); }

    }
}