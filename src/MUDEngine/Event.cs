using System;
using System.IO;
using ModernMUD;

namespace MUDEngine
{
    /// <summary>
    /// An event represents something that happens periodically or once at some
    /// point in the future. Used for ticks, corpse decay, time-delayed spells,
    /// and other events that don't happen immediately.
    /// </summary>
    public class Event
    {
        private static bool _isSlowRound;
        private static bool _isHasteRound;
        static int _pulse;
        static int _hunger;
        private static int _numEvents;
        private EventType _type; // Event type
        private Target _arg1; // Can be one of many types
        private Target _arg2; // Can be one of many types
        private int _time; // Time in pulses
        private object _var;  // A miscellaneous variable
        
        /// <summary>
        /// Parameterized constructor.
        /// </summary>
        /// <param name="itype"></param>
        /// <param name="itime"></param>
        /// <param name="varg1"></param>
        /// <param name="varg2"></param>
        /// <param name="var"></param>
        public Event( EventType itype, int itime, Target varg1, Target varg2, object var )
        {
            ++_numEvents;

            _type = itype;
            _time = itime;
            _arg1 = varg1;
            _arg2 = varg2;
            _var = var;
        }

        /// <summary>
        /// Destructor decrements in-memory item count.
        /// </summary>
        ~Event()
        {
            --_numEvents;
        }

        /// <summary>
        /// Gets a count of the current number of events in memory.
        /// </summary>
        public static int Count
        {
            get
            {
                return _numEvents;
            }
        }

        public EventType Type
        {
            get { return _type; }
            set { _type = value; }
        }

        public object Var
        {
            get { return _var; }
            set { _var = value; }
        }

        public int Time
        {
            get { return _time; }
            set { _time = value; }
        }

        public Target Target1
        {
            get { return _arg1; }
            set { _arg1 = value; }
        }

        public Target Target2
        {
            get { return _arg2; }
            set { _arg2 = value; }
        }

        /// <summary>
        /// Types of events
        /// </summary>
        public enum EventType
        {
            none = 0,
            acid_arrow,
            aggression_update,
            area_update,
            autosave,
            bard_effect_decay,
            bard_song,
            berzerk,
            bombard,
            brain_drain,
            camp,
            cast_tick,
            char_update,
            conflaguration,
            creeping_doom,
            darkness_end,
            delayed_command,
            extract_character,
            fire_plane_damage,
            heartbeat,
            hit_gain,
            hunger_update,
            immolate,
            incendiary_cloud,
            ko_update,
            light_end,
            mana_gain,
            memorize_update,
            mob_hunting,
            mob_procedure,
            mobile_update,
            move_gain,
            object_decay,
            object_procedure,
            object_special,
            object_update,
            person_damage_spell,
            quest_procedure,
            regeneration,
            remove_justdied_bit,
            reset_area,
            room_affect_add,
            room_affect_remove,
            room_damage_spell,
            room_procedure,
            room_update,
            save_corpses,
            save_sysdata,
            ship_move,
            slow_char_update,
            spell_cast,
            spell_nightmares,
            spell_scribing,
            starshell_end,
            stun_update,
            swimming,
            track,
            violence_update,
            water_breathing,
            weather_update,
            weird,
            zone_procedure
        }

        public const int TICK_PER_SECOND = 4;
        public const int TICK_MEMORIZE = 4;
        public const int TICK_AGGRESS = 5;
        public const int TICK_COMBAT = ( 3 * TICK_PER_SECOND );  // Standard combat round spacing
        public const int TICK_COMBAT_UPDATE = ( TICK_COMBAT / 2 ); // Twice as fast to take care of hasted folks.
        public const int TICK_HITGAIN = ( 5 * TICK_PER_SECOND );
        public const int TICK_MANAGAIN = ( 4 * TICK_PER_SECOND + 1 );
        public const int TICK_MOVEGAIN = ( 3 * TICK_PER_SECOND );
        public const int TICK_ROOM = ( 3 * TICK_PER_SECOND );
        public const int TICK_OBJECT = ( 9 * TICK_PER_SECOND );
        public const int TICK_SONG = ( 8 * TICK_PER_SECOND );
        public const int TICK_MOBILE = ( 7 ); // Actually is 7 seconds,
        // but every 7 pulses is a mob lag update
        public const int NUM_MOB_PULSES = 4; // Used with TICK_MOBILE to determine the number of
        // mob pulses that pass between movement and special function checks.
        public const int TICK_LIST = ( 40 * TICK_PER_SECOND );
        // OBJ_UPDATE pulse decrements an object's timer, so an object
        // with a timer of 6 will last 3 minutes.
        public const int TICK_OBJ_UPDATE = ( 30 * TICK_PER_SECOND );
        public const int TICK_CHAR_UPDATE = ( 30 * TICK_PER_SECOND );
        public const int TICK_WEATHER = ( 120 * TICK_PER_SECOND );
        public const int TICK_AREA = ( 300 * TICK_PER_SECOND );
        public const int AREA_RESET_VARIABILITY = (60 * TICK_PER_SECOND);
        public const int TICK_SAVE_CORPSES = ( 600 * TICK_PER_SECOND ); /* 10 minutes */
        public const int TICK_SAVE_SYSDATA = ( 900 * TICK_PER_SECOND ); /* 15 minutes */
        public const int TICK_DB_DUMP = ( 1800 * TICK_PER_SECOND ); /* 30 minutes */
        public const int TICK_CAMP = ( 5 * TICK_PER_SECOND );

        /// <summary>
        /// Creates and initializes a new event and then puts it into the event list. 
        /// </summary>
        /// <param name="type"></param>
        /// <param name="time"></param>
        /// <param name="arg1"></param>
        /// <param name="arg2"></param>
        /// <param name="var"></param>
        /// <returns></returns>
        public static Event CreateEvent( EventType type, int time, Target arg1, Target arg2, object var )
        {
            Event eventdata = new Event( type, time, arg1, arg2, var );
            Database.EventList.Add( eventdata );
            return eventdata;
        }

