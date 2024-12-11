﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using System;
using DevExpress.Xpo;
using DevExpress.Xpo.Metadata;
using DevExpress.Data.Filtering;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using Fihirana_database.Classes;
using DevExpress.Xpo.DB;
using System.Data.SQLite;

namespace Fihirana_database.fihirana
{
    public static class ConnectionHelperBackup
    {

        static Type[] persistentTypes = new Type[] {
            typeof(Category),
            typeof(Hymnal)
        };
        public static Type[] GetPersistentTypes()
        {
            Type[] copy = new Type[persistentTypes.Length];
            Array.Copy(persistentTypes, copy, persistentTypes.Length);
            return copy;
        }

        public static string ConnectionString = "";
        //public static string ConnectionString = $@"XpoProvider=SQLite;Data Source={ SpecialFolderPath + "\\rindrasoftware\\tahiry\\test.db"}";
        public static void Connect(DevExpress.Xpo.DB.AutoCreateOption autoCreateOption, bool threadSafe = false)
        {

            if (string.IsNullOrEmpty(ClassSettings.Read()))
                ClassSettings.Write();
            ConnectionString = $"XpoProvider =SQLite;Data Source={ClassSettings.DataSource()}";

            if (threadSafe)
            {
                var provider = XpoDefault.GetConnectionProvider(ConnectionString, autoCreateOption);
                var dictionary = new DevExpress.Xpo.Metadata.ReflectionDictionary();
                dictionary.GetDataStoreSchema(persistentTypes);
                XpoDefault.DataLayer = new ThreadSafeDataLayer(dictionary, provider);
            }
            else
            {
                XpoDefault.DataLayer = XpoDefault.GetDataLayer(ConnectionString, autoCreateOption);
            }
            XpoDefault.Session = null;
        }
        public static DevExpress.Xpo.DB.IDataStore GetConnectionProvider(DevExpress.Xpo.DB.AutoCreateOption autoCreateOption)
        {
            if (string.IsNullOrEmpty(ClassSettings.Read())) ClassSettings.Write();
            ConnectionString = $"XpoProvider =SQLite;Data Source={ClassSettings.DataSource()}";

            return XpoDefault.GetConnectionProvider(ConnectionString, autoCreateOption);
        }
        public static DevExpress.Xpo.DB.IDataStore GetConnectionProvider(DevExpress.Xpo.DB.AutoCreateOption autoCreateOption, out IDisposable[] objectsToDisposeOnDisconnect)
        {
            if (string.IsNullOrEmpty(ClassSettings.Read())) ClassSettings.Write();
            ConnectionString = $"XpoProvider =SQLite;Data Source={ClassSettings.DataSource()}";

            return XpoDefault.GetConnectionProvider(ConnectionString, autoCreateOption, out objectsToDisposeOnDisconnect);
        }
        public static IDataLayer GetDataLayer(DevExpress.Xpo.DB.AutoCreateOption autoCreateOption)
        {
            if (string.IsNullOrEmpty(ClassSettings.Read())) ClassSettings.Write();
            ConnectionString = $"XpoProvider =SQLite;Data Source={ClassSettings.DataSource()}";

            return XpoDefault.GetDataLayer(ConnectionString, autoCreateOption);
        }
    }

}
