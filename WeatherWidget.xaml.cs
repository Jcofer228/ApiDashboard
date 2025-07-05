/***************************************************************
* Title: Weather Widget
* Author: Jeffrey Cofer
* Created: July 4, 2025
* Description:
*   This widget retrieves the user's approximate location via IP,
*   determines the appropriate unit of measure (°C or °F),
*   and fetches current weather conditions from OpenWeatherMap.
*   It also opens a detailed Google weather search on click.
***************************************************************/

using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Newtonsoft.Json.Linq;

namespace ApiDashboard.Widgets
{
    /// <summary>
    /// A UserControl that displays real-time weather for the user's current location.
    /// </summary>
    public partial class WeatherWidget : UserControl
    {
        private static readonly HttpClient client = new();
        private string cityName = "your city";      // fallback default
        private string countryCode = "US";          // fallback default
        private string unitSymbol = "°C";           // °C or °F depending on region

        /// <summary>
        /// Constructor that initializes the widget and loads weather data.
        /// </summary>
        public WeatherWidget()
        {
            InitializeComponent();
            LoadWeather(); // Begin async fetch
        }

        /// <summary>
        /// Retrieves location and weather data, then displays it.
        /// Automatically detects user region to show °C or °F.
        /// </summary>
        private async void LoadWeather()
        {
            try
            {
                // 🌍 Step 1: Use IP geolocation to get lat/lon
                var locationResp = await client.GetStringAsync("http://ip-api.com/json/");
                dynamic location = JObject.Parse(locationResp);
                string lat = location.lat;
                string lon = location.lon;
                cityName = location.city;
                countryCode = location.countryCode;

                // 🌡️ Step 2: Decide unit system (°C or °F)
                string units = "metric";
                unitSymbol = "°C";

                if (countryCode == "US" || countryCode == "LR" || countryCode == "MM")
                {
                    units = "imperial";
                    unitSymbol = "°F";
                }

                // ☁️ Step 3: Call OpenWeatherMap API
                string apiKey = App.Configuration["ApiKeys:OpenWeatherMap"];
                string weatherUrl = $"https://api.openweathermap.org/data/2.5/weather?lat={lat}&lon={lon}&appid={apiKey}&units={units}";

                var weatherResp = await client.GetStringAsync(weatherUrl);
                dynamic weather = JObject.Parse(weatherResp);

                double temp = weather.main.temp;
                string condition = weather.weather[0].main;

                // 🖼️ Display results
                LocationText.Text = $"📍 {cityName}";
                TempText.Text = $"Temperature: {temp}{unitSymbol}";
                ConditionText.Text = $"Condition: {condition}";
            }
            catch (Exception ex)
            {
                TempText.Text = "Weather unavailable";
                ConditionText.Text = ex.Message;
            }
        }

        /// <summary>
        /// Opens Google search for full weather info on click.
        /// </summary>
        private void Card_Click(object sender, MouseButtonEventArgs e)
        {
            try
            {
                string url = $"https://www.google.com/search?q=weather+{Uri.EscapeDataString(cityName)}";
                Process.Start(new ProcessStartInfo
                {
                    FileName = url,
                    UseShellExecute = true
                });
            }
            catch
            {
                MessageBox.Show("Unable to open weather info.");
            }
        }
    }
}
