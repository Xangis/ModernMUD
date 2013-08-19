using System.Xml.Serialization;
using System;
using System.IO;
using System.Collections.Generic;
using ModernMUD;

namespace MUDEngine
{
    /// <summary>
    /// Crime data for justice.
    /// </summary>
    public class Crime
    {
        public string Criminal { get; set; }
        public string Victim { get; set; }
        public string Zone { get; set; }
        public int CrimeType { get; set; }
        public DateTime Time { get; set; }
        public CrimeStatus Status { get; set; }
        private static int _numCrimes;

        public Crime()
        {
            Time = DateTime.Now;
            ++_numCrimes;
        }

        ~Crime()
        {
            --_numCrimes;
        }

        public static int Count
        {
            get
            {
                return _numCrimes;
            }
        }

        const int CRIME_MURDER = Bitvector.BV01;
        const int CRIME_ATT_MURDER = Bitvector.BV02;
        const int CRIME_THEFT = Bitvector.BV03;
        const int CRIME_JAILBREAK = Bitvector.BV04;
        const int CRIME_ATT_THEFT = Bitvector.BV05;
        const int CRIME_WALLING = Bitvector.BV06;
        const int CRIME_BRIBERY = Bitvector.BV07;
        const int CRIME_SHAPECHANGE = Bitvector.BV08;
        const int CRIME_DEADBEAT = Bitvector.BV09; // non payment of debt
        const int CRIME_KIDNAPPING = Bitvector.BV10;
        const int CRIME_TREASON = Bitvector.BV11; // crime against town
        const int CRIME_PLOOTING = Bitvector.BV12; // corpse looting
        const int CRIME_CORPSEDRAG = Bitvector.BV13; // corpse dragging
        const int CRIME_DISGUISING = Bitvector.BV14; // disguise
        const int CRIME_FORGERY = Bitvector.BV15;
        const int CRIME_SUMMONING = Bitvector.BV16; // summoning creatures
        const int CRIME_BLASPHEMY = Bitvector.BV17; // insulting the gods

        [Flags]
        public enum CrimeStatus
        {
            wanted = Bitvector.BV00,
            pardoned = Bitvector.BV01,
            falsely_accused = Bitvector.BV02,
            perpetrator_in_jail = Bitvector.BV03,
            deleted = Bitvector.BV04,
            guards_dispatched = Bitvector.BV05,
            time_served = Bitvector.BV06,
            fine_levied = Bitvector.BV07
        }

        /// <summary>
        /// Checks whether an attack justifies an attempted murder crime entry.
        /// 
        /// This requires that the victim be in justice and that there be a mob
        /// flagged with the ACT_WITNESS flag in the room.
        /// </summary>
        /// <param name="ch"></param>
        /// <param name="victim"></param>
        public static void CheckAttemptedMurder( CharData ch, CharData victim )
        {
            // Make sure input is valid.
            if( ch == null || victim == null )
                return;

            // Check for justice.  Suicide is ok.  Make sure areas are there.
            if( ch._inRoom == null || ch._inRoom.Area == null || ch._inRoom.Area.JusticeType == 0 )
                return;

            // NPC's are fair game.
            if( victim.IsNPC() )
                return;

            // NPC's are cool of course
            // Hitting yourself is cool too (bleeding).
            // Hitting immortals are fine.
            if (ch.IsNPC() || ch == victim || victim._level > Limits.LEVEL_HERO)
            {
                return;
            }

            // Vampires and the living dead are fair game.
            if (victim.IsUndead())
            {
                return;
            }

            // Defending yourself once you're already attacked is okay.
            if (victim._fighting != null && victim._fighting == ch)
            {
                return;
            }

            foreach( CharData wch in ch._inRoom.People )
            {
                if( wch.IsNPC() && wch.HasActBit( MobTemplate.ACT_WITNESS ) )
                {
                    // Crime committed and witnessed by a mob, add a crime data
                    CreateCrime( ch, victim, CRIME_ATT_MURDER );
                    Save();
                    return;
                }
                // Crime witnessed by player, give them the chance to report it
                return;
            }

            return;
        }

