using System;
using ModernMUD;

namespace MUDEngine
{
    /// <summary>
    /// Defines special functions are available for mobiles.
    /// 
    /// TODO: Make it possible to define these via a configuration file. Hard-coding
    /// game-specific behavior is icky.
    /// </summary>
    [Serializable]
    public class MobFun
    {
        // Special function defines
        public const int PROC_NORMAL = 0;
        public const int PROC_DEATH = 1;

        const int XORN_MOVE_PHASE = 0; // SpecXorn
        const int XORN_MOVE_EARTH = 1; // SpecXorn
        static CharData _wasFighting; // grumbar shout


        /*
        * If a spell casting mob is hating someone, try to summon them.
        *
        * Xangis - Need to add code to also gate to the person if they can't be summoned
        */
        static void SummonIfHating( CharData ch )
        {
            string name = String.Empty;
            string buf = String.Empty;
 
            if( ch.Fighting || ch.Fearing || ch.Hating.Count == 0 || ch.InRoom.HasFlag( RoomTemplate.ROOM_SAFE ) )
                return;

            /* If summoner is busy hunting someone aleady, don't summon. */
            if( ch.Hunting )
                return;

            CharData victim = ch.GetRandomHateTarget( false );

            // Pretty stupid to summon someone who's in the same room.
            if( !victim || ch.InRoom == victim.InRoom )
                return;

            if( ( ch.HasSpell( "relocate" ) ))
            {
                if( !victim.IsNPC() )
                    buf += "relocate 0." + name;
                else
                    buf += "relocate " + name;
            }
            else if( ch.HasSpell( "summon" ) )
            {
                if( !victim.IsNPC() )
                    buf += "summon 0." + name;
                else
                    buf += "summon " + name;
            }
            else if ((ch.Level * 4 - 3) >= Spell.SpellList["spirit jump"].SpellCircle[(int)ch.CharacterClass.ClassNumber])
            {
                if( !victim.IsNPC() )
                    buf += "'spirit jump' 0." + name;
                else
                    buf += "'spirit jump' " + name;
            }

            CommandType.Interpret(ch, "cast " + buf);
            return;
        }

        /// <summary>
        /// Core procedure for dragons.
        /// </summary>
        /// <param name="mob"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool Dragon( System.Object mob, string type )
        {
            if (mob == null) return false;
            CharData ch = (CharData)mob;
            if (!ch.Fighting)
            {
                return false;
            }

            if( ch.CurrentPosition < Position.fighting )
                return false;

            // Only performing an action half the time.
            if( MUDMath.NumberPercent() < 50 )
                return false;

            // use dragon roar 1/2 half the time
            if( MUDMath.NumberPercent() > 50 )
            {
                ch.SendText( "You roar like the enraged beast you are.\r\n" );
                SocketConnection.Act( "$n&n &+WROARS&n, sending you into a panic!",
                     ch, null, null, SocketConnection.MessageTarget.room );
                foreach( CharData victim in ch.InRoom.People )
                {
                    if( victim == ch )
                        continue;
                    if( victim.FlightLevel != ch.FlightLevel )
                        continue;
                    if( victim.IsNPC() )
                        continue;
                    if( Magic.SavesBreath( ch.Level, victim, AttackType.DamageType.sound ) )
                    {
                        if( Magic.SavesBreath( ch.Level, victim, AttackType.DamageType.sound ) )
                        {
                            victim.SendText( "You control the urge to flee and stand your ground.\r\n" );
                        }
                        else
                        {
                            victim.SendText( "You are bowled over by the powerful blast!\r\n" );
                            SocketConnection.Act( "$N&n is floored by $n&n's roar.", ch, null, victim, SocketConnection.MessageTarget.everyone_but_victim );
                            victim.CurrentPosition = Position.sitting;
                        }
                    }
                    else
                    {
                        victim.SendText( "Run away! Run away!\r\n" );
                        SocketConnection.Act( "$n&n attempts to flee from the room!", victim, null, null, SocketConnection.MessageTarget.room );
                        Command.Flee( victim, null );
                    }
                } //end for
                return true;
            }
            ch.SendText( "You feel the urge to &+Gburp&n!\r\n" );
            SocketConnection.Act( "$n&n breathes!", ch, null, null, SocketConnection.MessageTarget.room );

            Spell spell = Spell.SpellList[type];

            if( spell != null )
            {
                string lbuf = String.Format( "Dragon ({0}) breathing {1}.", ch.ShortDescription, spell.Name );
                ImmortalChat.SendImmortalChat( null, ImmortalChat.IMMTALK_SPAM, 0, lbuf );
            }

            if( spell != null )
                foreach( CharData victim in ch.InRoom.People )
                {
                    if( victim == ch )
                        continue;

                    /* NPC's do not hit NPC's! (charmies?) */
                    if( victim.IsNPC() && ch.IsNPC() )
                        continue;

                    int level = Macros.Range(1, ch.Level, Limits.LEVEL_HERO);
                    spell.Invoke(ch, level, victim);
                }

            return true;
        }

        /// <summary>
        /// Random breath ability check.
        /// </summary>
        /// <param name="mob"></param>
        /// <param name="cmd"></param>
        /// <returns></returns>
        public static bool SpecBreathAny( System.Object mob, int cmd )
        {
            if (mob == null) return false;
            
            if( cmd == PROC_DEATH )
                return false;
            CharData ch = (CharData)mob;
            if( !ch.Fighting )
                return false;

            switch( MUDMath.NumberRange( 0, 7 ) )
            {
                case 0:
                    return SpecBreathFrost( ch, PROC_NORMAL );
                case 1:
                    return SpecBreathWater( ch, PROC_NORMAL );
                case 2:
                    return SpecBreathLightning( ch, PROC_NORMAL );
                case 3:
                    return SpecBreathGas( ch, PROC_NORMAL );
                case 4:
                    return SpecBreathAcid( ch, PROC_NORMAL );
                case 5:
                case 6:
                case 7:
                    return SpecBreathFire( ch, PROC_NORMAL );
            }

            return false;
        }

        static bool SpecBreathAcid( System.Object mob, int cmd )
        {
            if (mob == null) return false;
            
            if( cmd == PROC_DEATH )
                return false;

            return Dragon( mob, "acid breath" );
        }

        static bool SpecBreathFire( System.Object mob, int cmd )
        {
            if (mob == null) return false;
            if (cmd == PROC_DEATH)
                return false;

            return Dragon( mob, "fire breath" );
        }

        static bool SpecBreathFrost( System.Object mob, int cmd )
        {
            if (mob == null) return false;
            if (cmd == PROC_DEATH)
                return false;

            return Dragon( mob, "frost breath" );
        }

        static bool SpecBreathWater( System.Object mob, int cmd )
        {
            if (mob == null) return false;
            
            if( cmd == PROC_DEATH )
                return false;

            return Dragon( mob, "water breath" );
        }

        static bool SpecBreathGas( System.Object mob, int cmd )
        {
            if (mob == null) return false;
            
            if( cmd == PROC_DEATH )
                return false;

            return Dragon( mob, "gas breath" );
        }

        static bool SpecBreathShadow( System.Object mob, int cmd )
        {
            if (mob == null) return false;
            
            if( cmd == PROC_DEATH )
                return false;
            if( MUDMath.NumberPercent() > 50 )
                return Dragon( mob, "shadow breath i" );
            return Dragon( mob, "shadow breath ii" );
        }

        static bool SpecBreathLightning( System.Object mob, int cmd )
        {
            if (mob == null) return false;
            
            if( cmd == PROC_DEATH )
                return false;

            return Dragon( mob, "lightning breath" );
        }

        static bool SpecCastAdept( System.Object mob, int cmd )
        {
            if (mob == null) return false;
            CharData ch = (CharData)mob;
            if( cmd == PROC_DEATH )
                return false;

            if( ch.InRoom.HasFlag( RoomTemplate.ROOM_NO_MAGIC ) )
                return false;

            if( !ch.IsAwake() || ch.Fighting )
                return false;

            if( !ch.CanSpeak() )
                return false;

            CharData victim = null;
            foreach( CharData ivictim in ch.InRoom.People )
            {
                if (ivictim != ch && CharData.CanSee(ch, ivictim) && MUDMath.NumberBits(1) == 0)
                {
                    victim = ivictim;
                    break;
                }
            }

            if( !victim || victim.IsNPC() )
                return false;

            if( victim.Level > 10 )
            {
                return false;
            }

            switch( MUDMath.NumberBits( 3 ) )
            {
                case 0:
                    {
                        SocketConnection.Act("$n&n utters the word 'tehctah'.", ch, null, null, SocketConnection.MessageTarget.room);
                        Spell spl = Spell.SpellList["armor"];
                        if (spl != null)
                        {
                            spl.Invoke(ch, ch.Level, victim);
                        }
                        return true;
                    }
                case 1:
                    {
                        SocketConnection.Act("$n&n utters the word 'nhak'.", ch, null, null, SocketConnection.MessageTarget.room);
                        Spell spl = Spell.SpellList["bless"];
                        if (spl != null)
                        {
                            spl.Invoke(ch, ch.Level, victim);
                        }
                        return true;
                    }
                case 2:
                    {
                        SocketConnection.Act("$n&n utters the word 'yeruf'.", ch, null, null, SocketConnection.MessageTarget.room);
                        Spell spl = Spell.SpellList["cure blindness"];
                        if (spl != null)
                        {
                            spl.Invoke(ch, ch.Level, victim);
                        }
                        return true;
                    }
                case 3:
                    {
                        SocketConnection.Act("$n&n utters the word 'garf'.", ch, null, null, SocketConnection.MessageTarget.room);
                        Spell spl = Spell.SpellList["cure light"];
                        if (spl != null)
                        {
                            spl.Invoke(ch, ch.Level, victim);
                        }
                        return true;
                    }
                case 4:
                    {
                        SocketConnection.Act("$n&n utters the words 'rozar'.", ch, null, null, SocketConnection.MessageTarget.room);
                        Spell spl = Spell.SpellList["remove poison"];
                        if (spl != null)
                        {
                            spl.Invoke(ch, ch.Level, victim);
                        }
                        return true;
                    }
                case 5:
                    {
                        SocketConnection.Act("$n&n utters the words 'nadroj'.", ch, null, null, SocketConnection.MessageTarget.room);
                        Spell spl = Spell.SpellList["vigorize light"];
                        if (spl != null)
                        {
                            spl.Invoke(ch, ch.Level, victim);
                        }
                        return true;
                    }
            }

            return false;
        }

