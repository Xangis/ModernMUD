using System;
using System.IO;
using System.Xml.Serialization;
using ModernMUD;

// TODO: Modify the _fraglist such that changing the classes and races available to PCs will not break what's
// already been saved.
namespace MUDEngine
{
    [Serializable]
    public class FragData
    {
        private string _name;
        private int _frags;

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public int Frags
        {
            get { return _frags; }
            set { _frags = value; }
        }
    };

    // Guild frags will be stored separately with each guild's data.
    // This is a huge struct, and updating and referencing it should be
    // a huge chore - Xangis
    [XmlRoot( "Fraglist" )]
    [Serializable]
    public class FraglistData
    {
        /// <summary>
        /// Number of people on each fraglist.
        /// </summary>
        public const int MAX_FRAG = 10;

        private FragData[] _topFrags =
            new FragData[ FraglistData.MAX_FRAG ];

        private FragData[] _bottomFrags =
            new FragData[ FraglistData.MAX_FRAG ];

        private FragData[][] _topRaceFrags =
            new FragData[ Limits.MAX_PC_RACE ][];

        private FragData[][] _bottomRaceFrags =
            new FragData[Limits.MAX_PC_RACE][];

        private FragData[][] _topClassFrags =
            new FragData[ Limits.MAX_CLASS ][];

        private FragData[][] _bottomClassFrags =
            new FragData[ Limits.MAX_CLASS ][];

        private int[][] _totalFragsByRaceAndClass =
            new int[ Limits.MAX_PC_RACE ][];

        private int[] _totalFragsBySide = 
            new int [ Race.MAX_RACEWAR_SIDE ] ;

        public FraglistData()
        {
        }

        // Frag functions
        // 
        //  check_top_bot_frag
        //  check_race_class_top_bot_frag
        // 
        //  When a person frags, they will gain/lose a frag, and so will their
        //  race/class totals and their racewar side totals.
        // 
        //  A race total is done by adding up all classes for that race and a class
        //  total is done by adding up all races for that class.

        private static FraglistData _fraglist = new FraglistData();

        public FragData[] TopFrags
        {
            get { return _topFrags; }
            set { _topFrags = value; }
        }

        public FragData[] BottomFrags
        {
            get { return _bottomFrags; }
            set { _bottomFrags = value; }
        }

        public FragData[][] TopRaceFrags
        {
            get { return _topRaceFrags; }
            set { _topRaceFrags = value; }
        }

        public FragData[][] BottomRaceFrags
        {
            get { return _bottomRaceFrags; }
            set { _bottomRaceFrags = value; }
        }

        public FragData[][] TopClassFrags
        {
            get { return _topClassFrags; }
            set { _topClassFrags = value; }
        }

        public FragData[][] BottomClassFrags
        {
            get { return _bottomClassFrags; }
            set { _bottomClassFrags = value; }
        }

        public int[][] TotalFragsByRaceAndClass
        {
            get { return _totalFragsByRaceAndClass; }
            set { _totalFragsByRaceAndClass = value; }
        }

        public int[] TotalFragsBySide
        {
            get { return _totalFragsBySide; }
            set { _totalFragsBySide = value; }
        }

        public static FraglistData Fraglist
        {
            get { return _fraglist; }
            set { _fraglist = value; }
        }

        // There are top lists, race lists, and class lists.  By passing the actual
        // list
        static public void SortFraglist( CharData ch, FragData[] list )
        {
            FragData person = new FragData();
            FragData temp = new FragData();
            int count;
            bool found = false;

            if( ch.IsNPC() )
            {
                Log.Error( "Mobile in Fraglist.SortFraglist(), check frag code!", 0 );
                return;
            }

            for( count = 0; count < FraglistData.MAX_FRAG; ++count )
            {
                if( list[ count ].Name.Equals(ch._name, StringComparison.CurrentCultureIgnoreCase ) )
                {
                    // remove them from the list if they're in it already
                    list[ count ].Name = String.Empty;
                    list[ count ].Frags = 0;
                    found = true;
                }
            }

            // Shift a blank entry to the bottom of the list
            if( found )
            {
                for( count = 0; count < FraglistData.MAX_FRAG; ++count )
                {
                    if( list[ count ].Name.Length < 1 && count < ( FraglistData.MAX_FRAG - 1 ) )
                    {
                        list[ count ].Name = list[ count + 1 ].Name;
                        list[ count ].Frags = list[ count + 1 ].Frags;
                        list[ count + 1 ].Name = String.Empty;
                        list[ count + 1 ].Frags = 0;
                    }
                }
            }

            person.Name = ch._name;
            person.Frags = ( (PC)ch ).Frags;

            // This works fine provided a blank entry isn't found where the player
            // used to be.  If a blank entry is found it'll plug in the data and continue
            // without setting the _name.
            for( count = 0; count < FraglistData.MAX_FRAG; ++count )
            {
                // Use absolute value so we can handle the negative frags properly too
                if( ( Math.Abs( list[ count ].Frags ) > Math.Abs( person.Frags ) ) )
                    continue;

                temp.Name = list[ count ].Name;
                temp.Frags = list[ count ].Frags;

                list[ count ].Name = String.Empty;
                list[ count ].Name = person.Name;
                list[ count ].Frags = person.Frags;

                person.Name = temp.Name;
                person.Frags = temp.Frags;
                temp.Name = String.Empty;

            }

            // See if the ch or victim qualify to replace an entry in either list.
            // damn hard code to write.
        }

