namespace LyricsMerge
{
    partial class LyricsMerge
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
            this.btnMerge = new System.Windows.Forms.Button();
            this.ofdMerge = new System.Windows.Forms.OpenFileDialog();
            this.ofdSave = new System.Windows.Forms.OpenFileDialog();
            this.label1 = new System.Windows.Forms.Label();
            this.txtSave = new System.Windows.Forms.TextBox();
            this.grpLyric = new System.Windows.Forms.GroupBox();
            this.btnAbout = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnSelect = new System.Windows.Forms.Button();
            this.labSave = new System.Windows.Forms.Label();
            this.labMerge = new System.Windows.Forms.Label();
            this.txtMerge = new System.Windows.Forms.TextBox();
            this.grpLyric.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnMerge
            // 
            this.btnMerge.Location = new System.Drawing.Point(37, 280);
            this.btnMerge.Name = "btnMerge";
            this.btnMerge.Size = new System.Drawing.Size(75, 23);
            this.btnMerge.TabIndex = 0;
            this.btnMerge.Text = "Merge";
            this.btnMerge.UseVisualStyleBackColor = true;
            this.btnMerge.Click += new System.EventHandler(this.btnMerge_Click);
            // 
            // ofdMerge
            // 
            this.ofdMerge.Filter = "Lyric Files|*.srt;*.ssa";
            this.ofdMerge.Multiselect = true;
            // 
            // ofdSave
            // 
            this.ofdSave.Filter = "Lyric Files|*.srt;*.ssa";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 12);
            this.label1.TabIndex = 3;
            // 
            // txtSave
            // 
            this.txtSave.Location = new System.Drawing.Point(17, 182);
            this.txtSave.Name = "txtSave";
            this.txtSave.Size = new System.Drawing.Size(378, 21);
            this.txtSave.TabIndex = 4;
            // 
            // grpLyric
            // 
            this.grpLyric.Controls.Add(this.btnAbout);
            this.grpLyric.Controls.Add(this.btnClose);
            this.grpLyric.Controls.Add(this.btnSave);
            this.grpLyric.Controls.Add(this.btnSelect);
            this.grpLyric.Controls.Add(this.labSave);
            this.grpLyric.Controls.Add(this.txtSave);
            this.grpLyric.Controls.Add(this.btnMerge);
            this.grpLyric.Controls.Add(this.labMerge);
            this.grpLyric.Controls.Add(this.txtMerge);
            this.grpLyric.Location = new System.Drawing.Point(12, 12);
            this.grpLyric.Name = "grpLyric";
            this.grpLyric.Size = new System.Drawing.Size(415, 309);
            this.grpLyric.TabIndex = 7;
            this.grpLyric.TabStop = false;
            this.grpLyric.Text = "Merge Lyric";
            // 
            // btnAbout
            // 
            this.btnAbout.Location = new System.Drawing.Point(308, 280);
            this.btnAbout.Name = "btnAbout";
            this.btnAbout.Size = new System.Drawing.Size(75, 23);
            this.btnAbout.TabIndex = 8;
            this.btnAbout.Text = "About";
            this.btnAbout.UseVisualStyleBackColor = true;
            this.btnAbout.Click += new System.EventHandler(this.btnAbout_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(171, 280);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 7;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(261, 209);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(134, 23);
            this.btnSave.TabIndex = 6;
            this.btnSave.Text = "Select Save Lyric";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnSelect
            // 
            this.btnSelect.Location = new System.Drawing.Point(261, 128);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(134, 23);
            this.btnSelect.TabIndex = 5;
            this.btnSelect.Text = "Select Merge Lyrics";
            this.btnSelect.UseVisualStyleBackColor = true;
            this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
            // 
            // labSave
            // 
            this.labSave.AutoSize = true;
            this.labSave.Location = new System.Drawing.Point(17, 167);
            this.labSave.Name = "labSave";
            this.labSave.Size = new System.Drawing.Size(95, 12);
            this.labSave.TabIndex = 2;
            this.labSave.Text = "Save The Lyric:";
            // 
            // labMerge
            // 
            this.labMerge.AutoSize = true;
            this.labMerge.Location = new System.Drawing.Point(17, 30);
            this.labMerge.Name = "labMerge";
            this.labMerge.Size = new System.Drawing.Size(131, 12);
            this.labMerge.TabIndex = 1;
            this.labMerge.Text = "Prepare Merge Lyrics:";
            // 
            // txtMerge
            // 
            this.txtMerge.Location = new System.Drawing.Point(17, 48);
            this.txtMerge.Multiline = true;
            this.txtMerge.Name = "txtMerge";
            this.txtMerge.Size = new System.Drawing.Size(378, 74);
            this.txtMerge.TabIndex = 0;
            // 
            // LyricsMerge
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(440, 333);
            this.Controls.Add(this.grpLyric);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.Name = "LyricsMerge";
            this.Text = "LyricsMerge";
            this.grpLyric.ResumeLayout(false);
            this.grpLyric.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnMerge;
        private System.Windows.Forms.OpenFileDialog ofdMerge;
        private System.Windows.Forms.OpenFileDialog ofdSave;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtSave;
        private System.Windows.Forms.GroupBox grpLyric;
        private System.Windows.Forms.Label labSave;
        private System.Windows.Forms.Label labMerge;
        private System.Windows.Forms.TextBox txtMerge;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnSelect;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnAbout;
    }
}