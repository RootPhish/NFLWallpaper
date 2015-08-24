using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;
using System.Xml.Linq;

namespace NFLWallpaper
{
    class retrieveData
    {
        private XDocument xd;

        public retrieveData()
        {
            xd = XDocument.Load("http://www.nfl.com/liveupdate/scorestrip/ss.xml");
        }

        public Dictionary<string, string> TeamNames = new Dictionary<string, string> {
            {"CIN", "Cincinnati Bengals"},
            {"HOU", "Houston Texans"},
            {"NE",  "New England Patriots"},
            {"ATL", "Atlanta Falcons" },
            {"DET", "Detroit Lions" },
            {"BUF", "Buffalo Bills" },
            {"SEA", "Seattle Seahawks" },
            {"MIA", "Miami Dolphins" },
            {"BAL", "Baltimore Ravens" },
            {"CHI", "Chicago Bears" },
            {"JAC", "Jacksonville Jaguars" },
            {"DEN", "Denver Broncos" },
            {"OAK", "Oakland Raiders" },
            {"SD", "San Diego Chargers" },
            {"GB", "Green Bay Packers" },
            {"DAL", "Dallas Cowboys" },
            {"STL", "St. Louis Rams" },
            {"WAS", "Washington Redskins" },
            {"CLE", "Cleveland Browns" },
            {"NYJ", "New York Jets" },
            {"KC", "Kansas City Chiefs" },
            {"CAR", "Carolina Panthers" },
            {"PHI", "Philadelphia Eagles" },
            {"IND", "Indianapolis Colts" },
            {"NO", "New Orleans Saints" },
            {"NYG", "New York Giants" },
            {"MIN", "Minnesota Vikings" },
            {"ARI", "Arizona Cardinals" },
            {"PIT", "Pittsburg Steelers" },
            {"SF", "San Francisco 49ers" },
            {"TB", "Tampa Bay Buccaneers" },
            {"TEN", "Tennessee Titans" }
        };

        public string GetTeamAbbreviation(string teamName)
        {
            return TeamNames.FirstOrDefault(x => x.Value == teamName).Key;
        }

        public string GetTeamFullname(string teamAbbr)
        {
            return TeamNames[teamAbbr];
        }

        public MatchData getData(string teamAbbr)
        {
            var data = from item in xd.Descendants("g")
                       where (string)item.Attribute("v") == teamAbbr || (string)item.Attribute("h") == teamAbbr
                       select new
                       {
                           home = item.Attribute("h").Value,
                           versus = item.Attribute("v").Value
                       };
            var p = data.First();
            MatchData matchData;
            matchData.home = p.home.ToString();
            matchData.versus = p.versus.ToString();
            return matchData;
        }
    }
}