using System;
using System.IO;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.Text;
using ModernMUD;

namespace MUDEngine
{
    /// <summary>
    /// Represents system/world information and game behavior flags.
    /// </summary>
    [Serializable]
    public class Sysdata
    {
        private bool _gameIsDown = false; /* Shutdown */
        private bool _gameIsWizlocked = false; /* Game is wizlocked */
        private int _numlockLevel = 0; /* Game is numlocked at <level> */
        private DateTime _gameBootTime = DateTime.Now;
        private DateTime _lastPlayerCount = DateTime.MinValue;
        private int _numPlayers = 0;
        private int _maxPlayers = 0; /* Maximum players this boot */
        private int _maxPlayersEver = 0; /* Maximum players ever */
        private DateTime _maxPlayersTime; /* Time of max players ever */
        private MudFlags _actFlags = MudFlags.autoprice | MudFlags.equipmentdamage; /* Mud act flags */
        private DateTime _shutdownTime; /* Shutdown/reboot time */
        private bool _shutdownIsScheduled = false; // Is there a scheduled shutdown?
        private DateTime _currentTime = DateTime.Now; /* Current time */
        private int _gameHour = 0; /* in-game _gameHour */
        private int _gameDay = 0; /* in-game _gameDay */
        private int _gameMonth = 0; /* in-game _gameMonth */
        private int _gameYear = 1; /* in-game _gameYear */
        private Weather _weather; /* Weather information */
        private string _bannedNames = String.Empty; /* Banned names */
        private int _totalBountiesPlaced = 0;
        private string _mudName = "Basternae";
        private string _mudAnsiName = "&+RBasternae&n";
        public int TotalBountiesPlacedValue { get; set; }
        public int TotalBountyFeesCharged { get; set; }
        public int TotalBountiesClaimed { get; set; }
        public int TotalBountiesClaimedValue { get; set; }
        public int TotalBountiesExpired { get; set; }
        public int TotalBountiesExpiredValue { get; set; }
        private List<NewsEntry> _newsEntries;

        /// <summary>
        /// The name of the MUD in plain text.
        /// </summary>
        public string MudName
        {
            get
            {
                return _mudName;
            }
            set
            {
                _mudName = value;
            }
        }

        /// <summary>
        /// The name of the MUD in ANSI color.
        /// </summary>
        public string MudAnsiName
        {
            get
            {
                return _mudAnsiName;
            }
            set
            {
                _mudAnsiName = value;
            }
        }

        /// <summary>
        /// MUD configuration flags.
        /// </summary>
        [Flags]
        public enum MudFlags
        {
            none = 0,
            /// <summary>
            /// Locked to new players.
            /// </summary>
            newlock = Bitvector.BV00,
            /// <summary>
            /// For quick leveling prior to player wipe.
            /// </summary>
            turbolevel = Bitvector.BV01,
            /// <summary>
            /// Equipment damages and degrades with use.
            /// </summary>
            equipmentdamage = Bitvector.BV02,
            /// <summary>
            /// Capture the flag mode.
            /// </summary>
            capturetheflag = Bitvector.BV03,
            /// <summary>
            /// Equipment quality effects PvP - naked raiding/stealing won't work well.
            /// </summary>
            equipmentpvp = Bitvector.BV04,
            /// <summary>
            /// Strict or lenient equipment requirements for PvP.
            /// </summary>
            stricteqpvp = Bitvector.BV05,
            /// <summary>
            /// Mobs' casting is limited by spell slots.
            /// </summary>
            mobcastslots = Bitvector.BV06,
            /// <summary>
            /// Do mobs loot players?
            /// </summary>
            mobslootplayers = Bitvector.BV07,
            /// <summary>
            /// Does the MUD override equipment prices set within zone files?
            /// </summary>
            autoprice = Bitvector.BV08,
            /// <summary>
            /// Can players walk on the ocean?
            /// </summary>
            walkableocean = Bitvector.BV09,
            /// <summary>
            /// Use the enhanced exp system.
            /// </summary>
            powerlevel = Bitvector.BV10,
            /// <summary>
            /// Use to enable/disable character deletion.
            /// </summary>
            candelete = Bitvector.BV11,
            /// <summary>
            /// Restrict players from using equipment far higher level than them.
            /// </summary>
            levelrestrictedeq = Bitvector.BV12,
            /// <summary>
            /// Require players to wait for name approval.
            /// </summary>
            nameapproval = Bitvector.BV13,
            /// <summary>
            /// Turns off MCCP mud-wide 
            /// </summary>
            disablemccp = Bitvector.BV14,
            /// <summary>
            /// Turns off MSP mud-wide. 
            /// </summary>
            disablemsp = Bitvector.BV15,
            /// <summary>
            /// Turns off faction adjustments.
            /// </summary>
            disablefaction = Bitvector.BV16, 
            /// <summary>
            /// Only adjusts faction for mobs that have the ACT_FACTION bit set.
            /// </summary>
            checkfactionbit = Bitvector.BV17, 
            /// <summary>
            /// Always give characters a set of newbie equipment when they die.
            /// </summary>
            alwaysequip = Bitvector.BV18 
        }

