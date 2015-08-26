using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;
using System.Xml.Linq;
using System.Drawing;
using System.Drawing.Text;
using System.Runtime.InteropServices;
using System.Reflection;
using System.Windows.Forms;
using log4net;
using log4net.Config;

namespace NFLWallpaper
{
    class RetrieveData: IDisposable
    {
        bool disposed = false;

        private static readonly ILog logger = LogManager.GetLogger(typeof(RetrieveData));

        private XDocument xd;
        private PrivateFontCollection pfc;

        public RetrieveData()
        {
            BasicConfigurator.Configure();
            loadSchedule();
            pfc = new PrivateFontCollection();
            AddFontFromResource(pfc, "NFLWallpaper.Resources.Font.endzone-tech.ttf");
            AddFontFromResource(pfc, "NFLWallpaper.Resources.Font.sans-cond-medium.ttf");
            TeamFullNames = new Dictionary<string, string> { };
            foreach (string key in TeamNames.Keys)
            {
                TeamFullNames.Add(key, TeamCities[key] + " " + TeamNames[key]);
            }
        }

        private void loadSchedule()
        {
            try {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://www.nfl.com/liveupdate/scorestrip/ss.xml");

                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    StreamReader reader = new StreamReader(response.GetResponseStream());
                    xd = XDocument.Load(reader);
                }
            } catch
            {
                logger.Fatal("Unable to load data from NFL website!");
                MessageBox.Show("Unable to load data from NFL website!\nApplication will exit.", "Fatal error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Environment.Exit(1);
            }
        }

        private static void AddFontFromResource(PrivateFontCollection privateFontCollection, string fontResourceName)
        {
            var fontBytes = GetFontResourceBytes(typeof(NFLWallpaper.Program).Assembly, fontResourceName);
            var fontData = Marshal.AllocCoTaskMem(fontBytes.Length);
            Marshal.Copy(fontBytes, 0, fontData, fontBytes.Length);
            privateFontCollection.AddMemoryFont(fontData, fontBytes.Length);
            Marshal.FreeCoTaskMem(fontData);
        }

        private static byte[] GetFontResourceBytes(Assembly assembly, string fontResourceName)
        {
            var resourceStream = assembly.GetManifestResourceStream(fontResourceName);
            if (resourceStream == null)
                throw new Exception(string.Format("Unable to find font '{0}' in embedded resources.", fontResourceName));
            var fontBytes = new byte[resourceStream.Length];
            resourceStream.Read(fontBytes, 0, (int)resourceStream.Length);
            resourceStream.Close();
            return fontBytes;
        }

        public Dictionary<string, string> TeamNames = new Dictionary<string, string> {
            {"CIN", "Bengals"},
            {"HOU", "Texans"},
            {"NE",  "Patriots"},
            {"ATL", "Falcons" },
            {"DET", "Lions" },
            {"BUF", "Bills" },
            {"SEA", "Seahawks" },
            {"MIA", "Dolphins" },
            {"BAL", "Ravens" },
            {"CHI", "Bears" },
            {"JAC", "Jaguars" },
            {"DEN", "Broncos" },
            {"OAK", "Raiders" },
            {"SD", "Chargers" },
            {"GB", "Packers" },
            {"DAL", "Cowboys" },
            {"STL", "Rams" },
            {"WAS", "Redskins" },
            {"CLE", "Browns" },
            {"NYJ", "Jets" },
            {"KC", "Chiefs" },
            {"CAR", "Panthers" },
            {"PHI", "Eagles" },
            {"IND", "Colts" },
            {"NO", "Saints" },
            {"NYG", "Giants" },
            {"MIN", "Vikings" },
            {"ARI", "Cardinals" },
            {"PIT", "Steelers" },
            {"SF", "49ers" },
            {"TB", "Buccaneers" },
            {"TEN", "Titans" }
        };

        public Dictionary<string, string> TeamCities = new Dictionary<string, string> {
            {"CIN", "Cincinnati"},
            {"HOU", "Houston"},
            {"NE",  "New England"},
            {"ATL", "Atlanta" },
            {"DET", "Detroit" },
            {"BUF", "Buffalo" },
            {"SEA", "Seattle" },
            {"MIA", "Miami" },
            {"BAL", "Baltimore" },
            {"CHI", "Chicago" },
            {"JAC", "Jacksonville" },
            {"DEN", "Denver" },
            {"OAK", "Oakland" },
            {"SD", "San Diego" },
            {"GB", "Green Bay" },
            {"DAL", "Dallas" },
            {"STL", "St. Louis" },
            {"WAS", "Washington" },
            {"CLE", "Cleveland" },
            {"NYJ", "New York" },
            {"KC", "Kansas City" },
            {"CAR", "Carolina" },
            {"PHI", "Philadelphia" },
            {"IND", "Indianapolis" },
            {"NO", "New Orleans" },
            {"NYG", "New York" },
            {"MIN", "Minnesota" },
            {"ARI", "Arizona" },
            {"PIT", "Pittsburg" },
            {"SF", "San Francisco" },
            {"TB", "Tampa Bay" },
            {"TEN", "Tennessee" }
        };

