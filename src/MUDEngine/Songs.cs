using System;
using ModernMUD;

namespace MUDEngine
{
    /// <summary>
    /// Bard song spell functions.
    /// </summary>
    public partial class SpellFun
    {
        /// <summary>
        /// Song of armor.
        /// </summary>
        /// <param name="ch"></param>
        /// <param name="spell"></param>
        /// <param name="level"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public static bool SongArmor( CharData ch, Spell spell, int level, Target target )
        {
            ch.SendText( "You Sing a song of protection.\r\n" );

            foreach( CharData victim in ch._inRoom.People )
            {
                if (victim.IsAffected( Affect.AFFECT_ARMOR))
                    continue;

                Affect af = new Affect( Affect.AffectType.song, spell.Name, 4, Affect.Apply.ac, ( 0 - ( 10 + ( level / 5 ) ) ), Affect.AFFECT_ARMOR );
                victim.CombineAffect( af );

                victim.SendText( "You feel someone protecting you.\r\n" );
            }

            return true;
        }

        /// <summary>
        /// Song of flight.
        /// </summary>
        /// <param name="ch"></param>
        /// <param name="spell"></param>
        /// <param name="level"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public static bool SongFlight( CharData ch, Spell spell, int level, Target target )
        {
            foreach( CharData victim in ch._inRoom.People )
            {
                if (victim.IsAffected(Affect.AFFECT_FLYING))
                    return true;

                Affect af = new Affect(Affect.AffectType.song, spell.Name, (level / 6), Affect.Apply.none, 0, Affect.AFFECT_FLYING);
                victim.AddAffect(af);

                victim.SendText( "&+WYour feet rise off the ground.&n\r\n" );
                SocketConnection.Act( "$n&n rises off the ground.", victim, null, null, SocketConnection.MessageTarget.room );
            }
            return true;
        }

        /// <summary>
        /// Song of babble.
        /// </summary>
        /// <param name="ch"></param>
        /// <param name="spell"></param>
        /// <param name="level"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public static bool SongBabble( CharData ch, Spell spell, int level, Target target )
        {
            foreach( CharData victim in ch._inRoom.People )
            {
            }
            return true;
        }

        /// <summary>
        /// Song of calming.
        /// </summary>
        /// <param name="ch"></param>
        /// <param name="spell"></param>
        /// <param name="level"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public static bool SongCalming( CharData ch, Spell spell, int level, Target target )
        {
            foreach( CharData victim in ch._inRoom.People )
            {
                Combat.StopFighting( victim, false );
                victim.WaitState( 2 );
            }
            return true;
        }

        /// <summary>
        /// Song of chaos.
        /// </summary>
        /// <param name="ch"></param>
        /// <param name="spell"></param>
        /// <param name="level"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public static bool SongChaos( CharData ch, Spell spell, int level, Target target )
        {
            foreach( CharData victim in ch._inRoom.People )
            {
            }
            return true;
        }

        /// <summary>
        /// Song of clumsiness.
        /// </summary>
        /// <param name="ch"></param>
        /// <param name="spell"></param>
        /// <param name="level"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public static bool SongClumsiness( CharData ch, Spell spell, int level, Target target )
        {
            foreach( CharData victim in ch._inRoom.People )
            {
            }
            return true;
        }

        /// <summary>
        /// Song of corruption.
        /// </summary>
        /// <param name="ch"></param>
        /// <param name="spell"></param>
        /// <param name="level"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public static bool SongCorruption( CharData ch, Spell spell, int level, Target target )
        {
            foreach( CharData victim in ch._inRoom.People )
            {
            }
            return true;
        }

        /// <summary>
        /// Song of cowardice.
        /// </summary>
        /// <param name="ch"></param>
        /// <param name="spell"></param>
        /// <param name="level"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public static bool SongCowardice( CharData ch, Spell spell, int level, Target target )
        {
            foreach( CharData victim in ch._inRoom.People )
            {
                if( !victim.IsSameGroup( ch ) )
                {
                    Affect af = new Affect(Affect.AffectType.song, spell.Name, (level / 6 + 1), Affect.Apply.hitroll, (0 - (level / 3)), Affect.AFFECT_COWARDLY);
                    victim.AddAffect(af);

                    SocketConnection.Act( "$n&n looks unsure of $mself.", victim, null, null, SocketConnection.MessageTarget.room );
                    victim.SendText( "You feel less confident about your battle skills.\r\n" );
                }
            }
            return true;
        }

