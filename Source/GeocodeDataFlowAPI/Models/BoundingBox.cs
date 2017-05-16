using System;
using System.Xml.Serialization;

namespace BingMapsSDSToolkit.GeocodeDataFlowAPI
{
    /// <summary>
    /// A set of geographical coordinates in degrees that define an area on the Earth that contains the location. 
    /// </summary>
    public class BoundingBox
    {
        #region Private Properties

        private double _southLatitude, _westLongitude, _northLatitude, _eastLongitude;

        #endregion

        #region Public Properties

        /// <summary>
        /// The southern most latitude.
        /// </summary>
        [XmlAttribute]
        public double SouthLatitude
        {
            get
            {
                return _southLatitude;
            }
            set
            {
                if (!double.IsNaN(value) && value <= 90 && value >= -90)
                {
                    //Only need to keep the first 5 decimal places. Any more just adds more data being passed around.
                    _southLatitude = Math.Round(value, 5, MidpointRounding.AwayFromZero);
                }
            }
        }

        /// <summary>
        /// The western most longitude.
        /// </summary>
        [XmlAttribute]
        public double WestLongitude
        {
            get
            {
                return _westLongitude;
            }
            set
            {
                if (!double.IsNaN(value) && value <= 180 && value >= -180)
                {
                    //Only need to keep the first 5 decimal places. Any more just adds more data being passed around.
                    _westLongitude = Math.Round(value, 5, MidpointRounding.AwayFromZero);
                }
            }
        }

        /// <summary>
        /// The northern most latitude.
        /// </summary>
        [XmlAttribute]
        public double NorthLatitude
        {
            get
            {
                return _northLatitude;
            }
            set
            {
                if (!double.IsNaN(value) && value <= 90 && value >= -90)
                {
                    //Only need to keep the first 5 decimal places. Any more just adds more data being passed around.
                    _northLatitude = Math.Round(value, 5, MidpointRounding.AwayFromZero);
                }
            }
        }

        /// <summary>
        /// The eastern most longitude.
        /// </summary>
        [XmlAttribute]
        public double EastLongitude
        {
            get
            {
                return _eastLongitude;
            }
            set
            {
                if (!double.IsNaN(value) && value <= 180 && value >= -180)
                {
                    //Only need to keep the first 5 decimal places. Any more just adds more data being passed around.
                    _eastLongitude = Math.Round(value, 5, MidpointRounding.AwayFromZero);
                }
            }
        }

        #endregion
    }
}