        /// <summary>
        /// This is called once per pulse.
        /// 
        /// Be sure to check for nulls and exceptions - this thing handles most activity
        /// in the game and targets may no longer exist.
        /// </summary>
        public static void EventUpdate()
        {
            Room room;
            CharData ch;
            Event eventdata;

            // Handle all the events
            //
            // We need the ToArray() method in order to avoid seeing the following error:
            // "Collection was modified; enumeration operation may not execute."
            foreach( Event it in Database.EventList.ToArray() )
            {
                eventdata = it;
                // We use nexteventdata so we can remove eventdatas from the list without losting our place
                eventdata._time -= 1;
                // Be sure and send them the stars for spellcast ticks.
                if( eventdata._type == EventType.spell_cast )
                {
                    if (((CharData)eventdata._arg1).IsAffected(Affect.AFFECT_CASTING))
                    {
                        if( !( (CharData)eventdata._arg1 ).IsNPC()
                                && ( (CharData)eventdata._arg1 ).HasActionBit(PC.PLAYER_CAST_TICK )
                                && eventdata._time > 0
                                && eventdata._time % TICK_PER_SECOND == 0 )
                        {
                            string text = "&+LCasting &n";
                            text += ((Spell)eventdata._var).Name;
                            text += "&n&+r:&+R ";
                            int count;
                            for( count = 0; count < ( eventdata._time / TICK_PER_SECOND ); ++count )
                            {
                                text += "*";
                            }
                            text += "&n\r\n";
                            ( (CharData)eventdata._arg1 ).SendText( text );
                        }
                    }
                    else
                    {
                        // Abort the spell
                        eventdata._time = 0;
                    }
                }
                if( eventdata._time <= 0 )
                {
                    switch( eventdata._type )
                    {
                        case EventType.spell_cast:
                            // We stored the CharData in arg1
                            // We stored the spell in var
                            // We stored the _targetType argument in arg2
                            try
                            {
                                Magic.FinishSpell((CharData)eventdata._arg1, (Spell)eventdata._var, eventdata._arg2);
                            }
                            catch(Exception ex)
                            {
                                Log.Error("Spell casting event error: " + ex);
                            }
                            Database.EventList.Remove( it );
                            break;
                        case EventType.bard_song:
                            ch = (CharData)eventdata._arg1;
                            if( ch == null )
                                break;
                            if( ch._currentMana < ((Song)eventdata._var)._minimumMana )
                            {
                                SocketConnection.Act( "$n&n chokes and falls silent.", ch, null, null, SocketConnection.MessageTarget.room );
                                break;
                            }
                            ch._currentMana -= ((Song)eventdata._var)._minimumMana;
                            // We stored the CharData in arg1
                            // We stored the spell in var
                            // We stored the target argument in arg2
                            try
                            {
                                Magic.SongVerse( (CharData)eventdata._arg1, (Song)eventdata._var, eventdata._arg2 );
                            }
                            catch(Exception ex)
                            {
                                Log.Error("Bard song event error: " + ex);
                            }
                            Database.EventList.Remove( it );
                            break;
                        case EventType.acid_arrow:
                            Log.Trace( "Event acid arrow" );
                            // We stored the CharData in arg1 for ch
                            // We stored the CharData in arg2 for victim
                            // We stored the damage in var
                            if( !(CharData)eventdata._arg2 || !(CharData)eventdata._arg1 )
                                break;
                            Combat.InflictSpellDamage( (CharData)eventdata._arg1, (CharData)eventdata._arg2, (int)eventdata._var, "acid arrow", AttackType.DamageType.acid );
                            Database.EventList.Remove( it );
                            break;
                        case EventType.immolate:
                            Log.Trace( "Event immolate" );
                            // We stored the CharData in arg1 for ch
                            // We stored the CharData in arg2 for victim
                            // We stored the damage in var
                            if( !(CharData)eventdata._arg2 || !(CharData)eventdata._arg1 )
                                break;
                            Combat.InflictSpellDamage((CharData)eventdata._arg1, (CharData)eventdata._arg2, (int)eventdata._var, "immolate", AttackType.DamageType.fire);
                            Database.EventList.Remove( it );
                            break;
                        case EventType.spell_nightmares:
                            Log.Trace( "Event nightmares" );
                            // We stored the CharData in arg1 for ch
                            // We stored the CharData in arg2 for victim
                            // We stored the damage in var
                            if( !(CharData)eventdata._arg2 || !(CharData)eventdata._arg1 )
                                break;
                            Combat.InflictSpellDamage((CharData)eventdata._arg1, (CharData)eventdata._arg2, (int)eventdata._var, "nightmares", AttackType.DamageType.mental);
                            Spell spl = Spell.SpellList["sleep"];
                            if (spl != null)
                            {
                                spl.Invoke((CharData)eventdata._arg1, ((CharData)eventdata._arg1)._level, eventdata._arg2);
                            }
                            Database.EventList.Remove( it );
                            break;
                        case EventType.weird:
                            Log.Trace( "Event weird" );
                            // We stored the CharData in arg1 for ch
                            // We stored the CharData in arg2 for victim
                            // We stored the damage in var
                            if( !(CharData)eventdata._arg2 || !(CharData)eventdata._arg1 )
                                break;
                            Combat.InflictSpellDamage((CharData)eventdata._arg1, (CharData)eventdata._arg2, (int)eventdata._var, "weird", AttackType.DamageType.mental);
                            Database.EventList.Remove( it );
                            break;
                        case EventType.conflaguration:
                            Log.Trace( "Event conflaguration" );
                            // We stored the CharData in arg1 for ch
                            // We stored the CharData in arg2 for victim
                            // We stored the damage in var
                            if( !(CharData)eventdata._arg2 || !(CharData)eventdata._arg1 )
                                break;
                            Combat.InflictSpellDamage((CharData)eventdata._arg1, (CharData)eventdata._arg2, (int)eventdata._var, "conflaguration", AttackType.DamageType.fire);
                            Database.EventList.Remove( it );
                            break;
                        case EventType.remove_justdied_bit:
                            Log.Trace( "Event remove justdied bit" );
                            // We stored the CharData in arg1 for ch
                            // We stored the CharData in arg2 for victim
                            // We stored the damage in var
                            Log.Trace( "checking just died args" );
                            if( !(CharData)eventdata._arg1 || ( (CharData)eventdata._arg1 ).IsNPC() )
                            {
                                break;
                            }
                            Log.Trace( "removing just died bit" );
                            ( (CharData)eventdata._arg1 ).RemoveActionBit(PC.PLAYER_JUST_DIED );
                            Log.Trace( "done with just died eventdata, erasing from list" );
                            Database.EventList.Remove( it );
                            Log.Trace( "deleting just died eventdata." );
                            break;
                        case EventType.creeping_doom:
                            Log.Trace( "Event creeping doom" );
                            // We stored the CharData in arg2 for room
                            // We stored the CharData in arg1 for ch (caster)
                            // We stored the damage in var
                            room = (Room)eventdata._arg2;
                            if( !room || !( ch = (CharData)eventdata._arg1 ) )
                            {
                                break;
                            }
                            for( int i = (room.People.Count -1); i >= 0; i-- )
                            {
                                CharData ivictim = room.People[i];
                                if( ch && ch.IsSameGroup( ivictim ) )
                                    continue;

                                Combat.InflictSpellDamage(ch, ivictim, (int)eventdata._var, "creeping doom", AttackType.DamageType.harm);
                            }
                            Database.EventList.Remove( it );
                            break;
                        case EventType.bombard:
                            Log.Trace( "Event bombard" );
                            // We stored the CharData in arg2 for room
                            // We stored the CharData in arg1 for ch (caster)
                            // We stored the damage in var
                            room = (Room)eventdata._arg2;
                            if( !room || !( ch = (CharData)eventdata._arg1 ) )
                            {
                                break;
                            }
                            for( int i = room.People.Count - 1; i >= 0; i-- )
                            {
                                CharData ivictim = room.People[i];
                                if( ch && ch.IsSameGroup( ivictim ) )
                                    continue;

                                Combat.InflictSpellDamage(ch, ivictim, (int)eventdata._var, "bombard", AttackType.DamageType.harm);
                            }
                            Database.EventList.Remove( it );
                            break;
                        case EventType.incendiary_cloud:
                            Log.Trace( "Event incendiary cloud" );
                            // We stored the CharData in arg2 for room
                            // We stored the CharData in arg1 for ch (caster)
                            // We stored the damage in var
                            room = (Room)eventdata._arg2;
                            if( !room || !( ch = (CharData)eventdata._arg1 ) )
                            {
                                break;
                            }
                            for (int i = room.People.Count - 1; i >= 0; i--)
                            {
                                CharData ivictim = room.People[i];
                                if (ch && ch.IsSameGroup(ivictim))
                                    continue;

                                Combat.InflictSpellDamage(ch, ivictim, (int)eventdata._var, "incendiary cloud", AttackType.DamageType.fire);
                            }
                            Database.EventList.Remove( it );
                            break;
                        case EventType.light_end:
                            Log.Trace( "Event light end" );               //  Continual light eventdata
                            room = (Room)eventdata._arg1;
                            if( !room )
                            {
                                break;
                            }
                            if( room.HasFlag( RoomTemplate.ROOM_MAGICLIGHT ) )
                            {
                                room.RemoveFlag( RoomTemplate.ROOM_MAGICLIGHT );
                                foreach( CharData watcher in room.People )
                                {
                                    watcher.SendText( "&+WThe magical light subsides.\r\n" );
                                }
                            }
                            Database.EventList.Remove( it );
                            break;
                        case EventType.darkness_end:
                            Log.Trace( "Event darkness end" );
                            room = (Room)eventdata._arg1;
                            if( !room )
                            {
                                break;
                            }
                            if( room.HasFlag( RoomTemplate.ROOM_MAGICDARK ) )
                            {
                                room.RemoveFlag( RoomTemplate.ROOM_MAGICDARK );
                                foreach( CharData charData in room.People )
                                {
                                    charData.SendText( "&+LThe darkness&n seems to lift a bit.\r\n" );
                                }
                            }
                            Database.EventList.Remove( it );
                            break;
                        case EventType.starshell_end:
                            Log.Trace( "Event starshell end" );
                            Database.EventList.Remove( it );
                            room = (Room)eventdata._arg1;
                            if( !room )
                            {
                                break;
                            }
                            if( room.HasFlag( RoomTemplate.ROOM_EARTHEN_STARSHELL ) || room.HasFlag( RoomTemplate.ROOM_FIERY_STARSHELL )
                                    || room.HasFlag( RoomTemplate.ROOM_AIRY_STARSHELL ) || room.HasFlag( RoomTemplate.ROOM_WATERY_STARSHELL ) )
                            {
                                room.RemoveFlag( RoomTemplate.ROOM_EARTHEN_STARSHELL );
                                room.RemoveFlag( RoomTemplate.ROOM_AIRY_STARSHELL );
                                room.RemoveFlag( RoomTemplate.ROOM_FIERY_STARSHELL );
                                room.RemoveFlag( RoomTemplate.ROOM_WATERY_STARSHELL );
                                foreach( CharData roomChar in room.People )
                                {
                                    roomChar.SendText( "The &+Ystarshell&n shatters and fades away.\r\n" );
                                }
                            }
                            if( room.HasFlag( RoomTemplate.ROOM_HYPNOTIC_PATTERN ) )
                            {
                                room.RemoveFlag( RoomTemplate.ROOM_HYPNOTIC_PATTERN );
                                foreach( CharData roomChar in room.People )
                                {
                                    roomChar.SendText( "&+CThe pa&+cttern &+Bfades a&+bway.&n\r\n" );
                                }
                            }
                            break;
                        case EventType.hit_gain:
                            HitpointUpdate();
                            eventdata._time = TICK_HITGAIN;
                            break;
                        case EventType.mana_gain:
                            ManaUpdate();
                            eventdata._time = TICK_MANAGAIN;
                            break;
                        case EventType.move_gain:
                            MovementPointUpdate();
                            eventdata._time = TICK_MOVEGAIN;
                            break;
                        // This particular eventdata could cause things to get pretty hairy if not used right.
                        case EventType.extract_character:
                            ch = (CharData)eventdata._arg1;
                            if( ch == null )
                            {
                                break;
                            }
                            Combat.StopFighting( ch, true );
                            if( ch._inRoom )
                            {
                                if( ch.IsNPC() )
                                {
                                    SocketConnection.Act( "$n&n disappears.", ch, null, null, SocketConnection.MessageTarget.room );
                                }
                                else
                                {
                                    SocketConnection.Act( "$n&n is lost to the void.", ch, null, null, SocketConnection.MessageTarget.room );
                                    ( (PC)ch ).LastRentLocation = ch._inRoom.IndexNumber;
                                    CharData.SavePlayer( ch );
                                }
                            }
                            CharData.ExtractChar( ch, true );
                            
                            Database.EventList.Remove( it );
                            break;
                        case EventType.camp:
                            // Camp_update returns true if they're still camping.
                            if( !CampUpdate( (CharData)eventdata._arg1, (Room)eventdata._arg2 ) )
                            {
                                ( (CharData)eventdata._arg1 ).RemoveActionBit(PC.PLAYER_CAMPING );
                                Database.EventList.Remove( it );
                            }
                            else
                            {
                                // 3 ticks off each time if standing, 2 if at least
                                // sitting, 1 if at least resting, 0 if sleeping.
                                int pos = ((CharData)eventdata._arg1)._position;
                                int timer = (int)eventdata._var;
                                if( pos == Position.standing )
                                    eventdata._var = (object)(--timer);
                                if( pos >= Position.sitting )
                                    eventdata._var = (object)(--timer);
                                if( pos > Position.sleeping )
                                    eventdata._var = (object)(--timer);
                                if( (int)eventdata._var <= 0 )
                                {
                                    SocketConnection.Quit((CharData)eventdata._arg1 );
                                    Database.EventList.Remove( it );
                                }
                                else
                                {
                                    eventdata._time = TICK_CAMP;
                                }
                            }
                            break;
                        case EventType.memorize_update:
                            ImmortalChat.SendImmortalChat( null, ImmortalChat.IMMTALK_TICKS, Limits.LEVEL_HERO, "Memorize update eventdata" );
                            Magic.MemorizeUpdate();
                            eventdata._time = TICK_MEMORIZE;
                            break;
                        case EventType.aggression_update:
                            ImmortalChat.SendImmortalChat( null, ImmortalChat.IMMTALK_TICKS, Limits.LEVEL_HERO, "Aggression eventdata" );
                            AggressionUpdate();
                            eventdata._time = TICK_AGGRESS;
                            break;
                        case EventType.heartbeat:
                            ImmortalChat.SendImmortalChat( null, ImmortalChat.IMMTALK_TICKS, Limits.LEVEL_HERO, "Heartbeat eventdata" );
                            HeartbeatUpdate();
                            eventdata._time = TICK_WEATHER;
                            break;
                        case EventType.weather_update:
                            ImmortalChat.SendImmortalChat( null, ImmortalChat.IMMTALK_TICKS, Limits.LEVEL_HERO, "Weather update eventdata" );
                            Database.SystemData.UpdateWeather();
                            eventdata._time = TICK_WEATHER;
                            break;
                        case EventType.char_update:
                            ImmortalChat.SendImmortalChat( null, ImmortalChat.IMMTALK_TICKS, Limits.LEVEL_HERO, "Char update eventdata" );
                            CharacterUpdate();
                            eventdata._time = TICK_CHAR_UPDATE;
                            break;
                        case EventType.object_special:
                            // Object specials only.
                            ImmortalChat.SendImmortalChat( null, ImmortalChat.IMMTALK_TICKS, Limits.LEVEL_HERO, "Object special eventdata" );
                            ObjectSpecialUpdate();
                            eventdata._time = TICK_OBJECT;
                            break;
                        case EventType.area_update:
                            ImmortalChat.SendImmortalChat( null, ImmortalChat.IMMTALK_TICKS, Limits.LEVEL_LESSER_GOD, "Area update eventdata" );
                            Database.AreaUpdate();
                            eventdata._time = TICK_AREA;
                            break;
                        case EventType.room_update:
                            ImmortalChat.SendImmortalChat( null, ImmortalChat.IMMTALK_TICKS, Limits.LEVEL_LESSER_GOD, "Room update eventdata" );
                            RoomUpdate();
                            eventdata._time = TICK_ROOM;
                            break;
                        case EventType.mobile_update:
                            ImmortalChat.SendImmortalChat( null, ImmortalChat.IMMTALK_TICKS, Limits.LEVEL_LESSER_GOD, "Mobile update eventdata" );
                            MobileUpdate();
                            eventdata._time = TICK_MOBILE;
                            break;
                        case EventType.object_update:
                            // Object timers only
                            ImmortalChat.SendImmortalChat( null, ImmortalChat.IMMTALK_TICKS, Limits.LEVEL_LESSER_GOD, "Object update eventdata" );
                            ObjectUpdate();
                            eventdata._time = TICK_OBJ_UPDATE;
                            break;
                        case EventType.violence_update:
                            ImmortalChat.SendImmortalChat( null, ImmortalChat.IMMTALK_TICKS, Limits.LEVEL_LESSER_GOD, "Violence update pulse" );
                            ViolenceUpdate();
                            eventdata._time = TICK_COMBAT_UPDATE;
                            break;
                        case EventType.save_sysdata:
                            ImmortalChat.SendImmortalChat( null, ImmortalChat.IMMTALK_TICKS, Limits.LEVEL_LESSER_GOD, "Save Sysdata eventdata" );
                            Sysdata.Save();
                            eventdata._time = TICK_SAVE_SYSDATA;
                            break;
                        case EventType.save_corpses:
                            ImmortalChat.SendImmortalChat( null, ImmortalChat.IMMTALK_TICKS, Limits.LEVEL_LESSER_GOD, "Save corpse eventdata" );
                            Database.CorpseList.Save();
                            eventdata._time = TICK_SAVE_CORPSES;
                            break;
                        default:
                            Log.Error( "Unsupported Event Type {0}", eventdata._type );
                            break;
                    } // end switch
                } // end if
            }

            TimeUpdate();
            return;
        }

