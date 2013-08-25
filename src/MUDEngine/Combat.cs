using System;
using System.Collections.Generic;
using ModernMUD;

namespace MUDEngine
{
    /// <summary>
    /// The Combat class contains violence-related utility functions that can be accessed from anywhere
    /// in the engine.
    /// </summary>
    public class Combat
    {
        /// <summary>
        /// The backstab function was cloned from Command.Backstab for use by mobile AI code.
        /// the difference is that a mob that wants to backstab already knows their
        /// victim, so no need for the argument stuff
        ///
        /// Modified by to allow auto-stab for backstabbers with aggr_level set.  Called
        /// from Commandbackstab()
        /// 
        /// Added ability to stab if piercer is secondary. Useful for mercs.
        ///
        /// Reduced damage for bards and mercs.
        /// </summary>
        /// <param name="ch"></param>
        /// <param name="victim"></param>
        /// <returns></returns>
        public static bool Backstab(CharData ch, CharData victim)
        {
            Object obj;
            Object obj2;
            int chance = 0;
            int stabChance;

            // Can't stab if lacking the skill

            if (!ch.HasSkill("backstab"))
            {
                ch.SendText("Leave backstabbing to the assassins!\r\n");
                return false;
            }

            if (victim == null)
            {
                ch.SendText("Pick a target!\r\n");
                return false;
            }

            // Can't stab if blind
            if (ch.IsBlind())
                return false;

            // Can't stab on horseback
            if (ch._riding)
            {
                ch.SendText("You can't get close enough while mounted.\r\n");
                return false;
            }

            // Can't stab yourself
            if (victim == ch)
                return false;

            victim = CheckGuarding(ch, victim);

            if (IsSafe(ch, victim))
                return false;
            // is_safe could wipe out victim, as it calls procs if a boss
            // check and see that victim is still valid
            if (!victim)
                return false;

            /* Check size of ch vs. victim. */
            if (victim._position > Position.sleeping)
            {
                /* If ch is more than 2 sizes smaller it's too small. */
                if ((ch._size - 2) > victim._size && victim._position >= Position.stunned
                        && !(ch.IsNPC() && ch._mobTemplate.IndexNumber == 10165))
                {
                    ch.SendText("Such tiny beings evade your skills.\r\n");
                    return false;
                }
                /* Ch 2 or more sizes larger than victim => bad! */
                if ((ch._size + 2) < victim._size && victim._position >= Position.stunned)
                {
                    ch.SendText("It is rather ineffective to stab someone in the calf.\r\n");
                    return false;
                }
            }

            ObjTemplate.WearLocation hand = 0;
            if ((obj = Object.GetEquipmentOnCharacter(ch, ObjTemplate.WearLocation.hand_one)) && obj.ItemType == ObjTemplate.ObjectType.weapon
                    && (AttackType.Table[obj.Values[3]].SkillName) == "1h piercing")
                hand = ObjTemplate.WearLocation.hand_one;
            if (hand == 0 && (obj2 = Object.GetEquipmentOnCharacter(ch, ObjTemplate.WearLocation.hand_two))
                    && obj2.ItemType == ObjTemplate.ObjectType.weapon && (AttackType.Table[obj2.Values[3]].SkillName) == "1h piercing")
                hand = ObjTemplate.WearLocation.hand_two;
            if (hand == 0)
            {
                ch.SendText("You need to wield a piercing weapon.\r\n");
                return false;
            }

            if (!ch.IsNPC())
                ch.PracticeSkill("backstab");

            Crime.CheckAttemptedMurder(ch, victim);

            ch.WaitState(Skill.SkillList["backstab"].Delay);
            if (ch.IsNPC())
            {
                stabChance = 2 * ch._level;
            }
            else
            {
                stabChance = ((PC)ch).SkillAptitude["backstab"];
            }
            if (Math.Abs(ch._size - victim._size) == 2)
                stabChance -= 10;
            switch (victim._position)
            {
                default:
                    break;
                case Position.mortally_wounded:
                case Position.incapacitated:
                case Position.unconscious:
                    stabChance += 80;
                    break;
                case Position.stunned:
                case Position.sleeping:
                    stabChance += 40;
                    break;
                case Position.reclining:
                    stabChance += 20;
                    break;
                case Position.resting:
                case Position.sitting:
                    stabChance += 10;
                    break;
            } //end switch
            // Half as likely to succeed on those that are aware
            if (victim.IsAffected(Affect.AFFECT_AWARE) || victim.IsAffected( Affect.AFFECT_SKL_AWARE))
            {
                chance /= 2;
            }
            string lbuf = String.Format("Commandbackstab: {0} is attempting with a {0}%% chance.", ch._name, stabChance);
            ImmortalChat.SendImmortalChat(null, ImmortalChat.IMMTALK_SPAM, 0, lbuf);
            if (MUDMath.NumberPercent() < stabChance)
            {
                /* First hit on backstab.  Check for instant kill. - Xangis */
                if (ch.HasSkill("instant kill"))
                {
                    if (!ch.IsNPC())
                        chance = ((PC)ch).SkillAptitude["instant kill"];
                    else
                        chance = (ch._level * 3) / 2 + 20;

                    // People over level 50 get a bonus, equates to about 1-2%
                    if (ch._level > 50)
                        chance += 25;

                    chance += (ch._level - victim._level);

                    // Immortals will get a bonus too
                    if (ch.IsImmortal())
                        chance *= 4;

                    // Half as likely to succeed on those that are aware
                    if (victim.IsAffected(Affect.AFFECT_AWARE) || victim.IsAffected(Affect.AFFECT_SKL_AWARE))
                    {
                        chance /= 2;
                    }

                    if (MUDMath.NumberRange(1, 20000) < chance)
                    {
                        if (!ch.IsNPC())
                            ch.PracticeSkill("instant kill");
                        {
                            lbuf = String.Format("backstab: {0} hit an instakill on {1} with a {2} chance in hundredths of a percent.",
                                      ch._name, victim._name, (chance / 30));
                            ImmortalChat.SendImmortalChat(null, ImmortalChat.IMMTALK_SPAM, 0, lbuf);
                            Log.Trace(lbuf);
                            ch.WaitState(15);
                            SingleAttack(ch, victim, "instant kill", hand);
                            return true;
                        }
                    }
                }

                SingleAttack(ch, victim, "backstab", hand);
                /* No double stabs when the first stab kills'm. */
                if (victim._position == Position.dead)
                    return true;

                /* Case of thieves/assassins doing a double backstab. */
                obj = Object.GetEquipmentOnCharacter(ch, ObjTemplate.WearLocation.hand_two);
                /* Stop 2nd-hand non-pierce backstabs. */
                if (!ch.IsNPC() && (obj != null) && (hand != ObjTemplate.WearLocation.hand_two)
                        && obj.ItemType == ObjTemplate.ObjectType.weapon
                        && (AttackType.Table[obj.Values[3]].SkillName) == "1h piercing")
                {
                    /* Thieves get 1/2 chance at double, assassins get 2/3. */
                    // Xangis - removed double stab for thieves 6-9-00
                    if ((ch.IsClass(CharClass.Names.assassin)) && (MUDMath.NumberPercent() < ((PC)ch).SkillAptitude["backstab"] * 2 / 3))
                    {
                        lbuf = String.Format("backstab: {0} hit a double backstab.", ch._name);
                        ImmortalChat.SendImmortalChat(null, ImmortalChat.IMMTALK_SPAM, 0, lbuf);
                        SingleAttack(ch, victim, "backstab", ObjTemplate.WearLocation.hand_two);
                    }
                }
            }
            else    /* Send a "you miss your backstab" messge & engage. */
            {
                InflictDamage(ch, victim, 0, "backstab", ObjTemplate.WearLocation.hand_one, AttackType.DamageType.pierce);
            }
            return true;
        }

        /// <summary>
        /// Do one round of attacks for one character.
        /// Note: This is a round, not a single attack!
        /// </summary>
        /// <param name="ch"></param>
        /// <param name="victim"></param>
        /// <param name="skill"></param>
        /// <returns></returns>
        public static bool CombatRound(CharData ch, CharData victim, string skill)
        {
            Object wield;
            int chance = 0;

            if( ch == null )
            {
                return false;
            }

            if( victim == null )
            {
                return false;
            }

            /* No casting/being para'd and hitting at the same time. */
            if( ( ch.IsAffected( Affect.AFFECT_CASTING ) ) || ch.IsAffected( Affect.AFFECT_MINOR_PARA )
                    || ch.IsAffected(Affect.AFFECT_HOLD))
            {
                return false;
            }

            /* I don't know how a dead person can hit someone/be hit. */
            if( victim._position == Position.dead || victim._hitpoints < -10 )
            {
                StopFighting( victim, true );
                return true;
            }

            /*
            * Set the fighting fields now.
            */
            if( victim._position > Position.stunned )
            {
                if( !victim._fighting )
                    SetFighting( victim, ch );
                // Can't have bashed/prone people just automatically be standing.
                if( victim._position == Position.standing )
                    victim._position = Position.fighting;
            }

            // HORRIBLE HORRIBLE! We've got index numbers hard-coded.  TODO: FIXME: BUG: Get rid of this!
            if( ch.IsNPC() && ch._mobTemplate != null && ( ch._mobTemplate.IndexNumber == 9316 ||
                   ch._mobTemplate.IndexNumber == 9748 ) && MUDMath.NumberPercent() < 20 )
            {
                CheckShout( ch, victim );
            }
            ch.BreakInvis();

            // Everyone gets at least one swing/breath in battle.
            // This handles breathing, roaring, etc.
            if (!CheckRaceSpecial(ch, victim, skill))
                SingleAttack(ch, victim, skill, ObjTemplate.WearLocation.hand_one);

            // Thrikreen primary hand extra attack, only thri extra attack that is
            // given to non-warriors in addition to warriors
            if( ch.GetRace() == Race.RACE_THRIKREEN && MUDMath.NumberPercent() < ch._level )
            {
                if( ch.IsClass(CharClass.Names.warrior) )
                {
                    switch( MUDMath.NumberRange( 1, 4 ) )
                    {
                        case 1:
                            if( Object.GetEquipmentOnCharacter( ch, ObjTemplate.WearLocation.hand_one ) )
                                SingleAttack(ch, victim, skill, ObjTemplate.WearLocation.hand_one);
                            break;
                        case 2:
                            if( Object.GetEquipmentOnCharacter( ch, ObjTemplate.WearLocation.hand_two ) )
                                SingleAttack(ch, victim, skill, ObjTemplate.WearLocation.hand_two);
                            break;
                        case 3:
                            if( Object.GetEquipmentOnCharacter( ch, ObjTemplate.WearLocation.hand_three ) )
                                SingleAttack(ch, victim, skill, ObjTemplate.WearLocation.hand_three);
                            break;
                        case 4:
                            if( Object.GetEquipmentOnCharacter( ch, ObjTemplate.WearLocation.hand_four ) )
                                SingleAttack(ch, victim, skill, ObjTemplate.WearLocation.hand_four);
                            break;
                    }
                }
                else
                {
                    SingleAttack(ch, victim, skill, ObjTemplate.WearLocation.hand_one);
                    if( MUDMath.NumberPercent() < ch._level / 2 )
                    {
                        SingleAttack(ch, victim, skill, ObjTemplate.WearLocation.hand_one);
                    }
                }
            }

            // Don't hurt a corpse.
            if( victim._position == Position.dead || victim._hitpoints < -10 )
            {
                StopFighting( ch, false );
                {
                    return true;
                }
            }

            // For NPCs we assume they have max skill value for their level.
            // When checking combat skills we only prDescriptor.actice them on a successful
            // check in order to make them go up slower.  If they go up too slow
            // we can always practice them before they check.

            chance = ch.GetAttackChance(2);
            if( MUDMath.NumberPercent() < chance )
            {
                ch.PracticeSkill( "second attack" );
                SingleAttack(ch, victim, skill, ObjTemplate.WearLocation.hand_one);
                if( ch._fighting != victim )
                {
                    return false;
                }
            }

            // Check for Thri-Kreen arm #3
            if( ch.GetRace() == Race.RACE_THRIKREEN && ( wield = Object.GetEquipmentOnCharacter( ch, ObjTemplate.WearLocation.hand_three ) ) )
            {
                if( wield.HasWearFlag( ObjTemplate.WEARABLE_WIELD ) )
                {
                    if( ch.IsNPC() )
                    {
                        if( !ch.HasSkill( "second attack" ))
                        {
                            chance = ch._level / 5;  // Up to 10% chance of third arm for psis and
                        }
                        // other miscellaneous thris
                        else
                        {
                            chance = ((ch._level - Skill.SkillList["second attack"].ClassAvailability[(int)ch._charClass.ClassNumber]) * 2 + 25);
                        }
                    }
                    else
                    {
                        if (((PC)ch).SkillAptitude.ContainsKey("second attack"))
                        {
                            chance = ((PC)ch).SkillAptitude["second attack"];
                        }
                        else
                        {
                            chance = 0;
                        }
                    }

                    if( chance > 95 )
                        chance = 95;

                    if( MUDMath.NumberPercent() < chance )
                    {
                        ch.PracticeSkill( "second attack" );
                        SingleAttack(ch, victim, skill, ObjTemplate.WearLocation.hand_three);
                        if( ch._fighting != victim )
                        {
                            return false;
                        }
                    }
                }
            }

            chance = ch.GetAttackChance(3);
            if( MUDMath.NumberPercent() < chance )
            {
                ch.PracticeSkill( "third attack" );
                SingleAttack(ch, victim, skill, ObjTemplate.WearLocation.hand_one);
                if( ch._fighting != victim )
                {
                    return false;
                }
            }

            chance = ch.GetAttackChance(4);
            if( MUDMath.NumberPercent() < chance )
            {
                ch.PracticeSkill( "fourth attack" );
                SingleAttack(ch, victim, skill, ObjTemplate.WearLocation.hand_one);
                if( ch._fighting != victim )
                {
                    return false;
                }
            }

            // Check for dual wield.  May want to allow a second swing when dual wielding.
            // We'll wait and see what combat looks like before we decide - Xangis
            wield = Object.GetEquipmentOnCharacter( ch, ObjTemplate.WearLocation.hand_two );
            if( wield )
            {
                if( wield.HasWearFlag( ObjTemplate.WEARABLE_WIELD ) )
                {
                    ch.PracticeSkill( "dual wield" );
                    if (ch.IsNPC())
                    {
                        chance = ch._level;
                    }
                    else
                    {
                        if (((PC)ch).SkillAptitude.ContainsKey("dual wield"))
                        {
                            chance = ((PC)ch).SkillAptitude["dual wield"] * 2 / 3;
                        }
                        else
                        {
                            chance = 0;
                        }
                    }
                    chance += ch.IsClass(CharClass.Names.ranger) ? 10 : 0;
                    if( MUDMath.NumberPercent() < chance )
                    {
                        SingleAttack(ch, victim, skill, ObjTemplate.WearLocation.hand_two);
                    }
                }
                if( ch._fighting != victim )
                {
                    return false;
                }
            }

            // Check for fourth arm on thrikreen
            if( ch.GetRace() == Race.RACE_THRIKREEN && ( wield = Object.GetEquipmentOnCharacter( ch, ObjTemplate.WearLocation.hand_four ) ) )
            {
                if( wield.HasWearFlag( ObjTemplate.WEARABLE_WIELD ) )
                {
                    ch.PracticeSkill( "dual wield" );
                    chance = ch.IsNPC() ? ( ch._level * 3 / 2 + 20 ) : ( (PC)ch ).SkillAptitude[ "dual wield" ];
                    if( chance > 95 )
                    {
                        chance = 95;
                    }
                    if( MUDMath.NumberPercent() < chance )
                    {
                        SingleAttack(ch, victim, skill, ObjTemplate.WearLocation.hand_four);
                    }
                }
                if( ch._fighting != victim )
                {
                    return false;
                }
            }

            // Don't hurt a corpse.
            if( victim._position == Position.dead || victim._hitpoints < -10 )
            {
                StopFighting( ch, false );
                return true;
            }

            return false;
        }

        /// <summary>
        /// Hit one guy once.
        ///
        /// Hitroll is now done on a 200-sided die rather than a 20-sided die
        /// This allows for more dynamic modifiers to hitroll.
        /// i.e. a couple extra points of strength and whatnot _may_ make the
        /// difference between a hit and a miss rather than incrementing something
        /// every 10-20 points of an ability we can modify it every 1-2 points.
        /// </summary>
        /// <param name="ch">attacker</param>
        /// <param name="victim">person being attacked</param>
        /// <param name="skill">damage type being used (skill)</param>
        /// <param name="weapon">wear location of weapon (usually primary or secondary hand)</param>
        /// <returns>true if victim is killed, otherwise false</returns>
        public static bool SingleAttack(CharData ch, CharData victim, string skill, ObjTemplate.WearLocation weapon)
        {
            string text;
            int dam;
            int chance;

            /*
            * Can't beat a dead char!
            * Guard against weird room-leavings.
            */
            if( victim._position == Position.dead || victim._hitpoints < -10 )
            {
                text = String.Format("SingleAttack: ch {0} fighting dead victim {1}.", ch._name, victim._name );
                Log.Error( text, 0 );
                ch._fighting = null;
                if( ch._position == Position.fighting )
                    ch._position = Position.standing;
                return true;
            }
            if( ch._inRoom != victim._inRoom )
            {
                text = String.Format("SingleAttack: ch {0} not with victim {1}.", ch._name, victim._name );
                Log.Error( text, 0 );
                ch._fighting = null;
                if( ch._position == Position.fighting )
                    ch._position = Position.standing;
                return false;
            }

            /* No casting/being para'd and hitting at the same time. */
            if ((ch.IsAffected(Affect.AFFECT_CASTING)) || ch.IsAffected(Affect.AFFECT_MINOR_PARA)
                    || ch.IsAffected(Affect.AFFECT_HOLD))
            {
                return false;
            }

            // Inertial barrier will prevent some attacks.  At the following levels a person
            // affected by inertial barrier will be able to avoid this percentage of attacks:
            // 1 = 7%  5 = 10%  10 = 13%  20 = 20%  30 = 27%  40 = 33%  50 = 39%  51 = 40%
            if (victim.IsAffected(Affect.AFFECT_INERTIAL_BARRIER) && MUDMath.NumberPercent() > (victim._level * 2 / 3 + 7))
                return false;

            // Keep in mind that CheckRiposte returns a boolean.
            if (skill != "kick" && skill != "backstab" && skill != "circle" && CheckRiposte( ch, victim ) )
            {
                SingleAttack( victim, ch, String.Empty, ObjTemplate.WearLocation.hand_one );
                return false;
            }
            if (CheckParry(ch, victim) && skill != "backstab" && skill != "circle")
                return false;
            if (CheckShieldBlock(ch, victim) && skill != "backstab" && skill != "circle")
                return false;
            if (CheckDodge(ch, victim) && skill != "backstab" && skill != "circle")
                return false;

            /*
            * Figure out the type of damage message if we don't have an associated attack skill.
            */
            Object wield = Object.GetEquipmentOnCharacter( ch, weapon );
            if (String.IsNullOrEmpty(skill))
            {
                skill = "barehanded fighting";
                if( wield && wield.ItemType == ObjTemplate.ObjectType.weapon )
                    skill = AttackType.Table[wield.Values[3]].SkillName;
            }

            /*
            * Weapon proficiencies.
            */
            string weaponGsn = "barehanded fighting";
            AttackType.DamageType damType = AttackType.DamageType.bludgeon;
            if( wield && wield.ItemType == ObjTemplate.ObjectType.weapon )
            {
                if( wield.Values[ 3 ] >= 0 && wield.Values[ 3 ] < AttackType.Table.Length )
                {
                    weaponGsn = (AttackType.Table[wield.Values[3]].SkillName);
                    damType = AttackType.Table[ wield.Values[ 3 ] ].DamageInflicted;
                }
                else
                {
                    text = String.Format( "SingleAttack: bad weapon damage type {0} caused by {1} ({2}).",
                              skill, wield.Name, wield.ObjIndexData ?
                              wield.ObjIndexData.IndexNumber : -1 );
                    Log.Error( text, 0 );
                    wield.Values[ 3 ] = 0;
                }
            }

            /*
            * Calculate to-hit-armor-class-0 versus armor.
            */
            int hitroll00 = ch._charClass.HitrollLevel0;
            int hitroll40 = ch._charClass.HitrollLevel40;

            /* Weapon-specific hitroll and damroll */

            int hitroll = MUDMath.Interpolate( ch._level, hitroll00, hitroll40 )
                          - ( ch.GetHitroll( weapon ) * 3 );
            int victimAC = Math.Max( -100, victim.GetAC() );

            // Added blindfighting skill - Xangis
            if( !CharData.CanSee( ch, victim ) )
            {
                if( ch.CheckSkill( "blindfighting" ) )
                {
                    victimAC -= 5;
                }
                else
                {
                    victimAC -= 40;
                }
            }

            /* Weapon proficiencies *
            *
            * The twohanded version of a weapon proficiency *MUST* follow the onehanded
            * version in the definitions.  This is stupid.
            */
            if( wield && wield.ItemType == ObjTemplate.ObjectType.weapon )
            {
                if( !wield.HasFlag( ObjTemplate.ITEM_TWOHANDED ) )
                {
                    if( ch.CheckSkill( weaponGsn ))
                    {
                        victimAC += 20;
                        ch.PracticeSkill( weaponGsn );
                    }
                }
                else
                {
                    // This is not going to work.
                    if (ch.CheckSkill(weaponGsn+1))
                    {
                        victimAC += 20;
                        ch.PracticeSkill(weaponGsn);
                    }
                }
            }
            else if( ch.CheckSkill("barehanded fighting"))
            {
                victimAC += 20;
            }

            /*
            * The moment of excitement!
            */
            int diceroll = MUDMath.NumberRange( 0, 199 );

            // Give them a small bonus if they can make a successful luck check.
            if( MUDMath.NumberPercent() <= ch.GetCurrLuck() )
                diceroll += 5;

            /* Made really lucky chars get saved by the godz. */
            if( diceroll == 0 || ( diceroll <= 196 && diceroll < hitroll - victimAC )
                     || ( MUDMath.NumberPercent() < victim.GetCurrLuck() / 40 ) )
            {
                /* Miss. */
                return InflictDamage(ch, victim, 0, skill, weapon, damType);
            }

            /*
            * Hit.
            * Calc damage.
            *
            * NPCs are more badass barehanded than players.  If they weren't
            * the game would be too damned easy since mobs almost never have
            * weapons.
            *
            * Increased mob damage by about 1/6 
            * It was previously level/2 to level*3/2 (25-75 at 50, average 50)
            * It is now level*3/5 to level*10/6      (30-87 at 50, average 59)
            *
            * Added the + ch.level - 1 'cause mobs still not hittin' hard enough
            */
            if( ch.IsNPC() )
            {
                dam = MUDMath.NumberRange( ( ch._level * 3 / 5 ), ( ch._level * 14 / 8 ) )
                      + ( ch._level - 1 );
                if (wield)
                {
                    dam += MUDMath.Dice(wield.Values[1], wield.Values[2]);
                }
                else if (ch.CheckSkill("unarmed damage"))
                {
                    dam += MUDMath.NumberRange(1, (ch.GetSkillChance("unarmed damage") / 12));
                }
            }
            else
            {
                if (wield)
                {
                    dam = MUDMath.Dice(wield.Values[1], wield.Values[2]);
                }
                else
                {
                    if (!ch.IsClass(CharClass.Names.monk))
                    {
                        dam = MUDMath.NumberRange(1, (2 + (int)ch._size / 3));
                        if (ch.CheckSkill("unarmed damage"))
                        {
                            dam += MUDMath.NumberRange(1, (ch.GetSkillChance("unarmed damage") / 12));
                        }
                    }
                    else
                    {
                        int min;
                        // monk barehanded damage - Xangis
                        ch.PracticeSkill("unarmed damage");
                        chance = ch.GetSkillChance("unarmed damage");
                        if (chance < 13)
                        {
                            min = 1;
                        }
                        else
                        {
                            min = chance / 13;
                        }
                        // at max skill for barehanded and unarmed, a monk will get
                        // a damage of 7-38, an average of 22.5 damage per hit before
                        // modifiers.  This is slightly better than a 6d6 weapon (average of 21 dmg)
                        // this is slightly worse than a 6d7 weapon (average of 24 dmg)
                        dam = MUDMath.NumberRange(min, ((chance / 3) + min));
                    }
                }
                if( ( wield && dam > 1000 ) && ch._level < Limits.LEVEL_AVATAR )
                {
                    text = String.Format( "SingleAttack damage range > 1000 from {0} to {1}",
                              wield.Values[ 1 ], wield.Values[ 2 ] );
                    Log.Error( text, 0 );
                }
            }

            /*
            * Played a character with an armor class of 126 (awful agility).
            * Wasn't getting pounded much at all.  Added a damage bonus applied
            * when the target's ac is worse than 100.
            *
            * This also means that someone who makes their weapon proficiency
            * check against someone with an ac of 81 or higher will also get a
            * damage bonus of 1% per ac point.
            *
            * This applies to mobs too, so if a mob has a terrible AC it will
            * get whacked harder.  I call this the "soft as a pudding" code.
            * 
            * This would also make AC debuffs stronger if they can make ac worse
            * than 100.
            */
            if( victimAC > 100 )
            {
                dam += ( (victimAC - 100) * dam) / 100;
            }

            /*
            * Bonuses.
            */
            dam += ch.GetDamroll( weapon );

            /* Weapon proficiencies, players only */
            /* Up to 50% increase based on weapon skill */
            if (wield && !ch.IsNPC())
            {
                dam += dam * ch.GetSkillChance(weaponGsn) / 180;
            }

            /* Up to 33% for offense skill */
            /* This means someone that has mastered a weapon and offense
            automatically does double damage in combat */
            chance = ch.GetSkillChance("offense");
            dam += dam * chance / 270;

            /* Bad idea to get caught napping in a fight */
            if( !victim.IsAwake() )
                dam *= 2;

            /* Backstab: 2 + one per 9 levels, 7x damage at 50 */
            if (skill == "backstab")
            {
                // Cap was previously too low.  It has been raised because a merc that was previously
                // stabbing for 180 now stabs for 64.  Assassins will still be able to stab for
                // 175 and mercs for 116 with this revised cap.  Keep in mind that a sorc can easily
                // fist for 250.
                int cap = 100 + 12 * ch._level;
                if( ch.IsClass(CharClass.Names.mercenary) || ch.IsClass(CharClass.Names.bard ))
                    cap = cap * 2 / 3;
                dam *= ( 2 + ( ch._level / 9 ) );
                /* damage cap applied here */
                dam = Math.Min( dam, cap );
            }
            else if (skill == "circle")              /* 150% to 200% at lev. 50 */
                dam += dam / 2 + ( dam * ch._level ) / 100;

            if( dam <= 0 )
                dam = 1;

            return InflictDamage(ch, victim, dam, skill, weapon, damType);
        }

