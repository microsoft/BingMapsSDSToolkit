
namespace BingMapsSDSToolkit.GeocodeDataflowAPI
{
    /// <summary>
    /// The different entity types a geocode result can have.
    /// </summary>
    public static class GeocodeEntityType
    {
        ///<summary>
        ///A physical address of a location.
        ///</summary>
        public const string Address = "Address";

        ///<summary>
        ///A first-order, initial political subdivision of a Sovereign, such as a state, a province, a department, a region, or a prefecture.
        ///</summary>
        public const string AdminDivision1 = "AdminDivision1";

        ///<summary>
        ///A second-order political subdivision of a CountryRegion, a division of an AdminDivision1 or a Dependent.
        ///</summary>
        public const string AdminDivision2 = "AdminDivision2";

        ///<summary>
        ///A third-order political subdivision of a CountryRegion, a division of an AdminDivision2.
        ///</summary>
        public const string AdminDivision3 = "AdminDivision3";

        ///<summary>
        ///A building that contains governmental offices or facilities.
        ///</summary>
        public const string AdministrativeBuilding = "AdministrativeBuilding";

        ///<summary>
        ///An administrative division of a CountryRegion, undifferentiated as to administrative level.
        ///</summary>
        public const string AdministrativeDivision = "AdministrativeDivision";

        ///<summary>
        ///A Structure used for agricultural purposes.
        ///</summary>
        public const string AgriculturalStructure = "AgriculturalStructure";

        ///<summary>
        ///A place where aircraft regularly land and take off, with runways, navigational aids, and facilities for handling passengers and/or cargo.
        ///</summary>
        public const string Airport = "Airport";

        ///<summary>
        ///An improved surface suitable for landing airplanes.
        ///</summary>
        public const string AirportRunway = "AirportRunway";

        ///<summary>
        ///A facility that contains rides and other attractions, such as a theme park.
        ///</summary>
        public const string AmusementPark = "AmusementPark";

        ///<summary>
        ///A place where archeological remains, old structures, or cultural artifacts are located.
        ///</summary>
        public const string AncientSite = "AncientSite";

        ///<summary>
        ///A place where marine life is displayed to the public.
        ///</summary>
        public const string Aquarium = "Aquarium";

        ///<summary>
        ///A logical grouping of Islands.
        ///</summary>
        public const string Archipelago = "Archipelago";

        ///<summary>
        ///A Railway that carries automobiles.
        ///</summary>
        public const string Autorail = "Autorail";

        ///<summary>
        ///A low-lying area mostly or wholly surrounded by higher ground.
        ///</summary>
        public const string Basin = "Basin";

        ///<summary>
        ///A site of a land battle of historical importance.
        ///</summary>
        public const string Battlefield = "Battlefield";

        ///<summary>
        ///An area of water partially enclosed by an indentation of shoreline.
        ///</summary>
        public const string Bay = "Bay";

        ///<summary>
        ///A Coast with a surface of sand, pebbles, or small rocks.
        ///</summary>
        public const string Beach = "Beach";

        ///<summary>
        ///A post or station at an international boundary for regulating the movement of people and goods.
        ///</summary>
        public const string BorderPost = "BorderPost";

        ///<summary>
        ///A structure erected across an obstacle, such as a stream or road, that is used by vehicles and pedestrians.
        ///</summary>
        public const string Bridge = "Bridge";

        ///<summary>
        ///A category that identifies a kind of business.
        ///</summary>
        public const string BusinessCategory = "BusinessCategory";

        ///<summary>
        ///A place where a number of businesses are located.
        ///</summary>
        public const string BusinessCenter = "BusinessCenter";

        ///<summary>
        ///A name that identifies a business.
        ///</summary>
        public const string BusinessName = "BusinessName";

        ///<summary>
        ///A Structure used for commercial purposes.
        ///</summary>
        public const string BusinessStructure = "BusinessStructure";

        ///<summary>
        ///A place where buses pick up and discharge passengers.
        ///</summary>
        public const string BusStation = "BusStation";

        ///<summary>
        ///A site occupied by tents, huts, or other shelters for temporary use.
        ///</summary>
        public const string Camp = "Camp";

