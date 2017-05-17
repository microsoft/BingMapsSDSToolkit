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

using BingSDSTestApp.Views;
using BingMapsSDSToolkit;
using BingMapsSDSToolkit.DataSourceAPI;
using BingMapsSDSToolkit.GeocodeDataflowAPI;
using BingMapsSDSToolkit.GeodataAPI;
using BingMapsSDSToolkit.QueryAPI;
using Microsoft.Maps.MapControl.WPF;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Xml.Serialization;

namespace BingSDSTestApp
{
    public partial class MainWindow : Window
    {
        #region Private Properties

        private DataSourceManager dataSourceManager;

        private static string BingMapsKey = ConfigurationManager.AppSettings.Get("BingMapsKey");

        private BasicDataSourceInfo _datasourceApiDataSourceSettings = new BasicDataSourceInfo()
        {
            MasterKey = BingMapsKey
        };

        private BasicDataSourceInfo _queryApiDataSourceSettings = new BasicDataSourceInfo()
        {
            //QueryURL = "http://spatial.virtualearth.net/REST/v1/data/f22876ec257b474b82fe2ffcb8393150/NavteqNA/NavteqPOIs",
            QueryURL = "https://spatial.virtualearth.net/REST/v1/data/20181f26d9e94c81acdf9496133d4f23/FourthCoffeeSample/FourthCoffeeShops",
            QueryKey = BingMapsKey,
        };

        #endregion

        #region Constructor

        public MainWindow()
        {
            InitializeComponent();

            dataSourceManager = new DataSourceManager();

            MyMap.CredentialsProvider = new ApplicationIdCredentialsProvider(BingMapsKey);
        }

        #endregion

        #region Data Source API Code Samples

        private void DataSourceAPIDataSourceSettings_Clicked(object sender, RoutedEventArgs e)
        {
            var modal = new DataSourceInfoModal()
            {
                DataContext = _datasourceApiDataSourceSettings
            };

            modal.ShowDialog();
        }
        