        /// <summary>
        /// Inflict damage from a single hit.  This could use some cleanup since it's way too unwieldy at more than 600 lines.
        /// </summary>
        /// <param name="ch"></param>
        /// <param name="victim"></param>
        /// <param name="dam"></param>
        /// <param name="skill"></param>
        /// <param name="weapon"></param>
        /// <param name="damType"></param>
        /// <returns></returns>
        public static bool InflictDamage(CharData ch, CharData victim, int dam, string skill, ObjTemplate.WearLocation weapon, AttackType.DamageType damType)
        {
            if (ch == null || victim == null)
            {
                return false;
            }

            Object obj;
            bool critical = false;

            if( victim._position == Position.dead || victim._hitpoints < -10 )
            {
                return true;
            }

            /*
            * Stop up any residual loopholes.
            */
            if( ( dam > 1276 ) && ch._level < Limits.LEVEL_AVATAR )
            {
                string text;

                if (ch.IsNPC() && ch._socket)
                {
                    text = String.Format("Damage: {0} from {1} by {2}: > 1276 points with {3} damage type!",
                              dam, ch._name, ch._socket.Original._name, skill);
                }
                else
                {
                    text = String.Format("Damage: {0} from {1}: > 1276 points with {2} damage type!",
                              dam, ch.IsNPC() ? ch._shortDescription : ch._name, skill);
                }
                Log.Error( text, 0 );
                dam = 1276;
            }

            // Remove memorization and meditation bits - Xangis
            victim.BreakMed();
            victim.BreakMem();
            if (victim.IsAffected( Affect.AFFECT_MINOR_PARA))
            {
                SocketConnection.Act( "$n&n disrupts the magic preventing $N&n from moving.", ch, null, victim, SocketConnection.MessageTarget.room_vict );
                SocketConnection.Act( "You disrupt the magic preventing $N&n from moving.", ch, null, victim, SocketConnection.MessageTarget.character );
                SocketConnection.Act( "&+YYou can move again.&n", ch, null, victim, SocketConnection.MessageTarget.victim );
                victim.RemoveAffect( Affect.AFFECT_MINOR_PARA );
            }

            bool immune = false;
            if( victim != ch )
            {
                /*
                * Certain attacks are forbidden.
                * Most other attacks are returned.
                */
                victim = CheckGuarding( ch, victim );
                if( IsSafe( ch, victim ) )
                    return false;
                // is_safe could wipe out victim, as it calls procs if a boss
                // check and see that victim is still valid
                if( victim == null )
                    return true;
                Crime.CheckAttemptedMurder( ch, victim );
                if( victim._position > Position.stunned )
                {
                    if( !victim._fighting )
                        SetFighting( victim, ch );
                    // Can't have prone people automatically stand
                    if( victim._position == Position.standing )
                        victim._position = Position.fighting;

                    if( !ch._fighting )
                        SetFighting( ch, victim );

                    /*
                    * If NPC victim is following, ch might attack victim's master.
                    * No charm check here because charm would be dispelled from
                    * tanking mobile when combat ensues thus ensuring PC charmer is
                    * not harmed.
                    * Check for IsSameGroup wont work as following mobile is not
                    * always grouped with PC charmer
                    *
                    * Added a check for whether ch has switch skill.  If not,
                    * much lower chancing of retargetting
                    */
                    if( ch.IsNPC()
                            && victim.IsNPC()
                            && victim._master
                            && victim._master._inRoom == ch._inRoom
                            && MUDMath.NumberBits( 2 ) == 0 )
                    {
                        StartGrudge( ch, victim._master, false );
                    }
                }

                /*
                * More charm stuff.
                */
                if( victim._master == ch )
                {
                    StopFighting( victim, true );
                }

                ch.BreakInvis();

                /*
                * Hunting stuff...
                */
                if( dam != 0 && victim.IsNPC() )
                {
                    /* StartGrudge is combined StartHating and StartHunting */
                    StartGrudge( victim, ch, false );
                }

                /*
                * Damage modifiers.
                */

                // Critical hits for double damage
                // Average of 5% for those that have average luck
                // Gnomes could concievably have 10%
                if( MUDMath.NumberPercent() < ( 2 + ( ch.GetCurrLuck() / 18 ) ) && dam > 0 )
                {
                    ch.SendText( "&+WYou score a CRITICAL HIT!&n\r\n" );
                    dam *= 2;
                    critical = true;
                }

                if( victim.IsAffected( Affect.AFFECT_SANCTUARY ) )
                    dam /= 2;

                if( victim.IsAffected( Affect.AFFECT_PROTECT_EVIL ) && ch.IsEvil() )
                    dam -= dam / 8;
                else if( victim.IsAffected( Affect.AFFECT_PROTECT_GOOD ) && ch.IsGood() )
                    dam -= dam / 8;

                // Check stoneskin.  People not affected by a stoneskin affect
                // cannot lose their stoneskin for any reason.  This should mean
                // that mobs will keep their stoneskin and players should always
                // have a chance to lose it, since no player should ever be
                // setbit stoneskin.
                //
                // The bool value of found is used so that we can have them
                // take full damage when their stoneskin shatters, but get the
                // damage reduction if they are either a mob or their stoneskin
                // wears off that round.
                //
                /* Yeah, yeah.. so maybe backstabs shouldn't be aff'd. */
                // Actually they should be affected, but they should have a much
                // higher chance of getting through (say 30-70%).
                //
                // Critical hits will now go through stoneskin
                // automatically
                if (!critical && victim.IsAffected( Affect.AFFECT_STONESKIN) &&
                        ( skill != "backstab" || MUDMath.NumberPercent() < ( 25 + ch._level ) ) )
                {
                    bool found = false;
                    for (int i = (victim._affected.Count - 1); i >= 0; i--)
                    {
                        if( victim._affected[i].HasBitvector( Affect.AFFECT_STONESKIN ) )
                        {
                            // Small chance of shattering the stoneskin on a good hit.
                            // Reduced chance by about 20%
                            if( dam >= 25 && MUDMath.NumberPercent() <= ( dam / 12 ) )
                            {
                                victim.SendText( "&+LYour stoneskin is shattered by the massive blow!&n\r\n" );
                                SocketConnection.Act( "$n&n's massive blow shatters $N&n's stoneskin!", ch, null, victim, SocketConnection.MessageTarget.everyone_but_victim );
                                SocketConnection.Act( "Your massive blow shatters $N&n's stoneskin!", ch, null, victim, SocketConnection.MessageTarget.character );
                                victim.RemoveAffect(victim._affected[i]);
                                found = true;
                            }
                            else if( dam > 0 ) // Added check for actual damage
                            {
                                for( int j = 0; j < victim._affected[i].Modifiers.Count; j++ )
                                {
                                    victim._affected[i].Modifiers[j].Amount--;
                                    if (victim._affected[i].Modifiers[j].Amount < 1)
                                    {
                                        victim.RemoveAffect(victim._affected[i]);
                                        victim.SendText("&+LYou feel your skin soften and return to normal.&n\r\n");
                                    }
                                    dam /= 15;
                                    found = true;
                                }
                            }
                        }
                    }
                    // This means they're Affect.AFFECT_STONESKIN as an innate/permenant.
                    // We will still allow it to shatter, but it will refresh itself
                    // upon a mob update.  Because of this, we make it easier to shatter.
                    // No damage reduction when it shatters.
                    if( !found )
                    {
                        if( dam >= 8 && MUDMath.NumberPercent() <= ( dam / 8 ) )
                        {
                            victim.SendText( "&+LYour stoneskin is shattered by the massive blow!&n\r\n" );
                            SocketConnection.Act( "$n&n's massive blow shatters $N&n's stoneskin!", ch, null, victim, SocketConnection.MessageTarget.everyone_but_victim );
                            SocketConnection.Act( "Your massive blow shatters $N&n's stoneskin!", ch, null, victim, SocketConnection.MessageTarget.character );
                            victim.RemoveAffect( Affect.AFFECT_STONESKIN );
                        }
                        else
                        {
                            dam = dam / 15 != 0 ? dam / 15 : 1;
                        }
                    }

                }

                if( dam < 0 )
                    dam = 0;

                /*
                * Check for disarm, trip, parry, dodge and shield block.
                */
                if (skill != "barehanded fighting" || skill == "kick")
                {
                    // Trip and disarm removed because those should be handled
                    // by each individual mob's special function.
                    if( ch.IsNPC()
                            && ch.HasInnate( Race.RACE_WEAPON_WIELD )
                            && MUDMath.NumberPercent() < Math.Min( 25, Math.Max( 10, ch._level ) )
                            && !victim.IsNPC() )
                        UseMagicalItem( ch );
                }
            }

            switch( victim.CheckRIS( damType ) )
            {
                case Race.ResistanceType.resistant:
                    dam -= dam / 3;
                    break;
                case Race.ResistanceType.immune:
                    immune = true;
                    dam = 0;
                    break;
                case Race.ResistanceType.susceptible:
                    dam += dam / 2;
                    break;
                case Race.ResistanceType.vulnerable:
                    dam *= 2;
                    break;
                default:
                    break;
            }

            if( ( damType == AttackType.DamageType.wind || damType == AttackType.DamageType.gas || damType == AttackType.DamageType.asphyxiation )
                    && victim.IsAffected(Affect.AFFECT_DENY_AIR))
            {
                if( MUDMath.NumberPercent() < 50 )
                {
                    ch.SendText( "&+CYou deny the damage.&n\r\n" );
                    immune = true;
                    dam = 0;
                }
                else
                    dam -= dam / 5;
            }
            if (damType == AttackType.DamageType.fire && victim.IsAffected( Affect.AFFECT_DENY_FIRE))
            {
                if( MUDMath.NumberPercent() < 50 )
                {
                    ch.SendText( "&+rYou deny the damage.&n\r\n" );
                    immune = true;
                    dam = 0;
                }
                else
                    dam -= dam / 5;
            }
            if( ( damType == AttackType.DamageType.earth || damType == AttackType.DamageType.crushing )
                    && victim.IsAffected( Affect.AFFECT_DENY_EARTH))
            {
                if( MUDMath.NumberPercent() < 50 )
                {
                    ch.SendText( "&+yYou deny the damage.&n\r\n" );
                    immune = true;
                    dam = 0;
                }
                else
                    dam -= dam / 5;
            }
            if( ( damType == AttackType.DamageType.water || damType == AttackType.DamageType.acid || damType == AttackType.DamageType.drowning )
                    && victim.IsAffected( Affect.AFFECT_DENY_WATER))
            {
                if( MUDMath.NumberPercent() < 50 )
                {
                    ch.SendText( "&+bYou deny the damage.&n\r\n" );
                    immune = true;
                    dam = 0;
                }
                else
                    dam -= dam / 5;
            }

            // Check for protection spells that give 25% damage reduction - Xangis
            if (damType == AttackType.DamageType.fire && victim.IsAffected( Affect.AFFECT_PROTECT_FIRE))
                dam = ( dam * 3 ) / 4;
            else if (damType == AttackType.DamageType.cold && victim.IsAffected( Affect.AFFECT_PROTECT_COLD))
                dam = ( dam * 3 ) / 4;
            else if (damType == AttackType.DamageType.acid && victim.IsAffected( Affect.AFFECT_PROTECT_ACID))
                dam = ( dam * 3 ) / 4;
            else if (damType == AttackType.DamageType.gas && victim.IsAffected( Affect.AFFECT_PROTECT_GAS))
                dam = ( dam * 3 ) / 4;
            else if (damType == AttackType.DamageType.electricity && victim.IsAffected( Affect.AFFECT_PROTECT_LIGHTNING))
                dam = ( dam * 3 ) / 4;

            // Barkskin protects from 8% of slash and 12% of pierce damage.
            if (victim.IsAffected( Affect.AFFECT_BARKSKIN))
            {
                if (skill == "1h slashing" || skill == "2h slashing")
                    dam = dam * 11 / 12;
                else if (skill == "1h piercing" || skill == "2h piercing")
                    dam = dam * 7 / 8;
            }

            // Check for vampiric touch for anti-paladins and vampires
            if( weapon == ObjTemplate.WearLocation.hand_one || weapon == ObjTemplate.WearLocation.hand_two || weapon == ObjTemplate.WearLocation.hand_three || weapon == ObjTemplate.WearLocation.hand_four )
            {
                if( ( ( ch.IsClass(CharClass.Names.antipaladin) || ch.GetRace() == Race.RACE_VAMPIRE )
                        && skill == "barehanded fighting" && !Object.GetEquipmentOnCharacter(ch, weapon)) || (ch.IsAffected( Affect.AFFECT_VAMP_TOUCH)
                             && ( !( obj = Object.GetEquipmentOnCharacter( ch, weapon ) ) || obj.HasAffect( Affect.AFFECT_VAMP_TOUCH ) ) ) )
                {
                    ch._hitpoints += dam / 3;
                    if( ch._hitpoints > ( ch.GetMaxHit() + 50 + ch._level * 5 ) )
                    {
                        ch._hitpoints = ch.GetMaxHit() + 50 + ch._level * 5;
                    }
                }
            }

            /* PC to PC damage quartered.
            *  NPC to PC damage divided by 3.
            */
            if( dam > 0 && !victim.IsNPC() && victim != ch )
            {
                if( !ch.IsNPC() )
                    dam /= 4;
                else
                    dam /= 3;
            }

            /*
            * Just a check for anything that is excessive damage
            * Send a log message, keeping the imms on their toes
            * Changed this from 300 to 250 'cause hitters get more than one
            *  attack/round and w/haste that's 1000 possible in the time one fist
            *  goes off.  That's more than the fist might do and it has to be
            *  memmed.
            */
            if (dam > 250 && skill != "backstab" )
            {
                string buf4;
                if (!string.IsNullOrEmpty(skill))
                {
                    buf4 = String.Format("Excessive damage: {0} attacking {1} for {2}, skill = {3}({4}).",
                              ch._name, victim._name, dam, Skill.SkillList[skill].DamageText, skill);
                }
                else
                {
                    buf4 = String.Format("Excessive damage: {0} attacking {1} for {2}, unknown damage type.",
                              ch._name, victim._name, dam);
                }
                Log.Trace( buf4 );
            }

            /*
            * We moved DamageMessage out of the victim != ch if above
            * so self damage would show.  Other valid type_undefined
            * damage is ok to avoid like mortally wounded damage 
            */
            if (!String.IsNullOrEmpty(skill))
            {
                SendDamageMessage(ch, victim, dam, skill, weapon, immune);
            }

            victim._hitpoints -= dam;

            /* Check for HOLY_SACRFICE and BATTLE_ECSTASY */
            if( dam > 0 && victim != ch )
            {
                CharData groupChar;
                if (victim.IsAffected( Affect.AFFECT_HOLY_SACRIFICE) && victim._groupLeader)
                {
                    for( groupChar = victim._groupLeader; groupChar; groupChar = groupChar._nextInGroup )
                    {
                        if( groupChar == victim || groupChar._inRoom != ch._inRoom )
                            continue;
                        groupChar._hitpoints += dam / 5;
                        if (groupChar._hitpoints > groupChar.GetMaxHit() + 50 + groupChar._level * 5)
                        {
                            groupChar._hitpoints = groupChar.GetMaxHit() + 50 + groupChar._level * 5;
                        }
                    } //end for loop
                } //end if holy sac
                if( ch._groupLeader != null )
                {
                    for( groupChar = ch._groupLeader; groupChar != null; groupChar = groupChar._nextInGroup )
                    {
                        if( groupChar == victim || groupChar._inRoom != ch._inRoom )
                            continue;
                        if( groupChar.IsAffected( Affect.AFFECT_BATTLE_ECSTASY ) )
                        {
                            groupChar._hitpoints += dam / 20;
                            if( groupChar._hitpoints > groupChar.GetMaxHit() + 50 + groupChar._level * 5 )
                                groupChar._hitpoints = groupChar.GetMaxHit() + 50 + groupChar._level * 5;
                        } // end if battle ecstasy
                    } //end for loop
                } //end if grouped
            } //end if

            // Make sure if they got an instant kill roll that the victim dies.
            if (skill == "instant kill")
            {
                if( victim.GetRace() != Race.RACE_DEVIL
                        && victim.GetRace() != Race.RACE_DEMON
                        && victim.GetRace() != Race.RACE_GOD )
                    victim._hitpoints = -20;
            }

            /* Added damage exp! */
            // chance added because people level faster and faster as they get higher level...
            // to be worked out when exp is redone.
            // you can now only get damage exp on mobs that con easy or better
            // and there's only a 25% chance per hit of you evern being eligible for damage exp.
            if( MUDMath.NumberPercent() < 25 && victim._level >= ( ch._level - 3 ) )
                ch.GainExperience( Math.Max( 1, dam / 20 ) );

            if( !victim.IsNPC()
                    && victim._level >= Limits.LEVEL_AVATAR
                    && victim._hitpoints < 1 )
                victim._hitpoints = 1;

            /*
            * Magic shields that retaliate
            *
            * Apparently two people with the same sort of shield do not
            * take damage from each other
            */
            if( ( dam > 1 ) && victim != ch )
            {
                if( victim.IsAffected( Affect.AFFECT_FIRESHIELD )
                        && !ch.IsAffected( Affect.AFFECT_FIRESHIELD ) )
                    InflictSpellDamage( victim, ch, dam / 2, "fireshield", AttackType.DamageType.fire );

                if (victim.IsAffected( Affect.AFFECT_COLDSHIELD)
                        && !ch.IsAffected(Affect.AFFECT_COLDSHIELD))
                    InflictSpellDamage( victim, ch, dam / 2, "coldshield", AttackType.DamageType.cold );

                if (victim.IsAffected(Affect.AFFECT_SHOCK_SHIELD)
                        && !ch.IsAffected(Affect.AFFECT_SHOCK_SHIELD))
                    InflictSpellDamage( victim, ch, dam / 2, "shockshield", AttackType.DamageType.electricity );

                /* Soulshield is a complex one.  If the attacker and victim are of
                * opposite alignment, the shield retaliates with 1/2 damage just like
                * any other shield.  If the victim is neutral and the attacker is
                * not, the shield retaliates with 1/4 damage.  If the victim is good
                * or evil and the attacker is neutral, the shield retaliates with
                * 1/8 damage.  If the attacker and victim are of same alignment,
                * the shield does nothing.
                */
                if (victim.IsAffected(Affect.AFFECT_SOULSHIELD)
                        && !ch.IsAffected(Affect.AFFECT_SOULSHIELD))
                {
                    if( victim.IsEvil() && ch.IsGood() )
                        InflictSpellDamage(victim, ch, dam / 2, "soulshield", AttackType.DamageType.harm);
                    else if( victim.IsGood() && ch.IsEvil() )
                        InflictSpellDamage(victim, ch, dam / 2, "soulshield", AttackType.DamageType.harm);
                    else if( victim.IsNeutral() && ( ch.IsEvil() || ch.IsGood() ) )
                        InflictSpellDamage(victim, ch, dam / 4, "soulshield", AttackType.DamageType.harm);
                    else if( victim.IsGood() && ch.IsNeutral() )
                        InflictSpellDamage(victim, ch, dam / 8, "soulshield", AttackType.DamageType.harm);
                    else if( victim.IsEvil() && ch.IsNeutral() )
                        InflictSpellDamage(victim, ch, dam / 8, "soulshield", AttackType.DamageType.harm);
                }
            }

            if (victim.IsAffected( Affect.AFFECT_BERZERK ) && victim._position <= Position.stunned )
                victim.RemoveAffect(Affect.AFFECT_BERZERK);

            if (dam > 0 && skill != "barehanded fighting"
                    && IsWieldingPoisoned( ch, weapon )
                    && !Magic.SpellSavingThrow( ch._level, victim, AttackType.DamageType.poison ) )
            {
                InflictPoison( "poison_weapon", ch._level, IsWieldingPoisoned( ch, weapon ), ch, victim );
                SocketConnection.Act( "$n&n suffers from the &+Gpoison&n inflicted upon $m.", victim, null, null, SocketConnection.MessageTarget.room, true );
                Object.StripAffect( Object.GetEquipmentOnCharacter( ch, weapon ), Affect.AffectType.skill, "poison weapon" );
            }

            victim.UpdatePosition();

            switch( victim._position )
            {
                case Position.mortally_wounded:
                    victim.SendText(
                        "&+LYou are &+Rmo&n&+rr&+Rt&n&+ral&+Rl&n&+ry&+L wounded, and will die soon, if not aided.&n\r\n" );
                    SocketConnection.Act( "$n&+L is &+Rmo&n&+rr&+Rt&n&+ral&+Rl&n&+ry&+L wounded, and will die soon, if not aided.&n",
                         victim, null, null, SocketConnection.MessageTarget.room, true );
                    StopNotVicious( victim );
                    break;

                case Position.incapacitated:
                    victim.SendText(
                        "&+LYou are incapacitated and will &n&+rbl&+Re&n&+re&+Rd&+L to death, if not aided.&n\r\n" );
                    SocketConnection.Act( "$n&+L is incapacitated and will slowly &n&+rbl&+Re&n&+re&+Rd&+L to death, if not aided.&n",
                         victim, null, null, SocketConnection.MessageTarget.room, true );
                    StopNotVicious( victim );
                    break;

                case Position.stunned:
                    victim.SendText( "&+LYou are stunned, but will probably recover.&n\r\n" );
                    SocketConnection.Act( "$n&+L is stunned, but will probably recover.&n",
                         victim, null, null, SocketConnection.MessageTarget.room, true );
                    break;

                case Position.dead:
                    if( victim == ch )
                    {
                        victim.SendText( "&+LYou have been &+Rsl&n&+ra&+Ri&n&+rn&+L!&n\r\n\r\n" );
                    }
                    else
                    {
                        string buf = String.Format( "&+LYou have been &+Rsl&n&+ra&+Ri&n&+rn&+L by&n {0}&+L!&n\r\n\r\n",
                                                    ch.ShowNameTo( victim, false ) );
                        victim.SendText( buf );
                    }
                    /* Added this to stop a bug. */
                    Combat.StopFighting( victim, true );
                    SocketConnection.Act( "$n&+L is &n&+rdead&+L!&n", victim, null, null, SocketConnection.MessageTarget.room, true );
                    break;

                default:
                    if( dam > victim.GetMaxHit() / 5 )
                        victim.SendText( "That really did &+RHURT&n!\r\n" );
                    if( victim._hitpoints < victim.GetMaxHit() / 10 )
                        victim.SendText( "You sure are &n&+rBL&+RE&n&+rE&+RDI&n&+rN&+RG&n!\r\n" );
                    break;
            }

            // Check for weapon procs
            if( ( obj = Object.GetEquipmentOnCharacter( ch, weapon ) ) && Position.dead != victim._position )
            {
                if( obj.SpecFun.Count > 0 )
                    obj.CheckSpecialFunction(true);
            }

            /*
            * Sleep spells and extremely wounded folks.
            */
            if( !victim.IsAwake() )      /* lets make NPC's not slaughter PC's */
            {
                if( victim._fighting
                        && victim._fighting._hunting
                        && victim._fighting._hunting.Who == victim )
                    StopHunting( victim._fighting );
                if( victim._fighting
                        && !victim.IsNPC()
                        && ch.IsNPC() )
                    StopFighting( victim, true );
                else
                    StopFighting( victim, false );
            }

            /*
            * Payoff for killing things.
            */
            if( victim._position == Position.dead )
            {
                // Done in attempt to squelch the combat continuation bug
                StopFighting( victim, true );

                if( !victim.HasActBit(MobTemplate.ACT_NOEXP ) || !victim.IsNPC() )
                    GroupExperienceGain( ch, victim );

                if( ch.IsNPC() )
                {
                    if( ch._hunting )
                    {
                        if( ch._hunting.Who == victim )
                            StopHunting( ch );
                    }
                    if( ch.IsHating(victim) )
                    {
                        ch.StopHating( victim );
                    }
                }

                if( !victim.IsNPC() )
                {
                    if( ch.IsNPC() )
                    {
                        ( (PC)victim ).MobDeaths++;
                        if( victim.IsGuild() )
                        {
                            ( (PC)victim ).GuildMembership.MonsterDeaths++;
                            ( (PC)victim ).GuildMembership.Score += CalculateDeathScore( ch, victim );
                        }
                        ( (PC)victim ).Score += CalculateDeathScore( ch, victim );

                    }
                    else
                    {
                        ( (PC)ch ).PlayerKills++;
                        ( (PC)victim ).PlayerDeaths++;

                        ( (PC)victim ).Score += CalculateDeathScore( ch, victim );
                        ( (PC)ch ).Score += CalculateKillScore( ch, victim );

                        if( ch.IsGuild()
                                && victim.IsGuild()
                                && ( (PC)ch ).GuildMembership != ( (PC)victim ).GuildMembership )
                        {
                            ( (PC)ch ).GuildMembership.PlayerKills++;
                            ( (PC)victim ).GuildMembership.PlayerDeaths++;
                            ( (PC)ch ).GuildMembership.Score += CalculateKillScore( ch, victim );
                            ( (PC)victim ).GuildMembership.Score += CalculateDeathScore( ch, victim );
                        }
                    }

                    string logBuf = String.Format( "{0}&n killed by {1}&n at {2}",
                              victim._name, ( ch.IsNPC() ? ch._shortDescription : ch._name ),
                              victim._inRoom.IndexNumber );
                    Log.Trace( logBuf );
                    ImmortalChat.SendImmortalChat( ch, ImmortalChat.IMMTALK_DEATHS, Limits.LEVEL_AVATAR, logBuf );

                    /*
                    * Dying penalty:
                    * 
                    * At level 1 you lose 12.5% of a level.
                    * At level 50 you lose 25% of a level.
                    */
                    // Made it so people level 5 and under lose no exp from death.
                    if( ch._level > 5 )
                        victim.GainExperience( ( 0 - ( ( ( 50 + victim._level ) * ExperienceTable.Table[ victim._level ].LevelExperience ) / 400 ) ) );
                    if( victim._level < 2 && victim._experiencePoints < 1 )
                        victim._experiencePoints = 1;

                }
                else
                {
                    if( !ch.IsNPC() )
                    {
                        ( (PC)ch ).MobKills++;
                        if( ch.IsGuild() )
                        {
                            ( (PC)ch ).GuildMembership.MonsterKills++;
                            ( (PC)ch ).GuildMembership.Score += CalculateKillScore( ch, victim );
                        }
                        ( (PC)ch ).Score += CalculateKillScore( ch, victim );
                    }
                }
                KillingBlow( ch, victim );
                

                return true;
            }

            if( victim == ch )
            {
                return false;
            }

            /*
            * Wimp out?
            */
            if( victim.IsNPC() && dam > 0 )
            {
                if( ( victim.HasActBit(MobTemplate.ACT_WIMPY ) && MUDMath.NumberBits( 1 ) == 0
                        && victim._hitpoints < victim.GetMaxHit() / 5 )
                        || (victim.IsAffected( Affect.AFFECT_CHARM) && victim._master
                             && victim._master._inRoom != victim._inRoom ) )
                {
                    StartFearing( victim, ch );
                    StopHunting( victim );
                    CommandType.Interpret(victim, "flee");
                }
            }

            if( !victim.IsNPC() && victim._hitpoints > 0 && victim._hitpoints <= victim._wimpy )
            {
                CommandType.Interpret(victim, "flee");
            }

            return false;
        }