        static bool SpecCastCleric( System.Object mob, int cmd )
        {
            if (mob == null) return false;
            if (cmd == PROC_DEATH)
                return false;

            CharData ch = (CharData)mob;
            if( ch.InRoom.HasFlag( RoomTemplate.ROOM_NO_MAGIC ) )
                return false;

            SummonIfHating( ch );

            if( !ch.CanSpeak() )
                return false;

            if( ch.CurrentPosition != Position.fighting )
            {
                if( ch.CurrentPosition == Position.standing )
                {
                    if( Spellup( ch ) )
                        return true;
                    if( ch.Hitpoints < ( ch.GetMaxHit() - 10 ) )
                        if( HealSelf( ch ) )
                            return true;
                    if( CheckVigorize( ch ) )
                        return true;
                    if( SpellupOthers( ch ) )
                        return true;
                }
                return false;
            }

            CharData victim = ch.Fighting;

            if( !victim )
                return false;

            // Not sure whether being able to see the victim should be required...

            // Give a 1/3 chance of picking an area spell over a direct damage spell
            // later on, I plan to code it so that it makes an intelligence check
            // based on the number of enemies in the room. -- Xangis
            if( MUDMath.NumberRange( 1, 3 ) == 3 )
            {
                if( AreaOffensive( ch, victim ) )
                    return true;
                if( OffensiveSpell( ch, victim ) )
                    return true;
            }
            else
            {
                if( OffensiveSpell( ch, victim ) )
                    return true;
                if( AreaOffensive( ch, victim ) )
                    return true;
            }

            return false;
        }

        static bool SpecCastDruid( System.Object mob, int cmd )
        {
            if (mob == null) return false;
            if (cmd == PROC_DEATH)
                return false;
            CharData ch = (CharData)mob;
            if( ch.InRoom.HasFlag( RoomTemplate.ROOM_NO_MAGIC ) )
                return false;

            SummonIfHating( ch );

            if( !ch.CanSpeak() )
                return false;

            if( ch.CurrentPosition != Position.fighting )
            {
                if( ch.CurrentPosition == Position.standing )
                {
                    if( Spellup( ch ) )
                        return true;
                    if( ch.Hitpoints < ( ch.GetMaxHit() - 10 ) )
                        if( HealSelf( ch ) )
                            return true;
                    if( CheckVigorize( ch ) )
                        return true;
                    if( SpellupOthers( ch ) )
                        return true;
                }
                return false;
            }

            CharData victim = ch.Fighting;

            if( !victim )
                return false;

            // Give a 1/3 chance of picking an area spell over a direct damage spell
            // later on, I plan to code it so that it makes an intelligence check
            // based on the number of enemies in the room. -- Xangis
            if( MUDMath.NumberRange( 1, 3 ) == 3 )
            {
                if( AreaOffensive( ch, victim ) )
                    return true;
                if( OffensiveSpell( ch, victim ) )
                    return true;
            }
            else
            {
                if( OffensiveSpell( ch, victim ) )
                    return true;
                if( AreaOffensive( ch, victim ) )
                    return true;
            }

            return false;
        }

        static bool SpecCastShaman( System.Object mob, int cmd )
        {
            if (mob == null) return false;
            if (cmd == PROC_DEATH)
                return false;
            CharData ch = (CharData)mob;
            if( ch.InRoom.HasFlag( RoomTemplate.ROOM_NO_MAGIC ) )
                return false;

            if( !ch.CanSpeak() )
                return false;

            if( ch.CurrentPosition != Position.fighting )
            {
                if( ch.CurrentPosition == Position.standing )
                {
                    if( Spellup( ch ) )
                        return true;
                    if( ch.Hitpoints < ( ch.GetMaxHit() - 10 ) )
                        if( HealSelf( ch ) )
                            return true;
                    if( CheckVigorize( ch ) )
                        return true;
                    if( SpellupOthers( ch ) )
                        return true;
                }
                return false;
            }

            CharData victim = ch.Fighting;

            if( !victim )
                return false;

            // Give a 1/3 chance of picking an area spell over a direct damage spell
            // later on, I plan to code it so that it makes an intelligence check
            // based on the number of enemies in the room. -- Xangis
            if( MUDMath.NumberRange( 1, 3 ) == 3 )
            {
                if( AreaOffensive( ch, victim ) )
                    return true;
                if( OffensiveSpell( ch, victim ) )
                    return true;
            }
            else
            {
                if( OffensiveSpell( ch, victim ) )
                    return true;
                if( AreaOffensive( ch, victim ) )
                    return true;
            }

            return false;
        }

        static bool SpecCastJudge( System.Object mob, int cmd )
        {
            if (mob == null) return false;
            if (cmd == PROC_DEATH)
                return false;
            CharData ch = (CharData)mob;
            if( ch.InRoom.HasFlag( RoomTemplate.ROOM_NO_MAGIC ) )
                return false;

            if( ch.CurrentPosition != Position.fighting )
                return false;

            if( !ch.CanSpeak() )
                return false;

            return false;
        }

        static bool SpecCastConjurer( System.Object mob, int cmd )
        {
            if (mob == null) return false;
            string spellname;
            Spell spell;

            if( cmd == PROC_DEATH )
                return false;
            CharData ch = (CharData)mob;
            if( ch.InRoom.HasFlag( RoomTemplate.ROOM_NO_MAGIC ) )
                return false;

            SummonIfHating( ch );

            if( !ch.CanSpeak() )
                return false;

            if( ch.CurrentPosition != Position.fighting )
            {
                if( ch.CurrentPosition == Position.standing )
                    if( Spellup( ch ) )
                        return true;
                if( SpellupOthers( ch ) )
                    return true;
                return false;
            }

            CharData victim = null;
            foreach( CharData ivictim in ch.InRoom.People )
            {
                if (ivictim.Fighting == ch && CharData.CanSee(ch, ivictim)
                        && MUDMath.NumberBits(5) == 0)
                {
                    victim = ivictim;
                    break;
                }
            }

            if( !victim )
                return false;

            for( ; ; )
            {
                int minLevel;

                switch( MUDMath.NumberBits( 4 ) )
                {
                    case 0:
                        minLevel = 0;
                        spellname = "blindness";
                        break;
                    case 1:
                        minLevel = 3;
                        spellname = "chill touch";
                        break;
                    case 2:
                        minLevel = 7;
                        spellname = "weaken";
                        break;
                    case 3:
                        minLevel = 8;
                        spellname = "teleport";
                        break;
                    case 4:
                        minLevel = 11;
                        spellname = "color spray";
                        break;
                    case 5:
                        minLevel = 12;
                        spellname = "sleep";
                        break;
                    case 6:
                        minLevel = 13;
                        spellname = "energy drain";
                        break;
                    case 7:
                        minLevel = 6;
                        spellname = "burning hands";
                        break;
                    case 8:
                    case 9:
                        minLevel = 15;
                        spellname = "fireball";
                        break;
                    case 12:
                        minLevel = 16;
                        spellname = "shocking grasp";
                        break;
                    case 13:
                        minLevel = 26;
                        spellname = "polymorph other";
                        break;
                    default:
                        minLevel = 20;
                        spellname = "acid blast";
                        break;
                }

                if( ch.Level >= minLevel )
                    break;
            }

            if( ( spell = StringLookup.SpellLookup( spellname ) ) == null )
                return false;

            int level = Macros.Range(1, ch.Level, Limits.LEVEL_HERO);
            spell.Invoke(ch, level, victim);
            return true;
        }

        static bool SpecCastNecromancer( System.Object mob, int cmd )
        {
            if (mob == null) return false;
            string spellname;
            Spell spell;

            if( cmd == PROC_DEATH )
            {
                return false;
            }
            CharData ch = (CharData)mob;
            if( ch.InRoom.HasFlag( RoomTemplate.ROOM_NO_MAGIC ) )
                return false;

            if( !ch.CanSpeak() )
                return false;

            if( ch.CurrentPosition != Position.fighting )
            {
                if( ch.CurrentPosition == Position.standing )
                    if( Spellup( ch ) )
                        return true;
                if( SpellupOthers( ch ) )
                    return true;
                return false;
            }

            CharData victim = null;
            foreach( CharData ivictim in ch.InRoom.People )
            {
                if (ivictim.Fighting == ch && CharData.CanSee(ch, ivictim)
                        && MUDMath.NumberBits(5) == 0)
                {
                    victim = ivictim;
                    break;
                }
            }

            if( !victim )
                return false;

            for( ; ; )
            {
                int minLevel;

                switch( MUDMath.NumberBits( 4 ) )
                {
                    case 0:
                        minLevel = 0;
                        spellname = "blindness";
                        break;
                    case 1:
                        minLevel = 3;
                        spellname = "chill touch";
                        break;
                    case 2:
                        minLevel = 7;
                        spellname = "weaken";
                        break;
                    case 3:
                        minLevel = 8;
                        spellname = "teleport";
                        break;
                    case 4:
                        minLevel = 11;
                        spellname = "color spray";
                        break;
                    case 5:
                        minLevel = 12;
                        spellname = "change sex";
                        break;
                    case 6:
                        minLevel = 13;
                        spellname = "energy drain";
                        break;
                    case 7:
                    case 8:
                    case 9:
                        minLevel = 15;
                        spellname = "fireball";
                        break;
                    case 12:
                        minLevel = 16;
                        spellname = "polymorph other";
                        break;
                    case 13:
                        minLevel = 16;
                        spellname = "polymorph other";
                        break;
                    default:
                        minLevel = 20;
                        spellname = "acid blast";
                        break;
                }

                if( ch.Level >= minLevel )
                    break;
            }

            if( ( spell = StringLookup.SpellLookup( spellname ) ) == null )
                return false;

            int level = Macros.Range(1, ch.Level, Limits.LEVEL_HERO);
            spell.Invoke(ch, level, victim);
            return true;
        }

        static bool SpecCastSorcerer( System.Object mob, int cmd )
        {
            if (mob == null) return false;
            if (cmd == PROC_DEATH)
                return false;
            CharData ch = (CharData)mob;
            if( ch.InRoom.HasFlag( RoomTemplate.ROOM_NO_MAGIC ) )
                return false;

            SummonIfHating( ch );

            if( !ch.CanSpeak() )
                return false;

            if( ch.CurrentPosition != Position.fighting )
            {
                if( ch.CurrentPosition == Position.standing )
                {
                    if( Spellup( ch ) )
                        return true;
                    if( SpellupOthers( ch ) )
                        return true;
                }
                return false;
            }

            CharData victim = ch.Fighting;

            if( !victim )
                return false;

            // Give a 1/3 chance of picking an area spell over a direct damage spell
            // later on, I plan to code it so that it makes an intelligence check
            // based on the number of enemies in the room. -- Xangis
            if( MUDMath.NumberRange( 1, 3 ) == 3 )
            {
                if( AreaOffensive( ch, victim ) )
                    return true;
                if( OffensiveSpell( ch, victim ) )
                    return true;
            }
            else
            {
                if( OffensiveSpell( ch, victim ) )
                    return true;
                if( AreaOffensive( ch, victim ) )
                    return true;
            }

            return false;
        }

