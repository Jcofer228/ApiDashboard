/***************************************************************
* Title: ThemeInfo Assembly Attribute
* Author: Jeffrey Cofer
* Created: July 4, 2025
* Description:
*   Specifies where to find theme-specific and generic resource
*   dictionaries used by the WPF application. This supports
*   styling, theming, and consistent UI resources.
***************************************************************/

using System.Windows;

// 👇 Specifies resource dictionary locations for theming in WPF
[assembly: ThemeInfo(
    ResourceDictionaryLocation.None,            // No separate theme-specific dictionaries used
                                                // If a style isn't found in the local or app resources,
                                                // it will NOT look for a theme-based dictionary.

    ResourceDictionaryLocation.SourceAssembly   // Use the main assembly's generic resources (e.g. App.xaml)
                                                // for fallback if no local or theme resource is found
)]
