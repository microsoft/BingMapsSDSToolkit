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
