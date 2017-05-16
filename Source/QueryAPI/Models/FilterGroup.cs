using System.Text;

namespace BingMapsSDSToolkit.QueryAPI
{
    /// <summary>
    /// A group of filter objects and the compare operator to use against them.
    /// </summary>
    public class FilterGroup: IFilter
    {
        #region Constructor

        /// <summary>
        /// A group of filter objects and the compare operator to use against them.
        /// </summary>
        public FilterGroup()
        {
        }

        /// <summary>
        /// A group of filter objects and the compare operator to use against them.
        /// </summary>
        /// <param name="filters">The filters to combine into a group.</param>
        /// <param name="compareOperator">The operator to use to combine the filters.</param>
        public FilterGroup(IFilter[] filters, CompareOperator compareOperator)
        {
            Filters = filters;
            CompareOperator = compareOperator;
        }

        /// <summary>
        /// A group of filter objects and the compare operator to use against them.
        /// </summary>
        /// <param name="filters">The filters to combine into a group.</param>
        /// <param name="compareOperator">The operator to use to combine the filters.</param>
        /// <param name="not">A boolean indicating if this filter should not make or not. </param>
        public FilterGroup(IFilter[] filters, CompareOperator compareOperator, bool not)
        {
            Filters = filters;
            CompareOperator = compareOperator;
            Not = not;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// An array of filters expressions.
        /// </summary>
        public IFilter[] Filters { get; set; }

        /// <summary>
        /// The comparison operator to use with the filters.
        /// </summary>
        public CompareOperator CompareOperator { get; set; }

        /// <summary>
        /// A boolean indicating if this filter should not make or not. 
        /// </summary>
        public bool Not { get; set; }

        #endregion

        #region Public Methods

        /// <summary>
        /// Converts the filter group to string.
        /// </summary>
        /// <returns>A stirng representation of the filter group.</returns>
        public override string ToString()
        {
            if (Filters != null)
            {
                int numFilters = Filters.Length;

                var sb = new StringBuilder();

                if (Not)
                {
                    sb.Append("not%20");
                }


                sb.Append("(");
                for (var i = 0; i < numFilters; i++)
                {
                    if (i < numFilters - 1)
                    {
                        sb.AppendFormat("{0}%20{1}%20", Filters[i].ToString(), CompareOperator);
                    }
                    else
                    {
                        sb.Append(Filters[i].ToString());
                    }
                }
                sb.Append(")");

                return sb.ToString();
            }

            return string.Empty;
        }

        #endregion
    }
}
