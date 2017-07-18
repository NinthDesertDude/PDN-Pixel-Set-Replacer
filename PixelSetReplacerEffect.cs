using PaintDotNet;
using PaintDotNet.Effects;
using PixSetRepl.Gui;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace PixSetRepl
{
    /// <summary>
    /// Contains assembly information, accessible through variables.
    /// </summary>
    public class PluginSupportInfo : IPluginSupportInfo
    {
        #region Properties
        /// <summary>
        /// Gets the author.
        /// </summary>
        public string Author
        {
            get
            {
                return ((AssemblyCompanyAttribute)base.GetType().Assembly.GetCustomAttributes(typeof(AssemblyCompanyAttribute), false)[0]).Company;
            }
        }

        /// <summary>
        /// Gets the copyright information.
        /// </summary>
        public string Copyright
        {
            get
            {
                return ((AssemblyCopyrightAttribute)base.GetType().Assembly.GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false)[0]).Copyright;
            }
        }

        /// <summary>
        /// Gets the name of the product.
        /// </summary>
        public string DisplayName
        {
            get
            {
                return ((AssemblyProductAttribute)base.GetType().Assembly.GetCustomAttributes(typeof(AssemblyProductAttribute), false)[0]).Product;
            }
        }

        /// <summary>
        /// Gets the version number.
        /// </summary>
        public Version Version
        {
            get
            {
                return base.GetType().Assembly.GetName().Version;
            }
        }

        /// <summary>
        /// Gets the URL where the plugin is released to the public.
        /// </summary>
        public Uri WebsiteUri
        {
            get
            {
                return new Uri("http://forums.getpaint.net/index.php?/forum/7-plugins-publishing-only/");
            }
        }
        #endregion
    }

    /// <summary>
    /// Controls the effect. In short, a GUI is instantiated by
    /// CreateConfigDialog and when the dialog signals OK, Render is called,
    /// passing OnSetRenderInfo to it. The dialog stores its result in an
    /// intermediate class called RenderSettings, which is then accessed to
    /// draw the final result in Render.
    /// </summary>
    [PluginSupportInfo(typeof(PluginSupportInfo), DisplayName = "Pixel Set Replacer")]
    public class EffectPlugin : Effect
    {
        #region Fields
        /// <summary>
        /// The dialog to be constructed.
        /// </summary>
        private winPixelSetReplacer dlg;

        /// <summary>
        /// Paint.net wants to call Render() a million times. This prevents it.
        /// </summary>
        private static bool renderReady = false;
        #endregion

        #region Properties
        /// <summary>
        /// The icon of the plugin to be displayed next to its menu entry.
        /// </summary>
        public static Bitmap StaticImage
        {
            get
            {
                return new Bitmap(Assembly.GetExecutingAssembly()
                    .GetManifestResourceStream("PixSetRepl.EffectPluginIcon.png"));
            }
        }

        /// <summary>
        /// The name of the plugin as it appears in Paint.NET.
        /// </summary>
        public static string StaticName
        {
            get
            {
                return Globalization.GlobalStrings.Title;
            }
        }

        /// <summary>
        /// The name of the menu category the plugin appears under.
        /// </summary>
        public static string StaticSubMenuName
        {
            get
            {
                return "Tools";
            }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Constructor.
        /// </summary>
        public EffectPlugin()
            : base(EffectPlugin.StaticName,
            EffectPlugin.StaticImage,
            EffectPlugin.StaticSubMenuName,
            EffectFlags.Configurable)
        {
            dlg = null;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Tells Paint.NET which form to instantiate as the plugin's GUI.
        /// Called remotely by Paint.NET.
        /// </summary>
        public override EffectConfigDialog CreateConfigDialog()
        {
            dlg = new winPixelSetReplacer();
            StaticSettings.dialogResult = StaticSettings.DialogResult.Default;
            renderReady = false;
            return dlg;
        }

        /// <summary>
        /// Gets the render information.
        /// </summary>
        /// <param name="parameters">
        /// Saved settings used to restore the GUI to the same settings it was
        /// saved with last time the effect was applied.
        /// </param>
        /// <param name="dstArgs">The destination canvas.</param>
        /// <param name="srcArgs">The source canvas.</param>
        protected override void OnSetRenderInfo(
            EffectConfigToken parameters,
            RenderArgs dstArgs,
            RenderArgs srcArgs)
        {
            base.OnSetRenderInfo(parameters, dstArgs, srcArgs);
        }

        /// <summary>
        /// Renders the effect over rectangular regions automatically
        /// determined and handled by Paint.NET for multithreading support.
        /// </summary>
        /// <param name="parameters">
        /// Saved settings used to restore the GUI to the same settings it was
        /// saved with last time the effect was applied.
        /// </param>
        /// <param name="dstArgs">The destination canvas.</param>
        /// <param name="srcArgs">The source canvas.</param>
        /// <param name="rois">
        /// A list of rectangular regions to split this effect into so it can
        /// be optimized by worker threads. Determined and managed by
        /// Paint.NET.
        /// </param>
        /// <param name="startIndex">
        /// The rectangle to begin rendering with. Used in Paint.NET's effect
        /// multithreading process.
        /// </param>
        /// <param name="length">
        /// The number of rectangles to render at once. Used in Paint.NET's
        /// effect multithreading process.
        /// </param>
        public override void Render(
            EffectConfigToken parameters,
            RenderArgs dstArgs,
            RenderArgs srcArgs,
            Rectangle[] rois,
            int startIndex,
            int length)
        {
            //Renders the effect if the dialog is closed and accepted.
            if (!IsCancelRequested && !renderReady)
            {
                var src = srcArgs.Surface;
                var dst = dstArgs.Surface;
                PersistentSettings token = (PersistentSettings)dlg.EffectToken;
                Rectangle selection = EnvironmentParameters.GetSelection(srcArgs.Bounds).GetBoundsInt();

                //Something happened, so this must be the final render.
                if (StaticSettings.dialogResult != StaticSettings.DialogResult.Default)
                {
                    renderReady = true;
                }

                if (StaticSettings.dialogResult ==
                    StaticSettings.DialogResult.StoringFirstImage)
                {
                    if (selection.Width <= 1 || selection.Height <= 1 ||
                        ((token.BmpReplacing.Width != selection.Width ||
                        token.BmpReplacing.Height != selection.Height) &&
                        (token.BmpReplacing.Width != 1 &&
                        token.BmpReplacing.Height != 1)))
                    {
                        MessageBox.Show("The image to replace and its " +
                            "replacing image must be the same size, and " +
                            "the selection must be larger than a pixel.");
                        return;
                    }

                    //Copies the data into the token.
                    token.BmpToReplace = new Bitmap(selection.Width, selection.Height);
                    for (int i = 0; i < selection.Width; i++)
                    {
                        for (int j = 0; j < selection.Height; j++)
                        {
                            token.BmpToReplace.SetPixel(i, j, src[selection.X + i, selection.Y + j]);
                        }
                    }
                }
                else if (StaticSettings.dialogResult ==
                    StaticSettings.DialogResult.StoringSecondImage)
                {
                    if (selection.Width <= 1 || selection.Height <= 1 ||
                        ((token.BmpToReplace.Width != selection.Width ||
                        token.BmpToReplace.Height != selection.Height) &&
                        (token.BmpToReplace.Width != 1 &&
                        token.BmpToReplace.Height != 1)))
                    {
                        MessageBox.Show("The image to replace and its " +
                            "replacing image must be the same size, and " +
                            "the selection must be larger than a pixel.");
                        return;
                    }

                    //Copies the data into the token.
                    token.BmpReplacing = new Bitmap(selection.Width, selection.Height);
                    for (int i = 0; i < selection.Width; i++)
                    {
                        for (int j = 0; j < selection.Height; j++)
                        {
                            token.BmpReplacing.SetPixel(i, j, src[selection.X + i, selection.Y + j]);
                        }
                    }
                }
                else if (StaticSettings.dialogResult ==
                    StaticSettings.DialogResult.Replacing)
                {
                    if (token.BmpToReplace.Width != token.BmpReplacing.Width ||
                        token.BmpToReplace.Height != token.BmpReplacing.Height ||
                        token.BmpToReplace.Width == 1 ||
                        token.BmpToReplace.Height == 1)
                    {
                        MessageBox.Show("Both images must be selected first.");
                        return;
                    }

                    List<Point> replacedPixels = new List<Point>();

                    //Looks at each pixel and compares it to the top-left
                    //corner of the first image. Skips pixels that are too
                    //far to the right or bottom to contain the entire image.
                    //Tracks the pixel location. When a match is found, all
                    //its pixels are checked. If everything matches, those
                    //pixels are replaced immediately and added to a list of
                    //pixels to skip over.
                    dst.CopySurface(src);

                    for (int y = 0; y <= src.Height - token.BmpToReplace.Height; y++)
                    {
                        for (int x = 0; x <= src.Width - token.BmpToReplace.Width; x++)
                        {
                            //Skips all pixels that have been replaced.
                            if (replacedPixels.Count > 0 &&
                                replacedPixels[0].X == x &&
                                replacedPixels[0].Y == y)
                            {
                                replacedPixels.RemoveAt(0);
                                continue;
                            }

                            //Checks if the first pixel matches.
                            if (ColorsMatch(ColorBgra.FromColor(
                                token.BmpToReplace.GetPixel(0, 0)),
                                src[x, y], token.Tolerance))
                            {
                                bool totalMatch = true;

                                //Checks if every pixel matches.
                                for (int yy = 0; yy < token.BmpToReplace.Height; yy++)
                                {
                                    for (int xx = 0; xx < token.BmpToReplace.Width; xx++)
                                    {
                                        if (!ColorsMatch(ColorBgra.FromColor(
                                            token.BmpToReplace.GetPixel(xx, yy)),
                                            src[x + xx, y + yy], token.Tolerance))
                                        {
                                            totalMatch = false;
                                            break;
                                        }
                                    }
                                    if (!totalMatch)
                                    {
                                        break;
                                    }
                                }

                                //Replaces the matching pixels. They will be skipped.
                                if (totalMatch)
                                {
                                    for (int yy = 0; yy < token.BmpReplacing.Height; yy++)
                                    {
                                        for (int xx = 0; xx < token.BmpReplacing.Width; xx++)
                                        {
                                            dst[x + xx, y + yy] = ColorBgra.FromColor(token.BmpReplacing.GetPixel(xx, yy));

                                            //All pixels except the first should be skipped.
                                            //The first has been checked already.
                                            if (xx != 0 && yy != 0)
                                            {
                                                replacedPixels.Add(new Point(x + xx, y + yy));
                                            }
                                        }
                                    }
                                    replacedPixels.OrderBy(p => p.X).ThenBy(p => p.Y);
                                }
                            }
                        }
                    }
                }
            }

            StaticSettings.dialogResult = StaticSettings.DialogResult.Default;
        }

        /// <summary>
        /// Returns if two colors match, given some tolerance.
        /// </summary>
        /// <param name="col1">The first color.</param>
        /// <param name="col2">The second color.</param>
        /// <param name="tolerance">
        /// Tolerance allows the pixels to be considered a match if they are
        /// near in value. Higher tolerances allow for bigger differences.
        /// </param>
        private bool ColorsMatch(ColorBgra col1, ColorBgra col2, byte tolerance)
        {
            if (col1.R >= col2.R - tolerance &&
                col1.R <= col2.R + tolerance)
            {
                if (col1.G >= col2.G - tolerance &&
                    col1.G <= col2.G + tolerance)
                {
                    if (col1.B >= col2.B - tolerance &&
                        col1.B <= col2.B + tolerance)
                    {
                        if (col1.A >= col2.A - tolerance &&
                            col1.A <= col2.A + tolerance)
                        {
                            return true;
                        }
                    }
                }
            }

            return false;
        }
        #endregion
    }
}