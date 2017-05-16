using System.Collections.Generic;

namespace BingMapsSDSToolkit.GeodataAPI
{
    /// <summary>
    /// An object storing the parsed bundary infromation as sets for rings for a polygon.
    /// </summary>
    public class GeodataPolygon
    {
        #region Constructor

        /// <summary>
        /// An object storing the parsed bundary infromation as sets for rings for a polygon.
        /// </summary>
        public GeodataPolygon()
        {
            InnerRings = new List<List<GeodataLocation>>();
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// A list of coordinates that make up the exterior ring of a polygon.
        /// </summary>
        public List<GeodataLocation> ExteriorRing { get; set; }

        /// <summary>
        /// A list of list of coordinates that make up all the inner rings of a polygon.
        /// </summary>
        public List<List<GeodataLocation>> InnerRings { get; set; }

        #endregion
    }
}
