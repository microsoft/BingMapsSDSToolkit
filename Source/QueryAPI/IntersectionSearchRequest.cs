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

namespace BingMapsSDSToolkit.QueryAPI
{
    /// <summary>
    /// A search query which looks to see if and locations intersect the specified Geography.
    /// </summary>
    public class IntersectionSearchRequest : FindByPropertyRequest
    {
        #region Private Properties

        private int _maxPoints = 250;

        #endregion

        #region Constructor

        /// <summary>
        /// A search query which looks to see if and locations intersect the specified Geography.
        /// </summary>
        public IntersectionSearchRequest()
        {
        }

        /// <summary>
        /// A search query which looks to see if and locations intersect the specified Geography.
        /// </summary>
        /// <param name="info">Basic information about the data source.</param>
        public IntersectionSearchRequest(BasicDataSourceInfo info)
        {
            this.QueryURL = info.QueryURL;
            this.MasterKey = info.MasterKey;
            this.QueryKey = info.QueryKey;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// The Geography to do an intersection test against.
        /// </summary>
        public Geography Geography { get; set; }
        
        #endregion

        #region Public Methods

        /// <summary>
        /// Retrieves the reuqest URL for the query.
        /// </summary>
        /// <returns>A string URL for the query.</returns>
        public override string GetRequestUrl()
        {
            if (Geography == null || string.IsNullOrWhiteSpace(Geography.WellKnownText))
            {
                throw new Exception("Geography is invalid.");
            }
            else if (Geography.NumPoints() > _maxPoints)
            {
                throw new Exception("Geography exceeds limit of " + _maxPoints + " data points.");
            }

            string spatialFilter = string.Format("?spatialFilter=intersects('{0}')", Geography.ToString());
            return string.Format(base.GetBaseRequestUrl(), spatialFilter);
        }

        #endregion
    }
}