        /// <summary>
        /// This function is used to delete all of the eventdatas attached to
        /// a character when they die or leave the game.
        ///
        /// Accessing a terminated character is big time bad news, since
        /// pointers will get way outta whack, the wrong characters will
        /// be affected, effects will persist after death possibly causing
        /// multiple deaths, and the mud may crash randomly and seemingly
        /// without reason.
        ///
        /// Affects like immolate and such still need to be resolved
        /// (If the caster dies, should the spell persist? Probably so.)
        /// </summary>
        /// <param name="ch"></param>
        public static void DeleteAttachedEvents( CharData ch )
        {
            Event eventdata;

            for( int i = (Database.EventList.Count - 1); i >= 0; i-- )
            {
                eventdata = Database.EventList[i];

                CharData eventCharacter;
                switch( eventdata._type )
                {
                    case EventType.camp:
                        eventCharacter = (CharData)eventdata._arg1;
                        if( !eventCharacter || eventCharacter == ch )
                        {
                            Database.EventList.Remove( eventdata );
                        }
                        break;
                    case EventType.extract_character:
                        // Does not eventdata = null upom caster's death.
                        // This may be a problem.
                        // However, it would be really smooth if the caster
                        // was able to kill someone after their death.
                        // This could cause problems if say, a shadow monster
                        // was killed before it was supposed to disappear.
                        // This would cause a corpse to be left for something
                        // that should not leave a corpse.
                        // However, the shadow monsters and such should be flagged
                        // race illusion and not leave corpses.
                        eventCharacter = (CharData)eventdata._arg1;
                        if( !eventCharacter || eventCharacter == ch )
                        {
                            Database.EventList.Remove(eventdata);
                        }
                        break;
                    case EventType.immolate:
                        // Does not eventdata = null upom caster's death.
                        // This may be a problem.
                        // However, it would be really smooth if the caster
                        // was able to kill someone after their death.
                        eventCharacter = (CharData)eventdata._arg2;
                        if( !eventCharacter || eventCharacter == ch )
                        {
                            Database.EventList.Remove(eventdata);
                        }
                        break;
                    case EventType.bombard:
                        // Room specific eventdata, should be deleted only if it
                        // should not continue if the caster is dead.
                        break;
                    case EventType.creeping_doom:
                        // Room specific eventdata, should be deleted only if it
                        // should not continue if the caster is dead.
                        break;
                    case EventType.incendiary_cloud:
                        // Room specific eventdata, should be deleted only if it
                        // should not continue if the caster is dead.
                        break;
                    case EventType.conflaguration:
                        // Does not eventdata = null upom caster's death.
                        // This may be a problem.
                        // However, it would be really smooth if the caster
                        // was able to kill someone after their death.
                        eventCharacter = (CharData)eventdata._arg2;
                        if( !eventCharacter || eventCharacter == ch )
                        {
                            Database.EventList.Remove(eventdata);
                        }
                        break;
                    case EventType.acid_arrow:
                        // Does not eventdata = null upom caster's death.
                        // This may be a problem.
                        // However, it would be really smooth if the caster
                        // was able to kill someone after their death.
                        eventCharacter = (CharData)eventdata._arg2;
                        if( !eventCharacter || eventCharacter == ch )
                        {
                            Database.EventList.Remove(eventdata);
                        }
                        break;
                    case EventType.spell_cast:
                        // Does not eventdata = null upon nonexistance of _targetType.
                        // This may be a problem.
                        // Ideally we should only have to worry about the caster
                        // and the validity of the _targetType is checked in finish_spell()
                        eventCharacter = (CharData)eventdata._arg1;
                        if( !eventCharacter || eventCharacter == ch )
                        {
                            SocketConnection.Act( "$n&n stops chanting abruptly!", ch, null, null, SocketConnection.MessageTarget.room );
                            Database.EventList.Remove(eventdata);
                        }
                        break;
                    case EventType.bard_song:
                        // All bard songs are area effects and not victim-dependent
                        eventCharacter = (CharData)eventdata._arg1;
                        if( !eventCharacter || eventCharacter == ch )
                        {
                            SocketConnection.Act( "$n&n gasps and falls silent.", ch, null, null, SocketConnection.MessageTarget.room );
                            Database.EventList.Remove(eventdata);
                        }
                        break;
                }
            }
            return;
        }

        /// <summary>
        /// Prints the event type as a string.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            if (this == null)
            {
                return "null eventdata";
            }
            return _type.ToString();
        }

        static void HitpointUpdate()
        {
            CharData ch;

            foreach( CharData it in Database.CharList )
            {
                ch = it;

                if( ch._position >= Position.incapacitated )
                {
                    if (ch._hitpoints < ch.GetMaxHit())
                    {
                        ch._hitpoints += CharData.HitGain(ch);
                    }
                    else if (ch._hitpoints > ch.GetMaxHit())
                    {
                        ch._hitpoints--;
                    }
                }
                ch.UpdatePosition();
            }
        }

        static void ManaUpdate()
        {
            foreach( CharData ch in Database.CharList )
            {
                if( ch._position >= Position.incapacitated )
                {
                    if (ch._currentMana < ch._maxMana)
                    {
                        ch._currentMana += CharData.ManaGain(ch);
                    }
                }
            }
        }

        static void MovementPointUpdate()
        {
            foreach( CharData ch in Database.CharList )
            {
                if( ch._currentMoves < ch._maxMoves )
                {
                    if( ch._position > Position.incapacitated )
                        ch._currentMoves += CharData.MoveGain( ch );
                }
            }
        }

        /// <summary>
        /// Camping timer ticking away.
        /// 
        /// Uses a single event for each person that is actively camping.
        /// </summary>
        /// <param name="ch"></param>
        /// <param name="room"></param>
        /// <returns></returns>
        static bool CampUpdate( CharData ch, Room room )
        {
            if( !ch || !room || ch._position <= Position.incapacitated )
                return false;
            if( !ch.HasActionBit(PC.PLAYER_CAMPING ) )
                return false;
            if( ch._position == Position.fighting || ch._fighting || ch._inRoom != room )
            {
                ch.SendText( "So much for that camping effort.\r\n" );
                ch.RemoveActionBit(PC.PLAYER_CAMPING);
                return false;
            }
            return true;
        }

