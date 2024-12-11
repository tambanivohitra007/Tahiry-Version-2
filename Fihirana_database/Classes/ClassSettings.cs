/*
-- Rindra Razafinjatovo
-- Developer and Teacher
-- 2018
*/

using DevExpress.LookAndFeel;
using Microsoft.Win32;
using System;

namespace Fihirana_database.Classes
{
    /// <summary>
    /// Manages application settings stored in the Windows Registry for the Tahiry application.
    /// </summary>
    public static class ClassSettings
    {
        // Path to the special folder in Application Data
        private static readonly string SpecialFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

        /// <summary>
        /// Reads the DataSource value from the registry.
        /// </summary>
        /// <returns>The DataSource value if found, otherwise null.</returns>
        public static string Read()
        {
            try
            {
                using RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Software\RindraSoftware\Tahiry");
                if (key != null)
                {
                    object dataSource = key.GetValue("DataSource");
                    return dataSource?.ToString();
                }
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading DataSource: {ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// Retrieves or initializes a specific configuration value in the registry.
        /// </summary>
        /// <param name="name">The name of the configuration key.</param>
        /// <returns>The value associated with the key.</returns>
        public static string ConfigName(string name)
        {
            try
            {
                using RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Software\RindraSoftware\Tahiry");
                if (key != null && key.GetValue(name) != null)
                {
                    return key.GetValue(name)?.ToString();
                }
                else
                {
                    using RegistryKey registryKey = Registry.CurrentUser.CreateSubKey(@"Software\RindraSoftware\Tahiry");
                    registryKey?.SetValue(name, "The Bezier");
                    return registryKey?.GetValue(name)?.ToString();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error handling config name: {ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// Writes or updates a configuration key-value pair in the registry.
        /// </summary>
        /// <param name="config">The configuration key.</param>
        /// <param name="value">The value to set.</param>
        public static void WriteConfig(string config, string value)
        {
            try
            {
                using RegistryKey registryKey = Registry.CurrentUser.CreateSubKey(@"Software\RindraSoftware\Tahiry");
                registryKey?.SetValue(config, value);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error writing config: {ex.Message}");
            }
        }

        /// <summary>
        /// Initializes default application settings in the registry.
        /// </summary>
        public static void Write()
        {
            try
            {
                using RegistryKey registryKey = Registry.CurrentUser.CreateSubKey(@"Software\RindraSoftware\Tahiry");
                registryKey?.SetValue("Provider", "SQLite");
                registryKey?.SetValue("DataSource", $@"{SpecialFolderPath}\Rindrasoftware\Tahiry\tahiry.db");
                registryKey?.SetValue("Password", "");
                registryKey?.SetValue("SkinName", "The Bezier");
                registryKey?.SetValue("Palette", "Gloom Gloom");
                registryKey?.SetValue("DirectX", true);
                registryKey?.SetValue("DPIAwarenessMode", "");
                registryKey?.SetValue("HidePanel", "0");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error writing default settings: {ex.Message}");
            }
        }

        /// <summary>
        /// Retrieves the DataSource value, initializing defaults if not present.
        /// </summary>
        /// <returns>The DataSource value if found, otherwise null.</returns>
        public static string DataSource()
        {
            try
            {
                using RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Software\RindraSoftware\Tahiry");
                if (key != null)
                {
                    return key.GetValue("DataSource")?.ToString();
                }
                Write();
                using RegistryKey newKey = Registry.CurrentUser.OpenSubKey(@"Software\RindraSoftware\Tahiry");
                return newKey?.GetValue("DataSource")?.ToString();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving DataSource: {ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// Toggles the application mode by switching palettes.
        /// </summary>
        /// <param name="mode">Indicates whether to update the mode explicitly.</param>
        /// <returns>The new SkinSvgPalette.</returns>
        public static SkinSvgPalette SwitchMode(bool mode = false)
        {
            const string RegistryPath = @"Software\RindraSoftware\Tahiry";
            const string SkinNameKey = "SkinName";
            const string PaletteKey = "Palette";
            const string BezierSkin = "The Bezier";
            const string GloomGloomPalette = "Gloom Gloom";
            const string FireballPalette = "Fireball";

            try
            {
                using RegistryKey registryKey = Registry.CurrentUser.CreateSubKey(RegistryPath);
                string currentPalette = registryKey?.GetValue(PaletteKey)?.ToString() ?? GloomGloomPalette;
                string newPalette = string.Equals(currentPalette, GloomGloomPalette, StringComparison.OrdinalIgnoreCase) ? FireballPalette : GloomGloomPalette;

                if (mode)
                {
                    registryKey?.SetValue(SkinNameKey, BezierSkin);
                    registryKey?.SetValue(PaletteKey, newPalette);
                }

                return newPalette == FireballPalette ? SkinSvgPalette.Bezier.MercuryIce : SkinSvgPalette.Bezier.GloomGloom;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error switching mode: {ex.Message}");
                return SkinSvgPalette.Bezier.GloomGloom;
            }
        }
    }
}