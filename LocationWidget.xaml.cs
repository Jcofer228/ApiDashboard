/***************************************************************
* Title: Location Widget
* Author: Jeffrey Cofer
* Created: July 4, 2025
* Description:
*   Displays basic location data based on the user's IP address.
*   Uses the ip-api.com service to fetch city, country, timezone,
*   and IP address. The widget opens Google Maps on click.
***************************************************************/

using System;
using System.Diagnostics;
using System.Net.Http;
using System.Windows.Controls;
using System.Windows.Input;
using Newtonsoft.Json;

namespace ApiDashboard.Widgets
{
    /// <summary>
    /// A UserControl that shows the user's approximate location
    /// including city, country, timezone, and IP address.
    /// </summary>
    public partial class LocationWidget : UserControl
    {
        private static readonly HttpClient client = new();

        /// <summary>
        /// Constructor: initializes the widget and loads location info.
        /// </summary>
        public LocationWidget()
        {
            InitializeComponent();
            LoadLocationInfo();
        }

        /// <summary>
        /// Asynchronously fetches the user's IP-based location
        /// and displays it in the UI.
        /// </summary>
        private async void LoadLocationInfo()
        {
            try
            {
                // 🌍 Get JSON data from IP geolocation API
                var response = await client.GetStringAsync("http://ip-api.com/json/");
                dynamic data = JsonConvert.DeserializeObject(response);

                // 🖼️ Display each field
                CityText.Text = $"City: {data.city}";
                CountryText.Text = $"Country: {data.country}";
                TimezoneText.Text = $"Timezone: {data.timezone}";
                IpText.Text = $"IP: {data.query}";
            }
            catch (Exception ex)
            {
                CityText.Text = "Location unavailable";
                CountryText.Text = ex.Message;
            }
        }

        /// <summary>
        /// Opens Google Maps in the default browser when the card is clicked.
        /// </summary>
        private void Card_Click(object sender, MouseButtonEventArgs e)
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = "https://www.google.com/maps",
                UseShellExecute = true
            });
        }
    }
}
