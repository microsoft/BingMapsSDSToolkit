using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace BingMapsSDSToolkit.GeocodeDataFlowAPI
{
    [KnownType(typeof(GeocodeRequest))]
    [KnownType(typeof(ReverseGeocodeRequest))]
    public class BaseRequest
    {
        #region Private Properties

        private int _maxResults;
        private ConfidenceFilter _confidenceFilter;

        #endregion
        
        #region Public Properties

        /// <summary>
        /// A string specifying the minimum confidence required for the result. 
        /// </summary>
        public ConfidenceFilter ConfidenceFilter
        {
            get
            {
                return _confidenceFilter;
            }
            set
            {
                _confidenceFilter = value;
            }
        }

        /// <summary>
        /// A string specifying the culture. 
        /// </summary>
        [XmlAttribute]
        public string Culture { get; set; }

        /// <summary>
        /// A boolean value that specifies whether to return neighborhood information in the address.
        /// </summary>
        [XmlAttribute]
        public bool IncludeNeighborhood { get; set; }

        [XmlIgnore]
        public bool IncludeNeighborhoodSpecified;

        /// <summary>
        /// An integer from 1 to 20 specifying the maximum number of results to return.
        /// </summary>
        [XmlAttribute]
        public int MaxResults
        {
            get { return _maxResults; }
            set
            {
                _maxResults = (value <= 1) ? 1 : ((value > 20) ? 20 : value);
            }
        }

        [XmlIgnore]
        public bool MaxResultsSpecified;

        #endregion
    }
}
