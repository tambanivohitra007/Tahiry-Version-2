using DevExpress.Data.Filtering;
using DevExpress.Xpo;
using Fihirana_database.Classes;
using Fihirana_database.fihirana;
using System;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Fihirana_database.Forms
{
    public partial class DisplayForm : Form
    {

        #region variables declarations
        public delegate void Message(string str);
        public static int DisplayTab { get; set; }
        private static string message;
        private bool isBibleOrSong = false; //False means song
        public static string Lyrics { get; set; }
        public static string Title { get; set; }
        public static string Category { get; set; }
        public static long Number { get; set; }
        public static bool isHide { get; set; } = false;
        public static string Animation { get; set; }
        public static string FontFamily { get; set; }
        public static int FontSize { get; set; }
        public static string ImagePath { get; set; }
        public static string VideoPath { get; set; }
        public static int[] Couleur { get; set; } = new int[3];
        public static event KeyEventHandler PreviousOrNext;
        public static event KeyEventHandler CloseProjection;
        #endregion
        public DisplayForm()
        {
            InitializeComponent();
        }

        private async Task InitializeAsync() => await webView.EnsureCoreWebView2Async(null);

        public void InitBrowser()
        {
            webView.CoreWebView2.Settings.AreBrowserAcceleratorKeysEnabled = false;
            webView.CoreWebView2.Settings.AreDefaultContextMenusEnabled = false;
        }

        /// <summary>
        /// Keystroke to be used when closing the display
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DisplayForm_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Escape || (e.Modifiers == Keys.Alt && e.KeyCode == Keys.F4))
            {
                CloseProjection?.Invoke(null, null);
                webView.Dispose();
                Close();
            }
            else
            {
                PreviousOrNext?.Invoke(sender, e);
            }

        }

        /// <summary>
        /// Paint the Adventist Logo to the display
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void panelLogo_Paint(object sender, PaintEventArgs e)
        {
            logoSidebar.Visible = false;
            Rectangle rect = new Rectangle((webView.Width - 450) / 2, (webView.Height - 450) / 2, 400, 380);
            Bitmap bitmap = new Bitmap(Properties.Resources.adventist_symbol__white);
            e.Graphics.DrawImage(bitmap, rect);
        }

        #region javascript

        /// <summary>
        /// Handle Font weight and Font style of the display
        /// </summary>
        /// <returns></returns>
        private async Task checkFontWeightStyle()
        {
            try
            {
                //bold
                string isBold = ClassFihirana.IsBold ? "700" : "";
                string script = $@" $('.grid').css('font-weight', '{isBold}');";
                _ = await webView.ExecuteScriptAsync(script);

                //italic

                string isItalic = ClassFihirana.IsItalic ? "italic" : "";
                script = $@" $('.grid').css('font-style', '{isItalic}');";
                _ = await webView.ExecuteScriptAsync(script);
            }
            catch { }
        }

        private async Task _Font(string fontName)
        {
            try
            {
                string script = $@"$('.grid').css('font-family', '{fontName}');";
                _ = await webView.CoreWebView2.ExecuteScriptAsync(script);
            }
            catch { }
        }

        #endregion

        /// <summary>
        /// When the display closed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DisplayForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            CloseProjection?.Invoke(sender, null);
            webView.Dispose();
        }

        /// <summary>
        /// When the Display loads, 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void DisplayForm_Load(object sender, EventArgs e)
        {

            await InitializeAsync();
            InitBrowser();
            if (DisplayTab == 0)
                LoadSource(
                   Properties.Resources.index,
                   Properties.Resources.style,
                   Properties.Resources.app
               );
            if (DisplayTab == 1)
                LoadSource(
                    Properties.Resources.index_bible_html,
                    Properties.Resources.style_bible_css,
                    Properties.Resources.app_bible
                );
            InitMainFormHandlers();
            initSettings();
        }

        /// <summary>
        /// Initialize Mainform handlers
        /// </summary>
        private void InitMainFormHandlers()
        {
            MainForm.closeProjection += (s, e) =>
            {
                webView.Dispose();
                Close();
            };
            MainForm.ChangeFont += async (s, e) =>
            {
                if (!ClassFihirana.isFontReady)
                {
                    await _Font(FontFamily);
                    webView.Refresh();
                }
            };
            MainForm.GoNear += (s, e) => changeAlignement(1);
            MainForm.GoFar += (s, e) => changeAlignement(2);
            MainForm.GoCenter += (s, e) => changeAlignement(3);
            MainForm.GoBold += async (s, e) => await checkFontWeightStyle();
            MainForm.GoItalic += async (s, e) => await checkFontWeightStyle();
            MainForm.Increase += (s, e) => webView.ZoomFactor += 0.05;
            MainForm.Decrease += (s, e) => webView.ZoomFactor -= 0.05;
            MainForm.ChangeImageBackground += (s, e) => changeImage(ImagePath);
            MainForm.ChangeVideoBackground += (s, e) => changeVideo(VideoPath);
            MainForm.HideHeader += (s, e) => logoSidebar.Visible = isHide;
            //MainForm.SetTitle += (s, e) => showInfo(isHide);
            MainForm.BlackScreen += (s, e) => webView.Visible = !LogClass.isBlackScreen;
            MainForm.showLogo += (s, e) =>
            {
                if (LogClass.showLogo)
                {
                    webView.Visible = false;
                    panelLogo.Dock = DockStyle.Fill;
                    panelLogo.Visible = true;
                }
                else
                {
                    panelLogo.Dock = DockStyle.None;
                    panelLogo.Visible = false;
                    webView.Visible = true;
                }

            };
            MainForm.SetColor += (s, e) => webView.ExecuteScriptAsync($"_Color({Couleur[0]}, {Couleur[1]}, {Couleur[2]})");
            MainForm.clickRefresh += async (s, el) =>
            {
                try
                {
                    //await InitializeAsync();
                    initSettings();
                    if (webView.CoreWebView2 != null)
                    {
                        if (MainForm.isSongOrBible)
                        {
                            Lyrics = Lyrics.Replace("\n", "<br>");
                            Lyrics = Lyrics.Replace("\"", "'");
                            _ = await webView.CoreWebView2.ExecuteScriptAsync($"_Lyrics(\"{Lyrics}\", \"{Animation}\", true)");
                        }
                        else
                        {
                            Lyrics = Lyrics.Replace("\n", "<br>");
                            Lyrics = Lyrics.Replace("\"", "'");
                            string delimiter = "<br>";
                            string[] parts = Lyrics.Split(new string[] { delimiter }, StringSplitOptions.None);

                            _ = await webView.CoreWebView2.ExecuteScriptAsync($"_BibleVerse(\"{parts[2]}\", \"{parts[0]}\", \"{Animation}\")");
                        }
                    }

                }
                catch { }
            };
            MainForm.SelectBackground += (s, e) =>
            {
                try
                {
                    if (webView.CoreWebView2 != null)
                    {
                        //await InitializeAsync();
                        switch (DisplayTab)
                        {
                            case 0:
                                LoadSource(
                                    Properties.Resources.index,
                                    Properties.Resources.style,
                                    Properties.Resources.app
                                );
                                break;
                            case 1:
                                LoadSource(
                                    Properties.Resources.index_bible_html,
                                    Properties.Resources.style_bible_css,
                                    Properties.Resources.app_bible
                                );
                                break;
                            default:
                                break;
                        }
                    }

                }
                catch { }

            };
        }
        private void changeAlignement(int index)
        {
            switch (index)
            {
                case 1: _ = webView.ExecuteScriptAsync($"_Alignment(\"left\")"); break;
                case 2: _ = webView.ExecuteScriptAsync($"_Alignment(\"right\")"); break;
                case 3: _ = webView.ExecuteScriptAsync($"_Alignment(\"center\")"); break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Set image as background
        /// </summary>
        /// <param name="imagePath"></param>
        private async void changeImage(string imagePath)
        {

            try
            {
                string img = imagePath;

                if (!string.IsNullOrEmpty(imagePath) && !imagePath.Equals("none"))
                {
                    // Convert image to Base64
                    byte[] imageArray = File.ReadAllBytes(imagePath);
                    string base64Image = "data:image/jpeg;base64," + Convert.ToBase64String(imageArray);
                    img = base64Image;
                }
                else
                    img = imagePath;

                _ = await webView.CoreWebView2.ExecuteScriptAsync($"_Image(\"{img}\")");
            }
            catch { }
        }

        /// <summary>
        /// Set video as background
        /// </summary>
        /// <param name="videoPath"></param>
        private async void changeVideo(string videoPath)
        {
            try
            {
                if (videoPath != string.Empty)
                {
                    byte[] videoArray = File.ReadAllBytes(videoPath);
                    string base64VideoRepresentation = Convert.ToBase64String(videoArray);
                    _ = await webView.ExecuteScriptAsync($"_Video(\"data:video/mp4;base64,{base64VideoRepresentation}\")");
                }
                else
                {
                    _ = await webView.ExecuteScriptAsync($"_Video(\"{string.Empty}\")");
                }
            }
            catch
            {
                //MessageBox.Show("The file is too big to be used. Please find another file.", "ERROR",
                //    MessageBoxButtons.OK, MessageBoxIcon.Error);
                //return;
            }
        }

        /// <summary>
        /// Load source to be displayed
        /// </summary>
        /// <param name="source">HTML file</param>
        /// <param name="css">related css</param>
        /// <param name="js">related javascript</param>
        public void LoadSource(string source, string css, string js)
        {
            isBibleOrSong = false;

            string animate_css = Properties.Resources.animate_min_css;
            string jquery_js = Properties.Resources.jquery_min_js;
            string textFit_js = Properties.Resources.textFit_min_js;

            // Combine all content into the HTML
            source = source.Replace("</head>", $"<style>{animate_css}</style></head>");
            source = source.Replace("</head>", $"<style>{css}</style></head>");
            source = source.Replace("</body>", $"<script>{jquery_js}</script></body>");
            source = source.Replace("</body>", $"<script>{textFit_js}</script></body>");
            source = source.Replace("</body>", $"<script>{js}</script></body>");
            // Load modified HTML content into WebView2 control
            webView.CoreWebView2.NavigateToString(source);

            initSettings();
        }

        /// <summary>
        /// Load settings from the database 
        /// </summary>

        private async void initSettings()
        {
            try
            {
                //alignement
                Settings sess = loadSetting(2);
                string valiny = !string.IsNullOrEmpty(sess.Value) ? sess.Value : "";
                valiny = valiny == "1" ? "left" : valiny == "2" ? "right" : "center";
                _ = await webView.CoreWebView2.ExecuteScriptAsync($"_Alignment('{valiny}')");

                //Font
                sess = loadSetting(3);
                valiny = !string.IsNullOrEmpty(sess.Value) ? sess.Value : "";
                await _Font(valiny);

                //bold
                sess = loadSetting(4);
                valiny = !string.IsNullOrEmpty(sess.Value) ? sess.Value : "";
                string isBold = valiny == "True" ? "700" : "";
                string script = $@" $('.grid').css('font-weight', '{isBold}');";
                _ = await webView.CoreWebView2.ExecuteScriptAsync(script);

                //italic
                sess = loadSetting(5);
                valiny = !string.IsNullOrEmpty(sess.Value) ? sess.Value : "";
                string isItalic = valiny == "True" ? "italic" : "";
                script = $@" $('.grid').css('font-style', '{isItalic}');";
                _ = await webView.CoreWebView2.ExecuteScriptAsync(script);

                //image
                sess = loadSetting(6);
                valiny = !string.IsNullOrEmpty(sess.Value) && DisplayTab == 0 ? sess.Value : "";
                changeImage(valiny);

                //video
                sess = loadSetting(7);
                valiny = !string.IsNullOrEmpty(sess.Value) && DisplayTab == 0 ? sess.Value : "";
                changeVideo(valiny);

                //titlie heading
                logoSidebar.Visible = isHide;

                //couleur
                sess = loadSetting(9);
                valiny = !string.IsNullOrEmpty(sess.Value) ? sess.Value : "";

                string[] couleurs = valiny.Split('-');
                Couleur[0] = Convert.ToInt32(couleurs[0]);
                Couleur[1] = Convert.ToInt32(couleurs[1]);
                Couleur[2] = Convert.ToInt32(couleurs[2]);
                _ = await webView.CoreWebView2.ExecuteScriptAsync($"_Color({Couleur[0]}, {Couleur[1]}, {Couleur[2]})");
            }
            catch { }
        }

        /// <summary>
        /// Retrieve particular settings based on its index
        /// </summary>
        /// <param name="index">Index of the setting</param>
        /// <returns>Sesttings</returns>
        private Settings loadSetting(int index)
        {
            try
            {
                Session session = new Session
                {
                    ConnectionString = XpoDefault.ConnectionString
                };
                return session.FindObject<Settings>(CriteriaOperator.Parse($"ID = {index}")); ;

            }
            catch
            {
                return null;
            }
        }
    }
}
