using DevExpress.Utils;
using DevExpress.Xpo;
using DevExpress.XtraEditors;
using DevExpress.XtraRichEdit;
using DevExpress.XtraRichEdit.API.Native;
using DevExpress.XtraRichEdit.Commands;
using Fihirana_database.Classes;
using Fihirana_database.fihirana;
using System;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Fihirana_database
{
    public partial class AddSongForm : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        private Hymnal datasource;
        private bool edit;

        // Constructor for adding a new song
        public AddSongForm()
        {
            InitializeComponent();
            comboCategorie.SelectedIndex = 1;
            edit = false;
            RefreshForm();
            spinNumber.Properties.MinValue = GetMaxCategoryNumber(comboCategorie.SelectedIndex) + 1;
        }

        // Constructor for editing an existing song
        public AddSongForm(Hymnal hymn)
        {
            InitializeComponent();
            datasource = hymn;
            _ = txtTitle.DataBindings.Add("Text", datasource, "Title");
            spinNumber.Value = datasource.Number;
            _ = txtClef.DataBindings.Add("Text", datasource, "Key");
            richEdit.RtfText = datasource.Lyrics;
            title.Text = datasource.Title;
            edit = true;
            RefreshForm();
            comboCategorie.SelectedIndex = (int)datasource.Category - 1;
            number.Text = datasource.Number.ToString();
            spinNumber.Value = datasource.Number;
        }

        // Get the maximum number in the selected category
        private int GetMaxCategoryNumber(int index)
        {
            return ClassFihirana.SessionHymnal.Query<Hymnal>()
                .Where(x => x.Category == index + 1)
                .Max(x => (int?)x.Number) ?? 0;
        }

        // Event handler for exit button click
        private void iExit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Close();
        }

        // Event handler for verse button click
        private void iVerse_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Regex expr = new Regex("[0-9]");
            DocumentRange[] found = richEdit.Document.FindAll(expr, richEdit.Document.Range);

            foreach (DocumentRange item in found)
            {
                CharacterProperties cp2 = richEdit.Document.BeginUpdateCharacters(item);
                cp2.FontSize = 14;
                cp2.ForeColor = Color.Orange;
                cp2.BackColor = Color.Snow;
                richEdit.Document.EndUpdateCharacters(cp2);
            }
        }

        // Event handler for flyout panel button click
        private void flyoutP_ButtonClick(object sender, FlyoutPanelButtonClickEventArgs e)
        {
            if (e.Button.Tag.ToString() == "0")
            {
                (sender as FlyoutPanel).HidePopup();
            }
            else if (e.Button.Tag.ToString() == "1")
            {
                title.Text = txtTitle.Text;
                category.Text = comboCategorie.EditValue.ToString();
                number.Text = spinNumber.Text;
                (sender as FlyoutPanel).HidePopup();
            }
        }

        // Event handler for select list item click
        private void iSelect_ListItemClick(object sender, DevExpress.XtraBars.ListItemClickEventArgs e)
        {
            DocumentRange selection = richEdit.Document.Selection;
            if (iSelect.ItemIndex == 0)
            {
                txtTitle.Text = richEdit.Document.GetText(selection);
            }
            else if (iSelect.ItemIndex == 1)
            {
                spinNumber.Text = richEdit.Document.GetText(selection);
            }
            richEdit.Document.Delete(selection);
            title.Text = txtTitle.Text;
            number.Text = spinNumber.Text;
        }

        // Event handler for reset button click
        private void iReset_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ClearControls();
        }

        // Clear input controls
        private void ClearControls()
        {
            txtTitle.Text = string.Empty;
            txtClef.Text = string.Empty;
        }

        // Event handler for save button click
        private void iSaveDatabase_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ClearFormatting();

            if (comboCategorie.EditValue == null)
            {
                dxError.SetError(comboCategorie, "Veuillez renseigner ce champ! ");
                _ = comboCategorie.Focus();
                return;
            }

            if (string.IsNullOrEmpty(txtTitle.Text))
            {
                dxError.SetError(txtTitle, "Veuillez renseigner ce champ! ");
                _ = txtTitle.Focus();
                return;
            }

            if (!edit)
            {
                var existingHymn = ClassFihirana.SessionHymnal.Query<Hymnal>()
                    .FirstOrDefault(a => a.Number == (long)spinNumber.Value && a.Category == comboCategorie.SelectedIndex + 1);

                if (existingHymn != null)
                {
                    _ = XtraMessageBox.Show("Ce numéro est déjà utilisé.\nVeuillez enregistrer par un autre.", "ERREUR", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    _ = spinNumber.Focus();
                    return;
                }

                using Session session = new Session();
                Hymnal hm = new Hymnal(session)
                {
                    Number = int.Parse(spinNumber.Text),
                    Title = txtTitle.Text,
                    Category = comboCategorie.SelectedIndex + 1,
                    Lyrics = richEdit.Document.RtfText,
                    Key = txtClef.Text
                };
                hm.Save();
                title.Text = txtTitle.Text;
                number.Text = spinNumber.Text;
                richEdit.RtfText = string.Empty;
                ClearControls();
            }
            else
            {
                _ = EditValue();
                DialogResult = DialogResult.OK;
                Close();
            }
        }

        // Clear formatting in the rich edit control
        private void ClearFormatting()
        {
            CharacterProperties cp = richEdit.Document.BeginUpdateCharacters(richEdit.Document.Range);
            cp.Reset(CharacterPropertiesMask.All);
            cp.Style = richEdit.Document.CharacterStyles[0];
            richEdit.Document.EndUpdateCharacters(cp);

            ParagraphProperties pp = richEdit.Document.BeginUpdateParagraphs(richEdit.Document.Range);
            pp.LineSpacingType = ParagraphLineSpacing.Single;
            pp.ContextualSpacing = false;
            pp.SpacingAfter = 0;
            pp.SpacingBefore = 1;
            pp.Alignment = ParagraphAlignment.Left;
            richEdit.Document.EndUpdateParagraphs(pp);

            _ = richEdit.Document.ReplaceAll(new Regex(@"[ ]{2,}"), string.Empty);
        }

        // Event handler for setting title from selection
        private void iAsTitle_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DocumentRange selection = richEdit.Document.Selection;
            string title = richEdit.Document.GetText(selection);
            richEdit.Document.Delete(selection);

            string[] words = title.Split('-');
            if (words.Length > 1)
            {
                txtTitle.Text = words[1];
                number.Text = words[0];
                spinNumber.Text = words[0];
            }
        }

        // Event handler for setting number from selection
        private void iAsNumber_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DocumentRange selection = richEdit.Document.Selection;
            spinNumber.Text = richEdit.Document.GetText(selection);
            richEdit.Document.Delete(selection);
            number.Text = spinNumber.Text;
        }

        // Event handler for category selection change
        private void comboCategorie_SelectedIndexChanged(object sender, EventArgs e)
        {
            category.Text = comboCategorie.EditValue.ToString();
            spinNumber.Value = GetMaxCategoryNumber(comboCategorie.SelectedIndex) + 1;
        }

        // Event handler for title text change
        private void txtTitle_EditValueChanged(object sender, EventArgs e)
        {
            title.Text = txtTitle.Text;
        }

        // Edit the current hymn values
        private Hymnal EditValue()
        {
            LogClass.hymn.Number = long.Parse(spinNumber.Value.ToString());
            LogClass.hymn.Category = comboCategorie.SelectedIndex + 1;
            LogClass.hymn.Title = txtTitle.Text;
            LogClass.hymn.Key = txtClef.Text;
            LogClass.hymn.Lyrics = richEdit.RtfText;

            return LogClass.hymn;
        }

        // Refresh the form data
        private void RefreshForm()
        {
            var categories = session.Query<Category>().Select(c => c.Description).ToList();
            comboCategorie.Properties.Items.AddRange(categories);
        }

        // Event handler for form activation
        private void AddSongForm_Activated(object sender, EventArgs e)
        {
            RefreshForm();
        }

        // Event handler for chorus button click
        private void iChorus_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            richEdit.CreateCommand(RichEditCommandId.InsertLineBreak).Execute();
        }

        // Event handler for spin number value change
        private void spinNumber_EditValueChanged(object sender, EventArgs e)
        {
            number.Text = spinNumber.Value.ToString();
        }

        // Event handler for generate button click
        private void iGenerate_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SelectCurrentLine(richEdit);
        }

        // Select the current line in the rich edit control
        private void SelectCurrentLine(RichEditControl richEditControl)
        {
            try
            {
                StartOfLineCommand startOfLineCommand = new StartOfLineCommand(richEditControl);
                EndOfLineCommand endOfLineCommand = new EndOfLineCommand(richEditControl);

                startOfLineCommand.Execute();
                int start = richEditControl.Document.CaretPosition.ToInt();
                endOfLineCommand.Execute();
                int length = richEditControl.Document.CaretPosition.ToInt() - start;

                DocumentRange range = richEditControl.Document.CreateRange(start, length + 1);
                txtTitle.Text = richEditControl.Document.GetText(range);
                richEditControl.Document.Delete(range);
            }
            catch { }
        }

        // Event handler for bulk button click
        private void iBulk_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                for (int i = 100; i < 521; i++)
                {
                    _ = richEdit.LoadDocument($@"C:\Users\rindr\Music\lyrics\D{i}.txt");
                    SelectCurrentLine(richEdit);
                    spinNumber.Value = i;

                    var existingHymn = ClassFihirana.SessionHymnal.Query<Hymnal>()
                        .FirstOrDefault(a => a.Number == (long)spinNumber.Value && a.Category == comboCategorie.SelectedIndex + 1);

                    if (existingHymn != null)
                    {
                        _ = XtraMessageBox.Show("Ce numéro est déjà utilisé.\nVeuillez enregistrer par un autre.", "ERREUR", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        _ = spinNumber.Focus();
                        return;
                    }

                    using Session session = new Session();
                    Hymnal hm = new Hymnal(session)
                    {
                        Number = int.Parse(spinNumber.Text),
                        Title = txtTitle.Text,
                        Category = comboCategorie.SelectedIndex + 1,
                        Lyrics = richEdit.Document.RtfText,
                        Key = txtClef.Text
                    };
                    hm.Save();
                    title.Text = txtTitle.Text;
                    number.Text = spinNumber.Text;
                    richEdit.RtfText = string.Empty;
                    ClearControls();
                }
            }
            catch (Exception ex)
            {
                _ = MessageBox.Show($"Erreur: {ex.Message}");
            }
        }
    }

}