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

using System.Collections.Generic;

namespace BingMapsSDSToolkit.DataSourceAPI
{
    /// <summary>
    /// An object used to store the results of validating a data source.
    /// </summary>
    public class ValidationResult
    {
        #region Constructor

        /// <summary>
        /// An object used to store the results of validating a data source.
        /// </summary>
        public ValidationResult()
        {
            Errors = new List<string>();
            Warnings = new List<string>();
            AllRowsHaveLocationInfo = true;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// A list of error messages. This should be empty for valid data sources.
        /// </summary>
        public List<string> Errors { get; set; }

        /// <summary>
        /// A list of warning messages that may be an issue if uploading a data source. i.e. More than 200,000 rows. Or not Latitue/Longitude values specified.
        /// </summary>
        public List<string> Warnings { get; set; }

        /// <summary>
        /// A boolean value indicating if all rows contain valid location data, either a Latitude/Longitude pair, or a Geography object.
        /// </summary>
        public bool AllRowsHaveLocationInfo { get; set; }

        #endregion
    }
}
