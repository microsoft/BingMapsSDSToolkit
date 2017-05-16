# Geocode Data Flow API Reference

**Namespace:** `BingMapsSDSToolkit.GeocodeDataflowAPI`

This API provides the ability to batch forward or reverse geocode up to 200,000 entities in a single request.

[MSDN API Documentation](https://msdn.microsoft.com/en-us/library/ff701733.aspx)

## Address Class

### Properties

| Name | Type | Description |
| ---- | ---- | ----------- |
| AddressLine | String | A string specifying the street line of an address. The AddressLine property is the most precise, official line for an address relative to the postal agency that services the area specified by the Locality, PostalTown, or PostalCode properties. |
| AdminDistrict | String | A string specifying the subdivision name within the country or region for an address. This element is also commonly treated as the first order administrative subdivision; but in some cases, it is the second, third, or fourth order subdivision within a country, a dependency, or a region. |
| AdminDistrict2 | String | A string specifying the higher level administrative subdivision used in some countries or regions. |
| CountryRegion | String | A string specifying the country or region name of an address. |
| FormattedAddress | String | A string that contains a full formatted address. |
| Landmark | String | A string specifying a landmark associated with an address. |
| Locality | String | A string specifying the populated place for the address. This commonly refers to a city, but may refer to a suburb or a neighborhood in certain countries. |
| Neighborhood | String | A string specifying the neighborhood for an address. |
| PostalCode | String | A string specifying the post code, postal code, or ZIP Code of an address. |
| PostalTown | String | A string specifying the post town of an address. |

## BaseRequest Class

### Properties

| Name | Type | Description |
| ---- | ---- | ----------- |
| ConfidenceFilter | ConfidenceFilter | A string specifying the minimum confidence required for the result. |
| Culture | String | A string specifying the culture. |
| IncludeNeighborhood | Boolean | A boolean value that specifies whether to return neighborhood information in the address. |
| MaxResults | Int32 | An integer from 1 to 20 specifying the maximum number of results to return. |

<a name="BatchFileFormat"></a>
## BatchFileFormat Enumerator

The data format of a batch geocode file.

| Name | Description |
| ---- | ----------- |
| CSV | Comma seperated (comma delimited) file format. |
| PIPE | Pipe (\|) delimited file format. |
| TAB | Tab delimited file format. |
| XML | XML file that matches the schema required for batch geocoding files in the Bing Spatial Data Services. |

<a name="BatchGeocodeManager"></a>
## BatchGeocodeManager Class

A tool for doing batch geocoding and reverse geocoding using the Bing Spatial Data Services.

### Constructor

> `BatchGeocodeManager()` 

> `BatchGeocodeManager(int statusUpdateInterval)` 

* `statusUpdateInterval` - Interval used to check the status of the batch job in ms. 

### Methods

| Name | Return Type | Description |
| ---- | ----------- | ----------- |
| Geocode(GeocodeFeed dataFeed, String bingMapsKey) | Task\<BatchGeocoderResults\> | Method to geocode a set of data. |

### Events

| Name | Type | Description |
| ---- | ---- | ----------- |
| StatusChanged | `(string x) => {}` | An event that provides update messages during the batch geocode process. |

## BatchGeocoderResults Class

An object used to store the results of a batch geocoding job.

### Properties

| Name | Type | Description |
| ---- | ---- | ----------- |
| Error | String | Error message if unable to process batch geocode job. |
| Failed | GeocodeFeed | All the locations the data that failed to be geocoded. |
| Succeeded | GeocodeFeed | All the locations the data that was geocoded successfully. |

## BoundingBox Class

A set of geographical coordinates in degrees that define an area on the Earth that contains the location.

### Properties

| Name | Type | Description |
| ---- | ---- | ----------- |
| EastLongitude | Double | The eastern most longitude. |
| NorthLatitude | Double | The northern most latitude. |
| SouthLatitude | Double | The southern most latitude. |
| WestLongitude | Double | The western most longitude. |

## CalculationMethodTypes Enumerator

The types of calculations methods used for location coodinates.

| Name | Description |
| ---- | ----------- |
| Interpolation | The geocode point was matched to a point on a road using interpolation. |
| InterpolationOffset | The geocode point was matched to a point on a road using interpolation with an additional offset to shift the point to the side of the street. |
| ParcelCentroid | The geocode point was matched to the center of a parcel. |
| Rooftop | The geocode point was matched to the rooftop of a building. |

## ConfidenceFilter Class

A string specifying the minimum confidence required for the result.

### Properties

| Name | Type | Description |
| ---- | ---- | ----------- |
| MinimumConfidence | String | A string specifying the minimum confidence required for the result. |

## ConfidenceTypes Class

The confidence types of a geocode result.

### Constants

| Name | Type | Description |
| ---- | ---- | ----------- |
| High | String | High confidence. |
| Low | String | Low confidence. |
| Medium | String | Medium confidence. |

### Methods

| Name | Return Type | Description |
| ---- | ----------- | ----------- |
| IsValid(String confidence) | Boolean | Checks a string confidence filter to see if it is a valid value. |

## GeocodeEntityType Class

The different entity types a geocode result can have.

### Constants

| Name | Type | Description |
| ---- | ---- | ----------- |
| Address | String | A physical address of a location. |
| AdminDivision1 | String | A first-order, initial political subdivision of a Sovereign, such as a state, a province, a department, a region, or a prefecture. |
| AdminDivision2 | String | A second-order political subdivision of a CountryRegion, a division of an AdminDivision1 or a Dependent. |
| AdminDivision3 | String | A third-order political subdivision of a CountryRegion, a division of an AdminDivision2. |
| AdministrativeBuilding | String | A building that contains governmental offices or facilities. |
| AdministrativeDivision | String | An administrative division of a CountryRegion, undifferentiated as to administrative level. |
| AgriculturalStructure | String | A Structure used for agricultural purposes. |
| Airport | String | A place where aircraft regularly land and take off, with runways, navigational aids, and facilities for handling passengers and/or cargo. |
| AirportRunway | String | An improved surface suitable for landing airplanes. |
| AmusementPark | String | A facility that contains rides and other attractions, such as a theme park. |
| AncientSite | String | A place where archeological remains, old structures, or cultural artifacts are located. |
| Aquarium | String | A place where marine life is displayed to the public. |
| Archipelago | String | A logical grouping of Islands. |
| Autorail | String | A Railway that carries automobiles. |
| Basin | String | A low-lying area mostly or wholly surrounded by higher ground. |
| Battlefield | String | A site of a land battle of historical importance. |
| Bay | String | An area of water partially enclosed by an indentation of shoreline. |
| Beach | String | A Coast with a surface of sand, pebbles, or small rocks. |
| BorderPost | String | A post or station at an international boundary for regulating the movement of people and goods. |
| Bridge | String | A structure erected across an obstacle, such as a stream or road, that is used by vehicles and pedestrians. |
| BusinessCategory | String | A category that identifies a kind of business. |
| BusinessCenter | String | A place where a number of businesses are located. |
| BusinessName | String | A name that identifies a business. |
| BusinessStructure | String | A Structure used for commercial purposes. |
| BusStation | String | A place where buses pick up and discharge passengers. |
| Camp | String | A site occupied by tents, huts, or other shelters for temporary use. |
| Canal | String | An artificially constructed watercourse. |
| Cave | String | An underground passageway or chamber, or a cavity on the side of a cliff. |
| CelestialFeature | String | A spherical body in space. |
| Cemetery | String | A burial place or a burial ground. |
| Census1 | String | One of the set of the most detailed, lowest-level CensusDistricts. |
| Census2 | String | One of the set of second-order CensusDistricts composed by aggregating Census1s. |
| CensusDistrict | String | A district defined by a national census bureau and used for statistical data collection. |
| Channel | String | A body of water between two landmasses. |
| Church | String | A building for public Christian worship. |
| CityHall | String | A building that contains the administrative offices of a municipal government. |
| Cliff | String | A high, steep-to-perpendicular slope that overlooks a lower area or a water body. |
| ClimateRegion | String | An area of homogenous climactic conditions, as defined by modified Koeppen classes. |
| Coast | String | An area of land adjacent to a WaterFeature. |
| CommunityCenter | String | A facility for community recreation and meetings. |
| Continent | String | A very large landmass, surrounded by water and larger than an Island, that forms one of the primary divisions of land on a CelestialFeature. |
| ConventionCenter | String | A large meeting hall for conventions and other meetings, and shows. |
| CountryRegion | String | A primary PoliticalUnit. |
| Courthouse | String | A building in which courts of law are held. |
| Crater | String | A generally circular, saucer-shaped, or bowl-shaped depression caused by volcanic or meteorite explosive action. |
| CulturalRegion | String | An area of land with strong local identity, but no political status. |
| Current | String | A large area of ocean where surface water flows in a certain constant general direction. |
| Dam | String | A barrier constructed across a stream to impound water. |
| Delta | String | An area where a River divides into many separate water channels as it enters a Sea or a Lake. |
| Dependent | String | A PoliticalUnit that is politically controlled by a Sovereign, but separate geographically, and to some degree politically, such as a territory, a colony, or a dependency. |
| Desert | String | A large area with low rainfall and little or no vegetation. |
| DisputedArea | String | An area in political dispute that is not considered part of any CountryRegion. |
| DrainageBasin | String | A land region where all surface water drains into one specific WaterFeature. |
| Dune | String | A wave form, a ridge, or a star-shaped feature composed of sand. |
| EarthquakeEpicenter | String | A place where the destructive force of a specific earthquake is centered. |
| Ecoregion | String | A region with a homogeneous ecosystem, flora, and/or fauna. |
| EducationalStructure | String | A place for providing instruction. |
| ElevationZone | String | An area where the surface elevation of all land is within a defined range. |
| Factory | String | A building or set of buildings where goods are manufactured, processed, or fabricated. |
| FerryRoute | String | A route used by a boat, or by other floating conveyances regularly used to transport people and vehicles across a WaterFeature. |
| FerryTerminal | String | A structure and associated facilities where a ferry boat docks and takes on passengers, automobiles, and/or cargo. |
| FishHatchery | String | A place for hatching fish eggs or raising fish. |
| Forest | String | A large area of trees. |
| FormerAdministrativeDivision | String | An AdministrativeDivision that no longer exists. |
| FormerPoliticalUnit | String | A PoliticalUnit that no longer exists. |
| FormerSovereign | String | A Sovereign that no longer exists. |
| Fort | String | A defensive structure or earthwork. |
| Garden | String | An enclosure for displaying selected plant life. |
| GeodeticFeature | String | An invisible point, line, or area on the surface of a CelestialFeature that is used for geographic reference. |
| GeoEntity | String | A single thing that has spatial extent and location. |
| GeographicPole | String | One of the two points of intersection of the surface of a CelestialFeature and its axis of rotation. |
| Geyser | String | A HotSpring that intermittently shoots water into the air. |
| Glacier | String | A mass of ice, usually at high latitudes or high elevations, with sufficient thickness to flow away from the source area. |
| GolfCourse | String | A recreational field where golf is played. |
| GovernmentStructure | String | A Structure typically owned and operated by a governmental entity. |
| Heliport | String | A place where helicopters land and take off. |
| Hemisphere | String | A half of the surface of a Celestial Feature, usually specified as northern, southern, eastern, or western. |
| HigherEducationFacility | String | A place where students receive advanced or specialized education, such as a college or a university. |
| HistoricalSite | String | A place of historical importance. |
| Hospital | String | A building in which the sick or injured, especially those confined to bed, are medically treated. |
| HotSpring | String | A place where hot water emerges from the ground. |
| Ice | String | A large area covered with frozen water. |
| IndigenousPeoplesReserve | String | An area of land set aside for aboriginal, tribal, or native populations. |
| IndustrialStructure | String | A Structure used for industrial or extractive purposes. |
| InformationCenter | String | A place where tourists and citizens can obtain information. |
| InternationalDateline | String | The line running between geographic poles designated as the point where a calendar day begins. |
| InternationalOrganization | String | An area of land composed of the member PoliticalUnits of an official governmental organization composed of two or more Sovereigns. |
| Island | String | An area of land completely surrounded by water and smaller than a Continent. |
| Isthmus | String | A narrow strip of land connecting two larger landmasses and bordered by water on two sides. |
| Junction | String | A place where two or more roads join. |
| Lake | String | An inland water body, usually fresh water. |
| LandArea | String | A relatively small area of land exhibiting a common characteristic that distinguishes it from the surrounding land. |
| Landform | String | A natural geographic feature on dry land. |
| LandmarkBuilding | String | A Structure that is a well-known point of reference. |
| LatitudeLine | String | An imaginary line of constant latitude that circles a CelestialFeature, in which every point on the line is equidistant from a geographic pole. |
| Library | String | A place where books and other media are stored and loaned out to the public or others. |
| Lighthouse | String | A tall structure with a major navigation light. |
| LinguisticRegion | String | An area of land where most of the population speaks the same language or speaks languages in the same linguistic family. |
| LongitudeLine | String | An imaginary line of constant longitude on a CelestialFeature that runs from one geographic pole to the other. |
| MagneticPole | String | A point on the surface of a CelestialFeature that is the origin for lines of magnetic force. |
| Marina | String | A harbor facility for small boats. |
| Market | String | A place where goods are bought and sold. |
| MedicalStructure | String | A Structure in which the sick or injured are medically treated. |
| MetroStation | String | A place where urban rapid transit trains pick up and drop off passengers, often underground or elevated. |
| MilitaryBase | String | A place used by an armed service for storing arms and supplies, for accommodating and training troops, and as a base from which operations can be initiated. |
| Mine | String | A place where mineral ores are extracted from the ground by excavating surface pits and subterranean passages. |
| Mission | String | A place characterized by dwellings, school, church, hospital, and other facilities operated by a religious group for the purpose of providing charitable services and to propagate religion. |
| Monument | String | A commemorative structure or statue. |
| Mosque | String | A building for public Islamic worship. |
| Mountain | String | An elevated landform that rises, often steeply, above surrounding land on most sides. |
| MountainRange | String | A group of connected Mountains. |
| Museum | String | A building where objects of permanent interest in one or more of the arts and sciences are preserved and exhibited. |
| NauticalStructure | String | A Structure used for nautical purposes. |
| NavigationalStructure | String | A Structure used for navigational purposes. |
| Neighborhood | String | A section of a PopulatedPlace, usually homogenous and/or well-known, but often with indistinct boundaries. |
| Oasis | String | An area in a Desert that contains water and plant life. |
| ObservationPoint | String | A wildlife or scenic observation point. |
| Ocean | String | A vast expanse of salt water, one of the major Seas covering part of the earth. |
| OfficeBuilding | String | A building that contains offices. |
| Park | String | An area maintained as a place of scenic beauty, or for recreation. |
| ParkAndRide | String | A parking lot reserved for mass transit commuters. |
| Pass | String | A break in a MountainRange used for transportation from one side of the mountain range to the other. |
| Peninsula | String | An elongated area of land projecting into a body of water and surrounded by water on three sides. |
| Plain | String | An extensive area of comparatively level to gently undulating land, lacking surface irregularities. |
| Planet | String | A CelestialFeature that orbits a star. |
| Plate | String | A section of a planetary crust that is in motion relative to other tectonic plates. |
| Plateau | String | An elevated plain with steep slopes on one or more sides. |
| PlayingField | String | A tract of land used for playing team sports and/or other athletic events. |
| Pole | String | A point on the surface of a CelestialFeature that marks an important geographical or astronomical location. |
| PoliceStation | String | A building in which police are stationed or posted. |
| PoliticalUnit | String | An area of land with well-defined borders that is subject to a specific political administration. |
| PopulatedPlace | String | A concentrated area of human settlement, such as a city, a town, or a village. |
| Postcode | String | A district used by a postal service as an aid in postal distribution and having a unique identifying code. |
| Postcode1 | String | One of the set of lowest-level and most detailed set of PostCodes in a Sovereign. |
| Postcode2 | String | One of the set of second-order (one level up from the lowest level) Postcodes in a Sovereign, composed by aggregating Postcode1s. |
| Postcode3 | String | One of the set of third-order Postcodes in a Sovereign, composed by aggregating Postcode2s. |
| Postcode4 | String | One of the set of fourth-order Postcodes in a Sovereign, composed by aggregating Postcode3s. |
| PostOffice | String | A public building in which mail is received, sorted, and distributed. |
| PowerStation | String | A facility for generating electric power. |
| Prison | String | A facility for confining persons convicted or accused of crimes. |
| Promontory | String | A small, usually pointed Peninsula that often marks the terminus of a landmass. |
| RaceTrack | String | A track where races are held. |
| Railway | String | A permanent twin steel-rail track on which trains move. |
| RailwayStation | String | A place comprised of ticket offices, platforms, and other facilities for loading and unloading train passengers and freight. |
| RecreationalStructure | String | A Structure used for watching or participating in sports or other athletic activities. |
| Reef | String | A partly submerged feature, usually of coral, that projects upward near the water"s surface and can be a navigational hazard. |
| Region | String | A large area of land where a specific characteristic of the land or its people is relatively homogenous. |
| ReligiousRegion | String | An area of land where the population holds relatively homogenous religious practices. |
| ReligiousStructure | String | A structure where organized, public religious services are held. |
| ResearchStructure | String | A Structure used for scientific purposes. |
| Reserve | String | A tract of public land set aside for restricted use or reserved for future use. |
| ResidentialStructure | String | A house, a hut, an apartment building, or another structure where people reside. |
| RestArea | String | A designated area, usually along a major highway, where motorists can stop to relax. |
| River | String | A stream of running water. |
| Road | String | An open way with an improved surface for efficient transportation of vehicles. |
| RoadBlock | String | A road. |
| RoadIntersection | String | A junction where two or more roads meet or cross at the same grade. |
| Ruin | String | A destroyed or decayed structure that is no longer functional. |
| Satellite | String | A CelestialFeature that orbits a Planet. |
| School | String | A place where people, usually children, receive a basic education. |
| ScientificResearchBase | String | A scientific facility used as a base from which research is carried out or monitored. |
| Sea | String | A large area of salt water. |
| SeaplaneLandingArea | String | A place on a water body where floatplanes land and take off. |
| ShipWreck | String | A site of the remains of a wrecked vessel. |
| ShoppingCenter | String | A collection of linked retail establishments. |
| Shrine | String | A structure or place that memorializes a person or religious concept. |
| Site | String | A place most notable because of an event that occurred in that location. |
| SkiArea | String | A place developed for recreational Alpine or Nordic skiing. |
| Sovereign | String | An independent nation-state, the highest level of political authority in that location. |
| SpotElevation | String | A point on a CelestialFeature"s surface with a known elevation. |
| Spring | String | A place where water emerges from the ground. |
| Stadium | String | A structure with an enclosure for athletic games with tiers of seats for spectators. |
| StatisticalDistrict | String | An area of land defined as a district to be used for statistical collection or service provision. |
| Structure | String | A building, a facility, or a group of buildings and/or facilities used for a certain common purpose. |
| TectonicBoundary | String | A line that forms the border between two Plates. |
| TectonicFeature | String | A Landform related to Plates and their movement. |
| Temple | String | An edifice dedicated to religious worship. |
| TimeZone | String | A large area within which the same time standard is used. |
| TouristStructure | String | A Structure typically used by tourists. |
| Trail | String | A path, a track, or a route used by pedestrians, animals, or off-road vehicles. |
| TransportationStructure  | String| A Structure used for transportation purposes. |
| Tunnel | String | A subterranean passageway for transportation. |
| UnderwaterFeature | String | A feature on the floor of a WaterFeature. |
| UrbanRegion | String | An area of land with high population density and extensive urban development. |
| Valley | String | A low area surrounded by higher ground on two or more sides. |
| Volcano | String | A Mountain formed by volcanic action and composed of volcanic rock. |
| Wall | String | An upright structure that encloses, divides, or protects an area. |
| Waterfall | String | A vertical or very steep section of a River. |
| WaterFeature | String | A geographic feature that has water on its surface. |
| Well | String | A cylindrical hole, pit, or tunnel drilled or dug down to a depth from which water, oil, or gas can be pumped or brought to the surface. |
| Wetland | String | An area of high soil moisture, partially or intermittently covered with shallow water. |
| Zoo | String | A zoological garden or park where wild animals are kept for exhibition. |

<a name="GeocodeEntity"></a>
## GeocodeEntity Class

An eneity in the the geocode feed to be geocoded or reverse geocoded.

### Constructor

> `GeocodeEntity()` 

> `GeocodeEntity(String query)` 

### Properties

| Name | Type | Description |
| ---- | ---- | ----------- |
| FaultReason | String | Information about an error that occurred during the geocode dataflow job. This value is provided only for data that was not processed successfully. Do not set this property. |
| GeocodeRequest | GeocodeRequest | The geocode request being made. |
| GeocodeResponse | GeocodeResponse\[\] | The results of the geocoding/reverse geocoding process. Do not set this property. |
| Id | String | A string that contains a unique ID to assign to the entity. |
| ReverseGeocodeRequest | ReverseGeocodeRequest | The reverse geocode request being made. |
| StatusCode | String | A string that provides information about the success of the operation. Do not set this property. |
| TraceId | String | An id used to trace the request through the Bing servers. Do not set this property. |

<a name="GeocodeFeed"></a>
## GeocodeFeed Class

An object that contains the data that is to be geocoded, or has been geocoded.

### Constructor

> `GeocodeFeed()` 

### Properties

| Name | Type | Description |
| ---- | ---- | ----------- |
| Entities | List\<GeocodeEntity\> | A list of all entities in the geocode feed. |
| Version | String | The Geocode Dataflow API version to use. This always returns version 2. |

### Methods

| Name | Return Type | Description |
| ---- | ----------- | ----------- |
| GetEntityById(String id) | GeocodeEntity | Searches for the first entity that has the specified id value. |
| ReadAsync(Stream stream) | Task\<GeocodeFeed\> | Deserializes a GeocodeFeed from an XML stream. |
| ReadAsync(Stream stream, BatchFileFormat fileFormat) | Task\<GeocodeFeed\>  | Deserializes a GeocodeFeed from a file stream. |
| WriteAsync(Stream outputStream) | Task | Serializes a GeocodeFeed into an XML stream. |

## GeocodePoint Class

> Inherits from the GeodataLocation class

An object that stores the coordinate information. 

### Properties

| Name | Type | Description |
| ---- | ---- | ----------- |
| CalculationMethod | String | The method used to calculate the coordinate. |
| Type | String | The Entity Type of the location result. |
| UsageTypes | String | The recommended usage based on the calculation type. |

<a name="GeocodeRequest"></a>
## GeocodeRequest Class

> Inherits from the BaseRequest class

Request information for geocoding a location.

### Constructor

> `GeocodeRequest()` 

> `GeocodeRequest(String query)` 

### Properties

| Name | Type | Description |
| ---- | ---- | ----------- |
| Address | Address | The address to geocode. |
| IncludeQueryParse | Boolean | A boolean value that specifies whether to return parsing information. |
| IncludeQueryParseSpecified | Boolean | A boolean indicating if the information on how the query propery was parsed should be returned. |
| Query | String | A query string that contains address information to geocode. Can be used instead of an address. |

## GeocodeResponse Class

Response information for geocoding a location.

### Properties

| Name | Type | Description |
| ---- | ---- | ----------- |
| Address | Address | The geocoded address result. |
| BoundingBox | BoundingBox | A bounding box for viewing the location. |
| Confidence | String | The confidence of the geococded results. |
| EntityType | String | The geocode entity type of the geocoded location. |
| GeocodePoint | GeocodePoint\[\] | An array of coordinates for the resulting locaiton. |
| MatchCodes | String | A comma seperated string of match codes. Use the MatchCodeTypes.ParseMatchCodes method to parse into an array of strings. |
| Name | String | The name of the geocoded location. |
| Point | GeodataLocation | A coordinate for the geocoded location. |
| QueryParseValue | QueryParseValue\[\] | An array of the parsed query properties. |

## MatchCodeTypes Class

Match codes used to indicate the level of match a geocode result has to the original address.

### Constants

| Name | Type | Description |
| ---- | ---- | ----------- |
| Ambiguous | String | The location is one of a set of possible matches. For example, when you query for the street address 128 Main St., the response may return two locations for 128 North Main St. and 128 South Main St. because there is not enough information to determine which option to choose. |
| Good | String | The location has only one match or all returned matches are considered strong matches. For example, a query for New York returns several Good matches. |
| UpHierarchy | String | The location represents a move up the geographic hierarchy. This occurs when a match for the location request was not found, so a less precise result is returned. For example, if a match for the requested address cannot be found, then a match code of UpHierarchy with a RoadBlock entity type may be returned. |

### Methods

| Name | Return Type | Description |
| ---- | ----------- | ----------- |
| ParseMatchCodes(String matchCodes) | string\[\] | Takes in a comma seperated string of match codes and parses them into an array of string. |

## QueryParsePropertyTypes Class

The different property names of a QueryParse object.

### Constants

| Name | Type | Description |
| ---- | ---- | ----------- |
| AddressLine | String | A string specifying the street line of an address. The AddressLine property is the most precise, official line for an address relative to the postal agency that services the area specified by the Locality, PostalTown, or PostalCode properties. |
| AdminDistrict | String | A string specifying the subdivision name within the country or region for an address. This element is also commonly treated as the first order administrative subdivision; but in some cases, it is the second, third, or fourth order subdivision within a country, a dependency, or a region. |
| AdminDistrict2 | String | A string specifying the higher level administrative subdivision used in some countries or regions. |
| CountryRegion | String | A string specifying the country or region name of an address. |
| Landmark | String | A string specifying a landmark associated with an address. |
| Locality | String | A string specifying the populated place for the address. This commonly refers to a city, but may refer to a suburb or a neighborhood in certain countries. |
| PostalCode | String | A string specifying the post code, postal code, or ZIP Code of an address. |

## QueryParseValue Class

A parsed query value.

### Properties

| Name | Type | Description |
| ---- | ---- | ----------- |
| Property | String | The property name of the parsed query. See QueryParsePropertyTypes for possible values. |
| Value | String | The value of parsed property. |

<a name="ReverseGeocodeRequest"></a>
## ReverseGeocodeRequest Class

Request information for reverse geocoding a location cooridinate.

### Properties

| Name | Type | Description |
| ---- | ---- | ----------- |
| IncludeEntityTypes | String | A list of Geocode Entity Types to return. This parameter only returns a geocoded address if the entity type for that address is one of the entity types you specified. |
| Location | GeodataLocation | The location coordinate to reverse geocode. |