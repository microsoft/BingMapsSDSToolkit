/*
 * Copyright(c) 2017 Microsoft Corporation. All rights reserved. 
 * 
 * This code is licensed under the MIT License (MIT). 
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal 
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies
 * of the Software, and to permit persons to whom the Software is furnished to do 
 * so, subject to the following conditions: 
 * 
 * The above copyright notice and this permission notice shall be included in all
 * copies or substantial portions of the Software. 
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, 
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, 
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
 * THE SOFTWARE. 
*/

using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;
using System.Linq;

namespace BingMapsSDSToolkit.GeodataAPI
{
    /// <summary>
    /// A Geodata boundary result.
    /// </summary>
    public class GeodataResult
    {
        #region Private Properties

        private List<GeodataPolygon> polygons = null;

        #endregion

        #region Public Properties

        /// <summary>
        /// A unique ID number associated with this entity. 
        /// </summary>
        [DataMember(Name = "EntityID", EmitDefaultValue = false)]
        public string EntityID { get; set; }

        /// <summary>
        /// A collection of metadata information associated with the entity.
        /// </summary>
        [DataMember(Name = "EntityMetadata", EmitDefaultValue = false)]
        public Metadata EntityMetadata { get; set; }

        /// <summary>
        /// Information about the name of the boundary location.
        /// </summary>
        [DataMember(Name = "Name", EmitDefaultValue = false)]
        public Name Name { get; set; }

        /// <summary>
        /// An array of objects that contain the polygon information for the boundary.
        /// </summary>
        [DataMember(Name = "Primitives", EmitDefaultValue = false)]
        public Primitive[] Primitives { get; set; }

        /// <summary>
        /// Copyright information for the boundary data.
        /// </summary>
        [DataMember(Name = "Copyright", EmitDefaultValue = false)]
        public Copyright Copyright { get; set; }

        #endregion

        #region Public Methods

        /// <summary>
        /// Parses all the Primitives into Polygon objects.
        /// </summary>
        /// <returns>A list of polygons that make up the shape boudary.</returns>
        public List<GeodataPolygon> GetPolygons()
        {
            if (polygons == null)
            {
                polygons = new List<GeodataPolygon>();
                foreach (var p in Primitives)
                {
                    polygons.Add(p.GetPolygon());
                }
            }
            return polygons;
        }

        /// <summary>
        /// Parses the BestMapViewBox property into bounding coordinates. 
        /// If the BestMapViewBox property is not returned in the Metadata, 
        /// the bounding coordinates will be calculated from the boundary coordinates.
        /// </summary>
        /// <param name="NorthWest">The north west coordinate of the bounding box.</param>
        /// <param name="SouthEast">The south east coordinate of the bounding box.</param>
        public void GetBestMapViewBox(out GeodataLocation NorthWest, out GeodataLocation SouthEast)
        {
            NorthWest = new GeodataLocation(-85, 180);
            SouthEast = new GeodataLocation(85, -180);

            if (EntityMetadata != null && !string.IsNullOrWhiteSpace(EntityMetadata.BestMapViewBox))
            {
                var rx = new Regex("(-?[0-9]+(.|,)?[0-9]*)");

                var matches = rx.Matches(EntityMetadata.BestMapViewBox);

                if (matches.Count == 4)
                {
                    SouthEast = new GeodataLocation()
                    {
                        Latitude = double.Parse(matches[1].Value),
                        Longitude = double.Parse(matches[0].Value),
                    };

                    NorthWest = new GeodataLocation()
                    {
                        Latitude = double.Parse(matches[3].Value),
                        Longitude = double.Parse(matches[2].Value),
                    };
                }
            }
            else
            {
                double maxLat, maxLon, minLat, minLon;
                foreach (var p in polygons)
                {
                    maxLat = p.ExteriorRing.Max(x => x.Latitude);
                    if (maxLat > NorthWest.Latitude)
                    {
                        NorthWest.Latitude = maxLat;
                    }

                    maxLon = p.ExteriorRing.Max(x => x.Longitude);
                    if (maxLon > SouthEast.Longitude)
                    {
                        SouthEast.Longitude = maxLon;
                    }

                    minLat = p.ExteriorRing.Min(x => x.Latitude);
                    if (minLat < SouthEast.Latitude)
                    {
                        SouthEast.Latitude = minLat;
                    }
                    
                    minLon = p.ExteriorRing.Min(x => x.Longitude);
                    if (minLon < NorthWest.Longitude)
                    {
                        NorthWest.Longitude = minLon;
                    }
                }
            }
        }

        #endregion
    }
}
