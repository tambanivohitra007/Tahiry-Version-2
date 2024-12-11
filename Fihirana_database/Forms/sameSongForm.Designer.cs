namespace Fihirana_database.Forms
{
    partial class sameSongForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(sameSongForm));
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.delBtn = new DevExpress.XtraEditors.SimpleButton();
            this.gridControl = new DevExpress.XtraGrid.GridControl();
            this.dgvCrossRef = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.id_chant = new DevExpress.XtraGrid.Columns.GridColumn();
            this.title = new DevExpress.XtraGrid.Columns.GridColumn();
            this.number = new DevExpress.XtraGrid.Columns.GridColumn();
            this.cancelBtn = new DevExpress.XtraEditors.SimpleButton();
            this.iSave = new DevExpress.XtraEditors.SimpleButton();
            this.addToListBtn = new DevExpress.XtraEditors.SimpleButton();
            this.chantTitre = new DevExpress.XtraEditors.LabelControl();
            this.songList = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.searchLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.ID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.NUMERO = new DevExpress.XtraGrid.Columns.GridColumn();
            this.TITRE = new DevExpress.XtraGrid.Columns.GridColumn();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lblChant = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem3 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.emptySpaceItem4 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.session = new DevExpress.Xpo.Session(this.components);
            this.xpBindingSource = new DevExpress.Xpo.XPBindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCrossRef)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.songList.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblChant)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.session)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xpBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.delBtn);
            this.layoutControl1.Controls.Add(this.gridControl);
            this.layoutControl1.Controls.Add(this.cancelBtn);
            this.layoutControl1.Controls.Add(this.iSave);
            this.layoutControl1.Controls.Add(this.addToListBtn);
            this.layoutControl1.Controls.Add(this.chantTitre);
            this.layoutControl1.Controls.Add(this.songList);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Margin = new System.Windows.Forms.Padding(4);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(846, 572);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // delBtn
            // 
            this.delBtn.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("delBtn.ImageOptions.SvgImage")));
            this.delBtn.Location = new System.Drawing.Point(12, 492);
            this.delBtn.Margin = new System.Windows.Forms.Padding(4);
            this.delBtn.Name = "delBtn";
            this.delBtn.Size = new System.Drawing.Size(201, 52);
            this.delBtn.StyleController = this.layoutControl1;
            this.delBtn.TabIndex = 11;
            this.delBtn.Text = "Supprimer";
            this.delBtn.Click += new System.EventHandler(this.delBtn_Click);
            // 
            // gridControl
            // 
            this.gridControl.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(4);
            this.gridControl.Location = new System.Drawing.Point(12, 102);
            this.gridControl.MainView = this.dgvCrossRef;
            this.gridControl.Margin = new System.Windows.Forms.Padding(4);
            this.gridControl.Name = "gridControl";
            this.gridControl.Size = new System.Drawing.Size(822, 386);
            this.gridControl.TabIndex = 10;
            this.gridControl.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.dgvCrossRef});
            // 
            // dgvCrossRef
            // 
            this.dgvCrossRef.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.id_chant,
            this.title,
            this.number});
            this.dgvCrossRef.DetailHeight = 437;
            this.dgvCrossRef.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.dgvCrossRef.GridControl = this.gridControl;
            this.dgvCrossRef.Name = "dgvCrossRef";
            this.dgvCrossRef.OptionsBehavior.Editable = false;
            this.dgvCrossRef.OptionsBehavior.ReadOnly = true;
            this.dgvCrossRef.OptionsEditForm.PopupEditFormWidth = 1029;
            this.dgvCrossRef.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.dgvCrossRef.OptionsView.ShowDetailButtons = false;
            this.dgvCrossRef.OptionsView.ShowGroupExpandCollapseButtons = false;
            this.dgvCrossRef.OptionsView.ShowGroupPanel = false;
            this.dgvCrossRef.OptionsView.ShowIndicator = false;
            this.dgvCrossRef.OptionsView.ShowVerticalLines = DevExpress.Utils.DefaultBoolean.False;
            // 
            // id_chant
            // 
            this.id_chant.Caption = "ID";
            this.id_chant.FieldName = "ID";
            this.id_chant.MinWidth = 31;
            this.id_chant.Name = "id_chant";
            this.id_chant.Width = 230;
            // 
            // title
            // 
            this.title.Caption = "Titre";
            this.title.FieldName = "Title";
            this.title.MinWidth = 31;
            this.title.Name = "title";
            this.title.Visible = true;
            this.title.VisibleIndex = 1;
            this.title.Width = 1255;
            // 
            // number
            // 
            this.number.Caption = "Numéro";
            this.number.FieldName = "Number";
            this.number.MinWidth = 31;
            this.number.Name = "number";
            this.number.Visible = true;
            this.number.VisibleIndex = 0;
            this.number.Width = 312;
            // 
            // cancelBtn
            // 
            this.cancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelBtn.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("cancelBtn.ImageOptions.SvgImage")));
            this.cancelBtn.Location = new System.Drawing.Point(626, 492);
            this.cancelBtn.Margin = new System.Windows.Forms.Padding(4);
            this.cancelBtn.Name = "cancelBtn";
            this.cancelBtn.Size = new System.Drawing.Size(208, 52);
            this.cancelBtn.StyleController = this.layoutControl1;
            this.cancelBtn.TabIndex = 9;
            this.cancelBtn.Text = "Annuler";
            // 
            // iSave
            // 
            this.iSave.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("iSave.ImageOptions.SvgImage")));
            this.iSave.Location = new System.Drawing.Point(424, 492);
            this.iSave.Margin = new System.Windows.Forms.Padding(4);
            this.iSave.Name = "iSave";
            this.iSave.Size = new System.Drawing.Size(198, 52);
            this.iSave.StyleController = this.layoutControl1;
            this.iSave.TabIndex = 8;
            this.iSave.Text = "Enregistrer";
            this.iSave.Click += new System.EventHandler(this.iSave_Click);
            // 
            // addToListBtn
            // 
            this.addToListBtn.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("addToListBtn.ImageOptions.SvgImage")));
            this.addToListBtn.ImageOptions.SvgImageSize = new System.Drawing.Size(25, 25);
            this.addToListBtn.Location = new System.Drawing.Point(552, 56);
            this.addToListBtn.Margin = new System.Windows.Forms.Padding(4);
            this.addToListBtn.Name = "addToListBtn";
            this.addToListBtn.Size = new System.Drawing.Size(266, 42);
            this.addToListBtn.StyleController = this.layoutControl1;
            this.addToListBtn.TabIndex = 6;
            this.addToListBtn.Text = "Ajouter dans la liste";
            this.addToListBtn.Click += new System.EventHandler(this.addToListBtn_Click);
            // 
            // chantTitre
            // 
            this.chantTitre.Appearance.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.chantTitre.Appearance.Options.UseFont = true;
            this.chantTitre.Location = new System.Drawing.Point(12, 12);
            this.chantTitre.Margin = new System.Windows.Forms.Padding(4);
            this.chantTitre.Name = "chantTitre";
            this.chantTitre.Size = new System.Drawing.Size(142, 24);
            this.chantTitre.StyleController = this.layoutControl1;
            this.chantTitre.TabIndex = 4;
            this.chantTitre.Text = "Titre du chant";
            // 
            // songList
            // 
            this.songList.EditValue = "Chant";
            this.songList.Location = new System.Drawing.Point(71, 56);
            this.songList.Margin = new System.Windows.Forms.Padding(4);
            this.songList.Name = "songList";
            this.songList.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.songList.Properties.NullText = "";
            this.songList.Properties.PopupFormSize = new System.Drawing.Size(358, 0);
            this.songList.Properties.PopupSizeable = false;
            this.songList.Properties.PopupView = this.searchLookUpEdit1View;
            this.songList.Size = new System.Drawing.Size(477, 26);
            this.songList.StyleController = this.layoutControl1;
            this.songList.TabIndex = 5;
            // 
            // searchLookUpEdit1View
            // 
            this.searchLookUpEdit1View.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.ID,
            this.NUMERO,
            this.TITRE});
            this.searchLookUpEdit1View.DetailHeight = 437;
            this.searchLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.searchLookUpEdit1View.Name = "searchLookUpEdit1View";
            this.searchLookUpEdit1View.OptionsBehavior.Editable = false;
            this.searchLookUpEdit1View.OptionsBehavior.ReadOnly = true;
            this.searchLookUpEdit1View.OptionsEditForm.PopupEditFormWidth = 1029;
            this.searchLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.searchLookUpEdit1View.OptionsView.ShowGroupPanel = false;
            this.searchLookUpEdit1View.OptionsView.ShowIndicator = false;
            // 
            // ID
            // 
            this.ID.Caption = "gridColumn3";
            this.ID.FieldName = "ID";
            this.ID.MinWidth = 26;
            this.ID.Name = "ID";
            this.ID.Width = 96;
            // 
            // NUMERO
            // 
            this.NUMERO.Caption = "Numéroe";
            this.NUMERO.FieldName = "Number";
            this.NUMERO.MinWidth = 26;
            this.NUMERO.Name = "NUMERO";
            this.NUMERO.Visible = true;
            this.NUMERO.VisibleIndex = 0;
            this.NUMERO.Width = 320;
            // 
            // TITRE
            // 
            this.TITRE.Caption = "Titre";
            this.TITRE.FieldName = "Title";
            this.TITRE.MinWidth = 26;
            this.TITRE.Name = "TITRE";
            this.TITRE.Visible = true;
            this.TITRE.VisibleIndex = 1;
            this.TITRE.Width = 991;
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lblChant,
            this.layoutControlItem2,
            this.layoutControlItem3,
            this.emptySpaceItem1,
            this.layoutControlItem4,
            this.emptySpaceItem3,
            this.emptySpaceItem4,
            this.layoutControlItem5,
            this.emptySpaceItem2,
            this.layoutControlItem1,
            this.layoutControlItem6});
            this.Root.Name = "Root";
            this.Root.Size = new System.Drawing.Size(846, 572);
            this.Root.TextVisible = false;
            // 
            // lblChant
            // 
            this.lblChant.Control = this.chantTitre;
            this.lblChant.Location = new System.Drawing.Point(0, 0);
            this.lblChant.Name = "lblChant";
            this.lblChant.Size = new System.Drawing.Size(826, 28);
            this.lblChant.TextSize = new System.Drawing.Size(0, 0);
            this.lblChant.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.songList;
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 44);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(540, 46);
            this.layoutControlItem2.Text = "Chant:";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(47, 20);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.addToListBtn;
            this.layoutControlItem3.Location = new System.Drawing.Point(540, 44);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(270, 46);
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.Location = new System.Drawing.Point(810, 44);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(16, 46);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.iSave;
            this.layoutControlItem4.Location = new System.Drawing.Point(412, 480);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(202, 56);
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextVisible = false;
            // 
            // emptySpaceItem3
            // 
            this.emptySpaceItem3.AllowHotTrack = false;
            this.emptySpaceItem3.Location = new System.Drawing.Point(0, 536);
            this.emptySpaceItem3.Name = "emptySpaceItem3";
            this.emptySpaceItem3.Size = new System.Drawing.Size(826, 16);
            this.emptySpaceItem3.TextSize = new System.Drawing.Size(0, 0);
            // 
            // emptySpaceItem4
            // 
            this.emptySpaceItem4.AllowHotTrack = false;
            this.emptySpaceItem4.Location = new System.Drawing.Point(0, 28);
            this.emptySpaceItem4.Name = "emptySpaceItem4";
            this.emptySpaceItem4.Size = new System.Drawing.Size(826, 16);
            this.emptySpaceItem4.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.cancelBtn;
            this.layoutControlItem5.Location = new System.Drawing.Point(614, 480);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(212, 56);
            this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem5.TextVisible = false;
            // 
            // emptySpaceItem2
            // 
            this.emptySpaceItem2.AllowHotTrack = false;
            this.emptySpaceItem2.Location = new System.Drawing.Point(205, 480);
            this.emptySpaceItem2.Name = "emptySpaceItem2";
            this.emptySpaceItem2.Size = new System.Drawing.Size(207, 56);
            this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.gridControl;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 90);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(826, 390);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.delBtn;
            this.layoutControlItem6.Location = new System.Drawing.Point(0, 480);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(205, 56);
            this.layoutControlItem6.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem6.TextVisible = false;
            // 
            // sameSongForm
            // 
            this.AcceptButton = this.iSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelBtn;
            this.ClientSize = new System.Drawing.Size(846, 572);
            this.Controls.Add(this.layoutControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.IconOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("sameSongForm.IconOptions.SvgImage")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "sameSongForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Même chant (musique)";
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCrossRef)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.songList.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblChant)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.session)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xpBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraEditors.LabelControl chantTitre;
        private DevExpress.XtraLayout.LayoutControlItem lblChant;
        private DevExpress.XtraEditors.SimpleButton addToListBtn;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.Xpo.Session session;
        private DevExpress.Xpo.XPBindingSource xpBindingSource;
        private DevExpress.XtraEditors.SearchLookUpEdit songList;
        private DevExpress.XtraGrid.Views.Grid.GridView searchLookUpEdit1View;
        private DevExpress.XtraGrid.Columns.GridColumn ID;
        private DevExpress.XtraGrid.Columns.GridColumn NUMERO;
        private DevExpress.XtraGrid.Columns.GridColumn TITRE;
        private DevExpress.XtraEditors.SimpleButton iSave;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem3;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem4;
        private DevExpress.XtraEditors.SimpleButton cancelBtn;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraGrid.GridControl gridControl;
        private DevExpress.XtraGrid.Views.Grid.GridView dgvCrossRef;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraGrid.Columns.GridColumn id_chant;
        private DevExpress.XtraGrid.Columns.GridColumn title;
        private DevExpress.XtraGrid.Columns.GridColumn number;
        private DevExpress.XtraEditors.SimpleButton delBtn;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
    }
}