using DevExpress.Data.Filtering;
using DevExpress.LookAndFeel;
using DevExpress.Xpo;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Localization;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraRichEdit;
using DevExpress.XtraRichEdit.API.Native;
using Fihirana_database.Classes;
using Fihirana_database.fihirana;
using Fihirana_database.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;


namespace Fihirana_database.UserControl
{

    public partial class FihiranaControl : XtraUserControl
    {
        private int countCantique;
        private RichEditDocumentServer server;
        private string MonTitre = "";
        private string lyrics = "";
        public static List<string> songParam;
        public string GetCount
        {
            get
            {
                return $"Cantiques enregistrés: {countCantique}";
            }
        }
        /// <summary>
        /// Fonction pour afficher ou cacher les catégories d'un cantique
        /// </summary>
        /// <returns></returns>
        public bool HideShowSidebar()
        {
            accordionCtrl.Visible = !accordionCtrl.Visible;
            splitContainerControl.SplitterPosition = accordionCtrl.Visible ? 600 : 330;
            return accordionCtrl.Visible;
        }
        /// <summary>
        /// Fonction pour rafraichier les données utilisées dans le GridView
        /// </summary>
        public void refreshData()
        {
            loadData();
        }
        private void loadData()
        {
            XPQuery<Hymnal> Hm = session.Query<Hymnal>();
            ClassFihirana.SessionHymnal = session;
            XPQuery<Category> resp = session.Query<Category>();

            var list = from s in Hm
                       join m in resp on s.Category equals m.ID_category
                       orderby s.Category, s.Number ascending
                       select new { s.ID, s.Number, s.Title, s.Author, s.Key, s.Stanzas, s.Lyrics, s.Active, s.Category, m.Description };

            BindCollection(list, rixCustomGrid);
            countCantique = list.Count();
        }
        /// <summary>
        /// Constructeur par défaut Fihirana Control
        /// </summary>
        public FihiranaControl()
        {
            BarLocalizer.Active = new CustomBarLocalizer();
            InitializeComponent();
            songParam = [];
            initCommand();
            refreshData();
            getListCategory();
            dataThematique.initAccordion(accordionCtrl);
            _ = accordionCtrl.ExpandAll();
            server = new RichEditDocumentServer();
            MainForm.changeColorSkin += (s, e) => BackColorChanged();
        }
        public object getSingle
        {
            get
            {
                if (customGridView.FocusedRowHandle >= 0)
                {
                    return customGridView.GetFocusedRow();
                }
                else
                    return null;
            }
        }
        public List<Chant> handleReady
        {
            get
            {
                return handleRichEdit();
            }
        }
        public void BindCollection(object collection, RixCustomGrid gridList)
        {
            try
            {
                // keep current index
                GridView view = gridList.Views[0] as GridView;
                int index = 0;
                if (view != null)
                    index = view.FocusedRowHandle;

                gridList.Refresh();
                gridList.BeginUpdate();
                gridList.DataSource = collection;
                gridList.RefreshDataSource();

                if (view != null)
                {
                    view.FocusedRowHandle = index;
                    _ = view.GetVisibleIndex(index);
                    view.TopRowIndex = index;
                    view.SelectRow(index);
                }

                gridList.EndUpdate();
            }
            catch (Exception ex)
            {
                _ = XtraMessageBox.Show($"Une erreur s'est produite: {ex.Message}", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #region Events Handler
        /// <summary>
        /// Function to registrer all the events
        /// </summary>
        private void initCommand()
        {
            addBtn.Click += new EventHandler(addBtn_Click);
            listBoxCross.DoubleClick += new EventHandler(listBoxCross_DoubleClick);
            customGridView.FocusedRowChanged += new FocusedRowChangedEventHandler(ProcessFocusedRowChanged);
            customGridView.ColumnFilterChanged += new EventHandler(this.customGridView_ColumnFilterChanged);
            readyBtn.Click += new EventHandler(readyBtn_Click);
            cmbFilterSong.SelectedIndexChanged += new EventHandler(this.iFilter_ListItemClick);
            searchLyrics.CheckedChanged += new EventHandler(searchLyrics_CheckedChanged);
            customGridView.RowCountChanged += new EventHandler(RowCountChanged);
            accordionCtrl.ElementClick += new DevExpress.XtraBars.Navigation.ElementClickEventHandler(accordionCtrl_ElementClick);
            customGridView.PopupMenuShowing += new DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventHandler(dgv_PopupMenuShowing);
            iModif.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(Modification_Click);
            iDelete.ItemClick += new ItemClickEventHandler(Delete_Click);
            iRefresh.ItemClick += (s, e) => loadData();
            iReference.ItemClick += new ItemClickEventHandler(iCrossReference_ItemClick);
        }
        private void Delete_Click(object sender, ItemClickEventArgs e)
        {
            if (getSingle != null)
                deleteItem();
        }
        private void Modification_Click(object sender, ItemClickEventArgs e)
        {
            getHymnal();
        }
        private void RowCountChanged(object sender, EventArgs e)
        {
            countCantique = customGridView.DataRowCount;
            FilterDropdownChanged?.Invoke(sender, e);
        }
        private void searchLyrics_CheckedChanged(object sender, EventArgs e)
        {
            customGridView.AdvancedSearch = searchLyrics.Checked;
            searchControl.ClearFilter();
            searchControl.EditValue = "";
            _ = searchControl.Focus();
        }
        private void addBtn_Click(object sender, EventArgs e)
        {
            try
            {
                XPQuery<Hymnal> Hm = session.Query<Hymnal>();

                long id = long.Parse(customGridView.GetRowCellValue(customGridView.FocusedRowHandle, customGridView.Columns["ID"]).ToString());

                using (UnitOfWork uow = new UnitOfWork())
                {
                    var p = uow.GetObjectByKey<Hymnal>(id);

                    using sameSongForm modalForm = new sameSongForm(p);
                    LogClass.CrossList.Clear();

                    if (modalForm.ShowDialog() == DialogResult.OK)
                    {
                        string joinned = string.Join("#", LogClass.CrossList);
                        p.CrossReference = joinned;
                        uow.CommitChanges();
                        loadData();
                    }
                }

                LogClass.CrossList.Add(id.ToString());
                foreach (var item in LogClass.CrossList)
                {
                    Hymnal tmpHymn = (Hymnal)session.FindObject(typeof(Hymnal),
                     CriteriaOperator.Parse($"ID={item}"));
                    List<string> itemsTmp =
                    [
                        item
                    ];

                    List<string> itemsOnly = LogClass.CrossList.Except(itemsTmp).ToList();

                    string joinned = string.Join("#", itemsOnly);
                    tmpHymn.CrossReference = joinned;
                    tmpHymn.Save();
                }
            }
            catch { }
        }
        private void listBoxCross_DoubleClick(object sender, EventArgs e)
        {
            cmbFilterSong.EditValue = null;
            customGridView.ClearColumnsFilter();
            customGridView.ClearFindFilter();

            ListBoxControl view = sender as ListBoxControl;
            string[] splitCrossSong = view.SelectedItem.ToString().Split('#');
            for (int i = 0; i < customGridView.DataRowCount; i++)
            {
                string dataInCell = customGridView.GetRowCellValue(i, "ID").ToString();
                if (dataInCell == splitCrossSong[1])
                {
                    customGridView.FocusedRowHandle = i;
                    break;
                }
            }
        }
        /// <summary>
        /// Used when focused has changed
        /// </summary>
        /// <param name="sender"></param>
        private void ProcessFocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            try
            {
                GridView dgv = sender as GridView;
                int rowHandle = dgv.FocusedRowHandle;

                if (dgv.FocusedRowHandle >= 0)
                {
                    //iDelete.Enabled = int.Parse(dgv.GetRowCellValue(dgv.FocusedRowHandle, dgv.Columns["Active"]).ToString()) == 0 ? true : false;
                    MonTitre = dgv.GetRowCellValue(dgv.FocusedRowHandle, dgv.Columns["Title"]).ToString();
                    string description = dgv.GetRowCellValue(dgv.FocusedRowHandle, dgv.Columns["Description"]).ToString();
                    int category = int.Parse(dgv.GetRowCellValue(dgv.FocusedRowHandle, dgv.Columns["Category"]).ToString());
                    ClassFihirana.Title = dgv.GetRowCellValue(dgv.FocusedRowHandle, dgv.Columns["Title"]).ToString();

                    listBoxCross.Items.Clear();
                    listCrossReference();

                    server = new RichEditDocumentServer
                    {
                        Text = MonTitre
                    };

                    Category tmpCategory = (Category)session.FindObject(typeof(Category),
                     CriteriaOperator.Parse($"ID_category={category}"));

                    DisplayForm.Title = MonTitre;
                    DisplayForm.Category = tmpCategory.Description;
                    DisplayForm.Number = long.Parse(dgv.GetRowCellValue(dgv.FocusedRowHandle, dgv.Columns["Number"]).ToString());

                    string rtfText = server.RtfText;
                    lyrics = dgv.GetRowCellValue(dgv.FocusedRowHandle, dgv.Columns["Lyrics"]).ToString();

                    richEdit.Document.RtfText = lyrics;
                    addBtn.Enabled = true;
                    //The target range is the first paragraph
                    DocumentRange range = richEdit.Document.Paragraphs[0].Range;
                    DocumentPosition pos = richEdit.Document.Range.Start;

                    // Create and customize an object
                    // that sets character formatting for the selected range
                    ParagraphProperties pp = richEdit.Document.BeginUpdateParagraphs(range);

                    // Set triple spacing
                    pp.LineSpacingType = ParagraphLineSpacing.Multiple;
                    pp.LineSpacingMultiplier = 2;
                    pp.SpacingAfter = 3;
                    //Finalize modifications
                    // with this method call
                    _ = richEdit.Document.AppendText(Environment.NewLine);
                    richEdit.Document.EndUpdateParagraphs(pp);
                    //}                                        

                    userCtrlTitle.Title = MonTitre;
                    userCtrlTitle.Number = dgv.GetRowCellValue(dgv.FocusedRowHandle, dgv.Columns["Number"]).ToString();

                    if (!string.IsNullOrWhiteSpace(dgv.GetRowCellValue(dgv.FocusedRowHandle, dgv.Columns["Key"]).ToString()))
                        userCtrlTitle.Key = dgv.GetRowCellValue(dgv.FocusedRowHandle, dgv.Columns["Key"]).ToString();
                    else userCtrlTitle.Key = "";

                    songParam.Clear();
                    // information sur le chant sélectionné **********************************************************
                    songParam.Add($"Titre: {dgv.GetRowCellValue(dgv.FocusedRowHandle, dgv.Columns["Title"])}");
                    songParam.Add($"Clé: {dgv.GetRowCellValue(dgv.FocusedRowHandle, dgv.Columns["Key"])}");
                    songParam.Add($"Numéro: {dgv.GetRowCellValue(dgv.FocusedRowHandle, dgv.Columns["Number"])}");
                    songParam.Add("Catégorie: " + description);
                    //checkClicked = false;
                    //end information sur le chant *******************************************************************                    

                    SetSongInfo?.Invoke(sender, e);
                }
                else
                {
                    addBtn.Enabled = false;
                    richEdit.Document.RtfText = "";
                    userCtrlTitle.Clear();
                }
            }
            catch { }
        }
        private void customGridView_ColumnFilterChanged(object sender, EventArgs e)
        {
            GridView view = sender as GridView;
            _ = BeginInvoke(new Action(() =>
            {
                ProcessFocusedRowChanged(sender, null);
            }));
        }
        private void readyBtn_Click(object sender, EventArgs e)
        {
            MainForm.isSongOrBible = true;
            ReadyButtonClick?.Invoke(this, e);
        }
        private void iCrossReference_ItemClick(object sender, ItemClickEventArgs e)
        {
            //GridView dgv = customGridView as GridView;
            ////int rowHandle = dgv.FocusedRowHandle;

            //if (!dgv.IsGroupRow(dgv.FocusedRowHandle))
            //{
            //    Chant f = new Chant();
            //    f.Number = "Numéro: " + dgv.GetRowCellValue(dgv.FocusedRowHandle, dgv.Columns["Number"]).ToString();
            //    f.Title = "Titre: " + dgv.GetRowCellValue(dgv.FocusedRowHandle, dgv.Columns["Title"]).ToString();

            //    //CrossReferenceForm sForm = new CrossReferenceForm(f);
            //    //sForm.ShowDialog();
            //}
            addBtn_Click(sender, e);

        }
        private void iFilter_ListItemClick(object sender, EventArgs e)
        {
            filtrerChant(cmbFilterSong.SelectedIndex + 1, 1);
            customGridView.FocusedRowHandle = 0;
        }
        #endregion        
        private void listCrossReference()
        {
            long id = long.Parse(customGridView.GetRowCellValue(customGridView.FocusedRowHandle, customGridView.Columns["ID"]).ToString());
            UnitOfWork uow = new UnitOfWork();
            var p = uow.GetObjectByKey<Hymnal>(id);

            if (!string.IsNullOrEmpty(p.CrossReference))
            {
                listBoxCross.Items.Clear();

                string[] splitCrossRef = p.CrossReference.Split('#');

                for (int i = 0; i < splitCrossRef.Length; i++)
                {
                    if (!string.IsNullOrEmpty(splitCrossRef[i]))
                    {
                        int index = int.Parse(splitCrossRef[i]);
                        //XPQuery<Hymnal> Hm = session.FindObject<Hymnal>(typeof(Hymnal), 
                        //    CriteriaOperator.Parse($"ID = {index}"));

                        Hymnal Hm = (Hymnal)session.FindObject(typeof(Hymnal),
                        CriteriaOperator.Parse($"ID={index}"));

                        _ = listBoxCross.Items.Add($"{Hm.Number} - {Hm.Title} #{Hm.ID}");
                    }
                }
            }
        }
        public List<Chant> handleRichEdit()
        {
            try
            {
                RichEditControl rich = richEdit;
                GridView dgv = customGridView;
                //The target range is the first paragraph
                DocumentRange range;

                List<Chant> getParoles = [];

                string titre = dgv.GetRowCellValue(dgv.FocusedRowHandle, dgv.Columns["Title"]).ToString();
                string number = "\nN° " + dgv.GetRowCellValue(dgv.FocusedRowHandle, dgv.Columns["Number"]).ToString();

                getParoles.Add(new Chant() { Parole = titre + number });


                Document document = rich.Document;
                document.BeginUpdate();
                range = document.Paragraphs[1].Range;

                ParagraphCollection pp = document.Paragraphs;

                int start = range.Start.ToInt();

                string getText = "";
                List<string> splitStr = [];
                var lastFound = pp.Last();
                DocumentRange range_ = null;
                //ISearchResult searchResult = document.StartSearch( "\\par", SearchOptions.CaseSensitive, SearchDirection.Forward, richEdit.Document.Range );

                foreach (var item in pp)
                {
                    if (item.Range.Length > 1)
                    {
                        start = item.Range.Start.ToInt();
                        range_ = rich.Document.CreateRange(start, item.Range.Length);
                        getText += rich.Document.GetText(range_) + "\n";
                    }
                    else
                    {
                        //getText += "~";
                        if (!string.IsNullOrEmpty(getText))
                        {
                            getParoles.Add(new Chant() { Parole = getText });
                            getText = string.Empty;
                        }
                    }

                    //string s = richEdit.Document.GetText(range_);
                }
                document.EndUpdate();

                return getParoles;
            }
            catch
            {
                return null;
            }
        }
        public void SaveChanges()
        {
            GridView dgv = customGridView;

            using UnitOfWork uow = new UnitOfWork();
            long id = long.Parse(dgv.GetRowCellValue(dgv.FocusedRowHandle, dgv.Columns["ID"]).ToString());
            var p = uow.GetObjectByKey<Hymnal>(id);
            p.Title = dgv.GetRowCellValue(dgv.FocusedRowHandle, dgv.Columns["Title"]).ToString();
            p.Number = int.Parse(dgv.GetRowCellValue(dgv.FocusedRowHandle, dgv.Columns["Number"]).ToString());
            p.Category = long.Parse(dgv.GetRowCellValue(dgv.FocusedRowHandle, dgv.Columns["Category"]).ToString());
            p.Key = dgv.GetRowCellValue(dgv.FocusedRowHandle, dgv.Columns["Key"]).ToString();
            p.Lyrics = richEdit.RtfText;

            uow.CommitChanges();
            loadData();
        }
        public void getHymnal()
        {
            GridView dgv = customGridView;
            XPQuery<Hymnal> Hm = session.Query<Hymnal>();

            int t = int.Parse(dgv.GetRowCellValue(dgv.FocusedRowHandle, dgv.Columns["ID"]).ToString());
            var list = Hm.Where(s => s.ID == t).SingleOrDefault();

            using NestedUnitOfWork nuow = session.BeginNestedUnitOfWork();
            LogClass.hymn = nuow.GetNestedObject(list);

            using AddSongForm modalForm = new AddSongForm(LogClass.hymn);
            if (modalForm.ShowDialog() == DialogResult.OK)
            {
                nuow.CommitChanges();
                int index = dgv.GetDataSourceRowIndex(dgv.FocusedRowHandle);
                loadData();
                int rowHandle = dgv.GetRowHandle(index);
                dgv.FocusedRowHandle = rowHandle;
            }
        }

        /// <summary>
        /// Une fonction pour filtrer un chant
        /// </summary>
        /// <param name="index"></param>
        /// <param name="type"></param>
        private void filtrerChant(int index, int type = 0)
        {
            var value = index;
            XPCollection colCategory = new XPCollection(session, typeof(Category));
            if (value <= colCategory.Count)
            {
                //ColumnView view = dgv;
                ColumnView view = customGridView;
                view.Columns["Category"].ClearFilter();
                _ = view.ActiveFilter.Add(view.Columns["Category"],
                  new ColumnFilterInfo($"[Category] = '{value}'", ""));
            }
            else
            {
                ColumnView view = customGridView;
                view.ClearColumnsFilter();
                //if (type == 0)
                //    iFilter.DataIndex = -1;
                //else
                //    cmbFilterSong.SelectedIndex = -1;
            }

            countCantique = customGridView.DataRowCount;
        }
        public void deleteItem()
        {
            string MonTitre = customGridView.GetRowCellValue(customGridView.FocusedRowHandle, customGridView.Columns["Title"]).ToString();
            Int64 o = Int64.Parse(customGridView.GetRowCellValue(customGridView.FocusedRowHandle, customGridView.Columns["ID"]).ToString(), System.Globalization.NumberStyles.Integer);
            _ = customGridView.FocusedRowHandle;
            if (XtraMessageBox.Show($"Voulez-vous supprimer le cantique <b>{MonTitre}</b>?", "CONFIRMATION", MessageBoxButtons.YesNo, MessageBoxIcon.Information, DevExpress.Utils.DefaultBoolean.True) == DialogResult.Yes)
            {

                Hymnal hymnalToDelete = session.GetObjectByKey<Hymnal>(o);
                session.Delete(hymnalToDelete);
                loadData();
            }
        }
        private void getListCategory()
        {
            Session sess = new Session
            {
                ConnectionString = XpoDefault.ConnectionString
            };

            XPCollection colCategory = new XPCollection(sess, typeof(Category));

            foreach (Category item in colCategory)
            {
                //iFilter.Strings.Add(item.Description).ToString();
                _ = cmbFilterSong.Properties.Items.Add(item.Description);
            }
            //iFilter.Strings.Add("Tous").ToString();
            _ = cmbFilterSong.Properties.Items.Add("Tous");
        }
        public void ClearItems()
        {
            cmbFilterSong.EditValue = null;
            customGridView.ClearColumnsFilter();
            customGridView.FocusedRowHandle = -1;
            listBoxCross.Items.Clear();
        }
        private void dgv_PopupMenuShowing(object sender, DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs e)
        {
            try
            {
                if (e.HitInfo.InRow)
                {
                    GridView view = sender as GridView;
                    view.FocusedRowHandle = e.HitInfo.RowHandle;

                    if (!view.IsGroupRow(e.HitInfo.RowHandle))
                    {
                        popupM.ShowPopup(MousePosition);
                    }
                }
            }
            catch
            {
            }
        }
        #region accordion pour les catégories
        /// <summary>
        /// Catégorie des chants
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void accordionCtrl_ElementClick(object sender, DevExpress.XtraBars.Navigation.ElementClickEventArgs e)
        {
            if (e.Element.Tag == null) return;

            switch (e.Element.Tag.ToString())
            {
                //Salamo
                case "mlg_1": change(1, 14, 1); break;

                //Andriamanitra
                case "mlg_2_1": change(15, 26, 1); break;
                case "mlg_2_2": change(27, 57, 1); break;
                case "mlg_2_3": change(58, 64, 1); break;

                //Jesosy Kristy
                case "mlg_3_1": change(65, 68, 1); break;
                case "mlg_3_2": change(69, 85, 1); break;
                case "mlg_3_3": change(86, 100, 1); break;
                case "mlg_3_4": change(101, 107, 1); break;
                case "mlg_3_5": change(108, 110, 1); break;
                case "mlg_3_6": change(111, 134, 1); break;
                case "mlg_3_7": change(135, 169, 1); break;

                //Fanahy Masina
                case "mlg_4": change(170, 184, 1); break;

                //Ny tenin'Andriamanitra
                case "mlg_5": change(185, 195, 1); break;

                //Ny Fiangonana
                case "mlg_6_1": change(196, 199, 1); break;
                case "mlg_6_2": change(200, 214, 1); break;
                case "mlg_6_3": change(215, 225, 1); break;
                case "mlg_6_4": change(226, 228, 1); break;
                case "mlg_6_5": change(229, 248, 1); break;
                case "mlg_6_6": change(249, 263, 1); break;
                case "mlg_6_7": change(264, 268, 1); break;
                case "mlg_6_8": change(269, 276, 1); break;
                case "mlg_6_9": change(277, 284, 1); break;
                case "mlg_6_10": change(285, 288, 1); break;
                case "mlg_6_11": change(289, 292, 1); break;

                //Fitoriana ny Filazantsara
                case "mlg_7_1": change(293, 345, 1); break;
                case "mlg_7_2": change(346, 365, 1); break;
                case "mlg_7_3": change(366, 376, 1); break;

                //Fiainana kristiana
                case "mlg_8_1": change(377, 395, 1); break;
                case "mlg_8_2": change(396, 418, 1); break;
                case "mlg_8_3": change(419, 448, 1); break;
                case "mlg_8_4": change(449, 472, 1); break;
                case "mlg_8_5": change(473, 473, 1); break;
                case "mlg_8_6": change(474, 492, 1); break;
                case "mlg_8_7": change(493, 513, 1); break;
                case "mlg_8_8": change(514, 533, 1); break;

                //Fotoana samihafa
                case "mlg_9_1": change(534, 540, 1); break;
                case "mlg_9_2": change(541, 546, 1); break;
                case "mlg_9_3": change(547, 553, 1); break;
                case "mlg_9_4": change(554, 562, 1); break;
                case "mlg_9_5": change(563, 564, 1); break;
                case "mlg_9_6": change(565, 566, 1); break;
                case "mlg_9_7": change(567, 568, 1); break;
                case "mlg_9_8": change(569, 571, 1); break;

                //Tanora
                case "mlg_10_1": change(572, 593, 1); break;
                case "mlg_10_2": change(594, 607, 1); break;
                case "mlg_10_3": change(608, 611, 1); break;
                case "mlg_10_4": change(612, 624, 1); break;
                case "mlg_10_5": change(625, 634, 1); break;
                case "mlg_10_6": change(635, 650, 1); break;

                //Hiran'olon-tokana
                case "mlg_11": change(651, 757, 1); break;

                //SDA HYMNAL
                //WORSHIP
                case "sda_1_1": change(1, 38, 3); break;
                case "sda_1_2": change(39, 45, 3); break;
                case "sda_1_3": change(46, 58, 3); break;
                case "sda_1_4": change(59, 63, 3); break;
                case "sda_1_5": change(64, 69, 3); break;

                //TRINITY
                case "sda_2": change(70, 73, 3); break;

                //GOD THE FATHER
                case "sda_3_1": change(74, 81, 3); break;
                case "sda_3_2": change(82, 91, 3); break;
                case "sda_3_3": change(92, 98, 3); break;
                case "sda_3_4": change(99, 104, 3); break;
                case "sda_3_5": change(105, 114, 3); break;

                //JESUS CHRIST
                case "sda_4_1": change(115, 117, 3); break;
                case "sda_4_2": change(118, 143, 3); break;
                case "sda_4_3": change(144, 153, 3); break;
                case "sda_4_4": change(154, 164, 3); break;
                case "sda_4_5": change(165, 176, 3); break;
                case "sda_4_6": change(177, 180, 3); break;
                case "sda_4_7": change(181, 199, 3); break;
                case "sda_4_8": change(200, 221, 3); break;
                case "sda_4_9": change(222, 227, 3); break;
                case "sda_4_10": change(228, 256, 3); break;

                //HOLY SPIRIT
                case "sda_5": change(257, 271, 3); break;

                //HOLY SCRIPTURE
                case "sda_6": change(271, 279, 3); break;

                //GOSPEL
                case "sda_7_1": change(279, 290, 3); break;
                case "sda_7_2": change(291, 296, 3); break;
                case "sda_7_3": change(297, 300, 3); break;
                case "sda_7_4": change(301, 331, 3); break;
                case "sda_7_5": change(332, 333, 3); break;
                case "sda_7_6": change(334, 343, 3); break;

                //CHRISTIAN CHURCH
                case "sda_8_1": change(344, 354, 3); break;
                case "sda_8_2": change(355, 375, 3); break;
                case "sda_8_3": change(376, 376, 3); break;
                case "sda_8_4": change(377, 378, 3); break;
                case "sda_8_5": change(379, 379, 3); break;

                //DOCTRINES
                case "sda_9_1": change(380, 395, 3); break;
                case "sda_9_2": change(396, 411, 3); break;
                case "sda_9_3": change(412, 412, 3); break;
                case "sda_9_4": change(413, 414, 3); break;
                case "sda_9_5": change(415, 417, 3); break;
                case "sda_9_6": change(418, 419, 3); break;
                case "sda_9_7": change(420, 437, 3); break;

                //EARLY ADVENT
                case "sda_10": change(438, 454, 3); break;

                //CHRISTIAN LIFE
                case "sda_11_1": change(455, 460, 3); break;
                case "sda_11_2": change(461, 471, 3); break;
                case "sda_11_3": change(472, 477, 3); break;
                case "sda_11_4": change(478, 505, 3); break;
                case "sda_11_5": change(506, 535, 3); break;
                case "sda_11_6": change(536, 555, 3); break;
                case "sda_11_7": change(556, 566, 3); break;
                case "sda_11_8": change(567, 570, 3); break;
                case "sda_11_9": change(571, 584, 3); break;
                case "sda_11_10": change(585, 589, 3); break;
                case "sda_11_11": change(590, 591, 3); break;
                case "sda_11_12": change(592, 605, 3); break;
                case "sda_11_13": change(606, 619, 3); break;
                case "sda_11_14": change(620, 633, 3); break;
                case "sda_11_15": change(634, 641, 3); break;
                case "sda_11_16": change(642, 644, 3); break;
                case "sda_11_17": change(645, 649, 3); break;

                //CHRISTIAN HOME
                case "sda_12_1": change(650, 655, 3); break;
                case "sda_12_2": change(656, 659, 3); break;

                //SENTENCES AND RESPONSES
                case "sda_13": change(660, 695, 3); break;

                //HYMNE ET LOUANGES
                //PSAUMES
                case "lou_1": change(1, 15, 2); break;

                // DIEU
                case "lou_2_1": change(16, 19, 2); break;
                case "lou_2_2": change(20, 37, 2); break;
                case "lou_2_3": change(38, 48, 2); break;

                //JESUS CHRIST
                case "lou_3_1": change(49, 56, 2); break;
                case "lou_3_2": change(57, 64, 2); break;
                case "lou_3_3": change(65, 78, 2); break;
                case "lou_3_4": change(79, 84, 2); break;
                case "lou_3_5": change(85, 91, 2); break;
                case "lou_3_6": change(92, 106, 2); break;
                case "lou_3_7": change(107, 121, 2); break;

                //LE SAINT-ESPRIT
                case "lou_4": change(122, 131, 2); break;

                //LA PAROLE DE DIEU, SA LOI
                case "lou_5": change(132, 141, 2); break;

                //L'EGLISE
                case "lou_6_1": change(142, 145, 2); break;
                case "lou_6_2": change(146, 155, 2); break;
                case "lou_6_3": change(156, 163, 2); break;
                case "lou_6_4": change(164, 175, 2); break;
                case "lou_6_5": change(176, 182, 2); break;
                case "lou_6_6": change(183, 193, 2); break;
                case "lou_6_7": change(194, 199, 2); break;
                case "lou_6_8": change(200, 211, 2); break;
                case "lou_6_9": change(212, 217, 2); break;
                case "lou_6_10": change(218, 225, 2); break;
                case "lou_6_11": change(226, 231, 2); break;

                //EVANGELISATION
                case "lou_7_1": change(232, 261, 2); break;
                case "lou_7_2": change(262, 274, 2); break;

                //VIE CHRETIENNE
                case "lou_8_1": change(275, 284, 2); break;
                case "lou_8_2": change(285, 303, 2); break;
                case "lou_8_3": change(304, 324, 2); break;
                case "lou_8_4": change(325, 343, 2); break;
                case "lou_8_5": change(344, 366, 2); break;
                case "lou_8_6": change(367, 388, 2); break;

                //ESPERANCE CHRETIENNE
                case "lou_9_1": change(389, 410, 2); break;

                //CHANT DIVERS
                case "lou_10_1": change(411, 415, 2); break;
                case "lou_10_2": change(416, 420, 2); break;
                case "lou_10_3": change(421, 426, 2); break;
                case "lou_10_4": change(427, 430, 2); break;
                case "lou_10_5": change(431, 436, 2); break;
                case "lou_10_6": change(437, 440, 2); break;
                case "lou_10_7": change(441, 444, 2); break;
                case "lou_10_8": change(445, 447, 2); break;
                case "lou_10_9": change(448, 453, 2); break;
                case "lou_10_10": change(454, 459, 2); break;

                //JEUNESSE
                case "lou_11_1": change(460, 480, 2); break;
                case "lou_11_2": change(481, 499, 2); break;
                case "lou_11_3": change(500, 512, 2); break;
                case "lou_11_4": change(513, 528, 2); break;
                case "lou_11_5": change(529, 541, 2); break;
                case "lou_11_6": change(542, 550, 2); break;

                //LES ENFANTS
                case "lou_12": change(551, 590, 2); break;

                //DUOS ET CHOEURS
                case "lou_13": change(591, 637, 2); break;

                //CHOEURS D'HOMMES
                case "lou_14": change(638, 654, 2); break;

                default:
                    break;
            }

            if (e.Element.Tag.ToString().Contains("7/"))
            {
                string[] first = e.Element.Tag.ToString().Split('/');
                string[] second = first[1].Split('-');

                change(int.Parse(second[0]), int.Parse(second[1]), int.Parse(first[0]));
            }
        }
        private void change(int debut, int fin, int cat)
        {
            ColumnView view = customGridView;

            view.ClearColumnsFilter();
            _ = view.ActiveFilter.Add(view.Columns["Category"],
              new ColumnFilterInfo($"[Category] = '{cat}' And [Number] Between ({debut},{fin})", ""));
        }
        #endregion...
        #region adding Event Handler 
        [Browsable(true)]
        [Category("Action")]
        [Description("Invoked when user clicks Ready Button")]
        public event EventHandler ReadyButtonClick;
        [Browsable(true)]
        [Category("Action")]
        [Description("Invoked when user clicks Filter dropdown")]
        public event EventHandler FilterDropdownChanged;
        [Description("Invoked when user select song")]
        public static event EventHandler SetSongInfo;
        #endregion

        private new void BackColorChanged()
        {
            if (UserLookAndFeel.Default.ActiveLookAndFeel.ActiveSvgPaletteName == SkinSvgPalette.Bezier.MercuryIce)
            {
                richEdit.ActiveView.BackColor = Color.FromArgb(56, 61, 69, 5);
                //richEdit.Document.DefaultCharacterProperties.ForeColor = Color.Black;
            }
            else
            {
                richEdit.ActiveView.BackColor = Color.White;
                //richEdit.Document.DefaultCharacterProperties.ForeColor = Color.Black;
            }

        }
    }
    public class CustomBarLocalizer : BarLocalizer
    {
        public CustomBarLocalizer() {}
        public override string GetLocalizedString(BarString id)
        {
            string ret;
            switch (id)
            {
                case BarString.AccordionControlSearchBoxPromptText: return "Recherche des mot-clés";
                default:
                    ret = base.GetLocalizedString(id);
                    break;
            }
            return ret;
        }
    }
}
