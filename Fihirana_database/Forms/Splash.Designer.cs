// ------------------------------------------------------------
// Author: Rindra Razafinjatovo
// Created on: 2018
// Last Modified: Dec 2024
// Project: Tahiry
// Description: A collection of Bible and Hymnals to streamline and enhance worship presentations for churches.
// ------------------------------------------------------------

﻿namespace Fihirana_database
{
    partial class Splash
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Splash));
            progressPanel = new DevExpress.XtraWaitForm.ProgressPanel();
            panel1 = new System.Windows.Forms.Panel();
            pictureEdit3 = new DevExpress.XtraEditors.PictureEdit();
            pictureEdit4 = new DevExpress.XtraEditors.PictureEdit();
            labelControl4 = new DevExpress.XtraEditors.LabelControl();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureEdit3.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureEdit4.Properties).BeginInit();
            SuspendLayout();
            // 
            // progressPanel
            // 
            progressPanel.Appearance.BackColor = System.Drawing.Color.DarkCyan;
            progressPanel.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, 0);
            progressPanel.Appearance.ForeColor = System.Drawing.Color.White;
            progressPanel.Appearance.Options.UseBackColor = true;
            progressPanel.Appearance.Options.UseFont = true;
            progressPanel.Appearance.Options.UseForeColor = true;
            progressPanel.AppearanceCaption.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            progressPanel.AppearanceCaption.ForeColor = System.Drawing.Color.White;
            progressPanel.AppearanceCaption.Options.UseFont = true;
            progressPanel.AppearanceCaption.Options.UseForeColor = true;
            progressPanel.AppearanceDescription.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            progressPanel.AppearanceDescription.ForeColor = System.Drawing.Color.White;
            progressPanel.AppearanceDescription.Options.UseFont = true;
            progressPanel.AppearanceDescription.Options.UseForeColor = true;
            progressPanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            progressPanel.BarAnimationMotionType = DevExpress.Utils.Animation.MotionType.WithAcceleration;
            progressPanel.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            progressPanel.Caption = "";
            progressPanel.CaptionToDescriptionDistance = 6;
            progressPanel.ContentAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            progressPanel.Description = "Chargement ...";
            progressPanel.LineAnimationElementHeight = 5;
            progressPanel.Location = new System.Drawing.Point(100, 182);
            progressPanel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            progressPanel.Name = "progressPanel";
            progressPanel.ShowCaption = false;
            progressPanel.ShowDescription = false;
            progressPanel.Size = new System.Drawing.Size(264, 26);
            progressPanel.TabIndex = 11;
            progressPanel.WaitAnimationType = DevExpress.Utils.Animation.WaitingAnimatorType.Line;
            // 
            // panel1
            // 
            panel1.BackColor = System.Drawing.Color.DarkCyan;
            panel1.Controls.Add(progressPanel);
            panel1.Controls.Add(pictureEdit3);
            panel1.Controls.Add(pictureEdit4);
            panel1.Controls.Add(labelControl4);
            panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            panel1.Location = new System.Drawing.Point(0, 0);
            panel1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            panel1.Name = "panel1";
            panel1.Size = new System.Drawing.Size(481, 299);
            panel1.TabIndex = 19;
            // 
            // pictureEdit3
            // 
            pictureEdit3.EditValue = resources.GetObject("pictureEdit3.EditValue");
            pictureEdit3.Location = new System.Drawing.Point(368, 237);
            pictureEdit3.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            pictureEdit3.Name = "pictureEdit3";
            pictureEdit3.Properties.AllowFocused = false;
            pictureEdit3.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            pictureEdit3.Properties.Appearance.Options.UseBackColor = true;
            pictureEdit3.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            pictureEdit3.Properties.ShowMenu = false;
            pictureEdit3.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Squeeze;
            pictureEdit3.Size = new System.Drawing.Size(101, 49);
            pictureEdit3.TabIndex = 8;
            // 
            // pictureEdit4
            // 
            pictureEdit4.EditValue = resources.GetObject("pictureEdit4.EditValue");
            pictureEdit4.Location = new System.Drawing.Point(141, 94);
            pictureEdit4.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            pictureEdit4.Name = "pictureEdit4";
            pictureEdit4.Properties.AllowFocused = false;
            pictureEdit4.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            pictureEdit4.Properties.Appearance.Options.UseBackColor = true;
            pictureEdit4.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            pictureEdit4.Properties.ShowMenu = false;
            pictureEdit4.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Zoom;
            pictureEdit4.Size = new System.Drawing.Size(187, 61);
            pictureEdit4.TabIndex = 9;
            // 
            // labelControl4
            // 
            labelControl4.Appearance.ForeColor = System.Drawing.Color.White;
            labelControl4.Appearance.Options.UseForeColor = true;
            labelControl4.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            labelControl4.Location = new System.Drawing.Point(14, 352);
            labelControl4.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            labelControl4.Name = "labelControl4";
            labelControl4.Size = new System.Drawing.Size(99, 16);
            labelControl4.TabIndex = 6;
            labelControl4.Text = "Copyright © 2019";
            // 
            // Splash
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(481, 299);
            Controls.Add(panel1);
            Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
            Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            Name = "Splash";
            Text = "Form1";
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureEdit3.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureEdit4.Properties).EndInit();
            ResumeLayout(false);
        }

        #endregion
        private DevExpress.XtraWaitForm.ProgressPanel progressPanel;
        private System.Windows.Forms.Panel panel1;
        private DevExpress.XtraEditors.PictureEdit pictureEdit3;
        private DevExpress.XtraEditors.PictureEdit pictureEdit4;
        private DevExpress.XtraEditors.LabelControl labelControl4;
    }
}
