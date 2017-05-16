
namespace BingMapsSDSToolkit.QueryAPI
{
    /// <summary>
    /// Compare Operators that can be used with filters.
    /// </summary>
    public enum CompareOperator
    {
        ///<summary>
        /// Logical and
        /// Note: Not supported when combined with StartsWith or EndsWith wildcard searches.
        ///</summary>
        And,

        ///<summary>
        /// Logical or
        /// Note: Not supported when combined with StartsWith or EndsWith wildcard searches.
        ///</summary>
        Or
    }
}
