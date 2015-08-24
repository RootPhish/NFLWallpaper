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
            foreach (KeyValuePair<string, string> entry in retrieveData.TeamNames)
            {
                comboBox1.Items.Add(entry.Value);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string teamName, teamAbbr;
            teamName = (sender as ComboBox).Text;
            teamAbbr = retrieveData.TeamNames.FirstOrDefault(x => x.Value == teamName).Key;
            MatchData matchData = retrieveData.getData(teamAbbr);
            label1.Text = retrieveData.GetTeamFullname(matchData.versus);
            label2.Text = retrieveData.GetTeamFullname(matchData.home);
        }
    }
}
