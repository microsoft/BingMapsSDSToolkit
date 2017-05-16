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
