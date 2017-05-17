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

using System.Collections.Generic;

namespace BingMapsSDSToolkit.GeocodeDataflowAPI
{
    /// <summary>
    /// Match codes used to indicate the level of match a geocode result has to the original address.
    /// </summary>
    public static class MatchCodeTypes
    {
        #region Public Properties

        /// <summary>
        /// The location has only one match or all returned matches are considered strong matches. 
        /// For example, a query for New York returns several Good matches. 
        /// </summary>
        public const string Good = "Good";

        /// <summary>
        /// The location is one of a set of possible matches. 
        /// For example, when you query for the street address 128 Main St., the response may return 
        /// two locations for 128 North Main St. and 128 South Main St. because there is not enough 
        /// information to determine which option to choose. 
        /// </summary>
        public const string Ambiguous = "Ambiguous";

        /// <summary>
        /// The location represents a move up the geographic hierarchy. This occurs when a match for 
        /// the location request was not found, so a less precise result is returned. 
        /// For example, if a match for the requested address cannot be found, then a match code of 
        /// UpHierarchy with a RoadBlock entity type may be returned.
        /// </summary>
        public const string UpHierarchy = "UpHierarchy";

        #endregion

        #region Public Methods

        /// <summary>
        /// Takes in a comma seperated string of match codes and parses them into an array of string.
        /// </summary>
        /// <param name="matchCodes">A string containing comma seperated Match Codes.</param>
        /// <returns>An array of MatchCodeTypes</returns>
        public static string[] ParseMatchCodes(string matchCodes)
        {
            List<string> mc = new List<string>();
            if (matchCodes.Contains(Good))
            {
                mc.Add(Good);
            }

            if (matchCodes.Contains(Ambiguous))
            {
                mc.Add(Ambiguous);
            }

            if (matchCodes.Contains(UpHierarchy))
            {
                mc.Add(UpHierarchy);
            }

            return mc.ToArray();
        }

        #endregion
    }
}