        // Code to check if someone just fragged.
        // Will also have to add to race, class, and guild frag tables in
        // addition to the master frag table.  This does not update any
        // lists yet and instead only updates the totals. - Xangis
        public static void CheckForFrag( CharData ch, CharData victim )
        {
            // NPC's don't participate in fragging, can't frag yourself,
            // have to be within 10 levels, no same side frags, no frags
            // from races not participating in racewars, have to be level
            // 20 to frag, and have to be a valid class.

            // Check to see if kill qualifies for a frag.

            if( ch.IsNPC() )
                return;
            if( victim.IsNPC() )
                return;
            if( ch == victim )
                return;
            if( ch.GetRacewarSide() == Race.RacewarSide.neutral )
                return;
            if( !ch.IsRacewar( victim ) )
                return;
            if( ch.IsImmortal() )
                return;
            if( victim.IsImmortal() )
                return;
            if( victim._level < 20 )
                return;
            if( ch._level < 20 )
                return;
            if( ( ch._level - 10 ) > victim._level )
                return;
            if (ch.IsClass(CharClass.Names.none) || victim.IsClass(CharClass.Names.none))
                return;
            if( victim.GetOrigRace() > Limits.MAX_PC_RACE )
                return;

            // Give frag to ch.
            ( (PC)ch ).Frags++;

            // Protect against polymorphed character race frags.
            if( ch.GetOrigRace() < Limits.MAX_PC_RACE )
            {
                _fraglist._totalFragsByRaceAndClass[ch.GetOrigRace()][ (int)ch._charClass.ClassNumber]++;
                _fraglist._totalFragsBySide[ (int)ch.GetRacewarSide() ]++;
            }

            if( ( (PC)ch ).Clan != null )
                ( (PC)ch ).Clan.Frags++;

            // Take frag from victim.
            ( (PC)victim ).Frags--;

            // Protect against polymorphed character race frags
            if( victim.GetOrigRace() < Limits.MAX_PC_RACE )
            {
                _fraglist._totalFragsByRaceAndClass[victim.GetOrigRace()][ (int)victim._charClass.ClassNumber]--;
                _fraglist._totalFragsBySide[ (int)victim.GetRacewarSide() ]--;
            }

            if( ( (PC)victim ).Clan != null )
                ( (PC)victim ).Clan.Frags--;

            ch.SendText( "&+WYou gain a frag!&n\r\n" );
            victim.SendText( "&+WYou lose a frag!&n\r\n" );

            string buf = ch._name + " has fragged " + victim._name + " in room " + ch._inRoom.IndexNumber + ".";
            Immtalk.SendImmtalk( ch, Immtalk.IMMTALK_DEATHS, Limits.LEVEL_AVATAR, buf );
            Log.Trace( buf );

            // Check to see if either person goes up or down on their particular lists.

            if( ( (PC)ch ).Frags > 0 )
            {
                SortFraglist( ch, _fraglist._topFrags );
                SortFraglist( ch, _fraglist._topRaceFrags[ ch.GetOrigRace() ] );
                SortFraglist(ch, _fraglist._topClassFrags[(int)ch._charClass.ClassNumber]);
            }
            else if( ( (PC)ch ).Frags < 0 )
            {
                SortFraglist( ch, _fraglist._bottomFrags );
                SortFraglist( ch, _fraglist._bottomRaceFrags[ ch.GetOrigRace() ] );
                SortFraglist(ch, _fraglist._bottomClassFrags[(int)ch._charClass.ClassNumber]);
            }

            if( ( (PC)victim ).Frags > 0 )
            {
                SortFraglist( victim, _fraglist._topFrags );
                SortFraglist( victim, _fraglist._topRaceFrags[ victim.GetOrigRace() ] );
                SortFraglist(victim, _fraglist._topClassFrags[(int)victim._charClass.ClassNumber]);
            }
            else if( ( (PC)victim ).Frags < 0 )
            {
                SortFraglist( victim, _fraglist._bottomFrags );
                SortFraglist( victim, _fraglist._bottomRaceFrags[ victim.GetOrigRace() ] );
                SortFraglist(victim, _fraglist._bottomClassFrags[(int)victim._charClass.ClassNumber]);
            }

            _fraglist.Save();

            return;
        }

