using Microsoft.Win32;

namespace Fihirana_database.Classes
{
    public static class ClassConnection
    {
        private static string DataSource;

        public static void connect()
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\RindraSoftware\Tahiry");
            if (key != null)
            {
                try
                {
                    DataSource = key.GetValue("DataSource").ToString();
                }
                catch
                {
                }
            }
        }
    }
}