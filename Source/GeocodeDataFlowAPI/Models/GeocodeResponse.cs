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
    /// A geocode response object.
    /// </summary>
    public class GeocodeResponse
    {
        /// <summary>
        /// The geocoded address result.
        /// </summary>
        public Address Address { get; set; }

        /// <summary>
        /// An array of coordinates for the resulting locaiton.
        /// </summary>
        [XmlElement]
        public GeocodePoint[] GeocodePoint { get; set; }

        /// <summary>
        /// An array of the parsed query properties.
        /// </summary>
        [XmlElement]
        public QueryParseValue[] QueryParseValue { get; set; }

        /// <summary>
        /// A bounding box for viewing the location.
        /// </summary>
        public BoundingBox BoundingBox { get; set; }

        /// <summary>
        /// A coordinate for the geocoded location.
        /// </summary>
        public GeodataLocation Point { get; set; }

        /// <summary>
        /// The name of the geocoded location.
        /// </summary>
        [XmlAttribute]
        public string Name { get; set; }

        /// <summary>
        /// The geocode entity type of the geocoded location.
        /// </summary>
        [XmlAttribute]
        public string EntityType { get; set; }

        /// <summary>
        /// The confidence of the geococded results.
        /// </summary>
        [XmlAttribute]
        public string Confidence { get; set; }

        /// <summary>
        /// A comma seperated string of match codes. Use the MatchCodeTypes.ParseMatchCodes method to parse into an array of strings.
        /// </summary>
        [XmlAttribute]
        public string MatchCodes { get; set; }
    }
}
