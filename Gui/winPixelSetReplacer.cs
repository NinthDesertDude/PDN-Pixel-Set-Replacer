using System;
using System.Drawing;
using System.Windows.Forms;
using PaintDotNet.Effects;

namespace PixSetRepl.Gui
{
    /// <summary>
    /// The dialog used for working with the effect.
    /// </summary>
    public partial class winPixelSetReplacer : EffectConfigDialog
    {
        #region Fields
        /// <summary>
        /// The bitmap to be replaced.
        /// </summary>
        public Bitmap bmpToReplace;

        /// <summary>
        /// The bitmap to replace the other bitmap. Must be the same size.
        /// </summary>
        public Bitmap bmpReplacing;
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes components and brushes.
        /// </summary>
        public winPixelSetReplacer()
        {
            InitializeComponent();
            bmpToReplace = new Bitmap(1, 1);
            bmpReplacing = new Bitmap(1, 1);
        }
        #endregion

        #region Methods (overridden)
        /// <summary>
        /// Configures settings so they can be stored between consecutive
        /// calls of the effect.
        /// </summary>
        protected override void InitialInitToken()
        {
            theEffectToken = new PersistentSettings(new Bitmap(1, 1), new Bitmap(1, 1), 5);
        }

        /// <summary>
        /// Sets up the GUI to reflect the previously-used settings; i.e. this
        /// loads the settings. Called twice by a quirk of Paint.NET.
        /// </summary>
        protected override void InitDialogFromToken(EffectConfigToken effectToken)
        {
            //Copies GUI values from the settings.
            PersistentSettings token = (PersistentSettings)effectToken;
            bmpToReplace = token.BmpToReplace;
            bmpReplacing = token.BmpReplacing;
            sliderTolerance.Value = token.Tolerance;
            lblTolerance.Text = "Tolerance: " + sliderTolerance.Value;
        }

        /// <summary>
        /// Overwrites the settings with the dialog's current settings so they
        /// can be reused later; i.e. this saves the settings.
        /// </summary>
        protected override void InitTokenFromDialog()
        {
            ((PersistentSettings)EffectToken).BmpToReplace = bmpToReplace;
            ((PersistentSettings)EffectToken).BmpReplacing = bmpReplacing;
            ((PersistentSettings)EffectToken).Tolerance = (byte)sliderTolerance.Value;
        }
        #endregion

        #region Methods (not event handlers)
        /// <summary>
        /// Sets/resets all persistent settings in the dialog to their default
        /// values.
        /// </summary>
        public void InitSettings()
        {
            InitialInitToken();
            InitDialogFromToken();
        }
        #endregion

        #region Methods (event handlers)
        /// <summary>
        /// Stores the image selected by the user.
        /// </summary>
        private void bttnImageToReplace_Click(object sender, EventArgs e)
        {
            StaticSettings.dialogResult = StaticSettings.DialogResult.StoringFirstImage;
            DialogResult = DialogResult.OK;
            FinishTokenUpdate();
            Close();
        }

        /// <summary>
        /// Stores the image to be replaced.
        /// </summary>
        private void bttnImageReplacing_Click(object sender, EventArgs e)
        {
            StaticSettings.dialogResult = StaticSettings.DialogResult.StoringSecondImage;
            DialogResult = DialogResult.OK;
            FinishTokenUpdate();
            Close();
        }

        /// <summary>
        /// Replaces all instances of the image to be replaced with the other.
        /// </summary>
        private void bttnReplace_Click(object sender, EventArgs e)
        {
            StaticSettings.dialogResult = StaticSettings.DialogResult.Replacing;
            DialogResult = DialogResult.OK;
            FinishTokenUpdate();
            Close();
        }

        /// <summary>
        /// Resets the existing bitmaps.
        /// </summary>
        private void bttnReset_Click(object sender, EventArgs e)
        {
            bmpToReplace = new Bitmap(1, 1);
            bmpReplacing = new Bitmap(1, 1);
            DialogResult = DialogResult.OK;
        }

        /// <summary>
        /// Updates the label and token when the tolerance is changed.
        /// </summary>
        private void sliderTolerance_ValueChanged(object sender, EventArgs e)
        {
            ((PersistentSettings)EffectToken).Tolerance =
                (byte)sliderTolerance.Value;

            lblTolerance.Text = "Tolerance: " + sliderTolerance.Value;
        }
        #endregion
    }
}