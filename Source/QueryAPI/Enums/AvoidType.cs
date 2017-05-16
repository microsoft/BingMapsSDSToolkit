
namespace BingMapsSDSToolkit.QueryAPI
{
    /// <summary>
    /// Specifies the road types to minimize or avoid when the route is created for the driving travel mode.
    /// </summary>
    public enum AvoidType
    {
        /// <summary>
        /// Avoids the use of highways in the route. 
        /// </summary>
        Highways,

        /// <summary>
        /// Avoids the use of toll roads in the route.
        /// </summary>
        Tolls,

        /// <summary>
        /// Minimizes (tries to avoid) the use of highways in the route.
        /// </summary>
        MinimizeHighways,

        /// <summary>
        /// Minimizes (tries to avoid) the use of toll roads in the route.
        /// </summary>
        MinimizeTolls
    }
}
