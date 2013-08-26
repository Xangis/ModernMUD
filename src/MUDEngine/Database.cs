using System;
using System.Collections.Generic;
using System.IO;
using HelpData;
using ModernMUD;

namespace MUDEngine
{
    public class Database
    {
        public static List<Help> HelpList = new List<Help>();
        // Because we need to keep track of the last area.
        public static readonly LinkedList<Area> AreaList = new LinkedList<Area>();
        public static readonly List<Vehicle> VehicleList = new List<Vehicle>();
        public static readonly List<BanData> BanList = new List<BanData>();
        public static readonly List<Event> EventList = new List<Event>();
        public List<ZoneConnection> ZoneConnectionList = new List<ZoneConnection>();
        public static List<Crime> CrimeList = new List<Crime>();
        public static List<Message> NoteList = new List<Message>();
        public static readonly List<CastData> CastList = new List<CastData>(); // To keep track of everyone casting spells
        public static readonly List<Guild> GuildList = new List<Guild>();
        public static List<Issue> IssueList = new List<Issue>();
        public static readonly List<CharData> CharList = new List<CharData>();
        public static readonly List<Object> ObjectList = new List<Object>();
        public static readonly List<SocketConnection> SocketList = new List<SocketConnection>();
        public static List<ChatterBot> ChatterBotList = new List<ChatterBot>();
        public static readonly Dictionary<String, Song> SongList = new Dictionary<String, Song>();
        public static readonly Dictionary<String, MonkSkill> MonkSkillList = new Dictionary<String, MonkSkill>();
        public static Socials SocialList;
        public static CorpseData CorpseList;
        public static Screen ScreenList;
        public static bool Reboot;
        public static Sysdata SystemData;

        public static int HighestRoomIndexNumber { get; set; }
        public static int HighestMobIndexNumber { get; set; }
        public static int HighestObjIndexNumber { get; set; }
        public static bool DatabaseIsBooting { get; set; }