        /// <summary>
        /// See if an theft justifies a THEFT crime entry.
        /// 
        /// This requires that the person be in justice and that there
        /// be a mob flagged witness present
        /// </summary>
        /// <param name="ch"></param>
        /// <param name="victim"></param>
        public static void CheckThief( CharData ch, CharData victim )
        {
            // Check for justice
            if (ch == null || victim == null)
            {
                return;
            }

            if (ch._inRoom.Area.JusticeType == 0)
            {
                return;
            }

            // NPC's are fair game.
            if (victim.IsNPC())
            {
                return;
            }

            // NPC's are cool of course
            // Hitting yourself is cool too (bleeding).
            // Hitting immortals are fine.
            if (ch.IsNPC() || ch == victim || victim._level > Limits.LEVEL_HERO)
            {
                return;
            }

            // Vampires and the living dead are fair game.
            if (victim.IsUndead())
            {
                return;
            }

            foreach( CharData wch in ch._inRoom.People )
            {
                if( wch.IsNPC() && wch.HasActBit(MobTemplate.ACT_WITNESS ) )
                {
                    // Crime committed and witnessed by an NPC, add a crime data
                    CreateCrime( ch, victim, CRIME_THEFT );
                    Save();
                    return;
                }
                // Crime witnessed by player, give them the chance to report it
                return;
            }

            return;
        }

        /// <summary>
        /// Creates a new piece of crime data.
        /// </summary>
        /// <param name="ch"></param>
        /// <param name="victim"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        static Crime CreateCrime( CharData ch, CharData victim, int type )
        {
            Log.Trace( "Create crime called." );

            Crime crime = new Crime();

            Database.CrimeList.Add( crime );

            crime.Criminal = ch._name;
            crime.Victim = victim._name;
            crime.Time = Database.SystemData.CurrentTime;
            crime.CrimeType = type;
            crime.Zone = ch._inRoom.Area.Filename;

            return crime;
        }

        /// <summary>
        /// Saves the crime database.
        /// </summary>
        /// <returns></returns>
        public static bool Save()
        {
            string filename = FileLocation.SystemDirectory + FileLocation.CrimeFile;
            try
            {
                XmlSerializer serializer = new XmlSerializer( typeof( List<Crime> ) );
                Stream stream = new FileStream( filename, FileMode.Create, FileAccess.Write, FileShare.None );
                serializer.Serialize( stream, Database.CrimeList );
                stream.Close();
                return true;
            }
            catch( Exception ex )
            {
                Log.Error( "Exception saving crimes: " + ex );
                return false;
            }
        }