        ///<summary>
        ///An artificially constructed watercourse.
        ///</summary>
        public const string Canal = "Canal";

        ///<summary>
        ///An underground passageway or chamber, or a cavity on the side of a cliff.
        ///</summary>
        public const string Cave = "Cave";

        ///<summary>
        ///A spherical body in space.
        ///</summary>
        public const string CelestialFeature = "CelestialFeature";

        ///<summary>
        ///A burial place or a burial ground.
        ///</summary>
        public const string Cemetery = "Cemetery";

        ///<summary>
        ///One of the set of the most detailed, lowest-level CensusDistricts.
        ///</summary>
        public const string Census1 = "Census1";

        ///<summary>
        ///One of the set of second-order CensusDistricts composed by aggregating Census1s.
        ///</summary>
        public const string Census2 = "Census2";

        ///<summary>
        ///A district defined by a national census bureau and used for statistical data collection.
        ///</summary>
        public const string CensusDistrict = "CensusDistrict";

        ///<summary>
        ///A body of water between two landmasses.
        ///</summary>
        public const string Channel = "Channel";

        ///<summary>
        ///A building for public Christian worship.
        ///</summary>
        public const string Church = "Church";

        ///<summary>
        ///A building that contains the administrative offices of a municipal government.
        ///</summary>
        public const string CityHall = "CityHall";

        ///<summary>
        ///A high, steep-to-perpendicular slope that overlooks a lower area or a water body.
        ///</summary>
        public const string Cliff = "Cliff";

        ///<summary>
        ///An area of homogenous climactic conditions, as defined by modified Koeppen classes.
        ///</summary>
        public const string ClimateRegion = "ClimateRegion";

        ///<summary>
        ///An area of land adjacent to a WaterFeature.
        ///</summary>
        public const string Coast = "Coast";

        ///<summary>
        ///A facility for community recreation and meetings.
        ///</summary>
        public const string CommunityCenter = "CommunityCenter";

        ///<summary>
        ///A very large landmass, surrounded by water and larger than an Island, that forms one of the primary divisions of land on a CelestialFeature.
        ///</summary>
        public const string Continent = "Continent";

        ///<summary>
        ///A large meeting hall for conventions and other meetings, and shows.
        ///</summary>
        public const string ConventionCenter = "ConventionCenter";

        ///<summary>
        ///A primary PoliticalUnit.
        ///</summary>
        public const string CountryRegion = "CountryRegion";

        ///<summary>
        ///A building in which courts of law are held.
        ///</summary>
        public const string Courthouse = "Courthouse";

        ///<summary>
        ///A generally circular, saucer-shaped, or bowl-shaped depression caused by volcanic or meteorite explosive action.
        ///</summary>
        public const string Crater = "Crater";

        ///<summary>
        ///An area of land with strong local identity, but no political status.
        ///</summary>
        public const string CulturalRegion = "CulturalRegion";

        ///<summary>
        ///A large area of ocean where surface water flows in a certain constant general direction.
        ///</summary>
        public const string Current = "Current";

        ///<summary>
        ///A barrier constructed across a stream to impound water.
        ///</summary>
        public const string Dam = "Dam";

        ///<summary>
        ///An area where a River divides into many separate water channels as it enters a Sea or a Lake.
        ///</summary>
        public const string Delta = "Delta";

        ///<summary>
        ///A PoliticalUnit that is politically controlled by a Sovereign, but separate geographically, and to some degree politically, such as a territory, a colony, or a dependency.
        ///</summary>
        public const string Dependent = "Dependent";

        ///<summary>
        ///A large area with low rainfall and little or no vegetation.
        ///</summary>
        public const string Desert = "Desert";

        ///<summary>
        ///An area in political dispute that is not considered part of any CountryRegion.
        ///</summary>
        public const string DisputedArea = "DisputedArea";

        ///<summary>
        ///A land region where all surface water drains into one specific WaterFeature.
        ///</summary>
        public const string DrainageBasin = "DrainageBasin";

        ///<summary>
        ///A wave form, a ridge, or a star-shaped feature composed of sand.
        ///</summary>
        public const string Dune = "Dune";

