
namespace BingMapsSDSToolkit.GeodataAPI
{
    /// <summary>
    /// The supported boundary entity types. Note that not all all entity types are available in all regions.
    /// </summary>
    public enum BoundaryEntityType
    {
        /// <summary>
        /// First administrative level within the country/region level, such as a state or a province.
        /// </summary>
        AdminDivision1,

        /// <summary>
        /// Second administrative level within the country/region level, such as a county.
        /// </summary>
        AdminDivision2,

        /// <summary>
        /// Country or region
        /// </summary>
        CountryRegion,
        
        /// <summary>
        /// A section of a populated place that is typically well-known, but often with indistinct boundaries. 
        /// </summary>
        Neighborhood,

        /// <summary>
        /// A concentrated area of human settlement, such as a city, town or village.
        /// </summary>
        PopulatedPlace,

        /// <summary>
        /// The smallest post code category, such as a zip code. 
        /// </summary>
        Postcode1,

        /// <summary>
        /// The next largest post code category after Postcode1 that is created by aggregating Postcode1 areas. 
        /// </summary>
        Postcode2,

        /// <summary>
        /// The next largest post code category after Postcode2 that is created by aggregating Postcode2 areas.
        /// </summary>
        Postcode3,

        /// <summary>
        /// The next largest post code category after Postcode3 that is created by aggregating Postcode3 areas.
        /// </summary>
        Postcode4
    }
}
