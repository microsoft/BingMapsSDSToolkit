
namespace BingMapsSDSToolkit.GeocodeDataflowAPI
{
    /// <summary>
    /// The confidence types of a geocode result.
    /// </summary>
    public static class ConfidenceTypes
    {
        #region Public Properties

        /// <summary>
        /// Low confidence. 
        /// </summary>
        public const string Low = "Low";

        /// <summary>
        /// Medium confidence. 
        /// </summary>
        public const string Medium = "Medium";
        
        /// <summary>
        /// High confidence. 
        /// </summary>
        public const string High = "High";

        #endregion

        #region Public Methods

        /// <summary>
        /// Checks a string confidence filter to see if it is a valid value.
        /// </summary>
        /// <param name="confidence">String confidence value.</param>
        /// <returns>A boolean indicating if the value is valid or not.</returns>
        public static bool IsValid(string confidence)
        {
            return string.Compare(confidence, Low) == 0 ||
                string.Compare(confidence, Medium) == 0 ||
                string.Compare(confidence, High) == 0;
        }

        #endregion
    }
}
