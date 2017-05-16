using System.Collections.Generic;

namespace BingMapsSDSToolkit.GeoDataAPI
{
    /// <summary>
    /// An object storing the parsed bundary infromation as sets for rings for a polygon.
    /// </summary>
    public class GeoDataPolygon
    {
        #region Constructor

        /// <summary>
        /// An object storing the parsed bundary infromation as sets for rings for a polygon.
        /// </summary>
        public GeoDataPolygon()
        {
            InnerRings = new List<List<GeoDataLocation>>();
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// A list of coordinates that make up the exterior ring of a polygon.
        /// </summary>
        public List<GeoDataLocation> ExteriorRing { get; set; }

        /// <summary>
        /// A list of list of coordinates that make up all the inner rings of a polygon.
        /// </summary>
        public List<List<GeoDataLocation>> InnerRings { get; set; }

        #endregion
    }
}