        /// <summary>
        /// Autonomous mobile actions.
        /// </summary>
        static void MobileUpdate()
        {
            Exit exit;
            _pulse = ( ++_pulse % NUM_MOB_PULSES );
            // Examine all mobs.
            for( int i = (Database.CharList.Count - 1); i >= 0; i--)
            {
                CharData ch = Database.CharList[i];
                int rnum;

                if( !ch._inRoom )
                    continue;

                if( !ch.IsNPC() )
                    continue;

                // Mobs may have to wait for lag too
                if( ch._wait > 0 )
                {
                    ch._wait -= TICK_MOBILE;
                    if( ch._wait < 0 )
                    {
                        ch._wait = 0;
                    }
                    continue;
                }

                // Mobs bashed or knocked down will try to get back up...
                if( ch._position < Position.fighting && ch._position > Position.sleeping
                        && ( ch._position != ch._mobTemplate.DefaultPosition || ch._fighting ) )
                {
                    CommandType.Interpret( ch, "stand" );
                    continue;
                }

                if (ch._position == Position.sleeping && !ch.IsAffected( Affect.AFFECT_SLEEP ))
                {
                    // If there is a fight going on have a chance to waken
                    foreach( CharData roomChar in ch._inRoom.People )
                    {
                        if (roomChar._fighting && MUDMath.NumberBits(3) == 0 && !ch.IsAffected(Affect.AFFECT_SLEEP))
                        {
                            SocketConnection.Act( "$n awakens from $s slumber.", ch, null, null, SocketConnection.MessageTarget.room );
                            ch._position = Position.reclining;
                            break;
                        }
                    }
                }

                if( ch._position == Position.sleeping )
                    continue;

                // If we're just updating lag, we have no need at all for any
                // mob movement or special function code
                // every mob pulse-check they'll just check for stand and such, but they
                // won't check special functions.
                if( _pulse != 0 )
                    continue;

                // Only thing charmies will do is autostand
                if( ch.IsAffected( Affect.AFFECT_CHARM ) )
                    continue;

                /* Examine call for special procedure */
                // FIXME: specs broken, now disabled.
                // TODO: Reactivate mob special functions.
                /*if ( ch.spec_fun != 0 )
                {
                MobSpecial* spec ;
                bool executed = false;
                for( spec = ch.spec_fun; spec; spec = spec.next )
                {
                if ( ( *spec.spec_fun ) ( ch, MobFun.PROC_NORMAL ) )
                {
                executed = true;
                break;
                }
                }
                if( executed )
                continue;
                }*/

                // check if MobSpecial wiped out ch
                if( ch == null )
                {
                    continue;
                }
                if( !ch._fighting && ch._hunting && ch._position == Position.standing )
                {
                    ch.WaitState( 2 * TICK_COMBAT );
                    Track.HuntVictim( ch );
                    continue;
                }

                // That's all for a sleeping or busy monster.
                if( ch._position < Position.standing )
                    continue;

                /* MOBprogram random trigger */
                if( ch._inRoom.Area.NumPlayers > 0 )
                {
                    //            prog_random_trigger( ch );
                    /* If ch dies or changes
                    position due to it's random
                    trigger, continue */
                    if( ch._position < Position.standing )
                    {
                        continue;
                    }
                }

                // Scavenge
                if( ch.HasActionBit(MobTemplate.ACT_SCAVENGER ) && ch._inRoom.Contents.Count != 0
                    && MUDMath.NumberBits( 2 ) == 0 )
                {
                    int max = 1;
                    Object objBest = null;
                    foreach( Object obj in ch._inRoom.Contents )
                    {
                        if( obj.HasWearFlag( ObjTemplate.WEARABLE_CARRY )
                                && obj.Cost > max
                                && CharData.CanSeeObj( ch, obj ) )
                        {
                            objBest = obj;
                            max = obj.Cost;
                        }
                    }

                    if( objBest )
                    {
                        objBest.RemoveFromRoom();
                        objBest.ObjToChar( ch );
                        SocketConnection.Act( "$n&n gets $p&n.", ch, objBest, null, SocketConnection.MessageTarget.room );
                    }
                }
 
                // Wander or flee.
                if( ch._hitpoints < ch.GetMaxHit() / 5 )
                {
                    rnum = 3;
                }
                else
                {
                    rnum = 5;
                }

                Exit.Direction door;
                if( !ch.HasActionBit(MobTemplate.ACT_SENTINEL ) && ( door = (Exit.Direction)MUDMath.NumberBits( rnum ) ) != Exit.Direction.invalid
                    && (exit = ch._inRoom.GetExit(door)) && exit.TargetRoom && !exit.HasFlag(Exit.ExitFlag.closed)
                    && !Room.GetRoom(exit.IndexNumber).HasFlag( RoomTemplate.ROOM_NO_MOB ) && ( !ch.HasActionBit(MobTemplate.ACT_STAY_AREA )
                    || exit.TargetRoom.Area == ch._inRoom.Area )
                    && !( !Room.GetRoom(exit.IndexNumber).IsWater() && ch.HasInnate( Race.RACE_SWIM ) ) )
                {
                    // Give message if hurt.
                    if( rnum == 3 )
                    {
                        SocketConnection.Act( "$n&n wanders off terrified!", ch, null, null, SocketConnection.MessageTarget.room );
                    }

                    ch.Move( door );
                    // In case ch changes position due to its or some other mob's movement via MOBProgs.
                    if( ch._position < Position.standing )
                    {
                        continue;
                    }
                }

                // If people are in the room, then flee.
                if( rnum == 3 && !ch.HasActionBit(MobTemplate.ACT_SENTINEL ) )
                {
                    for (int j = (ch._inRoom.People.Count - 1); j >= 0; j-- )
                    {
                        CharData roomCharacter = ch._inRoom.People[j];
                        // If NPC can't see PC it shouldn't feel fear.
                        if (!roomCharacter.IsNPC() && CharData.CanSee(ch, roomCharacter) && !roomCharacter.IsImmortal())
                        {
                            int direction;
                            door = Exit.Direction.invalid;

                            if (Combat.IsFearing(ch, roomCharacter))
                            {
                                string text;
                                switch (MUDMath.NumberBits(5))
                                {
                                    default:
                                        break;
                                    case 0:
                                        text = String.Format("Get away from me, {0}!", roomCharacter._name);
                                        CommandType.Interpret(ch, "yell " + text);
                                        break;
                                    case 1:
                                        text = String.Format("Leave me be, {0}!", roomCharacter._name);
                                        CommandType.Interpret(ch, "yell " + text);
                                        break;
                                    case 2:
                                        text = String.Format("{0} is trying to kill me! Help!", roomCharacter._name);
                                        CommandType.Interpret(ch, "yell " + text);
                                        break;
                                    case 3:
                                        text = String.Format("Someone save me from {0}!", roomCharacter._name);
                                        CommandType.Interpret(ch, "yell " + text);
                                        break;
                                }
                            }

                            SocketConnection.Act("$n&n attempts to flee...", ch, null, null, SocketConnection.MessageTarget.room);
                            if (ch.IsAffected(Affect.AFFECT_HOLD) || ch.IsAffected(Affect.AFFECT_MINOR_PARA) ||
                                    ch.IsAffected(Affect.AFFECT_BOUND))
                            {
                                SocketConnection.Act("$n&n cannot move!", ch, null, null, SocketConnection.MessageTarget.room);
                                break;
                            }

                            // Find an exit giving each one an equal chance.
                            for (direction = 0; direction < Limits.MAX_DIRECTION; direction++)
                            {
                                if (ch._inRoom.ExitData[direction] && MUDMath.NumberRange(0, direction) == 0)
                                {
                                    door = (Exit.Direction)direction;
                                }
                            }

                            // If no exit, attack.  Else flee!
                            if (door == Exit.Direction.invalid)
                            {
                                ch.AttackCharacter( roomCharacter );
                            }
                            else
                            {
                                ch.Move(door);
                            }
                            break;
                        }
                    }
                }

                // Wander back home last, low priority.
                if( !ch._fighting && !ch._hunting
                        && ch._position == Position.standing
                        && ch.HasActionBit(MobTemplate.ACT_SENTINEL )
                        && ch._loadRoomIndexNumber != 0
                        && ch._loadRoomIndexNumber != ch._inRoom.IndexNumber )
                {
                    Track.ReturnToLoad( ch );
                }

            }
            return;
        }