        /// <summary>
        /// Inflict spell damage based on the spell name.  Looks up the actual spell data and jumps to the other overload.
        /// </summary>
        /// <param name="ch"></param>
        /// <param name="victim"></param>
        /// <param name="dam"></param>
        /// <param name="spell"></param>
        /// <param name="dam_type"></param>
        /// <returns></returns>
        public static bool InflictSpellDamage(CharData ch, CharData victim, int dam, string spell, AttackType.DamageType dam_type)
        {
            if (Spell.SpellList.ContainsKey(spell))
            {
                Spell spl = Spell.SpellList[spell];
                if (spl != null)
                {
                    return InflictSpellDamage(ch, victim, dam, spl, dam_type);
                }
            }
            return false;
        }

        /// <summary>
        /// Inflicts damage from a spell, based on the weapon damage() function, but customized for spells.
        /// 
        /// Needs to be cleaned up because it's just too big (600+ lines).
        /// </summary>
        /// <param name="ch"></param>
        /// <param name="victim"></param>
        /// <param name="dam"></param>
        /// <param name="spell"></param>
        /// <param name="damType"></param>
        /// <returns></returns>
        public static bool InflictSpellDamage( CharData ch, CharData victim, int dam, Spell spell, AttackType.DamageType damType )
        {
            if( ch == null || victim == null || victim._position == Position.dead )
                return true;

            // Remove memorization and meditation bits.
            // And invis.
            ch.BreakInvis();
            victim.BreakMed();
            victim.BreakMem();

            if( CheckShrug( ch, victim ) )
                return false;

            if( victim._position == Position.sleeping
                    && !( victim.GetRace() == Race.RACE_FIRE_ELE && damType == AttackType.DamageType.fire )
                    && !( victim.GetRace() == Race.RACE_WATER_ELE && damType == AttackType.DamageType.water )
                    && !( victim.GetRace() == Race.RACE_EARTH_ELE && damType == AttackType.DamageType.earth )
                    && !( victim.GetRace() == Race.RACE_AIR_ELE && damType == AttackType.DamageType.wind ) )
            {
                SocketConnection.Act( "$n&n has a rude awakening!", victim, null, null, SocketConnection.MessageTarget.room );
                victim._position = Position.resting;
                if( ch._inRoom == victim._inRoom && ch._flyLevel == victim._flyLevel )
                    SetFighting( victim, ch );
            }

            // Check for globe spells.  See also FinishSpell under TargetType.singleCharacterOffensive
            // This check here is just to prevent area effect spells from
            // doing damage if too low level.  The check for direct spells is in
            // Magic.cs
            if (victim.IsAffected( Affect.AFFECT_MAJOR_GLOBE)
                    && (spell.SpellCircle[(int)ch._charClass.ClassNumber] <= 6
                         || spell.Name == "fireshield"
                         || spell.Name == "shockshield"
                         || spell.Name == "soulshield"
                         || spell.Name == "coldshield" ) )
            {
                SocketConnection.Act( "&+RThe globe around $N&+R's body bears the brunt of your assault!&n", ch, null, victim, SocketConnection.MessageTarget.character );
                SocketConnection.Act( "&+RYour globe deflects $n&+R's attack!&n", ch, null, victim, SocketConnection.MessageTarget.victim );
                SocketConnection.Act( "&+R$N&+R's globe deflects $n&+R's attack!&n", ch, null, victim, SocketConnection.MessageTarget.room_vict );
                return false;
            }
            if (victim.IsAffected( Affect.AFFECT_GREATER_SPIRIT_WARD) && spell.SpellCircle[(int)ch._charClass.ClassNumber] <= 5)
            {
                SocketConnection.Act( "&+WThe aura around $N&+W's body bears the brunt of your assault!&n", ch, null, victim, SocketConnection.MessageTarget.character );
                SocketConnection.Act( "&+WYour globe absorbs $n&+W's attack!&n", ch, null, victim, SocketConnection.MessageTarget.victim );
                SocketConnection.Act( "&+W$N&+W's aura absorbs $n&+W's attack!&n", ch, null, victim, SocketConnection.MessageTarget.room_vict );
                return false;
            }
            if (victim.IsAffected( Affect.AFFECT_MINOR_GLOBE) && spell.SpellCircle[(int)ch._charClass.ClassNumber] <= 4)
            {
                SocketConnection.Act( "&+RThe globe around $N&+R's body bears the brunt of your assault!&n", ch, null, victim, SocketConnection.MessageTarget.character );
                SocketConnection.Act( "&+RYour globe deflects $n&+R's attack!&n", ch, null, victim, SocketConnection.MessageTarget.victim );
                SocketConnection.Act( "&+R$N&+R's globe deflects $n&+R's attack!&n", ch, null, victim, SocketConnection.MessageTarget.room_vict );
                return false;
            }
            if (victim.IsAffected( Affect.AFFECT_SPIRIT_WARD) && spell.SpellCircle[(int)ch._charClass.ClassNumber] <= 3)
            {
                SocketConnection.Act( "&+WThe aura around $N&+W's body bears the brunt of your assault!&n", ch, null, victim, SocketConnection.MessageTarget.character );
                SocketConnection.Act( "&+WYour globe absorbs $n&+W's attack!&n", ch, null, victim, SocketConnection.MessageTarget.victim );
                SocketConnection.Act( "&+W$N&+W's aura absorbs $n&+W's attack!&n", ch, null, victim, SocketConnection.MessageTarget.victim );
                return false;
            }


            /*
            * Stop up any residual loopholes.
            */
            // 1275 is average damage from Akiaurn's Power Word
            // I changed this to reflect that.
            if( ( dam > 1275 ) && ch._level < Limits.LEVEL_AVATAR && ch.GetRace() != Race.RACE_DRAGON )
            {
                string buf3;

                if( ch.IsNPC() && ch._socket )
                    buf3 = String.Format(
                              "Spell_Damage: {0} from {1} by {2}: > 1275 points with {3} spell!",
                              dam, ch._name, ch._socket.Original._name, spell.Name );
                else
                    buf3 = String.Format(
                              "Spell_Damage: {0} from {1}: > 1275 points with {2} spell!",
                              dam, ch.IsNPC() ? ch._shortDescription : ch._name, spell.Name );

                Log.Error( buf3, 0 );
                dam = 1275;
            }

            if (victim.IsAffected( Affect.AFFECT_MINOR_PARA)
                    && !( victim.GetRace() == Race.RACE_FIRE_ELE && damType == AttackType.DamageType.fire )
                    && !( victim.GetRace() == Race.RACE_WATER_ELE && damType == AttackType.DamageType.water )
                    && !( victim.GetRace() == Race.RACE_EARTH_ELE && damType == AttackType.DamageType.earth )
                    && !( victim.GetRace() == Race.RACE_AIR_ELE && damType == AttackType.DamageType.wind ) )
            {
                SocketConnection.Act( "$n&n disrupts the magic preventing $N&n from moving.", ch, null, victim, SocketConnection.MessageTarget.room_vict );
                SocketConnection.Act( "You disrupt the magic preventing $N&n from moving.", ch, null, victim, SocketConnection.MessageTarget.character );
                SocketConnection.Act( "&+YYou can move again.&n", ch, null, victim, SocketConnection.MessageTarget.victim );
                victim.RemoveAffect( Affect.AFFECT_MINOR_PARA );
                victim.AffectStrip( Affect.AffectType.spell, "earthen grasp" );
                victim.AffectStrip( Affect.AffectType.spell, "greater earthen grasp");
            }

            bool immune = false;
            if( victim != ch )
            {
                /*
                * Certain attacks are forbidden.
                * Most other attacks are returned.
                */
                if( IsSafe( ch, victim ) )
                    return false;
                // is_safe could wipe out victim, as it calls procs if a boss
                // check and see that victim is still valid
                if( !victim )
                    return true;
                Crime.CheckAttemptedMurder( ch, victim );

                if( victim._position > Position.stunned
                        && !( victim.GetRace() == Race.RACE_FIRE_ELE && damType == AttackType.DamageType.fire )
                        && !( victim.GetRace() == Race.RACE_WATER_ELE && damType == AttackType.DamageType.water )
                        && !( victim.GetRace() == Race.RACE_EARTH_ELE && damType == AttackType.DamageType.earth )
                        && !( victim.GetRace() == Race.RACE_AIR_ELE && damType == AttackType.DamageType.wind ) )
                {
                    // Offensive spells engage victim if not fighting, and
                    //   caster only if neither are fighting.
                    if( !victim._fighting && victim._inRoom == ch._inRoom
                            && victim._flyLevel == ch._flyLevel )
                    {
                        SetFighting( victim, ch );
                        if( !ch._fighting )
                            SetFighting( ch, victim );
                        // Can't have prone people automaticaly stand.
                        if( victim._position == Position.standing )
                            victim._position = Position.fighting;
                    }
                    /*
                    * If NPC victim is following, ch might attack victim's master.
                    * No charm check here because charm would be dispelled from
                    * tanking mobile when combat ensues thus ensuring PC charmer is
                    * not harmed.
                    * Check for is_same_group wont work as following mobile is not
                    * always grouped with PC charmer 
                    */
                    if( ch.IsNPC()
                            && victim.IsNPC()
                            && victim._master
                            && victim._master._inRoom == ch._inRoom
                            && MUDMath.NumberBits( 2 ) == 0 )
                    {
                        StopFighting( ch, false );
                        SetFighting( ch, victim._master );
                        return false;
                    }
                }

                /*
                * More charm stuff.
                */
                if( victim._master == ch
                        && !( victim.GetRace() == Race.RACE_FIRE_ELE && damType == AttackType.DamageType.fire )
                        && !( victim.GetRace() == Race.RACE_WATER_ELE && damType == AttackType.DamageType.water )
                        && !( victim.GetRace() == Race.RACE_EARTH_ELE && damType == AttackType.DamageType.earth )
                        && !( victim.GetRace() == Race.RACE_AIR_ELE && damType == AttackType.DamageType.wind ) )
                    StopFighting( victim, true );

                /*
                * Hunting stuff...
                */
                if( dam != 0 && victim.IsNPC()
                        && !( victim.GetRace() == Race.RACE_FIRE_ELE && damType == AttackType.DamageType.fire )
                        && !( victim.GetRace() == Race.RACE_WATER_ELE && damType == AttackType.DamageType.water )
                        && !( victim.GetRace() == Race.RACE_EARTH_ELE && damType == AttackType.DamageType.earth )
                        && !( victim.GetRace() == Race.RACE_AIR_ELE && damType == AttackType.DamageType.wind ) )
                {
                    StartGrudge( victim, ch, false );
                }

                /*
                * Damage modifiers.
                */
                if (victim.IsAffected( Affect.AFFECT_SANCTUARY))
                    dam /= 2;

                if ((victim.IsAffected( Affect.AFFECT_PROTECT_EVIL)) && ch.IsEvil())
                    dam -= dam / 8;
                else if ((victim.IsAffected( Affect.AFFECT_PROTECT_GOOD)) && ch.IsGood())
                    dam -= dam / 8;

                if( dam < 0 )
                    dam = 0;
            }

            switch( victim.CheckRIS( damType ) )
            {
                case Race.ResistanceType.resistant:
                    dam -= dam / 3;
                    break;
                case Race.ResistanceType.immune:
                    immune = true;
                    dam = 0;
                    break;
                case Race.ResistanceType.susceptible:
                    dam += dam / 2;
                    break;
                case Race.ResistanceType.vulnerable:
                    dam *= 2;
                    break;
            }

            if( ( damType == AttackType.DamageType.wind || damType == AttackType.DamageType.gas || damType == AttackType.DamageType.asphyxiation )
                    && victim.IsAffected( Affect.AFFECT_DENY_AIR))
            {
                if( MUDMath.NumberPercent() < 50 )
                {
                    victim.SendText( "&+CYou deny the damage.&n\r\n" );
                    immune = true;
                    dam = 0;
                }
                else
                    dam -= dam / 5;
            }
            if (damType == AttackType.DamageType.fire && victim.IsAffected( Affect.AFFECT_DENY_FIRE))
            {
                if( MUDMath.NumberPercent() < 50 )
                {
                    victim.SendText( "&+rYou deny the damage.&n\r\n" );
                    immune = true;
                    dam = 0;
                }
                else
                    dam -= dam / 5;
            }
            if( ( damType == AttackType.DamageType.earth || damType == AttackType.DamageType.crushing )
                    && victim.IsAffected(Affect.AFFECT_DENY_EARTH))
            {
                if( MUDMath.NumberPercent() < 50 )
                {
                    victim.SendText( "&+yYou deny the damage.&n\r\n" );
                    immune = true;
                    dam = 0;
                }
                else
                    dam -= dam / 5;
            }
            if( ( damType == AttackType.DamageType.water || damType == AttackType.DamageType.acid || damType == AttackType.DamageType.drowning )
                    && victim.IsAffected(Affect.AFFECT_DENY_WATER))
            {
                if( MUDMath.NumberPercent() < 50 )
                {
                    victim.SendText( "&+bYou deny the damage.&n\r\n" );
                    immune = true;
                    dam = 0;
                }
                else
                    dam -= dam / 5;
            }

            // Check for protection spells that give 25% damage reduction - Xangis
            if (damType == AttackType.DamageType.fire && victim.IsAffected( Affect.AFFECT_PROTECT_FIRE))
                dam = ( dam * 3 ) / 4;
            else if (damType == AttackType.DamageType.cold && victim.IsAffected( Affect.AFFECT_PROTECT_COLD))
                dam = ( dam * 3 ) / 4;
            else if (damType == AttackType.DamageType.acid && victim.IsAffected( Affect.AFFECT_PROTECT_ACID))
                dam = ( dam * 3 ) / 4;
            else if (damType == AttackType.DamageType.gas && victim.IsAffected( Affect.AFFECT_PROTECT_GAS))
                dam = ( dam * 3 ) / 4;
            else if (damType == AttackType.DamageType.electricity && victim.IsAffected( Affect.AFFECT_PROTECT_LIGHTNING))
                dam = ( dam * 3 ) / 4;

            /*
            * We moved DamageMessage out of the victim != ch if above
            * so self damage would show.  Other valid type_undefined
            * damage is ok to avoid like mortally wounded damage 
            */
            if( spell != Spell.SpellList["reserved"]
                    && !( victim.GetRace() == Race.RACE_FIRE_ELE && damType == AttackType.DamageType.fire )
                    && !( victim.GetRace() == Race.RACE_WATER_ELE && damType == AttackType.DamageType.water )
                    && !( victim.GetRace() == Race.RACE_EARTH_ELE && damType == AttackType.DamageType.earth )
                    && !( victim.GetRace() == Race.RACE_AIR_ELE && damType == AttackType.DamageType.wind ) )
                SendSpellDamageMessage( ch, victim, dam, spell, immune );

            /*  PC to PC damage quartered.
            *  NPC to PC damage divided by 3.
            */
            if( dam > 0 && !victim.IsNPC() && victim != ch )
            {
                if( !ch.IsNPC() )
                    dam /= 4;
                else
                    dam /= 3;
            }

            /*
            * Hurt the victim.
            * Inform the victim of his new state.
            */
            if( !( victim.GetRace() == Race.RACE_FIRE_ELE && damType == AttackType.DamageType.fire )
                    && !( victim.GetRace() == Race.RACE_WATER_ELE && damType == AttackType.DamageType.water )
                    && !( victim.GetRace() == Race.RACE_EARTH_ELE && damType == AttackType.DamageType.earth )
                    && !( victim.GetRace() == Race.RACE_AIR_ELE && damType == AttackType.DamageType.wind ) )
            {
                /* Added damage exp! */
                // chance added because people level faster and faster as they get higher level...
                // you can now only get damage exp on mobs that con easy or better
                // and there's only a 25% chance per hit of you evern being eligible for damage exp.
                if( MUDMath.NumberPercent() < 25 && victim._level >= ( ch._level - 3 ) )
                    ch.GainExperience( Math.Max( 1, dam / 20 ) );
                victim._hitpoints -= dam;
            }
            else
            {
                string attack;

                if( spell != null && spell != Spell.SpellList["none"] )
                    attack = spell.Name;
                else
                    attack = "it";

                SocketConnection.Act( "$N&n absorbs your $t!", ch, attack, victim, SocketConnection.MessageTarget.character );
                SocketConnection.Act( "You absorb $n&n's $t!", ch, attack, victim, SocketConnection.MessageTarget.victim );
                SocketConnection.Act( "$N&n absorbs $n&n's $t", ch, attack, victim, SocketConnection.MessageTarget.room_vict );
                if( ch.IsImmortal() )
                {
                    string buf4 = String.Format( "You healed {0} damage.",
                                                 victim.GetMaxHit() >= dam + victim._hitpoints ? dam : victim.GetMaxHit() - victim._hitpoints );
                    ch.SendText( buf4 );
                }

                victim._hitpoints = Math.Min( victim.GetMaxHit(), victim._hitpoints + dam );

                return false;
            }

            if( !victim.IsNPC()
                    && victim._level >= Limits.LEVEL_AVATAR
                    && victim._hitpoints < 1 )
                victim._hitpoints = 1;

            if (victim.IsAffected(Affect.AFFECT_BERZERK)
                    && victim._position <= Position.stunned )
                victim.RemoveAffect(Affect.AFFECT_BERZERK);

            victim.UpdatePosition();

            switch( victim._position )
            {
                case Position.mortally_wounded:
                    victim.SendText(
                        "&+LYou are &+Rmo&n&+rr&+Rt&n&+ral&+Rl&n&+ry&+L wounded, and will die soon, if not aided.&n\r\n" );
                    SocketConnection.Act( "$n&+L is &+Rmo&n&+rr&+Rt&n&+ral&+Rl&n&+ry&+L wounded, and will die soon, if not aided.&n",
                         victim, null, null, SocketConnection.MessageTarget.room, true );
                    StopNotVicious( victim );
                    break;

                case Position.incapacitated:
                    victim.SendText(
                        "&+LYou are incapacitated and will slowly &n&+rbl&+Re&n&+re&+Rd&+L to death, if not aided.\r\n" );
                    SocketConnection.Act( "$n&+L is incapacitated and will slowly &n&+rbl&+Re&n&+re&+Rd&+L to death, if not aided.&n",
                         victim, null, null, SocketConnection.MessageTarget.room, true );
                    StopNotVicious( victim );
                    break;

                case Position.stunned:
                    victim.SendText( "&+LYou are stunned, but will probably recover.&n\r\n" );
                    SocketConnection.Act( "$n&+L is stunned, but will probably recover.&n",
                         victim, null, null, SocketConnection.MessageTarget.room, true );
                    break;

                case Position.dead:
                    SocketConnection.Act( spell.MessageKill, ch, null, victim, SocketConnection.MessageTarget.room_vict );
                    SocketConnection.Act( spell.MessageKill, ch, null, victim, SocketConnection.MessageTarget.character );
                    if( victim == ch )
                    {
                        victim.SendText( "&+LYou have been &+Rsl&n&+ra&+Ri&n&+rn&+L!&n\r\n\r\n" );
                    }
                    else
                    {
                        string buf = String.Format( "&+LYou have been &+Rsl&n&+ra&+Ri&n&+rn&+L by&n {0}&+L!&n\r\n\r\n",
                                                    ch.ShowNameTo( victim, false ) );
                        victim.SendText( buf );
                    }
                    StopFighting( victim, true );
                    SocketConnection.Act( "$n&+L is &n&+rdead&+L!&n", victim, null, null, SocketConnection.MessageTarget.room, true );
                    break;

                default:
                    if( dam > victim.GetMaxHit() / 5 )
                        victim.SendText( "That really did &+RHURT&n!\r\n" );
                    if( victim._hitpoints < victim.GetMaxHit() / 10 )
                        victim.SendText( "You sure are &n&+rBL&+RE&n&+rE&+RDI&n&+rN&+RG&n!\r\n" );
                    break;
            }

            /*
            * Sleep spells and extremely wounded folks.
            */
            if( !victim.IsAwake() )      /* lets make NPC's not slaughter PC's */
            {
                if( victim._fighting
                        && victim._fighting._hunting
                        && victim._fighting._hunting.Who == victim )
                    StopHunting( victim._fighting );
                if( victim._fighting
                        && !victim.IsNPC()
                        && ch.IsNPC() )
                    StopFighting( victim, true );
                else
                    StopFighting( victim, false );
            }

            /*
            * Payoff for killing things.
            */
            if( victim._position == Position.dead )
            {
                StopFighting( ch, false );

                if( !victim.HasActBit(MobTemplate.ACT_NOEXP ) || !victim.IsNPC() )
                    GroupExperienceGain( ch, victim );

                if( !victim.IsNPC() )
                {
                    if( ch.IsNPC() )
                    {
                        ( (PC)victim ).MobDeaths++;
                        if( victim.IsGuild() )
                        {
                            ( (PC)victim ).GuildMembership.MonsterDeaths++;
                            ( (PC)victim ).GuildMembership.Score += CalculateDeathScore( ch, victim );
                        }
                        ( (PC)victim ).Score += CalculateDeathScore( ch, victim );
                    }
                    else
                    {
                        ( (PC)ch ).PlayerKills++;
                        ( (PC)victim ).PlayerDeaths++;

                        ( (PC)victim ).Score += CalculateDeathScore( ch, victim );
                        ( (PC)ch ).Score += CalculateKillScore( ch, victim );

                        if( ch.IsGuild()
                                && victim.IsGuild()
                                && ( (PC)ch ).GuildMembership != ( (PC)victim ).GuildMembership )
                        {
                            ( (PC)ch ).GuildMembership.PlayerKills++;
                            ( (PC)victim ).GuildMembership.PlayerDeaths++;
                            ( (PC)ch ).GuildMembership.Score += CalculateKillScore( ch, victim );
                            ( (PC)victim ).GuildMembership.Score += CalculateDeathScore( ch, victim );
                        }
                    }

                    string logBuf = String.Format( "{0}&n killed by {1}&n at {2}",
                              victim._name,
                              ( ch.IsNPC() ? ch._shortDescription : ch._name ),
                              victim._inRoom.IndexNumber );
                    Log.Trace( logBuf );
                    ImmortalChat.SendImmortalChat( ch, ImmortalChat.IMMTALK_DEATHS, Limits.LEVEL_AVATAR, logBuf );

                    /*
                    * Dying penalty:
                    * 1/2 way back to previous 2 levels.
                    */
                    // Newbies do not lose exp from death.
                    if( ch._level > 5 )
                        victim.GainExperience( ( 0 - ( ( ( 50 + victim._level ) * ExperienceTable.Table[ victim._level ].LevelExperience ) / 400 ) ) );
                    if( victim._level < 2 && victim._experiencePoints < 1 )
                        victim._experiencePoints = 1;
                }
                else
                {
                    if( !ch.IsNPC() )
                    {
                        ( (PC)ch ).MobKills++;
                        if( ch.IsGuild() )
                        {
                            ( (PC)ch ).GuildMembership.MonsterKills++;
                            ( (PC)ch ).GuildMembership.Score += CalculateKillScore( ch, victim );
                        }
                        ( (PC)ch ).Score += CalculateKillScore( ch, victim );
                    }
                }

                KillingBlow( ch, victim );

                // Keep in mind after this point the character is not in the
                // CharList, not in any room, and is at the menu.  Don't do
                // anything that would cause a segmentation fault.

                if( ch.IsGuild()
                        && victim.IsGuild()
                        && ( (PC)ch ).GuildMembership != ( (PC)victim ).GuildMembership )
                {
                    ( (PC)ch ).GuildMembership.Score += 20;
                }

                return true;
            }

            if( victim == ch )
            {
                return false;
            }

            /*
            * Wimp out?
            */
            if( victim.IsNPC() && dam > 0 )
            {
                if( ( victim.HasActBit(MobTemplate.ACT_WIMPY ) && MUDMath.NumberBits( 1 ) == 0
                        && victim._hitpoints < victim.GetMaxHit() / 5 )
                        || (victim.IsAffected(Affect.AFFECT_CHARM) && victim._master
                             && victim._master._inRoom != victim._inRoom ) )
                {
                    StartFearing( victim, ch );
                    StopHunting( victim );
                    CommandType.Interpret(victim, "flee");
                }
            }

            if( !victim.IsNPC() && victim._hitpoints > 0 && victim._hitpoints <= victim._wimpy )
            {
                CommandType.Interpret(victim, "flee");
            }

            return false;
        }