        static bool SpecCastAirEle( System.Object mob, int cmd )
        {
            if (mob == null) return false;
            if (cmd == PROC_DEATH)
                return false;
            CharData ch = (CharData)mob;
            if( ch.InRoom.HasFlag( RoomTemplate.ROOM_NO_MAGIC ) )
                return false;

            SummonIfHating( ch );

            if( !ch.CanSpeak() )
                return false;

            if( ch.CurrentPosition != Position.fighting )
            {
                if( ch.CurrentPosition == Position.standing )
                {
                    if( Spellup( ch ) )
                        return true;
                    if( SpellupOthers( ch ) )
                        return true;
                }
                return false;
            }

            CharData victim = ch.Fighting;

            if( !victim )
                return false;

            // Not sure whether being able to see the victim should be required...

            // Give a 1/3 chance of picking an area spell over a direct damage spell
            // later on, I plan to code it so that it makes an intelligence check
            // based on the number of enemies in the room. -- Xangis
            if( MUDMath.NumberRange( 1, 3 ) == 3 )
            {
                if( AreaOffensive( ch, victim ) )
                    return true;
                if( OffensiveSpell( ch, victim ) )
                    return true;
            }
            else
            {
                if( OffensiveSpell( ch, victim ) )
                    return true;
                if( AreaOffensive( ch, victim ) )
                    return true;
            }

            return false;
        }

        static bool SpecCastEarthEle( System.Object mob, int cmd )
        {
            if (mob == null) return false;
            if (cmd == PROC_DEATH)
                return false;
            CharData ch = (CharData)mob;
            if( ch.InRoom.HasFlag( RoomTemplate.ROOM_NO_MAGIC ) )
                return false;

            SummonIfHating( ch );

            if( !ch.CanSpeak() )
                return false;

            if( ch.CurrentPosition != Position.fighting )
            {
                if( ch.CurrentPosition == Position.standing )
                {
                    if( Spellup( ch ) )
                        return true;
                    if( SpellupOthers( ch ) )
                        return true;
                }
                return false;
            }

            CharData victim = ch.Fighting;

            if( !victim )
                return false;

            // Not sure whether being able to see the victim should be required...

            // Give a 1/3 chance of picking an area spell over a direct damage spell
            // later on, I plan to code it so that it makes an intelligence check
            // based on the number of enemies in the room. -- Xangis
            if( MUDMath.NumberRange( 1, 3 ) == 3 )
            {
                if( AreaOffensive( ch, victim ) )
                    return true;
                if( OffensiveSpell( ch, victim ) )
                    return true;
            }
            else
            {
                if( OffensiveSpell( ch, victim ) )
                    return true;
                if( AreaOffensive( ch, victim ) )
                    return true;
            }

            return false;
        }

        static bool SpecCastFireEle( System.Object mob, int cmd )
        {
            if (mob == null) return false;
            if (cmd == PROC_DEATH)
                return false;
            CharData ch = (CharData)mob;
            if( ch.InRoom.HasFlag( RoomTemplate.ROOM_NO_MAGIC ) )
                return false;

            SummonIfHating( ch );

            if( !ch.CanSpeak() )
                return false;

            if( ch.CurrentPosition != Position.fighting )
            {
                if( ch.CurrentPosition == Position.standing )
                {
                    if( Spellup( ch ) )
                        return true;
                    if( SpellupOthers( ch ) )
                        return true;
                }
                return false;
            }

            CharData victim = ch.Fighting;

            if( !victim )
                return false;

            // Not sure whether being able to see the victim should be required...

            // Give a 1/3 chance of picking an area spell over a direct damage spell
            // later on, I plan to code it so that it makes an intelligence check
            // based on the number of enemies in the room. -- Xangis
            if( MUDMath.NumberRange( 1, 3 ) == 3 )
            {
                if( AreaOffensive( ch, victim ) )
                    return true;
                if( OffensiveSpell( ch, victim ) )
                    return true;
            }
            else
            {
                if( OffensiveSpell( ch, victim ) )
                    return true;
                if( AreaOffensive( ch, victim ) )
                    return true;
            }

            return false;
        }

        static bool SpecCastWaterEle( System.Object mob, int cmd )
        {
            if (mob == null) return false;
            if (cmd == PROC_DEATH)
                return false;
            CharData ch = (CharData)mob;
            if( ch.InRoom.HasFlag( RoomTemplate.ROOM_NO_MAGIC ) )
                return false;

            SummonIfHating( ch );

            if( !ch.CanSpeak() )
                return false;

            if( ch.CurrentPosition != Position.fighting )
            {
                if( ch.CurrentPosition == Position.standing )
                {
                    if( Spellup( ch ) )
                        return true;
                    if( SpellupOthers( ch ) )
                        return true;
                }
                return false;
            }

            CharData victim = ch.Fighting;

            if( !victim )
                return false;

            // Not sure whether being able to see the victim should be required...

            // Give a 1/3 chance of picking an area spell over a direct damage spell
            // later on, I plan to code it so that it makes an intelligence check
            // based on the number of enemies in the room. -- Xangis
            if( MUDMath.NumberRange( 1, 3 ) == 3 )
            {
                if( AreaOffensive( ch, victim ) )
                    return true;
                if( OffensiveSpell( ch, victim ) )
                    return true;
            }
            else
            {
                if( OffensiveSpell( ch, victim ) )
                    return true;
                if( AreaOffensive( ch, victim ) )
                    return true;
            }

            return false;
        }

        static bool SpecCastRanger( System.Object mob, int cmd )
        {
            if (mob == null) return false;
            if (cmd == PROC_DEATH)
                return false;
            CharData ch = (CharData)mob;
            if( ch.Fighting && ch.CurrentPosition == Position.fighting )
            {
                if( CombatSkillCheck( ch ) )
                    return true;
            }

            if( ch.InRoom.HasFlag( RoomTemplate.ROOM_NO_MAGIC ) )
                return false;

            if( !ch.CanSpeak() )
                return false;

            SummonIfHating( ch );

            if( ch.CurrentPosition != Position.fighting )
            {
                if( ch.CurrentPosition == Position.standing )
                {
                    if( Spellup( ch ) )
                        return true;
                    if( ch.Hitpoints < ( ch.GetMaxHit() - 10 ) )
                        if( HealSelf( ch ) )
                            return true;
                    if( SpellupOthers( ch ) )
                        return true;
                }
                return false;
            }

            CharData victim = ch.Fighting;

            if( !victim )
                return false;

            // Not sure whether being able to see the victim should be required...

            // Give a 1/3 chance of picking an area spell over a direct damage spell
            // later on, I plan to code it so that it makes an intelligence check
            // based on the number of enemies in the room. -- Xangis
            if( MUDMath.NumberRange( 1, 3 ) == 3 )
            {
                if( AreaOffensive( ch, victim ) )
                    return true;
                if( OffensiveSpell( ch, victim ) )
                    return true;
            }
            else
            {
                if( OffensiveSpell( ch, victim ) )
                    return true;
                if( AreaOffensive( ch, victim ) )
                    return true;
            }

            return false;
        }

        static bool SpecCastHunter( System.Object mob, int cmd )
        {
            if (mob == null) return false;
            if (cmd == PROC_DEATH)
                return false;
            CharData ch = (CharData)mob;
            if( ch.Fighting && ch.CurrentPosition == Position.fighting )
            {
                if( CombatSkillCheck( ch ) )
                    return true;
            }

            if( ch.InRoom.HasFlag( RoomTemplate.ROOM_NO_MAGIC ) )
                return false;

            if( !ch.CanSpeak() )
                return false;

            SummonIfHating( ch );

            if( ch.CurrentPosition != Position.fighting )
            {
                if( ch.CurrentPosition == Position.standing )
                {
                    if( Spellup( ch ) )
                        return true;
                    if( SpellupOthers( ch ) )
                        return true;
                }
                return false;
            }

            CharData victim = ch.Fighting;

            if( !victim )
                return false;

            // Not sure whether being able to see the victim should be required...

            // Give a 1/3 chance of picking an area spell over a direct damage spell
            // later on, I plan to code it so that it makes an intelligence check
            // based on the number of enemies in the room. -- Xangis
            if( MUDMath.NumberRange( 1, 3 ) == 3 )
            {
                if( AreaOffensive( ch, victim ) )
                    return true;
                if( OffensiveSpell( ch, victim ) )
                    return true;
            }
            else
            {
                if( OffensiveSpell( ch, victim ) )
                    return true;
                if( AreaOffensive( ch, victim ) )
                    return true;
            }

            return false;
        }

        static bool SpecCastPaladin( System.Object mob, int cmd )
        {
            if (mob == null) return false;
            if (cmd == PROC_DEATH)
                return false;
            CharData ch = (CharData)mob;
            if( ch.Fighting && ch.CurrentPosition == Position.fighting )
            {
                if( CombatSkillCheck( ch ) )
                    return true;
            }

            if( ch.InRoom.HasFlag( RoomTemplate.ROOM_NO_MAGIC ) )
                return false;


            if( !ch.CanSpeak() )
                return false;

            SummonIfHating( ch );

            if( ch.CurrentPosition != Position.fighting )
            {
                if( ch.CurrentPosition == Position.standing )
                {
                    if( Spellup( ch ) )
                        return true;
                    if( ch.Hitpoints < ( ch.GetMaxHit() - 10 ) )
                        if( HealSelf( ch ) )
                            return true;
                    if( CheckVigorize( ch ) )
                        return true;
                    if( SpellupOthers( ch ) )
                        return true;
                }
                return false;
            }

            CharData victim = ch.Fighting;

            if( !victim )
                return false;

            // Not sure whether being able to see the victim should be required...

            // Give a 1/3 chance of picking an area spell over a direct damage spell
            // later on, I plan to code it so that it makes an intelligence check
            // based on the number of enemies in the room. -- Xangis
            if( MUDMath.NumberRange( 1, 3 ) == 3 )
            {
                if( AreaOffensive( ch, victim ) )
                    return true;
                if( OffensiveSpell( ch, victim ) )
                    return true;
            }
            else
            {
                if( OffensiveSpell( ch, victim ) )
                    return true;
                if( AreaOffensive( ch, victim ) )
                    return true;
            }

            return false;
        }

