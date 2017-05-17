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

namespace BingMapsSDSToolkit.GeodataAPI
{
    /// <summary>
    /// An object that contains metadata about a boundary. Not all fields will always be populated.
    /// </summary>
    [DataContract]
    public class Metadata
    {
        /// <summary>
        /// The approximate total surface area (in square kilometers) covered by all the polygons that comprise this entity.
        /// </summary>
        [DataMember(Name = "AreaSqKm", EmitDefaultValue = false)]
        public string AreaSqKm { get; set; }

        /// <summary>
        /// An area on the Earth that provides the best map view for this entity. This area is defined as a 
        /// bounding box which represents an area using a set of latitude and longitude values: South Latitude, 
        /// East Longitude, North Latitude, East Longitude. 
        /// </summary>
        [DataMember(Name = "BestMapViewBox", EmitDefaultValue = false)]
        public string BestMapViewBox { get; set; }

        /// <summary>
        /// The culture associated with this entity.
        /// </summary>
        [DataMember(Name = "OfficialCulture", EmitDefaultValue = false)]
        public string OfficialCulture { get; set; }

        /// <summary>
        /// The regional culture associated with this entity.
        /// </summary>
        [DataMember(Name = "RegionalCulture", EmitDefaultValue = false)]
        public string RegionalCulture { get; set; }

        /// <summary>
        /// The approximate population within this entity. 
        /// </summary>
        /// <example>PopClass20000to99999</example>
        [DataMember(Name = "PopulationClass", EmitDefaultValue = false)]
        public string PopulationClass { get; set; }
    }
}