        /// <summary>
        /// Song of dragons.
        /// </summary>
        /// <param name="ch"></param>
        /// <param name="spell"></param>
        /// <param name="level"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public static bool SongDragons( CharData ch, Spell spell, int level, Target target )
        {
            foreach( CharData victim in ch._inRoom.People )
            {
            }
            return true;
        }

        /// <summary>
        /// Song of feasting.
        /// </summary>
        /// <param name="ch"></param>
        /// <param name="spell"></param>
        /// <param name="level"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public static bool SongFeasting( CharData ch, Spell spell, int level, Target target )
        {
            foreach( CharData victim in ch._inRoom.People )
            {
                if( victim.IsNPC() )
                    continue;

                int amount = level / 7 + 1;

                if( ( (PC)victim ).Hunger < 48 )
                    ( (PC)victim ).Hunger += amount;
                if( ( (PC)victim ).Thirst < 48 )
                    ( (PC)victim ).Thirst += amount;

                victim.SendText( "You stomach feels fuller.\r\n" );
            }
            return true;
        }

        /// <summary>
        /// Song of forgetfulness.
        /// </summary>
        /// <param name="ch"></param>
        /// <param name="spell"></param>
        /// <param name="level"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public static bool SongForgetfulness( CharData ch, Spell spell, int level, Target target )
        {
            foreach( CharData victim in ch._inRoom.People )
            {
            }
            return true;
        }

        /// <summary>
        /// Song of harming (damage).
        /// </summary>
        /// <param name="ch"></param>
        /// <param name="spell"></param>
        /// <param name="level"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public static bool SongHarming( CharData ch, Spell spell, int level, Target target )
        {
            foreach( CharData victim in ch._inRoom.People )
            {
            }
            return true;
        }

        /// <summary>
        /// Song of healing.
        /// </summary>
        /// <param name="ch"></param>
        /// <param name="spell"></param>
        /// <param name="level"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public static bool SongHealing( CharData ch, Spell spell, int level, Target target )
        {
            foreach( CharData victim in ch._inRoom.People )
            {
                if( !ch.IsSameGroup( victim ) )
                    continue;

                int heal = MUDMath.Dice( 4, ( level / 3 ) ) + 1;

                if( victim._hitpoints < victim.GetMaxHit() )
                    victim._hitpoints = Math.Min( victim._hitpoints + heal, victim.GetMaxHit() );
                victim.UpdatePosition();

                victim.SendText( "&+WYour wounds begin to heal.&n\r\n" );
            }
            return true;
        }

        /// <summary>
        /// Song of heroism.
        /// </summary>
        /// <param name="ch"></param>
        /// <param name="spell"></param>
        /// <param name="level"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public static bool SongHeroism( CharData ch, Spell spell, int level, Target target )
        {
            foreach( CharData victim in ch._inRoom.People )
            {
                Affect af = new Affect(Affect.AffectType.song, spell.Name, (level / 8), Affect.Apply.hitroll, (level / 6 + 1), Affect.AFFECT_NONE);
                victim.AddAffect(af);
                af = new Affect(Affect.AffectType.song, spell.Name, (level / 8), Affect.Apply.damroll, (level / 11 + 1), Affect.AFFECT_NONE);
                victim.AddAffect(af);

                SocketConnection.Act( "$n&n looks more courageous.", victim, null, null, SocketConnection.MessageTarget.room );
                victim.SendText( "You feel righteous.\r\n" );
            }
            return true;
        }

