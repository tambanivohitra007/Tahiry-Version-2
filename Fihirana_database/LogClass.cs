using Fihirana_database.fihirana;
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