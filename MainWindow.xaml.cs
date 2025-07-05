/***************************************************************
* Title: API Dashboard Main Window
* Author: Jeffrey Cofer
* Created: July 4, 2025
* Description:
*   This is the entry point for the WPF application UI.
*   It initializes and loads a set of custom user widgets
*   including weather, crypto, news, location, world clocks,
*   and currency exchange rates.
***************************************************************/

using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using ApiDashboard.Widgets;

namespace ApiDashboard
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml.
    /// Initializes the widget dashboard and adds all user controls.
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            // 🔧 Add user widgets to the main panel
            // Each widget is a custom UserControl representing a live API feed
            WidgetPanel.Children.Add(new WeatherWidget());      // 🌦 Weather based on user's IP
            WidgetPanel.Children.Add(new CryptoWidget());       // 💰 Live crypto prices (BTC/ETH)
            WidgetPanel.Children.Add(new NewsWidget());         // 📰 Latest news headlines
            WidgetPanel.Children.Add(new LocationWidget());     // 📍 Location & IP info
            WidgetPanel.Children.Add(new WorldClockWidget());   // 🕒 Global time zones
            WidgetPanel.Children.Add(new CurrencyWidget());     // 💱 Currency exchange rates
        }
    }
}
