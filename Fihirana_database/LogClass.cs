// ------------------------------------------------------------
// Author: Rindra Razafinjatovo
// Created on: 2018
// Last Modified: Dec 2024
// Project: Tahiry
// Description: A collection of Bible and Hymnals to streamline and enhance worship presentations for churches.
// ------------------------------------------------------------

﻿using Fihirana_database.fihirana;
using System.Collections.Generic;

namespace Fihirana_database
{
    public class LogClass
    {
        public static Hymnal hymn { get; set; }
        public static bool isTextVerse { get; set; }
        public static bool isBlackScreen { get; set; } = false;
        public static bool showLogo { get; set; } = false;
        public static bool showlogoSideBar { get; set; } = false;
        public static List<string> CrossList { get; set; } = [];
    }
}