        static bool SpecCastAntipaladin( System.Object mob, int cmd )
        {
            if (mob == null) return false;
            if (cmd == PROC_DEATH)
                return false;
            CharData ch = (CharData)mob;
            if( ch.Fighting && ch.CurrentPosition == Position.fighting )
            {
                if( CombatSkillCheck( ch ) )
                    return true;
            }

            if( ch.InRoom.HasFlag( RoomTemplate.ROOM_NO_MAGIC ) )
                return false;

            if( !ch.CanSpeak() )
            {
                return false;
            }

            SummonIfHating( ch );

            if( ch.CurrentPosition != Position.fighting )
            {
                if( ch.CurrentPosition == Position.standing )
                {
                    if( Spellup( ch ) )
                        return true;
                    if( CheckVigorize( ch ) )
                        return true;
                    if( SpellupOthers( ch ) )
                        return true;
                }
                return false;
            }

            CharData victim = ch.Fighting;

            if( !victim )
                return false;

            // Not sure whether being able to see the victim should be required...

            // Give a 1/3 chance of picking an area spell over a direct damage spell
            // later on, I plan to code it so that it makes an intelligence check
            // based on the number of enemies in the room. -- Xangis
            if( MUDMath.NumberRange( 1, 3 ) == 3 )
            {
                if( AreaOffensive( ch, victim ) )
                    return true;
                if( OffensiveSpell( ch, victim ) )
                    return true;
            }
            else
            {
                if( OffensiveSpell( ch, victim ) )
                    return true;
                if( AreaOffensive( ch, victim ) )
                    return true;
            }

            return false;
        }

        static bool SpecCastIllusionist( System.Object mob, int cmd )
        {
            if (mob == null) return false;
            if (cmd == PROC_DEATH)
                return false;
            CharData ch = (CharData)mob;
            if( ch.InRoom.HasFlag( RoomTemplate.ROOM_NO_MAGIC ) )
                return false;

            if( !ch.CanSpeak() )
                return false;

            SummonIfHating( ch );

            if( ch.CurrentPosition != Position.fighting )
            {
                if( ch.CurrentPosition == Position.standing )
                {
                    if( Spellup( ch ) )
                        return true;
                    if( SpellupOthers( ch ) )
                        return true;
                }
                return false;
            }

            CharData victim = ch.Fighting;

            if( !victim )
                return false;

            // Not sure whether being able to see the victim should be required...

            // Give a 1/3 chance of picking an area spell over a direct damage spell
            // later on, I plan to code it so that it makes an intelligence check
            // based on the number of enemies in the room. -- Xangis
            if( MUDMath.NumberRange( 1, 3 ) == 3 )
            {
                if( AreaOffensive( ch, victim ) )
                    return true;
                if( OffensiveSpell( ch, victim ) )
                    return true;
            }
            else
            {
                if( OffensiveSpell( ch, victim ) )
                    return true;
                if( AreaOffensive( ch, victim ) )
                    return true;
            }

            return false;
        }

        static bool SpecAssassin( System.Object mob, int cmd )
        {
            if (mob == null) return false;
            
            if( cmd == PROC_DEATH )
                return false;
            CharData ch = (CharData)mob;
            if( ch.CurrentPosition != Position.standing )
            {
                if( ch.CurrentPosition == Position.fighting && ch.Fighting
                        && ch.Fighting.Fighting == ch )
                {
                    Combat.StartGrudge( ch, ch.Fighting, false );
                    CommandType.Interpret(ch, "flee");
                    CommandType.Interpret(ch, "hide");
                }
                return false;
            }

            // Backstab a random hate _targetType if one is in the room and we're not already busy.
            if( ch.Hating.Count > 0 )
            {
                if( !ch.Fighting && ch.CurrentPosition != Position.fighting )
                {
                    CharData vict = ch.GetRandomHateTarget(true);
                    if( vict != null )
                    {
                        if( Combat.Backstab( ch, vict ) )
                            return true;
                    }
                }
            }

            if( ch.Fighting && ch.CurrentPosition == Position.fighting )
            {
                if( CombatSkillCheck( ch ) )
                    return true;
            }

            return false;
        }

        static bool SpecWarrior( System.Object mob, int cmd )
        {
            if (mob == null) return false;
            
            if( cmd == PROC_DEATH )
                return false;
            CharData ch = (CharData)mob;
            if( ch.Fighting && ch.CurrentPosition == Position.fighting )
            {
                if( CombatSkillCheck( ch ) )
                    return true;
            }

            return false;
        }

        static bool SpecFireElemental( System.Object mob, int cmd )
        {
            if (mob == null) return false;
            
            if( cmd == PROC_DEATH )
                return false;
            CharData ch = (CharData)mob;
            if( ch.Fighting && ch.CurrentPosition == Position.fighting )
            {
                if( CombatSkillCheck( ch ) )
                    return true;
            }

            return false;
        }

        static bool SpecEarthElemental( System.Object mob, int cmd )
        {
            if (mob == null) return false;
            
            if( cmd == PROC_DEATH )
                return false;
            CharData ch = (CharData)mob;
            if( ch.Fighting && ch.CurrentPosition == Position.fighting )
            {
                if( CombatSkillCheck( ch ) )
                    return true;
            }

            return false;
        }

        static bool SpecAirElemental( System.Object mob, int cmd )
        {
            if (mob == null) return false;
            
            if( cmd == PROC_DEATH )
                return false;
            CharData ch = (CharData)mob;
            if( ch.Fighting && ch.CurrentPosition == Position.fighting )
            {
                if( CombatSkillCheck( ch ) )
                    return true;
            }

            return false;
        }

        static bool SpecWaterElemental( System.Object mob, int cmd )
        {
            if (mob == null) return false;
            
            if( cmd == PROC_DEATH )
                return false;
            CharData ch = (CharData)mob;
            if( ch.Fighting && ch.CurrentPosition == Position.fighting )
            {
                if( CombatSkillCheck( ch ) )
                    return true;
            }

            return false;
        }

        static bool SpecMonk( System.Object mob, int cmd )
        {
            if (mob == null) return false;
            
            if( cmd == PROC_DEATH )
                return false;
            CharData ch = (CharData)mob;
            if( ch.Fighting && ch.CurrentPosition == Position.fighting )
            {
                if( CombatSkillCheck( ch ) )
                    return true;
            }

            return false;
        }

        static bool SpecMercenary( System.Object mob, int cmd )
        {
            if (mob == null) return false;
            
            if( cmd == PROC_DEATH )
                return false;
            CharData ch = (CharData)mob;
            if( ch.Fighting && ch.CurrentPosition == Position.fighting )
            {
                if( CombatSkillCheck( ch ) )
                    return true;
            }

            // Backstab a random hate _targetType if one is in the room and we're not already busy.
            if( ch.Hating.Count > 0 )
            {
                if( !ch.Fighting && ch.CurrentPosition != Position.fighting )
                {
                    CharData vict = ch.GetRandomHateTarget( true );
                    if( vict != null )
                    {
                        if (Combat.Backstab(ch, vict))
                            return true;
                    }
                }
            }

            return false;
        }

        static bool SpecBard( System.Object mob, int cmd )
        {
            if (mob == null) return false;
            
            if( cmd == PROC_DEATH )
                return false;
            CharData ch = (CharData)mob;
            if( ch.Fighting && ch.CurrentPosition == Position.fighting )
            {
                if( CombatSkillCheck( ch ) )
                    return true;
            }

            return false;
        }

        static bool SpecCastUndead( System.Object mob, int cmd )
        {
            if (mob == null) return false;
            string spellname = String.Empty;
            Spell spell;

            if( cmd == PROC_DEATH )
                return false;
            CharData ch = (CharData)mob;
            if( ch.InRoom.HasFlag( RoomTemplate.ROOM_NO_MAGIC ) )
                return false;

            SummonIfHating( ch );

            if( ch.CurrentPosition != Position.fighting )
            {
                return false;
            }

            if( !ch.CanSpeak() )
            {
                return false;
            }

            CharData victim = null;
            foreach( CharData ivictim in ch.InRoom.People )
            {
                if (ivictim.Fighting == ch && CharData.CanSee(ch, ivictim)
                        && MUDMath.NumberBits(4) == 0)
                {
                    victim = ivictim;
                    break;
                }
            }

            if( !victim )
            {
                return false;
            }

            for( ; ; )
            {
                int minLevel = 0;

                switch( MUDMath.NumberBits( 4 ) )
                {
                    // case  0: min_level =  0; spell = "curse"; break;
                    case 1:
                        minLevel = 3;
                        spellname = "weaken";
                        break;
                    case 2:
                        minLevel = 6;
                        spellname = "chill touch";
                        break;
                    case 3:
                        minLevel = 9;
                        spellname = "blindness";
                        break;
                    case 4:
                        minLevel = 12;
                        spellname = "poison";
                        break;
                    case 5:
                        minLevel = 15;
                        spellname = "energy drain";
                        break;
                    case 6:
                        minLevel = 18;
                        spellname = "harm";
                        break;
                    case 7:
                        minLevel = 21;
                        spellname = "teleport";
                        break;
                    case 8:
                    case 9:
                    case 10:
                        if( ch.GetRace() == Race.RACE_VAMPIRE )
                        {
                            minLevel = 24;
                            spellname = "vampiric bite";
                        }
                        break;
                    default:
                        minLevel = 24;
                        spellname = "gate";
                        break;
                }

                if( ch.Level >= minLevel )
                    break;
            }

            if ((spell = StringLookup.SpellLookup(spellname)) == null)
                return false;

            int level = Macros.Range(1, ch.Level, Limits.LEVEL_HERO);
            spell.Invoke(ch, level, victim);
            return true;
        }

        static bool SpecCastVampire( System.Object mob, int cmd )
        {
            if (mob == null) return false;
            CharData ch = (CharData)mob;
            CharData victim = ch.Fighting;
            if( !victim )
            {
                return false;
            }

            if( cmd == PROC_DEATH )
                return false;

            if( ch.CurrentPosition >= Position.fighting )
            {
                // Allow a vampire to gaze in addition to its other abilities.
                if( MUDMath.NumberPercent() < 40 )
                {
                    SocketConnection.Act( "$n&n  turns $s gaze upon you.", ch, null, victim, SocketConnection.MessageTarget.victim );
                    Combat.StopFighting( ch, false );
                    foreach( CharData groupChar in ch.InRoom.People )
                    {
                        if( ch.IsSameGroup( groupChar ) && groupChar != ch )
                        {
                            SocketConnection.Act( "$n&n switches targets.", victim, null, null, SocketConnection.MessageTarget.room );
                            Combat.SetFighting( ch, groupChar );
                            break;
                        }
                    }
                }
            }

            return false;
        }

