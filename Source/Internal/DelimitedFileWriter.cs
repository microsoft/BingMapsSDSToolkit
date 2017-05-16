using System.Collections.Generic;
using System.IO;

namespace BingMapsSDSToolkit.Internal
{
    internal class DelimitedFileWriter : StreamWriter
    {
        #region Private Properties

        /// <summary>
        /// Number of columns that the delimited file has.
        /// </summary>
        private int _numColumns;

        private char _delimiter;

        private char[] protectedDelimiters;

        #endregion

        #region Constructor

        /// <summary>
        /// Generates a delimited file from lists of data. 
        /// </summary>
        /// <param name="numColumns">The number of columns in the file</param>
        public DelimitedFileWriter(Stream stream, int numColumns, char delimiter)
            : base(stream)
        {
            _numColumns = numColumns;
            _delimiter = delimiter;

            protectedDelimiters = ("\"\x0A\x0D" + delimiter).ToCharArray();
        }

        /// <summary>
        /// Generates a delimited file from lists of data. 
        /// </summary>
        /// <param name="header">The delimited header file. This must contain a value for all columns.</param>
        public DelimitedFileWriter(Stream stream, IList<string> header, char delimiter)
            : base(stream)
        {
            _numColumns = header.Count;
            _delimiter = delimiter;

            protectedDelimiters = ("\"\x0A\x0D" + delimiter).ToCharArray();

            WriteRow(header, false);
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Writes a list of string as a row in the delimited StringWriter
        /// </summary>
        /// <param name="row">A list of string to add to the StringWriter</param>
        /// <param name="protectDelimiter">Wraps items with quotes if item contains the delimiter. Often true, usually false for headers.</param>
        public void WriteRow(IList<string> row, bool protectDelimiter)
        {
            for (int i = 0; i < _numColumns; i++)
            {
                if (i < row.Count)
                {
                    WriteItem(row[i], protectDelimiter);
                }

                if (i < _numColumns - 1)
                {
                    this.Write(_delimiter);
                }
                else
                {
                    this.Write('\n');
                }
            }
        }

        public void WriteRow(string[] row, bool protectDelimiter)
        {
            for (int i = 0; i < _numColumns; i++)
            {
                if (i < row.Length)
                {
                    WriteItem(row[i], protectDelimiter);
                }

                if (i < _numColumns - 1)
                {
                    this.Write(_delimiter);
                }
                else
                {
                    this.Write('\n');
                }
            }
        }

        /// <summary>
        /// Writes a list of objects as a row in the delimited StringWriter.
        /// </summary>
        /// <param name="row">A list of objects to add to the StringWriter.</param>
        public void WriteRow(IList<object> row, bool protectDelimiter)
        {
            for (int i = 0; i < _numColumns; i++)
            {
                if (i < row.Count)
                {
                    WriteItem(row[i], protectDelimiter);
                }

                if (i < _numColumns - 1)
                {
                    this.Write(_delimiter);
                }
                else
                {
                    this.Write('\n');
                }
            }
        }

        public void WriteRow(object[] row, bool protectDelimiter)
        {
            for (int i = 0; i < _numColumns; i++)
            {
                if (i < row.Length)
                {
                    WriteItem(row[i], protectDelimiter);
                }

                if (i < _numColumns - 1)
                {
                    this.Write(_delimiter);
                }
                else
                {
                    this.Write('\n');
                }
            }
        }

        #endregion

        #region Private Properties

        /// <summary>
        /// Writes an object to the StringWriter.
        /// </summary>
        /// <param name="item">An object to add to the StringWriter.</param>
        private void WriteItem(object item, bool protectDelimiter)
        {
            if (item == null)
            {
                return;
            }

            string s = item.ToString();
            if (protectDelimiter && item is string && s.IndexOfAny(protectedDelimiters) > -1) //enclose any unsafe strings with quotes
            {
                this.Write("\"" + s.Replace("\"", "\"\"") + "\"");
            }
            else
            {
                this.Write(s);
            }
        }

        #endregion
    }
}