using System.Collections.Generic;

namespace Fihirana_database.Classes
{
    public class Effects
    {
        public string Category { get; set; }
        public string[] Liste { get; set; }

        public static List<Effects> AllEffects()
        {
            var list = new List<Effects>
            {
                new Effects()
                {
                    Category = "boucing_entrances",
                    Liste = new string[] { "bounceIn", "bounceInDown", "bounceInLeft", "bounceInRight", "bounceInUp" }
                },
                new Effects()
                {
                    Category = "fading_entrances",
                    Liste = new string[] { "fadeIn", "fadeInDown", "fadeInDownBig", "fadeInLeft", "fadeInLeftBig", "fadeInRight", "fadeInRightBig", "fadeInUp", "fadeInUpBig", "fadeInTopLeft", "fadeInTopRight", "fadeInBottomLeft", "fadeInBottomRight" }
                },
                new Effects()
                {
                    Category = "back_entrances",
                    Liste = new string[] { "backInDown", "backInLeft", "backInRight", "backInUp" }
                },
                new Effects()
                {
                    Category = "rotating_entrances",
                    Liste = new string[] { "rotateIn", "rotateInDownLeft", "rotateInDownRight", "rotateInUpLeft",
                     "rotateInUpRight"}
                },
                new Effects()
                {
                    Category = "lightSpeed",
                    Liste = new string[] { "lightSpeedInRight", "lightSpeedInLeft" }
                },
                new Effects()
                {
                    Category = "specials",
                    Liste = new string[] {"bounce", "flash", "pusle", "rubberBand", "shakeX", "shakeY",
                       "headShake", "swing", "tada", "wobble", "jello", "heartBeat", "jackInTheBox", "rollIn"}
                },
                new Effects()
                {
                    Category = "zooming_entrances",
                    Liste = new string[] { "zoomIn", "zoomInDown", "zoomInLeft", "zoomInRight", "zoomInUp" }
                },
                new Effects()
                {
                    Category = "sliding_entrances",
                    Liste = new string[] { "slideInDown", "slideInLeft", "slideInRight", "slideInUp" }
                }
            };

            return list;
        }
    }
}