        ///<summary>
        ///A place where the destructive force of a specific earthquake is centered.
        ///</summary>
        public const string EarthquakeEpicenter = "EarthquakeEpicenter";

        ///<summary>
        ///A region with a homogeneous ecosystem, flora, and/or fauna.
        ///</summary>
        public const string Ecoregion = "Ecoregion";

        ///<summary>
        ///A place for providing instruction.
        ///</summary>
        public const string EducationalStructure = "EducationalStructure";

        ///<summary>
        ///An area where the surface elevation of all land is within a defined range.
        ///</summary>
        public const string ElevationZone = "ElevationZone";

        ///<summary>
        ///A building or set of buildings where goods are manufactured, processed, or fabricated.
        ///</summary>
        public const string Factory = "Factory";

        ///<summary>
        ///A route used by a boat, or by other floating conveyances regularly used to transport people and vehicles across a WaterFeature.
        ///</summary>
        public const string FerryRoute = "FerryRoute";

        ///<summary>
        ///A structure and associated facilities where a ferry boat docks and takes on passengers, automobiles, and/or cargo.
        ///</summary>
        public const string FerryTerminal = "FerryTerminal";

        ///<summary>
        ///A place for hatching fish eggs or raising fish.
        ///</summary>
        public const string FishHatchery = "FishHatchery";

        ///<summary>
        ///A large area of trees.
        ///</summary>
        public const string Forest = "Forest";

        ///<summary>
        ///An AdministrativeDivision that no longer exists.
        ///</summary>
        public const string FormerAdministrativeDivision = "FormerAdministrativeDivision";

        ///<summary>
        ///A PoliticalUnit that no longer exists.
        ///</summary>
        public const string FormerPoliticalUnit = "FormerPoliticalUnit";

        ///<summary>
        ///A Sovereign that no longer exists.
        ///</summary>
        public const string FormerSovereign = "FormerSovereign";

        ///<summary>
        ///A defensive structure or earthwork.
        ///</summary>
        public const string Fort = "Fort";

        ///<summary>
        ///An enclosure for displaying selected plant life.
        ///</summary>
        public const string Garden = "Garden";

        ///<summary>
        ///An invisible point, line, or area on the surface of a CelestialFeature that is used for geographic reference.
        ///</summary>
        public const string GeodeticFeature = "GeodeticFeature";

        ///<summary>
        ///A single thing that has spatial extent and location.
        ///</summary>
        public const string GeoEntity = "GeoEntity";

        ///<summary>
        ///One of the two points of intersection of the surface of a CelestialFeature and its axis of rotation.
        ///</summary>
        public const string GeographicPole = "GeographicPole";

        ///<summary>
        ///A HotSpring that intermittently shoots water into the air.
        ///</summary>
        public const string Geyser = "Geyser";

        ///<summary>
        ///A mass of ice, usually at high latitudes or high elevations, with sufficient thickness to flow away from the source area.
        ///</summary>
        public const string Glacier = "Glacier";

        ///<summary>
        ///A recreational field where golf is played.
        ///</summary>
        public const string GolfCourse = "GolfCourse";

        ///<summary>
        ///A Structure typically owned and operated by a governmental entity.
        ///</summary>
        public const string GovernmentStructure = "GovernmentStructure";

        ///<summary>
        ///A place where helicopters land and take off.
        ///</summary>
        public const string Heliport = "Heliport";

        ///<summary>
        ///A half of the surface of a Celestial Feature, usually specified as northern, southern, eastern, or western.
        ///</summary>
        public const string Hemisphere = "Hemisphere";

        ///<summary>
        ///A place where students receive advanced or specialized education, such as a college or a university.
        ///</summary>
        public const string HigherEducationFacility = "HigherEducationFacility";

        ///<summary>
        ///A place of historical importance.
        ///</summary>
        public const string HistoricalSite = "HistoricalSite";

        ///<summary>
        ///A building in which the sick or injured, especially those confined to bed, are medically treated.
        ///</summary>
        public const string Hospital = "Hospital";

        ///<summary>
        ///A place where hot water emerges from the ground.
        ///</summary>
        public const string HotSpring = "HotSpring";

