using DevExpress.XtraSplashScreen;
using System;

namespace Fihirana_database
{
    public partial class Splash : SplashScreen
    {
        public Splash()
        {
            InitializeComponent();
        }
        #region Overrides

        public override void ProcessCommand(Enum cmd, object arg)
        {
            base.ProcessCommand(cmd, arg);
        }

        #endregion Overrides
    }
}