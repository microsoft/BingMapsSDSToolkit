
using System.Globalization;
namespace BingMapsSDSToolkit.QueryAPI
{
    /// <summary>
    /// A search request that looks for locations that are inside a bounding box.
    /// </summary>
    public class FindInBoundingBoxRequest: FindByPropertyRequest
    {
        #region Private Properties

        private double north = 90, south = 90, east = 180, west = -180;

        #endregion

        #region Constructor

        /// <summary>
        /// A search request that looks for locations that are inside a bounding box.
        /// </summary>
        public FindInBoundingBoxRequest()
        {
            this._getDistance = true;
        }

        /// <summary>
        /// A search request that looks for locations that are inside a bounding box.
        /// </summary>
        /// <param name="info">Basic information about the data source.</param>
        public FindInBoundingBoxRequest(BasicDataSourceInfo info)
        {
            this._getDistance = true;

            this.QueryURL = info.QueryURL;
            this.MasterKey = info.MasterKey;
            this.QueryKey = info.QueryKey;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// The northern most latitude of the bounding box. Value between -90 and 90.
        /// </summary>
        public double North
        {
            get { return north; }
            set
            {
                if (!double.IsNaN(value) && value <= 90 && value >= -90)
                {
                    north = value;
                }
            }
        }

        /// <summary>
        /// The southern most latitude of the bounding box. Value between -90 and 90.
        /// </summary>
        public double South
        {
            get { return south; }
            set
            {
                if (!double.IsNaN(value) && value <= 90 && value >= -90)
                {
                    south = value;
                }
            }
        }

        /// <summary>
        /// The eastern most longitude of the bounding box. Value between -180 and 180.
        /// </summary>
        public double East
        {
            get { return east; }
            set
            {
                if (!double.IsNaN(value) && value <= 180 && value >= -180)
                {
                    east = value;
                }
            }
        }

        /// <summary>
        /// The western most longitude of the bounding box. Value between -180 and 180.
        /// </summary>
        public double West
        {
            get { return west; }
            set
            {
                if (!double.IsNaN(value) && value <= 180 && value >= -180)
                {
                    west = value;
                }
            }
        }
        
        #endregion

        #region Public Methods

        /// <summary>
        /// Retrieves the reuqest URL for the query.
        /// </summary>
        /// <returns>A string URL for the query.</returns>
        public override string GetRequestUrl()
        {
            string spatialFilter = string.Format(CultureInfo.InvariantCulture, "?spatialFilter=bbox({0:0.#####},{1:0.#####},{2:0.#####},{3:0.#####})", south, west, north, east);
            return string.Format(base.GetBaseRequestUrl(), spatialFilter);
        }

        #endregion
    }
}