        static bool SpecExecutioner( System.Object mob, int cmd )
        {
            if (mob == null) return false;
            //string crime;
            //string buf;

            if( cmd == PROC_DEATH )
                return false;
            CharData ch = (CharData)mob;
            if( !ch.IsAwake() || ch.Fighting )
                return false;

            //crime = "pretty annoying person";

            //if( !victim )
                return false;

            //buf = String.Format( "{0} is a {1}!  Justice must be served!",
            //          victim._name, crime );
            //Command.shout( ch, buf );
            //ch.AttackCharacter( victim );
            //( Database.CreateMobile( Database.get_mob_index( StaticMobs.MOB_NUMBER_CITYGUARD ) ) ).AddToRoom( ch._inRoom );
            //( Database.CreateMobile( Database.get_mob_index( StaticMobs.MOB_NUMBER_CITYGUARD ) ) ).AddToRoom( ch._inRoom );
            //return true;
        }

        static bool SpecFido( System.Object mob, int cmd )
        {
            if (mob == null) return false;
            if (cmd == PROC_DEATH)
                return false;
            CharData ch = (CharData)mob;
            if( !ch.IsAwake() )
                return false;

            foreach( Object corpse in ch.InRoom.Contents )
            {
                 if( corpse.ItemType != ObjTemplate.ObjectType.npc_corpse )
                    continue;

                SocketConnection.Act( "$n&n savagely devours a corpse.", ch, null, null, SocketConnection.MessageTarget.room );
                foreach( Object obj in corpse.Contains )
                {
                    obj.RemoveFromObject();
                    obj.AddToRoom( ch.InRoom );
                }
                corpse.RemoveFromWorld();
                return true;
            }

            return false;
        }

        /// <summary>
        /// Guard special proc - rescue/assist allies in distress.
        /// </summary>
        /// <param name="mob"></param>
        /// <param name="cmd"></param>
        /// <returns></returns>
        static bool SpecGuard( System.Object mob, int cmd )
        {
            if (mob == null) return false;
            CharData target = null;

            if (cmd == PROC_DEATH)
            {
                return false;
            }
            CharData ch = (CharData)mob;
            if (!ch.IsAwake() || ch.Fighting)
            {
                return false;
            }

            foreach( CharData victim in ch.InRoom.People )
            {
                if (victim.IsNPC() && victim.GetRace() == ch.GetRace())
                {
                    continue;
                }

                if( victim.Fighting && ( victim.Fighting != ch ) && ( victim.Fighting.GetRace() == ch.GetRace() ) && ( MUDMath.NumberPercent() < 90 ) )
                {
                    target = victim;
                    continue;
                }
            }

            /*    if ( victim )
            {
            if(String.IsNullOrEmpty(crime))
            crime = "bastard";
            buf = String.Format(  "{0} is a {1}!  PROTECT THE INNOCENT!!  BANZAI!!",
            victim._name, crime );
            Command.shout( ch, buf );
            ch.AttackCharacter( ref victim );
            return true;
            }*/

            if( target != null )
            {
                SocketConnection.Act( "$n&n screams 'PROTECT THE INNOCENT!!  BANZAI!!",
                     ch, null, null, SocketConnection.MessageTarget.room );
                ch.AttackCharacter( target );
                return true;
            }

            return false;
        }

        static bool SpecJanitor( System.Object mob, int cmd )
        {
            if (mob == null) return false;
            if (cmd == PROC_DEATH)
                return false;
            CharData ch = (CharData)mob;
            if( !ch.IsAwake() )
                return false;

            foreach( Object trash in ch.InRoom.Contents )
            {
                if( !trash.HasWearFlag( ObjTemplate.WEARABLE_CARRY ) )
                    continue;
                if( trash.ItemType == ObjTemplate.ObjectType.drink_container
                        || trash.ItemType == ObjTemplate.ObjectType.trash
                        || trash.Cost < 10 )
                {
                    SocketConnection.Act( "$n&n picks up some trash.", ch, null, null, SocketConnection.MessageTarget.room );
                    trash.RemoveFromRoom();
                    trash.ObjToChar( ch );
                    return true;
                }
            }

            return false;
        }

        static bool SpecPoison( System.Object mob, int cmd )
        {
            if (mob == null) return false;
            CharData victim;

            if( cmd == PROC_DEATH )
            {
                return false;
            }
            CharData ch = (CharData)mob;
            if( !ch.Fighting || !( victim = ch.Fighting ) || MUDMath.NumberPercent() > 2 * ch.Level )
            {
                return false;
            }

            SocketConnection.Act( "You bite $N&n!", ch, null, victim, SocketConnection.MessageTarget.character );
            SocketConnection.Act( "$n&n bites you!", ch, null, victim, SocketConnection.MessageTarget.victim );
            SocketConnection.Act( "$n&n bites $N&n!", ch, null, victim, SocketConnection.MessageTarget.everyone_but_victim );
            Spell spl = Spell.SpellList["poison"];
            if (spl != null)
            {
                spl.Invoke(ch, ch.Level, victim);
            }
            return true;
        }

        static bool SpecThief( System.Object mob, int cmd )
        {
            if (mob == null) return false;
            if (cmd == PROC_DEATH)
            {
                return false;
            }
            CharData ch = (CharData)mob;
            if( ch.CurrentPosition != Position.standing )
            {
                if( ch.CurrentPosition == Position.fighting && ch.Fighting && ch.Fighting.Fighting == ch )
                {
                    Combat.StartGrudge( ch, ch.Fighting, false );
                    CommandType.Interpret( ch, "flee" );
                    CommandType.Interpret(ch, "hide");
                }
                return false;
            }

            // Backstab a random hate _targetType if one is in the room and we're not already busy.
            if( ch.Hating.Count > 0 )
            {
                if( !ch.Fighting && ch.CurrentPosition != Position.fighting )
                {
                    CharData vict = ch.GetRandomHateTarget( true );
                    if( vict != null )
                    {
                        if (Combat.Backstab(ch, vict))
                            return true;
                    }
                }
            }

            if( ch.Fighting && ch.CurrentPosition == Position.fighting )
            {
                if( CombatSkillCheck( ch ) )
                    return true;
            }

            foreach( CharData victim in ch.InRoom.People )
            {
                if (victim.IsNPC() || victim.Level >= Limits.LEVEL_AVATAR
                    || victim.FlightLevel != ch.FlightLevel || MUDMath.NumberBits(3) != 0
                    || !CharData.CanSee(ch, victim))
                {
                    continue;
                }

                if( victim.IsAwake() && victim.Level > 5 && MUDMath.NumberPercent() + ch.Level - victim.Level >= 33 )
                {
                    SocketConnection.Act( "You discover $n&n's hands in your purse!",
                         ch, null, victim, SocketConnection.MessageTarget.victim );
                    SocketConnection.Act( "$N&n discovers $n&n's hands in $S purse!",
                         ch, null, victim, SocketConnection.MessageTarget.everyone_but_victim );
                    return true;
                }
                int gold = victim.GetCash() * MUDMath.NumberRange( 1, 20 ) / 100;
                ch.ReceiveCash( gold );
                victim.SpendCash( gold );
                return true;
            }

            return false;
        }

        /*
        * Psionicist spec_fun
        */
        static bool SpecCastPsionicist( System.Object mob, int cmd )
        {
            if (mob == null) return false;
            string spellname;
            Spell spell;

            if( cmd == PROC_DEATH )
            {
                return false;
            }
            CharData ch = (CharData)mob;
            if( !ch.CanSpeak() )
            {
                return false;
            }

            if( ch.CurrentPosition != Position.fighting )
            {
                if( ch.CurrentPosition == Position.standing )
                {
                    if( Spellup( ch ) )
                        return true;
                    if( SpellupOthers( ch ) )
                        return true;
                }
                return false;
            }

            CharData victim = null;
            foreach( CharData ivictim in ch.InRoom.People )
            {
                if (ivictim.Fighting == ch && CharData.CanSee(ch, ivictim)
                        && MUDMath.NumberBits(2) == 0)
                {
                    victim = ivictim;
                    break;
                }
            }

            if( !victim )
                return false;

            for( ; ; )
            {
                int minLevel;

                switch( MUDMath.NumberBits( 4 ) )
                {
                    case 0:
                        minLevel = 0;
                        spellname = "mind thrust";
                        break;
                    case 1:
                        minLevel = 4;
                        spellname = "psychic drain";
                        break;
                    case 2:
                        minLevel = 6;
                        spellname = "agitation";
                        break;
                    case 3:
                        minLevel = 8;
                        spellname = "psychic crush";
                        break;
                    case 4:
                        minLevel = 9;
                        spellname = "project force";
                        break;
                    case 5:
                        minLevel = 13;
                        spellname = "ego whip";
                        break;
                    case 6:
                        minLevel = 14;
                        spellname = "energy drain";
                        break;
                    case 7:
                    case 8:
                        minLevel = 17;
                        spellname = "psionic blast";
                        break;
                    case 9:
                        minLevel = 31;
                        spellname = "detonate";
                        break;
                    case 10:
                        minLevel = 27;
                        spellname = "disintegrate";
                        break;
                    case 11:
                        minLevel = 41;
                        spellname = "neural fragmentation";
                        break;
                    case 12:
                        minLevel = 21;
                        spellname = "inflict pain";
                        break;
                    default:
                        minLevel = 25;
                        spellname = "ultrablast";
                        break;
                }

                if( ch.Level >= minLevel )
                    break;
            }

            if ((spell = StringLookup.SpellLookup(spellname)) == null)
                return false;

            int level = Macros.Range(1, ch.Level, Limits.LEVEL_HERO);
            spell.Invoke(ch, level, victim);
            return true;
        }

