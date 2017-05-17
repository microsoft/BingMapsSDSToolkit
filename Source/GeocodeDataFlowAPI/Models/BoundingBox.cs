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
using System.Xml.Serialization;

namespace BingMapsSDSToolkit.GeocodeDataflowAPI
{
    /// <summary>
    /// A set of geographical coordinates in degrees that define an area on the Earth that contains the location. 
    /// </summary>
    public class BoundingBox
    {
        #region Private Properties

        private double _southLatitude, _westLongitude, _northLatitude, _eastLongitude;

        #endregion

        #region Public Properties

        /// <summary>
        /// The southern most latitude.
        /// </summary>
        [XmlAttribute]
        public double SouthLatitude
        {
            get
            {
                return _southLatitude;
            }
            set
            {
                if (!double.IsNaN(value) && value <= 90 && value >= -90)
                {
                    //Only need to keep the first 5 decimal places. Any more just adds more data being passed around.
                    _southLatitude = Math.Round(value, 5, MidpointRounding.AwayFromZero);
                }
            }
        }

        /// <summary>
        /// The western most longitude.
        /// </summary>
        [XmlAttribute]
        public double WestLongitude
        {
            get
            {
                return _westLongitude;
            }
            set
            {
                if (!double.IsNaN(value) && value <= 180 && value >= -180)
                {
                    //Only need to keep the first 5 decimal places. Any more just adds more data being passed around.
                    _westLongitude = Math.Round(value, 5, MidpointRounding.AwayFromZero);
                }
            }
        }

        /// <summary>
        /// The northern most latitude.
        /// </summary>
        [XmlAttribute]
        public double NorthLatitude
        {
            get
            {
                return _northLatitude;
            }
            set
            {
                if (!double.IsNaN(value) && value <= 90 && value >= -90)
                {
                    //Only need to keep the first 5 decimal places. Any more just adds more data being passed around.
                    _northLatitude = Math.Round(value, 5, MidpointRounding.AwayFromZero);
                }
            }
        }

        /// <summary>
        /// The eastern most longitude.
        /// </summary>
        [XmlAttribute]
        public double EastLongitude
        {
            get
            {
                return _eastLongitude;
            }
            set
            {
                if (!double.IsNaN(value) && value <= 180 && value >= -180)
                {
                    //Only need to keep the first 5 decimal places. Any more just adds more data being passed around.
                    _eastLongitude = Math.Round(value, 5, MidpointRounding.AwayFromZero);
                }
            }
        }

        #endregion
    }
}
