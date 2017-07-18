namespace PixSetRepl.Gui
{
    partial class winPixelSetReplacer
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.bttnImageToReplace = new System.Windows.Forms.Button();
            this.bttnImageReplacing = new System.Windows.Forms.Button();
            this.bttnReplace = new System.Windows.Forms.Button();
            this.bttnReset = new System.Windows.Forms.Button();
            this.sliderTolerance = new System.Windows.Forms.TrackBar();
            this.lblTolerance = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.sliderTolerance)).BeginInit();
            this.SuspendLayout();
            // 
            // bttnImageToReplace
            // 
            this.bttnImageToReplace.Location = new System.Drawing.Point(12, 12);
            this.bttnImageToReplace.Name = "bttnImageToReplace";
            this.bttnImageToReplace.Size = new System.Drawing.Size(260, 23);
            this.bttnImageToReplace.TabIndex = 0;
            this.bttnImageToReplace.Text = "Store Image to Replace";
            this.bttnImageToReplace.UseVisualStyleBackColor = true;
            this.bttnImageToReplace.Click += new System.EventHandler(this.bttnImageToReplace_Click);
            // 
            // bttnImageReplacing
            // 
            this.bttnImageReplacing.Location = new System.Drawing.Point(13, 42);
            this.bttnImageReplacing.Name = "bttnImageReplacing";
            this.bttnImageReplacing.Size = new System.Drawing.Size(259, 23);
            this.bttnImageReplacing.TabIndex = 1;
            this.bttnImageReplacing.Text = "Store Replacing Image";
            this.bttnImageReplacing.UseVisualStyleBackColor = true;
            this.bttnImageReplacing.Click += new System.EventHandler(this.bttnImageReplacing_Click);
            // 
            // bttnReplace
            // 
            this.bttnReplace.BackColor = System.Drawing.Color.PaleGreen;
            this.bttnReplace.Location = new System.Drawing.Point(172, 137);
            this.bttnReplace.Name = "bttnReplace";
            this.bttnReplace.Size = new System.Drawing.Size(100, 23);
            this.bttnReplace.TabIndex = 4;
            this.bttnReplace.Text = "Replace";
            this.bttnReplace.UseVisualStyleBackColor = false;
            this.bttnReplace.Click += new System.EventHandler(this.bttnReplace_Click);
            // 
            // bttnReset
            // 
            this.bttnReset.BackColor = System.Drawing.Color.MistyRose;
            this.bttnReset.Location = new System.Drawing.Point(13, 137);
            this.bttnReset.Name = "bttnReset";
            this.bttnReset.Size = new System.Drawing.Size(100, 23);
            this.bttnReset.TabIndex = 3;
            this.bttnReset.Text = "Reset";
            this.bttnReset.UseVisualStyleBackColor = false;
            this.bttnReset.Click += new System.EventHandler(this.bttnReset_Click);
            // 
            // sliderTolerance
            // 
            this.sliderTolerance.Location = new System.Drawing.Point(12, 71);
            this.sliderTolerance.Maximum = 255;
            this.sliderTolerance.Name = "sliderTolerance";
            this.sliderTolerance.Size = new System.Drawing.Size(260, 45);
            this.sliderTolerance.TabIndex = 2;
            this.sliderTolerance.TickStyle = System.Windows.Forms.TickStyle.None;
            this.sliderTolerance.Value = 5;
            this.sliderTolerance.ValueChanged += new System.EventHandler(this.sliderTolerance_ValueChanged);
            // 
            // lblTolerance
            // 
            this.lblTolerance.AutoSize = true;
            this.lblTolerance.Location = new System.Drawing.Point(112, 93);
            this.lblTolerance.Name = "lblTolerance";
            this.lblTolerance.Size = new System.Drawing.Size(67, 13);
            this.lblTolerance.TabIndex = 5;
            this.lblTolerance.Text = "Tolerance: 5";
            this.lblTolerance.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // winPixelSetReplacer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 175);
            this.Controls.Add(this.lblTolerance);
            this.Controls.Add(this.sliderTolerance);
            this.Controls.Add(this.bttnReset);
            this.Controls.Add(this.bttnReplace);
            this.Controls.Add(this.bttnImageReplacing);
            this.Controls.Add(this.bttnImageToReplace);
            this.Location = new System.Drawing.Point(0, 0);
            this.Name = "winPixelSetReplacer";
            this.Text = "Pixel Set Replacer";
            ((System.ComponentModel.ISupportInitialize)(this.sliderTolerance)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button bttnImageToReplace;
        private System.Windows.Forms.Button bttnImageReplacing;
        private System.Windows.Forms.Button bttnReplace;
        private System.Windows.Forms.Button bttnReset;
        private System.Windows.Forms.TrackBar sliderTolerance;
        private System.Windows.Forms.Label lblTolerance;
    }
}