        /// <summary>
        /// Loads the entire MUD database including all classes, races, areas, helps, sytem data, etc.
        /// </summary>
        public void LoadDatabase()
        {
            DatabaseIsBooting = true;

            Log.Trace( DateTime.Now.ToShortDateString() + " BOOT: -------------------------[ Boot Log ]-------------------------" );

            Log.Trace("Validating that player directories exist.");
            for (Char letter = 'a'; letter <= 'z'; letter++)
            {
                String directory = FileLocation.PlayerDirectory + letter;
                if (!Directory.Exists(directory))
                {
                    Log.Trace("Creating directory: " + directory + ".");
                    Directory.CreateDirectory(directory);
                }
            }
            Log.Trace("Player directories validated.");

            Log.Trace( "Loading Database.SystemData." );
            Sysdata.Load();
            SystemData.CurrentTime = DateTime.Now;
            SystemData.GameBootTime = SystemData.CurrentTime;

            // Set time and weather.
            Log.Trace( "Setting time and weather." );
            SystemData.SetWeather();

            Log.Trace("Loading static rooms.");
            StaticRooms.Load();
            Log.Trace("Loaded " + StaticRooms.Count + " static rooms.");

            Log.Trace("Loading spells.");
            Spell.LoadSpells();
            Log.Trace("Loaded " + Spell.SpellList.Count + " spells.");

            Log.Trace("Loading skills.");
            Skill.LoadSkills();
            Log.Trace("Loaded " + Skill.SkillList.Count + " skills.");

            Log.Trace( "Loading races." );
            Race.LoadRaces();
            Log.Trace( "Loaded " + Race.Count + " races." );

            Log.Trace( "Initializing skill Levels." );
            {
                int cclass;

                foreach (KeyValuePair<String, Skill> kvp in Skill.SkillList)
                {
                    for (cclass = 0; cclass < CharClass.ClassList.Length; cclass++)
                        kvp.Value.ClassAvailability[ cclass ] = Limits.LEVEL_LESSER_GOD;
                }
                foreach (KeyValuePair<String, Spell> kvp in Spell.SpellList)
                {
                    for (cclass = 0; cclass < CharClass.ClassList.Length; cclass++)
                        kvp.Value.SpellCircle[cclass] = Limits.MAX_CIRCLE + 3;
                }
            }

            Log.Trace( "Loading classes." );
            CharClass.LoadClasses(true);
            Log.Trace( "Loaded " + CharClass.Count + " classes." );

            Log.Trace("Assigning spell circles.");
            AssignSpellCircles();
            Log.Trace("Assigned spell circles.");

            Log.Trace( "Loading socials." );
            SocialList = Socials.Load();
            Log.Trace( "Loaded " + Social.Count + " socials." );

            Log.Trace( "Loading bans." );
            LoadBans();
            Log.Trace( "Loaded " + BanList.Count + " bans." );

            Log.Trace( "Loading help entries." );
            HelpList = Help.Load(FileLocation.SystemDirectory + FileLocation.HelpFile);
            Log.Trace( "Loaded " + Help.Count + " help entries." );

            Log.Trace( "Loading screens." );
            Screen.Load(FileLocation.SystemDirectory + FileLocation.ScreenFile);
            Log.Trace( "Loaded " + Screen.Count + " screens." );

            // Chatbots have to be loaded before mobs.
            Log.Trace( "Loading chatbots." );
            ChatterBot.Load();
            Log.Trace( "Loaded " + ChatterBot.Count + " chatbots." );

            // Read in all the area files.
            Log.Trace( "Reading in area files..." );
            LoadAreaFiles();
            Log.Trace( "Loaded " + Area.Count + " areas." );

            string buf = String.Format( "Loaded {0} mobs, {1} objects, {2} rooms, {3} shops, {4} helps, {5} resets, and {6} quests.",
                                        MobTemplate.Count, ObjTemplate.Count, Room.Count, Shop.Count, Help.Count, Reset.Count, QuestData.Count );
            Log.Trace( buf );

            Log.Trace( "Loading guilds." );
            Guild.LoadGuilds();
            Log.Trace( "Loaded " + Guild.Count + " guilds." );

            Log.Trace( "Loading corpses." );
            CorpseList = CorpseData.Load();
            Log.Trace( "Loaded " + CorpseData.Count + " corpses." );

            Log.Trace( "Loading crimes." );
            Crime.Load();
            Log.Trace( "Loaded " + Crime.Count + " crimes." );

            Log.Trace( "Loading fraglist." );
            FraglistData.Fraglist.Load();

            Log.Trace( "Loading issues." );
            Issue.Load();
            Log.Trace( "Loaded " + Issue.Count + " issues." );

            Log.Trace("Loading bounties.");
            Bounty.Load();
            Log.Trace("Loaded " + Bounty.Count + " bounties.");

            Log.Trace("Initializing movement parameters.");
            Movement.Initialize();
            Log.Trace("Movement parameters initialized.");

            // Only compile spells that have attached code.  Otherwise use default handlers.
            Log.Trace("Compiling spells.");
            foreach (KeyValuePair<String, Spell> kvp in Spell.SpellList)
            {
                if( !String.IsNullOrEmpty(kvp.Value.Code))
                {
                    SpellFunction.CompileSpell(kvp.Value);
                }
            }
            Log.Trace("Done compiling spells.");
            
            // Links up exits and makes rooms runtime-ready so we can access them.
            Log.Trace( "Linking exits." );
            LinkExits();

            // This has to be after LinkExits().
            Log.Trace("Loading zone connections.");
            ZoneConnectionList = ZoneConnection.Load();
            // Link zones together based on file.
            foreach (ZoneConnection connection in ZoneConnectionList)
            {
                RoomTemplate room1 = Room.GetRoom(connection.FirstRoomNumber);
                RoomTemplate room2 = Room.GetRoom(connection.SecondRoomNumber);
                int direction = Exit.DoorLookup(connection.FirstToSecondDirection);
                if (room1 != null && room2 != null && direction > -1 && direction < Limits.MAX_DIRECTION)
                {
                    Exit exit = new Exit();
                    exit.TargetRoom = room2;
                    exit.IndexNumber = connection.SecondRoomNumber;
                    room1.ExitData[direction] = exit;
                    exit = new Exit();
                    exit.TargetRoom = room1;
                    exit.IndexNumber = connection.FirstRoomNumber;
                    room2.ExitData[Exit.ReverseDirection[direction]] = exit;
                    Log.Trace("Connected " + room1.Area.Name + " to " + room2.Area.Name + " at " + room1.IndexNumber);
                }
                else
                {
                    Log.Error("Unable to connect room " + connection.FirstRoomNumber + " to " + connection.SecondRoomNumber + " in direction " + connection.FirstToSecondDirection);
                }
            }
            Log.Trace("Loaded " + ZoneConnectionList.Count + " zone connections.");
            
            
            DatabaseIsBooting = false;
            Log.Trace( "Resetting areas." );
            AreaUpdate();

            Log.Trace( "Creating events." );
            Event.CreateEvent(Event.EventType.save_corpses, Event.TICK_SAVE_CORPSES, null, null, null);
            Event.CreateEvent(Event.EventType.save_sysdata, Event.TICK_SAVE_SYSDATA, null, null, null);
            Event.CreateEvent(Event.EventType.violence_update, Event.TICK_COMBAT_UPDATE, null, null, null);
            Event.CreateEvent(Event.EventType.area_update, Event.TICK_AREA, null, null, null);
            Event.CreateEvent(Event.EventType.room_update, Event.TICK_ROOM, null, null, null);
            Event.CreateEvent(Event.EventType.object_special, Event.TICK_OBJECT, null, null, null);
            Event.CreateEvent(Event.EventType.mobile_update, Event.TICK_MOBILE, null, null, null);
            Event.CreateEvent(Event.EventType.weather_update, Event.TICK_WEATHER, null, null, null);
            Event.CreateEvent(Event.EventType.char_update, Event.TICK_CHAR_UPDATE, null, null, null);
            Event.CreateEvent(Event.EventType.object_update, Event.TICK_OBJ_UPDATE, null, null, null);
            Event.CreateEvent(Event.EventType.aggression_update, Event.TICK_AGGRESS, null, null, null);
            Event.CreateEvent( Event.EventType.memorize_update, Event.TICK_MEMORIZE, null, null, null );
            Event.CreateEvent( Event.EventType.hit_gain, Event.TICK_HITGAIN, null, null, null );
            Event.CreateEvent( Event.EventType.mana_gain, Event.TICK_MANAGAIN, null, null, null );
            Event.CreateEvent( Event.EventType.move_gain, Event.TICK_MOVEGAIN, null, null, null );
            Event.CreateEvent( Event.EventType.heartbeat, Event.TICK_WEATHER, null, null, null );

            return;
        }

