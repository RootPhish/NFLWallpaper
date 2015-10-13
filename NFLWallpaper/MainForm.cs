﻿using System;
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

        static bool IsABackground(string resource)
        {
            return (resource.StartsWith("NFLWallpaper.Resources.Background."));
        }

        public MainForm()
        {
            InitializeComponent();
            teamSelectionCombo.DataSource = new BindingSource(retrieveData.TeamFullNames, null);
            teamSelectionCombo.DisplayMember = "Value";
            teamSelectionCombo.ValueMember = "Key";
            var assembly = typeof(NFLWallpaper.Program).Assembly;
            string[] resources = Array.FindAll(assembly.GetManifestResourceNames(), IsABackground);
            foreach (string resource in resources)
            {
                string background = resource.Substring(34, resource.Length - 38);
                bgSelectionCombo.Items.Add(background);
            }
            bgSelectionCombo.SelectedIndex = 0;
            bgSelectionCombo.Items.Add("Other...");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                teamSelectionCombo.SelectedItem = Helper.GetEntry(retrieveData.TeamFullNames, Application.UserAppDataRegistry.GetValue("DefaultTeam").ToString());
                string wallpaperStyle = Application.UserAppDataRegistry.GetValue("WallpaperStyle").ToString();
                switch (wallpaperStyle)
                {
                    case "Stretched": radioButton1.Checked = true; break;
                    case "Centered": radioButton2.Checked = true; break;
                    case "Tiled": radioButton3.Checked = true; break;
                }
            }
            catch (Exception ex)
            {
            }
            generateButton_Click(null, null);
        }

        private void generateButton_Click(object sender, EventArgs e)
        {
            if (bgSelectionCombo.SelectedItem.ToString() == "Other...")
            {
                openCustomWallpaper();
            } else
            {
                string teamAbbr;
                teamAbbr = ((KeyValuePair<string, string>)teamSelectionCombo.SelectedItem).Key.ToString();
                MatchData matchData = retrieveData.getData(teamAbbr);
                if ((matchData.away != null) &&
                    (matchData.home != null) &&
                    (matchData.day != null) &&
                    (matchData.time != null) &&
                    (matchData.eid != null))
                {
                    Image i = retrieveData.GenerateWallpaper(matchData, bgSelectionCombo.SelectedItem.ToString());
                    pictureBox1.Image = i;
                    saveButton.Enabled = true;
                    wallpaperBox.Enabled = true;
                }
            }
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

        private void openCustomWallpaper()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "JPEG Image|*.jpg";
            ofd.Title = "Select Background Image";
            ofd.ShowDialog();

            if (ofd.FileName != "")
            {
                bgSelectionCombo.Items.Remove("Other...");
                bgSelectionCombo.Items.Add(ofd.FileName);
                bgSelectionCombo.Items.Add("Other...");
                bgSelectionCombo.SelectedIndex = bgSelectionCombo.Items.Count - 2;
                generateButton_Click(null, null);
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                Application.UserAppDataRegistry.SetValue("DefaultTeam", ((KeyValuePair<string, string>)teamSelectionCombo.SelectedItem).Key.ToString());
                var checkedButton = wallpaperBox.Controls.OfType<RadioButton>().FirstOrDefault(r => r.Checked);
                Application.UserAppDataRegistry.SetValue("WallpaperStyle", checkedButton.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var checkedButton = wallpaperBox.Controls.OfType<RadioButton>().FirstOrDefault(r => r.Checked);
            Wallpaper.Style style;
            switch (checkedButton.Text)
            {
                case "Centered": style = Wallpaper.Style.Centered; break;
                case "Stretched": style = Wallpaper.Style.Stretched; break;
                case "Tiled": style = Wallpaper.Style.Tiled; break;
                default: style = Wallpaper.Style.Centered; break;
            }
            Wallpaper.Set(pictureBox1.Image, style);
        }
    }
}
