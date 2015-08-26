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
    public partial class Form1 : Form
    {
        private RetrieveData retrieveData = new RetrieveData();

        public Form1()
        {
            InitializeComponent();
            comboBox1.DataSource = new BindingSource(retrieveData.TeamFullNames, null);
            comboBox1.DisplayMember = "Value";
            comboBox1.ValueMember = "Key";
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string teamAbbr;
            teamAbbr = ((KeyValuePair<string, string>)comboBox1.SelectedItem).Key.ToString();
            MatchData matchData = retrieveData.getData(teamAbbr);
            label1.Text = retrieveData.TeamFullNames[matchData.away];
            label2.Text = retrieveData.TeamFullNames[matchData.home];
            Image i = retrieveData.GenerateWallpaper(matchData);
            pictureBox1.Image = i;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

    }
}
