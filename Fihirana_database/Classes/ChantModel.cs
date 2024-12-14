// ------------------------------------------------------------
// Author: Rindra Razafinjatovo
// Created on: 2018
// Last Modified: Dec 2024
// Project: Tahiry
// Description: A collection of Bible and Hymnals to streamline and enhance worship presentations for churches.
// ------------------------------------------------------------

﻿namespace Fihirana_database.Classes
{
    internal class ChantModel
    {
        public string Description { get; set; }
        public string Range { get; set; }

        public ChantModel(string description, string range)
        {
            Description = description;
            Range = range;
        }
    }
}
