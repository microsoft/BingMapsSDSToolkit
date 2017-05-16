using System.Collections.Generic;
using System.Runtime.Serialization;

namespace BingMapsSDSToolkit.GeodataAPI
{
    /// <summary>
    /// An object that stores the information for a single polygon in the boundary.
    /// </summary>
    [DataContract]
    public class Primitive
    {
        #region Public Properties

        /// <summary>
        /// A unique ID associated with this polygon primitive. 
        /// </summary>
        [DataMember(Name = "PrimitiveID", EmitDefaultValue = false)]
        public string PrimitiveID { get; set; }

        /// <summary>
        /// A comma-delimited sequence starting with the version number of the polygon set followed by a list of compressed polygon “rings” (closed paths represented by sequences of latitude and-longitude points) ordered as follows: 
        /// [version],[compressed polygon ring 1],[compressed polygon ring 2],...,[compressed polygon ring n]
        /// See the Decompression Algorithm below for code to use to retrieve the polygon points from the compressed strings.
        /// </summary>
        [DataMember(Name = "Shape", EmitDefaultValue = false)]
        public string Shape { get; set; }

        /// <summary>
        /// The number of vertex points used to define the polygon.
        /// </summary>
        [DataMember(Name = "NumPoints", EmitDefaultValue = false)]
        public string NumPoints { get; set; }

        /// <summary>
        /// An ID identifying the data provider that supplied the data. This ID number will reference one of the sources listed in the CopyrightSources collection attached to the parent entity .
        /// </summary>
        [DataMember(Name = "SourceID", EmitDefaultValue = false)]
        public string SourceID { get; set; }

        #endregion

        #region Public Methods

        /// <summary>
        /// Parses the encoded Shape into a Polygon that consists of a list of coordinates that make up the 
        /// exterior ring of the polygon and list of inner rings. 
        /// </summary>
        /// <returns>A list of polygon which contains parsed coordinate values from the Shape property.</returns>
        public GeodataPolygon GetPolygon()
        {
            var poly = new GeodataPolygon();
            var encodedRings = Shape.Split(new char[] { ',' });

            List<GeodataLocation> ring;

            //Skip first result as it is just a version number and not a ring.
            for (int i = 1; i < encodedRings.Length; i++)
            {
                if (PointCompression.TryDecode(encodedRings[i], out ring))
                {
                    if (i == 1)
                    {
                        poly.ExteriorRing = ring;
                    }
                    else
                    {
                        poly.InnerRings.Add(ring);
                    }
                }
            }

            return poly;
        }

        #endregion
    }
}
