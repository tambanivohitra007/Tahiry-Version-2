namespace Fihirana_database
{
    public partial class AboutForm : DevExpress.XtraEditors.XtraForm
    {
        public AboutForm()
        {
            InitializeComponent();

            infoLbl.Text = "Ce logiciel n'est plus soutenu par le propriétaire<br>" +
                     $"Ver. 2.0.0.3 <size=11><br>" +
                     "Pour plus d'information, veuillez contacter l'email suivant<br>" +
                     "<br><b>Email:</b> rindra.it @gmail.com<br>";
            infoLbl.AllowHtmlString = true;
            infoLbl.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            infoLbl.Appearance.Options.UseTextOptions = true;
            infoLbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.Vertical;
        }
    }
}