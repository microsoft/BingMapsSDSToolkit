using System.Collections.Generic;

namespace BingMapsSDSToolkit.DataSourceAPI
{
    /// <summary>
    /// An object used to store the results of validating a data source.
    /// </summary>
    public class ValidationResult
    {
        #region Constructor

        /// <summary>
        /// An object used to store the results of validating a data source.
        /// </summary>
        public ValidationResult()
        {
            Errors = new List<string>();
            Warnings = new List<string>();
            AllRowsHaveLocationInfo = true;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// A list of error messages. This should be empty for valid data sources.
        /// </summary>
        public List<string> Errors { get; set; }

        /// <summary>
        /// A list of warning messages that may be an issue if uploading a data source. i.e. More than 200,000 rows. Or not Latitue/Longitude values specified.
        /// </summary>
        public List<string> Warnings { get; set; }

        /// <summary>
        /// A boolean value indicating if all rows contain valid location data, either a Latitude/Longitude pair, or a Geography object.
        /// </summary>
        public bool AllRowsHaveLocationInfo { get; set; }

        #endregion
    }
}
