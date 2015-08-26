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
            label1.Text = retrieveData.TeamFullNames[matchData.away];
            label2.Text = retrieveData.TeamFullNames[matchData.home];
            Image i = retrieveData.GenerateWallpaper(matchData);
            pictureBox1.Image = i;
        }
    }
}
