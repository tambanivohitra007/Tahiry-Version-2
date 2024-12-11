namespace Fihirana_database.Forms
{
    public partial class LockForm : DevExpress.XtraEditors.XtraForm
    {
        public LockForm()
        {
            InitializeComponent();
        }

        private void PassText_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (string.IsNullOrEmpty(PassText.Text)) return;
        }
    }
}