        /// <summary>
        /// Load all of the area files.
        /// </summary>
        public static void LoadAreaFiles()
        {
            if( !DatabaseIsBooting )
            {
                Log.Error( "LoadAreaFiles: Can't load area files if not booting!", 0 );
                return;
            }

            string strsave = FileLocation.AreaDirectory + FileLocation.AreaLoadList;

            try
            {
                FileStream fp = File.OpenRead( strsave );
                StreamReader sr = new StreamReader( fp );

                while( !sr.EndOfStream )
                {
                    string line = sr.ReadLine();

                    if( line[ 0 ] == '$' )
                    {
                        break;
                    }

                    Log.Trace(String.Format("Loading area: {0}", line));
                    Area area = Area.Load(FileLocation.AreaDirectory + line);

                    if( area != null )
                    {
                        // Check for area index number overlap
                        foreach (Area previousArea in AreaList)
                        {
                            if ((area.LowIndexNumber > previousArea.LowIndexNumber && area.LowIndexNumber < previousArea.HighIndexNumber) ||
                                (area.HighIndexNumber < previousArea.HighIndexNumber && area.HighIndexNumber > previousArea.LowIndexNumber) ||
                                (area.LowIndexNumber < previousArea.LowIndexNumber && area.HighIndexNumber > previousArea.HighIndexNumber))
                            {
                                Log.Error(String.Format( "Area {0} has index numbers that range from {1} to {2}.  This overlaps the indexes of area {3} that range from {4} to {5}.  This could cause problems.",
                                    line, area.LowIndexNumber, area.HighIndexNumber, SocketConnection.RemoveANSICodes(previousArea.Name), previousArea.LowIndexNumber, previousArea.HighIndexNumber));
                            }
                        }
                        AreaList.AddLast(area);
                        Log.Trace( "Area loaded with {0} mobs, {1} objects, {2} rooms, {3} shops, {4} resets, and {5} quests.",
                            area.Mobs.Count, area.Objects.Count, area.Rooms.Count, 
                            area.Shops.Count, area.Resets.Count, area.Quests.Count );
                    }
                    else
                    {
                        Log.Error("Area file " + line + " failed to load.");
                    }
                }
            }
            catch( Exception ex )
            {
                Log.Error( "Exception in Database.LoadAreaFiles(): " + ex );
            }
        }

        /// <summary>
        /// Translates a dragon breath type _name into its special function.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        static MobSpecial GetBreathType( string name )
        {
            if (string.IsNullOrEmpty(name))
                return null;

            if( MUDString.NameContainedIn( "br_f", name ) )
                return MobSpecial.SpecMobLookup( "spec_breath_fire" )[0];
            if( MUDString.NameContainedIn( "br_a", name ) )
                return MobSpecial.SpecMobLookup("spec_breath_acid")[0];
            if( MUDString.NameContainedIn( "br_c", name ) )
                return MobSpecial.SpecMobLookup("spec_breath_frost")[0];
            if( MUDString.NameContainedIn( "br_g", name ) )
                return MobSpecial.SpecMobLookup("spec_breath_gas")[0];
            if( MUDString.NameContainedIn( "br_l", name ) )
                return MobSpecial.SpecMobLookup("spec_breath_lightning")[0];
            if( MUDString.NameContainedIn( "br_w", name ) )
                return MobSpecial.SpecMobLookup("spec_breath_water")[0];
            if( MUDString.NameContainedIn( "br_s", name ) )
                return MobSpecial.SpecMobLookup("spec_breath_shadow")[0];

            return MobSpecial.SpecMobLookup("spec_breath_any")[0];
        }

        /// <summary>
        /// Loads the ban file from disk.
        /// </summary>
        public static void LoadBans()
        {
            BanData ban;
            string fileLocation = FileLocation.SystemDirectory + FileLocation.BanFile;
            string blankFileLocation = FileLocation.BlankSystemFileDirectory + FileLocation.BanFile;

            try
            {
                FileStream fp = null;
                try
                {
                    fp = File.OpenRead(fileLocation);
                }
                catch (FileNotFoundException)
                {
                    Log.Info("Ban file not found, using blank file.");
                    File.Copy(blankFileLocation, fileLocation);
                    fp = File.OpenRead(fileLocation);
                }
                StreamReader sr = new StreamReader(fp);
                while (!sr.EndOfStream)
                {
                    string name = sr.ReadLine();

                    if (name[0] == '$')
                        break;

                    ban = new BanData();
                    ban.Name = name;
                    BanList.Add(ban);
                }
            }
            catch (Exception ex)
            {
                Log.Error("Exception in Database.LoadBans(): " + ex);
            }
        }

