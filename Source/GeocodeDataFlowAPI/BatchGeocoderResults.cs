
namespace BingMapsSDSToolkit.GeocodeDataFlowAPI
{
    /// <summary>
    /// An object used to store the results of a batch geocoding job.
    /// </summary>
    public class BatchGeocoderResults
    {
        /// <summary>
        /// All the locations the data that was geocoded successfully.
        /// </summary>
        public GeocodeFeed Succeeded { get; set; }

        /// <summary>
        /// All the locations the data that failed to be geocoded.
        /// </summary>
        public GeocodeFeed Failed { get; set; }

        /// <summary>
        /// Error message if unable to process batch geocode job.
        /// </summary>
        public string Error { get; set; }
    }
}