        /// <summary>
        /// Song of invisibility.
        /// </summary>
        /// <param name="ch"></param>
        /// <param name="spell"></param>
        /// <param name="level"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public static bool SongInvisibility( CharData ch, Spell spell, int level, Target target )
        {
            int total = 0;

            int max = level / 8;

            foreach( CharData victim in ch._inRoom.People )
            {
                if( !victim.IsSameGroup( ch ) || victim.IsAffected( Affect.AFFECT_INVISIBLE ) )
                    continue;

                if( total >= max )
                    return true;

                victim.SendText( "You slowly fade out of existence.\r\n" );
                SocketConnection.Act( "$n&n slowly fades out of existence.", victim, null, null, SocketConnection.MessageTarget.room );

                Affect af = new Affect(Affect.AffectType.song, spell.Name, (level / 6), Affect.Apply.none, 0, Affect.AFFECT_INVISIBLE);
                victim.AddAffect(af);
                total++;
            }
            foreach( Object obj in ch._inRoom.Contents )
            {
                if( obj.HasFlag( ObjTemplate.ITEM_INVIS ) )
                    continue;

                if( total >= max )
                    return true;

                SocketConnection.Act( "&+L$p&+L fades away.", ch, obj, null, SocketConnection.MessageTarget.room );
                SocketConnection.Act( "&+L$p&+L fades away.", ch, obj, null, SocketConnection.MessageTarget.character );
                obj.AddFlag( ObjTemplate.ITEM_INVIS );
                total++;
            }
            return true;
        }

        /// <summary>
        /// Song of idiocy.
        /// </summary>
        /// <param name="ch"></param>
        /// <param name="spell"></param>
        /// <param name="level"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public static bool SongIdiocy( CharData ch, Spell spell, int level, Target target )
        {
            Affect af = new Affect();

            foreach( CharData victim in ch._inRoom.People )
            {
                if (victim.IsAffected(Affect.AFFECT_FEEBLEMIND)
                        || Magic.SpellSavingThrow( level, victim, AttackType.DamageType.black_magic ) )
                {
                    ch.SendText( "You failed!\r\n" );
                    continue;
                }

                af.Type = Affect.AffectType.song;
                af.Value = spell.Name;
                af.Duration = level / 9;
                af.AddModifier(Affect.Apply.intelligence, 0 - (level + 15));
                af.SetBitvector(Affect.AFFECT_FEEBLEMIND);
                victim.AddAffect(af);

                SocketConnection.Act( "A dumb look crosses $n&n's face and $e starts to drool.", victim, null, null, SocketConnection.MessageTarget.room );
                victim.SendText( "You feel _REALLY_ dumb.\r\n" );
            }
            return true;
        }

        /// <summary>
        /// Song of nightmares.
        /// </summary>
        /// <param name="ch"></param>
        /// <param name="spell"></param>
        /// <param name="level"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public static bool SongNightmares( CharData ch, Spell spell, int level, Target target )
        {
            Affect af = new Affect();

            foreach( CharData victim in ch._inRoom.People )
            {
                if (victim.IsAffected(Affect.AFFECT_FEAR) || Magic.SpellSavingThrow(level, victim,
                        AttackType.DamageType.black_magic ) )
                {
                    ch.SendText( "You have failed.\r\n" );
                    ch.SendText( "You resist the urge to panic.\r\n" );
                    continue;
                }

                af.Type = Affect.AffectType.song;
                af.Value = spell.Name;
                af.Duration = 1 + ( level / 7 );
                af.SetBitvector( Affect.AFFECT_FEAR );
                victim.AddAffect(af);

                SocketConnection.Act( "$N&n is scared!", ch, null, victim, SocketConnection.MessageTarget.character );
                victim.SendText( "You are scared!\r\n" );
                SocketConnection.Act( "$N&n is scared!", ch, null, victim, SocketConnection.MessageTarget.everyone_but_victim );

                CommandType.Interpret( victim, "flee" );
                if( victim.IsNPC() )
                    Combat.StartFearing( victim, ch );
            }
            return true;
        }

        /// <summary>
        /// Song of obscrurement.
        /// </summary>
        /// <param name="ch"></param>
        /// <param name="spell"></param>
        /// <param name="level"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public static bool SongObscurement( CharData ch, Spell spell, int level, Target target )
        {
            Affect af = new Affect();

            foreach( CharData victim in ch._inRoom.People )
            {
                if( victim.IsAffected( Affect.AFFECT_MINOR_INVIS ) || victim.IsAffected( Affect.AFFECT_INVISIBLE ) )
                    return true;

                SocketConnection.Act( "$n&n fades out of existence.", ch, null, null, SocketConnection.MessageTarget.room );
                ch.SendText( "You vanish.\r\n" );

                af.Type = Affect.AffectType.song;
                af.Value = spell.Name;
                af.Duration = level / 6;
                af.SetBitvector( Affect.AFFECT_MINOR_INVIS );
                victim.AddAffect(af);
            }
            return true;
        }

