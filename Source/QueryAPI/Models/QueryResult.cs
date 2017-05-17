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
using System.Linq;

namespace BingMapsSDSToolkit.QueryAPI
{
    /// <summary>
    /// An object that represents a single entity result in the query.
    /// </summary>
    public class QueryResult
    {
        #region Constructor

        /// <summary>
        /// An object the represents a single entity result in the query.
        /// </summary>
        public QueryResult()
        {
            Properties = new Dictionary<string, object>();
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// A url that points to this entity in the data source.
        /// </summary>
        public string EntityUrl { get; set; }

        /// <summary>
        /// The distance the result is from the search query. The distance value will be in the specified distance units or will default to KM.
        /// This value is only set if the request is of type FindNearby, FindInBoundingBox, or FindNearRoute. 
        /// </summary>
        public double Distance { get; set; }

        /// <summary>
        /// The intersected section of the Geography
        /// </summary>
        public Geography IntersectedGeography { get; set; }

        /// <summary>
        /// The location coordinate of the result.
        /// </summary>
        public GeodataLocation Location { get; set; }

        /// <summary>
        /// Indicates if the result has a property of type Geography or not.
        /// </summary>
        public bool HasGeography { get; set; }

        /// <summary>
        /// A dictionary of additional properties this entity has.
        /// </summary>
        public Dictionary<string, object> Properties { get; set; }

        #endregion

        #region Public Methods

        /// <summary>
        /// Gets first property of type geography for the result.
        /// </summary>
        /// <returns></returns>
        public Geography GetGeography()
        {
            if(HasGeography)
            {
                return (from p in this.Properties
                        where p.Value is Geography
                        select p.Value as Geography).FirstOrDefault();
            }

            return null;
        }

        #endregion
    }
}
