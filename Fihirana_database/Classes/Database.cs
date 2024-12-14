// ------------------------------------------------------------
// Author: Rindra Razafinjatovo
// Created on: 2018
// Last Modified: Dec 2024
// Project: Tahiry
// Description: A collection of Bible and Hymnals to streamline and enhance worship presentations for churches.
// ------------------------------------------------------------

﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fihirana_database.Classes
{
    public class Database
    {
        /// <summary>
        /// Ensures that the database file exists in the application's data directory.
        /// If the directory does not exist, it creates it.
        /// If the database file does not exist, it copies it from the application's base directory.
        /// </summary>
        public static void EnsureDatabaseExists()
        {
            // Get the path to the Application Data folder for the current user
            string appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

            // Define the path to the application's specific folder within the Application Data directory
            string appFolder = Path.Combine(appDataPath, "RindraSoftware\\Tahiry");

            // Define the full path to the database file within the application's folder
            string databasePath = Path.Combine(appFolder, "tahiry.db");

            // Check if the application's folder exists, if not, create it
            if (!Directory.Exists(appFolder))
            {
                Directory.CreateDirectory(appFolder);
            }

            // Check if the database file exists in the application's folder, if not, copy it from the source path
            if (!File.Exists(databasePath))
            {
                // Define the source path of the database file within the application's base directory
                string sourcePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Database\\tahiry.db");

                // Copy the database file from the source path to the application's folder
                File.Copy(sourcePath, databasePath);
            }
        }
    }
}
