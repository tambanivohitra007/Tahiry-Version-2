using DevExpress.Skins;
using DevExpress.XtraEditors;
using Fihirana_database.Classes;
using Fihirana_database.fihirana;
using Microsoft.Win32;
using System;
using System.Reflection;
using System.Windows.Forms;

namespace Fihirana_database
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            try
            {
                WindowsFormsSettings.ForceDirectXPaint();
                RegistryKey key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\RindraSoftware\Tahiry");

                if (key == null)
                    Classes.ClassSettings.Write();

                Database.EnsureDatabaseExists();

                ConnectionHelper.Connect(DevExpress.Xpo.DB.AutoCreateOption.DatabaseAndSchema);

               

                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                SkinManager.EnableFormSkins();
                Application.Run(new MainForm());
            }
            catch (TargetInvocationException e)
            {
                _ = XtraMessageBox.Show($"Erreur cible: {e.Message} ", "ERREUR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (NullReferenceException e)
            {
                _ = XtraMessageBox.Show($"Erreur de référence nulle: {e.Message}", "ERREUR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Application.Exit();
            }
        }
    }
}