        /// <summary>
        /// Updates all characters, mobs and players alike.
        /// 
        /// This function is performance sensitive.
        /// </summary>
        static void CharacterUpdate()
        {
            CharData ch;
            CharData chSave = null;
            CharData chQuit = null;

            _hunger = ( ( ++_hunger ) % 16 );

            DateTime saveTime = Database.SystemData.CurrentTime;

            for( int i = (Database.CharList.Count - 1); i >= 0; i-- )
            {
                ch = Database.CharList[i];

                if( !ch._inRoom )
                {
                    continue;
                }

                if( ch._position == Position.stunned )
                    ch.UpdatePosition();

                if( ch._position == Position.dead )
                {
                    ch.SendText( "&+lYour soul finally leaves your body.&n\r\n" );
                    SocketConnection.Act( "&+l$n&+l's corpse grows &+bcold&+l.", ch, null, null, SocketConnection.MessageTarget.room );
                    Combat.KillingBlow( ch, ch );
                    continue;
                }

                if( ch._position == Position.fighting && !ch._fighting )
                    ch._position = Position.standing;

                if( !ch.IsNPC() )
                    ch.UpdateInnateTimers();

                Affect affect;
                for( int j = (ch._affected.Count - 1); j >= 0; j-- )
                {
                    affect = ch._affected[j];
                    if( affect.Duration > 0 )
                        affect.Duration--;
                    else if( affect.Duration < 0 )
                    {
                        // Permanent affect, not on a timer.
                    }
                    else
                    {
                        if( affect.Type == Affect.AffectType.skill && Skill.SkillList[ affect.Value ].WearOffMessage.Length > 0 )
                        {
                            ch.SendText(Skill.SkillList[affect.Value].WearOffMessage);
                            ch.SendText( "\r\n" );

                            if( affect.Value == "vampiric bite" )
                                ch.SetPermRace( Race.RACE_VAMPIRE );
                        }
                        else if (affect.Type == Affect.AffectType.spell && Spell.SpellList[affect.Value].MessageWearOff.Length > 0)
                        {
                            ch.SendText( Spell.SpellList[ affect.Value ].MessageWearOff );
                            ch.SendText( "\r\n" );
                        }
                        else if( affect.Type == Affect.AffectType.song && Database.SongList[ affect.Value ]._messageWearOff.Length > 0 )
                        {
                            ch.SendText( Database.SongList[ affect.Value ]._messageWearOff );
                            ch.SendText( "\r\n" );
                        }

                        ch.RemoveAffect( ch._affected[j] );
                    }
                }

                if( ch.IsAffected( Affect.AFFECT_VACANCY ) && !ch.IsAffected( Affect.AFFECT_HIDE ) )
                {
                    ch.SendText( "You become part of your surroundings.\r\n" );
                    SocketConnection.Act( "$n&n fades from view.", ch, null, null, SocketConnection.MessageTarget.room );
                    ch.SetAffectBit( Affect.AFFECT_HIDE );
                }

                // Careful with the damages here, MUST NOT refer to ch after damage taken,
                // as it may be lethal damage (on NPC).
                if (ch.IsAffected(Affect.AFFECT_DISEASE))
                {
                    Affect plague = new Affect();
                    int level = 0;

                    SocketConnection.Act( "$n&n writhes in agony as plague sores erupt from $s skin.", ch, null, null, SocketConnection.MessageTarget.room );
                    ch.SendText( "You writhe in agony from the plague.\r\n" );
                    foreach (Affect af in ch._affected)
                    {
                        if (af.Type == Affect.AffectType.spell && af.Value == "plague")
                        {
                            level = af.Level;
                            break;
                        }
                    }

                    if( level == 0 )
                    {
                        ch.RemoveAffect( Affect.AFFECT_DISEASE );
                        continue;
                    }

                    if( level == 1 )
                        continue;

                    plague.Type = Affect.AffectType.spell;
                    plague.Value = "plague";
                    plague.Level = level - 1;
                    plague.Duration = MUDMath.NumberRange( 1, 2 * plague.Level );
                    plague.AddModifier(Affect.Apply.strength, -5);
                    plague.SetBitvector( Affect.AFFECT_DISEASE );
                    int save = plague.Level;

                    foreach( CharData ivch in ch._inRoom.People )
                    {
                        if( save != 0 && !Magic.SpellSavingThrow( save, ivch, AttackType.DamageType.disease )
                                && !ivch.IsImmortal()
                                && !ivch.IsAffected(Affect.AFFECT_DISEASE)
                                && MUDMath.NumberBits( 4 ) == 0 )
                        {
                            ivch.SendText( "You feel hot and feverish.\r\n" );
                            SocketConnection.Act( "$n&n shivers and looks very ill.", ivch, null, null,
                                 SocketConnection.MessageTarget.room );
                            ivch.CombineAffect(plague);
                        }
                    }

                    int dam = Math.Min( ch._level, 5 );
                    ch._currentMana -= dam;
                    ch._currentMoves -= dam;
                    Combat.InflictSpellDamage( ch, ch, dam, "plague", AttackType.DamageType.disease );
                }

                if( ( Database.SystemData.GameHour > 5 && Database.SystemData.GameHour < 21 )
                        && CharData.CheckSusceptible( ch, Race.DamageType.light ) )
                {
                    int dmg = 0;

                    // Shouldn't this mess be a switch statement?
                    if( ch.IsUnderground() )
                    {
                        dmg = 0;
                    }
                    if( ch._inRoom.TerrainType == TerrainType.inside )
                    {
                        dmg = 1;
                    }
                    else
                    {
                        if( ch._inRoom.TerrainType == TerrainType.forest ||
                                ch._inRoom.TerrainType == TerrainType.swamp )
                        {
                            dmg = 2;
                        }
                        else
                        {
                            dmg = 4;
                        }
                    }

                    if( Database.SystemData.WeatherData.Sky == Sysdata.SkyType.cloudy )
                        dmg /= 2;
                    if (Database.SystemData.WeatherData.Sky == Sysdata.SkyType.rain)
                    {
                        dmg *= 3;
                        dmg /= 4;
                    }

                    ch.SendText( "&+RThe heat of the sun feels terrible!&n\r\n" );
                    Combat.InflictSpellDamage( ch, ch, dmg, Spell.SpellList["poison"], AttackType.DamageType.light );
                }
                if( ch._inRoom.TerrainType == TerrainType.underwater_has_ground
                    && ( !ch.IsImmortal() && !ch.IsAffected( Affect.AFFECT_BREATHE_UNDERWATER )
                         && !CharData.CheckImmune( ch, Race.DamageType.drowning )
                         && !ch.HasInnate( Race.RACE_WATERBREATH ) ) )
                {
                    ch.SendText( "You can't breathe!\r\n" );
                    SocketConnection.Act( "$n&n sputters and chokes!", ch, null, null, SocketConnection.MessageTarget.room );
                    ch._hitpoints -= 5;
                    ch.UpdatePosition();
                }
                else if( ch._inRoom.TerrainType != TerrainType.underwater_has_ground
                         && ch._inRoom.TerrainType != TerrainType.unswimmable_water
                         && ch._inRoom.TerrainType != TerrainType.swimmable_water
                         && ch.HasInnate( Race.RACE_WATERBREATH )
                         && !Race.RaceList[ch.GetRace()].Name.Equals("Object", StringComparison.CurrentCultureIgnoreCase)
                         && ch.GetRace() != Race.RACE_GOD )
                {
                    ch.SendText( "You can't breathe!\r\n" );
                    SocketConnection.Act( "$n&n sputters and gurgles!", ch, null, null, SocketConnection.MessageTarget.room );
                    ch._hitpoints -= 5;
                    ch.UpdatePosition();
                }
                else if( ch.IsAffected( Affect.AFFECT_POISON ) )
                {
                    Combat.ApplyPoison(ch);
                }
                else
                {
                    string text;
                    if( ch._position == Position.incapacitated )
                    {
                        text = String.Format( "char_update: {0}&n is incapacitated.", ch._name );
                        ImmortalChat.SendImmortalChat( null, ImmortalChat.IMMTALK_SPAM, 0, text );
                        if( ch.HasInnate( Race.RACE_REGENERATE ) && ch._hitpoints < ch.GetMaxHit() )
                        {
                            ++ch._hitpoints;
                        }
                        else if( !ch.IsNPC() )
                        {
                            Combat.InflictDamage(ch, ch, 1, String.Empty, ObjTemplate.WearLocation.none, AttackType.DamageType.none);
                        }
                        else if( MUDMath.NumberPercent() < 50 )
                        {
                            ch._hitpoints--;
                        }
                    }
                    else if( ch._position == Position.mortally_wounded )
                    {
                        text = String.Format( "char_update: {0}&n is mortally wounded.", ch._name );
                        ImmortalChat.SendImmortalChat( null, ImmortalChat.IMMTALK_SPAM, 0, text );
                        if( ch.HasInnate( Race.RACE_REGENERATE ) && ch._hitpoints < ch.GetMaxHit() )
                        {
                            ++ch._hitpoints;
                        }
                        else if( !ch.IsNPC() )
                        {
                            Combat.InflictDamage(ch, ch, 2, String.Empty, ObjTemplate.WearLocation.none, AttackType.DamageType.none);
                        }
                        else
                        {
                            Combat.InflictDamage(ch, ch, 1, String.Empty, ObjTemplate.WearLocation.none, AttackType.DamageType.none);
                        }
                    }
                }
                ch.UpdatePosition();

                // Refresh stoneskin on perm stone mobs.
                if( ch.IsNPC() && Macros.IsSet( ch._mobTemplate.AffectedBy[ Affect.AFFECT_STONESKIN.Group ], Affect.AFFECT_STONESKIN.Vector ) &&
                        !ch.IsAffected( Affect.AFFECT_STONESKIN ) )
                {
                    ch.SetAffectBit( Affect.AFFECT_STONESKIN );
                    SocketConnection.Act( "$n&+L's skin turns to stone.&n", ch, null, null, SocketConnection.MessageTarget.room );
                }

                // Thats all for mobs.
                if( ch.IsNPC() )
                {
                    continue;
                }

                if( ch._position == Position.dead )
                {
                    Combat.KillingBlow( ch, ch );
                    continue;
                }

                // Find player with oldest save time.
                if( ( !ch._socket || ch._socket._connectionState == SocketConnection.ConnectionState.playing )
                        && ch._level >= 2
                        && ch._saveTime < saveTime )
                {
                    chSave = ch;
                    saveTime = ch._saveTime;
                }

                if( ( ch._level < Limits.LEVEL_AVATAR || ( !ch._socket && !ch.IsSwitched ) ) )
                {
                    Object obj = Object.GetEquipmentOnCharacter( ch, ObjTemplate.WearLocation.hand_one );

                    if( ( obj ) && obj.ItemType == ObjTemplate.ObjectType.light && obj.Values[ 2 ] > 0 )
                    {
                        if( --obj.Values[ 2 ] == 0 && ch._inRoom )
                        {
                            --ch._inRoom.Light;
                            SocketConnection.Act( "$p&n goes out.", ch, obj, null, SocketConnection.MessageTarget.room );
                            SocketConnection.Act( "$p&n goes out.", ch, obj, null, SocketConnection.MessageTarget.character );
                            obj.RemoveFromWorld();
                        }
                    }

                    if( ch._timer > 15 && !ch.IsSwitched )
                        chQuit = ch;

                    // This is so that hunger updates once every 16 CharacterUpdates,
                    // This is so that thirst updates once every 16 CharacterUpdates,
                    // This is so that drunkeness updates once every 2 CharacterUpdates, all at different intervals.
                    if( ( _hunger % 2 ) == 1 )
                    {
                        ch.AdjustDrunk( -1 );
                    }
                    if( _hunger == 4 )
                    {
                        ch.AdjustHunger(-1);
                    }
                    if( _hunger == 8 )
                    {
                        ch.AdjustThirst(-1);
                    }
                    if (ch.IsAffected(Affect.AFFECT_THIRST))
                    {
                        ch.AdjustThirst(-2);
                    }
                    if( ( ch._hitpoints - ch.GetMaxHit() ) > 50
                            && ( _hunger % 5 == 0 ) )
                    {
                        ch._hitpoints--;
                    }
                    else if( ( ch._hitpoints - ch.GetMaxHit() ) > 100
                             && ( _hunger % 4 == 0 ) )
                    {
                        ch._hitpoints--;
                    }
                    else if( ( ch._hitpoints - ch.GetMaxHit() ) > 150 )
                    {
                        ch._hitpoints--;
                    }
                }
            }

            // Autosave and autoquit. Check that these chars still exist.
            if( chSave || chQuit )
            {
                foreach( CharData it in Database.CharList )
                {
                    ch = it;
                    if( ch == chSave )
                    {
                        CharData.SavePlayer( ch );
                    }
                    if( ch == chQuit )
                    {
                        CommandType.Interpret(ch, "camp");
                    }
                }
            }

            return;
        }

