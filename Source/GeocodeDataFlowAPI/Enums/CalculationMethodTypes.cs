
namespace BingMapsSDSToolkit.GeocodeDataflowAPI
{
    /// <summary>
    /// The types of calculations methods used for location coodinates.
    /// </summary>
    public static class CalculationMethodTypes
    {
        /// <summary>
        /// The geocode point was matched to a point on a road using interpolation.
        /// </summary>
        public const string Interpolation = "Interpolation";

        /// <summary>
        /// The geocode point was matched to a point on a road using interpolation with an additional offset to shift the point to the side of the street.
        /// </summary>
        public const string InterpolationOffset = "InterpolationOffset";

        /// <summary>
        /// The geocode point was matched to the center of a parcel.
        /// </summary>
        public const string ParcelCentroid = "ParcelCentroid";

        /// <summary>
        /// The geocode point was matched to the rooftop of a building.
        /// </summary>
        public const string Rooftop = "Rooftop";
    }
}