        /// <summary>
        /// Loads the crime database.
        /// </summary>
        /// <returns></returns>
        public static bool Load()
        {
            string filename = FileLocation.SystemDirectory + FileLocation.CrimeFile;
            string blankFilename = FileLocation.BlankSystemFileDirectory + FileLocation.CrimeFile;
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<Crime>));
                FileStream stream = null;
                try
                {
                    stream = new FileStream( filename, FileMode.Open, FileAccess.Read, FileShare.None );
                }
                catch (FileNotFoundException)
                {
                    Log.Info("Crime file not found, using blank file.");
                    File.Copy(blankFilename, filename);
                    stream = new FileStream( filename, FileMode.Open, FileAccess.Read, FileShare.None );
                }
                Database.CrimeList = (List<Crime>)serializer.Deserialize( stream );
                stream.Close();
                return true;
            }
            catch( Exception ex )
            {
                Log.Error( "Exception in Crime.Load(): " + ex );
                Database.CrimeList = new List<Crime>();
                return false;
            }
        }

        /// <summary>
        /// Check whether the being entering the area is an enemy and needs to be dealt with.
        /// </summary>
        /// <param name="ch"></param>
        public static void CheckInvader( CharData ch )
        {
            string lbuf;

            if (ch == null || ch.IsNPC() || ch.IsImmortal())
                return;

            if( ch._inRoom == null || ch._inRoom.Area == null ||
                    ch._inRoom.Area.JusticeType == 0 )
                return;

            switch( ch.GetRacewarSide() )
            {
                case Race.RacewarSide.good:
                    if( ch._inRoom.Area.JusticeType == JusticeType.chaotic_evil
                            || ch._inRoom.Area.JusticeType == JusticeType.chaotic_evil_only
                            || ch._inRoom.Area.JusticeType == JusticeType.chaotic_neutral_only
                            || ch._inRoom.Area.JusticeType == JusticeType.evil_only
                            || ch._inRoom.Area.JusticeType == JusticeType.neutral_only
                            || ch._inRoom.Area.JusticeType == JusticeType.evil )
                    {
                        lbuf = String.Format( "Check_invader: {0} invading !good justice", ch._name );
                        ImmortalChat.SendImmortalChat( null, ImmortalChat.IMMTALK_SPAM, 0, lbuf );
                        StartInvasion( ch );
                    }
                    return;
                case Race.RacewarSide.evil:
                    if( ch._inRoom.Area.JusticeType == JusticeType.chaotic_good
                            || ch._inRoom.Area.JusticeType == JusticeType.chaotic_good_only
                            || ch._inRoom.Area.JusticeType == JusticeType.chaotic_neutral_only
                            || ch._inRoom.Area.JusticeType == JusticeType.good_only
                            || ch._inRoom.Area.JusticeType == JusticeType.neutral_only
                            || ch._inRoom.Area.JusticeType == JusticeType.good )
                    {
                        lbuf = String.Format( "Check_invader: {0} invading !evil justice", ch._name );
                        ImmortalChat.SendImmortalChat( null, ImmortalChat.IMMTALK_SPAM, 0, lbuf );
                        StartInvasion( ch );
                    }
                    return;
                case Race.RacewarSide.neutral:
                    if( ch._inRoom.Area.JusticeType == JusticeType.chaotic_good_only
                            || ch._inRoom.Area.JusticeType == JusticeType.chaotic_evil_only
                            || ch._inRoom.Area.JusticeType == JusticeType.evil_only
                            || ch._inRoom.Area.JusticeType == JusticeType.good_only)
                    {
                        lbuf = String.Format( "Check_invader: {0} invading !neutral justice", ch._name );
                        ImmortalChat.SendImmortalChat( null, ImmortalChat.IMMTALK_SPAM, 0, lbuf );
                        StartInvasion( ch );
                    }
                    return;
            }

            return;
        }

        /// <summary>
        /// Alert and dispatch the guards when an enemy has entered the area.
        /// </summary>
        /// <param name="ch"></param>
        static void StartInvasion( CharData ch )
        {
            SocketConnection socket;
            int count;
            CharData mob;
            string lbuf;

            // if there are no protector mobs, who cares if someone walks in.
            if( ch._inRoom.Area.DefenderTemplateNumber == 0 || ch._inRoom.Area.DefendersPerSquad == 0)
            {
                lbuf = String.Format( "Start_invasion: no defender mobs" );
                ImmortalChat.SendImmortalChat( null, ImmortalChat.IMMTALK_SPAM, 0, lbuf );
                return;
            }

            // any town can only dispatch 5 batches of guards.
            if( ch._inRoom.Area.NumDefendersDispatched >= ( ch._inRoom.Area.DefendersPerSquad * 5 ) )
            {
                //    if( ch.in_room.area.dispatched >= (ch.in_room.area.defender_num *
                //        ch.in_room.area.squads )) {
                lbuf = String.Format( "Start_invasion: dispatched all defender mobs, disregarding" );
                ImmortalChat.SendImmortalChat( null, ImmortalChat.IMMTALK_SPAM, 0, lbuf );
                //      return;
            }

            // warn of invasion
            foreach( SocketConnection it in Database.SocketList )
            {
                socket = it;
                if( socket._connectionState == SocketConnection.ConnectionState.playing
                        && socket.Character._inRoom.Area == ch._inRoom.Area )
                {
                    socket.Character.SendText( "&+RYou hear the guards sound the invasion alarm!\r\n" );
                }
            }

            // create and dispatch defenders
            for( count = 0; count < ch._inRoom.Area.DefendersPerSquad; ++count )
            {
                if( ch._inRoom.Area.NumDefendersDispatched >= ch._inRoom.Area.DefendersPerSquad * 5 )
                {
                    break;
                }
                mob = Database.CreateMobile( Database.GetMobTemplate( ch._inRoom.Area.DefenderTemplateNumber ) );
                mob.AddToRoom( Room.GetRoom( ch._inRoom.Area.BarracksRoom ) );
                if (!mob.HasActBit(MobTemplate.ACT_MEMORY))
                    mob.SetActBit( MobTemplate.ACT_MEMORY );
                if (!mob.HasActBit(MobTemplate.ACT_HUNTER))
                    mob.SetActBit( MobTemplate.ACT_HUNTER );
                if (mob.HasActBit(MobTemplate.ACT_SENTINEL))
                    mob.RemoveActBit(MobTemplate.ACT_SENTINEL);
                mob._mobIndexData.AddSpecFun( "spec_justice_guard" );
                Combat.StartGrudge( mob, ch, false );
                ch._inRoom.Area.NumDefendersDispatched++;
            }
            ch._inRoom.Area.DefenderSquads++;

            return;
        }

        // Dispatches a guard to hunt down the player that has committed a crime and
        // has the guard capture them.
        static void DispatchGuard( CharData ch )
        {
            CharData mob = Database.CreateMobile( Database.GetMobTemplate( ch._inRoom.Area.DefenderTemplateNumber ) );
            mob.AddToRoom( Room.GetRoom( ch._inRoom.Area.BarracksRoom ) );
            if (!mob.HasActBit(MobTemplate.ACT_MEMORY))
                mob.SetActBit( MobTemplate.ACT_MEMORY );
            if (!mob.HasActBit(MobTemplate.ACT_HUNTER))
                if (mob.HasActBit(MobTemplate.ACT_SENTINEL))
                    mob.RemoveActBit(MobTemplate.ACT_SENTINEL);
            mob.SetAffBit( Affect.AFFECT_JUSTICE_TRACKER );
            mob._mobIndexData.AddSpecFun( "spec_justice_guard" );
            Combat.StartGrudge( mob, ch, false );

            return;
        }

        /// <summary>
        /// Returns a string representing the type of justice in an area.
        /// </summary>
        /// <param name="justice"></param>
        /// <returns></returns>
        public static string GetInvaderString( JusticeType justice )
        {
            switch( justice )
            {
                case JusticeType.good:
                    return "Evil races prohibited.";
                case JusticeType.evil:
                    return "Good races killed on sight.";
                case JusticeType.neutral:
                    return "All races welcome.";
                case JusticeType.neutral_only:
                    return "Evil and good races prohibited.";
                case JusticeType.chaotic_evil:
                    return "Good races killed on sight.";
                case JusticeType.chaotic_good:
                    return "Evil races killed on sight.";
                case JusticeType.chaotic_neutral_only:
                    return "Evil and good races killed on sight.";
                case JusticeType.good_only:
                    return "Evil and neutral races prohibited.";
                case JusticeType.evil_only:
                    return "Good and neutral races prohibited.";
                case JusticeType.chaotic_evil_only:
                    return "Good and neutral races killed on sight.";
                case JusticeType.chaotic_good_only:
                    return "Neutral and evil races killed on sight.";
                case JusticeType.none:
                    return "No justice.";
            } //end switch
            return null;
        }

    };


}