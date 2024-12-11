using System.Drawing;

namespace Fihirana_database.Classes
{
    public class ClassFihirana
    {
        public static int ExIndex { get; set; }
        public static string Police { get; set; } = "Century Gothic";
        public static string Title { get; set; }
        public static StringAlignment Alignment { get; set; } = StringAlignment.Center;
        public static bool IsBold { get; set; } = false;
        public static bool IsItalic { get; set; } = false;

        public static DevExpress.Xpo.Session SessionHymnal { get; set; }
        public static bool isHide { get; set; }
        public static bool isFontReady { get; set; } = false;
    }
}