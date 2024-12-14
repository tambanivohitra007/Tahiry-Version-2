// ------------------------------------------------------------
// Author: Rindra Razafinjatovo
// Created on: 2018
// Last Modified: Dec 2024
// Project: Tahiry
// Description: A collection of Bible and Hymnals to streamline and enhance worship presentations for churches.
// ------------------------------------------------------------

﻿namespace Fihirana_database.UserControl
{
    partial class userCtrl2
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.title = new DevExpress.XtraEditors.LabelControl();
            this.number = new DevExpress.XtraEditors.LabelControl();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.sidePanel4 = new DevExpress.XtraEditors.SidePanel();
            this.key = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            this.sidePanel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // title
            // 
            this.title.Appearance.Font = new System.Drawing.Font("Tahoma", 20F);
            this.title.Appearance.Options.UseFont = true;
            this.title.Location = new System.Drawing.Point(26, 7);
            this.title.Margin = new System.Windows.Forms.Padding(5);
            this.title.Name = "title";
            this.title.Size = new System.Drawing.Size(75, 48);
            this.title.TabIndex = 8;
            this.title.Text = "Title";
            // 
            // number
            // 
            this.number.Appearance.Font = new System.Drawing.Font("Tahoma", 14F);
            this.number.Appearance.Options.UseFont = true;
            this.number.Location = new System.Drawing.Point(26, 80);
            this.number.Margin = new System.Windows.Forms.Padding(5);
            this.number.Name = "number";
            this.number.Size = new System.Drawing.Size(99, 34);
            this.number.TabIndex = 7;
            this.number.Text = "Number";
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.sidePanel4);
            this.panelControl1.Controls.Add(this.title);
            this.panelControl1.Controls.Add(this.number);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(856, 131);
            this.panelControl1.TabIndex = 9;
            // 
            // sidePanel4
            // 
            this.sidePanel4.Appearance.BackColor = System.Drawing.Color.DarkOrange;
            this.sidePanel4.Appearance.Options.UseBackColor = true;
            this.sidePanel4.Controls.Add(this.key);
            this.sidePanel4.Dock = System.Windows.Forms.DockStyle.Right;
            this.sidePanel4.Location = new System.Drawing.Point(682, 2);
            this.sidePanel4.Margin = new System.Windows.Forms.Padding(14, 13, 14, 13);
            this.sidePanel4.Name = "sidePanel4";
            this.sidePanel4.Size = new System.Drawing.Size(172, 127);
            this.sidePanel4.TabIndex = 13;
            this.sidePanel4.Text = "sidePanel4";
            // 
            // key
            // 
            this.key.BackColor = System.Drawing.Color.Transparent;
            this.key.Dock = System.Windows.Forms.DockStyle.Fill;
            this.key.Font = new System.Drawing.Font("Tahoma", 18F);
            this.key.ForeColor = System.Drawing.Color.White;
            this.key.Location = new System.Drawing.Point(1, 0);
            this.key.Margin = new System.Windows.Forms.Padding(14, 0, 14, 0);
            this.key.Name = "key";
            this.key.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.key.Size = new System.Drawing.Size(171, 127);
            this.key.TabIndex = 13;
            this.key.Text = "Key";
            this.key.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // userCtrl2
            // 
            this.Appearance.BackColor = System.Drawing.Color.White;
            this.Appearance.Options.UseBackColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelControl1);
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "userCtrl2";
            this.Size = new System.Drawing.Size(856, 131);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            this.sidePanel4.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private DevExpress.XtraEditors.LabelControl number;
        private DevExpress.XtraEditors.LabelControl title;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.SidePanel sidePanel4;
        private System.Windows.Forms.Label key;
    }
}