        public Dictionary<string, string> TeamFullNames;

        public string GetTeamAbbreviation(string teamName)
        {
            return TeamNames.FirstOrDefault(x => x.Value == teamName).Key;
        }

        public string GetTeamCity(string teamAbbr)
        {
            return TeamCities[teamAbbr];
        }

        public string GetTeamName(string teamAbbr)
        {
            return TeamNames[teamAbbr];
        }

        public string GetTeamFullname(string teamAbbr)
        {
            return TeamFullNames[teamAbbr];
        }

        public MatchData getData(string teamAbbr)
        {
            var data = from item in xd.Descendants("g")
                       where (string)item.Attribute("v") == teamAbbr || (string)item.Attribute("h") == teamAbbr
                       select new
                       {
                           home = item.Attribute("h").Value,
                           away = item.Attribute("v").Value,
                           time = item.Attribute("t").Value,
                           day  = item.Attribute("d").Value
                       };
            var p = data.First();
            MatchData matchData;
            matchData.home = p.home.ToString();
            matchData.away = p.away.ToString();
            matchData.day = p.day.ToString();
            matchData.time = p.time.ToString();
            return matchData;
        }

        private float MeasureDisplayString(Graphics g, string text, Font font)
        {
            float doubleWidth = g.MeasureString(text+text, font).Width;
            float singleWidth = g.MeasureString(text, font).Width;
            return doubleWidth - singleWidth;
        }

        public Image GenerateWallpaper(MatchData data)
        {
            string awayText = GetTeamCity(data.away).ToUpper();
            string awayTeam = GetTeamName(data.away).ToUpper();
            string homeText = GetTeamCity(data.home).ToUpper();
            string homeTeam = GetTeamName(data.home).ToUpper();
            PointF awayCityLocation = new PointF(0f, 400f);
            PointF awayTeamLocation = new PointF(-20f, 440f);
            var assembly = typeof(NFLWallpaper.Program).Assembly;
            string[] names = assembly.GetManifestResourceNames();
            Image image = Image.FromStream(assembly.GetManifestResourceStream("NFLWallpaper.Resources.Background.background.jpg"));
            Graphics graphics = Graphics.FromImage(image);
            using (Font teamFont = new Font(pfc.Families[0], 150, FontStyle.Bold, GraphicsUnit.Pixel),
                        cityFont = new Font(pfc.Families[1], 50, FontStyle.Bold, GraphicsUnit.Pixel),
                        dayFont  = new Font(pfc.Families[1], 50, FontStyle.Bold, GraphicsUnit.Pixel))
            {
                StringFormat format = new StringFormat(StringFormat.GenericTypographic);
                float height = cityFont.Size * cityFont.FontFamily.GetLineSpacing(FontStyle.Bold) / cityFont.FontFamily.GetEmHeight(FontStyle.Bold);
                height += teamFont.Size * teamFont.FontFamily.GetCellAscent(FontStyle.Bold) / teamFont.FontFamily.GetEmHeight(FontStyle.Bold);
                RectangleF rect = new RectangleF(20, (1200 - height) / 2, 1560, height);
                format.Alignment = StringAlignment.Near;
                format.LineAlignment = StringAlignment.Near;
                graphics.DrawString(awayText, cityFont, Brushes.White, rect, format);
                format.LineAlignment = StringAlignment.Far;
                graphics.DrawString(awayTeam, teamFont, Brushes.White, rect, format);
                format.Alignment = StringAlignment.Far;
                format.LineAlignment = StringAlignment.Near;
                graphics.DrawString(homeText, cityFont, Brushes.White, rect, format);
                format.LineAlignment = StringAlignment.Far;
                graphics.DrawString(homeTeam, teamFont, Brushes.White, rect, format);
                rect = new RectangleF(0, 20, 1600, 100);
                format.Alignment = StringAlignment.Center;
                format.LineAlignment = StringAlignment.Near;
                graphics.DrawString(data.day, dayFont, Brushes.White, rect, format);
                format.LineAlignment = StringAlignment.Far;
                graphics.DrawString(data.time, dayFont, Brushes.White, rect, format);
            }
            Image helmet = Image.FromStream(assembly.GetManifestResourceStream("NFLWallpaper.Resources.Helmets.BEN.png"));
            graphics.DrawImage(helmet, new RectangleF(10, 10, 200, 200));
            image.Save(@"C:\Temp\test.jpg");
            return image;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing && (pfc != null))
            {
                pfc.Dispose();
            }

            disposed = true;
        }
    }
}