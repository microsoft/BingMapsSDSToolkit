using System;
using System.Collections.Generic;
using System.Text;

namespace BingMapsSDSToolkit.QueryAPI
{
    /// <summary>
    /// An object that defines a filter expression.
    /// </summary>
    public class FilterExpression : IFilter
    {
        #region Constructor

        /// <summary>
        /// An object that defines a filter expression.
        /// </summary>
        public FilterExpression()
        {
        }

        /// <summary>
        /// An object that defines a filter expression.
        /// </summary>
        /// <param name="propertyName">he name of the property to filter against.</param>
        /// <param name="logicalOperator">The logical operator to use when comparing filtering against the filter value.</param>
        /// <param name="value">The value to compare against.</param>
        public FilterExpression(string propertyName, LogicalOperator logicalOperator, object value)
        {
            PropertyName = propertyName;
            Operator = logicalOperator;
        }

        #endregion

        #region Public Properties 

        /// <summary>
        /// The name of the property to filter against.
        /// Note: You cannot filter on the latitude and longitude entity properties.
        /// </summary>
        public string PropertyName {get;set;}

        /// <summary>
        /// The logical operator to use when comparing filtering against the filter value.
        /// </summary>
        public LogicalOperator Operator { get; set; }

        /// <summary>
        /// The value to compare against.
        /// </summary>
        public object Value { get; set; }

        #endregion

        #region Public Methods

        /// <summary>
        /// Converts the filter expression to string.
        /// </summary>
        /// <returns>A stirng representation of the filter expression.</returns>
        public override string ToString()
        {
            if (Operator == LogicalOperator.EndsWith || Operator == LogicalOperator.StartsWith)
            {
                if (Value is string || Value is char)
                {
                    return string.Format("{0}({1},'{2}')%20eq%20true", GetOperatorAsString(), PropertyName, (Value == null) ? "" : GetFilterValueAsString());
                }
            }
            else if(Operator == LogicalOperator.IsIn)
            {
                if(Value is IEnumerable<string>)
                {
                    var arr = Value as IEnumerable<string>;
                    var sb = new StringBuilder();

                    foreach(var v in arr)
                    {
                        sb.AppendFormat("'{0}',", v);
                    }

                    //remove trailing comma.
                    sb.Length--;

                    return string.Format("{0}%20{1}%20({2})", PropertyName, GetOperatorAsString(), sb.ToString());
                }
            }
            else
            {
                return string.Format("{0}%20{1}%20{2}", PropertyName, GetOperatorAsString(), GetFilterValueAsString());
            }

            return string.Empty;
        }

        #endregion

        #region Private Methods

        private string GetOperatorAsString()
        {
            switch (Operator)
            {
                case LogicalOperator.EndsWith:
                    return "endsWith";
                case LogicalOperator.Eq:
                    return "eq";
                case LogicalOperator.Ge:
                    return "ge";
                case LogicalOperator.Gt:
                    return "gt";
                case LogicalOperator.IsIn:
                    return "in";
                case LogicalOperator.Le:
                    return "le";
                case LogicalOperator.Lt:
                    return "lt";
                case LogicalOperator.Ne:
                    return "ne";
                case LogicalOperator.StartsWith:
                    return "startsWith";
                default:
                    break;
            }

            return null;
        }

        private string GetFilterValueAsString()
        {
            string val;

            if (Value == null)
            {
                val = "null";
            }
            else if (Value is DateTime)
            {
                val = ((DateTime)Value).ToUniversalTime().ToString("O");
            }
            else if (Value is bool)
            {
                val = ((bool)Value) ? "true" : "false";
            }
            else if (Value is string || Value is char)
            {
                //Need to escape single quotes by placing two of them in side by side.
                //Need to encode special characters.
                val = "'" + Uri.EscapeDataString(Value.ToString().Replace("'", "''")) + "'";
            }
            else
            {
                val = Value.ToString();
            }

            return val;
        }

        #endregion
    }
}
