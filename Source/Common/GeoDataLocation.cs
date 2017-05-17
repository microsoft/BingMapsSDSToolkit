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
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace BingMapsSDSToolkit
{
    /// <summary>
    /// An object that stores the coordinate information.
    /// </summary>
    [KnownType(typeof(BingMapsSDSToolkit.GeocodeDataflowAPI.GeocodePoint))]
    public class GeodataLocation
    {
        #region Private Properties

        private double _latitude, _longitude;

        #endregion

        #region Constructor

        /// <summary>
        /// A location coordinate.
        /// </summary>
        public GeodataLocation()
        {
        }

        /// <summary>
        /// A location coordinate.
        /// </summary>
        /// <param name="latitude">Latitude coordinate vlaue.</param>
        /// <param name="longitude">Longitude coordinate value.</param>
        public GeodataLocation(double latitude, double longitude)
        {
            Latitude = latitude;
            Longitude = longitude;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Latitude coordinate.
        /// </summary>
        [XmlAttribute]
        public double Latitude
        {
            get
            {
                return _latitude;
            }
            set
            {
                if (!double.IsNaN(value) && value <= 90 && value >= -90)
                {
                    //Only need to keep the first 5 decimal places. Any more just adds more data being passed around.
                    _latitude = Math.Round(value, 5, MidpointRounding.AwayFromZero);
                }
            }
        }

        /// <summary>
        /// Longitude coordinate.
        /// </summary>
        [XmlAttribute]
        public double Longitude
        {
            get
            {
                return _longitude;
            }
            set
            {
                if (!double.IsNaN(value) && value <= 180 && value >= -180)
                {
                    //Only need to keep the first 5 decimal places. Any more just adds more data being passed around.
                    _longitude = Math.Round(value, 5, MidpointRounding.AwayFromZero);
                }
            }
        }

        #endregion
    }
}
