namespace Fihirana_database
{
    partial class fullScreenMode
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
            this.components = new System.ComponentModel.Container();
            DevExpress.XtraBars.Docking2010.WindowsUIButtonImageOptions windowsUIButtonImageOptions1 = new DevExpress.XtraBars.Docking2010.WindowsUIButtonImageOptions();
            DevExpress.XtraBars.Docking2010.WindowsUIButtonImageOptions windowsUIButtonImageOptions2 = new DevExpress.XtraBars.Docking2010.WindowsUIButtonImageOptions();
            DevExpress.XtraBars.Docking2010.WindowsUIButtonImageOptions windowsUIButtonImageOptions3 = new DevExpress.XtraBars.Docking2010.WindowsUIButtonImageOptions();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(fullScreenMode));
            this.timerPaint = new System.Windows.Forms.Timer(this.components);
            this.panelT = new System.Windows.Forms.Panel();
            this.flyoutP = new DevExpress.Utils.FlyoutPanel();
            this.winUiBtn = new DevExpress.XtraBars.Docking2010.WindowsUIButtonPanel();
            this.panelT.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.flyoutP)).BeginInit();
            this.flyoutP.SuspendLayout();
            this.SuspendLayout();
            // 
            // timerPaint
            // 
            this.timerPaint.Interval = 50;
            this.timerPaint.Tick += new System.EventHandler(this.timerPaint_Tick);
            // 
            // panelT
            // 
            this.panelT.BackColor = System.Drawing.Color.Black;
            this.panelT.Controls.Add(this.flyoutP);
            this.panelT.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelT.Location = new System.Drawing.Point(0, 0);
            this.panelT.Name = "panelT";
            this.panelT.Size = new System.Drawing.Size(453, 345);
            this.panelT.TabIndex = 0;
            this.panelT.Visible = false;
            this.panelT.MouseLeave += new System.EventHandler(this.panelT_MouseLeave);
            this.panelT.MouseHover += new System.EventHandler(this.panelT_MouseHover);
            // 
            // flyoutP
            // 
            this.flyoutP.Appearance.BackColor = System.Drawing.Color.Black;
            this.flyoutP.Appearance.Options.UseBackColor = true;
            this.flyoutP.Controls.Add(this.winUiBtn);
            this.flyoutP.Location = new System.Drawing.Point(9, 298);
            this.flyoutP.Margin = new System.Windows.Forms.Padding(0);
            this.flyoutP.Name = "flyoutP";
            this.flyoutP.OptionsBeakPanel.BackColor = System.Drawing.Color.Black;
            this.flyoutP.OptionsBeakPanel.BeakLocation = DevExpress.Utils.BeakPanelBeakLocation.Bottom;
            this.flyoutP.OptionsBeakPanel.Opacity = 0.5D;
            this.flyoutP.OwnerControl = this;
            this.flyoutP.ParentForm = this;
            this.flyoutP.Size = new System.Drawing.Size(173, 54);
            this.flyoutP.TabIndex = 0;
            // 
            // winUiBtn
            // 
            this.winUiBtn.BackColor = System.Drawing.Color.Black;
            this.winUiBtn.Buttons.AddRange(new DevExpress.XtraEditors.ButtonPanel.IBaseButton[] {
            new DevExpress.XtraBars.Docking2010.WindowsUIButton("Previous", false, windowsUIButtonImageOptions1, DevExpress.XtraBars.Docking2010.ButtonStyle.PushButton, "", -1, true, null, true, false, true, ((short)(0)), -1, false),
            new DevExpress.XtraBars.Docking2010.WindowsUIButton("Next", false, windowsUIButtonImageOptions2, DevExpress.XtraBars.Docking2010.ButtonStyle.PushButton, "", -1, true, null, true, false, true, ((short)(1)), -1, false),
            new DevExpress.XtraBars.Docking2010.WindowsUIButton("Fermer", false, windowsUIButtonImageOptions3, DevExpress.XtraBars.Docking2010.ButtonStyle.PushButton, "", -1, true, null, true, false, true, ((short)(2)), -1, false)});
            this.winUiBtn.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.winUiBtn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.winUiBtn.Location = new System.Drawing.Point(0, 0);
            this.winUiBtn.Name = "winUiBtn";
            this.winUiBtn.Padding = new System.Windows.Forms.Padding(15, 0, 0, 0);
            this.winUiBtn.Size = new System.Drawing.Size(173, 54);
            this.winUiBtn.TabIndex = 0;
            this.winUiBtn.Text = "windowsUIButtonPanel1";
            this.winUiBtn.ButtonClick += new DevExpress.XtraBars.Docking2010.ButtonEventHandler(this.winUiBtn_ButtonClick);
            // 
            // fullScreenMode
            // 
            this.Appearance.BackColor = System.Drawing.Color.Black;
            this.Appearance.Options.UseBackColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(453, 345);
            this.ControlBox = false;
            this.Controls.Add(this.panelT);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "fullScreenMode";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.panelT_Paint);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.fullScreenMode_KeyDown);
            this.panelT.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.flyoutP)).EndInit();
            this.flyoutP.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer timerPaint;
        private System.Windows.Forms.Panel panelT;
        private DevExpress.XtraBars.Docking2010.WindowsUIButtonPanel winUiBtn;
        private DevExpress.Utils.FlyoutPanel flyoutP;
    }
}