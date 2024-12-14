// ------------------------------------------------------------
// Author: Rindra Razafinjatovo
// Created on: 2018
// Last Modified: Dec 2024
// Project: Tahiry
// Description: A collection of Bible and Hymnals to streamline and enhance worship presentations for churches.
// ------------------------------------------------------------

﻿using Microsoft.Web.WebView2.WinForms;

namespace Fihirana_database.Forms
{
    partial class DisplayForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DisplayForm));
            this.webView = new Microsoft.Web.WebView2.WinForms.WebView2();
            this.panelLogo = new System.Windows.Forms.Panel();
            this.logoSidebar = new DevExpress.XtraEditors.PictureEdit();
            ((System.ComponentModel.ISupportInitialize)(this.webView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.logoSidebar.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // webView
            // 
            this.webView.AllowExternalDrop = false;
            this.webView.BackColor = System.Drawing.Color.Black;
            this.webView.CreationProperties = null;
            this.webView.DefaultBackgroundColor = System.Drawing.Color.Black;
            this.webView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webView.Location = new System.Drawing.Point(0, 0);
            this.webView.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.webView.Name = "webView";
            this.webView.Size = new System.Drawing.Size(1191, 859);
            this.webView.TabIndex = 0;
            this.webView.ZoomFactor = 1D;            
            this.webView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DisplayForm_KeyDown);
            // 
            // panelLogo
            // 
            this.panelLogo.BackColor = System.Drawing.Color.Black;
            this.panelLogo.Location = new System.Drawing.Point(572, 365);
            this.panelLogo.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.panelLogo.Name = "panelLogo";
            this.panelLogo.Size = new System.Drawing.Size(133, 65);
            this.panelLogo.TabIndex = 7;
            this.panelLogo.Visible = false;
            this.panelLogo.Paint += new System.Windows.Forms.PaintEventHandler(this.panelLogo_Paint);
            // 
            // logoSidebar
            // 
            this.logoSidebar.Dock = System.Windows.Forms.DockStyle.Right;
            this.logoSidebar.EditValue = global::Fihirana_database.Properties.Resources.adventist_symbol__white;
            this.logoSidebar.Location = new System.Drawing.Point(1191, 0);
            this.logoSidebar.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.logoSidebar.Name = "logoSidebar";
            this.logoSidebar.Properties.Appearance.BackColor = System.Drawing.Color.Black;
            this.logoSidebar.Properties.Appearance.Options.UseBackColor = true;
            this.logoSidebar.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.logoSidebar.Properties.Padding = new System.Windows.Forms.Padding(0, 0, 5, 0);
            this.logoSidebar.Properties.PictureAlignment = System.Drawing.ContentAlignment.BottomRight;
            this.logoSidebar.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.Auto;
            this.logoSidebar.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Zoom;
            this.logoSidebar.Size = new System.Drawing.Size(91, 859);
            this.logoSidebar.TabIndex = 9;
            // 
            // DisplayForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(1282, 859);
            this.Controls.Add(this.webView);
            this.Controls.Add(this.logoSidebar);
            this.Controls.Add(this.panelLogo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "DisplayForm";
            this.Text = "Diaporama";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.DisplayForm_FormClosed);
            this.Load += new System.EventHandler(this.DisplayForm_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DisplayForm_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.webView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.logoSidebar.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        //private Microsoft.Web.WebView2.WinForms.WebView2 webView;
        private WebView2 webView;
        private System.Windows.Forms.Panel panelLogo;
        private DevExpress.XtraEditors.PictureEdit logoSidebar;
    }
}