        /// <summary>
        /// Checks whether anyone guarding the victim manages to prevent them from being hit.
        /// </summary>
        /// <param name="ch"></param>
        /// <param name="victim"></param>
        /// <returns></returns>
        public static CharData CheckGuarding( CharData ch, CharData victim )
        {
            if (victim == null)
            {
                return null;
            }

            foreach( CharData guard in victim._inRoom.People )
            {
                if( !guard.IsNPC() && ( ( (PC)guard ).Guarding == victim ) && ( guard != victim ) && ( guard != ch ) )
                {
                    guard.PracticeSkill( "guard" );
                    if( MUDMath.NumberPercent() < ( ( guard._level + ( (PC)guard ).SkillAptitude[ "guard" ] ) / 4 ) )
                    {
                        SocketConnection.Act( "$n&n bravely jumps in front of you!.", guard, null, victim, SocketConnection.MessageTarget.victim );
                        SocketConnection.Act( "$n&n bravely jumps in front of $N&n.", guard, null, victim, SocketConnection.MessageTarget.room_vict );
                        SocketConnection.Act( "You heriocally jump in front of $N&n's attacker.", guard, null, victim, SocketConnection.MessageTarget.character );
                        if( ch._fighting == victim )
                            ch._fighting = guard;
                        return guard;
                    }
                    SocketConnection.Act( "$n&n watches helplessly as you are attacked.", guard, null, victim, SocketConnection.MessageTarget.victim );
                    SocketConnection.Act( "$n&n unsuccessfully tries to protect $N&n.", guard, null, victim, SocketConnection.MessageTarget.room_vict );
                    SocketConnection.Act( "You heroically step aside for $N&n's attacker.", guard, null, victim, SocketConnection.MessageTarget.character );
                }
            }

            return victim;
        }

        /// <summary>
        /// Check to see if the char is able to attack the victim, and if not, give a message saying why they can't.
        /// </summary>
        /// <param name="ch"></param>
        /// <param name="victim"></param>
        /// <returns></returns>
        public static bool IsSafe( CharData ch, CharData victim )
        {
            if (ch == null || victim == null) return false;

            // Always safe to attack yourself.
            if (ch == victim) return false;

            if (!ch.IsNPC() && !victim.IsNPC() && victim.GetRacewarSide() == ch.GetRacewarSide() && !ch.IsImmortal())
            {
                SocketConnection.Act("You can't attack an ally.\n\r", ch, null, victim, SocketConnection.MessageTarget.character);
                return true;
            }

            if( !ch.IsNPC() && ch.IsAffected( Affect.AFFECT_WRAITHFORM ) )
            {
                ch.SendText( "&+LYou may not participate in combat while in &+Cgh&n&+co&+Cu&n&+cl&+L form.&n\r\n" );
                return true;
            }

            if( !victim.IsNPC() && victim.HasActBit(PC.PLAYER_JUST_DIED ) )
            {
                ch.SendText( "&+cThey just died, try in a few seconds...&n\r\n" );
                return true;
            }

            if (!victim.IsNPC() && victim.IsAffected(Affect.AFFECT_WRAITHFORM))
            {
                SocketConnection.Act( "Your attack passes through $N.", ch, null, victim, SocketConnection.MessageTarget.character );
                SocketConnection.Act( "$n's attack passes through $N.", ch, null, victim, SocketConnection.MessageTarget.room_vict );
                SocketConnection.Act( "$n's attack passes through you.", ch, null, victim, SocketConnection.MessageTarget.victim );
                return true;
            }

            // removed restriction allowing only registered players to pkill

            if( victim._level > Limits.LEVEL_HERO && victim != ch && !victim.IsNPC() )
            {
                SocketConnection.Act( "$N&n is an IMMORTAL and is automatically safe.", ch, null, victim, SocketConnection.MessageTarget.character );
                return true;
            }

            if( victim._fighting )
                return false;

            if( victim._inRoom.HasFlag( RoomTemplate.ROOM_SAFE ) )
            {
                SocketConnection.Act( "$N&n is in a safe room.", ch, null, victim, SocketConnection.MessageTarget.character );
                return true;
            }

            if( IsBossMob( victim ) )
            {
                //keep fighting, but allow the boss to invoke his special function
                // ensures the function will be called
                string lbuf = String.Format( "{0} is a boss", victim._shortDescription );
                Log.Trace( lbuf );
                victim._fighting = ch;
                if( victim._specFun.Count > 0 )
                {
                    foreach( MobSpecial spec in victim._specFun )
                    {
                        if( spec.SpecFunction( victim, MobFun.PROC_NORMAL ) )
                        {
                            // Only execute one function.
                            break;
                        }
                    }
                }
                return false;
            }

            if( ch.IsNPC() || victim.IsNPC() )
                return false;

            return false;
        }

        /// <summary>
        /// Check to see if weapon is poisoned.
        /// </summary>
        /// <param name="ch"></param>
        /// <param name="weapon"></param>
        /// <returns></returns>
        static bool IsWieldingPoisoned(CharData ch, ObjTemplate.WearLocation weapon)
        {
            Object obj = Object.GetEquipmentOnCharacter( ch, weapon );
            if( !obj )
                return false;
            foreach (Affect aff in obj.Affected)
            {
                if( aff.Type == Affect.AffectType.skill && aff.Value == "poison weapon" )
                    return true;
            }

            return false;

        }

        /// <summary>
        /// Checks whether the char is hunting the victim.
        /// </summary>
        /// <param name="ch"></param>
        /// <param name="victim"></param>
        /// <returns></returns>
        bool  IsHunting( CharData ch, CharData victim )
        {
            if( !ch._hunting || ch._hunting.Who != victim )
                return false;

            return true;
        }

        /// <summary>
        /// Check whenther the char is afraid of the target.
        /// </summary>
        /// <param name="ch"></param>
        /// <param name="victim"></param>
        /// <returns></returns>
        public static bool IsFearing( CharData ch, CharData victim )
        {
            if( !ch._fearing || ch._fearing.Who != victim )
                return false;

            return true;
        }

        /// <summary>
        /// Cancel hunting.
        /// </summary>
        /// <param name="ch"></param>
        public static void StopHunting( CharData ch )
        {
            if( ch._hunting )
            {
                string lbuf = String.Format( "{0}&n has stopped hunting {1}.", ch._shortDescription, ch._hunting.Name );
                ImmortalChat.SendImmortalChat( null, ImmortalChat.IMMTALK_HUNTING, 0, lbuf );
                ch._hunting = null;
            }
            // We may not want to let them continue hating the victim.
            // StopHating( ch );
            return;
        }

        /// <summary>
        /// Cancel all fear for a mob.
        /// </summary>
        /// <param name="ch"></param>
        static public void StopFearing( CharData ch )
        {    
            if( ch._fearing )
            {
                ch._fearing = null;
            }
            return;
        }

        /// <summary>
        /// Starts a mob tracking an enemy.
        /// </summary>
        /// <param name="ch"></param>
        /// <param name="victim"></param>
        public static void StartHunting( CharData ch, CharData victim )
        {
            if( ch._hunting )
            {
                if( ch._hunting.Who == victim )
                    return;
                StopHunting( ch );
            }
            ch._hunting = new EnemyData();
            ch._hunting.Name = victim._name;
            ch._hunting.Who = victim;
            string lbuf = String.Format( "{0}&n has started hunting {1}.", ch._shortDescription, ch._hunting.Name );
            ImmortalChat.SendImmortalChat( null, ImmortalChat.IMMTALK_HUNTING, 0, lbuf );
            return;
        }

        /// <summary>
        /// Add a person to a mob's hate list.
        /// </summary>
        /// <param name="ch"></param>
        /// <param name="victim"></param>
        public static void StartHating( CharData ch, CharData victim )
        {
            if( ch.IsHating( victim ) || !ch.IsNPC())
                return;

            EnemyData hating = new EnemyData();
            hating.Name = victim._name;
            hating.Who = victim;
            ch._hating.Add( hating );
            string lbuf = String.Format( "{0}&n has started hating {1}.", ch._shortDescription, victim._name );
            ImmortalChat.SendImmortalChat( null, ImmortalChat.IMMTALK_HATING, 0, lbuf );
            return;
        }

        /// <summary>
        /// Add a person to a mob's fear list.
        /// </summary>
        /// <param name="ch"></param>
        /// <param name="victim"></param>
        public static void StartFearing( CharData ch, CharData victim )
        {
            if( !ch.IsNPC() || !ch.HasActBit(MobTemplate.ACT_MEMORY ) )
                return;

            if( ch._fearing )
                StopFearing( ch );

            ch._fearing = new EnemyData();
            ch._fearing.Name = victim._name;
            ch._fearing.Who = victim;
            return;
        }

        /// <summary>
        /// A function to set a mob to being aggro and/or hunting a PC
        /// The parameter "ranged" is used if player ranged a spell or weapon
        /// at the mobile.  Otherwise its a simple call of start_hating
        /// and start_hunting.
        /// </summary>
        /// <param name="ch"></param>
        /// <param name="victim"></param>
        /// <param name="ranged"></param>
        public static void StartGrudge( CharData ch, CharData victim, bool ranged )
        {
            
            if( !ch.IsNPC() || victim.IsNPC() )
                return;
            if( ( ch.HasActBit(MobTemplate.ACT_HUNTER ) && ch.HasActBit(MobTemplate.ACT_MEMORY ) ) || ranged )
                StartHunting( ch, victim );
            if( ch.HasActBit(MobTemplate.ACT_MEMORY ) || ranged )
                StartHating( ch, victim );
            return;
        }

        /// <summary>
        /// Checks whether the victim is able to parry a swing.
        /// </summary>
        /// <param name="ch"></param>
        /// <param name="victim"></param>
        /// <returns></returns>
        static bool CheckParry( CharData ch, CharData victim )
        {
            if( !victim.IsAwake() || victim._position < Position.reclining )
                return false;

            if (ch.IsAffected(Affect.AFFECT_DAZZLE))
                return false;

            if( !victim.HasSkill( "parry" ) )
                return false;

            int chance = ch.GetSkillChance("parry");
            if (victim.IsNPC())
            {
                // Mobs more often than not don't have weapons
                // so they should get bonuses for actually
                // having them
                if( !Object.GetEquipmentOnCharacter( victim, ObjTemplate.WearLocation.hand_one ) )
                {
                    if( !Object.GetEquipmentOnCharacter( victim, ObjTemplate.WearLocation.hand_two ) )
                        chance -= 5;
                }
                else
                    chance += 5;
            }
            else
            {
                // No weapon means no parry.  Only secondary weapon means 50% chance to parry.
                if( !Object.GetEquipmentOnCharacter( victim, ObjTemplate.WearLocation.hand_one ) )
                {
                    if( !Object.GetEquipmentOnCharacter( victim, ObjTemplate.WearLocation.hand_two ) )
                        return false;
                    chance /= 2;
                }

                victim.PracticeSkill( "parry" );
            }

            if( MUDMath.NumberPercent() >= ( chance - ch._level ) / 2 )
                return false;
            
            switch( MUDMath.NumberRange(1,3))
            {
                case 1:
                    SocketConnection.Act( "$N&n skillfully parries your attack.", ch, null, victim, SocketConnection.MessageTarget.character );
                    SocketConnection.Act( "You parry $n&n's fierce attack.", ch, null, victim, SocketConnection.MessageTarget.victim );
                    SocketConnection.Act( "$N&n parries $n&n's attack.", ch, null, victim, SocketConnection.MessageTarget.room_vict );
                    break;
                case 2:
                    SocketConnection.Act("$N&n knocks your blow aside with $S weapon.", ch, null, victim, SocketConnection.MessageTarget.character);
                    SocketConnection.Act("You knock $n&n's clumsy attack aside with your weapon.", ch, null, victim, SocketConnection.MessageTarget.victim);
                    SocketConnection.Act("$N&n knocks $n&n's attack aside with $S weapon.", ch, null, victim, SocketConnection.MessageTarget.room_vict);
                    break;
                case 3:
                    SocketConnection.Act("$N&n deflects your attack with $S weapon.", ch, null, victim, SocketConnection.MessageTarget.character);
                    SocketConnection.Act("You deflect $n&n's attack with your weapon.", ch, null, victim, SocketConnection.MessageTarget.victim);
                    SocketConnection.Act("$N&n deflects $n&n's attack aside with $S weapon.", ch, null, victim, SocketConnection.MessageTarget.room_vict);
                    break;
            }
            if( ch._fighting == null )
                SetFighting( ch, victim );
            if( victim._fighting == null )
                SetFighting( victim, ch );
            return true;
        }

        /// <summary>
        /// Checks whether the victim is able to riposte the attacker's swing.  Returns false
        /// if failed, true if successful.
        /// </summary>
        /// <param name="ch"></param>
        /// <param name="victim"></param>
        /// <returns></returns>
        static bool CheckRiposte( CharData ch, CharData victim )
        {
            if( !victim.IsAwake() || victim._position < Position.reclining )
                return false;

            if( ch.IsAffected( Affect.AFFECT_DAZZLE ) )
                return false;

            if( ch.IsAffected( Affect.AFFECT_BLIND ) )
                return false;

            if( ch.IsAffected(Affect.AFFECT_CASTING ) )
                return false;

            if( !victim.HasSkill( "riposte" ) )
                return false;

            int chance = ch.GetSkillChance("riposte");
            if (victim.IsNPC())
            {
                // Mobs more often than not don't have weapons
                // so they should get bonuses for actually
                // having them
                if( Object.GetEquipmentOnCharacter( victim, ObjTemplate.WearLocation.hand_one ) )
                {
                    chance += 3;
                }
            }
            else
            {
                if( !Object.GetEquipmentOnCharacter( victim, ObjTemplate.WearLocation.hand_one ) )
                {
                    // Have to have a weapon to riposte.  If only holding secondary weapon chances are lowered.
                    if( !Object.GetEquipmentOnCharacter( victim, ObjTemplate.WearLocation.hand_two ))
                    {
                        return false;
                    }
                    chance /= 2;
                }

                victim.PracticeSkill( "riposte" );
            }

            if( MUDMath.NumberPercent() >= (( chance - ch._level ) / 3 ) )
                return false;

            switch( MUDMath.NumberRange(1,3))
            {
                case 1:
                    SocketConnection.Act( "$N&n deflects your blow and strikes back at YOU!", ch, null, victim, SocketConnection.MessageTarget.character ); 
                    SocketConnection.Act( "You deflect $n&n's attack and strike back at $m.", ch, null, victim, SocketConnection.MessageTarget.victim );
                    SocketConnection.Act( "$N&n deflects $n&n's attack and strikes back at $m.", ch, null, victim, SocketConnection.MessageTarget.room_vict );
                    break;
                case 2:
                    SocketConnection.Act("$N&n knocks your swing aside and strikes back at YOU!", ch, null, victim, SocketConnection.MessageTarget.character);
                    SocketConnection.Act("You knock $n&n's attack aside and strikes back at $m.", ch, null, victim, SocketConnection.MessageTarget.victim);
                    SocketConnection.Act("$N&n knocks $n&n's attack aside and strikes back at $m.", ch, null, victim, SocketConnection.MessageTarget.room_vict);
                    break;
                case 3:
                    SocketConnection.Act("$N&n blocks your strike and swings back at YOU!", ch, null, victim, SocketConnection.MessageTarget.character);
                    SocketConnection.Act("You block $n&n's strike aside and swing back at $m.", ch, null, victim, SocketConnection.MessageTarget.victim);
                    SocketConnection.Act("$N&n block $n&n's strike and swings back at $m.", ch, null, victim, SocketConnection.MessageTarget.room_vict);
                    break;
            }
            return true;
        }

