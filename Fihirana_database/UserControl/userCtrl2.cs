// ------------------------------------------------------------
// Author: Rindra Razafinjatovo
// Created on: 2018
// Last Modified: Dec 2024
// Project: Tahiry
// Description: A collection of Bible and Hymnals to streamline and enhance worship presentations for churches.
// ------------------------------------------------------------

﻿using System.ComponentModel;

namespace Fihirana_database.UserControl
{
    public partial class userCtrl2 : DevExpress.XtraEditors.XtraUserControl
    {
        public userCtrl2()
        {
            InitializeComponent();
        }

        [Category("Chant")]
        public string Number
        {
            get
            {
                return number.Text;
            }
            set
            {
                number.Text = value;
            }
        }
        [Category("Chant")]
        public string Title
        {
            get
            {
                return title.Text;
            }
            set
            {
                title.Text = value;
            }
        }
        [Category("Chant")]
        public string Key
        {
            get
            {
                return key.Text;
            }
            set
            {
                key.Text = value;
            }
        }

        public void Clear()
        {
            title.Text = "";
            number.Text = "";
            key.Text = "";
        }
    }
}
