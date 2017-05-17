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
    /// An eneity in the the geocode feed to be geocoded or reverse geocoded.
    /// </summary>
    public class GeocodeEntity
    {
        #region Constructor

        /// <summary>
        /// An eneity in the the geocode feed to be geocoded or reverse geocoded.
        /// </summary>
        public GeocodeEntity()
        {
        }

        /// <summary>
        /// An eneity in the the geocode feed to be geocoded or reverse geocoded.
        /// </summary>
        /// <param name="query">A query string that contains address information to geocode.Can be used instead of an address.</param>
        public GeocodeEntity(string query)
        {
            this.GeocodeRequest = new GeocodeRequest(query);
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// A string that contains a unique ID to assign to the entity.
        /// </summary>
        [XmlAttribute]
        public string Id { get; set; }

        /// <summary>
        /// The reverse geocode request being made.
        /// </summary>
        public ReverseGeocodeRequest ReverseGeocodeRequest { get; set; }

        /// <summary>
        /// The geocode request being made.
        /// </summary>
        public GeocodeRequest GeocodeRequest { get; set; }

        /// <summary>
        /// The results of the geocoding/reverse geocoding process. Do not set this property.
        /// </summary>
        [XmlElement]
        public GeocodeResponse[] GeocodeResponse { get; set; }

        /// <summary>
        /// A string that provides information about the success of the operation.
        /// Do not set this property.
        /// </summary>
        public string StatusCode { get; set; }

        /// <summary>
        /// Information about an error that occurred during the geocode dataflow job. 
        /// This value is provided only for data that was not processed successfully.
        /// Do not set this property.
        /// </summary>
        public string FaultReason { get; set; }

        /// <summary>
        /// An id used to trace the request through the Bing servers. Do not set this property.
        /// </summary>
        public string TraceId { get; set; }

        #endregion
    }
}
