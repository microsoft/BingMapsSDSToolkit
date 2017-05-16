
namespace BingMapsSDSToolkit.QueryAPI
{
    /// <summary>
    /// Specifies what parameters to use to optimize the route on the map.
    /// </summary>
    public enum RouteOptimizationType
    {
        /// <summary>
        /// Optimizes route for shortest distance.
        /// </summary>
        Distance,

        /// <summary>
        /// Optimizes route for shortst travel time.
        /// </summary>
        Time, 

        /// <summary>
        /// Optimizes route for shortst travel time with respect to current traffic conditions.
        /// </summary>
        TimeWithTraffic
    }
}
