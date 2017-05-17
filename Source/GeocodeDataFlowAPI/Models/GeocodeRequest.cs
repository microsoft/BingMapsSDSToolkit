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

using System.Xml.Serialization;

namespace BingMapsSDSToolkit.GeocodeDataflowAPI
{
    /// <summary>
    /// Request information for geocoding a location.
    /// </summary>
    public class GeocodeRequest : BaseRequest
    {
        #region Constructor

        /// <summary>
        /// Request information for geocoding a location.
        /// </summary>
        public GeocodeRequest()
        {
        }

        /// <summary>
        /// Request information for geocoding a location.
        /// </summary>
        /// <param name="query">A query string that contains address information to geocode. Can be used instead of an address.</param>
        public GeocodeRequest(string query)
        {
            this.Query = query;
        }

        #endregion

        /// <summary>
        /// The address to geocode.
        /// </summary>
        public Address Address { get; set; }

        /// <summary>
        /// A boolean value that specifies whether to return parsing information.
        /// </summary>
        [XmlAttribute]
        public bool IncludeQueryParse { get; set; }

        /// <summary>
        /// A boolean indicating if the information on how the query propery was parsed should be returned.
        /// </summary>
        [XmlIgnore]
        public bool IncludeQueryParseSpecified { get; set; }

        /// <summary>
        /// A query string that contains address information to geocode. Can be used instead of an address.
        /// </summary>
        [XmlAttribute]
        public string Query { get; set; }
    }
}