        /// <summary>
        /// Assigns spell circles 
        /// </summary>
        private void AssignSpellCircles()
        {
            if (CharClass.ClassList == null)
            {
                Log.Error("Database.AssignSpellCircles():  Class list is empty -- no classes have been loaded.  Cannot assign spell circles.");
                return;
            }

            // Reset levels to what matches classes.
            foreach (CharClass charclass in CharClass.ClassList)
            {
                if (charclass.Spells == null)
                    continue;
                foreach (SpellEntry spellentry in charclass.Spells)
                {
                    if (Spell.SpellList.ContainsKey(spellentry.Name))
                    {
                        Spell.SpellList[spellentry.Name].SpellCircle[(int)charclass.ClassNumber] = spellentry.Circle;
                    }
                }
            }
        }

        /// <summary>
        /// Links all room exits to the the appropriate _targetType room data.  Reports any problems with
        /// nonexistant rooms or one-way exits.  This must be done once and only once after all rooms are
        /// loaded.
        /// </summary>
        static void LinkExits()
        {
            Exit exit;
            Exit reverseExit;
            Room toRoom;
            int door;

            // First we have to convert into runtime rooms.
            foreach (Area area in AreaList)
            {
                for (int i = 0; i < area.Rooms.Count; i++)
                {
                    area.Rooms[i] = new Room(area.Rooms[i]);
                }
            }

            foreach( Area area in AreaList )
            {
                foreach( Room room in area.Rooms )
                {
                    // Set exit data.
                    for (door = 0; door < Limits.MAX_DIRECTION; door++)
                    {
                        exit = room.ExitData[door];
                        if (exit != null)
                        {
                            if (exit.IndexNumber <= 0)
                                exit.TargetRoom = null;
                            else
                            {
                                exit.TargetRoom = Room.GetRoom(exit.IndexNumber);
                                if (exit.TargetRoom == null)
                                {
                                    string buf = String.Format("Room {0} in zone {1} has an exit in direction {2} to room {3}.  Room {3} was not found.",
                                        room.IndexNumber,
                                        SocketConnection.RemoveANSICodes(room.Area.Name),
                                        Exit.DirectionName[door],
                                        exit.IndexNumber);
                                    Log.Error(buf);
                                    // NOTE: We do not delete the exit data here because most non-linkable exits are due to 
                                    // attached zones not loading.  If we delete the exit data, that means that the zone link
                                    // will be irrevocably lost if we re-save the zone.  However, if we leave the exit data
                                    // intact, when the missing zone is re-loaded in a future boot the exit should self-heal.
                                }
                            }
                        }
                    }

                }
            }

            foreach( Area area in AreaList )
            {
                foreach( RoomTemplate room in area.Rooms )
                {
                    for( door = 0; door < Limits.MAX_DIRECTION; door++ )
                    {
                        if( ( exit = room.ExitData[ door ] ) && ( toRoom = Room.GetRoom(exit.IndexNumber) )
                                && ( reverseExit = toRoom.ExitData[ Exit.ReverseDirection[ door ] ] )
                                && reverseExit.TargetRoom != room )
                        {
                            String buf = String.Format("Database.LinkExits(): Mismatched Exit - Room {0} Exit {1} to Room {2} in zone {3}: Target room's {4} Exit points to Room {5}.",
                            room.IndexNumber,
                            Exit.DirectionName[door],
                            toRoom.IndexNumber, 
                            SocketConnection.RemoveANSICodes(room.Area.Name),
                            Exit.DirectionName[Exit.ReverseDirection[door]],
                            (!reverseExit.TargetRoom) ? 0
                            : reverseExit.TargetRoom.IndexNumber );
                            Log.Info( buf );
                        }
                    }
                }
            }
            return;
        }

