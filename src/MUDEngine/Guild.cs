using System;
using System.IO;
using System.Xml.Serialization;
using ModernMUD;

namespace MUDEngine
{
    /// <summary>
    /// Represents an in-game player organization (guild, clan, order, gang, etc.)
    /// </summary>
    [Serializable]
    public class Guild
    {
        /// <summary>
        /// Latest guild applicant.
        /// </summary>
        public CharData Applicant { get; set; }
        /// <summary>
        /// Guild filename.
        /// </summary>
        public string Filename { set; get; }
        /// <summary>
        /// Guild name on the who list.
        /// </summary>
        public string WhoName { set; get; }
        /// <summary>
        /// Guild name.
        /// </summary>
        public string Name { set; get; }
        /// <summary>
        /// Guild motto.
        /// </summary>
        public string Motto { set; get; }
        /// <summary>
        /// Guild's brief description.
        /// </summary>
        public string Description;
        /// <summary>
        /// Leader's Name.
        /// </summary>
        public string Overlord { get; set; }
        /// <summary>
        /// Rank, join date, fines, other member data.
        /// </summary>
        public GuildMemberData[] Members = new GuildMemberData[ Limits.MAX_GUILD_MEMBERS ];
        /// <summary>
        /// Titles for each guild rank.
        /// </summary>
        public string[] RankNames = new string[ Enum.GetValues(typeof(Rank)).Length ];
        /// <summary>
        /// Faction standings with individual races.  This is a "how they feel about us" value.
        /// </summary>
        public double[] RaceFactionStandings = new double[Limits.MAX_RACE];
        /// <summary>
        /// The ostracized (attack on sight) list.
        /// </summary>
        public string Ostracized { set; get; }
        /// <summary>
        /// The money in the guild's bank account.
        /// </summary>
        public Coins GuildBankAccount { set; get; }
        /// <summary>
        /// Number of total frags guild has.
        /// </summary>
        public int Frags { get; set; }
        /// <summary>
        /// Number of player kills by guild members.
        /// </summary>
        public int PlayerKills;
        /// <summary>
        /// Number of PvP deaths by guild members.
        /// </summary>
        public int PlayerDeaths;
        /// <summary>
        /// Number of monsters killed by guild members.
        /// </summary>
        public int MonsterKills;
        /// <summary>
        /// Number of times guild members have been killed by monsters.
        /// </summary>
        public int MonsterDeaths;
        /// <summary>
        /// Number of times that the guild has killed illegally in a justice area.
        /// </summary>
        public int IllegalJusticeKills;
        /// <summary>
        /// Score of the guild, used for ranking.
        /// </summary>
        public double Score;
        /// <summary>
        /// The kind of guild this is (guild, clan, etc.)
        /// </summary>
        public GuildType TypeOfGuild { get; set; }
        /// <summary>
        /// Number of guild members.
        /// </summary>
        public int NumMembers;
        /// <summary>
        /// Index number of the guild ring object.
        /// </summary>
        public int GuildRingIndexNumber;
        /// <summary>
        /// Index number of the guild shield object.
        /// </summary>
        public int GuildShieldIndexNumber;
        /// <summary>
        /// Index number of the guild weapon.
        /// </summary>
        public int GuildWeaponIndexNumber;
        /// <summary>
        /// Index number of the guild badge.
        /// </summary>
        public int GuildBadgeIndexNumber;
        /// <summary>
        /// Index number of the guild respawn point.
        /// </summary>
        public int RecallRoom;
        /// <summary>
        /// Index number of the guild's guild chest.
        /// </summary>
        public int GuildChest;
        /// <summary>
        /// The class that this guild is restricted to, if any.
        /// </summary>
        public CharClass ClassRestriction { get; set; }
        /// <summary>
        /// Unique id.  Used for differentiating golems.
        /// </summary>
        public int ID { get; set; }
        private static int _nextGuildID;

        /// <summary>
        /// Gets the next available guild ID.
        /// </summary>
        public static int NextGuildID
        { 
            get{ return _nextGuildID; }
            set{ _nextGuildID = value; }
        }

        /// <summary>
        /// Player ranking within the guild.
        /// </summary>
        public enum Rank
        {
            exiled = 0,
            parole,
            normal,
            senior,
            officer,
            deputy,
            leader
        }

        /// <summary>
        /// Defines the different types of guild.
        /// </summary>
        public enum GuildType
        {
            undefined = 0,
            clan,
            guild,
            order
        }

