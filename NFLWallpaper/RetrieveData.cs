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
using System.Drawing.Drawing2D;
using System.Globalization;

namespace NFLWallpaper
{
    class RetrieveData: IDisposable
    {
        bool disposed = false;

        private static readonly ILog logger = LogManager.GetLogger(typeof(RetrieveData));

        private XDocument curWeek, nextWeek;
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
                    XDocument xml = XDocument.Load(reader);
                    var gms = (
                        from item in xml.Root.Descendants("gms")
                        select new
                        {
                            type = item.Attribute("t").Value,
                            year = item.Attribute("y").Value,
                            week = item.Attribute("w").Value
                        }).First();
                    string gameType;
                    switch (gms.type)
                    {
                        case "R":
                            gameType = "REG";
                            break;
                        case "P":
                            gameType = "PRE";
                            break;
                        default:
                            gameType = "";
                            break;
                    }
                    HttpWebRequest curWeekRequest = (HttpWebRequest)WebRequest.Create(string.Format("http://www.nfl.com/ajax/scorestrip?season={0}&seasonType={1}&week={2}", gms.year, gameType, gms.week));
                    using (HttpWebResponse subResponse = (HttpWebResponse)curWeekRequest.GetResponse())
                    {
                        StreamReader subReader = new StreamReader(subResponse.GetResponseStream());
                        curWeek = XDocument.Load(subReader);
                    }
                    HttpWebRequest nextWeekRequest = (HttpWebRequest)WebRequest.Create(string.Format("http://www.nfl.com/ajax/scorestrip?season={0}&seasonType={1}&week={2}", gms.year, gameType, int.Parse(gms.week) + 1));
                    using (HttpWebResponse subResponse = (HttpWebResponse)nextWeekRequest.GetResponse())
                    {
                        StreamReader subReader = new StreamReader(subResponse.GetResponseStream());
                        nextWeek = XDocument.Load(subReader);
                    }
                }
            } catch (Exception e)
            {
                logger.Debug(e.Message);
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
            // First get the date and time for this week's game. If it has already passed, go to next week's game.
            var data = from item in curWeek.Descendants("g")
                       where (string)item.Attribute("v") == teamAbbr || (string)item.Attribute("h") == teamAbbr
                       select new
                       {
                           eid = item.Attribute("eid").Value,
                           home = item.Attribute("h").Value,
                           away = item.Attribute("v").Value,
                           time = item.Attribute("t").Value,
                           day = item.Attribute("d").Value
                       };
            var p = data.FirstOrDefault();
            if ((p == null) || (DatePassed(p.eid.ToString(), p.time.ToString())))  // if p is null then there is probably a Bye week for the selected team
            {
                data = from item in nextWeek.Descendants("g")
                           where (string)item.Attribute("v") == teamAbbr || (string)item.Attribute("h") == teamAbbr
                           select new
                           {
                               eid = item.Attribute("eid").Value,
                               home = item.Attribute("h").Value,
                               away = item.Attribute("v").Value,
                               time = item.Attribute("t").Value,
                               day = item.Attribute("d").Value
                           };
                p = data.FirstOrDefault();
            }
            MatchData matchData = new MatchData();
            if (p != null)
            {
                matchData.eid = p.eid.ToString();
                matchData.home = p.home.ToString();
                matchData.away = p.away.ToString();
                matchData.day = p.day.ToString();
                matchData.time = p.time.ToString();
            }
            else   // if p is still null here, there is something wrong...
            {
                logger.Fatal("Unable to find next match for selected team!");
                MessageBox.Show("Unable to find next match for selected team!", "Fatal error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return matchData;
        }

        private bool DatePassed(String eid, String time)
        {
            return ConvertToLocalTime(eid, time) < DateTime.Now;
        }

        private DateTime ConvertToLocalTime(String eid, String time)
        {
            int h, m;
            DateTime easternTime, localTime;

            string dateString = eid.Substring(0, 8);
            string pacificZoneId = "Eastern Standard Time";
            TimeZoneInfo pacificZone = TimeZoneInfo.FindSystemTimeZoneById(pacificZoneId);

            easternTime = DateTime.ParseExact(dateString, "yyyyMMdd", null);
            int.TryParse(time.Substring(0, time.IndexOf(":")), out h);
            h += 12;
            int.TryParse(time.Substring(time.IndexOf(":") + 1), out m);
            easternTime = easternTime.Add(new TimeSpan(h, m, 0));
            DateTime UTC = TimeZoneInfo.ConvertTimeToUtc(easternTime, pacificZone);
            localTime = UTC.ToLocalTime();
            return localTime;
        }

        private void DrawText(Graphics graphics, string text, Font font, RectangleF rect, StringFormat format, float offset)
        {
            rect.Offset(offset, offset);
            graphics.DrawString(text, font, Brushes.Black, rect, format);
            rect.Offset(-offset, -offset);
            graphics.DrawString(text, font, Brushes.White, rect, format);
        }

        public Image GenerateWallpaper(MatchData data, string background)
        {
            string awayText = GetTeamCity(data.away).ToUpper();
            string awayTeam = GetTeamName(data.away).ToUpper();
            string homeText = GetTeamCity(data.home).ToUpper();
            string homeTeam = GetTeamName(data.home).ToUpper();
            CultureInfo enUS = new CultureInfo("en-US");
            PointF awayCityLocation = new PointF(0f, 400f);
            PointF awayTeamLocation = new PointF(-20f, 440f);
            var assembly = typeof(NFLWallpaper.Program).Assembly;
            string[] names = assembly.GetManifestResourceNames();
            Image image;
            if (background.Contains("\\"))
            {
                image = Image.FromFile(background);
            } else {
                image = Image.FromStream(assembly.GetManifestResourceStream("NFLWallpaper.Resources.Background." + background + ".jpg"));
            }
            if (image != null) {
                double backgroundWidth = image.Width;
                double backgroundHeight = image.Height;
                Graphics graphics = Graphics.FromImage(image);
                graphics.TextRenderingHint = TextRenderingHint.AntiAlias;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.SmoothingMode = SmoothingMode.AntiAlias;

                using (Font teamFont = new Font(pfc.Families[0], 150, FontStyle.Bold, GraphicsUnit.Pixel),
                            cityFont = new Font(pfc.Families[1], 50, FontStyle.Bold, GraphicsUnit.Pixel),
                            dayFont = new Font(pfc.Families[1], 50, FontStyle.Bold, GraphicsUnit.Pixel))
                {
                    StringFormat format = new StringFormat(StringFormat.GenericTypographic);
                    float height = cityFont.Size * cityFont.FontFamily.GetLineSpacing(FontStyle.Bold) / cityFont.FontFamily.GetEmHeight(FontStyle.Bold);
                    height += teamFont.Size * teamFont.FontFamily.GetCellAscent(FontStyle.Bold) / teamFont.FontFamily.GetEmHeight(FontStyle.Bold);
                    RectangleF rect = new RectangleF(0, (1300 - height) / 2, 600, height + 10);
                    format.Alignment = StringAlignment.Center;
                    format.LineAlignment = StringAlignment.Near;
                    DrawText(graphics, awayText, cityFont, rect, format, 5f);
                    format.LineAlignment = StringAlignment.Far;
                    DrawText(graphics, awayTeam, teamFont, rect, format, 5f);
                    rect = new RectangleF(1000, (1300 - height) / 2, 600, height + 10);
                    format.LineAlignment = StringAlignment.Near;
                    DrawText(graphics, homeText, cityFont, rect, format, 5f);
                    format.LineAlignment = StringAlignment.Far;
                    DrawText(graphics, homeTeam, teamFont, rect, format, 5f);
                    DateTime localTime = ConvertToLocalTime(data.eid, data.time);
                    rect = new RectangleF(0, 50, 1600, 120);
                    format.Alignment = StringAlignment.Center;
                    format.LineAlignment = StringAlignment.Near;
                    DrawText(graphics, localTime.ToString("D", enUS), dayFont, rect, format, 5f);
                    format.LineAlignment = StringAlignment.Far;
                    DrawText(graphics, localTime.ToString("t"), dayFont, rect, format, 5f);
                }
                Image helmet;
                helmet = Image.FromStream(assembly.GetManifestResourceStream("NFLWallpaper.Resources.Helmets." + data.home + ".png"));
                helmet.RotateFlip(RotateFlipType.RotateNoneFlipX);
                graphics.DrawImage(helmet, new RectangleF((600 - 280) / 2 + 1000, 300, 280, 212));
                helmet = Image.FromStream(assembly.GetManifestResourceStream("NFLWallpaper.Resources.Helmets." + data.away + ".png"));
                graphics.DrawImage(helmet, new RectangleF((600 - 280) / 2, 300, 280, 212));

                //            image.Save(@"C:\Temp\test.jpg");
                return image;
            } else
            {
                return null;
            }
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