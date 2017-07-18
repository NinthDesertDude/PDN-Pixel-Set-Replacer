namespace PixSetRepl
{
    /// <summary>
    /// Stores copies of any number of relevant environment parameters to be
    /// copied to and from the dialog.
    /// </summary>
    public class StaticSettings
    {
        /// <summary>
        /// Represents the result of the dialog.
        /// </summary>
        public enum DialogResult
        {
            /// <summary>
            /// Represents no meaningful result.
            /// </summary>
            Default,

            /// <summary>
            /// The first image was stored.
            /// </summary>
            StoringFirstImage,

            /// <summary>
            /// The second image was stored.
            /// </summary>
            StoringSecondImage,

            /// <summary>
            /// The replacement is applied.
            /// </summary>
            Replacing
        }

        #region Fields
        /// <summary>
        /// Represents the result of the dialog.
        /// </summary>
        public static DialogResult dialogResult;
        #endregion

        #region Constructors
        static StaticSettings()
        {
            dialogResult = DialogResult.Default;
        }
        #endregion
    }
}