        ///<summary>
        ///A large area covered with frozen water.
        ///</summary>
        public const string Ice = "Ice";

        ///<summary>
        ///An area of land set aside for aboriginal, tribal, or native populations.
        ///</summary>
        public const string IndigenousPeoplesReserve = "IndigenousPeoplesReserve";

        ///<summary>
        ///A Structure used for industrial or extractive purposes.
        ///</summary>
        public const string IndustrialStructure = "IndustrialStructure";

        ///<summary>
        ///A place where tourists and citizens can obtain information.
        ///</summary>
        public const string InformationCenter = "InformationCenter";

        ///<summary>
        ///The line running between geographic poles designated as the point where a calendar day begins.
        ///</summary>
        public const string InternationalDateline = "InternationalDateline";

        ///<summary>
        ///An area of land composed of the member PoliticalUnits of an official governmental organization composed of two or more Sovereigns.
        ///</summary>
        public const string InternationalOrganization = "InternationalOrganization";

        ///<summary>
        ///An area of land completely surrounded by water and smaller than a Continent.
        ///</summary>
        public const string Island = "Island";

        ///<summary>
        ///A narrow strip of land connecting two larger landmasses and bordered by water on two sides.
        ///</summary>
        public const string Isthmus = "Isthmus";

        ///<summary>
        ///A place where two or more roads join.
        ///</summary>
        public const string Junction = "Junction";

        ///<summary>
        ///An inland water body, usually fresh water.
        ///</summary>
        public const string Lake = "Lake";

        ///<summary>
        ///A relatively small area of land exhibiting a common characteristic that distinguishes it from the surrounding land.
        ///</summary>
        public const string LandArea = "LandArea";

        ///<summary>
        ///A natural geographic feature on dry land.
        ///</summary>
        public const string Landform = "Landform";

        ///<summary>
        ///A Structure that is a well-known point of reference.
        ///</summary>
        public const string LandmarkBuilding = "LandmarkBuilding";

        ///<summary>
        ///An imaginary line of constant latitude that circles a CelestialFeature, in which every point on the line is equidistant from a geographic pole.
        ///</summary>
        public const string LatitudeLine = "LatitudeLine";

        ///<summary>
        ///A place where books and other media are stored and loaned out to the public or others.
        ///</summary>
        public const string Library = "Library";

        ///<summary>
        ///A tall structure with a major navigation light.
        ///</summary>
        public const string Lighthouse = "Lighthouse";

        ///<summary>
        ///An area of land where most of the population speaks the same language or speaks languages in the same linguistic family.
        ///</summary>
        public const string LinguisticRegion = "LinguisticRegion";

        ///<summary>
        ///An imaginary line of constant longitude on a CelestialFeature that runs from one geographic pole to the other.
        ///</summary>
        public const string LongitudeLine = "LongitudeLine";

        ///<summary>
        ///A point on the surface of a CelestialFeature that is the origin for lines of magnetic force.
        ///</summary>
        public const string MagneticPole = "MagneticPole";

        ///<summary>
        ///A harbor facility for small boats.
        ///</summary>
        public const string Marina = "Marina";

        ///<summary>
        ///A place where goods are bought and sold.
        ///</summary>
        public const string Market = "Market";

        ///<summary>
        ///A Structure in which the sick or injured are medically treated.
        ///</summary>
        public const string MedicalStructure = "MedicalStructure";

        ///<summary>
        ///A place where urban rapid transit trains pick up and drop off passengers, often underground or elevated.
        ///</summary>
        public const string MetroStation = "MetroStation";

        ///<summary>
        ///A place used by an armed service for storing arms and supplies, for accommodating and training troops, and as a base from which operations can be initiated.
        ///</summary>
        public const string MilitaryBase = "MilitaryBase";

        ///<summary>
        ///A place where mineral ores are extracted from the ground by excavating surface pits and subterranean passages.
        ///</summary>
        public const string Mine = "Mine";

        ///<summary>
        ///A place characterized by dwellings, school, church, hospital, and other facilities operated by a religious group for the purpose of providing charitable services and to propagate religion.
        ///</summary>
        public const string Mission = "Mission";