        /// <summary>
        /// Update all rooms.
        /// 
        /// Check fall chance, fire plane flags, fall from losing fly, and
        /// tracking updates.
        /// 
        /// This function is performance sensitive.
        /// </summary>
        static void RoomUpdate()
        {
            foreach( Area area in Database.AreaList )
            {
                foreach( Room room in area.Rooms )
                {
                    Race.ResistanceType ris;
                    if( room.TerrainType == TerrainType.plane_of_fire || room.TerrainType == TerrainType.lava )
                    {
                        for (int k = room.People.Count - 1; k >= 0; k--)
                        {
                            CharData roomChar = room.People[k];
                            if ((!roomChar.IsNPC() && roomChar._level >= Limits.LEVEL_AVATAR)
                                    || roomChar.IsAffected(Affect.AFFECT_DENY_FIRE)
                                    || roomChar.GetRace() == Race.RACE_FIRE_ELE)
                            {
                                continue;
                            }
                            ris = roomChar.CheckRIS( AttackType.DamageType.fire );
                            if (ris == Race.ResistanceType.vulnerable)
                            {
                                roomChar._hitpoints -= 8;
                            }
                            else if (ris == Race.ResistanceType.susceptible)
                            {
                                roomChar._hitpoints -= 6;
                            }
                            else if (ris == Race.ResistanceType.normal)
                            {
                                roomChar._hitpoints -= 4;
                            }
                            else if (ris == Race.ResistanceType.resistant)
                            {
                                roomChar._hitpoints -= 2;
                            }
                            else if (ris == Race.ResistanceType.immune)
                            {
                                continue;
                            }
                            roomChar.SendText( "&+rYou are burned by the heat of the room.\r\n" );
                            roomChar.UpdatePosition();
                            if( roomChar._position == Position.dead )
                            {
                                Combat.KillingBlow( roomChar, roomChar );
                            }
                        }
                    }
                    if( room.TerrainType == TerrainType.plane_of_air && MUDMath.NumberPercent() < 20 )
                    {
                        for (int k = room.People.Count - 1; k >= 0; k-- )
                        {
                            CharData roomChar = room.People[k];
                            if ((!roomChar.IsNPC() && roomChar._level >= Limits.LEVEL_AVATAR)
                                    || roomChar.IsAffected(Affect.AFFECT_DENY_AIR) || roomChar.GetRace() == Race.RACE_AIR_ELE
                                    || Combat.CheckShrug(roomChar, roomChar))
                            {
                                continue;
                            }
                            ris = roomChar.CheckRIS(AttackType.DamageType.wind);
                            if (ris == Race.ResistanceType.vulnerable)
                            {
                                roomChar._hitpoints -= 8;
                            }
                            else if (ris == Race.ResistanceType.susceptible)
                            {
                                roomChar._hitpoints -= 6;
                            }
                            else if (ris == Race.ResistanceType.normal)
                            {
                                roomChar._hitpoints -= 4;
                            }
                            else if (ris == Race.ResistanceType.resistant)
                            {
                                roomChar._hitpoints -= 2;
                            }
                            else if (ris == Race.ResistanceType.immune)
                            {
                                continue;
                            }
                            roomChar.SendText("&+CYou're hit by a bolt of lightning!\r\n");
                            roomChar.UpdatePosition();
                            if (roomChar._position == Position.dead)
                            {
                                Combat.KillingBlow(roomChar, roomChar);
                            }
                        }
                    }

                    //  Check for falling char and objects from fly_level > 0
                    if( room.IsFlyable() )
                    {
                        for (int k = room.People.Count - 1; k >= 0; k-- )
                        {
                            CharData roomChar = room.People[k];
                            if (roomChar._flyLevel == 0)
                                continue;
                            if (roomChar._flyLevel < 0)
                            {
                                Log.Error("Mob has a fly_level less than 0", 0);
                                roomChar._flyLevel = 0;
                                continue;
                            }
                            if (roomChar.CanFly() || roomChar.IsAffected(Affect.AFFECT_LEVITATE)
                                    || (!roomChar.IsNPC() && roomChar._level >= Limits.LEVEL_AVATAR))
                                continue;
                            // ouch, gonna fall!
                            if (!roomChar.IsNPC())
                                roomChar.SendText("You fall tumbling down!\r\n");
                            SocketConnection.Act("$n&n falls away.", roomChar, null, roomChar, SocketConnection.MessageTarget.room);
                            roomChar._position = Position.sitting;
                            // Command.damage(ich, ich, MUDMath.Dice(ich.fly_level, 10), null, AttackType.DamageType.magic_other);
                            for (roomChar._flyLevel--; roomChar._flyLevel > 0; roomChar._flyLevel--)
                            {
                                SocketConnection.Act("$n&n falls past from above.", roomChar, null, null, SocketConnection.MessageTarget.room);
                            }
                            roomChar._flyLevel = 0;
                            SocketConnection.Act("$n&n falls from above.", roomChar, null, roomChar, SocketConnection.MessageTarget.room);
                        } //end of people falling from fly

                        for (int k = room.Contents.Count - 1; k >= 0; k-- )
                        {
                            Object obj = room.Contents[k];
                            if (obj.FlyLevel == 0)
                                continue;
                            if (!obj.HasWearFlag(ObjTemplate.WEARABLE_CARRY))
                                continue;
                            if (obj.HasFlag(ObjTemplate.ITEM_LEVITATES))
                                continue;
                            for (int l = room.People.Count - 1; l >= 0; l--)
                            {
                                CharData roomChar = room.People[l];
                                if (roomChar._flyLevel == obj.FlyLevel)
                                {
                                    SocketConnection.Act("$p&n falls away.", roomChar, obj, roomChar, SocketConnection.MessageTarget.character);
                                }
                                if (roomChar._flyLevel != 0 && roomChar._flyLevel < obj.FlyLevel)
                                {
                                    SocketConnection.Act("$p&n falls past you from above.", roomChar, obj, roomChar, SocketConnection.MessageTarget.character);
                                }
                                if (roomChar._flyLevel == 0)
                                {
                                    SocketConnection.Act("$p&n falls from above.", roomChar, obj, roomChar, SocketConnection.MessageTarget.character);
                                }
                            }
                            obj.FlyLevel = 0;
                        }
                    }

                    // Do track update here.
                    for (int k = room.People.Count - 1; k >= 0; k-- )
                    {
                        CharData roomChar = room.People[k];
                        if (roomChar._hunting && roomChar._wait <= 0)
                        {
                            if (roomChar.IsNPC())
                            {
                                roomChar.WaitState(2 * TICK_COMBAT);
                            }
                            Track.HuntVictim(roomChar);
                        }
                    }

                    // Now check for falling from room.
                    Room newRoom;
                    if( !room.ExitData[ 5 ] || !( newRoom = Room.GetRoom(room.ExitData[ 5 ].IndexNumber) ) )
                        continue;
                    if (room.ExitData[5].HasFlag(Exit.ExitFlag.blocked) || room.ExitData[5].HasFlag(Exit.ExitFlag.closed))
                        continue;

                    if( room.TerrainType != TerrainType.air && room.TerrainType != TerrainType.plane_of_air &&
                            room.TerrainType != TerrainType.underground_no_ground )
                    {
                        if( room.FallChance == 0 )
                            continue;
                    }

                    for (int l = room.Contents.Count - 1; l >= 0; l-- )
                    {
                        Object obj = room.Contents[l];
                        if (!obj.HasWearFlag(ObjTemplate.WEARABLE_CARRY))
                            continue;

                        if (obj.HasFlag(ObjTemplate.ITEM_LEVITATES))
                            continue;

                        if (obj.InRoom.People.Count > 0)
                        {
                            SocketConnection.Act("$p&n falls away.", obj.InRoom.People[0], obj, null, SocketConnection.MessageTarget.room);
                            SocketConnection.Act("$p&n falls away.", obj.InRoom.People[0], obj, null, SocketConnection.MessageTarget.character);
                        }

                        obj.RemoveFromRoom();
                        obj.AddToRoom(newRoom);

                        if (obj.InRoom.People.Count > 0)
                        {
                            SocketConnection.Act("$p&n falls by.", obj.InRoom.People[0], obj, null, SocketConnection.MessageTarget.room);
                            SocketConnection.Act("$p&n falls by.", obj.InRoom.People[0], obj, null, SocketConnection.MessageTarget.character);
                        }
                    }

                    for( int k = (room.People.Count - 1); k >= 0; k-- )
                    {
                        CheckFall( room, newRoom, room.People[k] );
                    }
                }
            }
            return;
        }

