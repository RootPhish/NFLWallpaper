using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace NFLWallpaper
{
    public partial class MainForm : Form
    {
        private RetrieveData retrieveData = new RetrieveData();

        public MainForm()
        {
            InitializeComponent();
            teamSelectionCombo.DataSource = new BindingSource(retrieveData.TeamFullNames, null);
            teamSelectionCombo.DisplayMember = "Value";
            teamSelectionCombo.ValueMember = "Key";
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void generateButton_Click(object sender, EventArgs e)
        {
            string teamAbbr;
            teamAbbr = ((KeyValuePair<string, string>)teamSelectionCombo.SelectedItem).Key.ToString();
            MatchData matchData = retrieveData.getData(teamAbbr);
            Image i = retrieveData.GenerateWallpaper(matchData);
            pictureBox1.Image = i;
            saveButton.Enabled = true;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "JPEG Image|*.jpg|Bitmap Image|*.bmp|GIF Image|*.gif";
            sfd.Title = "Save image file";
            sfd.ShowDialog();

            if (sfd.FileName != "")
            {
                System.IO.FileStream fs = (System.IO.FileStream)sfd.OpenFile();
                switch (sfd.FilterIndex)
                {
                    case 1:
                        this.pictureBox1.Image.Save(fs, System.Drawing.Imaging.ImageFormat.Jpeg);
                        break;
                    case 2:
                        this.pictureBox1.Image.Save(fs, System.Drawing.Imaging.ImageFormat.Bmp);
                        break;
                    case 3:
                        this.pictureBox1.Image.Save(fs, System.Drawing.Imaging.ImageFormat.Gif);
                        break;

                }
                fs.Close();
            }
        }
    }
}
