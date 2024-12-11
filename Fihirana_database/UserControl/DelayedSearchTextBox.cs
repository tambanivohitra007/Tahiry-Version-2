using DevExpress.XtraEditors;
using System;
using System.Timers;
using System.Windows.Forms;

namespace Fihirana_database.UserControl
{
    public partial class DelayedSearchTextBox : SearchControl
    {
        #region private globals

        private System.Timers.Timer DelayTimer; // used for the delay
        private bool TimerElapsed = false; // if true OnTextChanged is fired.
        private bool KeysPressed = false; // makes event fire immediately if it wasn't a keypress
        private int DELAY_TIME = 1000;

        #endregion

        #region object model

        // Delay property
        public int Delay
        {
            set { DELAY_TIME = value; }
        }

        #endregion

        #region ctor

        public DelayedSearchTextBox()
        {
            InitializeComponent();

            // Initialize Timer
            DelayTimer = new System.Timers.Timer(DELAY_TIME);
            DelayTimer.Elapsed += new ElapsedEventHandler(DelayTimer_Elapsed);
        }

        #endregion

        #region event handlers

        private void DelayTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            // stop timer.
            DelayTimer.Enabled = false;

            // set timer elapsed to true, so the OnTextChange knows to fire
            TimerElapsed = true;

            // use invoke to get back on the UI thread.
            _ = this.Invoke(new DelayOverHandler(DelayOver), null);
        }

        #endregion

        #region overrides

        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            if (!DelayTimer.Enabled)
                DelayTimer.Enabled = true;
            else
            {
                DelayTimer.Enabled = false;
                DelayTimer.Enabled = true;
            }

            KeysPressed = true;

            base.OnKeyPress(e);
        }

        protected override void OnTextChanged(EventArgs e)
        {
            // if the timer elapsed or text was changed by something besides a keystroke
            // fire base.OnTextChanged
            if (TimerElapsed || !KeysPressed)
            {
                TimerElapsed = false;
                KeysPressed = false;
                base.OnTextChanged(e);
            }
        }

        #endregion

        #region delegates

        public delegate void DelayOverHandler();

        #endregion

        #region private helpers

        private void DelayOver()
        {
            OnTextChanged(new EventArgs());
        }

        #endregion

    }
}
