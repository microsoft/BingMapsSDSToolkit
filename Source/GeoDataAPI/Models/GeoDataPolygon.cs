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

namespace BingMapsSDSToolkit.GeodataAPI
{
    /// <summary>
    /// An object storing the parsed bundary infromation as sets for rings for a polygon.
    /// </summary>
    public class GeodataPolygon
    {
        #region Constructor

        /// <summary>
        /// An object storing the parsed bundary infromation as sets for rings for a polygon.
        /// </summary>
        public GeodataPolygon()
        {
            InnerRings = new List<List<GeodataLocation>>();
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// A list of coordinates that make up the exterior ring of a polygon.
        /// </summary>
        public List<GeodataLocation> ExteriorRing { get; set; }

        /// <summary>
        /// A list of list of coordinates that make up all the inner rings of a polygon.
        /// </summary>
        public List<List<GeodataLocation>> InnerRings { get; set; }

        #endregion
    }
}
