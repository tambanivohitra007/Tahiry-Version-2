// ------------------------------------------------------------
// Author: Rindra Razafinjatovo
// Created on: 2018
// Last Modified: Dec 2024
// Project: Tahiry
// Description: A collection of Bible and Hymnals to streamline and enhance worship presentations for churches.
// ------------------------------------------------------------

﻿using System.Collections.Generic;
using System.Linq;

namespace Fihirana_database.Classes
{
    public class Effects
    {
        public string Category { get; set; }
        public string[] Liste { get; set; }

        public static List<Effects> AllEffects()
        {
            var effectsData = new[]
            {
                new { Category = "boucing_entrances", Liste = new[] { "bounceIn", "bounceInDown", "bounceInLeft", "bounceInRight", "bounceInUp" } },
                new { Category = "fading_entrances", Liste = new[] { "fadeIn", "fadeInDown", "fadeInDownBig", "fadeInLeft", "fadeInLeftBig", "fadeInRight", "fadeInRightBig", "fadeInUp", "fadeInUpBig", "fadeInTopLeft", "fadeInTopRight", "fadeInBottomLeft", "fadeInBottomRight" } },
                new { Category = "back_entrances", Liste = new[] { "backInDown", "backInLeft", "backInRight", "backInUp" } },
                new { Category = "rotating_entrances", Liste = new[] { "rotateIn", "rotateInDownLeft", "rotateInDownRight", "rotateInUpLeft", "rotateInUpRight" } },
                new { Category = "lightSpeed", Liste = new[] { "lightSpeedInRight", "lightSpeedInLeft" } },
                new { Category = "specials", Liste = new[] { "bounce", "flash", "pusle", "rubberBand", "shakeX", "shakeY", "headShake", "swing", "tada", "wobble", "jello", "heartBeat", "jackInTheBox", "rollIn" } },
                new { Category = "zooming_entrances", Liste = new[] { "zoomIn", "zoomInDown", "zoomInLeft", "zoomInRight", "zoomInUp" } },
                new { Category = "sliding_entrances", Liste = new[] { "slideInDown", "slideInLeft", "slideInRight", "slideInUp" } }
            };

            return effectsData.Select(e => new Effects { Category = e.Category, Liste = e.Liste }).ToList();
        }

    }
}