        /// <summary>
        /// Check for falling. Called from room update and movement.
        /// </summary>
        /// <param name="room"></param>
        /// <param name="target"></param>
        /// <param name="ch"></param>
        public static void CheckFall( Room room, Room target, CharData ch )
        {
            int chance;

            if( !room || !target || !ch )
                return;
            if( room.TerrainType != TerrainType.air &&
                    room.TerrainType != TerrainType.plane_of_air &&
                    room.TerrainType != TerrainType.underground_no_ground )
            {
                if( MUDMath.NumberPercent() > room.FallChance )
                    return;
            }

            if( ch.CanFly() || ch.IsAffected( Affect.AFFECT_LEVITATE ) )
                return;

            if( ch._inRoom.People != null )
            {
                SocketConnection.Act( "You are falling down!", ch, null, null, SocketConnection.MessageTarget.character );
                SocketConnection.Act( "$n&n falls away.", ch, null, null, SocketConnection.MessageTarget.room );
            }

            ch.RemoveFromRoom();
            ch.AddToRoom( target );

            if( !ch.HasSkill( "safe_fall" ) )
                chance = 0;
            else if( ch.IsNPC() )
                chance = ( ( ch._level * 3 ) / 2 ) + 15;
            else
                chance = ( (PC)ch ).SkillAptitude[ "safe fall" ];

            // People with high agility have a small chance to safe fall, and those with
            // the skill already get a bonus.
            chance += ( ch.GetCurrAgi() / 20 );

            // Minimum 1% chance of a bad fall.
            if( chance > 99 )
            {
                chance = 99;
            }

            // Safe fall added by Xangis
            if( target.FallChance == 0 || !target.ExitData[ 5 ]
                    || !target.ExitData[ 5 ].TargetRoom )
            {
                if( MUDMath.NumberPercent() < chance )
                {
                    // Decent chance of skill increase - people don't fall very often.
                    ch.PracticeSkill( "safe fall" );
                    ch.PracticeSkill( "safe fall" );
                    ch.PracticeSkill( "safe fall" );
                    ch.PracticeSkill( "safe fall" );
                    ch.SendText( "You fall to the ground!\r\n" );

                    if( MUDMath.NumberPercent() < chance )
                    {
                        SocketConnection.Act( "$n&n falls from above and lands gracefully.", ch, null, null, SocketConnection.MessageTarget.room );
                        ch.SendText( "You land gracefully, avoiding any injury.\r\n" );
                    }
                    else
                    {
                        SocketConnection.Act( "$n&n falls from above and lands on $s arse.", ch, null, null, SocketConnection.MessageTarget.room );
                        if( Race.MAX_SIZE > 0 && !ch.IsNPC() )
                        {
                            Combat.InflictDamage(ch, ch, MUDMath.NumberRange(2, 4), String.Empty, ObjTemplate.WearLocation.none, AttackType.DamageType.none);
                            ch._position = Position.sitting;
                            ch.WaitState( 3 );
                        }
                    }
                }
                else
                {
                    ch.SendText( "You slam into the ground!\r\n" );
                    ch._position = Position.sitting;
                    ch.WaitState( 8 );
                    SocketConnection.Act( "$n&n comes crashing in from above.", ch, null, null, SocketConnection.MessageTarget.room );
                    if( Race.MAX_SIZE > 0 && !ch.IsNPC() )
                    {
                        Combat.InflictDamage( ch, ch, ( ( MUDMath.NumberPercent() * (int)ch._size ) / Race.MAX_SIZE ),
                                String.Empty, ObjTemplate.WearLocation.none, AttackType.DamageType.none);
                    }
                }
            }
            else if( ch && ch._inRoom )
            {
                if( ch._inRoom.People.Count > 0 )
                {
                    SocketConnection.Act( "$n&n falls by.", ch, null, null, SocketConnection.MessageTarget.room );
                }
            }

            return;
        }

        /// <summary>
        /// Update all objects.
        /// This is only used for object special functions.
        /// This function is performance sensitive.
        /// </summary>
        static void ObjectSpecialUpdate()
        {
            foreach( Object it in Database.ObjectList )
            {
                // Examine call for special procedure.
                if( it.SpecFun.Count > 0 )
                {
                    if( it.CheckSpecialFunction(false)  )
                    {
                        continue;
                    }
                }
            }

            return;
        }

        /// <summary>
        /// Update all objs.
        /// This function is performance sensitive.
        /// This function just decrements object timers.
        /// </summary>
        static void ObjectUpdate()
        {
            Object obj;

            try
            {
                for (int i = (Database.ObjectList.Count - 1); i >= 0; --i)
                {
                    obj = Database.ObjectList[i];

                    if (obj.Timer < -1)
                        obj.Timer = -1;

                    if (obj.Timer < 0)
                    {
                        continue;
                    }

                    if (--obj.Timer == 0)
                    {
                        string message;
                        switch (obj.ItemType)
                        {
                            default:
                                message = "$p&n vanishes.";
                                break;
                            case ObjTemplate.ObjectType.drink_container:
                                message = "$p&n dries up.";
                                break;
                            case ObjTemplate.ObjectType.npc_corpse:
                                message = "$p&n decays into dust.";
                                break;
                            case ObjTemplate.ObjectType.pc_corpse:
                                message = "$p&n decays into dust.";
                                break;
                            case ObjTemplate.ObjectType.food:
                                message = "$p&n decomposes.";
                                break;
                            case ObjTemplate.ObjectType.portal:
                                message = "$p&n fades out of existence.";
                                break;
                            case ObjTemplate.ObjectType.wall:
                                message = StringConversion.WallDecayString(obj.ObjIndexData.IndexNumber);
                                if (obj.InRoom && obj.InRoom.ExitData[obj.Values[0]])
                                {
                                    obj.InRoom.ExitData[obj.Values[0]].RemoveFlag(Exit.ExitFlag.walled);
                                    obj.InRoom.ExitData[obj.Values[0]].RemoveFlag(Exit.ExitFlag.illusion);
                                }
                                break;
                        }
                        if (obj.CarriedBy)
                        {
                            SocketConnection.Act(message, obj.CarriedBy, obj, null, SocketConnection.MessageTarget.character);
                        }
                        else
                        {
                            if (obj.InRoom && (obj.InRoom.People.Count > 0))
                            {
                                SocketConnection.Act(message, obj.InRoom.People[0], obj, null, SocketConnection.MessageTarget.room);
                                SocketConnection.Act(message, obj.InRoom.People[0], obj, null, SocketConnection.MessageTarget.character);
                            }
                        }
                        // Don't nuke contents of containers upon decay.
                        if (obj.ItemType == ObjTemplate.ObjectType.pc_corpse || obj.ItemType == ObjTemplate.ObjectType.npc_corpse ||
                                obj.ItemType == ObjTemplate.ObjectType.container)
                        {
                            for (int j = (obj.Contains.Count - 1); j >= 0; j--)
                            {
                                Object cObj = obj.Contains[j];
                                cObj.RemoveFromObject();
                                cObj.AddToRoom(obj.InRoom);
                            }
                        }
                        obj.RemoveFromWorld();
                        continue;
                    }
                }
            }
            catch (IndexOutOfRangeException ex)
            {
                Log.Error("Index out of range exception in Event.ObjectUpdate: " + ex.ToString());
            }
            catch (Exception ex)
            {
                Log.Error("Exception in Event.ObjectUpdate: " + ex.ToString());
            }
            return;
        }

        /// <summary>
        /// Handle mob aggression.
        /// </summary>
        static void AggressionUpdate()
        {
            CharData ch = null;
            SocketConnection socket = null;

            // Let's not worry about link dead characters.
            for( int k = (Database.SocketList.Count - 1); k >= 0; k-- )
            {
                socket = Database.SocketList[k];
                ch = socket.Character;

                if (socket.Character == null || socket._connectionState != SocketConnection.ConnectionState.playing
                        || (ch._level >= Limits.LEVEL_HERO && ch.HasActionBit(PC.PLAYER_FOG)) || !ch._inRoom)
                {
                    continue;
                }

                /* mch wont get hurt */
                CharData roomCharacter;
                for( int i = (ch._inRoom.People.Count - 1); i >= 0; i--)
                {
                    roomCharacter = ch._inRoom.People[i];

                    if( !roomCharacter.IsNPC()
                            || roomCharacter._fighting
                            || roomCharacter.IsAffected( Affect.AFFECT_CHARM )
                            || !roomCharacter.IsAwake()
                            || (roomCharacter.HasActionBit(MobTemplate.ACT_WIMPY) && ch.IsAwake())
                            || ch._flyLevel != roomCharacter._flyLevel
                            || ch.IsImmortal()
                            || !CharData.CanSee( roomCharacter, ch ) )
                    {
                        continue; //next mch
                    }
                    bool hate = roomCharacter.IsAggressive(ch);
                    if( !hate )
                        continue;

                    if( roomCharacter.IsHating( ch ) )
                    {
                        Track.FoundPrey( roomCharacter, ch );
                        continue;
                    }

                    /*
                     * We have a 'ch' player character and a 'roomCharacter' npc aggressor.
                     * Now make the aggressor fight a RANDOM pc victim in the room,
                     * giving each 'victim' an equal chance of selection.
                     *   
                     * TODO: Use hate/fear level in EnemyData to weight the selection criteria.
                     */
                    int count = 0;
                    CharData victim = null;
                    for( int j = (roomCharacter._inRoom.People.Count - 1); j >= 0; j-- )
                    {
                        CharData possibleVictim = roomCharacter._inRoom.People[j];
                        if( possibleVictim.IsNPC()
                                || possibleVictim._level >= Limits.LEVEL_AVATAR
                                || possibleVictim._flyLevel != roomCharacter._flyLevel
                                || !CharData.CanSee( roomCharacter, possibleVictim )
                                || !roomCharacter.IsAggressive(possibleVictim))
                            continue;
                        if ((!roomCharacter.HasActionBit(MobTemplate.ACT_WIMPY) || !possibleVictim.IsAwake())
                                && CharData.CanSee( roomCharacter, possibleVictim ) )
                        {
                            if( MUDMath.NumberRange( 0, count ) == 0 )
                            {
                                victim = possibleVictim;
                            }
                            ++count;
                        }
                    }

                    if( !victim )
                    {
                        continue;
                    }
                    if( !Combat.CheckAggressive( victim, roomCharacter ) )
                    {
                        if (roomCharacter._position == Position.standing)
                        {
                            roomCharacter.AttackCharacter(victim);
                        }
                        else if (roomCharacter._position >= Position.reclining)
                        {
                            SocketConnection.Act("$n scrambles to an upright position.", roomCharacter, null, null, SocketConnection.MessageTarget.room);
                            roomCharacter._position = Position.standing;
                        }
                    }

                }

            }

            return;
        }


