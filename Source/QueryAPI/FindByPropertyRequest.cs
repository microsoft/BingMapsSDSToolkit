using System;
using System.Text;

namespace BingMapsSDSToolkit.QueryAPI
{
    /// <summary>
    /// A query that searches by property. This is also the basis to all other queries.
    /// </summary>
    public class FindByPropertyRequest : BasicDataSourceInfo
    {
        #region Private Properties

        private int _top = 25;
        private int _skip = 0;
        public DistanceUnitType _distanceUnits = DistanceUnitType.KM;
        
        internal bool _isIdRequest = false;
        internal bool _getDistance = false;

        #endregion

        #region Constructor

        /// <summary>
        /// A query that searches by property.
        /// </summary>
        public FindByPropertyRequest()
        {
        }

        /// <summary>
        /// A query that searches by property.
        /// </summary>
        /// <param name="info">Basic information about the data source.</param>
        public FindByPropertyRequest(BasicDataSourceInfo info)
        {
            this.QueryURL = info.QueryURL;
            this.MasterKey = info.MasterKey;
            this.QueryKey = info.QueryKey;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// A boolean value indicating if the staging version of the data source should be accessed or not.
        /// </summary>
        public bool IsStaging { get; set; }
        
        /// <summary>
        /// The distance units to use with the query.
        /// </summary>
        public DistanceUnitType DistanceUnits
        {
            get { return _distanceUnits; }
            set { _distanceUnits = value; }
        }

        /// <summary>
        /// An integer value between 1 and 250 that represents the maximium results that can be returned. Default 25. 
        /// </summary>
        public int Top
        {
            get { return _top; }
            set
            {
                if (value > 0 && _top <= 250)
                {
                    _top = value;
                }
            }
        }

        /// <summary>
        /// Specifies the number of results to skip. This lets you page through the results.
        /// </summary>
        public int Skip
        {
            get { return _skip; }
            set
            {
                if (value > 0)
                {
                    _skip = value;
                }
            }
        }

        /// <summary>
        /// A string that contains a comma delimited list of data source properties to return.
        /// </summary>
        public string Select { get; set; }

        /// <summary>
        /// A comma delimited list of data source properties to use to sort the results. (up to 3 properties can be specified).
        /// </summary>
        public string Orderby { get; set; }

        /// <summary>
        /// A boolean that specifies whether or not to return a count of the results in the response. Default: false.
        /// </summary>
        public bool InlineCount { get; set; }

        /// <summary>
        /// A filter that specifies conditional expressions for a list of properties and values. 
        /// </summary>
        public IFilter Filter { get; set; }

        #endregion

        #region Public Methods

        /// <summary>
        /// Gets a URL to query the Bing Spatial Data Services.
        /// </summary>
        /// <returns>A URL to query the Bing Spatial Data Services.</returns>
        public virtual string GetRequestUrl()
        {
            return GetBaseRequestUrl();
        }

        /// <summary>
        /// Generates the base reuqest URL that includes everything but the spatial filter.
        /// </summary>
        /// <returns></returns>
        public string GetBaseRequestUrl()
        {
            if (string.IsNullOrWhiteSpace(AccessId))
            {
                throw new Exception("Data source access id not specified.");
            }
            
            if (string.IsNullOrWhiteSpace(DataSourceName))
            {
                throw new Exception("Data source name id not specified.");
            }

            if (string.IsNullOrWhiteSpace(EntityTypeName))
            {
                throw new Exception("Data source entity type name id not specified.");
            }

            if (string.IsNullOrWhiteSpace(QueryKey) && string.IsNullOrWhiteSpace(MasterKey))
            {
                throw new Exception("Query key not specified.");
            }

            var sb = new StringBuilder();
            sb.Append("https://spatial.virtualearth.net/REST/v1/data/");
            sb.Append(AccessId);
            sb.Append("/");
            sb.Append(DataSourceName);
            sb.Append("/");
            sb.Append(EntityTypeName);
            sb.Append("{0}");

            if (!string.IsNullOrWhiteSpace(Select))
            {
                if (_getDistance)
                {
                    sb.AppendFormat("&$select={0},__Distance", Select);
                }
                else
                {
                    sb.AppendFormat("&$select={0}", Select);
                }
            }
            else if (_getDistance)
            {
                sb.AppendFormat("&$select=*,__Distance", Select);
            }

            if (!string.IsNullOrWhiteSpace(Orderby))
            {
                sb.AppendFormat("&$orderby={0}", Orderby);
            }

            if (IsStaging)
            {
                sb.Append("&isStaging=1");
            }

            if (!_isIdRequest)
            {
                if (_top != 25)
                {
                    sb.AppendFormat("&$top={0}", _top);
                }

                if (_skip != 0)
                {
                    sb.AppendFormat("&$skip={0}", _skip);
                }
                
                if (InlineCount)
                {
                    sb.Append("&$inlinecount=allpages");
                }

                if (Filter != null)
                {
                    sb.AppendFormat("&$filter={0}", Filter.ToString());
                }
            }

            sb.AppendFormat("&key={0}", string.IsNullOrEmpty(QueryKey)? MasterKey: QueryKey);

            return sb.ToString();
        }

        #endregion
    }
}
