using System.Collections.Generic;

namespace BingMapsSDSToolkit.DataSourceAPI
{
    /// <summary>
    /// An object for storing the results of geocoding a data source.
    /// </summary>
    public class DataSourceGeocodeResults
    {
        #region Constructor

        /// <summary>
        /// An object for storing the results of geocoding a data source.
        /// </summary>
        public DataSourceGeocodeResults()
        {
            FailedRows = new List<string>();
            Failed = 0;
            Succeeded = 0;
            Error = string.Empty;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// A list of all the primary keys, of the rows that faile to geocode.
        /// </summary>
        public List<string> FailedRows { get; set; }

        /// <summary>
        /// Number of rows that failed to geocode.
        /// </summary>
        public int Failed { get; set; }

        /// <summary>
        /// Number of rows that geocoded successfully.
        /// </summary>
        public int Succeeded { get; set; }

        /// <summary>
        /// Error during batch geocoding process.
        /// </summary>
        public string Error { get; set; }

        #endregion
    }
}
