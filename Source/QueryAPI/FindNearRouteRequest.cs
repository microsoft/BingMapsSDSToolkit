using System;
using System.Globalization;
using System.Text;

namespace BingMapsSDSToolkit.QueryAPI
{
    /// <summary>
    /// A search query that looks for locations that are within 1 mile or 1.6 kilometers of a route.
    /// </summary>
    public class FindNearRouteRequest: FindByPropertyRequest
    {
        #region Constructor

        /// <summary>
        /// A search query that looks for locations that are within 1 mile or 1.6 kilometers of a route.
        /// </summary>
        public FindNearRouteRequest()
        {
            this._getDistance = true;
            Optimize = RouteOptimizationType.Time;
            TravelMode = TravelModeType.Driving;
        }

        /// <summary>
        /// A search query that looks for locations that are within 1 mile or 1.6 kilometers of a route.
        /// </summary>
        /// <param name="info">Basic information about the data source.</param>
        public FindNearRouteRequest(BasicDataSourceInfo info)
        {
            this._getDistance = true;

            this.QueryURL = info.QueryURL;
            this.MasterKey = info.MasterKey;
            this.QueryKey = info.QueryKey;

            Optimize = RouteOptimizationType.Time;
            TravelMode = TravelModeType.Driving;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// A coordinate to use as the starting location of the route. Overrides the StartAddress value if both are specified.
        /// </summary>
        public GeodataLocation StartLocation { get; set; }

        /// <summary>
        /// A string address to use as the starting location of the route.
        /// If the StartLocation properties is specified it will override this value when doing the search.
        /// </summary>
        public string StartAddress { get; set; }

        /// <summary>
        /// A coordinate to use as the end location of the route. Overrides the EndAddress value if both are specified.
        /// </summary>
        public GeodataLocation EndLocation { get; set; }

        /// <summary>
        /// A string address to use as the end location of the route.
        /// If the EndLocation properties is specified it will override this value when doing the search.
        /// </summary>
        public string EndAddress { get; set; }

        /// <summary>
        /// Specifies the road types to minimize or avoid when the route is created for the driving travel mode. 
        /// </summary>
        public AvoidType[] Avoid { get; set; }

        /// <summary>
        /// Specifies the distance before the first turn is allowed in the route. This option only applies to the driving travel mode.
        /// </summary>
        public int DistanceBeforeFirstTurn { get; set; }

        /// <summary>
        /// Specifies the initial heading for the route. An integer value between 0 and 359 that represents degrees from north 
        /// where north is 0 degrees and the heading is specified clockwise from north. For example, setting the heading of 270 
        /// degrees creates a route that initially heads west.
        /// </summary>
        public int Heading { get; set; }

        /// <summary>
        /// Specifies what parameters to use to optimize the route on the map. Default: Time.
        /// </summary>
        public RouteOptimizationType Optimize { get; set; }

        /// <summary>
        /// The mode of travel for the route. Default: Driving. 
        /// </summary>
        public TravelModeType TravelMode { get; set; } 

        #endregion

        #region Public Methods

        /// <summary>
        /// Retrieves the reuqest URL for the query.
        /// </summary>
        /// <returns>A string URL for the query.</returns>
        public override string GetRequestUrl()
        {
            string start = string.Empty, end = string.Empty;

            if (StartLocation != null)
            {
                start = string.Format(CultureInfo.InvariantCulture, "'{0:0.#####},{1:0.#####}'", StartLocation.Latitude, StartLocation.Longitude);
            }
            else if (!string.IsNullOrWhiteSpace(StartAddress))
            {
                start = string.Format("'{0}'", Uri.EscapeDataString(StartAddress));
            }
            else
            {
                throw new Exception("Start location or address not specified.");
            }

            if (EndLocation != null)
            {
                end = string.Format(CultureInfo.InvariantCulture, "'{0:0.#####},{1:0.#####}'", EndLocation.Latitude, EndLocation.Longitude);
            }
            else if (!string.IsNullOrWhiteSpace(EndAddress))
            {
                end = string.Format("'{0}'", Uri.EscapeDataString(EndAddress));
            }
            else
            {
                throw new Exception("End location or address not specified.");
            }

            var routeOptions = new StringBuilder();

            if (TravelMode == TravelModeType.Driving)
            {
                routeOptions.Append("&travelMode=Driving");

                if (Avoid != null && Avoid.Length > 0)
                {
                    routeOptions.Append("&avoid=");
                    for (int i = 0; i < Avoid.Length; i++)
                    {
                        if (i < Avoid.Length - 1)
                        {
                            routeOptions.AppendFormat("{0},",Avoid[i]);
                        }
                        else
                        {
                            routeOptions.Append(Avoid[i]);
                        }
                    }
                }

                if (DistanceBeforeFirstTurn > 0)
                {
                    routeOptions.AppendFormat("&dbft={0}", DistanceBeforeFirstTurn);
                }

                if (Heading > 0 && Heading < 360)
                {
                    routeOptions.AppendFormat("&hd={0}", Heading);
                }
            }
            else
            {
                routeOptions.Append("&travelMode=Walking");
            }

            routeOptions.AppendFormat("&optmz={0}", Optimize);

            string spatialFilter = string.Format("?spatialFilter=nearRoute({0},{1}){2}", start, end, routeOptions.ToString());
            return string.Format(base.GetBaseRequestUrl(), spatialFilter);
        }

        #endregion
    }
}
