/***************************************************************
* Title: Application Startup and Configuration Loader
* Author: Jeffrey Cofer
* Created: July 4, 2025
* Description:
*   This file initializes the WPF application and sets up
*   global configuration loading from appsettings.json using
*   Microsoft.Extensions.Configuration.
***************************************************************/

using Microsoft.Extensions.Configuration;
using System.IO;
using System.Windows;

namespace ApiDashboard
{
    /// <summary>
    /// Interaction logic for the WPF App startup class.
    /// Loads configuration settings from appsettings.json.
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// Globally accessible configuration instance.
        /// Used throughout the app to access API keys and settings.
        /// </summary>
        public static IConfiguration Configuration { get; private set; }

        /// <summary>
        /// Overrides the application startup sequence.
        /// Loads configuration from JSON file before showing the UI.
        /// </summary>
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // 🔧 Build configuration from appsettings.json
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())        // Sets root path to app folder
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true); // Load config file

            Configuration = builder.Build(); // Make config available globally
        }
    }
}