        /// <summary>
        /// This function also handles various spell effects such as
        /// haste, blur, fear, and slowness
        ///
        /// This function used about 17-20% of total CPU time on Basternae
        /// If we were really concerned about CPU usage, we would revamp it,
        /// assuming the CPU usage amount still applies.
        /// </summary>
        public static void ViolenceUpdate()
        {
            CharData ch;
            CharData victim;

            // Hasteround and slowround are used to make hasted and slowed
            // people hit faster or slower (between rounds or every other
            // round)
            if (!_isHasteRound)
            {
                _isHasteRound = true;
            }
            else if (_isHasteRound)
            {
                _isHasteRound = false;

                if (!_isSlowRound)
                {
                    _isSlowRound = true;
                }
                else
                {
                    _isSlowRound = false;
                }
            }

            // This list is a bit dangerous because people can and do die and get removed from the
            // list while it is being iterated.  The second iterator is an attempt to try
            // to get around this dangerous problem.
            for( int i = (Database.CharList.Count - 1); i >= 0; i-- )
            {
                ch = Database.CharList[i];
                if (!ch._inRoom)
                {
                    continue;
                }

                if (_isHasteRound && !ch.IsAffected(Affect.AFFECT_HASTE))
                {
                    continue;
                }

                victim = ch._fighting;

                if (victim)
                {
                    if (ch.IsAwake() && ch._inRoom == victim._inRoom)
                    {
                        if (!Combat.CheckVicious(ch, victim))
                        {
                            Combat.StopFighting(ch, false);
                            continue;
                        }

                        /* Ok here we test for switch if victim is charmed */
                        if (victim.IsAffected(Affect.AFFECT_CHARM)
                                && !victim.IsAffected( Affect.AFFECT_BLIND)
                                && victim._master
                                && victim._inRoom == victim._master._inRoom
                                && ch != victim
                                && MUDMath.NumberPercent() > 40)
                        {
                            Combat.StopFighting(ch, false);
                            if (ch.IsAffected(Affect.AFFECT_SLOWNESS) && !_isSlowRound)
                            {
                                continue;
                            }
                            if (!Combat.BlurAttack(ch, victim))
                            {
                                if (Combat.CombatRound(ch, victim, String.Empty))
                                {
                                    continue;
                                }
                            }
                            else
                            {
                                continue;
                            }
                        }
                        else
                        {
                            if (ch.IsAffected(Affect.AFFECT_SLOWNESS) && !_isSlowRound)
                            {
                                continue;
                            }
                            if (!Combat.BlurAttack(ch, victim))
                            {
                                if (Combat.CombatRound(ch, victim, String.Empty))
                                {
                                    continue;
                                }
                            }
                            else
                            {
                                continue;
                            }
                        }
                        if (victim.IsAffected(Affect.AFFECT_FEAR) && MUDMath.NumberPercent() < 25)
                        {
                            victim.SendText("You are afraid to continue the battle any longer!\r\n");
                            CommandType.Interpret(victim, "flee");
                        }
                    }
                    else
                    {
                        Combat.StopFighting(ch, false);
                    }
                    continue;
                }

                if (ch.IsAffected(Affect.AFFECT_BLIND) || (ch.IsNPC() && ch._mobTemplate.ShopData))
                {
                    continue;
                }

                // Ok. So ch is not fighting anyone. Is there a fight going on?

                // Check for assist.
                CharData roomChar = null;
                CharData irch;
                for( int j = (ch._inRoom.People.Count - 1); j >= 0; j-- )
                {
                    irch = ch._inRoom.People[j];
                    bool assist = false;
                    if (!irch || !irch.IsAwake() || !(victim = irch._fighting))
                    {
                        continue;
                    }

                    // IF a character is fighting a  mobile, then we can check for assist.
                    // protector mobs will only assist their own race, a group member,
                    // or a follower/leader
                    if (ch.IsNPC() && ch.HasActionBit(MobTemplate.ACT_PROTECTOR)
                            && !ch._fighting && ch._position > Position.sleeping)
                    {
                        if (irch.GetRace() == ch.GetRace())
                            assist = true;
                        if (ch.IsSameGroup(irch))
                            assist = true;
                        if ((ch == irch._master && irch.IsNPC()) || irch == ch._master)
                            assist = true;
                        if (ch.IsSameGroup(irch._fighting))
                            assist = false;
                        if (ch._master && ch._master.IsSameGroup(irch._fighting))
                            assist = false;
                        if (irch.GetRace() == ch.GetRace() && victim.GetRace() == ch.GetRace()
                                && !irch.IsNPC())
                            assist = false;
                    }

                    if (assist && ch._wait <= 0)
                    {
                        // Lower level mobs will take longer to assist, 26% for a level one
                        // and 100% for a 50.  Newbie guards could concievably ignore a whole
                        // fight. I guess that's why newbie guards are rare.
                        if (CharData.CanSee(ch, victim) && MUDMath.NumberPercent() < ((ch._level * 3 / 2) + 25))
                        {
                            SocketConnection.Act("$n&n assists $N&n...", ch, null, irch, SocketConnection.MessageTarget.room);
                            roomChar = irch;
                            if (Combat.SingleAttack(ch, victim, String.Empty, ObjTemplate.WearLocation.hand_one))
                            {
                                continue;
                            }
                        }
                    }
                }
                // End of assist code

                if (victim == null || roomChar == null)
                {
                    continue;
                }

                if (ch.IsAffected(Affect.AFFECT_SLOWNESS) && !_isSlowRound)
                {
                    continue;
                }

                if (!Combat.BlurAttack(ch, victim))
                {
                    if (Combat.CombatRound(ch, victim, String.Empty))
                    {
                        continue;
                    }
                }
                else
                {
                    continue;
                }

                if (victim.IsAffected(Affect.AFFECT_FEAR) && MUDMath.NumberPercent() < 25)
                {
                    victim.SendText("You are afraid to continue the battle any longer!\r\n");
                    CommandType.Interpret(victim, "flee");
                }
            }
            return;
        }

        /// <summary>
        /// Heartbeat file update. The heartbeat file is a file that is updated periodically
        /// and its timestamp can be checked with an external script to see whether the MUD
        /// has hung.
        /// </summary>
        static void HeartbeatUpdate()
        {
            FileStream fp = File.OpenWrite(FileLocation.HeartbeatFile);
            fp.WriteByte(64);
            fp.Close();
        }

        // Update the check on time for autoshutdown.
        static void TimeUpdate()
        {
            string text;

            if( Database.SystemData.ShutdownIsScheduled == false )
                return;
            if( ( Database.SystemData.CurrentTime + TimeSpan.FromMinutes( 15 ) ) > Database.SystemData.ShutdownTime )
            {
                text = String.Format( "Warning!\r\n{0} in 15 minutes.\r\n", Database.Reboot ? "Reboot" : "Shutdown" );
                SocketConnection.SendToAllChar( text );
            }
            else if( ( Database.SystemData.CurrentTime + TimeSpan.FromMinutes( 5 ) ) > Database.SystemData.ShutdownTime )
            {
                text = String.Format( "Warning!\r\n{0} in 5 minutes.\r\n", Database.Reboot ? "Reboot" : "Shutdown" );
                SocketConnection.SendToAllChar( text );
            }
            else if( ( Database.SystemData.CurrentTime + TimeSpan.FromSeconds( 60 ) ) > Database.SystemData.ShutdownTime )
            {
                text = String.Format( "Warning!\r\n{0} in 1 minute.\r\n", Database.Reboot ? "Reboot" : "Shutdown" );
                SocketConnection.SendToAllChar( text );
            }
            else if( ( Database.SystemData.CurrentTime + TimeSpan.FromSeconds( 10 ) ) > Database.SystemData.ShutdownTime )
            {
                text = String.Format( "Final Warning!\r\n{0} in 10 seconds.\r\n", Database.Reboot ? "Reboot" : "Shutdown" );
                SocketConnection.SendToAllChar( text );
            }
            else if( Database.SystemData.CurrentTime > Database.SystemData.ShutdownTime )
            {
                text = String.Format( "Automatic {0} by system (Database.SystemData._currentTime > Database.SystemData._shutdownTime).\r\n", Database.Reboot ? "Reboot" : "Shutdown" );
                SocketConnection.SendToAllChar( text );
                Log.Trace( text );

                SocketConnection.EndOfGame();

                if( !Database.Reboot )
                {
                    FileStream fp = File.OpenRead(FileLocation.ShutdownFile);
                    StreamWriter sw = new StreamWriter( fp );
                    sw.WriteLine( "Shutdown by System" );
                    sw.Flush();
                    sw.Close();
                }
                Database.SystemData.GameIsDown = true;
            }
            return;
        }

        /// <summary>
        /// Updates the ban file.
        /// </summary>
        public static void UpdateBans()
        {
            string strsave = FileLocation.SystemDirectory + FileLocation.BanFile;

            FileStream fp = File.OpenWrite( strsave );
            StreamWriter sw = new StreamWriter( fp );
            foreach( BanData pban in Database.BanList )
            {
                sw.WriteLine( pban.Name );
            }
            sw.Flush();
            sw.Close();

            return;
        }
    }
}