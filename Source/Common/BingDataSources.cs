namespace BingMapsSDSToolkit
{
    /// <summary>
    /// A static class that contains data source information for all the built-in Bing data sources in the Bing Spatial Data Services.
    /// </summary>
    public static class BingDataSources
    {
        #region Public Properties

        /// <summary>
        /// A sample data source of coffee shops.
        /// </summary>
        public static BasicDataSourceInfo FourthCoffeeSample
        {
            get { return new BasicDataSourceInfo("https://spatial.virtualearth.net/REST/v1/data/20181f26d9e94c81acdf9496133d4f23/FourthCoffeeSample/FourthCoffeeShops"); }
        }

        /// <summary>
        /// A data source of points of interest for North America. 
        /// </summary>
        public static BasicDataSourceInfo NavteqPOIs_NA
        {
            get { return new BasicDataSourceInfo("https://spatial.virtualearth.net/REST/v1/data/f22876ec257b474b82fe2ffcb8393150/NavteqNA/NavteqPOIs"); }
        }

        /// <summary>
        /// A data source of points of interest for Europe. 
        /// </summary>
        public static BasicDataSourceInfo NavteqPOIs_EU
        {
            get { return new BasicDataSourceInfo("https://spatial.virtualearth.net/REST/v1/data/c2ae584bbccc4916a0acf75d1e6947b4/NavteqEU/NavteqPOIs"); }
        }

        /// <summary>
        /// A data source of real time traffic incidents (accidents, road closures, ...).
        /// </summary>
        public static BasicDataSourceInfo TrafficIncidents
        {
            get { return new BasicDataSourceInfo("https://spatial.virtualearth.net/REST/v1/data/8F77935E46704C718E45F52D0D5550A6/TrafficIncidents/TrafficIncident"); }
        }

        /// <summary>
        /// A data source that contains a subset of 2010 US Census data broken up by states.
        /// </summary>
        public static BasicDataSourceInfo USCensus2010_States
        {
            get { return new BasicDataSourceInfo("https://spatial.virtualearth.net/REST/v1/data/755aa60032b24cb1bfb54e8a6d59c229/USCensus2010_States/States"); }
        }

        /// <summary>
        /// A data source that contains a subset of 2010 US Census data broken up by counties.
        /// </summary>
        public static BasicDataSourceInfo USCensus2010_Counties
        {
            get { return new BasicDataSourceInfo("https://spatial.virtualearth.net/REST/v1/data/6c39d83e5812459f914832970618048e/USCensus2010_Counties/Counties"); }
        }

        /// <summary>
        /// A data source that contains a subset of 2010 US Census data broken up by 111th Congressional Districts.
        /// </summary>
        public static BasicDataSourceInfo USCensus2010_111CD
        {
            get { return new BasicDataSourceInfo("https://spatial.virtualearth.net/REST/v1/data/04566e63b0d74e45aa5fa19a2f8bb8bc/USCensus2010_CD/CongressionalDistrict111th"); }
        }

        /// <summary>
        /// A data source that contains a subset of 2010 US Census data broken up by Zip Code Tabulation areas (ZCTA5).
        /// </summary>
        public static BasicDataSourceInfo USCensus2010_ZCTA5
        {
            get { return new BasicDataSourceInfo("https://spatial.virtualearth.net/REST/v1/data/f42cab32d0ee41738d90856badd638d3/USCensus2010_ZCTA5/ZCTA5"); }
        }

        #endregion
    }
}