        static bool SpecCastGhost( System.Object mob, int cmd )
        {
            if (mob == null) return false;
            CharData victim = null;
            string spellname;
            Spell spell;
 
            if( cmd == PROC_DEATH )
            {
                return false;
            }
            CharData ch = (CharData)mob;
            SummonIfHating( ch );

            if( Database.SystemData.WeatherData.Sunlight == SunType.daytime )
            {

                if( !ch.InRoom )
                {
                    Log.Error( "Spec_cast_ghost: null in_room.", 0 );
                    return false;
                }

                if( ch.Fighting != null )
                    Combat.StopFighting( ch, true );

                SocketConnection.Act( "A beam of sunlight strikes $n&n, destroying $m.", ch, null, null, SocketConnection.MessageTarget.room );

                CharData.ExtractChar( ch, true );
                
                return true;

            }

            if( ch.CurrentPosition != Position.fighting )
                return false;

            if( !ch.CanSpeak() )
                return false;

            foreach( CharData ivictim in ch.InRoom.People )
            {
                if (ivictim.Fighting == ch && CharData.CanSee(ch, ivictim)
                        && MUDMath.NumberBits(2) == 0)
                {
                    victim = ivictim;
                    break;
                }
            }

            if( !victim )
                return false;

            for( ; ; )
            {
                int minLevel;

                switch( MUDMath.NumberBits( 4 ) )
                {
                    //  case  0: min_level =  0; spell = "curse";          break;
                    case 1:
                        minLevel = 3;
                        spellname = "weaken";
                        break;
                    case 2:
                        minLevel = 6;
                        spellname = "chill touch";
                        break;
                    case 3:
                        minLevel = 9;
                        spellname = "blindness";
                        break;
                    case 4:
                        minLevel = 12;
                        spellname = "poison";
                        break;
                    case 5:
                        minLevel = 15;
                        spellname = "energy drain";
                        break;
                    case 6:
                        minLevel = 18;
                        spellname = "harm";
                        break;
                    case 7:
                        minLevel = 21;
                        spellname = "teleport";
                        break;
                    default:
                        minLevel = 24;
                        spellname = "gate";
                        break;
                }

                if( ch.Level >= minLevel )
                    break;
            }

            if( ( spell = StringLookup.SpellLookup( spellname ) ) == null )
            {
                return false;
            }

            int level = Macros.Range(1, ch.Level, Limits.LEVEL_HERO);
            spell.Invoke(ch, level, victim);
            return true;
        }

        /*
        * spec_fun  to repair bashed doors 
        */
        static bool SpecRepairman( System.Object mob, int cmd )
        {
            if (mob == null) return false;
            Exit reverseExit;
            Room toRoom;

            if( cmd == PROC_DEATH )
                return false;
            CharData ch = (CharData)mob;
            if( !ch.IsAwake() )
                return false;

            Exit.Direction door = Database.RandomDoor();
            /*
            *   Could search through all doors randomly, but deathtraps would 
            *   freeze the game!  And I'd prefer not to go through from 1 to 6...
            *   too boring.  Instead, just check one direction at a time.  There's
            *   a 51% chance they'll find the door within 4 tries anyway.
            */
            Exit exit = ch.InRoom.GetExit(door);
            if( !exit )
            {
                return false;
            }

            if (exit.HasFlag(Exit.ExitFlag.bashed))
            {
                exit.RemoveFlag(Exit.ExitFlag.bashed);
                SocketConnection.Act( "You repair the $d.", ch, null, exit.Keyword, SocketConnection.MessageTarget.character );
                SocketConnection.Act( "$n&n repairs the $d.", ch, null, exit.Keyword, SocketConnection.MessageTarget.room );

                /* Don't forget the other side! */
                if ((toRoom = Room.GetRoom(exit.IndexNumber)) && (reverseExit = toRoom.GetExit(Exit.ReverseDirection(door)))
                        && reverseExit.TargetRoom == ch.InRoom )
                {
                    reverseExit.RemoveFlag(Exit.ExitFlag.bashed);

                    foreach (CharData roomChar in toRoom.People)
                    {
                        SocketConnection.Act("The $d is set back on its hinges.", roomChar, null, reverseExit.Keyword, SocketConnection.MessageTarget.character);
                    }
                }

                return true;
            }

            return false;
        }

        // Guards spawned during an invasion.  This causes them to unspawn if the victim is gone.
        static bool SpecJusticeGuard( System.Object mob, int cmd )
        {
            if (mob == null) return false;
            
            if( cmd == PROC_DEATH )
                return false;
            CharData ch = (CharData)mob;
            // if the victim is gone or dead, then get rid of the guard.
            if( !ch.Hunting && ch.Hating.Count == 0 )
            {
                if( ch.InRoom && ch.InRoom.Area.NumDefendersDispatched > 0 )
                    ch.InRoom.Area.NumDefendersDispatched--;
                {
                    SocketConnection.Act( "$n&n returns to the barracks.", ch, null, null, SocketConnection.MessageTarget.room );
                    CharData.ExtractChar( ch, true );
                }

                return true;
            }

            // if they leave the zone, the guards need to dissipate after a _bitvector
            // or the town will be left helpless because the guards will want to kill
            // someone who is long gone and not spawn to defend the town.
            // since they stop hunting someone who has left the zone, they should stop
            // hating them in order to dissipate.  To keep someone from popping in and
            // out to distractFlags the guards, it should take a _bitvector for them to dissipate.
            // this can be done by using a random chance of rehunting or stop hating
            if( ch.Hating.Count > 0 )
            {
                if( MUDMath.NumberPercent() < 50 )
                {
                    ch.Hating.Clear();
                    return true;
                }
                CharData vict = ch.GetRandomHateTarget(false);
                if( vict != null )
                {
                    Combat.StartHunting( ch, vict );
                }
                return true;
            }

            if( !ch.IsAwake() || ch.Fighting )
                return false;

            return false;
        }

        // Guard spawned when a crime is committed and the criminal needs to be captured
        static bool SpecJusticeTracker( System.Object mob, int cmd )
        {
            if (mob == null) return false;
            
            if( cmd == PROC_DEATH )
                return false;
            CharData ch = (CharData)mob;
            // if the victim is gone or dead, then get rid of the guard.
            if( !ch.Hunting )
            {
                CharData.ExtractChar( ch, true );
                
                return true;
            }

            if( !ch.IsAwake() || ch.Fighting )
                return false;

            return false;
        }

        // Checks for a mob spelling itself up.  Certain spells have higher
        // priority and percentage chances.
        //
        // The spell_lookup _function is horribly inefficient and anywhere possible
        // should be replaced with _skillNumber's (Spell.SpellNumber_armor and whatever) - Xangis
        //
        // The commented out spells have not been written yet.
        //
        // The CheckSpellup _function automatically checks to see whether they
        // are effected by the particular spell, so unless the spell has an
        // associated bitvector, there is no need for extra logic.
        //
        // Spells with associated bitvectors will need the "CharData.IsAffected" stuff
        // before the if( CheckSpellup( ... ) ) for the particular spell
        static bool Spellup( CharData ch )
        {
            if (ch == null) return false;
            if (ch.IsAffected(Affect.AFFECT_BLIND))
            {
                if( CheckSpellup( ch, "purify spirit", 35 ) )
                    return true;
            }

            if( !ch.IsAffected( Affect.AFFECT_BLUR ) )
                if( CheckSpellup( ch, "blur", 45 ) )
                    return true;

            /* Fireshield and Frostshield cancel eachother!  So, we don't want
            * that  to happen.  Easiest way is to give a chance to try either.
            * Odds  are, if they only have one of the two, that they'll take twice
            * as long to cast it on themselves. Yes, this is a  quick fix.
            */
            if( !ch.IsAffected( Affect.AFFECT_FIRESHIELD ) && !ch.IsAffected( Affect.AFFECT_COLDSHIELD ) )
            {
                if( MUDMath.NumberPercent() < 50 )
                {
                    if( CheckSpellup( ch, "fireshield", 30 ) )
                        return true;
                }
                else
                    if( CheckSpellup( ch, "coldshield", 30 ) )
                        return true;
            }

            if( CheckSpellup( ch, "vitality", 55 ) )
                return true;

            return false;
        }

        static bool OffensiveSpell( CharData ch, CharData victim )
        {
            if (ch == null) return false;
            if (CheckOffensive(ch, victim, "fireball", 60))
                return true;
            return false;
        }

        // Keep in mind that any healing spells that give affects to the mob
        // will only be able to be cast if they are not affected by them because
        // the CheckSpellup _function checks to see if they are affected by
        // that spell, so a heal spell that heals *and* blesses will not work
        // properly and we'll need to write a new _function for it -- Xangis
        static bool HealSelf( CharData ch )
        {
            if (ch == null) return false;
            if (CheckSpellup(ch, "heal", 75))
                return true;
            return false;
        }

        /// <summary>
        /// Check for casting of vigorize spells.
        /// </summary>
        /// <param name="ch"></param>
        /// <returns></returns>
        public static bool CheckVigorize( CharData ch )
        {
            if (ch == null) return false;
            if (ch.CurrentMoves > 30)
                return false;
            if( CheckSpellup( ch, "stamina", 35 ) )
                return true;
            return false;
        }

        static bool AreaOffensive( CharData ch, CharData victim )
        {
            if (ch == null) return false;
            if (ch.IsOutside())
            {
                if( CheckOffensive( ch, victim, "meteor swarm", 50 ) )
                    return true;
                if( !ch.IsUnderground() )
                    if( CheckOffensive( ch, victim, "earthen rain", 55 ) )
                        return true;
            }
            if( CheckOffensive( ch, victim, "incendiary cloud", 45 ) )
                return true;
            if( CheckOffensive( ch, victim, "ice storm", 25 ) )
                return true;
            if( CheckOffensive( ch, victim, "earthquake", 50 ) )
                return true;

            return false;
        }

        /// <summary>
        /// Check an offensive spell based on its _name.  If the spell doesn't exist we don't bother.
        /// </summary>
        /// <param name="ch"></param>
        /// <param name="victim"></param>
        /// <param name="spellName"></param>
        /// <param name="percent"></param>
        /// <returns></returns>
        public static bool CheckOffensive(CharData ch, CharData victim, string spellName, int percent)
        {
            if (ch == null) return false;
            Spell spell = Spell.SpellList[spellName];
            if (spell != null)
            {
                return CheckOffensive(ch, victim, spell, percent);
            }
            return false;
        }

