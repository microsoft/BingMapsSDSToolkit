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


namespace BingMapsSDSToolkit.GeocodeDataflowAPI
{
    /// <summary>
    /// The types of calculations methods used for location coodinates.
    /// </summary>
    public static class CalculationMethodTypes
    {
        /// <summary>
        /// The geocode point was matched to a point on a road using interpolation.
        /// </summary>
        public const string Interpolation = "Interpolation";

        /// <summary>
        /// The geocode point was matched to a point on a road using interpolation with an additional offset to shift the point to the side of the street.
        /// </summary>
        public const string InterpolationOffset = "InterpolationOffset";

        /// <summary>
        /// The geocode point was matched to the center of a parcel.
        /// </summary>
        public const string ParcelCentroid = "ParcelCentroid";

        /// <summary>
        /// The geocode point was matched to the rooftop of a building.
        /// </summary>
        public const string Rooftop = "Rooftop";
    }
}
