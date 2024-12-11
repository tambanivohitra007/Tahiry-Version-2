using DevExpress.Data.Filtering;
using DevExpress.LookAndFeel;
using DevExpress.Xpo;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Alerter;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraRichEdit;
using DevExpress.XtraRichEdit.API.Native;
using Fihirana_database.Classes;
using Fihirana_database.fihirana;
using Fihirana_database.Forms;
using Fihirana_database.UserControl;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Management;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Fihirana_database
{
    public partial class MainForm : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        #region variables declaration
        //Some instance variables

        private int selectBible = 0;
        private bool checkClicked = false;
        private bool addbibleToAgenda = false; //if false, then song will be by default, otherwise bible
        private bool isAgenda = false;
        private int selectBibleAll = 0;
        private bool hasBeenShown = false;
        private bool initColorHandle = false;

        //EventHandlers to be used for projection
        public static event EventHandler clickRefresh;
        public static event EventHandler SetFont;
        public static event EventHandler SetInfoSongs;
        public static event EventHandler closeProjection;
        public static event EventHandler ChangeFont;
        public static event EventHandler ChangeImageBackground;
        public static event EventHandler ChangeVideoBackground;
        public static event EventHandler showLogo;
        public static event EventHandler BlackScreen;
        public static event EventHandler GoNear;
        public static event EventHandler GoCenter;
        public static event EventHandler GoFar;
        public static event EventHandler SetTitle;
        public static event EventHandler GoBold;
        public static event EventHandler GoItalic;
        public static event EventHandler Increase;
        public static event EventHandler Decrease;
        public static event EventHandler HideHeader;
        public static event EventHandler SetColor;
        public static event EventHandler SelectBackground;
        public static event EventHandler changeColorSkin;
        public static event EventHandler InitializeCheckStsyle;
        public static bool isSongOrBible = false; //If true then isSongOrBible = song, otherwise, isSongOrBible = verse
        private List<Chant> getParoles;
        private FihiranaControl fihirana;
        private XPQuery<Settings> sett;

        //pour la recherche
        private XPQuery<verses_MLG> mlg;
        private XPQuery<verses_DIEM> diem;
        private XPQuery<verses_LSG> lsg;
        private XPQuery<verses_BDS> bds;
        private XPQuery<verses_NIV> niv;
        private XPQuery<verses_KJV> kjv;
        private XPQuery<books> book;
        private object Bible = null;

        private Screen[] screens;
        #endregion

        /// <summary>
        /// Close the projection
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void closePr_Clicked(object sender, EventArgs e)
        {
            closeProjection?.Invoke(sender, e);
        }

        /// <summary>
        /// Default constructor
        /// </summary>
        public MainForm()
        {
            InitializeComponent();
            SystemEvents.DisplaySettingsChanged += (s, e) => loadDisplays2();
            try
            {
                sett = session.Query<Settings>();
                screens = Screen.AllScreens;
                init();
                livreAutoComplete(txtSearchR, 0);

                timer.Enabled = true;

                getListCategory();
                listBible.SelectedIndex = 0;
                RestorePalette();
                initCommand();

                // Prevent the focused cell from being highlighted.
                gridV.OptionsSelection.EnableAppearanceFocusedCell = false;
                // Draw a dotted focus rectangle around the entire row.
                gridV.FocusRectStyle = DrawFocusRectStyle.RowFocus;

                var font = sett.Where(s => s.ID == 3).SingleOrDefault();
                iFonts.EditValue = font.Value;
                DisplayForm.FontFamily = iFonts.EditValue.ToString();
                selectAlignment();

                var bold = sett.Where(s => s.ID == 4).SingleOrDefault();
                var italic = sett.Where(s => s.ID == 5).SingleOrDefault();

                ClassFihirana.IsBold = iBold.Down = bold.Value == "True";
                ClassFihirana.IsItalic = iItalic.Down = italic.Value == "True";

                var HideTitle = sett.Where(s => s.ID == 8).SingleOrDefault();
                DisplayForm.isHide = HideTitle.Value == "1";
                iHideHeader.Down = DisplayForm.isHide;
                iHideHeader.DownChanged += (s, e) => iHideHeader.Caption = iHideHeader.Down ? "Afficher Logo" : "Cacher logo";

                string fontColor = sett.Where(s => s.ID == 9).SingleOrDefault().Value;
                string[] couleurs = fontColor.Split('-');
                iColorPick.EditValue = Color.FromArgb(Convert.ToInt32(couleurs[0]), Convert.ToInt32(couleurs[1]), Convert.ToInt32(couleurs[2]));

                listBibleAll2.SelectedIndex = 0;
                selectBibleRefresh(0);

                //populate items for effects
                foreach (Effects item in Effects.AllEffects())
                {
                    switch (item.Category)
                    {
                        case "boucing_entrances":
                            foreach (string eff in item.Liste)
                            {
                                _ = iBounce.Strings.Add(eff);

                            }
                            iBounce.ListItemClick += new ListItemClickEventHandler(iListEffect_ItemClick);
                            break;
                        case "fading_entrances":
                            foreach (string eff in item.Liste)
                            {
                                _ = iFading.Strings.Add(eff);
                            }
                            iFading.ListItemClick += new ListItemClickEventHandler(iListEffect_ItemClick);
                            break;
                        case "back_entrances":
                            foreach (string eff in item.Liste)
                            {
                                _ = iBack.Strings.Add(eff);
                            }
                            iBack.ListItemClick += new ListItemClickEventHandler(iListEffect_ItemClick);
                            break;
                        case "rotating_entrances":
                            foreach (string eff in item.Liste)
                            {
                                _ = iRotate.Strings.Add(eff);
                            }
                            iRotate.ListItemClick += new ListItemClickEventHandler(iListEffect_ItemClick);
                            break;
                        case "lightSpeed":
                            foreach (string eff in item.Liste)
                            {
                                _ = iLight.Strings.Add(eff);
                            }
                            iLight.ListItemClick += new ListItemClickEventHandler(iListEffect_ItemClick);
                            break;
                        case "specials":
                            foreach (string eff in item.Liste)
                            {
                                _ = iSpecial.Strings.Add(eff);
                            }
                            iSpecial.ListItemClick += new ListItemClickEventHandler(iListEffect_ItemClick);
                            break;
                        case "zooming_entrances":
                            foreach (string eff in item.Liste)
                            {
                                _ = iZoom.Strings.Add(eff);
                            }
                            iZoom.ListItemClick += new ListItemClickEventHandler(iListEffect_ItemClick);
                            break;
                        case "sliding_entrances":
                            foreach (string eff in item.Liste)
                            {
                                _ = iSlide.Strings.Add(eff);
                            }
                            iSlide.ListItemClick += new ListItemClickEventHandler(iListEffect_ItemClick);
                            break;
                        default:
                            break;
                    }

                }
                //Load effect from database
                string effectName = sett.Where(s => s.ID == 1).SingleOrDefault().Value;
                SetCheck(effectName);
                //load image path form database
                var imagePath = sett.Where(s => s.ID == 6).SingleOrDefault();
                DisplayForm.ImagePath = $"{imagePath.Value}";
            }
            catch (NullReferenceException ex)
            {
                _ = XtraMessageBox.Show($"Erreur de référence nulle: {ex.Message}");
            }
        }

        /// <summary>
        /// Initialisation method
        /// </summary>
        private void init()
        {
            //Fihirana initialization -----------------------------------------------------------
            fihirana = new FihiranaControl();
            fihirana.FilterDropdownChanged += new EventHandler(BarCountChanged);
            sidePanelLyrics.Controls.Add(fihirana);
            barCount.Caption = fihirana.GetCount;
            fihirana.Dock = DockStyle.Fill;
            //End fihirana ----------------------------------------------------------------------

            //UserLookAndFeel.Default.SetSkinStyle(ClassSettings.SwitchMode());

            iClearImage.ItemClick += (s, e) =>
            {
                DisplayForm.ImagePath = string.Empty;
                saveSettings(DisplayForm.ImagePath, 6);
                ChangeImageBackground?.Invoke(s, e);
            };
            iRemoveVideo.ItemClick += (s, e) =>
            {
                DisplayForm.VideoPath = string.Empty;
                saveSettings(DisplayForm.VideoPath, 7);
                ChangeVideoBackground?.Invoke(s, e);
            };

            txtSearchR.GotFocus += (s, e) =>
            {
                TextEdit txtbox = s as TextEdit;
                txtbox.Select(txtbox.Text.Length, 0);
            };

            txtSearchR.Click += (s, e) => txtSearchR.SelectAll();
            fihirana.ReadyButtonClick += (s, e) =>
            {
                getParoles = fihirana.handleReady;
                dataSourceUpdate(getParoles);
                HideHeader?.Invoke(null, null);
                SelectBackground?.Invoke(s, e);
            };
            FihiranaControl.SetSongInfo += (s, e) =>
            {
                titleSong.Text = FihiranaControl.songParam[0];
                keySong.Text = FihiranaControl.songParam[1];
                NumberSong.Text = FihiranaControl.songParam[2];
                categorySong.Text = FihiranaControl.songParam[3];
            };
            iColorPick.EditValueChanged += (s, e) =>
            {
                Color color = (Color)iColorPick.EditValue;
                DisplayForm.Couleur[0] = color.R;
                DisplayForm.Couleur[1] = color.G;
                DisplayForm.Couleur[2] = color.B;
                SetColor?.Invoke(s, e);
                if (initColorHandle)
                {
                    saveSettings($"{color.R}-{color.G}-{color.B}", 9);

                }
                initColorHandle = true;
            };

            loadDisplays2();

            barListProjector.ItemClick += (s, e) =>
            {
                barSelectProjector.Caption = barListProjector.Strings[barListProjector.DataIndex];
            };
        }

        /// <summary>
        /// Load different monitors from ObjectQuery
        /// </summary>
        private void loadDisplays()
        {
            ManagementScope scope = new ManagementScope("\\\\.\\ROOT\\cimv2");
            System.Management.ObjectQuery query = new System.Management.ObjectQuery("SELECT * FROM Win32_DesktopMonitor");

            using (ManagementObjectSearcher searcher = new ManagementObjectSearcher(scope, query))
            using (ManagementObjectCollection queryCollection = searcher.Get())
            {
                barListProjector.Strings.Clear();
                int count = 1;
                foreach (ManagementObject mo in queryCollection)
                {
                    _ = barListProjector.Strings.Add($"{count} - {mo["DeviceID"]}, {mo["Description"]}");
                    count++;
                }
            }

            //barListProjector.ItemClick += (s, e) => project();
            DisplayForm.CloseProjection += (s, e) => resetButtonSettings();
            selectAllBtn.Click += (s, e) =>
            {
                for (int i = 0; i < listVerse.Items.Count; i++)
                {
                    listVerse.SetSelected(i, true);
                }
            };
        }

        /// <summary>
        /// Load different monitors using Registry
        /// </summary>
        private void loadDisplays2()
        {
            // Set up the WMI query
            string query = "SELECT Caption, MonitorType, MonitorManufacturer, Name FROM Win32_DesktopMonitor";

            // Create a ManagementObjectSearcher with the query
            using (ManagementObjectSearcher searcher = new ManagementObjectSearcher(query))
            {
                // Execute the query and get the result
                ManagementObjectCollection result = searcher.Get();
                int count = 1;
                barListProjector.Strings.Clear();
                // Iterate through the result and print the values
                foreach (ManagementObject mo in result)
                {
                    _ = barListProjector.Strings.Add($"{count} - {mo["MonitorType"]}, {mo["Name"]}");
                    count++;
                }
            }

            //barListProjector.ItemClick += (s, e) => project();
            DisplayForm.CloseProjection += (s, e) => resetButtonSettings();
            selectAllBtn.Click += (s, e) =>
            {
                for (int i = 0; i < listVerse.Items.Count; i++)
                {
                    listVerse.SetSelected(i, true);
                }
            };
        }

        private static ManagementObjectCollection GetDisplayDevices(string wmiClass)
        {
            try
            {
                // Create a searcher object
                ManagementObjectSearcher searcher = new ManagementObjectSearcher(
                    $"SELECT * FROM {wmiClass}");

                // Retrieve all display devices
                return searcher.Get();
            }
            catch (ManagementException ex)
            {
                _ = XtraMessageBox.Show($"Error retrieving display devices: {ex.Message}", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        /// <summary>
        /// Set selected Effect, and use it in SetDataIndex();
        /// </summary>
        /// <param name="effectName"></param>
        private void SetCheck(string effectName)
        {
            BarListItem[] tmpItem = new BarListItem[]
            {
                iFading,iBounce, iBack, iRotate, iLight, iSpecial, iZoom, iSlide
            };
            foreach (BarListItem item in tmpItem)
            {
                item.ShowChecks = false;
            }

            SetDataIndex(tmpItem, effectName);
        }

        /// <summary>
        /// Fonction pour Sélectionné un effet
        /// </summary>
        /// <param name="items">Liste des effets connus</param>
        /// <param name="effectName">Le Nom de l'effet</param>
        private void SetDataIndex(BarListItem[] items, string effectName)
        {

            //Get selected index from database            
            foreach (BarListItem item in items)
            {
                if (item.Strings.Contains(effectName))
                {
                    item.DataIndex = item.Strings.IndexOf(effectName);
                    DisplayForm.Animation = effectName;
                    item.ShowChecks = true;
                }
            }
        }

        /// <summary>
        /// Fonction pour activer et sauver l'effet sélectionné
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void iListEffect_ItemClick(object sender, ListItemClickEventArgs e)
        {
            BarListItem barListItem = sender as BarListItem;
            SetCheck(barListItem.Strings[e.Index]);
            DisplayForm.Animation = barListItem.Strings[e.Index];
            saveSettings(barListItem.Strings[e.Index], 1);
        }
        private void BarCountChanged(object sender, EventArgs e) => barCount.Caption = fihirana.GetCount;

        /// <summary>
        /// function to add autocomplete mode
        /// </summary>
        /// <param name="rapidS"></param>
        /// <param name="type"></param>
        private void livreAutoComplete(TextEdit rapidS, int type)
        {
            XPCollection obj = xpCollBook;
            IEnumerable<books> t = obj.Session.Query<books>().ToList();
            _ = new List<string>();
            AutoCompleteStringCollection collection = [];
            foreach (var item in t)
            {
                switch (type)
                {
                    case 0: _ = collection.Add(item.long_name); break;
                    case 1: _ = collection.Add(item.long_name_french); break;
                    case 2: _ = collection.Add(item.long_name_english); break;
                    default: _ = collection.Add(item.long_name); break;
                }

            }

            rapidS.Properties.AdvancedModeOptions.AutoCompleteCustomSource = collection;
            rapidS.Properties.AdvancedModeOptions.AutoCompleteSource = AutoCompleteSource.CustomSource;
            rapidS.Properties.AdvancedModeOptions.AutoCompleteMode = TextEditAutoCompleteMode.Append;
        }

        #region Theming
        /// <summary>
        /// Used to write saved palette into registry
        /// </summary>
        private void SavePalette()
        {
            if (UserLookAndFeel.Default.SkinName == "The Bezier")
            {
                ClassSettings.WriteConfig("Palette", UserLookAndFeel.Default.ActiveSvgPaletteName);
            }
        }

        /// <summary>
        /// used to restore palette from the registry
        /// </summary>
        private void RestorePalette()
        {
            if (!string.IsNullOrEmpty(ClassSettings.ConfigName("SkinName")))
            {
                if (ClassSettings.ConfigName("SkinName") == "The Bezier" && !string.IsNullOrEmpty(ClassSettings.ConfigName("Palette")))
                {
                    UserLookAndFeel.Default.SetSkinStyle(ClassSettings.ConfigName("SkinName"), ClassSettings.ConfigName("Palette"));

                    iMode.Down = ClassSettings.ConfigName("Palette") == "Gloom Gloom";
                    changeColorSkin?.Invoke(null, null);
                }
                else
                    SetSkin(ClassSettings.ConfigName("SkinName"));
            }
            else if (!string.IsNullOrEmpty(ClassSettings.ConfigName("Palette")))
            {
                UserLookAndFeel.Default.SetSkinStyle(SkinStyle.Bezier, ClassSettings.ConfigName("Palette"));
            }
        }

        /// <summary>
        /// Set skin to the app
        /// </summary>
        /// <param name="skinName"></param>
        private void SetSkin(string skinName) => UserLookAndFeel.Default.SetSkinStyle(skinName);

        #endregion

        /// <summary>
        /// Get list of category inside database and put it on a BarListItem
        /// </summary>
        private void getListCategory()
        {
            Session sess = new Session
            {
                ConnectionString = XpoDefault.ConnectionString
            };

            XPCollection colCategory = new XPCollection(sess, typeof(Category));

            foreach (Category item in colCategory)
            {
                _ = iFilter.Strings.Add(item.Description).ToString();
                //cmbFilterSong.Properties.Items.Add(item.Description);
            }
            _ = iFilter.Strings.Add("Tous").ToString();
            //cmbFilterSong.Properties.Items.Add("Tous");
        }

        #region recherche

        private void selectBibleRefresh(int x)
        {
            book = session.Query<books>();
            switch (x)
            {
                case 0: mlg = session.Query<verses_MLG>(); Bible = from s in mlg join m in book on s.book_number equals m.book_number select new { text = StripHTML(s.text), book = m.long_name, chapter = s.chapter, verse = s.verse }; break;
                case 1: diem = session.Query<verses_DIEM>(); Bible = from s in diem join m in book on s.book_number equals m.book_number select new { text = StripHTML(s.text), book = m.long_name, chapter = s.chapter, verse = s.verse }; break;
                case 2: lsg = session.Query<verses_LSG>(); Bible = from s in lsg join m in book on s.book_number equals m.book_number select new { text = StripHTML(s.text), book = m.long_name_french, chapter = s.chapter, verse = s.verse }; break;
                case 3: bds = session.Query<verses_BDS>(); Bible = from s in bds join m in book on s.book_number equals m.book_number select new { text = StripHTML(s.text), book = m.long_name_french, chapter = s.chapter, verse = s.verse }; break;
                case 4: niv = session.Query<verses_NIV>(); Bible = from s in niv join m in book on s.book_number equals m.book_number select new { text = StripHTML(s.text), book = m.long_name_english, chapter = s.chapter, verse = s.verse }; break;
                case 5: kjv = session.Query<verses_KJV>(); Bible = from s in kjv join m in book on s.book_number equals m.book_number select new { text = StripHTML(s.text), book = m.long_name_english, chapter = s.chapter, verse = s.verse }; break;
                default:
                    break;
            }
        }
        #endregion

        //Open AddSongForm to add new song
        private void iAddSong_ItemClick(object sender, ItemClickEventArgs e)
        {
            using AddSongForm modalForm = new AddSongForm();
            if (modalForm.ShowDialog() == DialogResult.OK)
            {
                fihirana.refreshData();
            }
        }

        //Updating the song

        private void iRefresh_ItemClick(object sender, ItemClickEventArgs e)
        {
            fihirana.refreshData();
        }

        /// <summary>
        /// an Event to edit a song
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void iEdit_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                fihirana.getHymnal();
            }
            catch (Exception ex)
            {
                _ = XtraMessageBox.Show($"Erreur: {ex.Message}");
            }
        }

        /// <summary>
        /// to be used to print out date and time
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer_Tick(object sender, EventArgs e)
        {
            heure.Caption = $"Heure: {DateTime.Now.ToLongTimeString()}";
            date.Caption = $"Date: {DateTime.Now.ToString("dd MMMM, yyyy")}";
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            DialogResult = XtraMessageBox.Show("Voulez-vous quitter le programme?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (DialogResult == DialogResult.Yes)
            {
                base.OnFormClosing(e);
                SavePalette();
                Dispose();
                Environment.Exit(0);
                Application.Exit();
            }
            else e.Cancel = true;
        }

        private void dgv_DoubleClick(object sender, EventArgs e)
        {
            dgvAgenda.AddNewRow();
            addbibleToAgenda = false;
        }

        private void iAbout_ItemClick(object sender, ItemClickEventArgs e)
        {
            AboutForm dialogForm = new AboutForm();
            _ = dialogForm.ShowDialog();
        }

        /// <summary>
        /// wha will happen when focusing on a row 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timerFocused_Tick(object sender, EventArgs e)
        {
            //timerFocused.Enabled = false;
            //ProcessFocusedRowChanged( dgv );
        }

        private void setFormLocation(Screen screen)
        {
            try
            {
                if (gridV.DataRowCount > 0)
                {
                    displayForm = new DisplayForm
                    {
                        StartPosition = FormStartPosition.Manual
                    };
                    Rectangle bounds = screen.Bounds;
                    displayForm.SetBounds(bounds.X, bounds.Y, bounds.Width, bounds.Height);
                    displayForm.Show();

                    hasBeenShown = true;
                }

            }
            catch { return; }
        }
        private bool IsAnotherFormOpen()
        {
            foreach (Form openForm in Application.OpenForms)
            {
                if (openForm is DisplayForm)
                {
                    return true; // AnotherForm is already open
                }
            }
            return false; // AnotherForm is not open
        }
        /// <summary>
        /// List all available monitors in the system
        /// </summary>
        /// <param name="selectedMonitorIndex">Index of the monitor</param>
        private void showDisplay(int selectedMonitorIndex)
        {
            try
            {
                ManagementScope scope = new ManagementScope("\\\\.\\ROOT\\cimv2");
                System.Management.ObjectQuery query = new System.Management.ObjectQuery("SELECT * FROM Win32_DesktopMonitor");

                using ManagementObjectSearcher searcher = new ManagementObjectSearcher(scope, query);
                using ManagementObjectCollection queryCollection = searcher.Get();
                int currentMonitorIndex = 0;
                foreach (ManagementObject mo in queryCollection)
                {
                    if (currentMonitorIndex == selectedMonitorIndex)
                    {
                        setFormLocation(screens[currentMonitorIndex]);
                        break;
                    }
                    currentMonitorIndex++;
                }
            }
            catch { }
        }
        private DisplayForm displayForm;
        /// <summary>
        /// Projection sur les écrans disponibles
        /// </summary>
        /// <param name="project">Activer la projection ou pas</param>
        private void project()
        {
            if (gridV.DataRowCount > 0 && !IsAnotherFormOpen())
            {
                showDisplay(barListProjector.ItemIndex);

                gridV.FocusedRowHandle = 0;
                SetTitle?.Invoke(null, null);
            }
            else
            {
                //resetButtonSettings();                
                closeProjection?.Invoke(null, null);
            }

            InitializeCheckStsyle?.Invoke(null, null);
        }

        private void checkIfClosed(object sender, FormClosedEventArgs e)
        {
            iChooseMonitor.Down = false;
            iBlackScreen.Enabled = false;
            iClearProject.Enabled = false;
            iLogo.Enabled = false;
        }

        /// <summary>
        /// when Richedit contain something, what will happen
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void richEdit_ContentChanged(object sender, EventArgs e)
        {
            try
            {
                isAgenda = false;
                _ = fihirana.handleRichEdit();
                //handleRichEdit( richEdit, customGridView );
                //dataSourceUpdate();
            }
            catch { }
        }

        private void handleRichEdit(RichEditControl richEdit, GridView dgv)
        {
            try
            {
                //The target range is the first paragraph
                DocumentRange range;

                getParoles = [];

                string titre = dgv.GetRowCellValue(dgv.FocusedRowHandle, dgv.Columns["Title"]).ToString();
                string number = "\nN° " + dgv.GetRowCellValue(dgv.FocusedRowHandle, dgv.Columns["Number"]).ToString();

                if (!checkClicked)
                {
                    if (!isAgenda)
                    {
                        getParoles.Add(new Chant() { Parole = titre + number });
                    }
                    else
                    {
                        if (dgvAgenda.GetRowCellValue(dgvAgenda.FocusedRowHandle, dgvAgenda.Columns[0]).ToString() == "FF")
                        {
                            getParoles.Add(new Chant() { Parole = titre + number });
                        }
                    }
                }


                Document document = richEdit.Document;
                document.BeginUpdate();
                range = document.Paragraphs[1].Range;

                ParagraphCollection pp = document.Paragraphs;

                int start = range.Start.ToInt();

                string getText = "";
                List<string> splitStr = [];
                var lastFound = pp.Last();
                DocumentRange range_ = null;
                //ISearchResult searchResult = document.StartSearch( "\\par", SearchOptions.CaseSensitive, SearchDirection.Forward, richEdit.Document.Range );

                if (!isAgenda)
                {
                    foreach (var item in pp)
                    {
                        if (item.Range.Length > 1)
                        {
                            start = item.Range.Start.ToInt();
                            range_ = richEdit.Document.CreateRange(start, item.Range.Length);
                            getText += richEdit.Document.GetText(range_) + "\n";
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

                    //string[] splitS = getText.Split( '~' );
                    //foreach (string item in splitS)
                    //{
                    //    if (!string.IsNullOrEmpty( item )) getParoles.Add( new Chant() { Parole = item } );
                    //}
                }
                else
                {
                    if (dgvAgenda.GetRowCellValue(dgvAgenda.FocusedRowHandle, dgvAgenda.Columns[0]).ToString() == "FF")
                    {
                        foreach (var item in pp)
                        {
                            if (item.Range.Length > 1)
                            {
                                start = item.Range.Start.ToInt();
                                range_ = richEdit.Document.CreateRange(start, item.Range.Length);
                                getText += richEdit.Document.GetText(range_) + "\n";
                            }
                            else
                            {
                                //getText += "~";
                                getParoles.Add(new Chant() { Parole = getText });
                                getText = string.Empty;
                            }
                        }

                        //string[] splitS = getText.Split( '~' );
                        //foreach (string item in splitS)
                        //{
                        //    if (!string.IsNullOrEmpty( item )) getParoles.Add( new Chant() { Parole = item } );
                        //}
                    }
                    else
                    {
                        getParoles.Add(new Chant() { Parole = dgv.GetRowCellValue(dgv.FocusedRowHandle, dgv.Columns["Parole"]).ToString() });
                    }
                }

                document.EndUpdate();
            }
            catch
            {
                _ = XtraMessageBox.Show("Pas de paroles ni versets à présenter. ", "Avertissement", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
        }



        private void iDelete_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (fihirana.getSingle != null)
                fihirana.deleteItem();
        }

        private void xpColl_CollectionChanged(object sender, XPCollectionChangedEventArgs e)
        {
            if (e.CollectionChangedType == XPCollectionChangedType.AfterRemove)
                (sender as XPCollection).Session.Delete(e.ChangedObject);
            //refresh();
        }

        #region bible text manipulation

        /// <summary>
        /// Get number of bible chapters inside the database
        /// </summary>
        /// <param name="book_number">Number of the book (Old or New testament)</param>

        private void getChapter(int book_number, object sender)
        {
            XPQuery<books> livres = sessionBible.Query<books>();

            var getChapter = (from c in livres
                              where c.book_number == book_number
                              select c.chapter_count).Single();

            listChapter.Items.Clear();

            for (int i = 1; i <= getChapter; i++)
            {
                _ = listChapter.Items.Add(i);
            }
        }

        /// <summary>
        /// Get number of verses for each chapter (Old or new testament)
        /// </summary>
        /// <param name="book_number"></param>
        /// <param name="chapter"></param>
        private void GetVerseCount(int bookNumber, int chapter, int index)
        {
            int verseCount = 0;

            // Use a switch case to directly access the corresponding XPQuery type
            switch (index)
            {
                case 0:
                    verseCount = this.session.Query<verses_MLG>()
                        .Count(c => c.chapter == chapter && c.book_number == bookNumber);
                    break;
                case 1:
                    verseCount = this.session.Query<verses_DIEM>()
                        .Count(c => c.chapter == chapter && c.book_number == bookNumber);
                    break;
                case 2:
                    verseCount = this.session.Query<verses_LSG>()
                        .Count(c => c.chapter == chapter && c.book_number == bookNumber);
                    break;
                case 3:
                    verseCount = this.session.Query<verses_BDS>()
                        .Count(c => c.chapter == chapter && c.book_number == bookNumber);
                    break;
                case 4:
                    verseCount = this.session.Query<verses_NIV>()
                        .Count(c => c.chapter == chapter && c.book_number == bookNumber);
                    break;
                case 5:
                    verseCount = this.session.Query<verses_KJV>()
                        .Count(c => c.chapter == chapter && c.book_number == bookNumber);
                    break;
                default:
                    verseCount = 0;
                    break;
            }

            // Populate the listVerse items
            listVerse.Items.Clear();
            for (int i = 1; i <= verseCount; i++)
            {
                _ = listVerse.Items.Add(i);
            }
        }

        /// <summary>
        /// a Function to remove tags in the bible verse
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string StripHTML(string input)
        {
            return Regex.Replace(input, "<.*?>", String.Empty);
        }

        private BaseListBoxControl.SelectedItemCollection getSelectedVerses;
        private List<BibleVerse> getText = [];
        private string getTokoSyAndindiny;
        //private string verse = "";       

        /// <summary>
        /// get bible text verses from different versions
        /// </summary>
        /// <param name="x"></param>
        private void GetTextVerses(int x)
        {
            if (dgvBook.FocusedRowHandle >= 0)
            {
                int bookNumber = int.Parse(dgvBook.GetRowCellValue(dgvBook.FocusedRowHandle, dgvBook.Columns[1]).ToString());
                string longName = dgvBook.GetRowCellValue(dgvBook.FocusedRowHandle, dgvBook.Columns[5]).ToString();
                string longNameFr = dgvBook.GetRowCellValue(dgvBook.FocusedRowHandle, dgvBook.Columns[6]).ToString();
                string longNameEn = dgvBook.GetRowCellValue(dgvBook.FocusedRowHandle, dgvBook.Columns[7]).ToString();
                int chapter = listChapter.SelectedIndex + 1;

                getText.Clear();
                getSelectedVerses = listVerse.SelectedItems;

                // Dictionary mapping indices to queries
                var verseQueries = new Dictionary<int, Func<IQueryable<dynamic>>>
                {
                    { 0, () => session.Query<verses_MLG>().Where(c => c.chapter == chapter && c.book_number == bookNumber && getSelectedVerses.Contains(c.verse)).Select(c => new { c.verse, c.text }) },
                    { 1, () => session.Query<verses_DIEM>().Where(c => c.chapter == chapter && c.book_number == bookNumber && getSelectedVerses.Contains(c.verse)).Select(c => new { c.verse, c.text }) },
                    { 2, () => session.Query<verses_LSG>().Where(c => c.chapter == chapter && c.book_number == bookNumber && getSelectedVerses.Contains(c.verse)).Select(c => new { c.verse, c.text }) },
                    { 3, () => session.Query<verses_BDS>().Where(c => c.chapter == chapter && c.book_number == bookNumber && getSelectedVerses.Contains(c.verse)).Select(c => new { c.verse, c.text }) },
                    { 4, () => session.Query<verses_NIV>().Where(c => c.chapter == chapter && c.book_number == bookNumber && getSelectedVerses.Contains(c.verse)).Select(c => new { c.verse, c.text }) },
                    { 5, () => session.Query<verses_KJV>().Where(c => c.chapter == chapter && c.book_number == bookNumber && getSelectedVerses.Contains(c.verse)).Select(c => new { c.verse, c.text }) },
                };

                // Fetch verses using the selected index
                if (verseQueries.TryGetValue(x, out var query))
                {
                    foreach (var item in query())
                    {
                        string longNameToUse = x switch
                        {
                            2 or 3 => longNameFr,
                            4 or 5 => longNameEn,
                            _ => longName
                        };

                        getText.Add(new BibleVerse
                        {
                            Parole = $"{longNameToUse} {chapter} : {item.verse} \n\n {StripHTML(item.text)}"
                        });

                        getTokoSyAndindiny = $"{longNameToUse} {chapter} : {item.verse}";
                    }
                }

                // Update grid control with the fetched verses
                gridControlVerset.DataSource = null;
                gridControlVerset.DataSource = getText;
            }
        }


        private void getVerseFromMultipleBible()
        {
            if (dgvVerset.FocusedRowHandle >= 0)
            {
                getText.Clear();
                getText.Add(new BibleVerse() { Parole = $"{dgvVerset.GetRowCellValue(dgvVerset.FocusedRowHandle, dgvVerset.Columns[0]).ToString()}" });
                gridV.BeginDataUpdate();
                gridParole.DataSource = getText;
                gridV.EndDataUpdate();
                gridV.FocusedRowHandle = -1;
                gridV.RefreshData();
            }
        }

        #endregion bible text manipulation

        /// <summary>
        /// Initial commands to be used throughout the program at runtime
        /// </summary>
        private void initCommand()
        {

            viderAffichage.Click += (s, e) => { gridParole.DataSource = null; Mon_message = ""; screenShot.Invalidate(); };
            //Event to check if focused row is changed on dgvBook (for bible)
            dgvBook.FocusedRowObjectChanged += (s, e) =>
            {
                if (dgvBook.FocusedRowHandle <= -1)
                {
                    listChapter.Items.Clear();
                    listVerse.Items.Clear();
                    return;
                }
                else
                {
                    getChapter(int.Parse(dgvBook.GetRowCellValue(dgvBook.FocusedRowHandle, dgvBook.Columns[1]).ToString()), s);
                    GetTextVerses(selectBible);
                }

            };

            //event to check when iblackScreen button has been pushed
            iBlackScreen.DownChanged += (s, e) =>
            {
                LogClass.isBlackScreen = iBlackScreen.Down;
                BlackScreen?.Invoke(s, e);
            };

            //event to check when iLogo button has been pushed
            iLogo.DownChanged += (s, e) =>
            {
                LogClass.showLogo = iLogo.Down;
                showLogo?.Invoke(s, e);
                screenShot.Invalidate();
            };
            //event to make timerBibleFocused ticks
            timerBibleFocused.Tick += (s, e) =>
            {
                timerBibleFocused.Enabled = false;
            };

            //event when iCancelFiltre is clicked
            iCancelFiltre.ItemClick += (s, e) =>
            {
                fihirana.ClearItems();
                titleSong.Text = String.Empty;
                keySong.Text = String.Empty;
                NumberSong.Text = String.Empty;
                categorySong.Text = String.Empty;
                AuthorSong.Text = String.Empty;
                gridParole.DataSource = null;
            };

            //event to check selected index changed for listchapter
            listChapter.SelectedIndexChanged += (s, e) =>
            {
                listVerse.Items.Clear();
                if (dgvBook.FocusedRowHandle >= 0)
                    GetVerseCount(int.Parse(dgvBook.GetRowCellValue(dgvBook.FocusedRowHandle, dgvBook.Columns[1]).ToString()), listChapter.SelectedIndex + 1, listBible.SelectedIndex);
            };

            //Initialisation for gridVerset to have its own datasource at runtime
            gridVerset.DataSource = SampleDS();

            //add an event for dgvVerset to initiate row when new row has been added
            dgvVerset.InitNewRow += dgvVerset_InitNewRow;

            //Initialisation for gridAgenda to have its own datasource at runtime
            gridAgenda.DataSource = Agenda();
            dgvAgenda.InitNewRow += dgvAgenda_InitNewRow;
            iClearProject.ItemClick += (s, e) =>
            {
                if (gridParole.DataSource != null) gridParole.DataSource = null;
            };
            clearVersetBiblique.Click += (s, e) =>
            {
                gridVerset.DataSource = null;
                gridVerset.DataSource = SampleDS();
            };

            dgvVerset.Click += (s, e) => getVerseFromMultipleBible();
            iAddAgenda.ItemClick += (s, e) =>
            {
                addbibleToAgenda = false;
                dgvAgenda.AddNewRow();
            };

            DisplayForm.PreviousOrNext += (s, e) => keyDown(s, e);

            KeyDown += (s, e) =>
            {
                switch (e.KeyCode)
                {
                    case Keys.F5:
                        HandleF5Key(s);
                        break;
                    case Keys.R when e.Control:
                        HandleControlKey(iGoFar, GoFar, s, e);
                        break;
                    case Keys.L when e.Control:
                        HandleControlKey(iGoNear, GoNear, s, e);
                        break;
                    case Keys.E when e.Control:
                        HandleControlKey(iGoCenter, GoCenter, s, e);
                        break;
                    case Keys.B when e.Control:
                        HandleControlKey(iBold, GoBold, s, e);
                        break;
                    case Keys.I when e.Control:
                        HandleControlKey(iItalic, GoItalic, s, e);
                        break;
                    case Keys.T when e.Control:
                        HandleControlKey(iIncrease, Increase, s, e);
                        break;
                    case Keys.U when e.Control:
                        HandleControlKey(iDecrease, Decrease, s, e);
                        break;
                    default:
                        break;
                }
            };


            txtSearchR.KeyDown += (s, e) =>
            {

                if (e.KeyCode == Keys.Enter)
                {
                    try
                    {
                        string field = "";
                        switch (listBible.SelectedIndex)
                        {
                            case 0: case 1: field = "long_name"; break;
                            case 2: case 3: field = "long_name_french"; break;
                            case 4: case 5: field = "long_name_english"; break;
                            default:
                                break;
                        }

                        separateDigitString(txtSearchR.Text, field);
                    }
                    catch { }
                }
            };

            searchBibleVerseTxt.KeyDown += (s, e) =>
            {

                if (e.KeyCode == Keys.Enter)
                {
                    try
                    {
                        Search(searchBibleVerseTxt.Text);
                    }
                    catch { }
                }
            };

            resetBtn.Click += (s, e) =>
            {
                searchBibleVerseTxt.Text = "";
                gridControlBible.DataSource = null;

                lblCount.Text = $"Trouvés: 0";
                _ = searchBibleVerseTxt.Focus();
            };

            KeyPreview = true; // Enable key events to be captured at the form level            
        }
        private void HandleF5Key(object sender)
        {
            if (barListProjector.DataIndex < 0)
            {
                _ = XtraMessageBox.Show("Veuillez choisir un moniteur pour présenter le slide.\nCela peut être trouvé dans ACCUEIL> Ecran disponibles", "INFORMATION",
                   MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            iProjectToScreen.Down = !iProjectToScreen.Down;
            iPresent_DownChanged(sender, null);
        }

        private void HandleControlKey(BarButtonItem toggleButton, EventHandler? handler, object sender, KeyEventArgs e)
        {
            toggleButton.Down = true;
            handler?.Invoke(sender, e);
        }


        //sample data source
        public BindingList<BibleVerse> SampleDS()
        {
            BindingList<BibleVerse> ds = new BindingList<BibleVerse>
            {
                AllowNew = true
            };
            return ds;
        }

        public BindingList<Chant> Agenda()
        {
            BindingList<Chant> ds = new BindingList<Chant>
            {
                AllowNew = true
            };
            return ds;
        }

        private string Mon_message = "";

        /// <summary>
        /// Paint an extra small screen to see what is happening to the second screen
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void screenShot_Paint(object sender, PaintEventArgs e)
        {
            try
            {
                Rectangle rect = new Rectangle(0, 0, screenShot.Width, screenShot.Height);

                //base.OnPaint(e);

                int fontsize = 50;

                if (gridV.FocusedRowHandle >= 0)
                    Mon_message = gridV.GetRowCellValue(gridV.FocusedRowHandle, gridV.Columns[0]).ToString();
                else Mon_message = "";

                averina:

                Font font1 = new Font("Tahoma", fontsize, FontStyle.Bold, GraphicsUnit.Point);
                Size textSize = TextRenderer.MeasureText(Mon_message, font1);
                SizeF sf = e.Graphics.MeasureString(Mon_message, font1, screenShot.Height);

                StringFormat drawFormat = new StringFormat();

                StringAlignment aln;

                if (iGoNear.Down) { aln = StringAlignment.Near; saveSettings("1", 2); }
                else if (iGoFar.Down) { aln = StringAlignment.Far; saveSettings("2", 2); }
                else { aln = StringAlignment.Center; saveSettings("3", 2); }

                if (LogClass.isTextVerse)
                {
                    rect = new Rectangle(5, 10, screenShot.Width - 10, screenShot.Height);
                    drawFormat.Alignment = aln;

                    if (sf.Height > rect.Height)
                    {
                        fontsize -= 1;
                        goto averina;
                    }
                    e.Graphics.DrawString(Mon_message, font1, Brushes.White, rect, drawFormat);
                }
                else
                {
                    rect = new Rectangle(5, 0, screenShot.Width - 10, screenShot.Height);
                    drawFormat.Alignment = aln;

                    if (textSize.Height > rect.Height || textSize.Width > rect.Width)
                    {
                        fontsize -= 1;
                        goto averina;
                    }

                    TextFormatFlags flags = TextFormatFlags.HorizontalCenter | TextFormatFlags.EndEllipsis | TextFormatFlags.VerticalCenter;
                    TextRenderer.DrawText(e.Graphics, Mon_message, font1, rect, Color.White, flags);
                }

            }
            catch { }
        }

        private void tabPane_SelectedPageChanged(object sender, DevExpress.XtraBars.Navigation.SelectedPageChangedEventArgs e)
        {
            switch (tabPane.SelectedPageIndex)
            {
                case 0:
                    LogClass.isTextVerse = false;
                    panelInfoChant.Visible = true;
                    break;

                case 1:
                    LogClass.isTextVerse = true;
                    panelInfoChant.Visible = false;
                    break;
                case 3:
                    LogClass.isTextVerse = true;
                    panelInfoChant.Visible = false;
                    break;

                default:
                    break;
            }

            screenShot.Invalidate();
        }

        private void listBible_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtSearchR.Text = "";
            selectBible = listBible.SelectedIndex;
            switch (listBible.SelectedIndex)
            {
                case 0:
                case 1:
                    dgvBook.Columns["long_name"].Visible = true;
                    dgvBook.Columns["long_name_french"].Visible = false;
                    dgvBook.Columns["long_name_english"].Visible = false;
                    livreAutoComplete(txtSearchR, 0);
                    break;
                case 2:
                case 3:
                    dgvBook.Columns["long_name"].Visible = false;
                    dgvBook.Columns["long_name_french"].Visible = true;
                    dgvBook.Columns["long_name_english"].Visible = false;
                    livreAutoComplete(txtSearchR, 1);
                    break;
                case 4:
                case 5:
                    dgvBook.Columns["long_name"].Visible = false;
                    dgvBook.Columns["long_name_french"].Visible = false;
                    dgvBook.Columns["long_name_english"].Visible = true;
                    livreAutoComplete(txtSearchR, 2);
                    break;
            }
            GetTextVerses(selectBible);
        }

        private void listVerse_DoubleClick(object sender, EventArgs e)
        {
            addbibleToAgenda = true;
            dgvVerset.AddNewRow();
            dgvAgenda.AddNewRow();
        }

        private void dgvVerset_InitNewRow(object sender, InitNewRowEventArgs e)
        {
            GridView view = sender as GridView;
            view.SetRowCellValue(e.RowHandle, "Parole", getText[0].Parole);
        }

        /// <summary>
        /// Add new row in the agenda section
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvAgenda_InitNewRow(object sender, InitNewRowEventArgs e)
        {
            GridView view = sender as GridView;
            try
            {
                if (addbibleToAgenda)
                {
                    string[] s = getText[0].Parole.Split('\n', '*');
                    view.SetRowCellValue(e.RowHandle, "Number", getTokoSyAndindiny);
                    view.SetRowCellValue(e.RowHandle, "Title", s[2]);
                    view.SetRowCellValue(e.RowHandle, "Parole", getText[0].Parole);
                    view.SetRowCellValue(e.RowHandle, colCat, "BB");

                    string info = $"{getText[0].Parole.Substring(0, 50)} a été ajouté dans l'agenda";
                }
            }
            catch { }

        }

        /// <summary>
        /// Initialiser la partie multiple versets
        /// </summary>
        private void initGridMultipleVerse()
        {
            BindingSource bs = new BindingSource
            {
                DataSource = gridVerset.DataSource
            };

            gridParole.DataSource = bs;
        }
        private void dgvVerset_Click(object sender, EventArgs e)
        {
            initGridMultipleVerse();
        }

        #region backup and restoration

        private static string SpecialFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        /// <summary>
        /// For database backup
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void iBackup_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            string fileName = "database.rix";
            string sourcePath = SpecialFolderPath + @"\Rindrasoftware\tahiry";
            string sourceFile = Path.Combine(sourcePath, fileName);

            System.Windows.Forms.SaveFileDialog xSave = new System.Windows.Forms.SaveFileDialog
            {
                RestoreDirectory = true,

                Title = "Sauvegarde de la base de données",
                DefaultExt = "rix",

                Filter = "Fichier rix (*.rix)|*.rix"
            };
            _ = xSave.FileName;
            FileAttributes attribute = File.GetAttributes(sourceFile);
            if (xSave.ShowDialog() == DialogResult.OK)
            {
                string destFile = $"{xSave.FileName}";

                if ((attribute & FileAttributes.Hidden) == FileAttributes.Hidden)
                {
                    attribute = RemoveAttribute(attribute, FileAttributes.Hidden);
                }
                File.Copy(sourceFile, destFile, true);
                File.SetAttributes(destFile, attribute);

                _ = XtraMessageBox.Show($"Backup réussi:\n {DateTime.Now}", "CONFIRMATION");
            }
        }

        private static FileAttributes RemoveAttribute(FileAttributes attributes, FileAttributes attributesToRemove)
        {
            return attributes & ~attributesToRemove;
        }

        /// <summary>
        /// For restoring database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void iRestore_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            string fileName = "database.rix";
            string sourcePath = SpecialFolderPath + @"\rindrasoftware\tahiry";
            string destFile = System.IO.Path.Combine(sourcePath, fileName);

            System.Windows.Forms.OpenFileDialog dlg = new System.Windows.Forms.OpenFileDialog
            {
                Title = "Restauration de la base de données",
                DefaultExt = "rix",

                Filter = "Fichier rix (*.rix)|*.rix"
            };

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                string sourceFile = dlg.FileName;

                System.IO.File.Copy(sourceFile, destFile, true);
                _ = XtraMessageBox.Show("Restauration complète. \nMaintenant l'application va redémarrer.", "CONFIRMATION");
                System.Windows.Forms.Application.Restart();
            }
        }

        #endregion
        private void dgvAgenda_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            processAgendaRow(sender);
        }

        /// <summary>
        /// Process Agenda Row (Focus row handle)
        /// </summary>
        /// <param name="sender"></param>
        private void processAgendaRow(object sender)
        {
            try
            {
                GridView view = sender as GridView;

                if (view.FocusedRowHandle >= 0 && !view.IsGroupRow(view.FocusedRowHandle))
                {
                    if (dgvAgenda.GetRowCellValue(dgvAgenda.FocusedRowHandle, dgvAgenda.Columns[0]).ToString() == "FF")
                    {
                        richEditParole.RtfText = dgvAgenda.GetRowCellValue(dgvAgenda.FocusedRowHandle, dgvAgenda.Columns[3]).ToString();
                    }
                    else
                    {
                        richEditParole.Text = dgvAgenda.GetRowCellValue(dgvAgenda.FocusedRowHandle, dgvAgenda.Columns[3]).ToString();
                    }
                }
                else
                {
                    richEditParole.RtfText = "";
                }
            }
            catch (Exception)
            {

                throw;
            }


        }

        /// <summary>
        /// Une fonction pour afficher le contenu dans le GRID
        /// </summary>
        /// <param name="s"></param>
        /// <param name="e"></param>
        private void showContentGrid(object s, FocusedRowChangedEventArgs e)
        {
            try
            {
                if (gridV.FocusedRowHandle >= 0)
                {
                    screenShot.Invalidate();

                    DisplayForm.Lyrics = gridV.GetRowCellValue(gridV.FocusedRowHandle, gridV.Columns[0]).ToString();
                    clickRefresh?.Invoke(s, e);
                }
            }
            catch { }
        }

        private void dgvAgenda_FocusedRowObjectChanged(object sender, FocusedRowObjectChangedEventArgs e)
        {
            processAgendaRow(sender);
        }

        private void iPresent_DownChanged(object sender, ItemClickEventArgs e)
        {
            if (gridV.DataRowCount > 0 && barListProjector.DataIndex > -1 && iProjectToScreen.Down)
            {
                iBlackScreen.Enabled = true;
                iClearProject.Enabled = true;
                iLogo.Enabled = true;
                barListProjector.Enabled = false;
                iProjectToScreen.Down = true;


                project();
                gridV.FocusedRowHandle = 0;

            }
            else
            {
                resetButtonSettings();
                closeProjection?.Invoke(null, null);
            }
        }

        private void resetButtonSettings()
        {
            try
            {
                iChooseMonitor.Down = false;
                iBlackScreen.Enabled = false;
                iClearProject.Enabled = false;
                iLogo.Enabled = false;
                iProjectToScreen.Down = false;
                barListProjector.Enabled = true;
                barSelectProjector.Caption = "Aucun moniteur sélectionné";
            }
            catch { }
        }
        private void dgvAgenda_PopupMenuShowing(object sender, DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs e)
        {
            try
            {
                if (e.HitInfo.InRow)
                {
                    GridView view = sender as GridView;
                    view.FocusedRowHandle = e.HitInfo.RowHandle;

                    if (!view.IsGroupRow(e.HitInfo.RowHandle))
                    {
                        popupAgenda.ShowPopup(Control.MousePosition);
                    }
                }
            }
            catch
            {
            }
        }

        /// <summary>
        /// Save selected mode such as bold, italic...
        /// </summary>
        /// <param name="value"></param>
        /// <param name="index"></param>
        private void saveSettings(string value, int index)
        {
            try
            {
                Settings s = session.FindObject<Settings>(CriteriaOperator.Parse($"ID = {index}")); ;
                s.Value = value;
                session.Save(s);
            }
            catch { }
        }
        private void Align_ItemClick(object sender, ItemClickEventArgs e)
        {
            switch (e.Item.Tag.ToString())
            {
                case "near":
                    iGoFar.Down = false;
                    iGoNear.Down = true;
                    iGoCenter.Down = false;
                    GoNear?.Invoke(sender, e);
                    saveSettings("1", 2);
                    break;

                case "center":
                    iGoFar.Down = false;
                    iGoNear.Down = false;
                    iGoCenter.Down = true;
                    GoCenter?.Invoke(sender, e);
                    saveSettings("3", 2);
                    break;

                case "far":
                    iGoFar.Down = true;
                    iGoNear.Down = false;
                    iGoCenter.Down = false;
                    GoFar?.Invoke(sender, e);
                    saveSettings("2", 2);
                    break;
                case "bold": GoBold?.Invoke(sender, e); break;
                case "italic": GoItalic?.Invoke(sender, e); break;
                case "increase": Increase?.Invoke(sender, e); break;
                case "decrease": Decrease?.Invoke(sender, e); break;
            }
        }

        private void fileNewItem1_ItemClick(object sender, ItemClickEventArgs e)
        {
            checkClicked = true;
            gridAgenda.DataSource = null;
        }

        private void iFonts_EditValueChanged(object sender, EventArgs e)
        {
            DisplayForm.FontFamily = iFonts.EditValue.ToString();
            ChangeFont?.Invoke(sender, e);

            Settings s = session.FindObject<Settings>(CriteriaOperator.Parse("ID = 3"));
            //Mettre à jour la configuration dans la base
            s.Value = DisplayForm.FontFamily;
            session.Save(s);
        }
        private void gridV_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            showContentGrid(sender, e);
        }

        private void searchForBible_EditValueChanged(object sender, EventArgs e)
        {
            //Task.Delay(1000);
            //    Search(searchForBible.Text);
        }

        /// <summary>
        /// Find for a text or expression inside the Bible
        /// </summary>
        /// <param name="searchText"></param>
        private async void Search(string searchText)
        {
            object query = null;
            await Task.Run(() =>
            {
                switch (listBibleAll2.SelectedIndex)
                {
                    case 0:
                        query = from s in mlg
                                join m in book on s.book_number equals m.book_number
                                where s.text.ToLower().Contains(searchText.ToLower())
                                select new
                                {
                                    text = StripHTML(s.text),
                                    book = m.long_name,
                                    chapter = s.chapter,
                                    verse = s.verse
                                };
                        break;
                    case 1:
                        query = from s in diem
                                join m in book on s.book_number equals m.book_number
                                where s.text.ToLower().Contains(searchText.ToLower())
                                select new
                                {
                                    text = StripHTML(s.text),
                                    book = m.long_name,
                                    chapter = s.chapter,
                                    verse = s.verse
                                };
                        break;
                    case 2:
                        query = from s in lsg
                                join m in book on s.book_number equals m.book_number
                                where s.text.ToLower().Contains(searchText.ToLower())
                                select new
                                {
                                    text = StripHTML(s.text),
                                    book = m.long_name_french,
                                    chapter = s.chapter,
                                    verse = s.verse
                                };
                        break;
                    case 3:
                        query = from s in bds
                                join m in book on s.book_number equals m.book_number
                                where s.text.ToLower().Contains(searchText.ToLower())
                                select new
                                {
                                    text = StripHTML(s.text),
                                    book = m.long_name_french,
                                    chapter = s.chapter,
                                    verse = s.verse
                                };
                        break;
                    case 4:
                        query = from s in niv
                                join m in book on s.book_number equals m.book_number
                                where s.text.ToLower().Contains(searchText.ToLower())
                                select new
                                {
                                    text = StripHTML(s.text),
                                    book = m.long_name_english,
                                    chapter = s.chapter,
                                    verse = s.verse
                                };
                        break;
                    case 5:
                        query = from s in kjv
                                join m in book on s.book_number equals m.book_number
                                where s.text.ToLower().Contains(searchText.ToLower())
                                select new
                                {
                                    text = StripHTML(s.text),
                                    book = m.long_name_english,
                                    chapter = s.chapter,
                                    verse = s.verse
                                };
                        break;
                    default:
                        break;
                }


            });

            gridControlBible.DataSource = null;
            gridControlBible.DataSource = query;

            if (BibleTileView.FocusedRowHandle >= 0)
            {
                BibleTileView.FocusedRowHandle = 0;
            }

            lblCount.Text = $"Trouvés: {BibleTileView.DataRowCount}";

        }


        private void timerSearchBible_Tick(object sender, EventArgs e)
        {
            timerSearchBible.Enabled = false;
            ProcessSelectionChanged();
        }

        private void ProcessSelectionChanged()
        {
            if (BibleTileView.FocusedRowHandle >= 0)
            {

                int[] selectedRows = BibleTileView.GetSelectedRows();
                getText.Clear();
                foreach (int rowHandle in selectedRows)
                {
                    string book = BibleTileView.GetRowCellValue(rowHandle, BibleTileView.Columns["book"]).ToString();
                    string text = BibleTileView.GetRowCellValue(rowHandle, BibleTileView.Columns["text"]).ToString();
                    int chapter = int.Parse(BibleTileView.GetRowCellValue(rowHandle, BibleTileView.Columns["chapter"]).ToString());
                    int verse = int.Parse(BibleTileView.GetRowCellValue(rowHandle, BibleTileView.Columns["verse"]).ToString());

                    getText.Add(new BibleVerse() { Parole = $"{book} {chapter} : {verse} \n\n {text}" });
                }

                _ = gridV.FocusedRowHandle;
                gridV.BeginDataUpdate();
                gridParole.DataSource = getText;
                gridV.EndDataUpdate();
                gridV.FocusedRowHandle = 0;
                gridV.RefreshData();

            }
        }

        private void searchForBible_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            searchForBible.Text = "";
        }

        /// <summary>
        /// Bold mode
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void iBold_DownChanged(object sender, ItemClickEventArgs e)
        {
            ClassFihirana.IsBold = iBold.Down == true;
            saveSettings(ClassFihirana.IsBold.ToString(), 4);
        }

        /// <summary>
        /// Italic mode
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void iItalic_DownChanged(object sender, ItemClickEventArgs e)
        {
            ClassFihirana.IsItalic = iItalic.Down == true;
            saveSettings(ClassFihirana.IsItalic.ToString(), 5);
        }

        /// <summary>
        /// Alignment mode
        /// </summary>
        private void selectAlignment()
        {
            sett = session.Query<Settings>();
            var a = sett.Where(s => s.ID == 2).SingleOrDefault();

            switch (a.Value)
            {
                case "1": iGoNear.Down = true; iGoCenter.Down = false; iGoFar.Down = false; ClassFihirana.Alignment = StringAlignment.Near; break;
                case "2": iGoNear.Down = false; iGoCenter.Down = false; iGoFar.Down = true; ClassFihirana.Alignment = StringAlignment.Center; break;
                case "3": iGoNear.Down = false; iGoCenter.Down = true; iGoFar.Down = false; ClassFihirana.Alignment = StringAlignment.Far; break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Used when direction buttons are pressed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void keyDown(object sender, KeyEventArgs e)
        {
            GridView view = gridV;

            if (e.KeyCode == Keys.Down
                || e.KeyCode == Keys.PageDown
                || e.KeyCode == Keys.Right
                || e.KeyCode == Keys.Enter)
            {
                if (!view.IsLastRow)
                {
                    view.MoveNext();
                    e.Handled = true;

                    showContentGrid(sender, null);
                }
                else
                {
                    LogClass.isBlackScreen = true;
                    BlackScreen?.Invoke(sender, e);
                }

            }

            else if (e.KeyCode == Keys.Up
                || e.KeyCode == Keys.PageUp
                || e.KeyCode == Keys.Left)
            {

                if (!view.IsFirstRow)
                {
                    if (LogClass.isBlackScreen)
                    {
                        LogClass.isBlackScreen = false;
                        BlackScreen?.Invoke(sender, e);
                    }
                    else
                    {
                        view.MovePrev();
                        e.Handled = true;

                        showContentGrid(sender, null);
                    }
                }
            }
            else
            {
                iBlackScreen.Down = false;
            }
        }

        //private void readyBtn_Click(object sender, EventArgs e)
        //{           
        //    handleRichEdit(richEdit, customGridView);
        //    dataSourceUpdate();
        //}

        private void dataSourceBible(GridControl gC, GridView grid)
        {
            gC.DataSource = null;
            grid.FocusedRowHandle = -1;
            grid.BeginDataUpdate();
            gC.DataSource = getText;
            grid.EndDataUpdate();
            grid.RefreshData();
        }
        private void readyBtnBible_Click(object sender, EventArgs e)
        {
            isSongOrBible = false;
            SetTitle?.Invoke(sender, null);
            dataSourceBible(gridParole, gridV);
            SelectBackground?.Invoke(sender, e);
        }

        private void dataSourceUpdate(List<Chant> paroles)
        {
            gridParole.DataSource = null;
            gridV.BeginDataUpdate();
            gridParole.DataSource = paroles;
            gridV.EndDataUpdate();
            gridV.FocusedRowHandle = 0;
        }

        private void rixGridView_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            if (timerSearchBible.Enabled) timerSearchBible.Enabled = false; // reset timer
            timerSearchBible.Enabled = true;  // start timer
        }

        private void gridV_RowClick(object sender, RowClickEventArgs e)
        {
            if (gridV.FocusedRowHandle == 0)
            {
                showContentGrid(sender, null);
            }
        }

        private void readyBtn2_Click(object sender, EventArgs e)
        {
            handleRichEdit(richEditParole, dgvAgenda);
            dataSourceUpdate(getParoles);
        }

        private void listBibleAll2_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectBibleAll = listBibleAll2.SelectedIndex;
            selectBibleRefresh(selectBibleAll);
        }

        private void gridControlBible_DataSourceChanged(object sender, EventArgs e)
        {
            BibleTileView.FocusInvalidRow();
        }

        private void iHideHeader_ItemClick(object sender, ItemClickEventArgs e)
        {
            saveSettings(iHideHeader.Down ? "1" : "0", 8);
            DisplayForm.isHide = iHideHeader.Down;
            HideHeader?.Invoke(null, null);
            iHideHeader.Caption = iHideHeader.Down ? "Affichier Logo" : "Cacher logo";
        }

        private void listVerse_SelectedIndexChangedAsync(object sender, EventArgs e)
        {
            _ = Task.Delay(300);
            GetTextVerses(selectBible);
        }

        private void tabPane_SelectedPageIndexChanged(object sender, EventArgs e)
        {
            DisplayForm.DisplayTab = tabPane.SelectedPageIndex;
            if (tabPane.SelectedPage == tabPane.Pages[0])
            {
                ribbonEdition.Enabled = true;
                ribbonOutil.Enabled = true;
                ribbonBackground.Enabled = true;
            }
            else
            {
                ribbonEdition.Enabled = false;
                ribbonOutil.Enabled = false;
                ribbonBackground.Enabled = false;
            }
        }

        private void tileViewVerset_DoubleClick(object sender, EventArgs e)
        {
            List<BibleVerse> lstBible =
            [
                new BibleVerse { Parole = tileViewVerset.GetRowCellValue(tileViewVerset.FocusedRowHandle, tileViewVerset.Columns["Parole"]).ToString(), verse = "" }
            ];
            gridParole.DataSource = lstBible;
        }

        private void separateDigitString(string input, string field_name)
        {
            string[] numbers = Regex.Split(input, @"\D+");
            string[] livre = Regex.Split(input, @"\d+");

            foreach (string value in livre)
            {
                if (value.Length > 3)
                {
                    string boky = input.IndexOf(numbers[0]) == 0 ? $"{numbers[0]} {value.Trim()}" : value.Trim();

                    for (int i = 0; i < dgvBook.DataRowCount; i++)
                    {
                        string dataInCell = Convert.ToString(dgvBook.GetRowCellValue(i, field_name));
                        if (dataInCell.Trim().ToLower().Contains(boky.Trim().ToLower()))
                        {
                            dgvBook.FocusedRowHandle = i;
                            break;
                        }
                    }

                    int count = 0;
                    int verse = 0;
                    foreach (string item in numbers)
                    {
                        if (input.IndexOf(item) > 0)
                        {
                            if (count == 0)
                            {
                                listChapter.SelectedIndex = int.Parse(item) - 1;
                                count++;
                            }
                            else if (count == 1)
                            {
                                verse = int.Parse(item);
                                listVerse.SelectedIndex = int.Parse(item) - 1;
                                count++;
                            }
                            else
                            {
                                listVerse.SelectedIndex = -1;

                                for (int i = verse; i <= int.Parse(item); i++)
                                {
                                    listVerse.SetSelected(i - 1, true);
                                }
                            }
                        }
                    }
                }
            }
        }

        private void iShowSidebar_ItemClick(object sender, ItemClickEventArgs e)
        {

            iShowSidebar.Caption = fihirana.HideShowSidebar() ? "Fermer l'index thématique" : "Afficher l'index thématique";
        }

        private void iClear_ItemClick(object sender, ItemClickEventArgs e)
        {
            for (int i = 0; i < dgvAgenda.RowCount; i++)
            {
                dgvAgenda.DeleteRow(i);
            }

        }

        private void iDelAgenda_ItemClick(object sender, ItemClickEventArgs e)
        {
            GridView view = dgvAgenda;
            int rowhandle = view.FocusedRowHandle;
            dgvAgenda.DeleteRow(rowhandle);
        }

        private void fileSaveItem_ItemClick(object sender, ItemClickEventArgs e)
        {
            fihirana.SaveChanges();
            AlertInfo alert = new AlertInfo("Information", "La modification du chant a été complétée avec succès.");
            alert.ImageOptions.SvgImage = svgImageCollection[0];
            alertControl.Show(this, alert);
        }

        private void iChangeBackground_ItemClick(object sender, ItemClickEventArgs e)
        {
            System.Windows.Forms.OpenFileDialog xOpen = new System.Windows.Forms.OpenFileDialog
            {
                RestoreDirectory = true,
                Multiselect = false,
                Title = "Changer de fond",
                DefaultExt = "jpg",

                Filter = "Fichiers Image |*.jpg;*.jpeg"
            };

            if (xOpen.ShowDialog() == DialogResult.OK)
            {
                string getFileName = $"{xOpen.FileName}";
                //getFileName = getFileName.Replace("\\", "/");
                DisplayForm.ImagePath = $"{getFileName}";
                saveSettings(getFileName, 6);
                ChangeImageBackground?.Invoke(sender, e);
            }
        }

        private void iAddVideo_ItemClick(object sender, ItemClickEventArgs e)
        {
            System.Windows.Forms.OpenFileDialog xOpen = new System.Windows.Forms.OpenFileDialog
            {
                RestoreDirectory = true,
                Multiselect = false,
                Title = "Changer de fond",
                DefaultExt = "mp4",

                Filter = "Fichiers video |*.mp4"
            };

            if (xOpen.ShowDialog() == DialogResult.OK)
            {
                string getFileName = $"{xOpen.FileName}";
                //getFileName = getFileName.Replace("\\", "/");
                DisplayForm.VideoPath = $"{getFileName}";
                saveSettings(getFileName, 7);

                ChangeVideoBackground?.Invoke(sender, e);
            }
        }
        private void iMode_ItemClick(object sender, ItemClickEventArgs e)
        {
            UserLookAndFeel.Default.SetSkinStyle(ClassSettings.SwitchMode(true));
            changeColorSkin?.Invoke(null, null);
        }
    }
}