        /// <summary>
        /// Check for use of Direct targeted spells (TargetType.singleCharacterOffensive)
        /// </summary>
        /// <param name="ch"></param>
        /// <param name="victim"></param>
        /// <param name="spell"></param>
        /// <param name="percent"></param>
        /// <returns></returns>
        public static bool CheckOffensive( CharData ch, CharData victim, Spell spell, int percent )
        {
            if (ch == null) return false;
            string buf = String.Format("CheckOffensive: spell ({0})'{1}'", spell, spell.Name);
            Log.Trace( buf );

            if( spell == null )
                return false;

            if( ch.HasSpell(spell) && ( MUDMath.NumberPercent() < percent ) )
            {
                if (spell.ValidTargets != TargetType.singleCharacterOffensive &&
                        spell.ValidTargets != TargetType.none &&
                        spell.ValidTargets != TargetType.singleCharacterRanged)
                    Log.Error( "Check_spellup:  Mob casting spell {0} which is neither _targetType offensive nor ignore.a", spell );

                SocketConnection.Act( "$n&n  starts casting...", ch, null, null, SocketConnection.MessageTarget.room );
                ch.SetAffectBit( Affect.AFFECT_CASTING );
                CastData caster = new CastData();
                caster.Who = ch;
                caster.Eventdata = Event.CreateEvent(Event.EventType.spell_cast, spell.CastingTime, ch, victim, spell);
                Database.CastList.Add( caster );
                return true;
            }

            return false;
        }

        // Defensive targeted spells (TargetType.singleCharacterDefensive)
        // Mainly spellups that can be cast on others
        static bool CheckDefensive( CharData ch, CharData victim, string name, int percent )
        {
            if (ch == null) return false;
            if (String.IsNullOrEmpty(name))
                return false;

            Spell spell = Spell.SpellList[name];
            if (spell == null)
            {
                return false;
            }

            if( ( ch.HasSpell( name ) )
                    && ( MUDMath.NumberPercent() < percent ) )
            {
                if (spell.ValidTargets != TargetType.singleCharacterDefensive &&
                        spell.ValidTargets != TargetType.none)
                    Log.Error( "Check_defensive:  Mob casting spell {0} which is neither _targetType defensive nor ignore.b", spell );

                SocketConnection.Act( "$n&n  starts casting...", ch, null, null, SocketConnection.MessageTarget.room );
                ch.SetAffectBit( Affect.AFFECT_CASTING );
                CastData caster = new CastData();
                caster.Who = ch;
                caster.Eventdata = Event.CreateEvent(Event.EventType.spell_cast, spell.CastingTime, ch, victim, spell);
                Database.CastList.Add( caster );
                return true;
            }

            return false;
        }

        // This _function should prevent a mob from spamming
        // spellups on itself if it is already affected by the spell.
        // in the Spellup() _function, checks for Affect.AFFECT_WHATEVER
        // still need to be done. - Xangis
        static bool CheckSpellup( CharData ch, string name, int percent )
        {
            if (ch == null) return false;
            if (String.IsNullOrEmpty(name))
                return false;

            Spell spell = Spell.SpellList[name];
            if (spell == null)
            {
                return false;
            }

            // Keep mobs from casting spells they are affected by
            if( ch.HasAffect( Affect.AffectType.spell, spell ) )
                return false;

            if( ( ch.HasSpell( name ) )
                    && ( MUDMath.NumberPercent() < percent ) )
            {
                if (spell.ValidTargets != TargetType.singleCharacterDefensive &&
                        spell.ValidTargets != TargetType.self)
                    Log.Error( "CheckSpellup:  Mob casting spell {0} which is neither TargetType.self nor TargetType.defensive.", spell );

                SocketConnection.Act( "$n&n starts casting...", ch, null, null, SocketConnection.MessageTarget.room );
                ch.SetAffectBit( Affect.AFFECT_CASTING );
                CastData caster = new CastData();
                caster.Who = ch;
                caster.Eventdata = Event.CreateEvent(Event.EventType.spell_cast, spell.CastingTime, ch, ch, spell);
                Database.CastList.Add( caster );
                return true;
            }

            return false;
        }

        // Checks melee combat skills for mobs - Xangis
        static bool CombatSkillCheck( CharData ch )
        {
            if (ch == null) return false;
            if (ch.HasSkill("kick") && MUDMath.NumberPercent() < 60)
            {
                CommandType.Interpret(ch, "kick");
                return true;
            }

            if( ch.HasSkill( "bash" ) && MUDMath.NumberPercent() < 40
                    && ( ch.Fighting != null )
                    && ( ch.CurrentSize >= ch.Fighting.CurrentSize && ch.CurrentSize - 2 <= ch.Fighting.CurrentSize ) )
            {
                CommandType.Interpret(ch, "bash");
                return true;
            }

            if( ch.HasSkill( "springleap" ) && MUDMath.NumberPercent() < 50 )
            {
                CommandType.Interpret(ch, "springleap");
                return true;
            }

            if( ch.HasSkill( "headbutt" ) && MUDMath.NumberPercent() < 40 )
            {
                CommandType.Interpret(ch, "headbutt");
                return true;
            }

            if( ch.HasSkill( "circle" ) && MUDMath.NumberPercent() < 40 )
            {
                CommandType.Interpret(ch, "circle");
                return true;
            }

            if( ch.HasSkill( "trip" ) && MUDMath.NumberPercent() < 35 )
            {
                CommandType.Interpret(ch, "trip");
                return true;
            }

            if( ch.HasSkill( "dirt toss" ) && MUDMath.NumberPercent() < 60 )
            {
                CommandType.Interpret(ch, "dirt");
                return true;
            }

            return false;
        }

        static bool SpellupOthers( CharData ch )
        {
            if (ch == null) return false;
            if (ch.InRoom.HasFlag(RoomTemplate.ROOM_NO_MAGIC))
                return false;

            if( !ch.IsAwake() || ch.Fighting )
                return false;

            if( !ch.CanSpeak() )
                return false;

            CharData victim = null;
            foreach( CharData ivictim in ch.InRoom.People )
            {
                if (ivictim != ch && CharData.CanSee(ch, ivictim) && MUDMath.NumberBits(1) == 0 && ivictim.IsNPC())
                {
                    victim = ivictim;
                    break;
                }
            }

            if( !victim )
                return false;

            if( victim.Hitpoints < ( victim.GetMaxHit() - 10 ) )
            {
                if( CheckDefensive( ch, victim, "full heal", 75 ) )
                    return true;
                if( CheckDefensive( ch, victim, "aid", 60 ) )
                    return true;
                if( CheckDefensive( ch, victim, "heal", 75 ) )
                    return true;
                if( CheckDefensive( ch, victim, "mending", 75 ) )
                    return true;
            }

            if (!victim.IsAffected(Affect.AFFECT_HASTE))
                if( CheckDefensive( ch, victim, "haste", 45 ) )
                    return true;

            return false;
        }

        static bool SpecLostGirl( System.Object mob, int cmd )
        {
            if (mob == null) return false;
            CharData i;
            CharData wasFighting = null;
            const int indexNumber = 20531;
            CharData ch = (CharData)mob;
            if( !ch || !ch.IsAwake() )
                return false;

            if( ch.Fighting )
            {
                CommandType.Interpret(ch, "say I would not do such things if I were you!");

                CharData demon = TransformMob( ch, indexNumber, null );
                if( !demon )
                {
                    return false;
                }
                CommandType.Interpret(demon, "say Now you shall pay!");
                //call for assistance   (sets helpers hating whoever is fighting)
                ShoutAndHunt( demon, wasFighting, "Come, my demon brethren!", null );
                //      ShoutAndHunt( demon, was_fighting, "Come, my demon brethren!", helpers );

                
                // set the lil critters aggro, so   they can transform
                foreach( CharData it in Database.CharList )
                {
                    i = it;
                    if( !i.MobileTemplate )
                        continue;
                    if( i.IsNPC() && ( ( i.MobileTemplate.IndexNumber == 20524 ) ||
                                      ( i.MobileTemplate.IndexNumber == 20525 ) ||
                                      ( i.MobileTemplate.IndexNumber == 20526 ) )
                            && i.InRoom && i.InRoom.Area == ch.InRoom.Area )
                    {
                        SocketConnection.Act( "$n shivers.", i, null, null, SocketConnection.MessageTarget.room );
                        i.Hitpoints = i.MaxHitpoints = 5000;
                        i.SetActionBit(MobTemplate.ACT_AGGRESSIVE);
                    } //end if
                }   //end for
                // set the trees aggro too
                foreach( CharData it in Database.CharList )
                {
                    i = it;
                    if( i.IsNPC() && ( ( i.MobileTemplate.IndexNumber == 20528 ) ||
                                      ( i.MobileTemplate.IndexNumber == 20529 ) ) &&
                            i.InRoom && i.InRoom.Area == ch.InRoom.Area )
                    {
                        SocketConnection.Act( "$n shivers.", i, null, null, SocketConnection.MessageTarget.room );
                        i.SetActionBit(MobTemplate.ACT_AGGRESSIVE );
                    } //end if
                }   //end for
                CharData.ExtractChar( ch, true );
                return true;
            }

            return false;
        }

        /// <summary>
        /// Shout for one's minions to come and attack the victim.
        /// </summary>
        /// <param name="mob"></param>
        /// <param name="victim"></param>
        /// <param name="msg"></param>
        /// <param name="helpers"></param>
        /// <returns></returns>
        static bool ShoutAndHunt( System.Object mob, CharData victim, string msg, int[] helpers )
        {
            if (mob == null) return false;
            int count = 0;
            CharData ch = (CharData)mob;
            // Send the shout message
            string buf = String.Format( msg, victim.Name );
            CommandType.Interpret(ch, "shout " + buf);

            if (helpers[0] == 0)
            {
                return false;
            }
            // Loop through all chars
            foreach (CharData worldChar in Database.CharList)
            {
                if( !worldChar.InRoom )
                    continue;
                if( !worldChar.MobileTemplate )
                    continue;
                if( !worldChar.IsNPC() || worldChar.InRoom.Area != ch.InRoom.Area )
                    continue;
                bool isHelper = false;
                int i;
                for( i = 0; helpers[ i ] > 0; i++ )
                {
                    if (worldChar.MobileTemplate.IndexNumber == helpers[i])
                    {
                        isHelper = true;
                    }
                }
                if (!isHelper)
                {
                    continue;
                }

                Combat.StartGrudge( worldChar, victim, true );
                
                ++count;
            }
            if (count > 0)
            {
                return true;
            }
            return false;
        }

        static bool SpecGrumbarShout( System.Object mob, int cmd )
        {
            if (mob == null) return false;
            int[] helpers = new[] {9500, 9501, 9502, 9503, 9504, 9505, 9506, 9507,
                         9508, 9509, 9510, 0};

            if( cmd == PROC_DEATH )
                return false;
            CharData ch = (CharData)mob;
            if( !ch || !ch.IsAwake() )
                return false;

            if( !ch.Fighting )
            {
                _wasFighting = null;
                return false;
            }

            if( ch.Fighting == _wasFighting )
                return false;

            _wasFighting = ch.Fighting;
            return ShoutAndHunt( ch, ch.Fighting, "&+LDenizens of the Earth plane, {0} has trespassed upon us!", helpers );
        }

