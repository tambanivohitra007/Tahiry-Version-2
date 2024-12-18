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

    public partial class books : XPLiteObject
    {
        long fid_book;
        [Key]
        public long id_book
        {
            get { return fid_book; }
            set { SetPropertyValue<long>(nameof(id_book), ref fid_book, value); }
        }
        int fbook_number;
        [Size(SizeAttribute.Unlimited)]
        public int book_number
        {
            get { return fbook_number; }
            set { SetPropertyValue<int>(nameof(book_number), ref fbook_number, value); }
        }
        int fchapter_count;
        [Size(SizeAttribute.Unlimited)]
        public int chapter_count
        {
            get { return fchapter_count; }
            set { SetPropertyValue<int>(nameof(chapter_count), ref fchapter_count, value); }
        }
        int fbook_category;
        public int book_category
        {
            get { return fbook_category; }
            set { SetPropertyValue<int>(nameof(book_category), ref fbook_category, value); }
        }
        string fshort_name;
        [Size(SizeAttribute.Unlimited)]
        public string short_name
        {
            get { return fshort_name; }
            set { SetPropertyValue<string>(nameof(short_name), ref fshort_name, value); }
        }
        string flong_name;
        [Size(SizeAttribute.Unlimited)]
        public string long_name
        {
            get { return flong_name; }
            set { SetPropertyValue<string>(nameof(long_name), ref flong_name, value); }
        }

        string flong_name_french;
        [Size(SizeAttribute.Unlimited)]
        public string long_name_french
        {
            get { return flong_name_french; }
            set { SetPropertyValue<string>(nameof(long_name_french), ref flong_name_french, value); }
        }

        string flong_name_english;
        [Size(SizeAttribute.Unlimited)]
        public string long_name_english
        {
            get { return flong_name_english; }
            set { SetPropertyValue<string>(nameof(long_name_english), ref flong_name_english, value); }
        }
    }

}