        private static int _numGuilds;

        /// <summary>
        /// Constructor. Increments the number of guilds in memory.
        /// </summary>
        public Guild()
        {
            ++_numGuilds;
        }

        /// <summary>
        /// Destructor. Decrements the number of guilds in memory.
        /// </summary>
        ~Guild()
        {
            --_numGuilds;
        }

        /// <summary>
        /// Gets the number of guilds currently in memory.
        /// </summary>
        public static int Count
        {
            get
            {
                return _numGuilds;
            }
        }

        /// <summary>
        /// Looks up a guild by name and returns it.  Uses a prefix match.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static Guild GetGuild(string name)
        {
            foreach (Guild guild in Database.GuildList)
            {
                if (guild.Name.StartsWith(name, StringComparison.CurrentCultureIgnoreCase))
                {
                    return guild;
                }
            }

            return null;
        }

        /// <summary>
        /// New code for loading a single guild from a file.
        /// </summary>
        /// <param name="filename">The full pathname of the file to load.</param>
        public static Guild Load( String filename )
        {
            XmlSerializer serializer = new XmlSerializer( typeof(Guild) );
            // Filename should have already been built using the FileLocation.GuildDirectory path.
            Stream stream = new FileStream( filename, FileMode.Open, FileAccess.Read, FileShare.None );
            Guild guild = (Guild)serializer.Deserialize( stream );
            stream.Close();
            return guild;
        }

        /// <summary>
        /// Loads in all of the guild files.
        /// </summary>
        public static void LoadGuilds()
        {
            string filename;

            string guildsList = String.Format("{0}{1}", FileLocation.GuildDirectory, FileLocation.GuildLoadList);
            Log.Trace( "Loading guilds: " + guildsList);
            try
            {
                FileStream fpList = File.OpenRead( guildsList );
                StreamReader sw = new StreamReader( fpList );

                // Read next guild ID.
                bool success = Int32.TryParse( sw.ReadLine(), out _nextGuildID );
                if( !success )
                {
                    Log.Error( "Guild.LoadGuilds(): Unable to read next guild ID from guild list." );
                }

                while( !sw.EndOfStream )
                {
                    filename = sw.ReadLine();

                    if( filename.Length == 0 || !MUDString.StringsNotEqual( filename, "$" ) )
                    {
                        break;
                    }

                    Guild guild = Load(FileLocation.GuildDirectory + filename);
                    if( guild != null )
                    {
                        Database.GuildList.Add( guild );
                    }
                }
                sw.Close();
            }
            catch( Exception ex )
            {
                Log.Error( "Exception in Guild.LoadGuilds(): " + ex );
            }
            return;
        }

        /// <summary>
        /// Saves the list of in-game guilds.
        /// </summary>
        public static void SaveGuildList()
        {
            string guildslist = String.Format("{0}{1}", FileLocation.GuildDirectory, FileLocation.GuildLoadList);

            FileStream fp = File.OpenWrite( guildslist );
            StreamWriter sw = new StreamWriter( fp );

            sw.WriteLine( NextGuildID.ToString() );
            
            foreach( Guild guild in Database.GuildList )
            {
                sw.WriteLine( guild.Filename );
            }

            sw.WriteLine( "$" );
            sw.Flush();
            sw.Close();

            return;
        }

        /// <summary>
        /// New code for writing a single guild to a file.
        /// </summary>
        /// <param name="filename">The full pathname of the file to CharData.</param>
        public void Save(string filename)
        {
            XmlSerializer serializer = new XmlSerializer( GetType() );
            // Filename should have already been built using the FileLocation.GuildDirectory path.
            Stream stream = new FileStream( filename, FileMode.Create, FileAccess.Write, FileShare.None );
            serializer.Serialize( stream, this );
            stream.Close();   
        }

        /// <summary>
        /// Saves a guild file to the filename stored in the guild data.
        /// </summary>
        public void Save()
        {
            string filename = FileLocation.GuildDirectory + this.Filename;
            Save( filename );
        }

        /// <summary>
        /// Gets the guild that a golem is associated with.
        /// </summary>
        /// <param name="ch"></param>
        /// <returns></returns>
        public static int GolemGuildID( CharData ch )
        {
            int pos = ch.Name.IndexOf( "guild_" );
            if( pos == -1 )
            {
                return 0;
            }
            int val;
            Int32.TryParse( ch.Name.Substring(pos), out val);
            return val;
        }
    };


}