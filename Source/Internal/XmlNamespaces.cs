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

using System.Xml.Linq;

namespace BingMapsSDSToolkit.Internal
{
    internal class XmlNamespaces
    {
        public static XNamespace Atom = "http://www.w3.org/2005/Atom";
        public static XNamespace App = "http://www.w3.org/2007/app";
        public static XNamespace BingSpatialDataServices = "http://schemas.microsoft.com/bing/spatial/2010/11/odata";
        public static XNamespace DataServicesMetadata = "http://schemas.microsoft.com/ado/2007/08/dataservices/metadata";
        public static XNamespace DataServices = "http://schemas.microsoft.com/ado/2007/08/dataservices";
        public static XNamespace XmlnsNamespace = "http://www.w3.org/2000/xmlns/";
        public static XNamespace XmlSchemaNamespace = "http://www.w3.org/2001/XMLSchema";
        public static XNamespace MsDataNamespace = "urn:schemas-microsoft-com:xml-msdata";
    }
}
