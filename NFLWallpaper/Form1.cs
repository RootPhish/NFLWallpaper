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
        private retrieveData retrieveData = new retrieveData();

        public Form1()
        {
            InitializeComponent();
            comboBox1.DataSource = new BindingSource(retrieveData.TeamNames, null);
            comboBox1.DisplayMember = "Value";
            comboBox1.ValueMember = "Key";
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string teamName, teamAbbr;
            teamAbbr = ((KeyValuePair<string, string>)comboBox1.SelectedItem).Key.ToString();
            MatchData matchData = retrieveData.getData(teamAbbr);
            label1.Text = retrieveData.GetTeamFullname(matchData.away);
            label2.Text = retrieveData.GetTeamFullname(matchData.home);
            retrieveData.GenerateWallpaper(matchData);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }
    }
}