        /// <summary>
        /// Day names.
        /// </summary>
        [XmlIgnore]
        public readonly string[] DayName = new[]
        {
            "the Moon", "the Scythe", "Toil", "the Hammer", "Freedom", "the Gods", "the Sun"
        };

        /// <summary>
        /// Month names.
        /// </summary>
        [XmlIgnore]
        public readonly string[] MonthName = new[]
        {
            "Hoarfrost", "the White Bear", "the Old Gods",
            "the Sundering", "the Thaw", "the Wyrm",
            "the Clear Sky", "the Burning", "the Beast",
            "the Spirits", "the Long Shadows", "the Coming Darkness",
            "the Long Darkness"
        };

        public bool GameIsDown
        {
            get
            {
                return _gameIsDown; 
            }
            set
            {
                _gameIsDown = value; 
            }
        }

        public DateTime GameBootTime
        {
            get { return _gameBootTime; }
            set { _gameBootTime = value; }
        }

        /// <summary>
        /// Can only admins/immortals log in?
        /// </summary>
        public bool GameIsWizlocked
        {
            get { return _gameIsWizlocked; }
            set { _gameIsWizlocked = value; }
        }

        public int TotalBountiesPlaced
        {
            get { return _totalBountiesPlaced; }
            set { _totalBountiesPlaced = value; }
        }

        public int NumlockLevel
        {
            get { return _numlockLevel; }
            set { _numlockLevel = value; }
        }

        /// <summary>
        /// Don't serialize this -- it's supposed to be zero each boot.
        /// </summary>
        [XmlIgnore]
        public int NumPlayers
        {
            get { return _numPlayers; }
            set { _numPlayers = value; }
        }

        /// <summary>
        /// Don't serialize this -- it's the per-boot maximum.
        /// </summary>
        [XmlIgnore]
        public int MaxPlayers
        {
            get { return _maxPlayers; }
            set { _maxPlayers = value; }
        }

        /// <summary>
        /// Entries displayed when a player types the "news" command.
        /// </summary>
        public List<NewsEntry> NewsEntries
        {
            get
            {
                return _newsEntries;
            }
            set
            {
                _newsEntries = value;
            }
        }

        /// <summary>
        /// This is the ultimate record maximum players ever for the game.
        /// </summary>
        public int MaxPlayersEver
        {
            get { return _maxPlayersEver; }
            set { _maxPlayersEver = value; }
        }

        public DateTime MaxPlayersTime
        {
            get { return _maxPlayersTime; }
            set { _maxPlayersTime = value; }
        }

        public MudFlags ActFlags
        {
            get { return _actFlags; }
            set { _actFlags = value; }
        }

        public DateTime ShutdownTime
        {
            get { return _shutdownTime; }
            set { _shutdownTime = value; }
        }

        public bool ShutdownIsScheduled
        {
            get { return _shutdownIsScheduled; }
            set { _shutdownIsScheduled = value; }
        }

        public DateTime CurrentTime
        {
            get { return _currentTime; }
            set { _currentTime = value; }
        }

        public int GameHour
        {
            get { return _gameHour; }
            set { _gameHour = value; }
        }

        public int GameDay
        {
            get { return _gameDay; }
            set { _gameDay = value; }
        }

        public int GameMonth
        {
            get { return _gameMonth; }
            set { _gameMonth = value; }
        }

        public int GameYear
        {
            get { return _gameYear; }
            set { _gameYear = value; }
        }

        public Weather WeatherData
        {
            get { return _weather; }
            set { _weather = value; }
        }

        public string BannedNames
        {
            get { return _bannedNames; }
            set { _bannedNames = value; }
        }