        static bool SpecXorn( System.Object mob, int cmd )
        {
            if (mob == null) return false;
            CharData vict;

            if( cmd == PROC_DEATH )
                return false;
            CharData ch = (CharData)mob;
            if( ch.Fighting )
            {
                if( MUDMath.NumberPercent() < 50 )
                {
                    // make sure they are pissed off
                    Combat.StartGrudge( ch, ch.Fighting, true );
                    // teleport  out
                    ch.Fighting.Fighting = null;
                    ch.Fighting = null;
                    MoveXorn( ch, Room.GetRoom( 9676 ), XORN_MOVE_EARTH );
                    return true;
                }
                return false;
            }
            if( ch.Hating.Count > 0 || ch.Hunting )
            {
                //        if ( MUDMath.NumberPercent() > 50 ) return false;
                vict = ch.Hunting.Who;
                if( !vict )
                    vict = ch.GetRandomHateTarget(false);
                if( !vict )
                {
                    Log.Error( "SpecXorn: mob is hating or hunting, but who is null", 0 );
                    return false;
                }
                if( !vict.InRoom || vict.InRoom.Area != ch.InRoom.Area )
                {
                    // vict not in EP
                    return false;
                }
                MoveXorn( ch, vict.InRoom, XORN_MOVE_EARTH );
                ch.AttackCharacter( vict );
                return true;
            }

            if( MUDMath.NumberPercent() < 15 )
            {
                // phase in/out of existance
                if( ch.InRoom && ch.InRoom.IndexNumber == 9676 )
                {
                    MoveXorn( ch, null, XORN_MOVE_PHASE );
                }
                else
                {
                    MoveXorn( ch, Room.GetRoom( 9676 ), XORN_MOVE_PHASE );
                }
                return true;
            }

            return false;
        }

        static void MoveXorn( System.Object mob, Room toRoom, int type )
        {
            Room room = toRoom;
            CharData ch = (CharData)mob;
            if( room == null )
            {
                int min = ch.InRoom.Area.LowRoomIndexNumber;
                int max = ch.InRoom.Area.HighRoomIndexNumber;
                while( !room )
                {
                    room = Room.GetRoom( MUDMath.NumberRange( min, max ) );
                }
            }
            if( type == XORN_MOVE_EARTH )
                SocketConnection.Act( "$n&n&+L sinks into the ground and dissappears from sight!&n",
                     ch, null, null, SocketConnection.MessageTarget.room );
            else
                SocketConnection.Act( "$n&n phases out of existence.", ch, null, null, SocketConnection.MessageTarget.room );

            ch.RemoveFromRoom();
            ch.AddToRoom( room );
            if( type == XORN_MOVE_EARTH )
                SocketConnection.Act( "$n&n&+L rises out of the earth right beside you.&n",
                     ch, null, null, SocketConnection.MessageTarget.room );
            else
                SocketConnection.Act( "$n&n phases into existence.", ch, null, null, SocketConnection.MessageTarget.room );
            return;
        }

        static CharData TransformMob( CharData ch, int indexNumber, string msg )
        {
            CharData wasFighting = null;

            if( ch.Fighting )
            {
                wasFighting = ch.Fighting;
                Combat.StopFighting( ch, false );
            }

            CharData newCh = Database.CreateMobile( Database.GetMobTemplate( indexNumber ) );

            if( !newCh )
            {
                Log.Trace( "assert: mob load failed in TransformMob()" );
                return null;
            }
            newCh.AddToRoom( ch.InRoom );

            foreach( Object item in ch.Carrying )
            {
                item.RemoveFromChar();
                item.ObjToChar( newCh );
            }

            foreach (ObjTemplate.WearLocation pos in Enum.GetValues(typeof(ObjTemplate.WearLocation)))
            {
                Object item = Object.GetEquipmentOnCharacter( ch, pos );
                if( !item )
                    continue;
                ch.UnequipObject( item );
                item.RemoveFromChar();
                item.ObjToChar( newCh );
                newCh.EquipObject( ref item, pos );
            }
            newCh.CurrentPosition = ch.CurrentPosition;
            newCh.ActionFlags = ch.ActionFlags;
            newCh.RemoveActionBit(PC.PLAYER_WIZINVIS);
            newCh.Hating = ch.Hating;
            ch.Hating = null;
            newCh.Hunting = ch.Hunting;
            ch.Hunting = null;
            newCh.Fearing = ch.Fearing;
            ch.Fearing = null;

            if( msg.Length > 0 )
                SocketConnection.Act( msg, ch, null, newCh, SocketConnection.MessageTarget.room );
            else
                SocketConnection.Act( "$n&n suddenly changes into $N!&n", ch, null, newCh, SocketConnection.MessageTarget.room );

            if( wasFighting != null )
                Combat.SetFighting( newCh, wasFighting );

            return newCh;

        }

        static bool SpecLilCritter( System.Object mob, int cmd )
        {
            CharData newCh;
            const int indexNumber = 20530;
            CharData ch = (CharData)mob;
            if( cmd == PROC_DEATH )
            {
                SocketConnection.Act( "The corpse of $n&n &+Wglows&n with a strange light.",
                     ch, null, null, SocketConnection.MessageTarget.room );
                newCh = Database.CreateMobile( Database.GetMobTemplate( ch.MobileTemplate.IndexNumber ) );
                newCh.AddToRoom( ch.InRoom );
                newCh.ActionFlags = ch.ActionFlags;
                newCh.RemoveActionBit(PC.PLAYER_WIZINVIS);
                SocketConnection.Act( "$p&n comes back to life!", ch, null, null, SocketConnection.MessageTarget.room );
                if( ch.HasActionBit(MobTemplate.ACT_AGGRESSIVE ) )
                {
                    ch.Hitpoints = ch.MaxHitpoints = 5000;
                }
                return true; //make no corpse
            }
            if( ch.Fighting && ch.HasActionBit(MobTemplate.ACT_AGGRESSIVE ) )
            {
                // transform into a demon
                CharData demon = TransformMob( ch, indexNumber, null );
                if( !demon )
                {
                    return false;
                }
                /* get rid of the mob */
                CharData.ExtractChar( ch, true );

                return true;
            }
            return false;
        }

        /// <summary>
        /// Lookup table for special functions (procs) for mobs.
        /// </summary>
        static public MobSpecial[] MobSpecialTable = 
        {
            new MobSpecial( "spec_breath_any",        SpecBreathAny         ),
            new MobSpecial( "spec_breath_acid",       SpecBreathAcid        ),
            new MobSpecial( "spec_breath_fire",       SpecBreathFire        ),
            new MobSpecial( "spec_breath_frost",      SpecBreathFrost       ),
            new MobSpecial( "spec_breath_gas",        SpecBreathGas         ),
            new MobSpecial( "spec_breath_shadow",     SpecBreathShadow      ),
            new MobSpecial( "spec_breath_water",      SpecBreathWater       ),
            new MobSpecial( "spec_breath_lightning",  SpecBreathLightning   ),
            new MobSpecial( "spec_cast_adept",        SpecCastAdept         ),
            new MobSpecial( "spec_cast_cleric",       SpecCastCleric        ),
            new MobSpecial( "spec_cast_druid",        SpecCastDruid         ),
            new MobSpecial( "spec_cast_shaman",       SpecCastShaman        ),
            new MobSpecial( "spec_cast_ghost",        SpecCastGhost         ),
            new MobSpecial( "spec_cast_judge",        SpecCastJudge         ),
            new MobSpecial( "spec_cast_air_ele",      SpecCastAirEle       ),
            new MobSpecial( "spec_cast_earth_ele",    SpecCastEarthEle     ),
            new MobSpecial( "spec_cast_fire_ele",     SpecCastFireEle      ),
            new MobSpecial( "spec_cast_water_ele",    SpecCastWaterEle     ),
            new MobSpecial( "spec_cast_ranger",       SpecCastRanger        ),
            new MobSpecial( "spec_cast_hunter",       SpecCastHunter        ),
            new MobSpecial( "spec_cast_paladin",      SpecCastPaladin       ),
            new MobSpecial( "spec_cast_antipaladin",  SpecCastAntipaladin   ),
            new MobSpecial( "spec_cast_illusionist",  SpecCastIllusionist   ),
            new MobSpecial( "spec_cast_conjurer",     SpecCastConjurer      ),
            new MobSpecial( "spec_cast_necromancer",  SpecCastNecromancer   ),
            new MobSpecial( "spec_cast_sorcerer",     SpecCastSorcerer      ),
            new MobSpecial( "spec_cast_psionicist",   SpecCastPsionicist    ),
            new MobSpecial( "spec_cast_vampire",      SpecCastVampire       ),
            new MobSpecial( "spec_cast_undead",       SpecCastUndead        ),
            new MobSpecial( "spec_executioner",       SpecExecutioner        ),
            new MobSpecial( "spec_fido",              SpecFido               ),
            new MobSpecial( "spec_guard",             SpecGuard              ),
            new MobSpecial( "spec_janitor",           SpecJanitor            ),
            new MobSpecial( "spec_poison",            SpecPoison             ),
            new MobSpecial( "spec_repairman",         SpecRepairman          ),
            new MobSpecial( "spec_thief",             SpecThief              ),
            new MobSpecial( "spec_assassin",          SpecAssassin           ),
            new MobSpecial( "spec_mercenary",         SpecMercenary          ),
            new MobSpecial( "spec_warrior",           SpecWarrior            ),
            new MobSpecial( "spec_earth_elemental",   SpecEarthElemental    ),
            new MobSpecial( "spec_air_elemental",     SpecAirElemental      ),
            new MobSpecial( "spec_fire_elemental",    SpecFireElemental     ),
            new MobSpecial( "spec_water_elemental",   SpecWaterElemental    ),
            new MobSpecial( "spec_monk",              SpecMonk               ),
            new MobSpecial( "spec_bard",              SpecBard               ),
            new MobSpecial( "spec_justice_guard",     SpecJusticeGuard      ),
            new MobSpecial( "spec_justice_tracker",   SpecJusticeTracker    ),
            new MobSpecial( "spec_lost_girl",         SpecLostGirl          ),
            new MobSpecial( "spec_grumbar_shout",     SpecGrumbarShout      ),
            new MobSpecial( "spec_xorn",              SpecXorn               ),
            new MobSpecial( "spec_lil_critter",       SpecLilCritter        )
        };
    };
}