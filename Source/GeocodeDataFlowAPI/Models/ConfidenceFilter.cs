using System.Xml.Serialization;

namespace BingMapsSDSToolkit.GeocodeDataflowAPI
{
    /// <summary>
    /// A string specifying the minimum confidence required for the result. 
    /// </summary>
    public class ConfidenceFilter
    {
        private string _confidence;

        /// <summary>
        /// A string specifying the minimum confidence required for the result. 
        /// </summary>
        [XmlAttribute]
        public string MinimumConfidence
        {
            get { return _confidence; }
            set
            {
                if (!string.IsNullOrEmpty(value) && ConfidenceTypes.IsValid(value))
                {
                    _confidence = value;
                }
            }
        }
    }
}
