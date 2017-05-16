
namespace BingMapsSDSToolkit.QueryAPI
{
    /// <summary>
    /// The logical operators that can be used by a filter expression.
    /// </summary>
    public enum LogicalOperator
    {
        ///<summary>
        ///Equal
        ///<summary>
        Eq,

        ///<summary>
        ///Not equal
        ///<summary>
        Ne,

        ///<summary>
        ///Greater than
        ///<summary>
        Gt,

        ///<summary>
        ///Greater than or equal
        ///<summary>
        Ge,

        ///<summary>
        ///Less than
        ///<summary>
        Lt,

        ///<summary>
        ///Less than or equal
        ///<summary>
        Le,

        /// <summary>
        /// Finds all property values that start with a specified string value.
        /// Not supported with And or Or comparison operators.
        /// Is not supported for NavteqNA and NavteqEU data sources.
        /// </summary>
        StartsWith,

        /// <summary>
        /// Finds all property values that end with a specified string value.
        /// Not supported with And or Or comparison operators.
        /// Is not supported for NavteqNA and NavteqEU data sources.
        /// </summary>
        EndsWith,

        /// <summary>
        /// Finds all properties who's value is within a list of values.
        /// </summary>
        IsIn
    }
}
