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

using BingMapsSDSToolkit.Internal;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization.Json;
using System.Threading.Tasks;

namespace BingMapsSDSToolkit.GeodataAPI
{
    /// <summary>
    /// A static class for retrieving boundary data from the Geodata API.
    /// </summary>
    public static class GeodataManager
    {
        /// <summary>
        /// Gets the boundary for the specified request.
        /// </summary>
        /// <param name="request">The boundary request information.</param>
        /// <param name="bingMapsKey">A bing maps key to authenicate the request.</param>
        /// <exception cref="System.Exception">Thrown when reuqired information is mssing from request, or an error occurs.</exception>
        /// <returns>A list of GetDataResult objects.</returns>
        public static async Task<List<GeodataResult>> GetBoundary(GetBoundaryRequest request, string bingMapsKey)
        {
            if (string.IsNullOrWhiteSpace(bingMapsKey))
            {
                throw new Exception("A valid Bing Maps key must be specified.");
            }

            string locationInfo = string.Empty;
            if (request.Coordinate != null)
            {
                locationInfo += request.Coordinate.Latitude + "," + request.Coordinate.Longitude;
            }
            else if (!string.IsNullOrWhiteSpace(request.Address))
            {
                locationInfo = "'" + Uri.EscapeDataString(request.Address) + "'";
            }

            if (string.IsNullOrWhiteSpace(locationInfo))
            {
                throw new Exception("The Coordinate or Address property of the request must be set.");
            }

            string requestUrl = string.Format("https://platform.bing.com/geo/spatial/v1/public/Geodata?SpatialFilter=GetBoundary({0},{1},'{2}',{3},{4},'{5}','{6}')&$format=json&key={7}",
                locationInfo,
                request.LevelOfDetail,
                request.EntityType.ToString(),
                (request.GetAllPolygons) ? 1 : 0,
                (request.GetEntityMetadata) ? 1 : 0,
                request.Culture,
                request.UserRegion,
                bingMapsKey);

            using (var s = await ServiceHelper.GetStreamAsync(new Uri(requestUrl)))
            {
                var ser = new DataContractJsonSerializer(typeof(GeodataResponse));
                var response = ser.ReadObject(s) as GeodataResponse;

                if (response != null &&
                    response.ResultSet != null &&
                    response.ResultSet.Results != null &&
                    response.ResultSet.Results.Length > 0)
                {
                    return new List<GeodataResult>(response.ResultSet.Results);
                }
            }

            return null;
        }
    }
}
