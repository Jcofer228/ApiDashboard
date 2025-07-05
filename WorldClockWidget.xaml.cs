/***************************************************************
* Title: World Clock Widget
* Author: Jeffrey Cofer
* Created: July 4, 2025
* Description:
*   A custom WPF widget that displays the current time in
*   three major cities: New York, London, and Tokyo.
*   Uses system time zones and updates automatically every 30 seconds.
***************************************************************/

using System;
using System.Timers;
using System.Windows.Controls;
using System.Windows.Threading;

namespace ApiDashboard.Widgets
{
    /// <summary>
    /// A widget that displays real-time clocks for major time zones.
    /// </summary>
    public partial class WorldClockWidget : UserControl
    {
        private readonly DispatcherTimer timer = new(); // Timer for updating clock display

        /// <summary>
        /// Initializes the widget and starts the update timer.
        /// </summary>
        public WorldClockWidget()
        {
            InitializeComponent();
            UpdateClocks(); // Load initial values

            // Set update interval to 30 seconds
            timer.Interval = TimeSpan.FromSeconds(30);
            timer.Tick += (s, e) => UpdateClocks(); // Re-fetch time on each tick
            timer.Start();
        }

        /// <summary>
        /// Updates the three text fields with the current time.
        /// </summary>
        private void UpdateClocks()
        {
            TimeText1.Text = $"New York: {GetTime("Eastern Standard Time")}";
            TimeText2.Text = $"London: {GetTime("GMT Standard Time")}";
            TimeText3.Text = $"Tokyo: {GetTime("Tokyo Standard Time")}";
        }

        /// <summary>
        /// Gets the current time for a specified time zone ID.
        /// </summary>
        /// <param name="timezoneId">Windows time zone ID string</param>
        /// <returns>Formatted time or fallback if not found</returns>
        private string GetTime(string timezoneId)
        {
            try
            {
                // Convert UTC time to local time zone
                TimeZoneInfo tz = TimeZoneInfo.FindSystemTimeZoneById(timezoneId);
                DateTime local = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, tz);
                return local.ToString("hh:mm tt"); // 12-hour format
            }
            catch
            {
                return "[Unavailable]";
            }
        }
    }
}