        /// <summary>
        /// Checks for block if holding shield.
        /// </summary>
        /// <param name="ch"></param>
        /// <param name="victim"></param>
        /// <returns></returns>
        static bool CheckShieldBlock( CharData ch, CharData victim )
        {
            if( !victim.HasSkill( "shield block" ) )
                return false;

            if( ch.IsAffected( Affect.AFFECT_DAZZLE ) )
                return false;

            if( !victim.IsAwake() || victim._position < Position.reclining )
                return false;

            Object obj = Object.GetEquipmentOnCharacter( victim, ObjTemplate.WearLocation.hand_one );
            if( !obj || ( obj.ItemType != ObjTemplate.ObjectType.shield ) )
            {
                if( !( obj = Object.GetEquipmentOnCharacter( victim, ObjTemplate.WearLocation.hand_two ) ) )
                    return false;
                if( obj.ItemType != ObjTemplate.ObjectType.shield )
                    return false;
            }
            if( obj.ItemType != ObjTemplate.ObjectType.shield )
            {
                if( !( obj = Object.GetEquipmentOnCharacter( victim, ObjTemplate.WearLocation.hand_two ) ) )
                    return false;
                if( obj.ItemType != ObjTemplate.ObjectType.shield )
                    return false;
            }

            int chance = ch.GetSkillChance("shield block");
            victim.PracticeSkill("shield block");

            if (MUDMath.NumberPercent() >= ((chance - ch._level) / 2))
            {
                return false;
            }

            switch( MUDMath.NumberRange( 1, 5 ) )
            {
                case 1:
                    SocketConnection.Act( "You block $n&n's attack with your shield.", ch, null, victim, SocketConnection.MessageTarget.victim );
                    SocketConnection.Act( "$N&n blocks your attack with a shield.", ch, null, victim, SocketConnection.MessageTarget.character );
                    SocketConnection.Act( "$N&n blocks $n&n's attack with a shield.", ch, null, victim, SocketConnection.MessageTarget.room_vict );
                    break;
                case 2:
                    // If we were really smart we would check to see whether both the shield
                    // and weapon were made of metal before we gave a sparks message...
                    SocketConnection.Act( "&+CS&n&+cp&+Car&n&+ck&+Cs&n fly off your shield as you block $n&n's attack.", ch, null, victim, SocketConnection.MessageTarget.victim );
                    SocketConnection.Act( "$N&n defends against your attack with a shield.", ch, null, victim, SocketConnection.MessageTarget.character );
                    SocketConnection.Act( "$N&n deflects $n&n's attack with a shield.", ch, null, victim, SocketConnection.MessageTarget.room_vict );
                    break;
                case 3:
                    SocketConnection.Act("You bring up your shield to block $n&n's attack.", ch, null, victim, SocketConnection.MessageTarget.victim);
                    SocketConnection.Act("$N&n brings up %s shield to block your attack.", ch, null, victim, SocketConnection.MessageTarget.character);
                    SocketConnection.Act("$N&n brings up %s shield to blocks $n&n's attack.", ch, null, victim, SocketConnection.MessageTarget.room_vict);
                    break;
                case 4:
                    SocketConnection.Act("You knock $n&n's attack aside with your shield.", ch, null, victim, SocketConnection.MessageTarget.victim);
                    SocketConnection.Act("$N&n knocks your attack aside with $S shield.", ch, null, victim, SocketConnection.MessageTarget.character);
                    SocketConnection.Act("$N&n knocks $n&n's attack aside with $S shield.", ch, null, victim, SocketConnection.MessageTarget.room_vict);
                    break;
                case 5:
                    SocketConnection.Act("You hear a thud as $n&n's weapon smacks into your shield.", ch, null, victim, SocketConnection.MessageTarget.victim);
                    SocketConnection.Act("Your weapon smacks into $N&n's shield with a thud.", ch, null, victim, SocketConnection.MessageTarget.character);
                    SocketConnection.Act("$n&n's weapon smacks into $N&'s shield with a thud.", ch, null, victim, SocketConnection.MessageTarget.room_vict);
                    break;
                default:
                    break;
            }

            if( ch._fighting == null )
                SetFighting( ch, victim );
            if( victim._fighting == null )
                SetFighting( victim, ch );

            return true;
        }

        /// <summary>
        /// Checks whether the victim is able to dodge the attacker's swing.
        /// </summary>
        /// <param name="ch"></param>
        /// <param name="victim"></param>
        /// <returns></returns>
        public static bool CheckDodge( CharData ch, CharData victim )
        {
            if( !victim.IsAwake() || victim._position < Position.reclining )
                return false;

            if (ch.IsAffected(Affect.AFFECT_DAZZLE))
                return false;

            if( !victim.HasSkill( "dodge" ) )
                return false;

            int chance = victim.GetSkillChance("dodge");

            // Size difference bonus for dodge for halflings - they get 2% dodge
            // bonus per size difference between them and the attacker. -- Xangis
            // Drow get a flat 15% bonus.
            if( victim.GetRace() == Race.RACE_HALFLING )
            {
                if( ch._size > victim._size )
                {
                    chance += 3 * ( ch._size - victim._size );
                }
            }
            else if( victim.HasInnate( Race.RACE_GOOD_DODGE ) )
            {
                chance += 8;
            }
            else if( victim.HasInnate( Race.RACE_BAD_DODGE ) )
            {
                chance -= 3;
            }

            // Bashed mobs/creatures have a hard time dodging
            if( victim._position < Position.fighting )
            {
                chance -= 25;
            }

            // Leap is 16% max at level 50.  Considering crappy thri hitpoints it's necessary.
            if( victim.GetRace() == Race.RACE_THRIKREEN && MUDMath.NumberPercent() <= ( victim._level / 3 ) )
            {
                SocketConnection.Act( "$N&n leaps over your attack.", ch, null, victim, SocketConnection.MessageTarget.character );
                SocketConnection.Act( "You leap over $n&n's attack.", ch, null, victim, SocketConnection.MessageTarget.victim );
                SocketConnection.Act( "$N&n leaps over $n&n's attack.", ch, null, victim, SocketConnection.MessageTarget.room_vict );
                return true;
            }

            victim.PracticeSkill( "dodge" );

            if( MUDMath.NumberPercent() >= chance - ch._level )
                return false;

            switch( MUDMath.NumberRange( 1, 2 ) )
            {
                case 1:
                    SocketConnection.Act( "$N&n dodges your attack.", ch, null, victim, SocketConnection.MessageTarget.character );
                    SocketConnection.Act( "You dodge $n&n's attack.", ch, null, victim, SocketConnection.MessageTarget.victim );
                    SocketConnection.Act( "$N&n dodges $n&n's attack.", ch, null, victim, SocketConnection.MessageTarget.room_vict );
                    break;
                case 2:
                    SocketConnection.Act( "$N&n sidesteps your attack.", ch, null, victim, SocketConnection.MessageTarget.character );
                    SocketConnection.Act( "You narrowly dodge $n&n's attack.", ch, null, victim, SocketConnection.MessageTarget.victim );
                    SocketConnection.Act( "$N&n avoids $n&n's attack.", ch, null, victim, SocketConnection.MessageTarget.room_vict );
                    break;
                default:
                    break;
            }

            if( ch._fighting == null )
                SetFighting( ch, victim );
            if( victim._fighting == null )
                SetFighting( victim, ch );

            return true;
        }



        /// <summary>
        /// Set ch as fighting victim.
        /// </summary>
        /// <param name="ch"></param>
        /// <param name="victim"></param>
        public static void SetFighting( CharData ch, CharData victim )
        {
            if( ch == victim )
                return;

            if( ch._fighting )
            {
                Log.Error( "Set_fighting: already fighting", 0 );
                string buf = String.Format( "...{0} attacking {1} at {2}",
                                            ( ch.IsNPC() ? ch._shortDescription : ch._name ),
                                            ( victim.IsNPC() ? victim._shortDescription : victim._name ),
                                            victim._inRoom.IndexNumber );
                Log.Error( buf, 0 );
                return;
            }

            if( ch.IsAffected( Affect.AFFECT_SLEEP ) )
                ch.RemoveAffect(Affect.AFFECT_SLEEP);

            if( ch._flyLevel != victim._flyLevel )
            {
                StartGrudge( ch, victim, false );
                return;
            }

            ch._fighting = victim;
            if( ch._position == Position.standing )
                ch._position = Position.fighting;

            return;
        }

        /// <summary>
        /// Stops a player from actively participating in combat.  If stopBoth is
        /// true, any players attacking the calling player will also stop fighting.
        /// </summary>
        /// <param name="ch"></param>
        /// <param name="stopBoth"></param>
        public static void StopFighting( CharData ch, bool stopBoth )
        {
            /* If this is true, something is _bad_. */
            if( ch == null )
            {
                Log.Error( "Command.StopFighting: null character as argument!", 0 );
                return;
            }

            /* Always stop the character from fighting. */
            ch._fighting = null;
            if (ch._position == Position.fighting)
            {
                ch._position = Position.standing;
            }

            /* Taking this out of the for loop is MUCH faster. */
            if( stopBoth )
            {
                foreach( CharData fighter in Database.CharList )
                {
                    if( fighter._fighting == ch )
                    {
                        fighter._fighting = null;
                        if (fighter._position == Position.fighting)
                        {
                            fighter._position = Position.standing;
                        }
                        fighter.UpdatePosition();
                    }
                }
            }
            return;
        }

        /// <summary>
        /// Turns a character into a corpse.
        /// 
        /// TODO: Add a corpse to the corpse list when a corpse is created.
        /// TODO: Also remove corpses from the corpse list when a corpse decays.
        /// </summary>
        /// <param name="ch"></param>
        static void MakeCorpse( CharData ch )
        {
            Object corpse;
            string name;
            int corpseIndexNumber;
            int timer;
            int level;

            // Need a bailout here if undead corpses are supposed to disintegrate.
            if( LeavesNoCorpse( ch ) )
            {
                return;
            }

            // Different corpse settings for players and mobs - Xangis
            if( ch.IsNPC() )
            {
                corpseIndexNumber = StaticObjects.OBJECT_NUMBER_CORPSE_NPC;
                name = ch._shortDescription;
                timer = MUDMath.NumberRange( 15, 30 );
                level = ch._level;  // Corpse level
            }
            else
            {
                corpseIndexNumber = StaticObjects.OBJECT_NUMBER_CORPSE_PC;
                name = ch._name;
                timer = MUDMath.NumberRange( 180, 240 ) + ( ch._level * 2 );
                level = ch._level; // Corpse level
            }

            /*
            * This longwinded corpse creation routine comes about because
            * we dont want anything created AFTER a corpse to be placed  
            * INSIDE a corpse.  This had caused crashes from ObjectUpdate()
            * in Object.Extract().
            */

            if( ch.GetCash() > 0 )
            {
                Object coins = Object.CreateMoney( ch.GetCopper(), ch.GetSilver(), ch.GetGold(), ch.GetPlatinum() );
                corpse = Database.CreateObject( Database.GetObjTemplate( corpseIndexNumber ), 0 );
                corpse.AddToObject( coins );
                ch.SetCoins( 0, 0, 0, 0 );
            }
            else
            {
                corpse = Database.CreateObject( Database.GetObjTemplate( corpseIndexNumber ), 0 );
            }

            corpse.Timer = timer;
            corpse.Values[ 0 ] = (int)Race.RaceList[ ch.GetRace() ].BodyParts;

            corpse.Level = level; // corpse level

            string buf = String.Format( "corpse of {0}", name );
            corpse.Name = buf;

            buf = String.Format( corpse.ShortDescription, name );
            corpse.ShortDescription = buf;

            buf = String.Format( corpse.FullDescription, name );
            corpse.FullDescription = buf;

            Object obj;
            for(int i = (ch._carrying.Count - 1); i >= 0; i-- )
            {
                obj = ch._carrying[i];
                
                // Remove items flagged inventory-only from all corpses.
                if (obj.HasFlag(ObjTemplate.ITEM_INVENTORY))
                {
                    Log.Trace("Removing inventory-item " + obj + " from character before creating corpse: " + corpse);
                    obj.RemoveFromWorld();
                }
                else
                {
                    if (ch.IsNPC() && ch._mobTemplate.ShopData
                            && obj.WearLocation == ObjTemplate.WearLocation.none)
                    {
                        obj.RemoveFromChar();
                        obj.RemoveFromWorld();
                    }
                    else
                    {
                        obj.RemoveFromChar();
                        corpse.AddToObject(obj);
                    }
                }
            }

            corpse.AddToRoom( ch._inRoom );
            if( !corpse.InRoom )
            {
                Log.Error( "MakeCorpse: corpse " + corpse.ToString() + " sent to null room, deleting.", 0 );
                corpse.RemoveFromWorld();
            }

            return;
        }

        /*
        * Pad out first if() clause for elementals, etc. that
        * shouldn't leave corpses with appropriate messages.
        */
        static bool LeavesNoCorpse( CharData ch )
        {
            string msg = String.Empty;
            bool noCorpse = false;

            if( ch.IsUndead() )
            {
                noCorpse = true;
                msg = String.Format( "$n&N crumbles to dust." );
            }
            else if( ch.IsElemental() )
            {
                noCorpse = true;
                msg = String.Format( "$n&n returns to the elements from which it formed." );
            }

            if( noCorpse )
            {
                SocketConnection.Act( msg, ch, null, null, SocketConnection.MessageTarget.room );
                if( ch.GetCash() > 0 )
                {
                    Object coins = Object.CreateMoney( ch.GetCopper(), ch.GetSilver(), ch.GetGold(), ch.GetPlatinum() );
                    coins.AddToRoom( ch._inRoom );
                }
                for( int i = (ch._carrying.Count-1); i >= 0; i-- )
                {
                    Object obj = ch._carrying[i];
                    obj.RemoveFromChar();
                    obj.AddToRoom( ch._inRoom );
                }
            }

            return noCorpse;
        }

        /// <summary>
        /// Sends death cry message to current room and nearby rooms.
        /// </summary>
        /// <param name="ch"></param>
        void DeathCry( CharData ch )
        {
            int door;

            string msg = "You hear the animal shriek of $n&n's death cry.";
            Race.Parts parts = Race.RaceList[ ch.GetRace() ].BodyParts;

            if( ch == null )
            {
                Log.Error( "Death cry: death cry called with no arguments for CharData!", 0 );
                return;
            }
            if( !ch._inRoom )
            {
                Log.Error( "Death cry called with ch in no room.", 0 );
                return;
            }

            switch( MUDMath.NumberRange(1, 10))
            {
                default:
                    msg = "$n&n pitches toward the &n&+yground&n... &+Ldead&n.";
                    break;
                case 1:
                    msg = "You hear the animal shriek of $n&n's death cry.";
                    break;
                case 2:
                    msg = "$n&n splatters &n&+rbl&+Ro&n&+ro&+Rd&n all over you.";
                    break;
                case 3:
                    msg = "$n&n expires quietly.";
                    break;
                case 4:
                    msg = "$n&n dies writhing in agony.";
                    break;
            }

            string mesg = String.Format( "{0}", msg );
            SocketConnection.Act( mesg, ch, null, null, SocketConnection.MessageTarget.room );

            if( ch.IsNPC() )
                msg = "&+lA pained and guttural scream assaults your ears.&n";
            else
                msg = "&+rYour heart races as you hear a death cry nearby.&n";

            Room wasInRoom = ch._inRoom;
            for( door = 0; door < Limits.MAX_DIRECTION; door++ )
            {
                Exit exit;

                if( ( exit = wasInRoom.ExitData[ door ] ) && exit.TargetRoom && exit.TargetRoom != wasInRoom )
                {
                    ch._inRoom = Room.GetRoom(exit.IndexNumber);
                    SocketConnection.Act( msg, ch, null, null, SocketConnection.MessageTarget.room );
                }
            }
            ch._inRoom = wasInRoom;

            return;
        }

        /// <summary>
        /// Deliver a killing blow to the victim.
        /// </summary>
        /// <param name="ch"></param>
        /// <param name="victim"></param>
        public static void KillingBlow( CharData ch, CharData victim )
        {
            Event eventdata;
            Room room;
            bool noCorpse = false;

            StopFighting( victim, true );

            if( victim._groupLeader || victim._nextInGroup )
            {
                victim.RemoveFromGroup( victim );
            }
            if( ch != victim )
            {
                if( victim.IsNPC() && victim._mobTemplate.DeathFun.Count > 0 )
                {
                    victim._mobTemplate.DeathFun[0].SpecFunction( victim, MobFun.PROC_NORMAL );
                }
                //        prog_death_trigger( victim );
            }
            if( victim.IsNPC() && victim._deathFun != null )
            {
                noCorpse = victim._deathFun.SpecFunction( victim, MobFun.PROC_DEATH );
            }
            if( !noCorpse )
            {
                MakeCorpse( victim );
            }

            /* Strip all event-spells from victim! */
            for( int i = (Database.EventList.Count - 1); i >= 0; i-- )
            {
                eventdata = Database.EventList[i];

                if( eventdata.Type == Event.EventType.immolate || eventdata.Type == Event.EventType.acid_arrow )
                {
                    if( (CharData)eventdata.Target2 == victim )
                    {
                        Database.EventList.Remove( eventdata );
                    }
                }
            }

            if( victim._rider )
            {
                SocketConnection.Act( "$n&n dies suddenly, and you topple to the &n&+yground&n.", victim, null, victim._rider, SocketConnection.MessageTarget.victim );
                victim._rider._riding = null;
                victim._rider._position = Position.resting;
                victim._rider = null;
            }

            if( victim._riding )
            {
                SocketConnection.Act( "$n&n topples from you, &+Ldead&n.", victim, null, victim._riding, SocketConnection.MessageTarget.victim );
                victim._riding._rider = null;
                victim._riding = null;
            }

            if (!victim.IsNPC() && victim.IsAffected(Affect.AFFECT_VAMP_BITE))
            {
                victim.SetPermRace( Race.RACE_VAMPIRE );
            }

            for (int i = (victim._affected.Count - 1); i >= 0; i--)
            {
                /* Keep the ghoul affect */
                if (!victim.IsNPC() && victim.IsAffected(Affect.AFFECT_WRAITHFORM))
                {
                    continue;
                }

                victim.RemoveAffect(victim._affected[i]);
            }

            if( victim.IsNPC() )
            {
                victim._mobTemplate.NumberKilled++;
                // This may invalidate the char list.
                CharData.ExtractChar( victim, true );
                return;
            }
            CharData.ExtractChar( victim, false );
            //save corpses, don't wait til next save_corpse event
            Database.CorpseList.Save();
            // Character has died in combat, extract them to repop point and put
            // them at the menu.
            /*
                 * Pardon crimes once justice system is complete
                 */
            // This is where we send them to the menu.
            victim.DieFollower( victim._name );

            if( victim._inRoom )
            {
                room = victim._inRoom;
            }
            else
            {
                List<RepopulationPoint> repoplist = victim.GetAvailableRepops();
                if( repoplist.Count < 1 )
                {
                    victim.SendText( "There is no RepopPoint entry for your race and class.  Sending you to limbo.\r\n" );
                    room = Room.GetRoom( StaticRooms.GetRoomNumber("ROOM_NUMBER_START") );
                }
                else
                {
                    // Drop them at the first repop point in the list.  We may want to be fancier about this later, such as dropping them
                    // at the repop for class none if their particular class isn't found.
                    room = Room.GetRoom(repoplist[0].Room);
                    if( !room )
                    {
                        victim.SendText( "The repop point for your race/class does not exist.  Please bug this.  Sending you to limbo.\r\n" );
                        room = Room.GetRoom( StaticRooms.GetRoomNumber("ROOM_NUMBER_START") );
                    }
                    if( !victim.IsNPC() && Room.GetRoom( ( (PC)victim ).CurrentHome ) )
                    {
                        room = Room.GetRoom( ( (PC)victim ).CurrentHome );
                    }
                }
            }
            victim.RemoveFromRoom();
            if( room )
            {
                victim._inRoom = room;
            }

            // Put them in the correct body
            if( victim._socket && victim._socket.Original )
            {
                CommandType.Interpret(victim, "return");
            }

            // Reset reply pointers - handled by CharData.ExtractChar.

            CharData.SavePlayer( victim );

            // Remove from char list: handled by CharData.ExtractChar.

            victim._socket.ShowScreen(ModernMUD.Screen.MainMenuScreen);

            if( victim._socket != null )
            {
                victim._socket._connectionState = SocketConnection.ConnectionState.menu;
            }

            // Just died flag used for safe time after re-login.
            victim.SetActBit( PC.PLAYER_JUST_DIED );

            return;
        }

