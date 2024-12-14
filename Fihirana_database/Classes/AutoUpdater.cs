// ------------------------------------------------------------
// Author: Rindra Razafinjatovo
// Created on: 2018
// Last Modified: Dec 2024
// Project: Tahiry
// Description: A collection of Bible and Hymnals to streamline and enhance worship presentations for churches.
// ------------------------------------------------------------

﻿using DevExpress.XtraEditors;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Fihirana_database.Classes
{
    public class AutoUpdater
    {
        private static string currentVersion = "2.0.0.6";
        private static string updateCheckUrl = "https://fihirana.rindra.org/version.json";

        public static async Task CheckForUpdatesAsync()
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    // Fetch the version information from the server
                    string json = await client.GetStringAsync(updateCheckUrl);
                    JObject updateInfo = JObject.Parse(json);

                    string latestVersion = updateInfo["version"]?.ToString();
                    string downloadUrl = updateInfo["url"]?.ToString();
                    string releaseNotes = updateInfo["releaseNotes"]?.ToString();

                    if (string.IsNullOrWhiteSpace(latestVersion) || string.IsNullOrWhiteSpace(downloadUrl))
                    {
                        XtraMessageBox.Show("Failed to retrieve update information.", "Update Check", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    if (string.Compare(currentVersion, latestVersion, StringComparison.Ordinal) < 0)
                    {
                        DialogResult result = XtraMessageBox.Show(
                            $"A new version ({latestVersion}) is available.\n\nRelease Notes:\n{releaseNotes}\n\nDo you want to download and install the update?",
                            "Update Available",
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Information
                        );

                        if (result == DialogResult.Yes)
                        {
                            DownloadAndInstallUpdate(downloadUrl);
                        }
                    }
                    else
                    {
                        XtraMessageBox.Show("You already have the latest version.", "Update Check", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (HttpRequestException ex)
            {
                XtraMessageBox.Show($"Network error while checking for updates: {ex.Message}", "Update Check", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (JsonException ex)
            {
                XtraMessageBox.Show($"Error parsing update information: {ex.Message}", "Update Check", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"Unexpected error: {ex.Message}", "Update Check", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private static void DownloadAndInstallUpdate(string downloadUrl)
        {
            string tempInstallerPath = Path.Combine(Path.GetTempPath(), "setup.exe");

            using (var client = new System.Net.WebClient())
            {
                Console.WriteLine("Downloading update...");
                client.DownloadFile(downloadUrl, tempInstallerPath);
            }

            Console.WriteLine("Installing update...");
            Process.Start(tempInstallerPath, "/SILENT"); // Run the installer silently
            Environment.Exit(0); // Exit the current application
        }
    }
}
