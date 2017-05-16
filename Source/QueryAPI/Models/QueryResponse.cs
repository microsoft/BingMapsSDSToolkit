using System.Collections.Generic;

namespace BingMapsSDSToolkit.QueryAPI
{
    /// <summary>
    /// An objet that represents the response from a query.
    /// </summary>
    public class QueryResponse
    {
        #region Constructor

        /// <summary>
        /// An objet that represents the response from a query.
        /// </summary>
        public QueryResponse()
        {
            Results = new List<QueryResult>();
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// An array of query results.
        /// </summary>
        public List<QueryResult> Results { get; set; }

        /// <summary>
        /// A string containing an error message if an error occurs while processing the query.
        /// </summary>
        public string ErrorMessage { get; set; }

        #endregion
    }
}
