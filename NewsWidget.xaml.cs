/***************************************************************
* Title: News Widget
* Author: Jeffrey Cofer
* Created: July 4, 2025
* Description:
*   This widget fetches and displays the latest U.S. news headline
*   using the NewsAPI service. It shows the top article's title
*   and allows users to click the card to open the full article
*   in their browser.
***************************************************************/

using System;
using System.Diagnostics;
using System.Net.Http;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Newtonsoft.Json;

namespace ApiDashboard.Widgets
{
    /// <summary>
    /// A UserControl that displays the top U.S. news headline.
    /// Clicking the widget opens the article in the browser.
    /// </summary>
    public partial class NewsWidget : UserControl
    {
        private static readonly HttpClient client = new();

        // Stores the URL of the top article for clicking
        private string articleUrl = "https://news.google.com";

        /// <summary>
        /// Constructor: initializes the widget and loads headline data.
        /// </summary>
        public NewsWidget()
        {
            InitializeComponent();
            LoadNews(); // Begin async data fetch
        }

        /// <summary>
        /// Loads the top U.S. news headline using NewsAPI.
        /// Sets the headline text and stores the article URL.
        /// </summary>
        private async void LoadNews()
        {
            try
            {
                string apiKey = App.Configuration["ApiKeys:NewsApi"]?.Trim();
                string url = $"https://newsapi.org/v2/top-headlines?country=us&apiKey={apiKey}";

                // Required header for some APIs
                if (!client.DefaultRequestHeaders.Contains("User-Agent"))
                    client.DefaultRequestHeaders.Add("User-Agent", "WPF-NewsDashboard/1.0");

                var response = await client.GetAsync(url);
                if (!response.IsSuccessStatusCode)
                {
                    HeadlineText.Text = $"Error: {response.StatusCode} ({response.ReasonPhrase})";
                    return;
                }

                string json = await response.Content.ReadAsStringAsync();
                dynamic data = JsonConvert.DeserializeObject(json);

                // Check for valid data and at least one article
                if (data.status == "ok" && data.articles != null && data.articles.Count > 0)
                {
                    HeadlineText.Text = data.articles[0].title;
                    articleUrl = data.articles[0].url;
                }
                else
                {
                    HeadlineText.Text = "No articles found.";
                }
            }
            catch (Exception ex)
            {
                HeadlineText.Text = $"Exception: {ex.Message}";
            }
        }

        /// <summary>
        /// Handles mouse click on the widget to open the article in the browser.
        /// </summary>
        private void Card_Click(object sender, MouseButtonEventArgs e)
        {
            try
            {
                Process.Start(new ProcessStartInfo
                {
                    FileName = articleUrl,
                    UseShellExecute = true // Needed for opening URLs in default browser
                });
            }
            catch
            {
                MessageBox.Show("Unable to open article.");
            }
        }
    }
}
