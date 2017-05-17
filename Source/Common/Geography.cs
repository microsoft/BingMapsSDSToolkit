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

using System.Text.RegularExpressions;

namespace BingMapsSDSToolkit
{
    /// <summary>
    /// An object used to represent a geography column type.
    /// </summary>
    public class Geography
    {
        #region Public Properties

        /// <summary>
        /// The well known text that describes the geography object.
        /// </summary>
        public string WellKnownText { get; set; }

        #endregion

        #region Public Methods

        public override string ToString()
        {
            return WellKnownText;
        }

        /// <summary>
        /// Calculates the number of coordinate pairs in the geography.
        /// </summary>
        /// <returns>The number of cooridnate pairs in the geography.</returns>
        public int NumPoints()
        {
            if (string.IsNullOrEmpty(this.WellKnownText))
            {
                //For performance use a regular expression.
                var coordRx = new Regex(@"-?[0-9]+\.?[0-9]* -?[0-9]+\.?[0-9]*");
                var m = coordRx.Matches(this.WellKnownText);

                if (m != null)
                {
                    return m.Count;
                }
            }

            return 0;
        }

        #endregion
    }
}