        /// <summary>
        /// Sets up gametime and weather status at boot time.
        /// </summary>
        public void SetWeather()
        {
            if (!WeatherData)
            {
                WeatherData = new Weather();
            }

            // TODO: Let players configure the length of day and moon cycles on their MUD.
            if (GameHour < 4)
            {
                WeatherData.Sunlight = SunType.moonset;
            }
            else if (GameHour < 6)
            {
                WeatherData.Sunlight = SunType.sunrise;
            }
            else if (GameHour < 19)
            {
                WeatherData.Sunlight = SunType.daytime;
            }
            else if (GameHour < 20)
            {
                WeatherData.Sunlight = SunType.sunset;
            }
            else if (GameHour < 23)
            {
                WeatherData.Sunlight = SunType.night;
            }
            else
            {
                WeatherData.Sunlight = SunType.moonrise;
            }

            WeatherData.Change = 0;
            WeatherData.BarometricPressure = 960;
            if (GameMonth >= 7 && GameMonth <= 12)
            {
                WeatherData.BarometricPressure += MUDMath.NumberRange(1, 50);
            }
            else
            {
                WeatherData.BarometricPressure += MUDMath.NumberRange(1, 80);
            }

            if (WeatherData.BarometricPressure <= 980)
            {
                WeatherData.Sky = SkyType.thunderstorm;
            }
            else if (WeatherData.BarometricPressure <= 1000)
            {
                WeatherData.Sky = SkyType.rain;
            }
            else if (WeatherData.BarometricPressure <= 1020)
            {
                WeatherData.Sky = SkyType.cloudy;
            }
            else
            {
                WeatherData.Sky = SkyType.clear;
            }

            WeatherData.MoonPhase = MoonPhase.new_moon;
            WeatherData.Moonday = 0;
        }

