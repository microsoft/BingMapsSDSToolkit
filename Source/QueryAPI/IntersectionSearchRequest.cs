using System;

namespace BingMapsSDSToolkit.QueryAPI
{
    /// <summary>
    /// A search query which looks to see if and locations intersect the specified Geography.
    /// </summary>
    public class IntersectionSearchRequest : FindByPropertyRequest
    {
        #region Private Properties

        private int _maxPoints = 250;

        #endregion

        #region Constructor

        /// <summary>
        /// A search query which looks to see if and locations intersect the specified Geography.
        /// </summary>
        public IntersectionSearchRequest()
        {
        }

        /// <summary>
        /// A search query which looks to see if and locations intersect the specified Geography.
        /// </summary>
        /// <param name="info">Basic information about the data source.</param>
        public IntersectionSearchRequest(BasicDataSourceInfo info)
        {
            this.QueryURL = info.QueryURL;
            this.MasterKey = info.MasterKey;
            this.QueryKey = info.QueryKey;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// The Geography to do an intersection test against.
        /// </summary>
        public Geography Geography { get; set; }
        
        #endregion

        #region Public Methods

        /// <summary>
        /// Retrieves the reuqest URL for the query.
        /// </summary>
        /// <returns>A string URL for the query.</returns>
        public override string GetRequestUrl()
        {
            if (Geography == null || string.IsNullOrWhiteSpace(Geography.WellKnownText))
            {
                throw new Exception("Geography is invalid.");
            }
            else if (Geography.NumPoints() > _maxPoints)
            {
                throw new Exception("Geography exceeds limit of " + _maxPoints + " data points.");
            }

            string spatialFilter = string.Format("?spatialFilter=intersects('{0}')", Geography.ToString());
            return string.Format(base.GetBaseRequestUrl(), spatialFilter);
        }

        #endregion
    }
}