        /// <summary>
        /// Repopulate areas periodically.
        /// 
        /// Also gradually replenishes a town's guard store if guards have been killed.
        /// </summary>
        static public void AreaUpdate()
        {
            int numPlayers = 0;
            foreach( Area area in AreaList )
            {
                CharData roomChar;

                if (area.NumDefendersDispatched > 0)
                {
                    area.NumDefendersDispatched--;
                }

                // Increment the area's age in seconds.
                area.AgeInSeconds += Event.TICK_AREA;

                // Reset area normally.
                if( area.AreaResetMode == Area.ResetMode.normal && area.TimesReset != 0 )
                    continue;
                // Reset area only when empty of players
                if (area.AreaResetMode == Area.ResetMode.empty_of_players && area.NumPlayers > 0)
                {
                    String text = String.Format("{0} not being Reset, {1} players are present.", area.Filename, area.NumPlayers);
                    ImmortalChat.SendImmortalChat(null, ImmortalChat.IMMTALK_RESETS, Limits.LEVEL_OVERLORD, text);
                    Log.Trace(text);
                    continue;
                }
                // Reset area only when no objects are still in the area.
                if (area.AreaResetMode == Area.ResetMode.empty_of_objects)
                {
                    foreach (Room room in area.Rooms)
                    {
                        foreach( Object obj in room.Contents )
                        {
                            if (obj.HasWearFlag(ObjTemplate.WEARABLE_CARRY))
                            {
                                String buf = String.Format("{0} not being Reset, at least one takeable object is present.", area.Filename);
                                ImmortalChat.SendImmortalChat(null, ImmortalChat.IMMTALK_RESETS, Limits.LEVEL_OVERLORD, buf);
                                Log.Trace(buf);
                                continue;
                            }
                        }
                    }
                }
                // Reset area only when no mobiles are alive in the area.
                if (area.AreaResetMode == Area.ResetMode.empty_of_mobiles)
                {
                    foreach (Room room in area.Rooms)
                    {
                        foreach (CharData ch in room.People)
                        {
                            if (ch.IsNPC())
                            {
                                String buf = String.Format("{0} not being Reset, at least one mobile is present.", area.Filename);
                                ImmortalChat.SendImmortalChat(null, ImmortalChat.IMMTALK_RESETS, Limits.LEVEL_OVERLORD, buf);
                                Log.Trace(buf);
                                continue;
                            }
                        }
                    }
                }
                // Reset area only when all quests are completed.
                if (area.AreaResetMode == Area.ResetMode.all_quests_completed)
                {
                    foreach (QuestTemplate qst in area.Quests)
                    {
                        foreach (QuestData quest in qst.Quests)
                        {
                            foreach (QuestItem item in quest.Receive)
                            {
                                if (item.Completed == false)
                                {
                                    String buf = String.Format("{0} not being Reset, at least one quest has not been completed.", area.Filename);
                                    ImmortalChat.SendImmortalChat(null, ImmortalChat.IMMTALK_RESETS, Limits.LEVEL_OVERLORD, buf);
                                    Log.Trace(buf);
                                    continue;
                                }
                            }
                        }
                    }
                }
                if ((area.AgeInSeconds / 60) < area.MinutesBetweenResets && area.TimesReset != 0)
                    continue;

                foreach( CharData cd in CharList )
                {
                    roomChar = cd;
                    if( !roomChar.IsNPC() && roomChar._inRoom != null && roomChar._inRoom.Area == area )
                        numPlayers++;
                }

                // Check for PC's and notify them if necessary.
                if( area.NumPlayers > 0 )
                {
                    foreach( CharData chd in CharList )
                    {
                        roomChar = chd;
                        if( !roomChar.IsNPC() && roomChar.IsAwake() && roomChar._inRoom
                                && roomChar._inRoom.Area == area &&
                                !String.IsNullOrEmpty(area.ResetMessage))
                        {
                            roomChar.SendText( String.Format( "{0}\r\n", area.ResetMessage ) );
                        }
                    }
                }

                // Check age and Reset.
                String outbuf = String.Format("{0} has just been Reset after {1} minutes.", area.Filename, (area.AgeInSeconds / 60));
                ImmortalChat.SendImmortalChat( null, ImmortalChat.IMMTALK_RESETS, Limits.LEVEL_OVERLORD, outbuf );
                ResetArea( area );
            }

            return;
        }

        /// <summary>
        /// Resets a single area.
        /// </summary>
        public static void ResetArea( Area area )
        {
            if (area == null)
            {
                return;
            }
            Room room;
            int indexNumber;
            area.DefenderSquads = 0;
            area.NumDefendersDispatched = 0;
            for( indexNumber = area.LowRoomIndexNumber; indexNumber <= area.HighRoomIndexNumber; indexNumber++ )
            {
                if (indexNumber == 0)
                    continue;
                room = Room.GetRoom( indexNumber );
                if( room && room.Area == area )
                {
                    room.ResetRoom( area.TimesReset );
                }
            }
            area.AgeInSeconds = MUDMath.NumberRange(-Event.AREA_RESET_VARIABILITY, Event.AREA_RESET_VARIABILITY);
            area.TimesReset++;
            return;
        }

