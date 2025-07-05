/***************************************************************
* Title: Crypto Prices Widget
* Author: Jeffrey Cofer
* Created: July 4, 2025
* Description:
*   This widget fetches real-time prices for Bitcoin and Ethereum
*   in USD from the CoinGecko API. Prices are auto-refreshed
*   every 60 seconds. Clicking the widget opens CoinGecko.com.
***************************************************************/

using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading;
using System.Windows.Controls;
using System.Windows.Input;
using Newtonsoft.Json;

namespace ApiDashboard.Widgets
{
    /// <summary>
    /// A UserControl that shows current prices for Bitcoin and Ethereum.
    /// Data is retrieved from CoinGecko and refreshed every 60 seconds.
    /// </summary>
    public partial class CryptoWidget : UserControl
    {
        private static readonly HttpClient client = new();
        private Timer _timer; // Timer for automatic refresh

        /// <summary>
        /// Constructor: initializes the widget and starts the refresh timer.
        /// </summary>
        public CryptoWidget()
        {
            InitializeComponent();
            LoadCryptoPrices();

            // 🔁 Auto-refresh crypto data every 60 seconds
            _timer = new Timer(_ => Dispatcher.Invoke(LoadCryptoPrices), null, 60000, 60000);
        }

        /// <summary>
        /// Loads the latest BTC and ETH prices from CoinGecko's public API.
        /// </summary>
        private async void LoadCryptoPrices()
        {
            try
            {
                string url = "https://api.coingecko.com/api/v3/simple/price?ids=bitcoin,ethereum&vs_currencies=usd";
                var response = await client.GetStringAsync(url);
                dynamic data = JsonConvert.DeserializeObject(response);

                // 💰 Display live prices
                BitcoinText.Text = $"Bitcoin (BTC): ${data.bitcoin.usd}";
                EthereumText.Text = $"Ethereum (ETH): ${data.ethereum.usd}";
            }
            catch
            {
                // ⚠️ Fallback on error
                BitcoinText.Text = "Failed to load crypto data.";
                EthereumText.Text = "";
            }
        }

        /// <summary>
        /// Opens CoinGecko in the default web browser when the widget is clicked.
        /// </summary>
        private void Card_Click(object sender, MouseButtonEventArgs e)
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = "https://www.coingecko.com",
                UseShellExecute = true
            });
        }
    }
}