        public void Load()
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer( GetType() );
                Stream stream = new FileStream(FileLocation.SystemDirectory + FileLocation.FragFile, FileMode.Open,
                    FileAccess.Read, FileShare.None );
                _fraglist = (FraglistData)serializer.Deserialize( stream );
                stream.Close();
                InitializeData();
            }
            catch( Exception ex )
            {
                Log.Error( "Exception loading fraglist: " + ex );
            }
        }

        public void InitializeData()
        {
            // Validate and repair data.
            if (_fraglist._bottomClassFrags == null)
                _fraglist._bottomClassFrags = new FragData[Limits.MAX_CLASS][];
            if (_fraglist._bottomFrags == null)
                _fraglist._bottomFrags = new FragData[FraglistData.MAX_FRAG];
            if (_fraglist._bottomRaceFrags == null)
                _fraglist._bottomRaceFrags = new FragData[Limits.MAX_PC_RACE][];
            if (_fraglist._topClassFrags == null)
                _fraglist._topClassFrags = new FragData[Limits.MAX_CLASS][];
            if (_fraglist._topFrags == null)
                _fraglist._topFrags = new FragData[FraglistData.MAX_FRAG];
            if (_fraglist._topRaceFrags == null)
                _fraglist._topRaceFrags = new FragData[Limits.MAX_PC_RACE][];
            if (_fraglist._totalFragsByRaceAndClass == null)
                _fraglist._totalFragsByRaceAndClass = new int[Limits.MAX_PC_RACE][];
            for (int i = 0; i < FraglistData.MAX_FRAG; i++)
            {
                if (_fraglist._topFrags[i] == null)
                {
                    _fraglist._topFrags[i] = new FragData();
                }
                if (_fraglist._bottomFrags[i] == null)
                {
                    _fraglist._bottomFrags[i] = new FragData();
                }
            }
            for (int i = 0; i < Limits.MAX_CLASS; i++)
            {
                if (_fraglist._bottomClassFrags[i] == null)
                {
                    _fraglist._bottomClassFrags[i] = new FragData[FraglistData.MAX_FRAG];
                }
                if (_fraglist._topClassFrags[i] == null)
                {
                    _fraglist._topClassFrags[i] = new FragData[FraglistData.MAX_FRAG];
                }
                for (int j = 0; j < 10; j++)
                {
                    if (_fraglist._bottomClassFrags[i][j] == null)
                    {
                        _fraglist._bottomClassFrags[i][j] = new FragData();
                    }
                    if (_fraglist._topClassFrags[i][j] == null)
                    {
                        _fraglist._topClassFrags[i][j] = new FragData();
                    }
                }
            }
            for (int i = 0; i < Limits.MAX_PC_RACE; i++)
            {
                if (_fraglist._topRaceFrags[i] == null)
                {
                    _fraglist._topRaceFrags[i] = new FragData[FraglistData.MAX_FRAG];
                }
                if (_fraglist._bottomRaceFrags[i] == null)
                {
                    _fraglist._bottomRaceFrags[i] = new FragData[FraglistData.MAX_FRAG];
                }
                for (int j = 0; j < 10; j++)
                {
                    if (_fraglist._topRaceFrags[i][j] == null)
                    {
                        _fraglist._topRaceFrags[i][j] = new FragData();
                    }
                    if (_fraglist._bottomRaceFrags[i][j] == null)
                    {
                        _fraglist._bottomRaceFrags[i][j] = new FragData();
                    }
                }
            }
            if (_fraglist._totalFragsBySide == null)
                _fraglist._totalFragsBySide = new int[Race.MAX_RACEWAR_SIDE];
        }

        public void Save()
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer( GetType() );
                Stream stream = new FileStream(FileLocation.SystemDirectory + FileLocation.FragFile, FileMode.Create,
                    FileAccess.Write, FileShare.None );
                serializer.Serialize( stream, this );
                stream.Close();
            }
            catch( Exception ex )
            {
                Log.Error( "Exception saving fraglist: " + ex );
            }
        }
    };

}