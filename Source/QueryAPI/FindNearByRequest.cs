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
using System.Globalization;

namespace BingMapsSDSToolkit.QueryAPI
{
    /// <summary>
    /// A radial search request for finding results that are near a location.
    /// </summary>
    public class FindNearByRequest : FindByPropertyRequest
    {
        #region Constructor

        /// <summary>
        /// A radial search request for finding results that are near a location.
        /// </summary>
        public FindNearByRequest()
        {
            this._getDistance = true;
        }

        /// <summary>
        /// A radial search request for finding results that are near a location.
        /// </summary>
        /// <param name="info">Basic information about the data source.</param>
        public FindNearByRequest(BasicDataSourceInfo info)
        {
            this._getDistance = true;

            this.QueryURL = info.QueryURL;
            this.MasterKey = info.MasterKey;
            this.QueryKey = info.QueryKey;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// A centeral coordinate to perform the nearby search. Overrides the Address value if both are specified.
        /// </summary>
        public GeodataLocation Center { get; set; }

        /// <summary>
        /// A string address to perform a nearby search around. 
        /// If the Center properties is specified it will override this value when doing the search.
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// The radial search distance.
        /// You must specify the distance in between 0.16 and 1000 kilometers. 
        /// </summary>
        public double Distance { get; set; }
        
        #endregion

        #region Public Methods

        /// <summary>
        /// Retrieves the reuqest URL for the query.
        /// </summary>
        /// <returns>A string URL for the query.</returns>
        public override string GetRequestUrl()
        {
            string spatialFilter;

            double disKm = SpatialTools.ConvertDistance(Distance, DistanceUnits, DistanceUnitType.Kilometers);

            if (disKm < 0.16)
            {
                disKm = 0.16;
            }

            if (disKm > 1000)
            {
                disKm = 1000;
            }

            if(Center != null){
                spatialFilter = string.Format(CultureInfo.InvariantCulture, "?spatialFilter=nearby({0:0.#####},{1:0.#####},{2})", Center.Latitude, Center.Longitude, disKm);
            }else if(!string.IsNullOrWhiteSpace(Address)){
                spatialFilter = string.Format(CultureInfo.InvariantCulture, "?spatialFilter=nearby('{0}',{1})", Uri.EscapeDataString(Address), disKm);
            }
            else
            {
                throw new Exception("Location to search nearby is not specified.");
            }

            return string.Format(base.GetBaseRequestUrl(), spatialFilter);
        }

        #endregion
    }
}