        /// <summary>
        /// Gets a list of all current and recent jobs performed by the account and/or key specified.
        /// </summary>
        private async void GetJobList_Clicked(object sender, RoutedEventArgs e)
        {
            Clear();
            var key = GetDataSourceBingMapsKey();

            LoadingPanel.Visibility = System.Windows.Visibility.Visible;

            if (!string.IsNullOrEmpty(key))
            {
                try
                {
                    var jobs = await dataSourceManager.GetJobList(key);
                    DataSourceJobs.ItemsSource = jobs;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

            LoadingPanel.Visibility = System.Windows.Visibility.Collapsed;
        }

        /// <summary>
        /// Gets a list of all public data sources.
        /// </summary>
        private async void GetPublicDataSources_Clicked(object sender, RoutedEventArgs e)
        {
            Clear();
            
            LoadingPanel.Visibility = System.Windows.Visibility.Visible;

            var key = GetDataSourceBingMapsKey();

            if (!string.IsNullOrEmpty(key))
            {
                try
                {
                    var ds = await dataSourceManager.GetAllPublicDataSources(key);
                    UpdateDataSourceTable(ds);
                    DataSourceTableMessage.Text = string.Format("{0} Data Sources Found.", ds.Count);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

            LoadingPanel.Visibility = System.Windows.Visibility.Collapsed;
        }

        /// <summary>
        /// Gets a list of data sources in the account of the specified key.
        /// </summary>
        private async void GetAllDataSourceDetails_Clicked(object sender, RoutedEventArgs e)
        {
            Clear();
            
            LoadingPanel.Visibility = System.Windows.Visibility.Visible;

            var key = GetDataSourceBingMapsKey();

            if (!string.IsNullOrEmpty(key))
            {
                try
                {
                    var ds = await dataSourceManager.GetDetails(key);
                    UpdateDataSourceTable(ds);
                    DataSourceTableMessage.Text = string.Format("{0} Data Sources Found.", ds.Count);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

            LoadingPanel.Visibility = System.Windows.Visibility.Collapsed;
        }

        /// <summary>
        /// Opens a tool to convert a data source from one format to another.
        /// </summary>
        private void ConvertDataSource_Clicked(object sender, RoutedEventArgs e)
        {
            try
            {
                var modal = new DataSourceConverterModal();
                modal.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Gets the details of a specified data source.
        /// </summary>
        private async void GetDataSourceDetails_Clicked(object sender, RoutedEventArgs e)
        {
            Clear();

            if(string.IsNullOrEmpty(_datasourceApiDataSourceSettings.AccessId) ||
               string.IsNullOrEmpty(_datasourceApiDataSourceSettings.DataSourceName))
            {
                MessageBox.Show("Access id and data source name not specified in data source settings.");
                return;
            }

            LoadingPanel.Visibility = System.Windows.Visibility.Visible;

            var key = GetDataSourceBingMapsKey();

            if (!string.IsNullOrEmpty(key))
            {
                try
                {
                    var ds = await dataSourceManager.GetDetails(_datasourceApiDataSourceSettings);

                    if (ds.Count > 0)
                    {
                        UpdateDataSourceTable(ds);
                    }
                    else
                    {
                        UpdateDataSourceTable(null);
                    }

                    DataSourceTableMessage.Text = string.Format("{0} Data Sources Found.", ds.Count);
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

            LoadingPanel.Visibility = System.Windows.Visibility.Collapsed;
        }

        /// <summary>
        /// Changes the private setting of a data source.
        /// </summary>
        private async void ChangePrivateSetting_Clicked(object sender, RoutedEventArgs e)
        {
            Clear();

            LoadingPanel.Visibility = System.Windows.Visibility.Visible;

            var dsd = (sender as Button).Tag as DataSourceDetails;

            dsd.MasterKey = GetDataSourceBingMapsKey();

            if (!string.IsNullOrEmpty(dsd.MasterKey))
            {
                var job = await dataSourceManager.SetPublicSetting((BasicDataSourceInfo)dsd, !dsd.IsPublic);

                //Update data source table
                if (string.Compare(job.Status, "Completed", true) == 0 ||
                    string.Compare(job.Status, "Accepted", true) == 0)
                {
                    var items = DataSourceTable.ItemsSource as List<DataSourceDetails>;
                    for (int i = 0; i < items.Count; i++)
                    {
                        if (items[i] == dsd)
                        {
                            items[i].IsPublic = !items[i].IsPublic;
                            break;
                        }
                    }
                    UpdateDataSourceTable(items);
                }
                else if (!string.IsNullOrEmpty(job.ErrorMessage))
                {
                    MessageBox.Show(job.ErrorMessage);
                }
            }
            else
            {
                MessageBox.Show("No master key specified in data source settings.");
            }

            LoadingPanel.Visibility = System.Windows.Visibility.Collapsed;
        }

        /// <summary>
        /// Downloads a data sources
        /// </summary>
        private void DownloadDataSource_Clicked(object sender, RoutedEventArgs e)
        {
            var dsd = (sender as Button).Tag as DataSourceDetails;

            dsd.MasterKey = GetDataSourceBingMapsKey();

            var modal = new DownloadModal(dsd);
            modal.Show();
        }

        /// <summary>
        /// Deletes a data source.
        /// </summary>
        private void DeleteDataSource_Clicked(object sender, RoutedEventArgs e)
        {
            var dsd = (sender as Button).Tag as DataSourceDetails;

            dsd.MasterKey = GetDataSourceBingMapsKey();

            var modal = new DeleteModal(dsd);
            var b = modal.ShowDialog();

            if (b.HasValue && b.Value)
            {
                var items = DataSourceTable.ItemsSource as List<DataSourceDetails>;
                items.Remove(dsd);
                UpdateDataSourceTable(items);
            }
        }

        /// <summary>
        /// Uploads a data source.
        /// </summary>
        private void UploadDataSource_Clicked(object sender, RoutedEventArgs e)
        {
            try
            {
                var modal = new UploadModal(_datasourceApiDataSourceSettings);
                modal.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Sets the data source information so that it can be used with the Query API sample.
        /// </summary>
        private void UseWithQueryAPI_Clicked(object sender, RoutedEventArgs e)
        {
            var dsd = (sender as Button).Tag as DataSourceDetails;
            _queryApiDataSourceSettings.QueryURL = dsd.QueryURL;

            if (string.IsNullOrWhiteSpace(_queryApiDataSourceSettings.QueryKey) || string.IsNullOrWhiteSpace(_queryApiDataSourceSettings.MasterKey))
            {
                if (!string.IsNullOrWhiteSpace(_datasourceApiDataSourceSettings.QueryKey))
                {
                    _queryApiDataSourceSettings.QueryKey = _datasourceApiDataSourceSettings.QueryKey;
                }else if (!string.IsNullOrWhiteSpace(_datasourceApiDataSourceSettings.MasterKey))
                {
                    _queryApiDataSourceSettings.QueryKey = _datasourceApiDataSourceSettings.MasterKey;
                }
            }

            QueryApiTab.IsSelected = true;
        }

        private string GetDataSourceBingMapsKey()
        {
            var key = (!string.IsNullOrEmpty(_datasourceApiDataSourceSettings.MasterKey)) ? _datasourceApiDataSourceSettings.MasterKey : _datasourceApiDataSourceSettings.QueryKey;

            if (!string.IsNullOrEmpty(key))
            {
                return key;
            }
            else
            {
                MessageBox.Show("No Bing Maps key specified. Specify a Bing Maps master or query key in the data source settings.");
                return null;
            } 
        }

        private void UpdateDataSourceTable(System.Collections.IEnumerable data)
        {
            DataSourceTable.ItemsSource = null;
            DataSourceTable.ItemsSource = data;
        }

        #endregion

        #region Geodata API Code Samples

        /// <summary>
        /// Gets a boundary from the Geodata API.
        /// </summary>
        private void GetGeodataBoundary_Clicked(object sender, RoutedEventArgs e)
        {
            var request = new GetBoundaryRequest()
            {               
                EntityType = (BoundaryEntityType)Enum.Parse(typeof(BoundaryEntityType), ((string)(GeodataEntityTypeCbx.SelectedItem as ComboBoxItem).Content)),
                LevelOfDetail = int.Parse((string)(GeodataLevelOfDetailCbx.SelectedItem as ComboBoxItem).Content),
                UserRegion = (string)(GeodataRegionCbx.SelectedItem as ComboBoxItem).Content,
                GetAllPolygons = (GeodataGetPolysChbx.IsChecked.HasValue)? GeodataGetPolysChbx.IsChecked.Value : false,
                GetEntityMetadata = (GeodataGetMetadataChbx.IsChecked.HasValue)? GeodataGetMetadataChbx.IsChecked.Value : false,
                Culture = "en-US"
            };

            double lat, lon;

            if (!string.IsNullOrEmpty(GeodataLatTbx.Text) && !string.IsNullOrEmpty(GeodataLonTbx.Text) &&
                double.TryParse(GeodataLatTbx.Text, out lat) && double.TryParse(GeodataLonTbx.Text, out lon))
            {
                request.Coordinate = new BingMapsSDSToolkit.GeodataLocation(lat, lon);
            }
            else if (!string.IsNullOrWhiteSpace(GeodataAddressTbx.Text))
            {
                request.Address = GeodataAddressTbx.Text;
            }

            if (request.Coordinate != null || !string.IsNullOrWhiteSpace(request.Address))
            {
                MakeGeodataRequest(request);
            }
            else
            {
                MessageBox.Show("Invalid location information provided.");
            }
        }

        /// <summary>
        /// Makes a request to get boundary data from the Geodata API.
        /// </summary>
        private async void MakeGeodataRequest(GetBoundaryRequest request)
        {
            Clear();
            OutputTab.IsSelected = true;

            try
            {
                var response = await GeodataManager.GetBoundary(request, BingMapsKey);

                if (response != null && response.Count > 0)
                {
                    var locs = new List<Microsoft.Maps.MapControl.WPF.Location>();

                    var sb = new StringBuilder();
                    sb.AppendFormat("Found {0} result(s) for query.\r\n\r\nResults:\r\n", response.Count);

                    for (int i = 0; i < response.Count; i++)
                    {
                        var r = response[i];

                        sb.AppendFormat("[{0}]\r\n", i);

                        if (r.Primitives != null)
                        {
                            sb.AppendFormat("\tConsists of {0} Polygons", r.Primitives.Length);
                            
                            int numPoints = 0;

                            foreach(var p in r.Primitives)
                            {
                                var poly = p.GetPolygon();

                                numPoints += int.Parse(p.NumPoints);

                                var shapes = CreateComplexPolygon(poly);

                                if (shapes != null)
                                {
                                    foreach (var s in shapes)
                                    {
                                        MyMap.Children.Add(s);
                                    }

                                    locs.AddRange(shapes[0].Locations);
                                }
                            }

                            sb.AppendFormat(" with {0} points\r\n\r\n", numPoints);
                        }                        

                        if (r.EntityMetadata != null)
                        {
                            sb.AppendLine("\tMetadata:");

                            if (!string.IsNullOrEmpty(r.EntityMetadata.AreaSqKm))
                            {
                                sb.AppendFormat("\t\tArea in SQ KM: {0}\r\n", r.EntityMetadata.AreaSqKm);
                            }

                            if (!string.IsNullOrEmpty(r.EntityMetadata.BestMapViewBox))
                            {
                                sb.AppendFormat("\t\tBest Map View Box: {0}\r\n", r.EntityMetadata.BestMapViewBox);
                            }

                            if (!string.IsNullOrEmpty(r.EntityMetadata.OfficialCulture))
                            {
                                sb.AppendFormat("\t\tOfficial Culture: {0}\r\n", r.EntityMetadata.OfficialCulture);
                            }

                            if (!string.IsNullOrEmpty(r.EntityMetadata.PopulationClass))
                            {
                                sb.AppendFormat("\t\tPopulation Class: {0}\r\n", r.EntityMetadata.PopulationClass);
                            }

                            if (!string.IsNullOrEmpty(r.EntityMetadata.RegionalCulture))
                            {
                                sb.AppendFormat("\t\tRegional Culture: {0}\r\n", r.EntityMetadata.RegionalCulture);
                            }
                        }

                        if (r.Name != null)
                        {
                            sb.AppendLine("\tName:");

                            if (!string.IsNullOrEmpty(r.Name.Culture))
                            {
                                sb.AppendFormat("\t\tOfficial Culture: {0}\r\n", r.EntityMetadata.OfficialCulture);
                            }

                            if (!string.IsNullOrEmpty(r.Name.EntityName))
                            {
                                sb.AppendFormat("\t\tEntity Name: {0}\r\n", r.Name.EntityName);
                            }
                        }
                    }

                    OutputTbx.Text = sb.ToString();

                    SetMapView(locs);
                }
                else
                {
                    OutputTbx.Text += "No results found";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #endregion

        #region Query API Code Samples

        private void QueryAPIDataSourceSettings_Clicked(object sender, RoutedEventArgs e)
        {
            var modal = new DataSourceInfoModal()
            {
                DataContext = _queryApiDataSourceSettings
            };

            modal.ShowDialog();
        }

        /// <summary>
        /// Performs a search by ID on a data data source.
        /// </summary>
        private void FindByID_Clicked(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(FindByIdTbx.Text))
            {
                var request = new FindByPropertyRequest(_queryApiDataSourceSettings)
                {
                    Filter = new FilterExpression("entityId", LogicalOperator.IsIn, new List<string>(FindByIdTbx.Text.Split(',')))
                    
                };
                
                MakeQueryRequest(request);
            }
            else
            {
                MessageBox.Show("Invlaid list of IDs.");
            }
        }

        /// <summary>
        /// Performs an intersection search on a data data source.
        /// </summary>
        private void IntersectionSearch_Clicked(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(WKTIntersectionTbx.Text))
            {
                var request = new IntersectionSearchRequest(_queryApiDataSourceSettings)
                {
                    Geography = new Geography()
                    {
                        WellKnownText = WKTIntersectionTbx.Text
                    }
                };

                MakeQueryRequest(request);
            }
            else
            {
                MessageBox.Show("Invlaid list of IDs.");
            }
        }

        /// <summary>
        /// Performs a nearby (radial) search on a data data source.
        /// </summary>
        private void FindNearBy_Clicked(object sender, RoutedEventArgs e)
        {
            double lat, lon, distance = double.Parse((string)(DistanceCbx.SelectedItem as ComboBoxItem).Content);

            var request = new FindNearByRequest(_queryApiDataSourceSettings)
            {
                Distance = distance,
                DistanceUnits = (DistanceUnitType)Enum.Parse(typeof(DistanceUnitType), ((string)(DistanceUnitCbx.SelectedItem as ComboBoxItem).Content))
            };

            if (!string.IsNullOrEmpty(FNBLatTbx.Text) && !string.IsNullOrEmpty(FNBLonTbx.Text) &&
                double.TryParse(FNBLatTbx.Text, out lat) && double.TryParse(FNBLonTbx.Text, out lon))
            {
                request.Center = new BingMapsSDSToolkit.GeodataLocation(lat, lon);
            }
            else if (!string.IsNullOrWhiteSpace(FNBAddressTbx.Text))
            {
                request.Address = FNBAddressTbx.Text;
            }
            
            if (request.Center != null || !string.IsNullOrWhiteSpace(request.Address))
            {
                MakeQueryRequest(request);
            }
            else
            {
                MessageBox.Show("Invalid location information provided.");
            }
        }

        /// <summary>
        /// Performs a bounding box search on a data data source.
        /// </summary>
        private void FindInBoundingBox_Clicked(object sender, RoutedEventArgs e)
        {
            double north, south, east, west;

            if (!string.IsNullOrEmpty(FibbNorthTbx.Text) && double.TryParse(FibbNorthTbx.Text, out north) &&
                !string.IsNullOrEmpty(FibbSouthTbx.Text) && double.TryParse(FibbSouthTbx.Text, out south) &&
                !string.IsNullOrEmpty(FibbWestTbx.Text) && double.TryParse(FibbWestTbx.Text, out west) &&
                !string.IsNullOrEmpty(FibbEastTbx.Text) && double.TryParse(FibbEastTbx.Text, out east))
            {
                var request = new FindInBoundingBoxRequest(_queryApiDataSourceSettings)
                {
                    North = north,
                    South = south,
                    West = west,
                    East = east,
                    DistanceUnits = (DistanceUnitType)Enum.Parse(typeof(DistanceUnitType), ((string)(DistanceUnitCbx.SelectedItem as ComboBoxItem).Content))
                };

                MakeQueryRequest(request);
            }
            else
            {
                MessageBox.Show("Invalid bounding coordinates provided.");
            }
        }

        /// <summary>
        /// Performs a search along a route on a data data source.
        /// </summary>
        private void FindNearRoute_Clicked(object sender, RoutedEventArgs e)
        {
            double lat, lon;

            var request = new FindNearRouteRequest(_queryApiDataSourceSettings)
            {
                DistanceUnits = (DistanceUnitType)Enum.Parse(typeof(DistanceUnitType), ((string)(DistanceUnitCbx.SelectedItem as ComboBoxItem).Content)),
                TravelMode = (TravelModeType)Enum.Parse(typeof(TravelModeType), ((string)(TravelModeCbx.SelectedItem as ComboBoxItem).Content)),
                Optimize = (RouteOptimizationType)Enum.Parse(typeof(RouteOptimizationType), ((string)(RouteOptimizationCbx.SelectedItem as ComboBoxItem).Content)),
            };

            if (!string.IsNullOrEmpty(FNRSLatTbx.Text) && double.TryParse(FNRSLatTbx.Text, out lat) &&
                !string.IsNullOrEmpty(FNRSLonTbx.Text) && double.TryParse(FNRSLonTbx.Text, out lon))
            {
                request.StartLocation = new BingMapsSDSToolkit.GeodataLocation(lat, lon);
            }
            else if (!string.IsNullOrWhiteSpace(FNRSAddressTbx.Text))
            {
                request.StartAddress = FNRSAddressTbx.Text;
            }

            if (!string.IsNullOrEmpty(FNRELatTbx.Text) && double.TryParse(FNRELatTbx.Text, out lat) &&
               !string.IsNullOrEmpty(FNRELonTbx.Text) && double.TryParse(FNRELonTbx.Text, out lon))
            {
                request.EndLocation = new BingMapsSDSToolkit.GeodataLocation(lat, lon);
            }
            else if (!string.IsNullOrWhiteSpace(FNREAddressTbx.Text))
            {
                request.EndAddress = FNREAddressTbx.Text;
            }

            if ((request.StartLocation != null || !string.IsNullOrWhiteSpace(request.StartAddress)) &&
                (request.EndLocation != null || !string.IsNullOrWhiteSpace(request.EndAddress)))
            {
                MakeQueryRequest(request);
            }
            else
            {
                MessageBox.Show("Invalid location information provided.");
            }
        }
        
        private async void MakeQueryRequest(FindByPropertyRequest request)
        {
            request.Top = int.Parse((string)(TopCbx.SelectedItem as ComboBoxItem).Content);

            Clear();
            
            OutputTbx.Text = string.Format("Request URL:\r\n\r\n{0}\r\n\r\n", request.GetRequestUrl());            
            OutputTab.IsSelected = true;

            try
            {
                var response = await QueryManager.ProcessQuery(request);

                if (string.IsNullOrEmpty(response.ErrorMessage))
                {
                    if(response != null &&
                        response.Results != null &&
                        response.Results.Count > 0)
                    {
                        var locs = new List<Microsoft.Maps.MapControl.WPF.Location>();

                        var sb = new StringBuilder();
                        sb.AppendFormat("Found {0} results for query.\r\n\r\nResults:\r\n", response.Results.Count);

                        for (int i = 0; i < response.Results.Count; i++)
                        {
                            sb.AppendFormat("[{0}]\r\n", i);
                            sb.AppendFormat("\tLatitude: {0}\r\n", response.Results[i].Location.Latitude);
                            sb.AppendFormat("\tLongitude: {0}\r\n", response.Results[i].Location.Longitude);
                            sb.AppendFormat("\tDistance: {0:G2} {1}\r\n", response.Results[i].Distance, request.DistanceUnits);
                            
                            foreach (var p in response.Results[i].Properties)
                            {
                                sb.AppendFormat("\t{0}: {1}\r\n", p.Key, p.Value);
                            }

                            var pin = new Pushpin()
                            {
                                Content = i,
                                Location = new Microsoft.Maps.MapControl.WPF.Location()
                                {
                                    Latitude = response.Results[i].Location.Latitude,
                                    Longitude = response.Results[i].Location.Longitude
                                }
                            };

                            locs.Add(pin.Location);
                            MyMap.Children.Add(pin);
                        }

                        OutputTbx.Text += sb.ToString();

                        SetMapView(locs);
                    }
                    else
                    {
                        OutputTbx.Text += "No results found";
                    }
                }
                else
                {
                    OutputTbx.Text += response.ErrorMessage;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #endregion

        #region Private Methods

        private void Clear()
        {
            MyMap.Children.Clear(); 
            OutputTbx.Text = string.Empty;
        }

        private void SetMapView(List<Microsoft.Maps.MapControl.WPF.Location> locs)
        {
            if (locs.Count > 1)
            {
                var rect = new LocationRect(locs);
                MyMap.SetView(new LocationRect(rect.Center, rect.Width * 1.5, rect.Height * 1.5));
            }
            else if (locs.Count == 1)
            {
                MyMap.SetView(locs[0], 15);
            }
        }

        public static List<MapShapeBase> CreateComplexPolygon(GeodataPolygon polygon)
        {
            if (polygon != null && polygon.ExteriorRing != null && polygon.ExteriorRing.Count >= 3)
            {
                var shapes = new List<MapShapeBase>();
                var locs = polygon.ExteriorRing;

                //Ensure polygon is closed.
                locs.Add(locs[0]);

                //This uses a workaround to create outline of polygon
                var l = CreateMapPolyline(locs);

                if (l != null)
                {
                    shapes.Add(l);
                }

                //Outline inner rings.
                foreach (var i in polygon.InnerRings)
                {
                    //Ensure inner ring is closed.
                    i.Add(i[0]);

                    l = CreateMapPolyline(i);

                    if (l != null)
                    {
                        shapes.Add(l);

                        //Add inner rings to main list of locations
                        locs.AddRange(i);

                        //Loop back to starting point.
                        locs.Add(locs[0]);
                    }
                }

                var poly = new MapPolygon()
                {
                    Locations = CreateMapPolyline(locs).Locations,
                    Fill = new SolidColorBrush(Color.FromArgb(150, 0, 255, 0))
                };

                shapes.Insert(0, poly);

                return shapes;
            }
            
            return null;
        }
        
        public static MapPolyline CreateMapPolyline(List<GeodataLocation> coordinates)
        {
            if (coordinates != null && coordinates.Count >= 2)
            {
                var locs = new LocationCollection();

                foreach (var c in coordinates)
                {
                    locs.Add(new Microsoft.Maps.MapControl.WPF.Location(c.Latitude, c.Longitude));
                }

                return new MapPolyline()
                {
                    Locations = locs,
                    StrokeThickness = 2,
                    Stroke = new SolidColorBrush(Color.FromArgb(150, 0, 0, 150))
                };
            }

            return null;
        }

        private async void ProcessBatchGeocode_Clicked(object sender, RoutedEventArgs e)
        {
            SucceededBatchGeocodes.Text = string.Empty;
            FailedBatchGeocodes.Text = string.Empty;

            var type = (BatchFileFormat)Enum.Parse(typeof(BatchFileFormat), ((string)(BatchFileFormatCbx.SelectedItem as ComboBoxItem).Content));

            string filters, defaultExt;
            FileExtensionUtilities.GetFileExtensions(type, out defaultExt, out filters);

            var ofd = new OpenFileDialog()
            {
                DefaultExt = defaultExt,
                Filter = filters
            };

            var b = ofd.ShowDialog();

            if (b.HasValue && b.Value)
            {
                using (var s = ofd.OpenFile())
                {
                    var batchFile = await GeocodeFeed.ReadAsync(s, type);

                    var batchManager = new BatchGeocodeManager();
                    batchManager.StatusChanged += (msg) =>
                    {
                        BatchGeocodeStatus.Text = msg;
                    };

                    var results = await batchManager.Geocode(batchFile, BingMapsKey);

                    var serializer = new XmlSerializer(typeof(GeocodeFeed));

                    using (var writer = new StringWriter())
                    {
                        serializer.Serialize(writer, results.Succeeded);
                        SucceededBatchGeocodes.Text = writer.ToString();
                    }

                    using (var writer = new StringWriter())
                    {
                        serializer.Serialize(writer, results.Failed);
                        FailedBatchGeocodes.Text = writer.ToString();
                    }
                }
            }
        }

        private async void ParseBatchFile_Clicked(object sender, RoutedEventArgs e)
        {
            SucceededBatchGeocodes.Text = string.Empty;
            FailedBatchGeocodes.Text = string.Empty;

            var type = (BatchFileFormat)Enum.Parse(typeof(BatchFileFormat), ((string)(BatchFileFormatCbx.SelectedItem as ComboBoxItem).Content));

            string filters, defaultExt;
            FileExtensionUtilities.GetFileExtensions(type, out defaultExt, out filters);

            var ofd = new OpenFileDialog()
            {
                DefaultExt = defaultExt,
                Filter = filters
            };

            var b = ofd.ShowDialog();

            if (b.HasValue && b.Value)
            {
                using (var s = ofd.OpenFile())
                {
                    var batchFile = await GeocodeFeed.ReadAsync(s, type);

                    var serializer = new XmlSerializer(typeof(GeocodeFeed));

                    using(var writer = new StringWriter()){
                        serializer.Serialize(writer, batchFile);
                        SucceededBatchGeocodes.Text = writer.ToString();
                    }
                }
            }
        }

        private void UseDatasourceSettings_Clicked(object sender, RoutedEventArgs e)
        {
            var dsd = (sender as Button).Tag as DataSourceDetails;
            _datasourceApiDataSourceSettings.AccessId = dsd.AccessId;
            _datasourceApiDataSourceSettings.DataSourceName = dsd.DataSourceName;
            _datasourceApiDataSourceSettings.EntityTypeName = dsd.EntityTypeName;
        }

        #endregion
    }
}
