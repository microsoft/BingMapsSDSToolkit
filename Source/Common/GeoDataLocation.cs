using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace BingMapsSDSToolkit
{
    /// <summary>
    /// An object that stores the coordinate information.
    /// </summary>
    [KnownType(typeof(BingMapsSDSToolkit.GeocodeDataflowAPI.GeocodePoint))]
    public class GeodataLocation
    {
        #region Private Properties

        private double _latitude, _longitude;

        #endregion

        #region Constructor

        /// <summary>
        /// A location coordinate.
        /// </summary>
        public GeodataLocation()
        {
        }

        /// <summary>
        /// A location coordinate.
        /// </summary>
        /// <param name="latitude">Latitude coordinate vlaue.</param>
        /// <param name="longitude">Longitude coordinate value.</param>
        public GeodataLocation(double latitude, double longitude)
        {
            Latitude = latitude;
            Longitude = longitude;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Latitude coordinate.
        /// </summary>
        [XmlAttribute]
        public double Latitude
        {
            get
            {
                return _latitude;
            }
            set
            {
                if (!double.IsNaN(value) && value <= 90 && value >= -90)
                {
                    //Only need to keep the first 5 decimal places. Any more just adds more data being passed around.
                    _latitude = Math.Round(value, 5, MidpointRounding.AwayFromZero);
                }
            }
        }

        /// <summary>
        /// Longitude coordinate.
        /// </summary>
        [XmlAttribute]
        public double Longitude
        {
            get
            {
                return _longitude;
            }
            set
            {
                if (!double.IsNaN(value) && value <= 180 && value >= -180)
                {
                    //Only need to keep the first 5 decimal places. Any more just adds more data being passed around.
                    _longitude = Math.Round(value, 5, MidpointRounding.AwayFromZero);
                }
            }
        }

        #endregion
    }
}
