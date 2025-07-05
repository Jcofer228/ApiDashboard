/***************************************************************
* Title: Currency Exchange Widget
* Author: Jeffrey Cofer
* Created: July 4, 2025
* Description:
*   This widget fetches real-time currency exchange rates from
*   the Frankfurter API. It displays conversion from USD to EUR,
*   GBP, and JPY. Rates auto-refresh every 5 minutes.
***************************************************************/

using System;
using System.Net.Http;
using System.Windows.Controls;
using System.Windows.Threading;
using Newtonsoft.Json.Linq;

namespace ApiDashboard.Widgets
{
    /// <summary>
    /// A UserControl that displays real-time currency exchange rates
    /// from USD to EUR, GBP, and JPY using the Frankfurter API.
    /// </summary>
    public partial class CurrencyWidget : UserControl
    {
        private static readonly HttpClient client = new();         // Shared HTTP client instance
        private readonly DispatcherTimer timer = new();            // Timer to auto-refresh rates

        /// <summary>
        /// Constructor: initializes the widget and starts timer.
        /// </summary>
        public CurrencyWidget()
        {
            InitializeComponent();
            LoadExchangeRates(); // Load rates on startup

            // ⏱ Set up periodic refresh every 5 minutes
            timer.Interval = TimeSpan.FromMinutes(5);
            timer.Tick += (s, e) => LoadExchangeRates();
            timer.Start();
        }

        /// <summary>
        /// Asynchronously fetches USD exchange rates to EUR, GBP, and JPY.
        /// Displays values rounded to 2 decimal places.
        /// </summary>
        private async void LoadExchangeRates()
        {
            try
            {
                string url = "https://api.frankfurter.app/latest?from=USD&to=EUR,GBP,JPY";
                var response = await client.GetStringAsync(url);
                var data = JObject.Parse(response)["rates"];

                // 💱 Display formatted results
                UsdToEurText.Text = $"USD → EUR: {data["EUR"]:0.00}";
                UsdToGbpText.Text = $"USD → GBP: {data["GBP"]:0.00}";
                UsdToJpyText.Text = $"USD → JPY: {data["JPY"]:0.00}";
            }
            catch
            {
                // ⚠️ In case of error, show fallback message
                UsdToEurText.Text = "Exchange data unavailable";
                UsdToGbpText.Text = "";
                UsdToJpyText.Text = "";
            }
        }
    }
}
