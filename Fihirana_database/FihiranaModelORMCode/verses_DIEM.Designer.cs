// ------------------------------------------------------------
// Author: Rindra Razafinjatovo
// Created on: 2018
// Last Modified: Dec 2024
// Project: Tahiry
// Description: A collection of Bible and Hymnals to streamline and enhance worship presentations for churches.
// ------------------------------------------------------------

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
namespace Fihirana_database.fihirana
{

    public partial class verses_DIEM : XPLiteObject
    {
        long fid_diem;
        [Key]
        public long id_diem
        {
            get { return fid_diem; }
            set { SetPropertyValue<long>(nameof(fid_diem), ref fid_diem, value); }
        }
        long fbook_number;
        public long book_number
        {
            get { return fbook_number; }
            set { SetPropertyValue<long>(nameof(book_number), ref fbook_number, value); }
        }
        long fchapter;
        public long chapter
        {
            get { return fchapter; }
            set { SetPropertyValue<long>(nameof(chapter), ref fchapter, value); }
        }
        long fverse;
        public long verse
        {
            get { return fverse; }
            set { SetPropertyValue<long>(nameof(verse), ref fverse, value); }
        }
        string ftext;
        [Size(SizeAttribute.Unlimited)]
        public string text
        {
            get { return ftext; }
            set { SetPropertyValue<string>(nameof(text), ref ftext, value); }
        }
    }

}