        ///<summary>
        ///A commemorative structure or statue.
        ///</summary>
        public const string Monument = "Monument";

        ///<summary>
        ///A building for public Islamic worship.
        ///</summary>
        public const string Mosque = "Mosque";

        ///<summary>
        ///An elevated landform that rises, often steeply, above surrounding land on most sides.
        ///</summary>
        public const string Mountain = "Mountain";

        ///<summary>
        ///A group of connected Mountains.
        ///</summary>
        public const string MountainRange = "MountainRange";

        ///<summary>
        ///A building where objects of permanent interest in one or more of the arts and sciences are preserved and exhibited.
        ///</summary>
        public const string Museum = "Museum";

        ///<summary>
        ///A Structure used for nautical purposes.
        ///</summary>
        public const string NauticalStructure = "NauticalStructure";

        ///<summary>
        ///A Structure used for navigational purposes.
        ///</summary>
        public const string NavigationalStructure = "NavigationalStructure";

        ///<summary>
        ///A section of a PopulatedPlace, usually homogenous and/or well-known, but often with indistinct boundaries.
        ///</summary>
        public const string Neighborhood = "Neighborhood";

        ///<summary>
        ///An area in a Desert that contains water and plant life.
        ///</summary>
        public const string Oasis = "Oasis";

        ///<summary>
        ///A wildlife or scenic observation point.
        ///</summary>
        public const string ObservationPoint = "ObservationPoint";

        ///<summary>
        ///A vast expanse of salt water, one of the major Seas covering part of the earth.
        ///</summary>
        public const string Ocean = "Ocean";

        ///<summary>
        ///A building that contains offices.
        ///</summary>
        public const string OfficeBuilding = "OfficeBuilding";

        ///<summary>
        ///An area maintained as a place of scenic beauty, or for recreation.
        ///</summary>
        public const string Park = "Park";

        ///<summary>
        ///A parking lot reserved for mass transit commuters.
        ///</summary>
        public const string ParkAndRide = "ParkAndRide";

        ///<summary>
        ///A break in a MountainRange used for transportation from one side of the mountain range to the other.
        ///</summary>
        public const string Pass = "Pass";

        ///<summary>
        ///An elongated area of land projecting into a body of water and surrounded by water on three sides.
        ///</summary>
        public const string Peninsula = "Peninsula";

        ///<summary>
        ///An extensive area of comparatively level to gently undulating land, lacking surface irregularities.
        ///</summary>
        public const string Plain = "Plain";

        ///<summary>
        ///A CelestialFeature that orbits a star.
        ///</summary>
        public const string Planet = "Planet";

        ///<summary>
        ///A section of a planetary crust that is in motion relative to other tectonic plates.
        ///</summary>
        public const string Plate = "Plate";

        ///<summary>
        ///An elevated plain with steep slopes on one or more sides.
        ///</summary>
        public const string Plateau = "Plateau";

        ///<summary>
        ///A tract of land used for playing team sports and/or other athletic events.
        ///</summary>
        public const string PlayingField = "PlayingField";

        ///<summary>
        ///A point on the surface of a CelestialFeature that marks an important geographical or astronomical location.
        ///</summary>
        public const string Pole = "Pole";

        ///<summary>
        ///A building in which police are stationed or posted.
        ///</summary>
        public const string PoliceStation = "PoliceStation";

        ///<summary>
        ///An area of land with well-defined borders that is subject to a specific political administration.
        ///</summary>
        public const string PoliticalUnit = "PoliticalUnit";

        ///<summary>
        ///A concentrated area of human settlement, such as a city, a town, or a village.
        ///</summary>
        public const string PopulatedPlace = "PopulatedPlace";

        ///<summary>
        ///A district used by a postal service as an aid in postal distribution and having a unique identifying code.
        ///</summary>
        public const string Postcode = "Postcode";

        ///<summary>
        ///One of the set of lowest-level and most detailed set of PostCodes in a Sovereign.
        ///</summary>
        public const string Postcode1 = "Postcode1";

        ///<summary>
        ///One of the set of second-order (one level up from the lowest level) Postcodes in a Sovereign, composed by aggregating Postcode1s.
        ///</summary>
        public const string Postcode2 = "Postcode2";