        /// <summary>
        /// Loads the system data from a file.
        /// </summary>
        /// <returns></returns>
        public static bool Load()
        {

            string filename = FileLocation.SystemDirectory + FileLocation.SysdataFile;
            string blankFilename = FileLocation.BlankSystemFileDirectory + FileLocation.SysdataFile;
            XmlSerializer serializer = new XmlSerializer(typeof(CorpseData));
            Log.Trace("Loading sysdata file: " + filename);
            Stream stream = null;
            try
            {
                try
                {
                    stream = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.None);
                }
                catch (FileNotFoundException)
                {
                    Log.Info("SystemData file not found, using blank file.");
                    File.Copy(blankFilename, filename);
                    stream = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.None);
                }
                Database.SystemData = (Sysdata)serializer.Deserialize(stream);
                stream.Close();
                return true;
            }
            catch (Exception ex)
            {
                Log.Error("Exception in Database.SystemData.Load(): " + ex);
                Database.SystemData = new Sysdata();
                return false;
            }
        }

        /// <summary>
        /// Saves the system data file to disk.
        /// </summary>
        public static void Save()
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer( typeof(Sysdata) );
                Stream stream = new FileStream(FileLocation.SystemDirectory + FileLocation.SysdataFile, FileMode.Create,
                    FileAccess.Write, FileShare.None );
                serializer.Serialize( stream, Database.SystemData );
                stream.Close();
            }
            catch( Exception ex )
            {
                Log.Error( "There was an error saving Database.SystemData. Exception: " + ex );
            }
        }

        /// <summary>
        /// Logs the current player count to the player count file, for historical graphing and whatnot.
        /// </summary>
        public void UpdatePlayerCount()
        {
            DateTime time = DateTime.Now;
            if (time - _lastPlayerCount > TimeSpan.FromMinutes(58))
            {
                using (StreamWriter w = File.AppendText(FileLocation.SystemDirectory + FileLocation.PlayerCountFile))
                {
                    w.WriteLine(time.Month + "/" + time.Day + "/" + time.Year + " " + time.Hour + ":00," + time.DayOfWeek + "," + _numPlayers);
                    w.Flush();
                    w.Close();
                }
                _lastPlayerCount = time;
            }
        }

        /// <summary>
        /// Updates the weather and game hour.  Also updates things that depend on the game hour.
        /// 
        /// Weather depends on the time of year.
        /// </summary>
        public void UpdateWeather()
        {
            string text = String.Empty;

            UpdatePlayerCount();

            switch( ++GameHour )
            {
                case 4:
                    WeatherData.Sunlight = SunType.moonset;
                    if (WeatherData.MoonPhase != MoonPhase.new_moon)
                    {
                        text = "&+cThe &+Cmoon&n&+c slowly &+Lvanishes&n&+c from the horizon.&n\r\n";
                    }
                    break;

                case 6:
                    WeatherData.Sunlight = SunType.sunrise;
                    text = "&+cThe first &+Clights&n&+c of &+md&+Maw&n&+mn &n&+cilluminate the&+C sky&n.\r\n";
                    WeatherData.Temperature += MUDMath.FuzzyNumber(10);
                    break;

                case 7:
                    WeatherData.Sunlight = SunType.daytime;
                    text = "&+MA new day has begun as the &+Ysun&+M rises&n.&n\r\n";
                    if (GameMonth <= 4 || GameMonth >= 15)
                    {
                        WeatherData.Temperature = MUDMath.FuzzyNumber(20);
                    }
                    else
                    {
                        WeatherData.Temperature = MUDMath.FuzzyNumber(50);
                    }
                    break;

                case 12:
                    text = "&+cThe &+Ysun&n &+cnow hangs at high noon&n.\r\n";
                    WeatherData.Temperature += MUDMath.FuzzyNumber(20);
                    break;

                case 19:
                    WeatherData.Sunlight = SunType.sunset;
                    text = "&+mThe &+Ysun&n&+m slowly slips off the &+Mhorizon&n.\r\n";
                    WeatherData.Temperature -= MUDMath.FuzzyNumber(20);
                    break;

                case 20:
                    WeatherData.Sunlight = SunType.night;
                    text = "&+LThe night begins as darkness settles across the lands&n.\r\n";
                    WeatherData.Temperature -= MUDMath.FuzzyNumber(10);
                    break;

                case Limits.HOURS_PER_DAY:
                    WeatherData.Sunlight = SunType.moonrise;
                    WeatherData.Temperature -= MUDMath.FuzzyNumber(10);
                    GameHour = 0;
                    GameDay++;

                    // Moon stuff
                    WeatherData.Moonday++;

                    if (WeatherData.Moonday >= LUNAR_CYCLE_DAYS)
                    {
                        WeatherData.MoonPhase = MoonPhase.new_moon;
                        WeatherData.Moonday = 0;
                    }
                    else
                    {
                        if( WeatherData.Moonday < (LUNAR_CYCLE_DAYS / 8))
                        {
                            WeatherData.MoonPhase = MoonPhase.new_moon;
                        }
                        else if( WeatherData.Moonday < (LUNAR_CYCLE_DAYS / 4))
                        {
                            WeatherData.MoonPhase = MoonPhase.quarter;
                        }
                        else if (WeatherData.Moonday < (LUNAR_CYCLE_DAYS * 3 / 8))
                        {
                            WeatherData.MoonPhase = MoonPhase.half;
                        }
                        else if (WeatherData.Moonday < (LUNAR_CYCLE_DAYS / 2))
                        {
                            WeatherData.MoonPhase = MoonPhase.three_quarter;
                        }
                        else if (WeatherData.Moonday < (LUNAR_CYCLE_DAYS * 5 / 8))
                        {
                            WeatherData.MoonPhase = MoonPhase.full;
                        }
                        else if (WeatherData.Moonday < (LUNAR_CYCLE_DAYS * 3 / 4))
                        {
                            WeatherData.MoonPhase = MoonPhase.three_quarter;
                        }
                        else if (WeatherData.Moonday < (LUNAR_CYCLE_DAYS * 7 / 8))
                        {
                            WeatherData.MoonPhase = MoonPhase.half;
                        }
                        else if (WeatherData.Moonday < (LUNAR_CYCLE_DAYS))
                        {
                            WeatherData.MoonPhase = MoonPhase.quarter;
                        }
                    }

                    switch (WeatherData.MoonPhase)
                    {
                        default:
                            break;
                        case MoonPhase.new_moon:
                            text += "&+LThe night sky is overshadowed by an uncommon darkness.&n\r\n";
                            break;
                        case MoonPhase.full:
                            text += "&n&+LThe &+Cmoon&n&+L rises full, casting a &n&+wsi&+Wlv&n&+wer &n&+cglow&+L across the entire sky.&n\r\n";
                            break;
                        case MoonPhase.three_quarter:
                            text += "&n&+LThe &+Cmoon&n&+L ascends, a small &n&+csliver&+L absent against the night sky.&n\r\n";
                            break;
                        case MoonPhase.half:
                            text += "&n&+LA giant half-circle, the &+Cmoon&n&+L rises against the blanket of night.&n\r\n";
                            break;
                        case MoonPhase.quarter:
                            text += "&n&+LThe &+Cmoon&n&+L rises, a &n&+wsi&+Wlv&n&+wer &+csliver&n&+L against the dark firmament.&n\r\n";
                            break;
                    }
                    break;
            }

            if( GameDay >= Limits.DAYS_PER_MONTH )
            {
                GameDay = 0;
                GameMonth++;
            }

            if( GameMonth >= Limits.MONTHS_PER_YEAR )
            {
                GameMonth = 0;
                GameYear++;
            }

            // Weather change.
            WeatherData.WindDirection += MUDMath.NumberRange(0, 2) - 1;

            int diff = 0;
            if (GameMonth >= 9 && GameMonth <= 16)
            {
                diff = WeatherData.BarometricPressure > 985 ? -2 : 2;
            }
            else
            {
                diff = WeatherData.BarometricPressure > 1015 ? -2 : 2;
            }

            WeatherData.Change += diff * MUDMath.Dice(1, 4) + MUDMath.Dice(2, 6) - MUDMath.Dice(2, 6);
            WeatherData.Change = Math.Max(WeatherData.Change, -12);
            WeatherData.Change = Math.Min(WeatherData.Change, 12);

            WeatherData.BarometricPressure += WeatherData.Change;
            WeatherData.BarometricPressure = Math.Max(WeatherData.BarometricPressure, 960);
            WeatherData.BarometricPressure = Math.Min(WeatherData.BarometricPressure, 1040);

            switch (WeatherData.Sky)
            {
                default:
                    Log.Error( "WeatherUpdate: bad sky {0}.", WeatherData.Sky );
                    WeatherData.Sky = SkyType.clear;
                    break;

                case SkyType.clear:
                    if (WeatherData.BarometricPressure < 990 || (WeatherData.BarometricPressure < 1010 && MUDMath.NumberBits(2) == 0))
                    {
                        if (GameMonth <= 3 || GameMonth >= 11)
                        {
                            text += "&+wA few &+Wf&n&+wl&+Wa&n&+wk&+We&n&+ws of &+Ws&n&+wn&+Wo&n&+ww&+w are falling&n.\r\n";
                            WeatherData.Temperature -= 10;
                        }
                        else
                        {
                            text += "&+LStorm clouds &n&+mthunder&+L in the distance&n.\r\n";
                        }
                        WeatherData.Sky = SkyType.cloudy;
                        WeatherData.WindSpeed += 10;
                    }
                    break;

                case SkyType.cloudy:
                    if (WeatherData.BarometricPressure < 970 || (WeatherData.BarometricPressure < 990 && MUDMath.NumberBits(2) == 0))
                    {
                        if (GameMonth <= 3 || GameMonth >= 11)
                        {
                            text += "&+wThe &+Wharsh s&n&+wn&+Wo&n&+ww-&+Lstorm&n&+w makes visibility difficult&n.\r\n";
                            WeatherData.Temperature -= 10;
                        }
                        else
                        {
                            text += "&+cSmall drops of &+Crain&n&+w m&+Wis&n&+wt the air&n.\r\n";
                        }
                        WeatherData.Sky = SkyType.rain;
                        WeatherData.WindSpeed += 10;
                    }

                    if (WeatherData.BarometricPressure > 1030 && MUDMath.NumberBits(2) == 0)
                    {
                        if (GameMonth <= 3 || GameMonth >= 11)
                        {
                            text += "&+wThe &+Wsnow&n&+w-&+Lstorm&n&+w seems to settle&n.\r\n";
                            WeatherData.Temperature += 10;
                        }
                        else
                        {
                            text += "&+cThe &+Cclouds&n&+c disappear from the skyline&n.\r\n";
                        }
                        WeatherData.Sky = SkyType.clear;
                        WeatherData.WindSpeed -= 10;
                    }
                    break;

                case SkyType.rain:
                    if (WeatherData.BarometricPressure < 970 && MUDMath.NumberBits(2) == 0)
                    {
                        if( GameMonth <= 3 || GameMonth >= 11 )
                        {
                            text += "&+wThe &+Wsnow-&+cstorm&n&+w has evolved into a full &+Cblizzard&n.\r\n";
                            WeatherData.Temperature -= 30;
                        }
                        else
                            text += "&+WLightning flashes across the sky&n.\r\n";
                        WeatherData.Sky = SkyType.thunderstorm;
                        WeatherData.WindSpeed += 10;
                    }

                    if (WeatherData.BarometricPressure > 1030 || (WeatherData.BarometricPressure > 1010 && MUDMath.NumberBits(2) == 0))
                    {
                        if (GameMonth <= 3 || GameMonth >= 11)
                        {
                            text += "&+wThe &+Ws&n&+wn&+Wo&n&+ww seems to be letting up&n.\r\n";
                            WeatherData.Temperature += 30;
                        }
                        else
                        {
                            text += "&+cThe &+Crain&n&+c slows to a drizzle then quits&n.\r\n";
                        }
                        WeatherData.Sky = SkyType.cloudy;
                        WeatherData.WindSpeed -= 10;
                    }
                    break;

                case SkyType.thunderstorm:
                    if (WeatherData.BarometricPressure > 1010 || (WeatherData.BarometricPressure > 990 && MUDMath.NumberBits(2) == 0))
                    {
                        if (GameMonth <= 3 || GameMonth >= 11)
                        {
                            text += "&+wThe &+Wblizzard&n&+w subsides&n.\r\n";
                            WeatherData.Temperature += 10;
                        }
                        else
                        {
                            text += "&n&+wThe &+Lthunder &N&+wand &+Clightning&n&+w has stopped&N.\r\n";
                        }
                        WeatherData.Sky = SkyType.rain;
                        WeatherData.WindSpeed -= 10;
                        break;
                    }
                    break;
            }

            if( text.Length > 0 )
            {
                foreach( SocketConnection socket in Database.SocketList )
                {
                    if (socket.ConnectionStatus == SocketConnection.ConnectionState.playing
                        && socket.Character.IsOutside() && !socket.Character.IsUnderground()
                        && socket.Character.IsAwake() && !socket.Character.InRoom.HasFlag(RoomTemplate.ROOM_NO_PRECIP))
                    {
                        socket.Character.SendText(text);
                    }
                }
            }

            foreach( SocketConnection playerSocket in Database.SocketList )
            {
                if ((playerSocket.ConnectionStatus == SocketConnection.ConnectionState.playing) &&
                    !playerSocket.Character.IsNPC())
                {
                    if (((PC)playerSocket.Character).FirstaidTimer > 0)
                    {
                        ((PC)playerSocket.Character).FirstaidTimer -= 1;
                    }
                }
            }

            return;
        }

        /// <summary>
        /// Retrieves the news entries in a printable format.
        /// </summary>
        /// <returns></returns>
        public string ShowNewsEntries()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("&+YNEWS&n\r\n&+BXangis&n.\r\n\r\n");
            foreach (NewsEntry news in NewsEntries)
            {
                sb.Append("&+r" + news.Date + "&n\r\n");
                sb.Append(news.ColorCode + news.Content + "&n\r\n\r\n");
            }
            return sb.ToString();
        }

        /// <summary>
        /// Adds a new news entry to the list of news.
        /// </summary>
        /// <param name="authorName"></param>
        /// <param name="datePosted"></param>
        /// <param name="content"></param>
        /// <param name="colorCode"></param>
        public void AddNewsEntry(string authorName, string datePosted, string content, string colorCode)
        {
            NewsEntry ne = new NewsEntry();
            ne.Author = authorName;
            ne.Content = content;
            ne.Date = datePosted;
            ne.ColorCode = colorCode;
            if (_newsEntries == null)
                _newsEntries = new List<NewsEntry>();
            _newsEntries.Add(ne);
            _newsEntries.Sort();
            Save();
        }

        /// <summary>
        /// Represents phases of the moon.
        /// </summary>
        public enum MoonPhase
        {
            new_moon,
            quarter,
            half,
            three_quarter,
            full
        }

        [XmlIgnore]
        public readonly int MAX_MOON_PHASE = Enum.GetValues(typeof(MoonPhase)).Length;

        public const int LUNAR_CYCLE_DAYS = 32;

        /// <summary>
        /// Represents the state of the sky.
        /// </summary>
        public enum SkyType
        {
            clear,
            partly_cloudy,
            cloudy,
            dark_clouds,
            drizzle,
            rain,
            thunderstorm,
            snow,
            hail,
            freezing_rain,
            tornado,
            hurricane,
            blizzard
        }
    }
}