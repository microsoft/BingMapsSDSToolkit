/*
 * Copyright(c) 2017 Microsoft Corporation. All rights reserved. 
 * 
 * This code is licensed under the MIT License (MIT). 
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal 
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies
 * of the Software, and to permit persons to whom the Software is furnished to do 
 * so, subject to the following conditions: 
 * 
 * The above copyright notice and this permission notice shall be included in all
 * copies or substantial portions of the Software. 
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, 
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, 
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
 * THE SOFTWARE. 
*/

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace BingMapsSDSToolkit.Internal
{
    internal class DelimitedFileReader : IDisposable
    {
        #region Private Properties

        private TextReader _stream;

        /// <summary>
        /// End of Stream.
        /// </summary>
        private bool EOS = false;

        /// <summary>
        /// End of line.
        /// </summary>
        private bool EOL = false;

        private char[] _buffer = new char[4096];
        private long _position = 0;
        private long _length = 0;
        private char _delimiter;

        #endregion

        #region Constructor

        public DelimitedFileReader(string stringValue, char delimiter)
        {
            _delimiter = delimiter;
            _stream = new StringReader(stringValue);
        }

        public DelimitedFileReader(TextReader reader, char delimiter)
        {
            _delimiter = delimiter;
            _stream = reader;
        }

        public DelimitedFileReader(Stream stream, char delimiter)
        {
            _delimiter = delimiter;
            _stream = new StreamReader(stream);
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Disposes the DelimitedFileReader.
        /// </summary>
        public void Dispose()
        {
            _stream.Dispose();
            _stream = null;
            _buffer = null;
        }

        /// <summary>
        /// Reads the next row of the delimited file.
        /// </summary>
        /// <returns>An array of strings for each cell of the row</returns>
        public List<string> GetNextRow()
        {
            var row = new List<string>();

            //Loop through every cell until the row has been read.

            string cell = GetNextCell();

            while (cell != null)
            {
                row.Add(cell);
                cell = GetNextCell();
            }

            return row;
        }

        #endregion

        #region Private Methods

        /// <summary>Gets the next cell value from the delimited file.</summary>
        /// <returns>Returns the value of a cell in the delimited file.</returns>
        private string GetNextCell()
        {
            if (EOL || EOS)
            {
                //Reached the end of the line or stream. Reset line flags and return null.
                EOL = false;
                EOS = false;
                return null;
            }

            bool isQuoted = false;
            bool predata = true;
            bool postdata = false;
            var cell = new StringBuilder();

            while (true)
            {
                var character = GetChar(true);

                if (EOS)
                {
                    return cell.Length > 0 ? cell.ToString() : null;
                }

                if ((postdata || !isQuoted) && character == _delimiter)
                {
                    //Reached the end of a quoted cell.
                    return cell.ToString();
                }

                if ((predata || postdata || !isQuoted) && (character == '\x0A' || character == '\x0D'))
                {
                    //We are at the end of the line, eat the newline characters and exit.
                    EOL = true;

                    if (character == '\x0D' && GetChar(false) == '\x0A')
                    {
                        //New line sequence is 0D0A
                        character = GetChar(true);
                    }

                    return cell.ToString();
                }

                if (predata && character == ' ')
                {
                    //Whitespace preceeding data, discard.
                    continue;
                }

                if (predata && character == '"')
                {
                    //Quoted data is starting.
                    isQuoted = true;
                    predata = false;
                    continue;
                }

                if (predata)
                {
                    //Data is starting without quotes.
                    predata = false;
                    cell.Append(character);
                    continue;
                }

                if (character == '"' && isQuoted)
                {
                    if (GetChar(false) == '"')
                    {
                        //Double quotes within quoted string means add a quote.     
                        cell.Append(GetChar(true));
                    }
                    else
                    {
                        //End-quote reached.
                        postdata = true;
                    }

                    continue;
                }

                //All cases covered, character must be data.
                cell.Append(character);
            }
        }

        /// <summary>Gets the character for the current position in the stream unless told to get the next character.</summary>
        /// <param name="getNextChar">Specifies if the next character in the stream should be returned.</param>
        /// <returns>A character from the stream.</returns>
        private char GetChar(bool getNextChar)
        {
            if (_position >= _length)
            {
                _length = _stream.ReadBlock(_buffer, 0, _buffer.Length);

                if (_length == 0)
                {
                    EOS = true;
                    return '\0';
                }

                _position = 0;
            }

            if (getNextChar)
            {
                return _buffer[_position++];
            }
            else
            {
                return _buffer[_position];
            }
        }

        #endregion
    }
}