using System.Collections.Generic;
using System.Drawing;

namespace PixSetRepl
{
    /// <summary>
    /// Represents the settings used in the dialog so they can be stored and
    /// loaded when applying the effect consecutively for convenience.
    /// </summary>
    public class PersistentSettings : PaintDotNet.Effects.EffectConfigToken
    {
        #region Fields
        /// <summary>
        /// Contains the stored bitmap data.
        /// </summary>
        public Bitmap BmpToReplace
        {
            get;
            set;
        }

        /// <summary>
        /// Contains the replacing bitmap data.
        /// </summary>
        public Bitmap BmpReplacing
        {
            get;
            set;
        }

        /// <summary>
        /// Tolerance allows pixels that almost match to be considered a match.
        /// </summary>
        public byte Tolerance
        {
            get;
            set;
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Calls and sets up dialog settings to be stored.
        /// </summary>
        public PersistentSettings(
            Bitmap bmpStored,
            Bitmap bmpReplacing,
            byte tolerance)
            : base()
        {
            BmpToReplace = bmpStored;
            BmpReplacing = bmpReplacing;
            Tolerance = tolerance;
        }

        /// <summary>
        /// Copies all settings to another token.
        /// </summary>
        protected PersistentSettings(PersistentSettings other)
            : base(other)
        {
            BmpToReplace = other.BmpToReplace;
            BmpReplacing = other.BmpReplacing;
            Tolerance = other.Tolerance;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Copies all settings to another token.
        /// </summary>
        public override object Clone()
        {
            return new PersistentSettings(this);
        }
        #endregion
    }
}