        ///<summary>
        ///One of the set of third-order Postcodes in a Sovereign, composed by aggregating Postcode2s.
        ///</summary>
        public const string Postcode3 = "Postcode3";

        ///<summary>
        ///One of the set of fourth-order Postcodes in a Sovereign, composed by aggregating Postcode3s.
        ///</summary>
        public const string Postcode4 = "Postcode4";

        ///<summary>
        ///A public building in which mail is received, sorted, and distributed.
        ///</summary>
        public const string PostOffice = "PostOffice";

        ///<summary>
        ///A facility for generating electric power.
        ///</summary>
        public const string PowerStation = "PowerStation";

        ///<summary>
        ///A facility for confining persons convicted or accused of crimes.
        ///</summary>
        public const string Prison = "Prison";

        ///<summary>
        ///A small, usually pointed Peninsula that often marks the terminus of a landmass.
        ///</summary>
        public const string Promontory = "Promontory";

        ///<summary>
        ///A track where races are held.
        ///</summary>
        public const string RaceTrack = "RaceTrack";

        ///<summary>
        ///A permanent twin steel-rail track on which trains move.
        ///</summary>
        public const string Railway = "Railway";

        ///<summary>
        ///A place comprised of ticket offices, platforms, and other facilities for loading and unloading train passengers and freight.
        ///</summary>
        public const string RailwayStation = "RailwayStation";

        ///<summary>
        ///A Structure used for watching or participating in sports or other athletic activities.
        ///</summary>
        public const string RecreationalStructure = "RecreationalStructure";

        ///<summary>
        ///A partly submerged feature, usually of coral, that projects upward near the water"s surface and can be a navigational hazard.
        ///</summary>
        public const string Reef = "Reef";

        ///<summary>
        ///A large area of land where a specific characteristic of the land or its people is relatively homogenous.
        ///</summary>
        public const string Region = "Region";

        ///<summary>
        ///An area of land where the population holds relatively homogenous religious practices.
        ///</summary>
        public const string ReligiousRegion = "ReligiousRegion";

        ///<summary>
        ///A structure where organized, public religious services are held.
        ///</summary>
        public const string ReligiousStructure = "ReligiousStructure";

        ///<summary>
        ///A Structure used for scientific purposes.
        ///</summary>
        public const string ResearchStructure = "ResearchStructure";

        ///<summary>
        ///A tract of public land set aside for restricted use or reserved for future use.
        ///</summary>
        public const string Reserve = "Reserve";

        ///<summary>
        ///A house, a hut, an apartment building, or another structure where people reside.
        ///</summary>
        public const string ResidentialStructure = "ResidentialStructure";

        ///<summary>
        ///A designated area, usually along a major highway, where motorists can stop to relax.
        ///</summary>
        public const string RestArea = "RestArea";

        ///<summary>
        ///A stream of running water.
        ///</summary>
        public const string River = "River";

        ///<summary>
        ///An open way with an improved surface for efficient transportation of vehicles.
        ///</summary>
        public const string Road = "Road";

        ///<summary>
        ///A road.
        ///</summary>
        public const string RoadBlock = "RoadBlock";

        ///<summary>
        ///A junction where two or more roads meet or cross at the same grade.
        ///</summary>
        public const string RoadIntersection = "RoadIntersection";

        ///<summary>
        ///A destroyed or decayed structure that is no longer functional.
        ///</summary>
        public const string Ruin = "Ruin";

        ///<summary>
        ///A CelestialFeature that orbits a Planet.
        ///</summary>
        public const string Satellite = "Satellite";

        ///<summary>
        ///A place where people, usually children, receive a basic education.
        ///</summary>
        public const string School = "School";

        ///<summary>
        ///A scientific facility used as a base from which research is carried out or monitored.
        ///</summary>
        public const string ScientificResearchBase = "ScientificResearchBase";

        ///<summary>
        ///A large area of salt water.
        ///</summary>
        public const string Sea = "Sea";

        ///<summary>
        ///A place on a water body where floatplanes land and take off.
        ///</summary>
        public const string SeaplaneLandingArea = "SeaplaneLandingArea";

