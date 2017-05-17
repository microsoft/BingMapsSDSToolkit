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
    /// An object for storing the results of geocoding a data source.
    /// </summary>
    public class DataSourceGeocodeResults
    {
        #region Constructor

        /// <summary>
        /// An object for storing the results of geocoding a data source.
        /// </summary>
        public DataSourceGeocodeResults()
        {
            FailedRows = new List<string>();
            Failed = 0;
            Succeeded = 0;
            Error = string.Empty;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// A list of all the primary keys, of the rows that faile to geocode.
        /// </summary>
        public List<string> FailedRows { get; set; }

        /// <summary>
        /// Number of rows that failed to geocode.
        /// </summary>
        public int Failed { get; set; }

        /// <summary>
        /// Number of rows that geocoded successfully.
        /// </summary>
        public int Succeeded { get; set; }

        /// <summary>
        /// Error during batch geocoding process.
        /// </summary>
        public string Error { get; set; }

        #endregion
    }
}
