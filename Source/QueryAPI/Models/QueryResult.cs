using System;
using System.Collections.Generic;
using System.Linq;

namespace BingMapsSDSToolkit.QueryAPI
{
    /// <summary>
    /// An object the represents a single entity result in the query.
    /// </summary>
    public class QueryResult
    {
        #region Constructor

        /// <summary>
        /// An object the represents a single entity result in the query.
        /// </summary>
        public QueryResult()
        {
            Properties = new Dictionary<string, object>();
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// A url that points to this entity in the data source.
        /// </summary>
        public string EntityUrl { get; set; }

        /// <summary>
        /// The distance the result is from the search query. The distance value will be in the specified distance units or will default to KM.
        /// This value is only set if the request is of type FindNearby, FindInBoundingBox, or FindNearRoute. 
        /// </summary>
        public double Distance { get; set; }

        /// <summary>
        /// The intersected section of the Geography
        /// </summary>
        public Geography IntersectedGeography { get; set; }

        /// <summary>
        /// The location coordinate of the result.
        /// </summary>
        public GeoDataLocation Location { get; set; }

        /// <summary>
        /// Indicates if the result has a property of type Geography or not.
        /// </summary>
        public bool HasGeography { get; set; }

        /// <summary>
        /// A dictionary of additional properties this entity has.
        /// </summary>
        public Dictionary<string, object> Properties { get; set; }

        #endregion

        #region Public Methods

        /// <summary>
        /// Gets first property of type geography for the result.
        /// </summary>
        /// <returns></returns>
        public Geography GetGeography()
        {
            if(HasGeography)
            {
                return (from p in this.Properties
                        where p.Value is Geography
                        select p.Value as Geography).FirstOrDefault();
            }

            return null;
        }

        #endregion
    }
}
