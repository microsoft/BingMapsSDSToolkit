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


namespace BingMapsSDSToolkit.GeocodeDataflowAPI
{
    /// <summary>
    /// The confidence types of a geocode result.
    /// </summary>
    public static class ConfidenceTypes
    {
        #region Public Properties

        /// <summary>
        /// Low confidence. 
        /// </summary>
        public const string Low = "Low";

        /// <summary>
        /// Medium confidence. 
        /// </summary>
        public const string Medium = "Medium";
        
        /// <summary>
        /// High confidence. 
        /// </summary>
        public const string High = "High";

        #endregion

        #region Public Methods

        /// <summary>
        /// Checks a string confidence filter to see if it is a valid value.
        /// </summary>
        /// <param name="confidence">String confidence value.</param>
        /// <returns>A boolean indicating if the value is valid or not.</returns>
        public static bool IsValid(string confidence)
        {
            return string.Compare(confidence, Low) == 0 ||
                string.Compare(confidence, Medium) == 0 ||
                string.Compare(confidence, High) == 0;
        }

        #endregion
    }
}