        static void GroupExperienceGain( CharData ch, CharData victim )
        {
            int highest = 1;

            /*
            * Monsters will now eat a share of the experience in a group if it gets the kill.
            * Dying of mortal wounds or poison doesn't give xp to anyone!
            */
            if( victim == ch )
                return;

            int members = 0;

            // Check for highest level group member and number of members in room
            foreach( CharData groupChar in ch._inRoom.People )
            {
                if (groupChar.IsSameGroup(ch))
                {
                    members++;
                }
                if (groupChar._level > highest)
                {
                    highest = groupChar._level;
                }
            }

            if( members == 0 )
            {
                Log.Error( "GroupExperienceGain: {0} members.", members );
                members = 1;
            }

            if (!victim.IsNPC() && !ch.IsNPC())
            {
                // Player killed a player, check for frags.
                FraglistData.CheckForFrag(ch, victim);
            }
            else if (victim.IsNPC() && !ch.IsNPC())
            {
                // Player killed a mob, check for faction changes.
                AdjustFaction(ch, victim);
            }

            foreach( CharData groupChar in ch._inRoom.People )
            {
                int factor;

                if( !groupChar.IsSameGroup( ch ) || groupChar.IsNPC() )
                    continue;

                // Previously factor was set to be 110% - 10% per group member.  This caused people in groups
                // to get insane high amounts of experience.  This was originally added because players were
                // getting seemingly too little exp in groups larger than two.  This method gives them 94% +
                // 6% per group member of original exp which is then split among the members.
                // This will cause groups with the following number of members to get a certain percentage as
                // compared to the solo person:
                //  1 = 100%   2 = 53%   3 = 37%   4 = 29%   5 = 24%   6 = 21%   7 = 19%   8 = 17%   9 = 16%
                // 10 = 15%   11 = 14%  12 = 13%  13 = 13%  14 = 13%  15 = 13%  16 = 12%  17 = 11%  18 = 11%
                // 19 = 10%   20 = 10%
                // Previously the total amount of experience was split among the group.  This yielded percentages
                // of:
                //  1 = 100%   2 = 50%   3 = 33%   4 = 25%   5 = 20%   6 = 16%   7 = 14%   8 = 12%   9 = 11%
                // 10 = 10%   11 =  9%  12 =  8%  13 =  7%  14 =  7%  15 =  6%  16 =  6%  17 =  5%  18 =  5%
                // 19 =  5%   20 =  5%
                //
                // While this may not seem like a big difference, 3 to 4 person groups have basically gotten a
                // 10-15% increase, while 5-7 person groups have basically gotten a 20-35% increase.
                //
                // This should be quite a bit better, but depending on how the actual results are, this equation
                // may need to be changed so that it may be as high as 90% + 10% per group member of original
                // exp.  However, instead of making a large change, if things don't seem right, try a change
                // of just 1% at first and gauge the results.
                //
                // Since experience is one of the most delicate and precise factors of the MUD, it needs to be
                // changed and tweaked in very small increments.  Please contact Xangis (xangis@yahoo.com)
                // if you plan on making any changes
                //
                // After making a few observations on the math of the experience, I've decided to finalize it at
                // 91 + members * 9 / members.
                //
                // The chart for this is as follows:
                //  1 = 100%   2 = 54%   3 = 39%   4 = 31%   5 = 27%   6 = 24%   7 = 22%   8 = 20%   9 = 19%
                //
                // However we use a different curve for those in groups of 10 or more.  We don't like large
                // groups.
                //
                // We use the curve we were previously using, 94 + members * 6 / members.
                //
                // 10 = 15%   11 = 14%  12 = 13%  13 = 13%  14 = 13%  15 = 13%  16 = 12%  17 = 11%  18 = 11%
                // 19 = 10%   20 = 10%
                //
                // The overall bonus impDescriptor._actFlags of this is that compared to two days ago people will get this
                // percentage more experience:
                //
                // 1 = 0%  2 = 1.8%  3 = 5.4%  4 = 6.9%  5 = 12.5%  6 = 14.3%  7 = 15.8%  8 = 17.6%  9 = 18.8%
                //
                // Groups 10 or larger will not have any difference from 5-24.
                //
                // It seems that people didn't have the skills to handle our exp curve.  Wusses.  Well
                // i've made it quite a _bitvector easier on people in groups of 2-4 and a slight _bitvector easier on
                // groups of 5-9 by using
                //
                // The chart for this is as follows:
                //  1 = 100%   2 = 65%   3 = 53%   4 = 40%   5 = 36%   6 = 28%   7 = 26%   8 = 20%   9 = 19%
                //
                // The overall increases are:
                // 2 = 20.4% 3 = 35.9% 4 = 29.0% 5 = 33.3% 6 = 16.7% 7 = 18.2%

                if (members < 4)
                {
                    factor = (70 + (members * 30)) / members;
                }
                else if (members < 6)
                {
                    factor = (80 + (members * 20)) / members;
                }
                else if (members < 8)
                {
                    factor = (80 + (members * 15)) / members;
                }
                else if (members < 10)
                {
                    factor = (90 + (members * 10)) / members;
                }
                else
                {
                    factor = (94 + (members * 6)) / members;
                }

                int xp = ComputeExperience( groupChar, victim ) * factor / 100;

                // Prevent total cheese leveling with unbalanced groups.  Well, not
                // prevent, but reduce.  A level 10 grouped with a 30 will end up
                // getting 80% of experience.  A level 1 with a 50 will end up
                // getting 51%.  Not enough of a reduction IMHO, but a little might
                // be all that we need.
                /* I increased this 150% as plvling is too easy. */
                if( highest > ( ch._level + 10 ) )
                {
                    xp = ( xp * ( 100 - ( 3 * ( highest - ch._level ) ) / 2 ) ) / 100;
                }

                // Only check Trophy for NPC as victim, killer over level 5,
                // and victim less than 20 levels lower than killer.

                if (victim.IsNPC() && !groupChar.IsNPC() && (groupChar._level > 4) && ((ch._level - victim._level) < 20))
                {
                    xp = (xp * CheckTrophy(groupChar, victim, members)) / 100;
                }

                if( xp == 0 )
                    continue;

                if( ( ch.IsRacewar( victim ) ) && ( !ch.IsNPC() && !groupChar.IsNPC() ) )
                {
                    ch.SendText( "You lost half of the experience by grouping with scum.\r\n" );
                    xp /= 2;
                }

                string buf;
                if (xp > 0)
                {
                    buf = "You receive your portion of the experience.\r\n";
                }
                else
                {
                    buf = "You lose your portion of the experience.\r\n";
                }
                groupChar.SendText( buf );
                if (!groupChar.IsNPC())
                {
                    groupChar.GainExperience(xp);
                }

                foreach( Object obj in groupChar._carrying )
                {
                    if( obj.WearLocation == ObjTemplate.WearLocation.none )
                        continue;

                    if( ( obj.HasFlag( ObjTemplate.ITEM_ANTI_EVIL ) && groupChar.IsEvil() )
                          || ( obj.HasFlag( ObjTemplate.ITEM_ANTI_GOOD ) && groupChar.IsGood() )
                          || ( obj.HasFlag( ObjTemplate.ITEM_ANTI_NEUTRAL ) && groupChar.IsNeutral() ) )
                    {
                        SocketConnection.Act( "&+LYou are wracked with &n&+rp&+Ra&n&+ri&+Rn&+L by&n $p&+L.&n", groupChar, obj, null, SocketConnection.MessageTarget.character );
                        SocketConnection.Act( "$n&+L convulses in &+Cp&n&+ca&+Ci&n&+cn&+L from&n $p&+L.&n", groupChar, obj, null, SocketConnection.MessageTarget.room );
                        obj.RemoveFromChar();
                        obj.AddToRoom( groupChar._inRoom );
                    }
                }
            }

            return;
        }

        /*
        * Compute xp for a kill.
        * Also adjust alignment of killer.
        * Edit this function to change xp computations.
        */
        static int ComputeExperience( CharData killer, CharData victim )
        {
            int percent = 100;
            int sign;
            int alignDir;

            // Uses a semi-exponential table called ExperienceTable.Table
            // no exp is awarded for anything 20 levels below you.
            // exp is reduced by 10% per level of the creature below you
            // for the first 9 levels, and then another 1% for the next 9 levels.
            // It actually counts down starting at 91%.
            /* If victim is lower level */
            if( victim._level < killer._level )
            {
                /* If victim is less than 10 levels below */
                if( killer._level - victim._level < 10 )
                {
                    percent = 101 - ( ( killer._level - victim._level ) * 10 );
                }
                /* If victim is less than 20 levels below */
                else if( killer._level - victim._level < 20 )
                {
                    percent = 20 - ( killer._level - victim._level );
                }
                else
                {
                    percent = 0;
                }
            }
            /* If victim is over 10 levels over */
            else if( victim._level > ( killer._level + 10 ) && killer._level <= 20 )
            {
                // Experience penalty for killing stuff way higher than you, 33 level difference and you
                // get about nothing.
                // Tweaked this slightly, 96 % at 10 levels above, 96% at 10 levels, etc
                percent = 129 - ( ( victim._level - killer._level ) * 4 );
                if( percent < 2 )
                    percent = 2;
            }
            else
                percent += ( victim._level - killer._level );

            if( killer._alignment > 0 )
                sign = 1;
            else
                sign = -1;
            if( victim._alignment > 0 )
                alignDir = -1;
            else
                alignDir = 1;
            int chance = Math.Abs( killer._alignment - victim._alignment ) - sign * killer._alignment;
            chance /= 10;
            if( chance < 0 )
                chance *= -1;
            if( killer._level > victim._level )
                chance -= ( killer._level - victim._level );
            chance = Macros.Range( 0, chance, 100 );

            string lbuf = String.Format( "ComputeExperience: {0} has a {1} chance of gaining {2} align.",
                                         killer._name, chance, alignDir );
            ImmortalChat.SendImmortalChat( null, ImmortalChat.IMMTALK_SPAM, 0, lbuf );
            if( MUDMath.NumberPercent() < chance && killer._alignment != -1000 )
            {
                killer._alignment += alignDir;
                if( killer._alignment <= -1000 )
                    killer.SendText( "&+RThe d&+rarkside t&+lakes over...&n\r\n" );
            }

            killer._alignment = Macros.Range( -1000, killer._alignment, 1000 );

            // 25% bonus for sanctuary
            if (victim.IsAffected(Affect.AFFECT_SANCTUARY))
            {
                percent = ( percent * 5 ) / 4;
            }

            // 10% bonus for fireshield
            if( victim.IsAffected( Affect.AFFECT_FIRESHIELD ) )
            {
                percent = ( percent * 11 ) / 10;
            }

            // 12.5% bonus for an armed opponent
            Object obj = Object.GetEquipmentOnCharacter( victim, ObjTemplate.WearLocation.hand_one );
            if( obj )
            {
                percent = ( percent * 9 ) / 8;
            }

            // 8.25% bonus for secondary weapon
            obj = Object.GetEquipmentOnCharacter( victim, ObjTemplate.WearLocation.hand_two );
            if( obj )
            {
                percent = ( percent * 13 ) / 12;
            }

            // 10% bonus for racially hated mobs
            if( MUDString.NameContainedIn( Race.RaceList[ victim.GetRace() ].Name, Race.RaceList[ killer.GetOrigRace() ].Hate ) )
            {
                percent = ( percent * 11 ) / 10;
            }

            // 10% penalty for killing same race
            if( victim.GetRace() == killer.GetOrigRace() )
            {
                percent = ( percent * 9 ) / 10;
            }

            // Lowbie experience bonus was eliminated since the ExperienceTable.Table
            // made it no longer necessary

            if( victim.IsNPC() )
            {
                // 5% bonus for aggros
                if( victim.HasActBit(MobTemplate.ACT_AGGRESSIVE ) )
                {
                    percent = ( percent * 21 ) / 20;
                }

                // 50% penalty for killing shopkeepers
                if( victim._mobTemplate.ShopData != null )
                    percent = percent / 2;

                // No bonus for special function #1 because they get that by having a class.
                // 10% bonus for each extra special function.
                int count = victim._specFun.Count;
                percent = ( percent * ( 10 + count ) ) / 10;
            }
            else
            {
                // Player-vs-player experience
                // 
                // Killing a level 1-5 makes you lose a small amount of experience
                // and you get no experience for anyone under level 10.
                //
                // Those 11-20 are worth one fifth experience.
                if( victim._level < 6 )
                {
                    killer.SendText( "You killed a newbie!  You feel like a twink!\r\n" );
                    return (victim._level - 6);
                }
                if( victim._level < 11 )
                {
                    return 0;
                }
                if( victim._level < 20 )
                {
                    percent /= 5;
                }
                else
                {
                    percent *= 2;
                }

                if( !killer.IsRacewar( victim ) )
                {
                    if( !killer.HasActBit( PC.PLAYER_BOTTING ))
                    {
                        killer.SendText("You gain no experience for killing your own side.\r\n");
                        return 0;
                    }
                    // Same-side bot kills are worth normal experience.
                    killer.SendText("You gain experience for vanquishing an automaton.\r\n");
                }
                else if( killer.HasActBit( PC.PLAYER_BOTTING ))
                {
                    // Racewar bot kills.  50% bonus exp.
                    killer.SendText("You gain bonus experience for killing an automaton.\r\n");
                    percent = percent * 3/2;
                }
            }

            int xp = ( percent * ExperienceTable.Table[ victim._level ].MobExperience ) / 100;
            xp = Math.Max( 0, xp );
            return xp;
        }

        static void SendSpellDamageMessage( CharData ch, CharData victim, int dam, Spell spell, bool immune )
        {
            string attack = String.Empty;
            string buf1;
            string buf2;
            string buf3;
            string buf4;
            string buf5;

            if( dam < 0 )
            {
                Log.Error( "SendSpellDamageMessage: Negative damage from spell {0}", spell.Name );
                dam = 0;
            }

            if( spell != null )
                attack = spell.Name;

            if( immune )
            {
                buf1 = String.Format( "$N&n seems unaffected by your {0}!", attack );
                buf2 = String.Format( "$n&n's {0} seems powerless against you.", attack );
                buf3 = String.Format( "$N&n seems unaffected by $n&n's {0}!", attack );
                buf4 = String.Format( "Luckily, you are immune to your {0}.", attack );
                buf5 = String.Format( "$n&n seems unaffected by $s own {0}.", attack );
            }
            else if( dam == 0 )
            {
                buf1 = String.Format( "Your {0} misses $N&n.", attack );
                buf2 = String.Format( "$n&n's {0} misses you.", attack );
                buf3 = String.Format( "$N&n avoids being hit by $n&n's {0}!", attack );
                buf5 = String.Format( "Luckily, you are not hit by your {0}.", attack );
                buf4 = String.Format( "$n&n fails to hit $mself with $s own {0}.", attack );
            }
            else
            {
                buf1 = String.Format( spell.MessageDamage );
                buf2 = String.Format( spell.MessageDamageToVictim);
                buf3 = String.Format(spell.MessageDamageToRoom);
                buf4 = String.Format(spell.MessageDamageToSelf);
                buf5 = String.Format(spell.MessageDamageSelfToRoom);
            }

            if( victim != ch )
            {
                SocketConnection.Act( buf1, ch, null, victim, SocketConnection.MessageTarget.character );
                SocketConnection.Act( buf2, ch, null, victim, SocketConnection.MessageTarget.victim );
                SocketConnection.Act( buf3, ch, null, victim, SocketConnection.MessageTarget.room_vict );
            }
            else
            {
                SocketConnection.Act( buf4, ch, null, victim, SocketConnection.MessageTarget.character );
                SocketConnection.Act( buf5, ch, null, victim, SocketConnection.MessageTarget.room );
            }

            return;
        }

        /// <summary>
        /// Sends a damage message to a player.
        /// </summary>
        /// <param name="ch"></param>
        /// <param name="victim"></param>
        /// <param name="damage"></param>
        /// <param name="skill"></param>
        /// <param name="weapon"></param>
        /// <param name="immune"></param>
        static void SendDamageMessage( CharData ch, CharData victim, int damage, string skill, ObjTemplate.WearLocation weapon, bool immune )
        {
            if( victim._position == Position.sleeping )
            {
                SocketConnection.Act( "$n&n has a rude awakening!", victim, null, null, SocketConnection.MessageTarget.room );
                victim._position = Position.resting;
                SetFighting( victim, ch );
            }

            if (skill == "bash")
            {
                SocketConnection.Act( "You send $N&n crashing to the &n&+yground&n with your powerful bash.", ch, null, victim, SocketConnection.MessageTarget.character );
                SocketConnection.Act( "$n&n's powerful bash sends you sprawling!", ch, null, victim, SocketConnection.MessageTarget.victim );
                SocketConnection.Act( "$n&n sends $N&n sprawling with a powerful bash!", ch, null, victim, SocketConnection.MessageTarget.room_vict );
                return;
            }

            if (skill == "headbutt")
            {
                if( ch != victim && damage > victim._hitpoints + 10 )
                {
                    // a killing blow needs some nice verbage
                    SocketConnection.Act( "You swiftly split $N&n's skull with your forehead, sending &+Rblood&n&+r and brains&n flying!",
                        ch, null, victim, SocketConnection.MessageTarget.character );
                    SocketConnection.Act( "$n&n rears back and splits $N&n's skull with $s forehead!", ch, null, victim, SocketConnection.MessageTarget.everyone_but_victim );
                    SocketConnection.Act( "&+RBlood&n&+r and brains&n splatter everywhere as $N&n goes limp and collapses.",
                        ch, null, victim, SocketConnection.MessageTarget.everyone_but_victim );
                    SocketConnection.Act( "$n&n's forehead smashes into you, crushing your skull and bringing on a wave of &+Lblackness&n.",
                        ch, null, victim, SocketConnection.MessageTarget.victim );
                    //deal some decent damage
                }
                else if( ch != victim && damage > 100 )
                {
                    // a pretty nasty headbutt
                    SocketConnection.Act( "Your headbutt smashes into $N&n's skull.", ch, null, victim, SocketConnection.MessageTarget.character );
                    SocketConnection.Act( "$n&n's headbutt smashes into $N&n's skull.", ch, null, victim, SocketConnection.MessageTarget.everyone_but_victim );
                    SocketConnection.Act( "$n&n's head smashes into you, sending you reeling.", ch, null, victim, SocketConnection.MessageTarget.victim );
                    //deal some decent damage
                }
                else if( ch != victim )
                {
                    SocketConnection.Act( "$n&n's headbutt leaves a red welt on $N&n's forehead.", ch,
                        null, victim, SocketConnection.MessageTarget.everyone_but_victim );
                    SocketConnection.Act( "Your headbutt leaves a red welt on $N&n's forehead.", ch,
                        null, victim, SocketConnection.MessageTarget.character );
                    SocketConnection.Act( "$n&n's headbutt leaves a red welt on your forehead.", ch,
                        null, victim, SocketConnection.MessageTarget.victim );
                }

                /* left the damage to self messages in the Command.Headbutt() function
                * because they are a little too tricky to implement through
                * this function */
                return;
            }

            if (skill == "bodyslam" && ch != victim)
            {
                SocketConnection.Act( "You bodyslam $N&n!", ch, null, victim, SocketConnection.MessageTarget.character );
                SocketConnection.Act( "$n&n bodyslams you!\r\nYou are stunned!", ch, null, victim, SocketConnection.MessageTarget.victim );
                SocketConnection.Act( "$n&n bodyslams $N&n.", ch, null, victim, SocketConnection.MessageTarget.room_vict );
            }

            if (skill == "instant kill")
            {
                switch( MUDMath.NumberRange( 1, 3 ) )
                {
                    case 1:
                    case 2:
                        SocketConnection.Act( "You place your weapon in the back of $N&n, resulting in some strange noises, some blood, and a corpse!", ch, null, victim, SocketConnection.MessageTarget.character );
                        SocketConnection.Act( "You realize you should have kept your vital organs somewhere safe as $n&n stabs you to death.", ch, null, victim, SocketConnection.MessageTarget.victim );
                        SocketConnection.Act( "$n&n places $s weapon in the back of $N&n, resulting in some strange noises, some blood, and a corpse!", ch, null, victim, SocketConnection.MessageTarget.room_vict );
                        break;
                    case 3:
                        SocketConnection.Act( "You place your weapon in the back of $N&n with such force that it comes out the other side!", ch, null, victim, SocketConnection.MessageTarget.character );
                        SocketConnection.Act( "$n&n ends your life with a well-placed backstab.", ch, null, victim, SocketConnection.MessageTarget.victim );
                        SocketConnection.Act( "$n&n places $s weapon in the back of $N&n with such force that it comes out the other side!", ch, null, victim, SocketConnection.MessageTarget.room_vict );
                        break;
                }
                return;
            }

            // Multiple backstab messages, feel free to add more.
            if (skill == "backstab" && damage > 0)
            {
                switch( MUDMath.NumberRange( 1, 4 ) )
                {
                    case 1:
                        SocketConnection.Act( "$N&n howls in agony as you pierce $S backbone!", ch, null, victim, SocketConnection.MessageTarget.character );
                        SocketConnection.Act( "You howl in agony as you feel &+rpain&n in your back!", ch, null, victim, SocketConnection.MessageTarget.victim );
                        SocketConnection.Act( "$N&N howls in agony as $n&n pierces $S backbone!", ch, null, victim, SocketConnection.MessageTarget.room_vict );
                        break;
                    case 2:
                        SocketConnection.Act( "You place your $p&n silently and skillfully through the spine of $N&n.", ch, Object.GetEquipmentOnCharacter( ch, weapon ), victim, SocketConnection.MessageTarget.character );
                        SocketConnection.Act( "Your spine feels $p&n neatly slicing through it.", ch, Object.GetEquipmentOnCharacter( ch, weapon ), victim, SocketConnection.MessageTarget.victim );
                        SocketConnection.Act( "$n&n places $s $p&n into $N&n's back!", ch, Object.GetEquipmentOnCharacter( ch, weapon ), victim, SocketConnection.MessageTarget.room_vict );
                        break;
                    case 3:
                        SocketConnection.Act( "Blood flies everywhere as you stab $N&n in the back!", ch, null, victim, SocketConnection.MessageTarget.character );
                        SocketConnection.Act( "You feel a sharp stabbing sensation in your back!", ch, null, victim, SocketConnection.MessageTarget.victim );
                        SocketConnection.Act( "Blood flies everywhere as $n&n places $s $p&n into $N&n's back!", ch, Object.GetEquipmentOnCharacter( ch, weapon ), victim, SocketConnection.MessageTarget.room_vict );
                        break;
                    case 4:
                        SocketConnection.Act( "You smile with perverse pleasure as your $p&n plunges through $N&n's soft tissue, piercing vital organs.", ch, Object.GetEquipmentOnCharacter( ch, weapon ), victim, SocketConnection.MessageTarget.character );
                        SocketConnection.Act( "$n&n smiles with perverse pleasure as $s $p&n plunges through $N&n's soft tissue, piercing vital organs.", ch, Object.GetEquipmentOnCharacter( ch, weapon ), victim, SocketConnection.MessageTarget.victim );
                        SocketConnection.Act( "$n&n smile with perverse pleasure as $s $p&n plunges through $N&n's soft tissue, piercing vital organs.", ch, Object.GetEquipmentOnCharacter( ch, weapon ), victim, SocketConnection.MessageTarget.room_vict );
                        break;

                }
                return;
            }
            if (skill == "poison weapon" || skill == "poison"
                    || skill == "poison bite")
                return;

            string vp1;
            string attack;
            string buf1;
            string buf2;
            string buf3;
            string buf4;
            string buf5;

            // Adjectives based on amount of damage done.
            string adjective = String.Empty;
            if( damage > 100 )
            {
                adjective = " godly";
            }
            else if( damage > 75 )
            {
                adjective = " devastating";
            }
            else if( damage > 55 )
            {
                adjective = " mighty";
            }
            else if( damage > 40 )
            {
                adjective = " awesome";
            }
            else if( damage > 25 )
            {
                adjective = " powerful";
            }
            else if( damage > 4 )
            {
                adjective = String.Empty;
            }  // no message modifier for normal hits
            else if( damage > 2 )
            {
                adjective = " mediocre";
            }
            else if( damage > 0 )
            {
                adjective = " feeble";
            }

            string vp2 = String.Empty;
            if( damage == 0 )
            {
                vp1 = "misses";
                vp2 = String.Empty;
            }
            else
            {
                damage *= 100;
                if( victim._hitpoints > 0 )
                    damage /= victim._hitpoints;

                if( damage <= 1 )
                {
                    vp1 = "scratches";
                }
                else if( damage <= 2 )
                {
                    vp1 = "grazes";
                }
                else if( damage <= 3 )
                {
                    vp1 = "hits";
                }
                else if( damage <= 4 )
                {
                    vp1 = "hits";
                    vp2 = " hard";
                }
                else if( damage <= 5 )
                {
                    vp1 = "hits";
                    vp2 = " very hard";
                }
                else if( damage <= 10 )
                {
                    vp1 = "mauls";
                }
                else if( damage <= 15 )
                {
                    vp1 = "decimates";
                }
                else if( damage <= 20 )
                {
                    vp1 = "makes";
                    vp2 = " stagger in pain";
                }
                else if( damage <= 25 )
                {
                    vp1 = "maims";
                }
                else if( damage <= 30 )
                {
                    vp1 = "mutilates";
                }
                else if( damage <= 40 )
                {
                    vp1 = "disembowels";
                }
                else if( damage <= 50 )
                {
                    vp1 = "eviscerates";
                }
                else if( damage <= 75 )
                {
                    vp1 = "enshrouds";
                    vp2 = " in a mist of blood";
                }
                else
                {
                    vp1 = "beats the crap out of";
                }
            }

            string punct = ( damage <= 40 ) ? "." : "!";

            if (skill == "barehanded fighting")
            {
                if( ch.GetRace() >= Race.RaceList.Length )
                {
                    Log.Error( "SendDamageMessage:  {0} invalid race", ch.GetRace() );
                    ch.SetPermRace( 0 );
                }

                attack = Race.RaceList[ ch.GetRace() ].DamageMessage;

                buf1 = String.Format( "Your{0} {1} {2} $N&n{3}{4}", adjective, attack, vp1, vp2, punct );
                buf2 = String.Format("$n&n's{0} {1} {2} you{3}{4}", adjective, attack, vp1, vp2, punct);
                buf3 = String.Format("$n&n's{0} {1} {2} $N&n{3}{4}", adjective, attack, vp1, vp2, punct);
                buf4 = String.Format("You{0} {1} {2} yourself{3}{4}", adjective, attack, vp1, vp2, punct);
                buf5 = String.Format("$n&n's{0} {1} {2} $m{3}{4}", adjective, attack, vp1, vp2, punct);
            }
            else
            {
                if (!String.IsNullOrEmpty(skill))
                    attack = Skill.SkillList[skill].DamageText;
                else
                {
                    string buf = String.Format( "SendDamageMessage: bad damage type {0} for {1} damage caused by {2} to {3} with weapon {4}.",
                                                skill,
                                                damage,
                                                ch._name,
                                                victim._name,
                                                weapon );
                    Log.Error( buf, 0 );
                    skill = "barehanded fighting";
                    attack = AttackType.Table[ 0 ].Name;
                }

                if( immune )
                {
                    buf1 = String.Format("$N&n seems unaffected by your {0}!", attack);
                    buf2 = String.Format("$n&n's {0} seems powerless against you.", attack);
                    buf3 = String.Format("$N&n seems unaffected by $n&n's {0}!", attack);
                    buf4 = String.Format("Luckily, you seem immune to {0}.", attack);
                    buf5 = String.Format("$n&n seems unaffected by $s own {0}.", attack);
                }
                else
                {
                    if (skill != "barehanded fighting" && IsWieldingPoisoned(ch, weapon))
                    {
                        buf1 = String.Format("Your poisoned {0} {1} $N&n{2}{3}", attack, vp1, vp2, punct);
                        buf2 = String.Format("$n&n's poisoned {0} {1} you{2}{3}", attack, vp1, vp2, punct);
                        buf3 = String.Format("$n&n's poisoned {0} {1} $N&n{2}{3}", attack, vp1, vp2, punct);
                        buf4 = String.Format("Your poisoned {0} {1} you{2}{3}", attack, vp1, vp2, punct);
                        buf5 = String.Format("$n&n's poisoned {0} {1} $m{2}{3}", attack, vp1, vp2, punct);
                    }
                    else
                    {
                        buf1 = String.Format("Your{0} {1} {2} $N&n{3}{4}", adjective, attack, vp1, vp2, punct);
                        buf2 = String.Format("$n&n's{0} {1} {2} you{3}{4}", adjective, attack, vp1, vp2, punct);
                        buf3 = String.Format("$n&n's{0} {1} {2} $N&n{3}{4}", adjective, attack, vp1, vp2, punct);
                        buf4 = String.Format("You{0} {1} {2} yourself{3}{4}", adjective, attack, vp1, vp2, punct);
                        buf5 = String.Format("$n&n's{0} {1} {2} $m{3}{4}", adjective, attack, vp1, vp2, punct);
                    }
                }
            }

            if( victim != ch )
            {
                SocketConnection.Act( buf1, ch, null, victim, SocketConnection.MessageTarget.character, true );
                SocketConnection.Act( buf2, ch, null, victim, SocketConnection.MessageTarget.victim, true );
                SocketConnection.Act( buf3, ch, null, victim, SocketConnection.MessageTarget.room_vict, true );
            }
            else
            {
                SocketConnection.Act( buf4, ch, null, victim, SocketConnection.MessageTarget.character, true );
                SocketConnection.Act( buf5, ch, null, victim, SocketConnection.MessageTarget.room, true );
            }

            return;
        }