        /// <summary>
        /// Song of purity.
        /// </summary>
        /// <param name="ch"></param>
        /// <param name="spell"></param>
        /// <param name="level"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public static bool SongPurity( CharData ch, Spell spell, int level, Target target )
        {
            foreach( CharData victim in ch._inRoom.People )
            {
            }
            return true;
        }

        /// <summary>
        /// Song of quagmire (move drain).
        /// </summary>
        /// <param name="ch"></param>
        /// <param name="spell"></param>
        /// <param name="level"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public static bool SongQuagmire( CharData ch, Spell spell, int level, Target target )
        {
            foreach( CharData victim in ch._inRoom.People )
            {
                if( victim._currentMoves < 0 )
                    continue;

                victim._currentMoves -= MUDMath.Dice( 2, ( level / 2 ) ) + 5;

                if( victim._currentMoves < 0 )
                    victim._currentMoves = 0;

                victim.SendText( "Your feet feel mired to the ground.\r\n" );
            }
            return true;
        }

        /// <summary>
        /// Song of revelation.
        /// </summary>
        /// <param name="ch"></param>
        /// <param name="spell"></param>
        /// <param name="level"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public static bool SongRevelation( CharData ch, Spell spell, int level, Target target )
        {
            foreach( CharData victim in ch._inRoom.People )
            {
                victim.AffectStrip( Affect.AffectType.skill, "shadow form");
                victim.RemoveAffect(Affect.AFFECT_HIDE);
                victim.RemoveAffect(Affect.AFFECT_INVISIBLE);
                SocketConnection.Act( "$n&n is revealed!", victim, null, null, SocketConnection.MessageTarget.room );
                victim.SendText( "You are revealed!\r\n" );
            }
            return true;
        }

        /// <summary>
        /// Song of the skylark.
        /// </summary>
        /// <param name="ch"></param>
        /// <param name="spell"></param>
        /// <param name="level"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public static bool SongSkylark( CharData ch, Spell spell, int level, Target target )
        {
            foreach( CharData victim in ch._inRoom.People )
            {
            }
            return true;
        }

        /// <summary>
        /// Song of sleep.
        /// </summary>
        /// <param name="ch"></param>
        /// <param name="spell"></param>
        /// <param name="level"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public static bool SongSleep( CharData ch, Spell spell, int level, Target target )
        {
            foreach( CharData victim in ch._inRoom.People )
            {
                if (victim.IsAffected(Affect.AFFECT_SLEEP)
                        || Magic.SpellSavingThrow( level, victim, AttackType.DamageType.charm )
                        || victim.GetRace() == Race.RACE_VAMPIRE
                        || ch.IsSameGroup( victim ) )
                {
                    continue;
                }

                Affect af = new Affect(Affect.AffectType.song, spell.Name, level / 8, Affect.Apply.none, 0, Affect.AFFECT_SLEEP);
                victim.CombineAffect(af);

                if( victim.IsAwake() )
                {
                    victim.SendText( "You feel very sleepy ..... zzzzzz.\r\n" );
                    if( ch._fighting || victim._position == Position.fighting )
                        Combat.StopFighting( victim, false );
                    CommandType.Interpret( victim, "sleep" );
                }

            }

            return true;
        }