        /// <summary>
        /// Create an instance of a mobile from the provided template.
        /// </summary>
        /// <param name="mobTemplate"></param>
        /// <returns></returns>
        public static CharData CreateMobile( MobTemplate mobTemplate )
        {
            int count;

            if( !mobTemplate )
            {
                Log.Error("CreateMobile: null MobTemplate.", 0);
                throw new NullReferenceException();
            }

            CharData mob = new CharData();

            mob._mobTemplate = mobTemplate;
            mob._followers = null;
            mob._name = mobTemplate.PlayerName;
            mob._shortDescription = mobTemplate.ShortDescription;
            mob._fullDescription = mobTemplate.FullDescription;
            mob._description = mobTemplate.Description;
            mob._specFun = mobTemplate.SpecFun;
            mob._specFunNames = mobTemplate.SpecFunNames;
            mob._charClass = mobTemplate.CharacterClass;
            mob._level = MUDMath.FuzzyNumber( mobTemplate.Level );
            mob._actionFlags = mobTemplate.ActionFlags;
            mob._position = mobTemplate.DefaultPosition;
            mob._chatterBotName = mobTemplate.ChatterBotName;
            // TODO: Look up the chatter bot name and load a runtime bot into the variable.
            mob._chatterBot = null;
            for( count = 0; count < Limits.NUM_AFFECT_VECTORS; ++count )
            {
                mob._affectedBy[ count ] = mobTemplate.AffectedBy[ count ];
            }
            mob._alignment = mobTemplate.Alignment;
            mob._sex = mobTemplate.Gender;
            mob.SetPermRace( mobTemplate.Race );
            mob._size = Race.RaceList[ mob.GetRace() ].DefaultSize;
            if (mob.HasActionBit(MobTemplate.ACT_SIZEMINUS))
                mob._size--;
            if (mob.HasActionBit(MobTemplate.ACT_SIZEPLUS))
                mob._size++;

            mob._castingSpell = 0;
            mob._castingTime = 0;
            mob._permStrength = MUDMath.Dice( 2, 46 ) + 8;
            mob._permIntelligence = MUDMath.Dice( 2, 46 ) + 8;
            mob._permWisdom = MUDMath.Dice( 2, 46 ) + 8;
            mob._permDexterity = MUDMath.Dice( 2, 46 ) + 8;
            mob._permConstitution = MUDMath.Dice( 2, 46 ) + 7;
            mob._permAgility = MUDMath.Dice( 2, 46 ) + 8;
            mob._permCharisma = MUDMath.Dice( 2, 46 ) + 8;
            mob._permPower = MUDMath.Dice( 2, 46 ) + 8;
            mob._permLuck = MUDMath.Dice( 2, 46 ) + 8;
            mob._modifiedStrength = 0;
            mob._modifiedIntelligence = 0;
            mob._modifiedWisdom = 0;
            mob._modifiedDexterity = 0;
            mob._modifiedConstitution = 0;
            mob._modifiedAgility = 0;
            mob._modifiedCharisma = 0;
            mob._modifiedPower = 0;
            mob._modifiedLuck = 0;
            mob._resistant = mobTemplate.Resistant;
            mob._immune = mobTemplate.Immune;
            mob._susceptible = mobTemplate.Susceptible;
            mob._vulnerable = mobTemplate.Vulnerable;
            mob._maxMana = mob._level * 10;
            if( Race.RaceList[mobTemplate.Race].Coins )
            {
                int level = mobTemplate.Level;
                mob.ReceiveCopper( MUDMath.Dice( 12, level ) / 32 );
                mob.ReceiveSilver( MUDMath.Dice( 9, level ) / 32 );
                mob.ReceiveGold( MUDMath.Dice( 5, level ) / 32 );
                mob.ReceivePlatinum( MUDMath.Dice( 2, level ) / 32 );
            }
            else
            {
                mob.SetCoins( 0, 0, 0, 0 );
            }
            mob._armorPoints = MUDMath.Interpolate( mob._level, 100, -100 );

            // * MOB HITPOINTS *
            //
            // Was level d 8, upped it to level d 13
            // considering mobs *still* won't have as many hitpoints as some players until
            // at least level 10, this shouldn't be too big an upgrade.
            //
            // Mob hitpoints are not based on constitution *unless* they have a
            // constitution modifier from an item, spell, or other affect

            // In light of recent player dissatisfaction with the
            // mob hitpoints, I'm implementing a log curve, using
            //  hp = exp( 2.15135 + level*0.151231)
            // This will will result in the following hp matrix:
            //     Level    Hitpoints
            //      20        175
            //      30        803
            //      40        3643
            //      50        16528
            //      55        35207
            //      60        75000
            mob._maxHitpoints = MUDMath.Dice( mob._level, 13 ) + 1;
            // Mob hps are non-linear above level 10.
            if( mob._level > 20 )
            {
                int upper = (int)Math.Exp( 1.85 + mob._level * 0.151231 );
                int lower = (int)Math.Exp( 1.80 + mob._level * 0.151231 );
                mob._maxHitpoints += MUDMath.NumberRange( lower, upper );
            }
            else if (mob._level > 10)
            {
                mob._maxHitpoints += MUDMath.NumberRange(mob._level * 2, ((mob._level - 8) ^ 2 * mob._level) / 2);
            }

            // Demons/devils/dragons gain an extra 30 hitpoints per level (+1500 at lvl 50).
            if (mob.GetRace() == Race.RACE_DEMON || mob.GetRace() == Race.RACE_DEVIL || mob.GetRace() == Race.RACE_DRAGON)
            {
                mob._maxHitpoints += (mob._level * 30);
            }

            mob._hitpoints = mob.GetMaxHit();

            // Horses get more moves, necessary for mounts.
            if(Race.RaceList[ mob.GetRace() ].Name.Equals( "Horse", StringComparison.CurrentCultureIgnoreCase ))
            {
                mob._maxMoves = 290 + MUDMath.Dice( 4, 5 );
                mob._currentMoves = mob._maxMoves;
            }
            mob._loadRoomIndexNumber = 0;

            // Insert in list.
            CharList.Add( mob );
            // Increment count of in-game instances of mob.
            mobTemplate.NumActive++;
            return mob;
        }