        /// <summary>
        /// Test for special abilities based on an attacker's race.
        /// </summary>
        /// <param name="ch"></param>
        /// <param name="victim"></param>
        /// <param name="skill"></param>
        /// <returns></returns>
        static bool CheckRaceSpecial( CharData ch, CharData victim, string skill )
        {
            if( !MUDString.StringsNotEqual( Race.RaceList[ ch.GetRace() ].Name, "Earth Elemental" ) )
            {
                SingleAttack(ch, victim, skill, ObjTemplate.WearLocation.hand_one);
                if( MUDMath.NumberPercent() < 9 )
                {
                    Spell.SpellList["dust blast"].Invoke(ch, ch._level, victim );
                }
                else if( MUDMath.NumberPercent() < 6 )
                {
                    Spell.SpellList["pebble"].Invoke(ch, ch._level, victim );
                }
                else if( MUDMath.NumberPercent() < 3 )
                {
                    Spell.SpellList["dirt cloud"].Invoke(ch, ch._level, victim);
                }
                return true;
            }

            if( !MUDString.StringsNotEqual( Race.RaceList[ ch.GetRace() ].Name, "Fire Elemental" ) )
            {
                SingleAttack(ch, victim, skill, ObjTemplate.WearLocation.hand_one);
                if( MUDMath.NumberPercent() < 9 )
                {
                    Spell.SpellList["fire bolt"].Invoke(ch, ch._level, victim );
                }
                else if( MUDMath.NumberPercent() < 6 )
                {
                    Spell.SpellList["burning hands"].Invoke(ch, ch._level, victim );
                }
                else if( MUDMath.NumberPercent() < 3 )
                {
                    Spell.SpellList["spark"].Invoke(ch, ch._level, victim);
                }
                return true;
            }

            if( !MUDString.StringsNotEqual( Race.RaceList[ ch.GetRace() ].Name, "Air Elemental" ) )
            {
                SingleAttack(ch, victim, skill, ObjTemplate.WearLocation.hand_one);
                if( MUDMath.NumberPercent() < 9 )
                {
                    Spell.SpellList["ice bolt"].Invoke(ch, ch._level, victim);
                }
                else if( MUDMath.NumberPercent() < 6 )
                {
                    Spell.SpellList["cutting breeze"].Invoke(ch, ch._level, victim);
                }
                else if( MUDMath.NumberPercent() < 3 )
                {
                    Spell.SpellList["cutting breeze"].Invoke(ch, ch._level, victim);
                }
                return true;
            }

            if( !MUDString.StringsNotEqual( Race.RaceList[ ch.GetRace() ].Name, "Water Elemental" ) )
            {
                SingleAttack(ch, victim, skill, ObjTemplate.WearLocation.hand_one);
                if( MUDMath.NumberPercent() < 9 )
                {
                    Spell.SpellList["water bolt"].Invoke(ch, ch._level, victim);
                }
                else if( MUDMath.NumberPercent() < 6 )
                {
                    Spell spell = Spell.SpellList["chill touch"];
                    if (spell != null)
                    {
                        spell.Invoke(ch, ch._level, victim);
                    }
                }
                else if( MUDMath.NumberPercent() < 3 )
                {
                    Spell.SpellList["water blast"].Invoke(ch, ch._level, victim);
                }
                return true;
            }

            if( !MUDString.StringsNotEqual( Race.RaceList[ ch.GetRace() ].Name, "Dragon" ) )
            {
                if( MUDMath.NumberPercent() < ch._level )
                {
                    return true;
                }
            }

            return false;
        }

        static void UseMagicalItem( CharData ch )
        {
            Object cobj = null;
            int number = 0;
            int i;

            foreach( Object obj in ch._carrying )
            {
                if( ( obj.ItemType == ObjTemplate.ObjectType.scroll
                         || obj.ItemType == ObjTemplate.ObjectType.wand
                         || obj.ItemType == ObjTemplate.ObjectType.staff
                         || obj.ItemType == ObjTemplate.ObjectType.pill )
                        && MUDMath.NumberRange( 0, number ) == 0 )
                {
                    cobj = obj;
                    number++;
                }
            }

            if (!cobj)
            {
                return;
            }

            switch( cobj.ItemType )
            {
                case ObjTemplate.ObjectType.scroll:
                    for (i = 1; i < 5; i++)
                    {
                        String name = SpellNumberToTextMap.GetSpellNameFromNumber(cobj.Values[i]);
                        // If the spell is not valid, just delete the object.
                        if (String.IsNullOrEmpty(name))
                        {
                            Log.Error("UseMagicalItem: No spell found for spell number " + cobj.Values[i] + " on object " + cobj.ObjIndexNumber + ". Make sure that spell exists in the SpellNumberToTextMap.");
                            cobj.RemoveFromWorld();
                            return;
                        }

                        Spell spell = StringLookup.SpellLookup(name);
                        if (!spell)
                        {
                            Log.Error("UseMagicalItem: Spell '" + name + "' not found for object " + cobj.ObjIndexNumber + ". Make sure that spell exists in the spell file.");
                        }
                        if (!spell || spell.ValidTargets == TargetType.singleCharacterDefensive)
                        {
                            SocketConnection.Act("$n discards a $p.", ch, cobj, null, SocketConnection.MessageTarget.room);
                            cobj.RemoveFromWorld();
                            return;
                        }
                    }
                    break;
                case ObjTemplate.ObjectType.potion:
                case ObjTemplate.ObjectType.pill:
                    for( i = 1; i < 5; i++ )
                    {
                        String name = SpellNumberToTextMap.GetSpellNameFromNumber(cobj.Values[i]);
                        // If the spell is not valid, just delete the object.
                        if (String.IsNullOrEmpty(name))
                        {
                            Log.Error("UseMagicalItem: No spell found for spell number " + cobj.Values[i] + " on object " + cobj.ObjIndexNumber + ". Make sure that spell exists in the SpellNumberToTextMap.");
                            cobj.RemoveFromWorld();
                            return;
                        }

                        Spell spell = StringLookup.SpellLookup(name);
                        if (!spell)
                        {
                            Log.Error("UseMagicalItem: Spell '" + name + "' not found for object " + cobj.ObjIndexNumber + ". Make sure that spell exists in the spell file.");
                        }
                        if (!spell || spell.ValidTargets == TargetType.singleCharacterOffensive)
                        {
                            SocketConnection.Act("$n discards a $p.", ch, cobj, null, SocketConnection.MessageTarget.room);
                            cobj.RemoveFromWorld();
                            return;
                        }
                    }
                    break;
            }

            switch( cobj.ItemType )
            {
                case ObjTemplate.ObjectType.scroll:
                    CommandType.Interpret(ch, "recite scroll");
                    break;
                case ObjTemplate.ObjectType.wand:
                    if( cobj.WearLocation == ObjTemplate.WearLocation.hand_one )
                        CommandType.Interpret(ch, "zap");
                    break;
                case ObjTemplate.ObjectType.staff:
                    if( cobj.WearLocation == ObjTemplate.WearLocation.hand_one )
                        CommandType.Interpret(ch, "brandish");
                    break;
                case ObjTemplate.ObjectType.potion:
                    CommandType.Interpret(ch, "quaff potion");
                    break;
                case ObjTemplate.ObjectType.pill:
                    CommandType.Interpret(ch, "eat " + cobj.Name);
                    break;
            }
            return;

        }

        /// <summary>
        /// Returns the percent of experience player should get for kill
        /// based on their Trophy and increases the amount of kills on
        /// the player's Trophy
        /// </summary>
        /// <param name="ch"></param>
        /// <param name="victim"></param>
        /// <param name="members"></param>
        /// <returns></returns>
        static int CheckTrophy( CharData ch, CharData victim, int members )
        {
            int count;
            int maxlev;
            bool found = false;
            bool found2 = false;

            if( ch.IsNPC() )
                return 0;

            if( !victim.IsNPC() )
                return 100;

            if( ch._level < 5 )
                return 100;

            if( ch._level < Limits.MAX_LEVEL )
            {
                maxlev = ch._level;
            }
            else
            {
                maxlev = Limits.MAX_LEVEL;
            }

            int indexNumber = victim._mobTemplate.IndexNumber;
            if( indexNumber == 0 )
            {
                Log.Error( "Mobile without index number in CheckTrophy!", 0 );
                return 100;
            }

            // If you've found an entry for the mob already just increase the
            // number of kills.
            for( count = 0; count < maxlev; ++count )
            {
                if( ( (PC)ch ).TrophyData[ count ].MobIndexNumber == indexNumber )
                {
                    found = true;
                    ( (PC)ch ).TrophyData[ count ].NumberKilled += ( 100 / members );
                    break;
                }
            }

            // If it's not on the list yet, see if there's a blank slot we could
            // toss it into.
            if( !found )
            {
                for( count = 0; count < maxlev; ++count )
                {
                    if( ( (PC)ch ).TrophyData[ count ].MobIndexNumber == 0
                            || ( (PC)ch ).TrophyData[ count ].NumberKilled == 0 )
                    {
                        ( (PC)ch ).TrophyData[ count ].MobIndexNumber = indexNumber;
                        ( (PC)ch ).TrophyData[ count ].NumberKilled = ( 100 / members );
                        found2 = true;
                        return 100;
                    }
                }
            }

            // No blank slot or previous entry, roll the oldest item off of Trophy
            // to make room for the newest one.
            // we cycle from the bottom to the top, moving them all up a notch, then
            // we replace the last one with the new entry
            if( !found && !found2 )
            {
                for( count = 0; count < ( maxlev - 1 ); ++count )
                {
                    ( (PC)ch ).TrophyData[ count ].MobIndexNumber = ( (PC)ch ).TrophyData[ count + 1 ].MobIndexNumber;
                    ( (PC)ch ).TrophyData[ count ].NumberKilled = ( (PC)ch ).TrophyData[ count + 1 ].NumberKilled;
                }
                ( (PC)ch ).TrophyData[ maxlev - 1 ].MobIndexNumber = indexNumber;
                ( (PC)ch ).TrophyData[ maxlev - 1 ].NumberKilled = ( 100 / members );
                return 100;
            }

            int percent = 100 - ( ( (PC)ch ).TrophyData[ count ].NumberKilled / 40 );
            if( percent < 1 )
                percent = 1;
            if( percent <= 50 )
                ch.SendText( "What's the point anymore?  It just doesen't seem worth it.\r\n" );
            else if( percent <= 80 )
                ch.SendText( "This is starting to get dull.\r\n" );
            else if( percent <= 90 )
                ch.SendText( "You are beginning to learn your victim's weak spots.\r\n" );

            return percent;
        }


        /// <summary>
        /// Processes a blur-type attack.  Returns true if the victim died.
        /// </summary>
        /// <param name="ch"></param>
        /// <param name="victim"></param>
        /// <returns></returns>
        public static bool BlurAttack( CharData ch, CharData victim )
        {
            if (ch == null) return false;

            int numAttacks;
            int count;

            if( ch.IsAffected( Affect.AFFECT_CASTING ) )
            {
                return false;
            }

            if( ch.IsAffected( Affect.AFFECT_BLUR ) && MUDMath.NumberPercent() < 25 )
            {
                SocketConnection.Act( "$n&n moves with a BLUR of speed!", ch, null, null, SocketConnection.MessageTarget.room );
                SocketConnection.Act( "You move with a BLUR of speed!", ch, null, null, SocketConnection.MessageTarget.character );
                for( count = 0, numAttacks = 4; count < numAttacks && victim._position > Position.dead; ++count )
                {
                    SingleAttack( ch, victim, String.Empty, ObjTemplate.WearLocation.hand_one );
                }
            }
            else
            {
                Object wield;

                if( MUDMath.NumberPercent() > 10 )
                    return false;
                if( ch.IsClass(CharClass.Names.hunter) || ch.IsClass(CharClass.Names.ranger ))
                    numAttacks = 2;
                else if( ch.GetRace() == Race.RACE_OGRE || ch.GetRace() == Race.RACE_CENTAUR )
                    numAttacks = 4;
                else
                    numAttacks = 9;
                if( MUDMath.NumberPercent() < ch.GetCurrLuck() )
                    numAttacks++;
                if( MUDMath.NumberPercent() < victim.GetCurrLuck() )
                    numAttacks--;

                /* 9716 is the index number for the dagger of the wind. */
                // HORRIBLE HORRIBLE TODO: FIXME: BUG: Never hard-code item index numbers.
                if( ( wield = Object.GetEquipmentOnCharacter( ch, ObjTemplate.WearLocation.hand_one ) ) && wield.ObjIndexData.IndexNumber == 9716 )
                {
                    SocketConnection.Act( "&+c$n&+c's $p&n &+cbegins to move with the &+Wspeed&+c of a &+lstorm&+c!&n", ch, wield, null, SocketConnection.MessageTarget.room );
                    SocketConnection.Act( "Your $p&n &+cbegins to move with the &+Wspeed&+c of a &+lstorm&+c!&n", ch, wield, null, SocketConnection.MessageTarget.character );
                    for( count = 0; count < numAttacks && victim._position > Position.dead; ++count )
                    {
                        SingleAttack(ch, victim, String.Empty, ObjTemplate.WearLocation.hand_one);
                    }
                    return ( victim._position > Position.dead );
                }
                if( ( wield = Object.GetEquipmentOnCharacter( ch, ObjTemplate.WearLocation.hand_two ) ) && wield.ObjIndexData.IndexNumber == 9716 )
                {
                    SocketConnection.Act( "&+c$n&+c's $p&n &+cbegins to move with the &+Wspeed&+c of a &+lstorm&+c!&n", ch, wield, null, SocketConnection.MessageTarget.room );
                    SocketConnection.Act( "Your $p&n &+cbegins to move with the &+Wspeed&+c of a &+lstorm&+c!&n", ch, wield, null, SocketConnection.MessageTarget.character );
                    for( count = 0; count < numAttacks && victim._position > Position.dead; ++count )
                    {
                        SingleAttack(ch, victim, String.Empty, ObjTemplate.WearLocation.hand_two);
                    }
                    return ( victim._position > Position.dead );
                }
                if( ( wield = Object.GetEquipmentOnCharacter( ch, ObjTemplate.WearLocation.hand_three ) ) && wield.ObjIndexData.IndexNumber == 9716 )
                {
                    SocketConnection.Act( "&+c$n&+c's $p&n &+cbegins to move with the &+Wspeed&+c of a &+lstorm&+c!&n", ch, wield, null, SocketConnection.MessageTarget.room );
                    SocketConnection.Act( "Your $p&n &+cbegins to move with the &+Wspeed&+c of a &+lstorm&+c!&n", ch, wield, null, SocketConnection.MessageTarget.character );
                    for( count = 0; count < numAttacks && victim._position > Position.dead; ++count )
                    {
                        SingleAttack(ch, victim, String.Empty, ObjTemplate.WearLocation.hand_three);
                    }
                    return ( victim._position > Position.dead );
                }
                if( ( wield = Object.GetEquipmentOnCharacter( ch, ObjTemplate.WearLocation.hand_four ) ) && wield.ObjIndexData.IndexNumber == 9716 )
                {
                    SocketConnection.Act( "&+c$n&+c's $p&n &+cbegins to move with the &+Wspeed&+c of a &+lstorm&+c!&n", ch, wield, null, SocketConnection.MessageTarget.room );
                    SocketConnection.Act( "Your $p&n &+cbegins to move with the &+Wspeed&+c of a &+lstorm&+c!&n", ch, wield, null, SocketConnection.MessageTarget.character );
                    for( count = 0; count < numAttacks && victim._position > Position.dead; ++count )
                    {
                        SingleAttack(ch, victim, String.Empty, ObjTemplate.WearLocation.hand_four);
                    }
                    return ( victim._position > Position.dead );
                }
            }
            return false;
        }

        /// <summary>
        /// Checks for magic resistance, also known as "shrug" -- as in "shrugging off the effects".
        /// </summary>
        /// <param name="ch"></param>
        /// <param name="victim"></param>
        /// <returns></returns>
        public static bool CheckShrug( CharData ch, CharData victim )
        {
            int chance;

            if( !victim.HasInnate( Race.RACE_SHRUG ) )
            {
                return false;
            }

            switch( victim.GetRace() )
            {   // Default at 99 to show races not listed here.
                case Race.RACE_GREYELF:
                    chance = 25;
                    break;
                case Race.RACE_GITHZERAI:
                    chance = 10;
                    break;
                case Race.RACE_DROW:
                    chance = 40;
                    break;
                case Race.RACE_RAKSHASA:
                    chance = 15;
                    break;
                case Race.RACE_GITHYANKI:
                    chance = 20;
                    break;
                // Demons, devils, etc.
                default:
                    chance = 25 + ( victim._level ) / 2;
                    break;
            }

            if( MUDMath.NumberPercent() < chance )
            {
                SocketConnection.Act( "&+MYour spell flows around &n$N&+M, leaving $M unharmed!&n", ch, null, victim, SocketConnection.MessageTarget.character );
                SocketConnection.Act( "&+M$N&+M's spell flows around you, leaving you unharmed!&n", victim, null, ch, SocketConnection.MessageTarget.character );
                SocketConnection.Act( "&+M$N&+M's spell flows around $n&+M, leaving $m unharmed!&n", victim, null, ch, SocketConnection.MessageTarget.room_vict );
                return true;
            }
            return false;
        }

        public static bool CheckVicious( CharData ch, CharData victim )
        {
            if( ch.HasActBit(PC.PLAYER_VICIOUS ) )
                return true;
            if( victim._position < Position.reclining
                    && victim._position != Position.stunned )
                return false;
            return true;
        }

