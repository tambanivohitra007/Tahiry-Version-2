// ------------------------------------------------------------
// Author: Rindra Razafinjatovo
// Created on: 2018
// Last Modified: Dec 2024
// Project: Tahiry
// Description: A collection of Bible and Hymnals to streamline and enhance worship presentations for churches.
// ------------------------------------------------------------

﻿namespace Fihirana_database
{
    public partial class AboutForm : DevExpress.XtraEditors.XtraForm
    {
        public AboutForm()
        {
            InitializeComponent();

            infoLbl.Text = "Ce logiciel n'est plus pris en charge par le propriétaire.<br>" +
               "Version 2.0.0.6 <size=11><br>" +
               "Pour plus d'informations, veuillez contacter l'adresse e-mail suivante :<br>" +
               "<br><b>E-mail :</b> rindra.it@gmail.com<br>";

            infoLbl.AllowHtmlString = true;
            infoLbl.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            infoLbl.Appearance.Options.UseTextOptions = true;
            infoLbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.Vertical;
        }
    }
}