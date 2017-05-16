using System.Text.RegularExpressions;

namespace BingMapsSDSToolkit
{
    /// <summary>
    /// An object used to represent a geography column type.
    /// </summary>
    public class Geography
    {
        #region Public Properties

        /// <summary>
        /// The well known text that describes the geography object.
        /// </summary>
        public string WellKnownText { get; set; }

        #endregion

        #region Public Methods

        public override string ToString()
        {
            return WellKnownText;
        }

        /// <summary>
        /// Calculates the number of coordinate pairs in the geography.
        /// </summary>
        /// <returns>The number of cooridnate pairs in the geography.</returns>
        public int NumPoints()
        {
            if (string.IsNullOrEmpty(this.WellKnownText))
            {
                //For performance use a regular expression.
                var coordRx = new Regex(@"-?[0-9]+\.?[0-9]* -?[0-9]+\.?[0-9]*");
                var m = coordRx.Matches(this.WellKnownText);

                if (m != null)
                {
                    return m.Count;
                }
            }

            return 0;
        }

        #endregion
    }
}