        public static bool CheckAggressive( CharData ch, CharData victim )
        {
            if( ch == null )
            {
                Log.Error( "CheckAggressive: called with null ch.", 0 );
                return false;
            }
            if( victim == null )
            {
                Log.Error( "CheckAggressive: called with null victim.", 0 );
                return false;
            }

            if (!victim.IsAggressive(ch))
            {
                return false;
            }
            if( !ch.IsNPC() && ( (PC)ch ).AggressiveLevel >= 1 && ( (PC)ch ).AggressiveLevel < ch._hitpoints && ch._position == Position.standing
                    && !ch.IsAffected( Affect.AFFECT_CASTING ) )
            {
                ch.SendText( "You charge aggressively at your foe!\r\n" );
                if (ch.IsClass(CharClass.Names.thief) || ch.IsClass(CharClass.Names.assassin)
                        || ch.IsClass(CharClass.Names.bard) || ch.IsClass(CharClass.Names.mercenary))
                {
                    Backstab(ch, victim);
                    SetFighting(victim, ch);
                }
                else
                {
                    CombatRound(ch, victim, String.Empty);
                }
                return true;
            }
            return false;
        }

        /// <summary>
        /// Attempt to disarm a victim.  Attacker must make a successful attack.
        /// </summary>
        /// <param name="ch"></param>
        /// <param name="victim"></param>
        public static void Disarm(CharData ch, CharData victim)
        {
            if ((ch._size - victim._size) < -2)
            {
                return;
            }

            Object obj = Object.GetEquipmentOnCharacter(victim, ObjTemplate.WearLocation.hand_one);
            if (!obj)
            {
                obj = Object.GetEquipmentOnCharacter(victim, ObjTemplate.WearLocation.hand_two);
                if (!obj)
                {
                    return;
                }
            }

            if (obj.HasFlag(ObjTemplate.ITEM_NODROP))
            {
                return;
            }

            if (!Object.GetEquipmentOnCharacter(ch, ObjTemplate.WearLocation.hand_one)
                    && !Object.GetEquipmentOnCharacter(ch, ObjTemplate.WearLocation.hand_two)
                    && MUDMath.NumberBits(1) == 0)
            {
                return;
            }

            SocketConnection.Act("You disarm $N!", ch, null, victim, SocketConnection.MessageTarget.character);
            SocketConnection.Act("$n DISARMS you!", ch, null, victim, SocketConnection.MessageTarget.victim);
            SocketConnection.Act("$n DISARMS $N!", ch, null, victim, SocketConnection.MessageTarget.room_vict);

            obj.RemoveFromChar();
            // obj.AddToRoom( victim.in_room ); // Removed drop in room
            obj.ObjToChar(victim); // put it in inventory instead
            string lbuf = String.Format("Disarm: {0} disarmed by {1}.", victim._name, ch._name);
            ImmortalChat.SendImmortalChat(null, ImmortalChat.IMMTALK_SPAM, 0, lbuf);
            return;
        }

        /// <summary>
        /// Attempt to trip a creature.  Attacker must make a successful attack in order to succeed.
        /// </summary>
        /// <param name="ch"></param>
        /// <param name="victim"></param>
        void Trip(CharData ch, CharData victim)
        {
            if (victim.CanFly())
            {
                return;
            }

            if (victim._riding)
            {
                if (victim._riding.CanFly())
                {
                    return;
                }

                SocketConnection.Act("$n trips your mount and you crash to the &n&+yground&n!", ch, null, victim, SocketConnection.MessageTarget.victim);
                SocketConnection.Act("You trip $N's mount and $N is thrown off!", ch, null, victim, SocketConnection.MessageTarget.character);
                SocketConnection.Act("$n trips $N's mount and $N falls off!", ch, null, victim, SocketConnection.MessageTarget.room_vict);
                victim._riding._rider = null;
                victim._riding = null;

                ch.WaitState(2 * Event.TICK_COMBAT);
                victim.WaitState(2 * Event.TICK_COMBAT);
                victim._position = Position.resting;
                return;
            }

            if (victim._wait == 0)
            {
                SocketConnection.Act("You trip $N and $N goes down!", ch, null, victim, SocketConnection.MessageTarget.character);
                SocketConnection.Act("$n trips you and you topple like a pile of bricks!", ch, null, victim, SocketConnection.MessageTarget.victim);
                SocketConnection.Act("$n trips $N and $N's face meets the &nearth&n!", ch, null, victim, SocketConnection.MessageTarget.room_vict);

                ch.WaitState(2 * Event.TICK_COMBAT);
                victim.WaitState(2 * Event.TICK_COMBAT);
                victim._position = Position.resting;
            }

            return;
        }

        /// <summary>
        /// Check tumble skill to see whether an attack is avoided.
        /// </summary>
        /// <param name="ch"></param>
        /// <returns></returns>
        public static bool CheckTumble( CharData ch )
        {
            if (ch == null) return false;

            int chance;

            if( !ch.HasSkill( "tumble" ) )
                return false;

            if( ch.IsNPC() )
                chance = ch._level / 2 + 8;
            else
                chance = ( (PC)ch ).SkillAptitude[ "tumble" ] / 3;

            chance += ch.GetCurrAgi() / 15;

            ch.PracticeSkill( "tumble" );

            if( MUDMath.NumberPercent() >= chance )
                return false;

            return true;
        }

        static void StopNotVicious( CharData victim )
        {
            foreach( CharData niceCh in victim._inRoom.People )
            {
                if (!niceCh.HasActBit(PC.PLAYER_VICIOUS)
                        && niceCh._fighting == victim )
                    StopFighting( niceCh, false );
            }
        }

        /*
        * To be used only with skills, not spells.
        */
        static void InflictPoison( string name, int level, bool type, CharData ch, CharData victim )
        {
            Affect af = new Affect();

            if( Magic.SpellSavingThrow( level, victim, AttackType.DamageType.poison ) )
                return;

            int typ;
            if (type)
                typ = 1;
            else
                typ = 0;

            af.Value = name;
            af.Type = Affect.AffectType.skill;
            af.Level = level;
            af.Duration = level / 4;
            af.AddModifier(Affect.Apply.none, typ );
            af.SetBitvector( Affect.AFFECT_POISON );
            victim.CombineAffect(af);

            if( ch != victim )
                ch.SendText( "You have poisoned your victim.\r\n" );
            victim.SendText( "&+GYou don't feel very well.&n\r\n" );
            return;
        }

        /* Check to see if mob should shout for protection.  This applies to only
        * 2 plane gods atm, but you can add others.  The call for this function
        * should be within an if statement checking IS_NPC and valid index number of the
        * NPC.  It'll make things a lot faster than calling it too often.
        */
        static bool CheckShout( CharData ch, CharData victim )
        {
            if (ch == null || victim == null) return false;

            string buf;
            CharData minion;

            if( !ch.IsNPC() || victim.IsNPC() || !ch._mobTemplate )
                return false;

            switch( ch._mobTemplate.IndexNumber )
            {
                case 9316:
                    buf = String.Format( "Denizens of the Fire Plane, come slay {0}!",
                              victim._name );
                    break;
                case 9748:
                    buf = String.Format( "Denizens of the Air Plane, come slay {0}!",
                              victim._name );
                    break;
                default:
                    buf = String.Format( "Someone kill {0}!", victim._name );
                    return false;
            }

            CommandType.Interpret(ch, "shout " + buf);

            foreach( CharData it in Database.CharList )
            {
                minion = it;
                /* Minion must be a NPC from the right plane and on that plane */
                if( !minion.IsNPC()
                        || minion._inRoom.Area != ch._inRoom.Area
                        || minion._mobTemplate.Area != ch._inRoom.Area )
                    continue;

                /* This is a ranged call to start_grudge */
                StartGrudge( minion, victim, true );
            }

            return true;
        }

        static bool IsBossMob( CharData ch )
        {
            int i;
            // TODO: FIXME: BUG: Never Hard-code numbers!
            int[] list = new[] { 9511, 20527, 20537, 0 };

            if( !ch.IsNPC() )
                return false;
            for( i = 0; list[ i ] > 0; i++ )
            {
                if( list[ i ] == ch._mobTemplate.IndexNumber )
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Calculates the number of points awarded for killing a foe.  Killing a player will
        /// be worth 10x more than killing a mob.
        /// </summary>
        /// <param name="killer"></param>
        /// <param name="victim"></param>
        /// <returns></returns>
        static double CalculateKillScore( CharData killer, CharData victim )
        {
            if( killer == null || victim == null )
                return 0.0;

            // The scale is 1.0x victim level +/- 5% for each of the first five levels
            // of difference and 2% for each additional level of difference.

            double ratio;
            int leveldiff = victim._level - killer._level;

            if( leveldiff <= 5 && leveldiff >= -5 )
            {
                ratio = 1.0 + ( leveldiff * 0.05 );
            }
            // victim is more than 5 levels higher than killer.
            else if( leveldiff > 5 )
            {
                ratio = 1.25 + ( ( leveldiff - 5 ) * 0.02 );
            }
            // victim is more than 5 levels below killer.
            else
            {
                ratio = 0.75 + ( ( leveldiff + 5 ) * 0.02 );
            }

            if( !victim.IsNPC() )
                ratio *= 10;

            return victim._level * ratio;
        }

        /// <summary>
        /// Calculates the number of points to subtract from a player's score after
        /// they have been killed.
        /// 
        /// A death to a player or a mob is worth the same amount of point loss.
        /// </summary>
        /// <param name="killer"></param>
        /// <param name="victim"></param>
        /// <returns></returns>
        static double CalculateDeathScore( CharData killer, CharData victim )
        {
            double score = ( 0.0 - CalculateKillScore( killer, victim ) );

            if( victim.IsNPC() )
                return score * 10.0;
            return score; // player is already multiplied by 10x in the CalculateKillScore function.
        }


        public static void ApplyPoison( CharData ch )
        {
            Affect af = new Affect();
            bool isSpell = false;

            foreach (Affect aff in ch._affected)
            {
                if( aff.Type == Affect.AffectType.spell && aff.Value == "poison" )
                {
                    isSpell = true;
                }
                else if ((aff.Type == Affect.AffectType.skill && aff.Value == "poison weapon" || aff.Value == "poison bite") )
                {
                    foreach (AffectApplyType apply in aff.Modifiers)
                    {
                        if (apply.Location != Affect.Apply.none || ch.IsAffected(Affect.AFFECT_SLOW_POISON) && MUDMath.NumberBits(1) == 0)
                            continue;
                        Poison.Type poisonType = (Poison.Type)apply.Amount;
                        int dam;
                        switch (poisonType)
                        {
                            case Poison.Type.damage:
                                SocketConnection.Act("$n&n goes into a brief siezure as the poison courses through $s body.", ch, null, null, SocketConnection.MessageTarget.room);
                                ch.SendText("Your muscles twitch randomly as the poison courses through your body.\r\n");
                                dam = MUDMath.Dice(1, 10);
                                if (!Magic.SpellSavingThrow(aff.Level, ch, AttackType.DamageType.poison))
                                    InflictDamage(ch, ch, dam, "poison weapon", ObjTemplate.WearLocation.none, AttackType.DamageType.poison);
                                else
                                    InflictDamage(ch, ch, dam / 2, "poison weapon", ObjTemplate.WearLocation.none, AttackType.DamageType.poison);
                                return;
                            case Poison.Type.attributes:
                                if (!Magic.SpellSavingThrow(aff.Level, ch, AttackType.DamageType.poison))
                                {
                                    int lev = aff.Level;
                                    ch.AffectStrip(Affect.AffectType.skill, "poison weapon");
                                    af.Type = Affect.AffectType.skill;
                                    af.Value = "poison";
                                    af.Duration = lev / 4;
                                    af.Level = lev;
                                    af.AddModifier(Affect.Apply.strength, 0 - MUDMath.Dice(1, 20));
                                    af.AddModifier(Affect.Apply.dexterity, 0 - MUDMath.Dice(1, 20));
                                    af.AddModifier(Affect.Apply.agility, 0 - MUDMath.Dice(1, 20));
                                    af.AddModifier(Affect.Apply.constitution, 0 - MUDMath.Dice(1, 20));
                                    af.SetBitvector(Affect.AFFECT_POISON);
                                    ch.AddAffect(af);
                                    ch.SendText("You suddenly feel quite weak as the poison is distributed through your body.&n\r\n");
                                    SocketConnection.Act("$n&n pales visibly and looks much weaker.", ch, null, null, SocketConnection.MessageTarget.room);
                                    return;
                                }
                                ch.SendText("You feel the poison working its way into your system.\r\n");
                                InflictDamage(ch, ch, 2, "poison weapon", ObjTemplate.WearLocation.none, AttackType.DamageType.poison);
                                break;
                            case Poison.Type.damage_major:
                                dam = MUDMath.Dice(10, 10);
                                SocketConnection.Act("$n&n screams in agony as the poison courses through $s body.", ch, null, null, SocketConnection.MessageTarget.room);
                                ch.SendText("&+RYour blood is on fire!&n\r\n");

                                if (!Magic.SpellSavingThrow(aff.Level, ch, AttackType.DamageType.poison))
                                    InflictDamage(ch, ch, dam, "poison weapon", ObjTemplate.WearLocation.none, AttackType.DamageType.poison);
                                else
                                    InflictDamage(ch, ch, dam / 2, "poison weapon", ObjTemplate.WearLocation.none, AttackType.DamageType.poison);
                                return;
                            case Poison.Type.minor_para:
                                if (!Magic.SpellSavingThrow(aff.Level, ch, AttackType.DamageType.poison))
                                {
                                    ch.AffectStrip(Affect.AffectType.skill, "poison_weapon");
                                    af.Value = "poison";
                                    af.Type = Affect.AffectType.skill;
                                    af.Duration = MUDMath.NumberRange(1, 10);
                                    af.SetBitvector(Affect.AFFECT_MINOR_PARA);
                                    ch.AddAffect(af);
                                    ch.SendText("&+YYou are paralyzed!&n\r\n");
                                    StopFighting(ch, false);
                                    SocketConnection.Act("$n&n&+y is suddenly overcome with rigor and cannot move.&n",
                                        ch, null, null, SocketConnection.MessageTarget.room);
                                }
                                break;
                            case Poison.Type.minor_para_extended:
                                if (!Magic.SpellSavingThrow(aff.Level, ch, AttackType.DamageType.poison))
                                {
                                    ch.AffectStrip(Affect.AffectType.skill, "poison_weapon");
                                    af.Value = "poison";
                                    af.Type = Affect.AffectType.skill;
                                    af.Duration = MUDMath.NumberRange(5, 30);
                                    af.SetBitvector(Affect.AFFECT_MINOR_PARA);
                                    ch.AddAffect(af);
                                    ch.SendText("&+YYou are paralyzed!&n\r\n");
                                    StopFighting(ch, false);
                                    SocketConnection.Act("$n&n&+y is suddenly overcome with rigor and cannot move.&n",
                                        ch, null, null, SocketConnection.MessageTarget.room);
                                }
                                break;
                            case Poison.Type.major_para:
                                if (!Magic.SpellSavingThrow(aff.Level, ch, AttackType.DamageType.poison))
                                {
                                    ch.AffectStrip(Affect.AffectType.skill, "poison_weapon");
                                    af.Value = "poison";
                                    af.Type = Affect.AffectType.skill;
                                    af.Duration = MUDMath.NumberRange(1, 10);
                                    af.SetBitvector(Affect.AFFECT_HOLD);
                                    ch.AddAffect(af);
                                    ch.SendText("&+YYou are paralyzed!&n\r\n");
                                    StopFighting(ch, false);
                                    SocketConnection.Act("$n&n&+y is suddenly overcome with rigor and cannot move.&n",
                                        ch, null, null, SocketConnection.MessageTarget.room);
                                }
                                break;
                            case Poison.Type.major_para_extended:
                                if (!Magic.SpellSavingThrow(aff.Level, ch, AttackType.DamageType.poison))
                                {
                                    ch.AffectStrip(Affect.AffectType.skill, "poison_weapon");
                                    af.Value = "poison";
                                    af.Type = Affect.AffectType.skill;
                                    af.Duration = MUDMath.NumberRange(5, 30);
                                    af.SetBitvector(Affect.AFFECT_HOLD);
                                    ch.AddAffect(af);
                                    ch.SendText("&+YYou are paralyzed!&n\r\n");
                                    StopFighting(ch, false);
                                    SocketConnection.Act("$n&n&+y is suddenly overcome with rigor and cannot move.&n",
                                        ch, null, null, SocketConnection.MessageTarget.room);
                                }
                                break;
                            case Poison.Type.perm_constitution:
                                // TODO: Implement perm constitution loss from poison.
                                break;
                            case Poison.Type.perm_hitpoints:
                                // TODO: Implement perm hitpoint loss from poison.
                                break;
                            case Poison.Type.near_death:
                                // TODO: Implement "near death" poison - one that drains HP very quickly but does not kill.
                                break;
                            default:
                                Log.Error("Unimplemented or unknown poison type: " + poisonType.ToString());
                                break;
                        }
                    }
                } //end if
            }
            if( isSpell )
            {
                //normal poison from the spell
                ch.SendText( "You shiver and suffer.\r\n" );
                SocketConnection.Act( "$n&n shivers and suffers.", ch, null, null, SocketConnection.MessageTarget.room );
                if (!ch.IsAffected(Affect.AFFECT_SLOW_POISON))
                {
                    InflictSpellDamage( ch, ch, 2, "poison", AttackType.DamageType.poison );
                }
                else
                {
                    // Slow poison gives them a 20% chance of avoiding damage
                    // and does half damage when it does hit them, giving
                    // them 40% as much damage overall as a non-slowed person
                    if( MUDMath.NumberPercent() < 80 )
                        InflictSpellDamage( ch, ch, 1, "poison", AttackType.DamageType.poison );
                }
            }
            else
            {
                //normal poison from a bite or weapon
                ch.SendText( "You shiver and suffer.\r\n" );
                SocketConnection.Act( "$n&n shivers and suffers.", ch, null, null, SocketConnection.MessageTarget.room );
                if (!ch.IsAffected(Affect.AFFECT_SLOW_POISON))
                {
                    InflictDamage( ch, ch, 2, "poison", ObjTemplate.WearLocation.none, AttackType.DamageType.poison );
                }
                else
                {
                    // Slow poison gives them a 20% chance of avoiding damage
                    // and does half damage when it does hit them, giving
                    // them 40% as much damage overall as a non-slowed person
                    if( MUDMath.NumberPercent() < 80 )
                        InflictDamage( ch, ch, 1, "poison", ObjTemplate.WearLocation.none, AttackType.DamageType.poison );
                }
            }

        }

        /// <summary>
        /// At 100% and 80k hps, does 600 minimum, 900 maximum.
        /// 
        /// The breath is intended to get weaker as the dragon goes down in hitpoints.
        /// </summary>
        /// <param name="ch"></param>
        /// <param name="percent"></param>
        /// <returns></returns>
        public static int DragonBreathDamage(CharData ch, int percent)
        {
            if (percent < 10)
                percent = 10;
            if (percent > 100)
                percent = 100;
            int dam = ch._hitpoints / 267 + MUDMath.NumberRange(ch._hitpoints / 267, ch._hitpoints / 133);
            dam = dam * percent / 100;
            //    if ( dam > 900 ) dam = 900;
            string lbuf = String.Format("{0}&n breathing for {1} base damage", ch._shortDescription, dam);
            Log.Trace(lbuf);

            return dam;
        }


        /// <summary>
        /// When player ch kills victim, adjusts faction standings of the player, player's guild,
        /// and the player's race.
        /// 
        /// Keep in mind that _raceFaction values are "how they feel about us" values and not
        /// "how we feel about them" values.
        /// </summary>
        /// <param name="ch"></param>
        /// <param name="victim"></param>
        public static void AdjustFaction(CharData ch, CharData victim)
        {
            // Faction hits can only be had when a player kills a mob.
            if (ch == null || victim == null || ch.IsNPC() || !victim.IsNPC())
            {
                return;
            }

            if (Macros.IsSet((int)Database.SystemData.ActFlags, (int)Sysdata.MudFlags.disablefaction))
            {
                return;
            }

            if (Macros.IsSet((int)Database.SystemData.ActFlags, (int)Sysdata.MudFlags.checkfactionbit) &&
                victim.HasActBit(MobTemplate.ACT_FACTION))
            {
                return;
            }

            // First we do player faction.
            PC pc = (PC)ch;
            int race = victim.GetOrigRace();
            pc.RaceFaction[race] -= 1.0;
            if (pc.RaceFaction[race] < Limits.MIN_FACTION)
            {
                pc.RaceFaction[race] = Limits.MIN_FACTION;
            }

            // Now we do guild faction at 1/10 the strength.
            if (pc.GuildMembership != null)
            {
                pc.GuildMembership.RaceFactionStandings[race] -= 0.1;
                if (pc.GuildMembership.RaceFactionStandings[race] < Limits.MIN_FACTION)
                {
                    pc.GuildMembership.RaceFactionStandings[race] = Limits.MIN_FACTION;
                }
            }

            // Now we do race faction at 1/100 the strength.
            int pcrace = pc.GetOrigRace();
            Race.RaceList[pcrace].RaceFaction[race] -= 0.01;
            if (Race.RaceList[pcrace].RaceFaction[race] < Limits.MIN_FACTION)
            {
                Race.RaceList[pcrace].RaceFaction[race] = Limits.MIN_FACTION;
            }

            // Now we check rival races to see whether we get any faction bonuses.
            for( int racenum = 0; racenum < Race.RaceList.Length; racenum++ )
            {
                // Already adjusted this one or it's our race.
                if (racenum == race || racenum == pcrace )
                    continue;

                // Adjustment is done in fractions of a percent of a point (-1% to 1% based on how strongly the
                // opposing race feels).  This may need to be increased.
                double adjustment = (Race.RaceList[racenum].RaceFaction[race] / Limits.MAX_FACTION) * 0.01;
                // Since a race that has high faction with the race that was just killed will result in a positive number
                // we subtract it from the player's faction with that race.
                pc.RaceFaction[racenum] -= adjustment;
                if (pc.RaceFaction[racenum] < Limits.MIN_FACTION)
                {
                    pc.RaceFaction[racenum] = Limits.MIN_FACTION;
                }
                else if (pc.RaceFaction[racenum] > Limits.MAX_FACTION)
                {
                    pc.RaceFaction[racenum] = Limits.MAX_FACTION;
                }

                // Adjust the race faction rankings of the player's guild.
                if (pc.GuildMembership != null)
                {
                    // Guild takes 1/10 the adjustment.
                    pc.GuildMembership.RaceFactionStandings[racenum] -= (adjustment / 10.0);
                    if (pc.GuildMembership.RaceFactionStandings[racenum] < Limits.MIN_FACTION)
                    {
                        pc.GuildMembership.RaceFactionStandings[racenum] = Limits.MIN_FACTION;
                    }
                    else if (pc.GuildMembership.RaceFactionStandings[racenum] > Limits.MAX_FACTION)
                    {
                        pc.GuildMembership.RaceFactionStandings[racenum] = Limits.MAX_FACTION;
                    }
                }

                // Adjust the race faction rankings of the players race.
                Race.RaceList[pcrace].RaceFaction[racenum] -= (adjustment / 100.0);
                if (Race.RaceList[pcrace].RaceFaction[racenum] < Limits.MIN_FACTION)
                {
                    Race.RaceList[pcrace].RaceFaction[racenum] = Limits.MIN_FACTION;
                }
                else if (Race.RaceList[pcrace].RaceFaction[racenum] > Limits.MAX_FACTION)
                {
                    Race.RaceList[pcrace].RaceFaction[racenum] = Limits.MAX_FACTION;
                }
            }
        }
    }
}