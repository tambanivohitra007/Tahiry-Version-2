using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Fihirana_database.Classes
{
    public class AutoUpdater
    {
        private static string currentVersion = "2.0.0.6";
        private static string updateCheckUrl = "https://fihirana.rindra.org/version.json";

        public static async void CheckForUpdates()
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    // Fetch the version information from the server
                    string json = await client.GetStringAsync(updateCheckUrl);
                    JObject updateInfo = JObject.Parse(json);

                    string latestVersion = updateInfo["version"].ToString();
                    string downloadUrl = updateInfo["url"].ToString();
                    string releaseNotes = updateInfo["releaseNotes"].ToString();

                    if (string.Compare(currentVersion, latestVersion) < 0)
                    {
                        Console.WriteLine($"New version available: {latestVersion}");
                        Console.WriteLine(releaseNotes);

                        DownloadAndInstallUpdate(downloadUrl);
                    }
                    else
                    {
                        Console.WriteLine("You already have the latest version.");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error checking for updates: {ex.Message}");
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
