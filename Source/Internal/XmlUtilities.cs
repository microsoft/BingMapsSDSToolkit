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
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace BingMapsSDSToolkit.Internal
{
    internal static class XmlUtilities
    {
        #region Internal Methods

        internal static bool IsStreamCompressed(Stream inputStream)
        {
            bool isCompressed = false;

            var reader = new BinaryReader(inputStream);
            inputStream.Seek(0, SeekOrigin.Begin);
            if (reader.BaseStream.Position < reader.BaseStream.Length)
            {
                if (reader.ReadInt32() == 67324752)
                    isCompressed = true;
                inputStream.Seek(0, SeekOrigin.Begin);
            }

            return isCompressed;
        }

        internal static IList<XElement> GetElementsByTagName(XElement node, string nodeName)
        {
            return (from x in node.Elements()
                    where string.Compare(x.Name.LocalName, nodeName, StringComparison.OrdinalIgnoreCase) == 0
                    select x).ToList();
        }

        internal static IList<XElement> GetElementsByTagName(XElement node, string nodeName, string ns)
        {
            return (from x in node.Elements(XName.Get(nodeName, ns))
                    select x).ToList();
        }

        internal static XAttribute GetAttribute(XElement node, string nodeName)
        {
            if (node != null && node.HasAttributes)
            {
                return node.Attributes().Where(x => string.Compare(x.Name.LocalName, nodeName, StringComparison.OrdinalIgnoreCase) == 0).FirstOrDefault();
            }

            return null;
        }

        internal static string GetStringAttribute(XElement node, string nodeName)
        {
            var attr = GetAttribute(node, nodeName);
            if (attr == null)
            {
                return string.Empty;
            }

            if (string.IsNullOrWhiteSpace(attr.Value))
            {
                return string.Empty;
            }

            return attr.Value;
        }

        internal static double GetDoubleAttribute(XElement node, string nodeName)
        {
            var attr = GetAttribute(node, nodeName);
            if (attr == null)
            {
                return double.NaN;
            }

            double tempDouble;
            if (!string.IsNullOrWhiteSpace(attr.Value) && double.TryParse(attr.Value, NumberStyles.Float, CultureInfo.InvariantCulture, out tempDouble))
            {
                return tempDouble;
            }

            return double.NaN;
        }

        internal static XElement GetChildNode(XElement node, string nodeName)
        {
            if (node != null)
            {
                return node.Elements().Where(x => string.Compare(x.Name.LocalName, nodeName, StringComparison.OrdinalIgnoreCase) == 0).FirstOrDefault();
            }

            return null;
        }

        internal static List<XElement> GetChildNodes(XElement node, string nodeName)
        {
            if (node != null)
            {
                return node.Elements().Where(x => string.Compare(x.Name.LocalName, nodeName, StringComparison.OrdinalIgnoreCase) == 0).ToList();
            }

            return null;
        }

        internal static List<XElement> GetChildNodes(XElement node, string[] nodeNames)
        {
            if (node != null)
            {
                List<XElement> nodes = new List<XElement>();

                foreach (var nn in nodeNames)
                {
                    var n = GetChildNodes(node, nn);

                    if (n != null)
                    {
                        nodes.AddRange(n);
                    }
                }

                return nodes;
            }

            return null;
        }

        internal static bool GetBoolean(XElement node, bool defaultValue)
        {
            if (node != null)
            {
                string temp = GetString(node);
                switch (temp.ToLowerInvariant())
                {
                    case "true":
                    case "1":
                    case "yes":
                        return true;
                    case "false":
                    case "0":
                    case "no":
                        return false;
                }
            }

            return defaultValue;
        }

        internal static bool GetBoolean(XElement node, string nodeName, bool defaultValue)
        {
            if (node != null)
            {
                var n = node.Elements().Where(x => string.Compare(x.Name.LocalName, nodeName, StringComparison.OrdinalIgnoreCase) == 0).FirstOrDefault();
                return GetBoolean(n, defaultValue);
            }

            return defaultValue;
        }

        internal static int GetInt32(XElement node, int defaultValue)
        {
            int temp;
            if (node != null && int.TryParse(GetString(node), NumberStyles.Integer, CultureInfo.InvariantCulture, out temp))
            {
                return temp;
            }

            return defaultValue;
        }

        internal static Int64 GetInt64(XElement node, int defaultValue)
        {
            Int64 temp;
            if (node != null && Int64.TryParse(GetString(node), NumberStyles.Integer, CultureInfo.InvariantCulture, out temp))
            {
                return temp;
            }

            return defaultValue;
        }

        internal static double GetDouble(XElement node, double defaultValue)
        {
            double temp;
            if (node != null && double.TryParse(GetString(node), NumberStyles.Float, CultureInfo.InvariantCulture, out temp))
            {
                return temp;
            }

            return defaultValue;
        }

        internal static double GetDouble(XElement node, string nodeName, double defaultValue)
        {
            if (node != null)
            {
                var n = node.Elements().Where(x => string.Compare(x.Name.LocalName, nodeName, StringComparison.OrdinalIgnoreCase) == 0).FirstOrDefault();
                return GetDouble(n, defaultValue);
            }

            return defaultValue;
        }

        internal static string GetString(XElement node)
        {
            string value = string.Empty;

            if (node != null && !string.IsNullOrEmpty(node.Value))
            {
                value = node.Value;
            }

            return value;
        }

        internal static string GetString(XElement node, string nodeName)
        {
            if (node != null)
            {
                var n = node.Elements().Where(x => string.Compare(x.Name.LocalName, nodeName, StringComparison.OrdinalIgnoreCase) == 0).FirstOrDefault();
                return GetString(n);
            }

            return string.Empty;
        }

        internal static DateTime? GetDateTime(XElement node)
        {
            var sDate = GetString(node);
            DateTime date;

            if (!string.IsNullOrWhiteSpace(sDate) && DateTime.TryParse(sDate, out date))
            {
                return date;
            }

            return null;
        }

        #endregion
    }
}

