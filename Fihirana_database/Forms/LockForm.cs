// ------------------------------------------------------------
// Author: Rindra Razafinjatovo
// Created on: 2018
// Last Modified: Dec 2024
// Project: Tahiry
// Description: A collection of Bible and Hymnals to streamline and enhance worship presentations for churches.
// ------------------------------------------------------------

﻿namespace Fihirana_database.Forms
{
    public partial class LockForm : DevExpress.XtraEditors.XtraForm
    {
        public LockForm()
        {
            InitializeComponent();
        }

        private void PassText_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (string.IsNullOrEmpty(PassText.Text)) return;
        }
    }
}