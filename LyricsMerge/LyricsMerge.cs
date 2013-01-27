using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using LyricsMergeLib;

namespace LyricsMerge
{
    public partial class LyricsMerge : Form
    {
        public LyricsMerge()
        {
            InitializeComponent();
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            if (this.ofdMerge.ShowDialog() == DialogResult.OK)
            {
                if (this.ofdMerge.FileNames.Length > 0)
                {
                    StringBuilder mergeFiles = new StringBuilder();
                    foreach (string file in this.ofdMerge.FileNames)
                    {
                        mergeFiles.AppendLine(file);
                    }
                    this.txtMerge.Text = mergeFiles.ToString();
                }
            }
        }

        private void btnMerge_Click(object sender, EventArgs e)
        {
            string mergeFiles = (this.txtMerge.Text ?? string.Empty).Trim();
            if (string.IsNullOrEmpty(mergeFiles))
            {
                this.ShowErrorMessage("Please select the files to prepare merge lyrics!");
                return;
            }
            string saveFile = (this.txtSave.Text ?? string.Empty).Trim();
            if (string.IsNullOrEmpty(saveFile))
            {
                this.ShowErrorMessage("Please select or input the files to save lyric!");
                return;
            }
            try
            {
                LyricsMergeProcessor.MergeLyrics(this.Split(mergeFiles, "\n"), saveFile);
                this.ShowAlterMessage("Merge Lyrics Successfully!");
            }
            catch
            {
                this.ShowErrorMessage("Merge Lyrics Failure!");
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (this.ofdSave.ShowDialog() == DialogResult.OK)
            {
                if (this.ofdSave.FileNames.Length > 0)
                {
                    this.txtSave.Text = this.ofdSave.FileName;
                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAbout_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Program：Lyrics Merge\n\nVersion：V1.0\n\nAuthor：HaoLiang, ETao", "Copyright");
        }

        private void ShowErrorMessage(string mess)
        {
            MessageBox.Show(mess ?? string.Empty, "Error Message");
        }

        private void ShowAlterMessage(string mess)
        {
            MessageBox.Show(mess ?? string.Empty, "Message");
        }


        private string[] Split(string text, string separator)
        {
            string[] result = text.Split(new string[] { separator }, StringSplitOptions.None);
            return result;
        }


    }
}
