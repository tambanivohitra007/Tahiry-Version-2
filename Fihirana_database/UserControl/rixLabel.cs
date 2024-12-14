// ------------------------------------------------------------
// Author: Rindra Razafinjatovo
// Created on: 2018
// Last Modified: Dec 2024
// Project: Tahiry
// Description: A collection of Bible and Hymnals to streamline and enhance worship presentations for churches.
// ------------------------------------------------------------

﻿using DevExpress.XtraEditors;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Fihirana_database.UserControl
{
    [ToolboxItem(true)]
    public partial class rixLabel : LabelControl
    {
        private bool initializationComplete;
        private BufferedGraphicsContext backbufferContext;
        private BufferedGraphics backbufferGraphics;
        private Graphics drawingGraphics;
        private bool isDisposing;

        public rixLabel()
        {
            InitializeComponent();

            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, false);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);

            // Assign our buffer context.
            backbufferContext = BufferedGraphicsManager.Current;
            initializationComplete = true;

            RecreateBuffers();
        }

        protected override void Dispose(bool disposing)
        {
            //isDisposing = true;
            //if (disposing)
            //{
            //    if (components != null)
            //        components.Dispose();

            //    // We must dispose of backbufferGraphics before we dispose of backbufferContext or we will get an exception.
            //    if (backbufferGraphics != null)
            //        backbufferGraphics.Dispose();
            //    if (backbufferContext != null)
            //        backbufferContext.Dispose();
            //}
            base.Dispose(disposing);
        }

        private void RecreateBuffers()
        {
            // Check initialization has completed so we know backbufferContext has been assigned.
            // Check that we aren't disposing or this could be invalid.
            if (!initializationComplete || isDisposing)
                return;

            // We recreate the buffer with a width and height of the control. The "+ 1" 
            // guarantees we never have a buffer with a width or height of 0. 
            backbufferContext.MaximumBuffer = new Size(this.Width + 1, this.Height + 1);

            // Dispose of old backbufferGraphics (if one has been created already)
            if (backbufferGraphics != null)
                backbufferGraphics.Dispose();

            // Create new backbufferGrpahics that matches the current size of buffer.
            backbufferGraphics = backbufferContext.Allocate(this.CreateGraphics(),
                new Rectangle(0, 0, Math.Max(this.Width, 1), Math.Max(this.Height, 1)));

            // Assign the Graphics object on backbufferGraphics to "drawingGraphics" for easy reference elsewhere.
            drawingGraphics = backbufferGraphics.Graphics;

            // This is a good place to assign drawingGraphics.SmoothingMode if you want a better anti-aliasing technique.

            // Invalidate the control so a repaint gets called somewhere down the line.
            this.Invalidate();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            RecreateBuffers();
            //Redraw();
        }

        //protected override void OnPaint(PaintEventArgs e)
        //{
        //    // If we've initialized the backbuffer properly, render it on the control. 
        //    // Otherwise, do just the standard control paint.
        //    if (!isDisposing && backbufferGraphics != null)
        //        backbufferGraphics.Render(e.Graphics);
        //}
    }
}
