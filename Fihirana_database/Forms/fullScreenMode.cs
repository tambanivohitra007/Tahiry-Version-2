// ------------------------------------------------------------
// Author: Rindra Razafinjatovo
// Created on: 2018
// Last Modified: Dec 2024
// Project: Tahiry
// Description: A collection of Bible and Hymnals to streamline and enhance worship presentations for churches.
// ------------------------------------------------------------

﻿using System;
using System.Drawing;
using System.Windows.Forms;

namespace Fihirana_database
{
    public partial class fullScreenMode : DevExpress.XtraEditors.XtraForm
    {
        public delegate void Message(string str);

        private static string message;
        private static StringAlignment aln = StringAlignment.Center;

        public string Mon_message
        {
            get { return message; }
            set { message = value; }
        }

        private Rectangle rect;

        public fullScreenMode()
        {
            InitializeComponent();
            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;
            //timerPaint.Enabled = true;
            //timerPaint.Start();

            MainForm.clickRefresh += (s, e) => panelT.Invalidate();
            MainForm.closeProjection += (s, e) => this.Close();
            MainForm.showLogo += (s, e) => panelT.Invalidate();
            MainForm.GoNear += (s, e) => changeAlignment(StringAlignment.Near);
            MainForm.GoFar += (s, e) => changeAlignment(StringAlignment.Far);
            MainForm.GoCenter += (s, e) => changeAlignment(StringAlignment.Center);
            MainForm.BlackScreen += (s, e) =>
            {
                if (LogClass.isBlackScreen) panelT.Hide();
                else panelT.Show();
            };

            timerPaint.Enabled = true;
            timerPaint.Start();
        }

        private void changeAlignment(StringAlignment e)
        {
            aln = e;
            panelT.Refresh();
        }

        public void writeMessage(string str, bool projected = true)
        {
            if (!projected)
            {
                Close();
                this.Dispose();
            }
            else message = str;
        }

        private void fullScreenMode_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Escape)
            //{
            //    this.Close();
            //}
        }

        private void timerPaint_Tick(object sender, EventArgs e)
        {
            if (panelT.ForeColor != Color.FromArgb(255, 255, 255))
            {
                panelT.ForeColor = Color.FromArgb(255, 232, 232);
            }
            // You should replace the bolded image
            // in the sample below with an image of your own choosing.
            // Note the escape character used (@) when specifying the path.
            //panelT.BackgroundImage = Image.FromFile
            //   (System.Environment.GetFolderPath
            //   (System.Environment.SpecialFolder.Personal)
            //   + @"\Image.gif");

            Random rnd = new Random();
            _ = Color.FromArgb(rnd.Next(0, 255), rnd.Next(0, 255), rnd.Next(0, 255));
        }

        private void panelT_Paint(object sender, PaintEventArgs e)
        {
            int fontsize = 54;
            if (!LogClass.showLogo)
            {
            averina:

                Font font1 = new Font("Arial", fontsize, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point);
                Size textSize = TextRenderer.MeasureText(Mon_message, font1);
                SizeF sf = e.Graphics.MeasureString(Mon_message, font1, this.Height);

                StringFormat drawFormat = new StringFormat();

                rect = new Rectangle(25, 50, this.Width - 50, this.Height - 100);
                drawFormat.Alignment = aln;
                drawFormat.Trimming = StringTrimming.Word;
                drawFormat.LineAlignment = StringAlignment.Center;
                if (LogClass.isTextVerse)
                {
                    if (sf.Height > rect.Height)
                    {
                        fontsize -= 2;
                        goto averina;
                    }
                }
                else
                {
                    if (textSize.Height > rect.Height || textSize.Width > rect.Width + 50)
                    {
                        fontsize -= 2;
                        goto averina;
                    }
                    //TextFormatFlags flags = TextFormatFlags.Left | TextFormatFlags.EndEllipsis | TextFormatFlags.VerticalCenter;
                    //TextRenderer.DrawText(e.Graphics, Mon_message, font1, rect, Color.White, flags);
                }
                e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
                //e.Graphics.RotateTransform(45.0f, System.Drawing.Drawing2D.MatrixOrder.Prepend);
                //e.Graphics.TranslateTransform(-20, -70);
                e.Graphics.DrawString(Mon_message, font1, Brushes.White, rect, drawFormat);
                //e.Graphics.DrawRectangle(Pens.Red, rect);
                e.Graphics.DrawString("Eglise Adventiste Vohitsoa", this.Font, Brushes.AliceBlue, this.Width - 170, this.Height - 25);
            }
            else
            {
                rect = new Rectangle((this.Width - 400) / 2, (this.Height - 400) / 2, 400, 380);
                Bitmap bitmap = new Bitmap(Properties.Resources.adventist_symbol__white);
                e.Graphics.DrawImage(bitmap, rect);
            }

            // If there is an image and it has a location,
            // paint it when the Form is repainted.

            base.OnPaint(e);
        }

        private void panelT_MouseHover(object sender, EventArgs e)
        {
            flyoutP.ShowPopup();
        }

        private void panelT_MouseLeave(object sender, EventArgs e)
        {
            flyoutP.HidePopup();
        }

        private void winUiBtn_ButtonClick(object sender, DevExpress.XtraBars.Docking2010.ButtonEventArgs e)
        {
            switch (e.Button.Properties.Tag.ToString())
            {
                case "0": break;
                case "2": this.Close(); break;
                default:
                    break;
            }
        }
    }
}