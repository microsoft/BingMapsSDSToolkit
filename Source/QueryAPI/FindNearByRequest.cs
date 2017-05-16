using System;
using System.Globalization;

namespace BingMapsSDSToolkit.QueryAPI
{
    /// <summary>
    /// A radial search request for finding results that are near a location.
    /// </summary>
    public class FindNearByRequest : FindByPropertyRequest
    {
        #region Constructor

        /// <summary>
        /// A radial search request for finding results that are near a location.
        /// </summary>
        public FindNearByRequest()
        {
            this._getDistance = true;
        }

        /// <summary>
        /// A radial search request for finding results that are near a location.
        /// </summary>
        /// <param name="info">Basic information about the data source.</param>
        public FindNearByRequest(BasicDataSourceInfo info)
        {
            this._getDistance = true;

            this.QueryURL = info.QueryURL;
            this.MasterKey = info.MasterKey;
            this.QueryKey = info.QueryKey;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// A centeral coordinate to perform the nearby search. Overrides the Address value if both are specified.
        /// </summary>
        public GeoDataLocation Center { get; set; }

        /// <summary>
        /// A string address to perform a nearby search around. 
        /// If the Center properties is specified it will override this value when doing the search.
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// The radial search distance.
        /// You must specify the distance in between 0.16 and 1000 kilometers. 
        /// </summary>
        public double Distance { get; set; }
        
        #endregion

        #region Public Methods

        /// <summary>
        /// Retrieves the reuqest URL for the query.
        /// </summary>
        /// <returns>A string URL for the query.</returns>
        public override string GetRequestUrl()
        {
            string spatialFilter;

            double disKm = SpatialTools.ConvertDistance(Distance, DistanceUnits, DistanceUnitType.KM);

            if (disKm < 0.16)
            {
                disKm = 0.16;
            }

            if (disKm > 1000)
            {
                disKm = 1000;
            }

            if(Center != null){
                spatialFilter = string.Format(CultureInfo.InvariantCulture, "?spatialFilter=nearby({0:0.#####},{1:0.#####},{2})", Center.Latitude, Center.Longitude, disKm);
            }else if(!string.IsNullOrWhiteSpace(Address)){
                spatialFilter = string.Format(CultureInfo.InvariantCulture, "?spatialFilter=nearby('{0}',{1})", Uri.EscapeDataString(Address), disKm);
            }
            else
            {
                throw new Exception("Location to search nearby is not specified.");
            }

            return string.Format(base.GetBaseRequestUrl(), spatialFilter);
        }

        #endregion
    }
}