        /// <summary>
        /// Song of slowness.
        /// </summary>
        /// <param name="ch"></param>
        /// <param name="spell"></param>
        /// <param name="level"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public static bool SongSlowness( CharData ch, Spell spell, int level, Target target )
        {
            Affect af = new Affect();

            foreach( CharData victim in ch._inRoom.People )
            {
                if( Magic.SpellSavingThrow( level, victim, AttackType.DamageType.magic_other ) )
                {
                    ch.SendText( "You failed!\r\n" );
                    continue;
                }

                // Removes haste, takes two castings to make a hasted person slowed
                if (victim.IsAffected( Affect.AFFECT_HASTE))
                {
                    victim.RemoveAffect(Affect.AFFECT_HASTE);
                    victim.SendText( "You slow to your normal speed.\r\n" );
                    continue;
                }

                if (victim.IsAffected(Affect.AFFECT_SLOWNESS))
                    continue;

                af.Type = Affect.AffectType.song;
                af.Value = spell.Name;
                af.Duration = 6;
                af.SetBitvector( Affect.AFFECT_SLOWNESS );
                victim.AddAffect(af);

                SocketConnection.Act( "&+R$n&+R moves much more slowly.&n", victim, null, null, SocketConnection.MessageTarget.room );
                victim.SendText( "&+RYou feel yourself slowing down.&n\r\n" );
            }
            return true;
        }

        /// <summary>
        /// Song of susceptibility.
        /// </summary>
        /// <param name="ch"></param>
        /// <param name="spell"></param>
        /// <param name="level"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public static bool SongSusceptibility( CharData ch, Spell spell, int level, Target target )
        {
            Affect af = new Affect();

            foreach( CharData victim in ch._inRoom.People )
            {
                af.Type = Affect.AffectType.song;
                af.Value = spell.Name;
                af.Duration = level / 7;
                af.AddModifier(Affect.Apply.save_spell, (level / 5) + 1);
                af.AddModifier(Affect.Apply.save_paralysis, (level / 5) + 1);
                af.AddModifier(Affect.Apply.save_petrification, (level / 5) + 1);
                af.AddModifier(Affect.Apply.save_poison, (level / 5) + 1);
                af.AddModifier(Affect.Apply.save_breath, (level / 5) + 1);
                af.SetBitvector( Affect.AFFECT_NONE );
                victim.AddAffect(af);
            }
            return true;
        }

        /// <summary>
        /// Song of warding, protective.
        /// </summary>
        /// <param name="ch"></param>
        /// <param name="spell"></param>
        /// <param name="level"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public static bool SongWarding( CharData ch, Spell spell, int level, Target target )
        {
            Affect af = new Affect();

            foreach( CharData victim in ch._inRoom.People )
            {
                af.Type = Affect.AffectType.song;
                af.Value = spell.Name;
                af.Duration = level / 7;
                af.AddModifier(Affect.Apply.save_spell, 0 - ( ( level / 5 ) + 1 ));
                af.AddModifier(Affect.Apply.save_paralysis, 0 - ((level / 5) + 1));
                af.AddModifier(Affect.Apply.save_petrification, 0 - ((level / 5) + 1));
                af.AddModifier(Affect.Apply.save_poison, 0 - ((level / 5) + 1));
                af.AddModifier(Affect.Apply.save_breath, 0 - ((level / 5) + 1));
                af.SetBitvector(Affect.AFFECT_NONE);
                victim.AddAffect(af);
            }
            return true;
        }

        /// <summary>
        /// Song of weakness.
        /// </summary>
        /// <param name="ch"></param>
        /// <param name="spell"></param>
        /// <param name="level"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public static bool SongWeakling( CharData ch, Spell spell, int level, Target target )
        {
            Affect af = new Affect();

            foreach( CharData victim in ch._inRoom.People )
            {
                if (victim.IsAffected( Affect.AFFECT_STRENGTH_REDUCED)
                        || Magic.SpellSavingThrow( level, victim, AttackType.DamageType.black_magic ) )
                    continue;

                af.Type = Affect.AffectType.song;
                af.Value = spell.Name;
                af.Duration = level / 7;
                af.AddModifier(Affect.Apply.strength, -( level / 2 ));
                af.SetBitvector(Affect.AFFECT_STRENGTH_REDUCED);

                if( level > 25 )
                {
                    af.AddModifier( Affect.Apply.damroll, 0 - ( level / 7 ));
                }

                victim.AddAffect(af);

                SocketConnection.Act( "$n&n looks weaker.", victim, null, null, SocketConnection.MessageTarget.room );
                victim.SendText( "You feel weaker.\r\n" );
            }
            return true;
        }
    };

}