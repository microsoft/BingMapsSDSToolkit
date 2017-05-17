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

using System;

namespace BingMapsSDSToolkit
{
    /// <summary>
    /// A set of useful spatial calculation tools.
    /// </summary>
    public static class SpatialTools
    {
        #region Earth Related Constants

        /// <summary>
        /// The approximate spherical radius of the Earth
        /// </summary>
        public static class EarthRadius
        {
            /// <summary>
            /// Earth Radius in Kilometers
            /// </summary>
            public const double KM = 6378.135;

            /// <summary>
            /// Earth Radius in Meters
            /// </summary>
            public const double Meters = 6378135;

            /// <summary>
            /// Earth Radius in Miles
            /// </summary>
            public const double Miles = 3963.189;

            /// <summary>
            /// Earth Radius in Feet
            /// </summary>
            public const double Feet = 20925640;
        }

        #endregion

        #region Earth Radius

        /// <summary>
        /// Retrieves the radius of the earth in a specific distance unit for WGS84. Defaults unit is in Meters.
        /// </summary>
        /// <param name="units">Unit of distance measurement</param>
        /// <returns>A double that represents the radius of the earth in a specific distance unit. Defaults unit is in KM's.</returns>
        public static double GetEarthRadius(DistanceUnitType units)
        {
            switch (units)
            {
                case DistanceUnitType.Feet:
                    return EarthRadius.Feet;
                case DistanceUnitType.Meters:
                    return EarthRadius.Meters;
                case DistanceUnitType.Miles:
                    return EarthRadius.Miles;
                case DistanceUnitType.Yards:
                    return ConvertDistance(EarthRadius.KM, DistanceUnitType.Kilometers, DistanceUnitType.Yards);
                case DistanceUnitType.Kilometers:
                default:
                    return EarthRadius.KM;
            }
        }

        #endregion

        #region Distance Conversion

        /// <summary>
        /// Converts distances from one unit to another.
        /// </summary>
        /// <param name="distance">The distance to convert</param>
        /// <param name="fromUnits">The units of the distance.</param>
        /// <param name="toUnits">The units to convert to.</param>
        /// <returns>Distance converted to the specified units.</returns>
        public static double ConvertDistance(double distance, DistanceUnitType fromUnits, DistanceUnitType toUnits)
        {
            //Convert the distance to kilometers
            switch (fromUnits)
            {
                case DistanceUnitType.Meters:
                    distance /= 1000;
                    break;
                case DistanceUnitType.Feet:
                    distance /= 3288.839895;
                    break;
                case DistanceUnitType.Miles:
                    distance *= 1.609344;
                    break;
                case DistanceUnitType.Yards:
                    distance *= 0.0009144;
                    break;
                case DistanceUnitType.Kilometers:
                    break;
            }

            //Convert from kilometers to output distance unit
            switch (toUnits)
            {
                case DistanceUnitType.Meters:
                    distance *= 1000;
                    break;
                case DistanceUnitType.Feet:
                    distance *= 5280;
                    break;
                case DistanceUnitType.Miles:
                    distance /= 1.609344;
                    break;
                case DistanceUnitType.Yards:
                    distance *= 1093.6133;
                    break;
                case DistanceUnitType.Kilometers:
                    break;
            }

            return distance;
        }

        #endregion

        #region Degree and Radian Conversions

        /// <summary>
        /// Converts an angle that is in degrees to radians. Angle * (PI / 180)
        /// </summary>
        /// <param name="angle">An angle in degrees</param>
        /// <returns>An angle in radians</returns>
        public static double ToRadians(double angle)
        {
            return angle * (Math.PI / 180);
        }

        /// <summary>
        /// Converts an angle that is in radians to degress. Angle * (180 / PI)
        /// </summary>
        /// <param name="angle">An angle in radians</param>
        /// <returns>An angle in degrees</returns>
        public static double ToDegrees(double angle)
        {
            return angle * (180 / Math.PI);
        }

        #endregion

        #region Haversine Distance Calculation method

        /// <summary>
        /// Calculate the distance between two coordinates on the surface of a sphere (Earth).
        /// </summary>
        /// <param name="origin">First coordinate to calculate distance between.</param>
        /// <param name="destination">Second coordinate to calculate distance between.</param>
        /// <param name="units">Unit of distance measurement.</param>
        /// <returns>The shortest distance in the specifed units.</returns>
        public static double HaversineDistance(GeodataLocation origin, GeodataLocation destination, DistanceUnitType units)
        {
            return HaversineDistance(origin.Latitude, origin.Longitude, destination.Latitude, destination.Longitude, units);
        }

        /// <summary>
        /// Calculate the distance between two coordinates on the surface of a sphere (Earth).
        /// </summary>
        /// <param name="origLat">Origin Latitude.</param>
        /// <param name="origLon">Origin Longitude.</param>
        /// <param name="destLat">Destination Latitude.</param>
        /// <param name="destLon">Destination Longitude.</param>
        /// <param name="units">Unit of distance measurement.</param>
        /// <returns>The shortest distance in the specifed units.</returns>
        public static double HaversineDistance(double origLat, double origLon, double destLat, double destLon, DistanceUnitType units)
        {
            double radius = GetEarthRadius(units);

            double dLat = ToRadians(destLat - origLat);
            double dLon = ToRadians(destLon - origLon);

            double a = Math.Pow(Math.Sin(dLat / 2), 2) + Math.Pow(Math.Cos(ToRadians(origLat)), 2) * Math.Pow(Math.Sin(dLon / 2), 2);
            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

            return radius * c;
        }

        #endregion
    }
}
