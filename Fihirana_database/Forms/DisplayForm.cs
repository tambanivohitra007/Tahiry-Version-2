// ------------------------------------------------------------
// Author: Rindra Razafinjatovo
// Created on: 2018
// Last Modified: Dec 2024
// Project: Tahiry
// Description: A collection of Bible and Hymnals to streamline and enhance worship presentations for churches.
// ------------------------------------------------------------

﻿using DevExpress.Data.Filtering;
using DevExpress.Xpo;
using Fihirana_database.Classes;
using Fihirana_database.fihirana;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Fihirana_database.Forms
{
    public partial class DisplayForm : Form
    {
        #region Variable Declarations

        // Delegate to send messages
        public delegate void Message(string str);

        // Static properties for display settings and states
        public static int DisplayTab { get; set; }
        private static string message;
        private bool isBibleOrSong = false; // False means song, true means Bible

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

        // Events for user interaction
        public static event KeyEventHandler PreviousOrNext;
        public static event KeyEventHandler CloseProjection;

        #endregion

        public DisplayForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Asynchronously initializes the WebView component.
        /// </summary>
        private async Task InitializeAsync() => await webView.EnsureCoreWebView2Async(null);

        /// <summary>
        /// Configures WebView settings to disable default browser features.
        /// </summary>
        public void InitBrowser()
        {
            webView.CoreWebView2.Settings.AreBrowserAcceleratorKeysEnabled = false;
            webView.CoreWebView2.Settings.AreDefaultContextMenusEnabled = false;
            //webView.CoreWebView2.Settings.AreDevToolsEnabled = true;

        }

        /// <summary>
        /// Handles keyboard shortcuts for closing or navigating the display.
        /// </summary>
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
        /// Paints the Adventist logo on the display panel.
        /// </summary>
        private void panelLogo_Paint(object sender, PaintEventArgs e)
        {
            logoSidebar.Visible = false;
            Rectangle rect = new Rectangle((webView.Width - 450) / 2, (webView.Height - 450) / 2, 400, 380);
            Bitmap bitmap = new Bitmap(Properties.Resources.adventist_symbol__white);
            e.Graphics.DrawImage(bitmap, rect);
        }

        #region JavaScript Integration

        /// <summary>
        /// Updates font weight and style dynamically in the display.
        /// </summary>
        private async Task checkFontWeightStyle()
        {
            try
            {
                string isBold = ClassFihirana.IsBold ? "700" : "";
                string script = $"$('.grid').css('font-weight', '{isBold}');";
                _ = await webView.ExecuteScriptAsync(script);

                string isItalic = ClassFihirana.IsItalic ? "italic" : "";
                script = $"$('.grid').css('font-style', '{isItalic}');";
                _ = await webView.ExecuteScriptAsync(script);
            }
            catch
            {
                // Log or handle errors silently
            }
        }

        /// <summary>
        /// Sets the font family dynamically in the display.
        /// </summary>
        private async Task _Font(string fontName)
        {
            try
            {
                string script = $"$('.grid').css('font-family', '{fontName}');";
                _ = await webView.CoreWebView2.ExecuteScriptAsync(script);
            }
            catch
            {
                // Log or handle errors silently
            }
        }

        #endregion

        /// <summary>
        /// Cleans up resources when the form is closed.
        /// </summary>
        private void DisplayForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            CloseProjection?.Invoke(sender, null);
            webView.Dispose();
        }

        /// <summary>
        /// Initializes the display form and loads the appropriate resources.
        /// </summary>
        private async void DisplayForm_Load(object sender, EventArgs e)
        {
            await InitializeAsync();
            InitBrowser();

            string basePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"source");
            string contentType = DisplayTab == 0 ? "song" : DisplayTab == 1 ? "bible" : null;

            if (contentType != null)
            {
                LoadContent(basePath, contentType);
            }

            InitMainFormHandlers();
        }

        private void LoadContent(string basePath, string contentType)
        {
            string htmlSource = Path.Combine(basePath, contentType, "index.html");

            List<string> cssFiles = new List<string>
            {
                Path.Combine(basePath, $@"{contentType}\css", "animate.min.css"),
                Path.Combine(basePath, $@"{contentType}\css", "style.css"),
            };

                    List<string> jsFiles = new List<string>
            {
                Path.Combine(basePath, $@"{contentType}\js", "jquery.min.js"),
                Path.Combine(basePath, $@"{contentType}\js", "textFit.min.js"),
                Path.Combine(basePath, contentType, "app.js"),
            };

            LoadSource(htmlSource, cssFiles, jsFiles);
        }


        /// <summary>
        /// Initializes event handlers for MainForm interactions.
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

            MainForm.SetColor += (s, e) =>
                webView.ExecuteScriptAsync($"_Color({Couleur[0]}, {Couleur[1]}, {Couleur[2]})");

            MainForm.clickRefresh += async (s, e) =>
            {
                try
                {                    
                    if (webView.CoreWebView2 != null)
                    {
                        Lyrics = Lyrics.Replace("\n", "<br>").Replace("\"", "'");
                        if (MainForm.isSongOrBible)
                        {
                            _ = await webView.CoreWebView2.ExecuteScriptAsync($"_Lyrics(\"{Lyrics}\", \"{Animation}\", true)");
                        }
                        else
                        {
                            string[] parts = Lyrics.Split(new[] { "<br>" }, StringSplitOptions.None);
                            _ = await webView.CoreWebView2.ExecuteScriptAsync($"_BibleVerse(\"{parts[2]}\", \"{parts[0]}\", \"{Animation}\")");
                        }
                    }
                }
                catch
                {
                    // Log or handle errors silently
                }
            };
        }

        /// <summary>
        /// Changes text alignment dynamically in the display.
        /// </summary>
        private void changeAlignement(int index)
        {
            string alignment = index switch
            {
                1 => "left",
                2 => "right",
                3 => "center",
                _ => ""
            };
            _ = webView.CoreWebView2.ExecuteScriptAsync($"_Alignment(\"{alignment}\")");
        }

        /// <summary>
        /// Sets the background image for the display.
        /// </summary>
        private async void changeImage(string imagePath)
        {
            try
            {
                if (!string.IsNullOrEmpty(imagePath) && !imagePath.Equals("none"))
                {
                    byte[] imageArray = File.ReadAllBytes(imagePath);
                    string base64Image = "data:image/jpeg;base64," + Convert.ToBase64String(imageArray);
                    _ = await webView.CoreWebView2.ExecuteScriptAsync($"_Image(\"{base64Image}\")");
                }
                else
                {
                    _ = await webView.CoreWebView2.ExecuteScriptAsync($"_Image(\"{imagePath}\")");
                }
            }
            catch
            {
                // Log or handle errors silently
            }
        }

        /// <summary>
        /// Sets the background video for the display.
        /// </summary>
        private async void changeVideo(string videoPath)
        {
            try
            {
                if (!string.IsNullOrEmpty(videoPath))
                {
                    byte[] videoArray = File.ReadAllBytes(videoPath);
                    string base64Video = Convert.ToBase64String(videoArray);
                    _ = await webView.CoreWebView2.ExecuteScriptAsync($"_Video(\"data:video/mp4;base64,{base64Video}\")");
                }
            }
            catch
            {
                // Log or handle errors silently
            }
        }

        /// <summary>
        /// Loads HTML, CSS, and JavaScript resources into the display.
        /// </summary>
        /// <summary>
        /// Loads the HTML source into the WebView2 and dynamically injects CSS and JavaScript.
        /// </summary>
        /// <param name="source">The base HTML source string.</param>
        /// <param name="css">Additional CSS to inject.</param>
        /// <param name="js">Additional JavaScript to inject.</param>
        //public void LoadSource(string source, string css, string js)
        //{
        //    if (string.IsNullOrWhiteSpace(source))
        //    {
        //        MessageBox.Show("HTML source cannot be null or empty.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        return;
        //    }

        //    try
        //    {
        //        // Ensure WebView2 is ready
        //        if (webView.CoreWebView2 == null)
        //        {
        //            MessageBox.Show("WebView2 is not initialized.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //            return;
        //        }

        //        // Load required resources from project resources
        //        string animateCss = Properties.Resources.animate_min_css;
        //        string jqueryJs = Properties.Resources.jquery_min_js;
        //        string textFitJs = Properties.Resources.textFit_min_js;

        //        // Inject CSS and JS into the HTML source
        //        source = source.Replace("</head>", $"<style>{animateCss}</style></head>")
        //                       .Replace("</head>", $"<style>{css}</style></head>")
        //                       .Replace("</body>", $"<script>{jqueryJs}</script></body>")
        //                       .Replace("</body>", $"<script>{textFitJs}</script></body>")
        //                       .Replace("</body>", $"<script>{js}</script></body>");

        //        // Navigate to the modified HTML string
        //        webView.CoreWebView2.NavigateToString(source);

        //        // Call settings initialization if required
        //        initSettings();
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show($"Failed to load source: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //}

        public async void LoadSource(string htmlFilePath, List<string> cssFiles, List<string> jsFiles, string customJsFilePath = null)
        {
            if (string.IsNullOrWhiteSpace(htmlFilePath) || !File.Exists(htmlFilePath))
            {
                MessageBox.Show("HTML file cannot be null or empty and must exist.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                // Ensure WebView2 is ready
                if (webView.CoreWebView2 == null)
                {
                    MessageBox.Show("WebView2 is not initialized.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Read the HTML source from the file
                string source = File.ReadAllText(htmlFilePath);

                // Build the CSS block dynamically
                var cssBlock = new StringBuilder();
                foreach (var cssFile in cssFiles)
                {
                    if (File.Exists(cssFile))
                    {
                        string cssContent = File.ReadAllText(cssFile);
                        cssBlock.AppendLine($"<style>{cssContent}</style>");
                    }
                    else
                    {
                        MessageBox.Show($"CSS file not found: {cssFile}", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }

                // Build the JavaScript block dynamically
                var jsBlock = new StringBuilder();
                foreach (var jsFile in jsFiles)
                {
                    if (File.Exists(jsFile))
                    {
                        string jsContent = File.ReadAllText(jsFile);
                        jsBlock.AppendLine($"<script>{jsContent}</script>");
                    }
                    else
                    {
                        MessageBox.Show($"JavaScript file not found: {jsFile}", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }

                // Add custom JavaScript if provided
                if (!string.IsNullOrWhiteSpace(customJsFilePath) && File.Exists(customJsFilePath))
                {
                    string customJsContent = File.ReadAllText(customJsFilePath);
                    jsBlock.AppendLine($"<script>{customJsContent}</script>");
                }

                // Inject CSS and JavaScript into the HTML source
                source = source.Replace("</head>", $"{cssBlock}</head>")
                               .Replace("</body>", $"{jsBlock}</body>");

                // Navigate to the modified HTML string
                webView.CoreWebView2.NavigateToString(source);

                // Call settings initialization if required
                await initSettings();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to load source: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private Dictionary<int, string> cachedSettings;
        private string currentImage = string.Empty; // Tracks the currently displayed image
        private string currentVideo = string.Empty; // Tracks the currently displayed video

        private void CacheSettings()
        {
            cachedSettings = new Dictionary<int, string>();
            for (int i = 1; i <= 9; i++) // Assuming 9 settings
            {
                var setting = loadSetting(i);
                cachedSettings[i] = setting?.Value;
            }
        }

        /// <summary>
        /// Loads and applies settings for the display from the database.
        /// </summary>
        private async Task initSettings()
        {
            if (webView.CoreWebView2 == null) return;

            try
            {
                // Cache settings
                CacheSettings();

                // Prepare script for combined execution
                string alignment = cachedSettings[2] switch
                {
                    "1" => "left",
                    "2" => "right",
                    _ => "center"
                };

                string isBold = cachedSettings[4] == "True" ? "700" : "";
                string isItalic = cachedSettings[5] == "True" ? "italic" : "";

                string colorScript = string.Empty;
                if (!string.IsNullOrEmpty(cachedSettings[9]))
                {
                    string[] colors = cachedSettings[9].Split('-');
                    Couleur[0] = int.Parse(colors[0]);
                    Couleur[1] = int.Parse(colors[1]);
                    Couleur[2] = int.Parse(colors[2]);
                    colorScript = $"_Color({Couleur[0]}, {Couleur[1]}, {Couleur[2]});";
                }

                string script = $@"
                    _Alignment('{alignment}');
                    $('.grid').css('font-weight', '{isBold}');
                    $('.grid').css('font-style', '{isItalic}');
                    {colorScript}
                ";
                await webView.CoreWebView2.ExecuteScriptAsync(script);

                // Image and video updates
                if (!string.IsNullOrEmpty(cachedSettings[5]) && DisplayTab == 0)
                {
                    if (!currentImage.Equals(cachedSettings[5]))
                    {
                        changeImage(cachedSettings[5]);
                        currentImage = cachedSettings[5];
                    }
                }

                if (!string.IsNullOrEmpty(cachedSettings[6]) && DisplayTab == 0)
                {
                    if (!currentVideo.Equals(cachedSettings[6]))
                    {
                        changeVideo(cachedSettings[6]);
                        currentVideo = cachedSettings[6];
                    }
                }

                // Update UI
                logoSidebar.Visible = isHide;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error in initSettings: {ex.Message}");
            }
        }


        /// <summary>
        /// Retrieves settings from the database based on the index.
        /// </summary>
        private Settings loadSetting(int index)
        {
            try
            {
                Session session = new Session { ConnectionString = XpoDefault.ConnectionString };
                return session.FindObject<Settings>(CriteriaOperator.Parse($"ID = {index}"));
            }
            catch
            {
                return null;
            }
        }
    }

}