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
    /// An object that contains information about the boundary, such as the name and culture.
    /// </summary>
    [DataContract]
    public class Name
    {
        /// <summary>
        /// The name of boundary. 
        /// </summary>
        /// <example>United States</example>
        [DataMember(Name = "EntityName", EmitDefaultValue = false)]
        public string EntityName { get; set; }

        /// <summary>
        /// The culture of the region.
        /// </summary>
        [DataMember(Name = "Culture", EmitDefaultValue = false)]
        public string Culture { get; set; }

        /// <summary>
        /// An ID identifying the data provider that supplied the data. 
        /// </summary>
        [DataMember(Name = "SourceID", EmitDefaultValue = false)]
        public string SourceID { get; set; }
    }

}