        ///<summary>
        ///A site of the remains of a wrecked vessel.
        ///</summary>
        public const string ShipWreck = "ShipWreck";

        ///<summary>
        ///A collection of linked retail establishments.
        ///</summary>
        public const string ShoppingCenter = "ShoppingCenter";

        ///<summary>
        ///A structure or place that memorializes a person or religious concept.
        ///</summary>
        public const string Shrine = "Shrine";

        ///<summary>
        ///A place most notable because of an event that occurred in that location.
        ///</summary>
        public const string Site = "Site";

        ///<summary>
        ///A place developed for recreational Alpine or Nordic skiing.
        ///</summary>
        public const string SkiArea = "SkiArea";

        ///<summary>
        ///An independent nation-state, the highest level of political authority in that location.
        ///</summary>
        public const string Sovereign = "Sovereign";

        ///<summary>
        ///A point on a CelestialFeature"s surface with a known elevation.
        ///</summary>
        public const string SpotElevation = "SpotElevation";

        ///<summary>
        ///A place where water emerges from the ground.
        ///</summary>
        public const string Spring = "Spring";

        ///<summary>
        ///A structure with an enclosure for athletic games with tiers of seats for spectators.
        ///</summary>
        public const string Stadium = "Stadium";

        ///<summary>
        ///An area of land defined as a district to be used for statistical collection or service provision.
        ///</summary>
        public const string StatisticalDistrict = "StatisticalDistrict";

        ///<summary>
        ///A building, a facility, or a group of buildings and/or facilities used for a certain common purpose.
        ///</summary>
        public const string Structure = "Structure";

        ///<summary>
        ///A line that forms the border between two Plates.
        ///</summary>
        public const string TectonicBoundary = "TectonicBoundary";

        ///<summary>
        ///A Landform related to Plates and their movement.
        ///</summary>
        public const string TectonicFeature = "TectonicFeature";

        ///<summary>
        ///An edifice dedicated to religious worship.
        ///</summary>
        public const string Temple = "Temple";

        ///<summary>
        ///A large area within which the same time standard is used.
        ///</summary>
        public const string TimeZone = "TimeZone";

        ///<summary>
        ///A Structure typically used by tourists.
        ///</summary>
        public const string TouristStructure = "TouristStructure";

        ///<summary>
        ///A path, a track, or a route used by pedestrians, animals, or off-road vehicles.
        ///</summary>
        public const string Trail = "Trail";

        ///<summary>
        ///A Structure used for transportation purposes.
        ///</summary>
        public const string TransportationStructure = "TransportationStructure";

        ///<summary>
        ///A subterranean passageway for transportation.
        ///</summary>
        public const string Tunnel = "Tunnel";

        ///<summary>
        ///A feature on the floor of a WaterFeature.
        ///</summary>
        public const string UnderwaterFeature = "UnderwaterFeature";

        ///<summary>
        ///An area of land with high population density and extensive urban development.
        ///</summary>
        public const string UrbanRegion = "UrbanRegion";

        ///<summary>
        ///A low area surrounded by higher ground on two or more sides.
        ///</summary>
        public const string Valley = "Valley";

        ///<summary>
        ///A Mountain formed by volcanic action and composed of volcanic rock.
        ///</summary>
        public const string Volcano = "Volcano";

        ///<summary>
        ///An upright structure that encloses, divides, or protects an area.
        ///</summary>
        public const string Wall = "Wall";

        ///<summary>
        ///A vertical or very steep section of a River.
        ///</summary>
        public const string Waterfall = "Waterfall";

        ///<summary>
        ///A geographic feature that has water on its surface.
        ///</summary>
        public const string WaterFeature = "WaterFeature";

        ///<summary>
        ///A cylindrical hole, pit, or tunnel drilled or dug down to a depth from which water, oil, or gas can be pumped or brought to the surface.
        ///</summary>
        public const string Well = "Well";

        ///<summary>
        ///An area of high soil moisture, partially or intermittently covered with shallow water.
        ///</summary>
        public const string Wetland = "Wetland";

        ///<summary>
        ///A zoological garden or park where wild animals are kept for exhibition.
        ///</summary>
        public const string Zoo = "Zoo";
    }
}