        /// <summary>
        /// Creates a duplicate of a mobile minus its inventory.
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="clone"></param>
        public static void CloneMobile( CharData parent, CharData clone )
        {
            int i;

            if( parent == null || clone == null || !parent.IsNPC() )
                return;

            // Fix values.
            clone._name = parent._name;
            clone._shortDescription = parent._shortDescription;
            clone._fullDescription = parent._fullDescription;
            clone._description = parent._description;
            clone._sex = parent._sex;
            clone._charClass = parent._charClass;
            clone.SetPermRace( parent.GetRace() );
            clone._level = parent._level;
            clone._trustLevel = 0;
            clone._specFun = parent._specFun;
            clone._specFunNames = parent._specFunNames;
            clone._timer = parent._timer;
            clone._wait = parent._wait;
            clone._hitpoints = parent._hitpoints;
            clone._maxHitpoints = parent._maxHitpoints;
            clone._currentMana = parent._currentMana;
            clone._maxMana = parent._maxMana;
            clone._currentMoves = parent._currentMoves;
            clone._maxMoves = parent._maxMoves;
            clone.SetCoins( parent.GetCopper(), parent.GetSilver(), parent.GetGold(), parent.GetPlatinum() );
            clone._experiencePoints = parent._experiencePoints;
            clone._actionFlags = parent._actionFlags;
            clone._affected = parent._affected;
            clone._position = parent._position;
            clone._alignment = parent._alignment;
            clone._hitroll = parent._hitroll;
            clone._damroll = parent._damroll;
            clone._wimpy = parent._wimpy;
            clone._deaf = parent._deaf;
            clone._hunting = parent._hunting;
            clone._hating = parent._hating;
            clone._fearing = parent._fearing;
            clone._resistant = parent._resistant;
            clone._immune = parent._immune;
            clone._susceptible = parent._susceptible;
            clone._size = parent._size;
            clone._permStrength = parent._permStrength;
            clone._permIntelligence = parent._permIntelligence;
            clone._permWisdom = parent._permWisdom;
            clone._permDexterity = parent._permDexterity;
            clone._permConstitution = parent._permConstitution;
            clone._permAgility = parent._permAgility;
            clone._permCharisma = parent._permCharisma;
            clone._permPower = parent._permPower;
            clone._permLuck = parent._permLuck;
            clone._modifiedStrength = parent._modifiedStrength;
            clone._modifiedIntelligence = parent._modifiedIntelligence;
            clone._modifiedWisdom = parent._modifiedWisdom;
            clone._modifiedDexterity = parent._modifiedDexterity;
            clone._modifiedConstitution = parent._modifiedConstitution;
            clone._modifiedAgility = parent._modifiedAgility;
            clone._modifiedCharisma = parent._modifiedCharisma;
            clone._modifiedPower = parent._modifiedPower;
            clone._modifiedLuck = parent._modifiedLuck;
            clone._armorPoints = parent._armorPoints;
            clone._mpactnum = parent._mpactnum;

            for (i = 0; i < 6; i++)
            {
                clone._savingThrows[i] = parent._savingThrows[i];
            }

            // Now add the affects.
            foreach (Affect affect in parent._affected)
            {
                clone.AddAffect(affect);
            }
        }

        /// <summary>
        /// Create an instance of an object.
        /// </summary>
        /// <param name="objTempalte"></param>
        /// <param name="level"></param>
        /// <returns></returns>
        public static Object CreateObject( ObjTemplate objTempalte, int level )
        {
            if( level < 1 )
            {
                level = 1;
            }

            if( !objTempalte )
            {
                Log.Error("CreateObject: null ObjTemplate.", 0);
                return null;
            }

            Object obj = new Object( objTempalte );
            if( !obj )
            {
                Log.Error("Database.CreateObject: new Object(ObjIndex*) failed.", 0);
                return null;
            }
            obj.Level = level;
            return obj;
        }

