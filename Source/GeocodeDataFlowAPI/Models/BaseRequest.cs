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

using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace BingMapsSDSToolkit.GeocodeDataflowAPI
{
    [KnownType(typeof(GeocodeRequest))]
    [KnownType(typeof(ReverseGeocodeRequest))]
    public class BaseRequest
    {
        #region Private Properties

        private int _maxResults;
        private ConfidenceFilter _confidenceFilter;

        #endregion
        
        #region Public Properties

        /// <summary>
        /// A string specifying the minimum confidence required for the result. 
        /// </summary>
        public ConfidenceFilter ConfidenceFilter
        {
            get
            {
                return _confidenceFilter;
            }
            set
            {
                _confidenceFilter = value;
            }
        }

        /// <summary>
        /// A string specifying the culture. 
        /// </summary>
        [XmlAttribute]
        public string Culture { get; set; }

        /// <summary>
        /// A boolean value that specifies whether to return neighborhood information in the address.
        /// </summary>
        [XmlAttribute]
        public bool IncludeNeighborhood { get; set; }

        [XmlIgnore]
        public bool IncludeNeighborhoodSpecified;

        /// <summary>
        /// An integer from 1 to 20 specifying the maximum number of results to return.
        /// </summary>
        [XmlAttribute]
        public int MaxResults
        {
            get { return _maxResults; }
            set
            {
                _maxResults = (value <= 1) ? 1 : ((value > 20) ? 20 : value);
            }
        }

        [XmlIgnore]
        public bool MaxResultsSpecified;

        #endregion
    }
}
