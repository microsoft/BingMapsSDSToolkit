
namespace BingMapsSDSToolkit.GeoDataAPI
{
    /// <summary>
    /// An object the contains the reuqest information for retrieving a boundary.
    /// </summary>
    public class GetBoundaryRequest
    {
        #region Private Properties

        private int levelOfDetail = 0;
        private BoundaryEntityType entityType = BoundaryEntityType.CountryRegion;
        private string culture = "en";
        private string userRegion = "US";

        #endregion

        #region Public Properties

        /// <summary>
        /// The coordinate to use to find intersecting boundary. If set, this will override the Address property when doing a search.
        /// </summary>
        public GeoDataLocation Coordinate { get; set; }

        /// <summary>
        /// An address string that is geocoded by the service to get latitude and longitude coordinates.
        /// Note: This call will result in two individual usage transactions: RESTLocations (for geocoding) and RESTSpatialDataService:Geodata.
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// The level of detail for the boundary polygons returned. An integer between 0 and 3, where 0 specifies the coarsest level of boundary detail and 3 specifies the best. Default: 0.
        /// </summary>
        public int LevelOfDetail
        {
            get { return levelOfDetail; }
            set
            {
                levelOfDetail = (value <= 3 && value >= 0)? value : levelOfDetail;
            }
        }

        /// <summary>
        /// The entity type to return. Default: CountryRegion. Note that not all entity types are available for each location.
        /// </summary>
        public BoundaryEntityType EntityType
        {
            get { return entityType; }
            set { entityType = value; }
        }

        /// <summary>
        /// Specifies whether the response should include all of the boundary polygons for the requested entity or just return a single polygon that represents the main outline of the entity.
        /// </summary>
        public bool GetAllPolygons { get; set; }

        /// <summary>
        /// Specifies whether the response should include metadata about the entity, such as AreaSqKm and others.
        /// </summary>
        public bool GetEntityMetadata { get; set; }

        /// <summary>
        /// Specifies the preferred language to use for any metadata text about the entity or polygons. Defualt: en
        /// </summary>
        public string Culture
        {
            get { return culture; }
            set { culture = value; }
        }

        /// <summary>
        /// Specifies the user’s home country or region. Default: US
        /// </summary>
        public string UserRegion
        {
            get { return userRegion; }
            set { userRegion = value; }
        }

        #endregion
    }
}