        /// <summary>
        /// Duplicate an object exactly minus contents.
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="clone"></param>
        public static void CloneObject( Object parent, ref Object clone )
        {
            int i;
            ExtendedDescription edNew;

            if( parent == null || clone == null )
                return;

            // Start fixing the object.
            clone.Name = parent.Name;
            clone.ShortDescription = parent.ShortDescription;
            clone.FullDescription = parent.FullDescription;
            clone.SpecFun = parent.SpecFun;
            clone.Affected = parent.Affected;
            clone.ItemType = parent.ItemType;
            clone.WearFlags = parent.WearFlags;
            clone.UseFlags = parent.UseFlags;
            clone.Material = parent.Material;
            clone.Size = parent.Size;
            clone.Volume = parent.Volume;
            clone.Craftsmanship = parent.Craftsmanship;
            clone.Weight = parent.Weight;
            clone.Cost = parent.Cost;
            clone.Level = parent.Level;
            clone.Condition = parent.Condition;
            clone.Timer = parent.Timer;
            clone.Condition = parent.Condition;
            clone.Trap = parent.Trap;

            for( i = 0; i < Limits.NUM_ITEM_EXTRA_VECTORS; i++ )
                clone.ExtraFlags[ i ] = parent.ExtraFlags[ i ];

            for( i = 0; i < Limits.NUM_AFFECT_VECTORS; i++ )
                clone.AffectedBy[ i ] = parent.AffectedBy[ i ];

            for( i = 0; i < 8; i++ )
                clone.Values[ i ] = parent.Values[ i ];

            /* extended desc */
            foreach( ExtendedDescription ed in parent.ExtraDescription )
            {
                edNew = new ExtendedDescription();
                edNew.Keyword = ed.Keyword;
                edNew.Description = ed.Description;
                clone.ExtraDescription.Add( edNew );
            }
        }

        /// <summary>
        /// Get an extra description from a list.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="ed"></param>
        /// <returns></returns>
        public static string GetExtraDescription( string name, List<ExtendedDescription> ed )
        {
            if (String.IsNullOrEmpty(name) || ed == null || ed.Count < 1)
            {
                return String.Empty;
            }

            foreach( ExtendedDescription desc in ed )
            {
                if( MUDString.NameContainedIn( name, desc.Keyword ) )
                {
                    return ( desc.Description );
                }
            }
            return String.Empty;
        }

        /// <summary>
        /// Gets a mob template class based on the supplied virtual number.
        /// </summary>
        /// <param name="indexNumber"></param>
        /// <returns></returns>
        public static MobTemplate GetMobTemplate( int indexNumber )
        {
            // No indexNumber = give up.  We also can't have dupes if there are less than two mobs.
            if( indexNumber < 0 || MobTemplate.Count <= 1 )
            {
                return null;
            }

            foreach( Area area in AreaList )
            {
                foreach( MobTemplate mob in area.Mobs )
                {
                    if( mob.IndexNumber == indexNumber )
                    {
                        return mob;
                    }
                }
            }

            if( DatabaseIsBooting )
            {
                Log.Error("GetMobTemplate: bad indexNumber " + indexNumber);
                throw new NullReferenceException();
            }
            return null;
        }

        /// <summary>
        /// Gets an object template based on a virtual number.
        /// </summary>
        /// <param name="indexNumber"></param>
        /// <returns></returns>
        public static ObjTemplate GetObjTemplate( int indexNumber )
        {
            // There is a possibility of indexNumber passed is negative.
            if( indexNumber < 0 )
            {
                return null;
            }

            foreach( Area area in AreaList )
            {
                foreach( ObjTemplate obj in area.Objects )
                {
                    if( obj.IndexNumber == indexNumber )
                    {
                        return obj;
                    }
                }
            }

            if( DatabaseIsBooting )
            {
                Log.Error("Database.GetObjTemplate: bad indexNumber " + indexNumber);
                Log.Trace( "FIX THIS!  IT WILL CAUSE PROBLEMS!" );
            }

            return null;
        }

        /// <summary>
        /// Generate a random door.
        /// </summary>
        /// <returns></returns>
        public static int RandomDoor()
        {
            return MUDMath.NumberRange( 0, ( Limits.MAX_DIRECTION - 1 ) );
        }

        /// <summary>
        /// Appends a string to a file.
        /// </summary>
        /// <param name="ch"></param>
        /// <param name="file"></param>
        /// <param name="str"></param>
        public static void AppendFile( CharData ch, string file, string str )
        {
            if( ch == null || String.IsNullOrEmpty(file) || String.IsNullOrEmpty(str) || ch.IsNPC() )
            {
                return;
            }
            FileStream fp = File.OpenWrite( file );
            StreamWriter sw = new StreamWriter( fp );
            sw.WriteLine( "[{0}] {1}: {2}\n", ch._inRoom ? MUDString.PadInt(ch._inRoom.IndexNumber,5) : MUDString.PadInt(0,5),
                ch._name, str );
            sw.Flush();
            sw.Close();
            return;
        }

        /// <summary>
        /// Logs a message to the guild log file.
        /// </summary>
        /// <param name="str"></param>
        public static void LogGuild( string str )
        {
            Log.Trace("GUILD: ");
        }

        /// <summary>
        /// Checks whether a particular indexNumber is flagged as an artifact.
        /// </summary>
        /// <param name="indexNumber"></param>
        /// <returns></returns>
        public static bool IsArtifact( int indexNumber )
        {
            ObjTemplate obj = GetObjTemplate( indexNumber );
            if (obj != null && Macros.IsSet(obj.ExtraFlags[ObjTemplate.ITEM_ARTIFACT.Group], ObjTemplate.ITEM_ARTIFACT.Vector))
            {
                return true;
            }

            return false;
        }

    }
}