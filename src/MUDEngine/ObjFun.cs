using System;
using ModernMUD;

namespace MUDEngine
{
    /// <summary>
    /// Special functions/procedures for objects and items.
    ///
    /// TODO: Make it possible to define these via a configuration file. Hard-coding
    /// game-specific behavior is icky.
    /// </summary>
    [Serializable]
    public class ObjFun
    {
        static bool SpecGiggle( System.Object obj, System.Object owner, bool hit )
        {
            CharData keeper = (CharData)owner;
            if (hit)
            {
                return false;
            }

            if( !keeper || !keeper.InRoom )
                return false;

            if( MUDMath.NumberPercent() < 5 )
            {
                SocketConnection.Act( "$p&n carried by $n&n starts giggling to itself!",
                     keeper, (Object)obj, null, SocketConnection.MessageTarget.room );
                SocketConnection.Act( "$p&n carried by you starts giggling to itself!",
                     keeper, (Object)obj, null, SocketConnection.MessageTarget.character);
                return true;
            }

            return false;
        }

        static bool SpecHum(System.Object obj, System.Object owner, bool hit)
        {
            CharData keeper = (CharData)owner;
            if( hit )
                return false;

            if( !keeper || !keeper.InRoom )
                return false;

            if( MUDMath.NumberPercent() < 9 )
            {
                SocketConnection.Act( "&+LA faint hum can be heard from&n $p&n &+Lcarried by&n $n&n.",
                     keeper, (Object)obj, null, SocketConnection.MessageTarget.room);
                SocketConnection.Act( "&+LA faint hum can be heard from&n $p&n &+Lyou are carrying.&n",
                     keeper, (Object)obj, null, SocketConnection.MessageTarget.character);
                return true;
            }

            return false;
        }


        static bool SpecSoulMoan(System.Object obj, System.Object owner, bool hit)
        {
            CharData keeper = (CharData)owner;
            if( hit )
                return false;

            if( !keeper || !keeper.InRoom )
                return false;

            if( MUDMath.NumberPercent() < 3 )
            {
                SocketConnection.Act( "The soul in $p&n carried by $n&n moans in agony.",
                     keeper, (Object)obj, null, SocketConnection.MessageTarget.room);
                SocketConnection.Act( "The soul in $p&n carried by you moans to be set free!",
                     keeper, (Object)obj, null, SocketConnection.MessageTarget.character);
                return true;
            }

            if( MUDMath.NumberPercent() < 2 )
            {
                SocketConnection.Act( "The soul in $p&n carried by $n&n tries to free itself!",
                     keeper, (Object)obj, null, SocketConnection.MessageTarget.room);
                SocketConnection.Act( "The soul in $p&n carried by you starts writhing!",
                     keeper, (Object)obj, null, SocketConnection.MessageTarget.character);
                return true;
            }

            return false;
        }


        static bool SpecHaste(System.Object obj, System.Object owner, bool hit)
        {
            CharData keeper = (CharData)owner;
            if( hit )
                return false;
            if( !keeper.IsAffected(Affect.AFFECT_HASTE ) )
            {
                Spell spl = Spell.SpellList["haste"];
                if (spl != null)
                {
                    spl.Invoke(keeper, 30, keeper);
                }
                return true;
            }

            return false;
        }

        static bool SpecSneak(System.Object obj, System.Object keeper, bool hit)
        {
            
            return false;
        }

        static bool SpecHide(System.Object obj, System.Object keeper, bool hit)
        {
            
            return false;
        }

        static bool SpecInvisibility(System.Object obj, System.Object keeper, bool hit)
        {
            return false;
        }

        static bool SpecStoneskin(System.Object obj, System.Object keeper, bool hit)
        {
            return false;
        }

        static bool SpecWpChill(System.Object obj, System.Object owner, bool hit)
        {
            CharData keeper = (CharData)owner;
            if( !keeper || !keeper.Fighting || !hit )
                return false;

            if( MUDMath.NumberPercent() < 10 && ( keeper.Fighting.Hitpoints >= -9 ) )
            {
                Spell spell = Spell.SpellList["chill touch"];
                if (spell != null)
                {
                    spell.Invoke(keeper, keeper.Level, keeper.Fighting);
                }
                return true;
            }

            return false;
        }

        static bool SpecWpBurn(System.Object obj, System.Object owner, bool hit)
        {
            CharData keeper = (CharData)owner;
            if( !keeper || !keeper.Fighting || !hit )
                return false;

            if( MUDMath.NumberPercent() < 10 && ( keeper.Fighting.Hitpoints >= -9 ) )
            {
                Spell.SpellList["burning hands"].Invoke(keeper, keeper.Level, keeper.Fighting);
                return true;
            }

            return false;
        }

        static bool SpecWpFireball(System.Object obj, System.Object owner, bool hit)
        {
            CharData keeper = (CharData)owner;
            if( !keeper || !keeper.Fighting || !hit )
                return false;

            if( MUDMath.NumberPercent() < 10 && ( keeper.Fighting.Hitpoints >= -9 ) )
            {
                Spell spell = Spell.SpellList["fireball"];
                if (spell != null)
                {
                    spell.Invoke(keeper, keeper.Level, keeper.Fighting);
                }
                return true;
            }

            return false;
        }

        static bool SpecWpLightning(System.Object obj, System.Object owner, bool hit)
        {
            CharData keeper = (CharData)owner;
            if( !keeper || !keeper.Fighting || !hit )
                return false;

            if( MUDMath.NumberPercent() < 10 && ( keeper.Fighting.Hitpoints >= -9 ) )
            {
                Spell.SpellList["lightning bolt"].Invoke(keeper, keeper.Level, keeper.Fighting);
                return true;
            }

            return false;
        }

        static bool SpecWpHarm(System.Object obj, System.Object owner, bool hit)
        {
            CharData keeper = (CharData)owner;
            if( !keeper || !keeper.Fighting || !hit )
                return false;

            if( MUDMath.NumberPercent() < 10 && ( keeper.Fighting.Hitpoints >= -9 ) )
            {
                Spell.SpellList["harm"].Invoke(keeper, keeper.Level, keeper.Fighting);
                return true;
            }

            return false;
        }

        static bool SpecWpDestroyUndead(System.Object obj, System.Object owner, bool hit)
        {
            CharData keeper = (CharData)owner;

            if( !keeper || !keeper.Fighting || !hit )
                return false;

            if( keeper.Fighting.IsUndead() )
            {

                if( MUDMath.NumberPercent() < 10 && ( keeper.Fighting.Hitpoints >= -9 ) )
                {
                    Spell.SpellList["destroy undead"].Invoke(keeper, 1, keeper.Fighting );
                    return true;
                }
            }
            return false;
        }

        static bool SpecWpMagicMissile(System.Object obj, System.Object owner, bool hit)
        {
            CharData keeper = (CharData)owner;
            if( !keeper || !keeper.Fighting || !hit )
                return false;

            if( MUDMath.NumberPercent() < 10 && ( keeper.Fighting.Hitpoints >= -9 ) )
            {
                Spell spl = Spell.SpellList["magic missile"];
                if (spl != null)
                {
                    spl.Invoke(keeper, keeper.Level, keeper.Fighting);
                }
                return true;
            }

            return false;
        }

        static bool SpecWpCuttingBreeze(System.Object obj, System.Object owner, bool hit)
        {
            CharData keeper = (CharData)owner;
            if( !keeper || !keeper.Fighting || !hit )
                return false;

            if( MUDMath.NumberPercent() < 10 && ( keeper.Fighting.Hitpoints >= -9 ) )
            {
                Spell.SpellList["cutting breeze"].Invoke(keeper, keeper.Level, keeper.Fighting);
                return true;
            }

            return false;
        }

        static bool SpecWpWither(System.Object obj, System.Object owner, bool hit)
        {
            CharData keeper = (CharData)owner;            
            if( !keeper || !keeper.Fighting || !hit )
                return false;

            if( MUDMath.NumberPercent() < 10 && ( keeper.Fighting.Hitpoints >= -9 ) )
            {
                Spell spl = Spell.SpellList["wither"];
                if (spl != null)
                {
                    spl.Invoke(keeper, keeper.Level, keeper.Fighting);
                }

                return true;
            }

            return false;
        }

        static bool SpecSundial(System.Object obj, System.Object keeper, bool hit)
        {
            if( hit )
                return false;
            return false;
        }

        static bool SpecSki(System.Object obj, System.Object owner, bool hit)
        {
            CharData keeper = (CharData)owner;
            /* check to make sure the object has an owner and he's in the room */

            if( !keeper || !keeper.InRoom )
                return false;

            if( MUDMath.NumberPercent() < 20 )
            {
                SocketConnection.Act( "&+LA faint hum can be heard from&n $p&n &+Lcarried by&n $n&n.",
                     keeper, (Object)obj, null, SocketConnection.MessageTarget.room);
                SocketConnection.Act( "&+LA faint hum can be heard from&n $p&n &+Lyou are carrying.&n",
                     keeper, (Object)obj, null, SocketConnection.MessageTarget.character);
                return true;
            }

            /* chance of weapon screaming */

            if( MUDMath.NumberPercent() < 20 )
            {
                SocketConnection.Act( "&+LThe soul in&n $p&n &+Lcarried b&ny $n&n &+Lmoans in&n &+Ragony&n&+L.&n",
                     keeper, (Object)obj, null, SocketConnection.MessageTarget.room);
                SocketConnection.Act( "&+LThe soul in&n $p&n &+Lcarried by you&n &+Rmoans&n &+Lto be set free!&n",
                     keeper, (Object)obj, null, SocketConnection.MessageTarget.character);
                return true;
            }

            if( MUDMath.NumberPercent() < 20 )
            {
                SocketConnection.Act( "&+LThe soul in&n $p&n &+Lcarried by&n $n&n &+Ltries to free itself!&n",
                     keeper, (Object)obj, null, SocketConnection.MessageTarget.room);
                SocketConnection.Act( "&+LThe soul in&n $p&n &+Lcarried by you starts&n &+Bwrithing!&n",
                     keeper, (Object)obj, null, SocketConnection.MessageTarget.character);
                return true;
            }

            /* check to see if the weapon is in combat */

            if( !keeper.Fighting || !hit )
                return false;

            /* harm proc - note that there has to be a spec_wp_harm above before this
            will work */

            if( MUDMath.NumberPercent() < 20 && ( keeper.Fighting.Hitpoints >= -9 ) )
            {
                Spell.SpellList["harm"].Invoke(keeper, keeper.Level, keeper.Fighting);
                return true;
            }

            return false;
        }

        static bool SpecAutumndecay(System.Object obj, System.Object owner, bool hit)
        {
            CharData keeper = (CharData)owner;
            if( !keeper || !keeper.Fighting || !hit )
                return false;

            if( MUDMath.NumberPercent() < 10 && ( keeper.Fighting.Hitpoints >= -9 )
                    && !keeper.Fighting.IsAffected(Affect.AFFECT_WITHER))
            {
                SocketConnection.Act( "&+y$n's&n $p&n &+ydives into &n$N&n&+y, and a &+Lblack m&N&+wis&+Lt&n&+y flows into the &+rwound!&N",
                     keeper, (Object)obj, keeper.Fighting, SocketConnection.MessageTarget.everyone_but_victim);
                SocketConnection.Act( "&+yYour&n $p&n &+ydives into &n$N&n&+y, and a &+Lblack m&N&+wis&+Lt&n&+y flows into the &+rwound!&N",
                     keeper, (Object)obj, keeper.Fighting, SocketConnection.MessageTarget.character);
                SocketConnection.Act( "&+y$n's&n $p&n &+ydives into you, and a &+Lblack m&N&+wis&+Lt&n&+y flows into the &+rwound!&N",
                     keeper, (Object)obj, keeper.Fighting, SocketConnection.MessageTarget.victim);
                Spell spl = Spell.SpellList["wither"];
                if (spl != null)
                {
                    spl.Invoke(keeper, keeper.Level, keeper.Fighting);
                }
                return true;
            }

            return false;
        }

        static bool SpecCelestial(System.Object obj, System.Object owner, bool hit)
        {
            CharData keeper = (CharData)owner;
            if( !keeper || !keeper.Fighting || !hit )
                return false;

            if( MUDMath.NumberPercent() < 10 && ( keeper.Fighting.Hitpoints >= -9 ) )
            {
                SocketConnection.Act( "&+b$n's&n $p&n &+bs&+Bp&+ca&+Wr&n&+Ck&n&+cl&N&+Be&n&+bs with a &+csoft &+Bblue &n&+wg&+Wl&n&+wi&+Wtt&N&+we&+Wr&n&+b...&N",
                     keeper, (Object)obj, keeper.Fighting, SocketConnection.MessageTarget.everyone_but_victim);
                SocketConnection.Act( "&+bYour&n $p&n &+bs&+Bp&N&+ca&+Cr&+Wk&+Cl&+Be&n&+bs at &n$N&n&+b, &+Bglowing&N&+b with a&+c soft &+Bblue &N&+wg&+Wl&n&+wi&+Wtt&n&+we&+Wr&n&+b!&N ",
                     keeper, (Object)obj, keeper.Fighting, SocketConnection.MessageTarget.character);
                SocketConnection.Act( "&+b$n's&n $p&n &+bs&+Bp&N&+ca&+Cr&+Wk&+Cl&+Be&n&+bs at you, &+Bglowing&N&+b with a&+c soft &+Bblue &N&+wg&+Wl&n&+wi&+Wtt&n&+we&+Wr&n&+b!&N ",
                     keeper, (Object)obj, keeper.Fighting, SocketConnection.MessageTarget.victim);
                Spell spl = Spell.SpellList["magic missile"];
                if (spl != null)
                {
                    spl.Invoke(keeper, keeper.Level, keeper.Fighting);
                }
                return true;
            }

            return false;
        }

        static bool SpecWindsabre(System.Object obj, System.Object owner, bool hit)
        {
            CharData keeper = (CharData)owner;
            if( !keeper || !keeper.Fighting || !hit )
                return false;

            if( MUDMath.NumberPercent() < 10 + keeper.Level / 10 && ( keeper.Fighting.Hitpoints >= -9 ) )
            {
                SocketConnection.Act( "&+c$n's&n $p&n &+csummons forth a &+Cgust&n&+c of &+Wstrong winds!&N",
                     keeper, (Object)obj, keeper.Fighting, SocketConnection.MessageTarget.everyone_but_victim);
                SocketConnection.Act( "&+cYour&n $p&n &+csummons forth a &+Cgust&n&+c of &+Wstrong winds&N&+c, cutting into &n$N&n&+c!&N ",
                     keeper, (Object)obj, keeper.Fighting, SocketConnection.MessageTarget.character);
                SocketConnection.Act( "&+c$n's&n $p&n &+csummons forth a &+Cgust&n&+c of &+Wstrong winds&N&+c, cutting into you!&N ",
                     keeper, (Object)obj, keeper.Fighting, SocketConnection.MessageTarget.victim);
                Spell spell = Spell.SpellList["chill touch"];
                if (spell != null)
                {
                    spell.Invoke(keeper, keeper.Level, keeper.Fighting);
                } 
                return true;
            }

            return false;
        }


        static bool SpecHornofplenty(System.Object obj, System.Object owner, bool hit)
        {
            CharData keeper = (CharData)owner;
            /* check to make sure the object has an owner and he's in the room */

            if( !keeper || !keeper.InRoom )
                return false;

            if( MUDMath.NumberPercent() < 2 )
            {
                SocketConnection.Act( "&+LA faint hum can be heard from&n $p&n &+Lcarried by&n $n&n.",
                     keeper, (Object)obj, null, SocketConnection.MessageTarget.room);
                SocketConnection.Act( "&+LA faint hum can be heard from&n $p&n &+Lyou are carrying.&n",
                     keeper, (Object)obj, null, SocketConnection.MessageTarget.character);
                return true;
            }

            /* we need to make sure it doesn't check for hunger on an NPC/

            /* check for the hunger level of the holder */

            if( ( (PC)keeper ).Hunger < 1 || ( (PC)keeper ).Thirst < 1 )
            {
                SocketConnection.Act( "$n's $p&n &+mglows&n&+y softly.&n",
                     keeper, (Object)obj, null, SocketConnection.MessageTarget.room);
                SocketConnection.Act( "&+yYour&n $p&n &+mglows&n&+y softly.&n",
                     keeper, (Object)obj, null, SocketConnection.MessageTarget.character);

                foreach( CharData victim in keeper.InRoom.People )
                {
                    if( victim.IsNPC() )
                        continue;

                    if( ( (PC)victim ).Hunger < 30 )
                    {
                        ( (PC)victim ).Hunger = 30;
                        victim.SendText( "You feel full.\r\n" );
                    }

                    if( ( (PC)victim ).Thirst < 30 )
                    {
                        ( (PC)victim ).Thirst = 30;
                        victim.SendText( "You do not feel thirsty.\r\n" );
                    }

                }
                return true;
            }

            return false;
        }

        static bool SpecGhoulbane(System.Object obj, System.Object owner, bool hit)
        {
            CharData keeper = (CharData)owner;
            if( !keeper || !keeper.Fighting || !hit )
                return false;

            CharData victim = keeper.Fighting;

            if( victim.IsUndead() )
            {

                if( MUDMath.NumberPercent() < 10 && ( keeper.Fighting.Hitpoints >= -9 ) )
                {
                    SocketConnection.Act( "&+w$n's&n $p&n &+Wglows&n &+wwith a powerful light!&N",
                         keeper, (Object)obj, keeper.Fighting, SocketConnection.MessageTarget.everyone_but_victim);
                    SocketConnection.Act( "&+wYour&n $p&n &+Wglows&n &+was it bites into its victim!&n", keeper,
                         (Object)obj, keeper.Fighting, SocketConnection.MessageTarget.character);
                    SocketConnection.Act( "&+w$n's&n $p&n &+Wglows&n &+win an angry light as it burns you horribly!&N", keeper,
                         (Object)obj, keeper.Fighting, SocketConnection.MessageTarget.victim);
                    Spell.SpellList["destroy undead"].Invoke(keeper, 1, keeper.Fighting);
                    return true;
                }
            }
            return false;
        }

        static bool SpecWpBonethunder(System.Object obj, System.Object owner, bool hit)
        {
            CharData keeper = (CharData)owner;
            
            if( keeper == null || keeper.Fighting == null || !hit )
                return false;

            if( MUDMath.NumberPercent() < 10 && ( keeper.Fighting.Hitpoints >= -9 ) )
            {
                Spell spell = StringLookup.SpellLookup("bonethunder");
                if (!spell)
                {
                    Log.Error("SpecWpBonethunder: 'bonethunder' spell not found. Check the spells file.");
                    return false;
                }
                SocketConnection.Act("&+w$n's&n $p&n &+wemits a horrible &+WCRACKING&n &+wnoise and&n $N&n &+wshreaks in agony!&N",
                     keeper, (Object)obj, keeper.Fighting, SocketConnection.MessageTarget.everyone_but_victim);
                SocketConnection.Act("&+wYour&n $p&n &+wmakes a horrible&n &+WCRACKING&n &+wnoise and&n $N&n &+wshreaks in pain!", keeper,
                     (Object)obj, keeper.Fighting, SocketConnection.MessageTarget.character);
                SocketConnection.Act("&+w$n's&n $p&n &+wmakes a horrible&n &+WCRACKING&n &+wnoise and you feel your bones breaking!&N", keeper,
                     (Object)obj, keeper.Fighting, SocketConnection.MessageTarget.victim);
                spell.Invoke(keeper, keeper.Level, keeper.Fighting);
                return true;
            }

            return false;
        }

        static bool SpecInferno(System.Object obj, System.Object owner, bool hit)
        {
            CharData keeper = (CharData)owner;            
            /* check to make sure the object has an owner and he's in the room */

            if( !keeper || !keeper.InRoom )
                return false;

            if( MUDMath.NumberPercent() < 10 )
            {
                SocketConnection.Act( "&+LA faint hum can be heard from&n $p&n &+Lcarried by&n $n&n.",
                     keeper, (Object)obj, null, SocketConnection.MessageTarget.room);
                SocketConnection.Act( "&+LA faint hum can be heard from&n $p&n &+Lyou are carrying.&n",
                     keeper, (Object)obj, null, SocketConnection.MessageTarget.character);
                return true;
            }

            /* check to see if the weapon is in combat */
            if( !keeper.Fighting || !hit )
                return false;


            if( keeper.Fighting && keeper.InRoom && MUDMath.NumberPercent() < 10
                    && keeper.Fighting.Hitpoints >= -9 )
            {
                SocketConnection.Act( "&+r$n&+r's $p&+r glows brightly and emits a storm of fire!&n",
                     keeper, (Object)obj, null, SocketConnection.MessageTarget.room);
                SocketConnection.Act( "Your $p&+r glows brightly and emits a storm of fire!&n",
                     keeper, (Object)obj, null, SocketConnection.MessageTarget.character);
                int dam = MUDMath.Dice( 10, 10 ) + 150;
                foreach( CharData victim in keeper.InRoom.People )
                {
                    if( victim.IsSameGroup( keeper )
                            || victim == keeper || victim.Hitpoints < -9 )
                        continue;
                    if( victim.FlightLevel != keeper.FlightLevel )
                        continue;
                    if (Magic.SpellSavingThrow(((Object)obj).Level, victim, AttackType.DamageType.fire))
                        Combat.InflictSpellDamage( keeper, victim, dam / 2, "inferno", AttackType.DamageType.fire );
                    else
                        Combat.InflictSpellDamage( keeper, victim, dam, "inferno", AttackType.DamageType.fire );
                }
            }

            return false;
        }

        static bool SpecLightanddark(System.Object obj, System.Object owner, bool hit)
        {
            CharData keeper = (CharData)owner;            
            if( !keeper || !keeper.Fighting || !hit )
                return false;

            //Hum
            if( MUDMath.NumberPercent() < 10 )
            {
                SocketConnection.Act( "&+LA faint hum can be heard from&n $p&n &+Lcarried by&n $n&n.",
                     keeper, (Object)obj, null, SocketConnection.MessageTarget.room);
                SocketConnection.Act( "&+LA faint hum can be heard from&n $p&n &+Lyou are carrying.&n",
                     keeper, (Object)obj, null, SocketConnection.MessageTarget.character);
                return true;
            }

            // Weapon proc
            if( keeper.Fighting && MUDMath.NumberPercent() < 15 )
            {
                Spell spell = StringLookup.SpellLookup("lightanddark");
                if (!spell)
                {
                    Log.Error("SpecLightanddark: 'lightanddark' spell not found. Check the spells file.");
                    return false;
                }
                SocketConnection.Act("&n$n&+L's sword fulgurates fiercely as a &+Csearing &+clight &+Lcollects at the blade's end. ",
                     keeper, (Object)obj, keeper.Fighting, SocketConnection.MessageTarget.room);
                spell.Invoke(keeper, keeper.Level, keeper.Fighting);
                return true;
            }

            return false;
        }

        static bool SpecTrident(System.Object obj, System.Object owner, bool hit)
        {
            CharData keeper = (CharData)owner;
            if( !keeper || !keeper.Fighting || !hit )
                return false;

            // Hum
            if( MUDMath.NumberPercent() < 10 )
            {
                SocketConnection.Act( "&+LA faint hum can be heard from&n $p&n &+Lcarried by&n $n&n.",
                     keeper, (Object)obj, null, SocketConnection.MessageTarget.room);
                SocketConnection.Act( "&+LA faint hum can be heard from&n $p&n &+Lyou are carrying.&n",
                     keeper, (Object)obj, null, SocketConnection.MessageTarget.character);
                return true;
            }

            // Weapon proc
            if( keeper.Fighting && MUDMath.NumberPercent() < 15 )
            {
                Spell spell = StringLookup.SpellLookup("lightanddark");
                if (!spell)
                {
                    Log.Error("SpecTrident: 'lightanddark' spell not found. Check the spells file.");
                    return false;
                }
                SocketConnection.Act("&n$n&+c's &n&+Ctr&n&+cident &n&+bglows &+Bblue&n&+c as &n&+Csur&n&+crea&n&+Cl &n&+Bwat&n&+bers " +
                     "&n&+Bbeg&n&+bin &n&+cto &n&+Cg&n&+cath&n&+Cer &n&+bat i&n&+Bt&n&+bs &n&+Cti&n&+cp.&n",
                     keeper, (Object)obj, keeper.Fighting, SocketConnection.MessageTarget.room);
                spell.Invoke(keeper, keeper.Level, keeper.Fighting);
                return true;
            }

            return false;
        }

        static bool SpecHammer(System.Object obj, System.Object owner, bool hit)
        {
            CharData keeper = (CharData)owner;
            bool retval = false;
 
            if( !keeper )
                return false;

            //Hum
            if( ( !keeper.Fighting && MUDMath.NumberPercent() < 10 ) ||
                    ( keeper.Fighting && MUDMath.NumberPercent() < 2 ) )
            {
                SocketConnection.Act( "&+LA faint hum can be heard from&n $p&n &+Lcarried by&n $n&n.",
                     keeper, (Object)obj, null, SocketConnection.MessageTarget.room);
                SocketConnection.Act( "&+LA faint hum can be heard from&n $p&n &+Lyou are carrying.&n",
                     keeper, (Object)obj, null, SocketConnection.MessageTarget.character);
                if (!keeper.IsAffected(Affect.AFFECT_STONESKIN) && MUDMath.NumberPercent() < 30)
                {
                    Spell spl = Spell.SpellList["stoneskin"];
                    if (spl != null)
                    {
                        spl.Invoke(keeper, 60, keeper);
                    }
                }
                retval = true;
            }

            if( !keeper.Fighting )
                return retval;

            // it fires off 1 in 12 times
            if( MUDMath.NumberPercent() > 8 )
                return false;

            // if not wielded as primary, do nothing
            if( Object.GetEquipmentOnCharacter( keeper, ObjTemplate.WearLocation.hand_one ) != obj )
                return false;

            // grumbar's hammer needs to be grounded to work
            if( keeper.FlightLevel != 0 )
                return false;

            // need to have some earthly elements around
            //    if ( keeper.in_room.sector == RoomIndex.TerrainType.plane_of_air ||
            //         keeper.in_room.sector == RoomIndex.TerrainType.plane_of_water ||
            //         keeper.in_room.sector == RoomIndex.TerrainType.plane_of_fire )
            //        return false;

            CharData vict = keeper.Fighting;

            switch( MUDMath.NumberRange( 1, 3 ) )
            {
                case 1:
                    //throw a wall
                    int dir = MUDMath.NumberRange( 0, Limits.MAX_DIRECTION - 1 );
                    if (keeper.InRoom.ExitData[dir])
                    {
                        Spell spl = Spell.SpellList["wall of stone"];
                        if (spl != null)
                        {
                            spl.Invoke(keeper, 50, dir.ToString());
                        }
                    }
                    retval = true;
                    break;

                case 2:
                    // earthen rain
                    if( !keeper.IsOutside() )
                    {
                        retval = false;
                        break;
                    }
                    SocketConnection.Act( "Your $p crushes $N in a rain of &+yearth&N and &+Lstone&N!", keeper,
                        (Object)obj, vict, SocketConnection.MessageTarget.character);
                    SocketConnection.Act( "$n's $p crushes you under a rain of &+yearth&N and &+Lstone&N!", keeper,
                        (Object)obj, vict, SocketConnection.MessageTarget.victim);
                    SocketConnection.Act( "$n's $p crushes $N under a rain of &+yearth&N and &+Lstone&N!.", keeper,
                        (Object)obj, vict, SocketConnection.MessageTarget.everyone_but_victim);
                    // same damage as earthen rain at level 50
                    int dam = MUDMath.Dice( 150, 3 ) + 100;
                    Combat.InflictSpellDamage( keeper, vict, dam, "earthen rain", AttackType.DamageType.crushing );
                    retval = true;
                    break;
                case 3:
                    // cause an earthquake
                    SocketConnection.Act( "Your $p causes the ground to rise up!",
                        keeper, (Object)obj, vict, SocketConnection.MessageTarget.character);
                    SocketConnection.Act( "$n's $p blasts the ground with a &+yshockwave&N!",
                        keeper, (Object)obj, null, SocketConnection.MessageTarget.room);
                    foreach( CharData targetChar in keeper.InRoom.People )
                    {
                        if( ( targetChar == keeper ) || targetChar.IsImmortal() )
                            continue;
                        if( keeper.IsSameGroup( targetChar ) )
                            continue;
                        if( targetChar.IsAffected( Affect.AFFECT_FLYING ) )
                            continue;
                        if( keeper.IsNPC() && targetChar.IsNPC() && !vict.IsSameGroup( targetChar ) )
                            continue;
                        if( Magic.SpellSavingThrow( 60, targetChar, AttackType.DamageType.earth ) )
                        {
                            SocketConnection.Act( "You wobble precipitously, but keep your feet.",
                                keeper, null, targetChar, SocketConnection.MessageTarget.victim );
                            SocketConnection.Act( "$n&n stumbles slightly but keeps $s balance.", targetChar, null, null, SocketConnection.MessageTarget.room );
                        }
                        else
                        {
                            SocketConnection.Act( "You are knocked to the ground!",
                                keeper, null, targetChar, SocketConnection.MessageTarget.victim );
                            SocketConnection.Act( "$n&n is knocked to the ground!", targetChar, null, null, SocketConnection.MessageTarget.room );
                            targetChar.CurrentPosition = Position.kneeling;
                            targetChar.WaitState( Event.TICK_COMBAT );
                        }
                    } // end for
                    retval = true;
                    break;
                default:
                    break;
            } //end switch
            return retval;
        }

        /// <summary>
        /// Object special function commands.
        /// 
        /// NOTE: No special function for an object should EVER contain a complete
        /// substring of another object function because we use the String.Contains()
        /// contains for matching object functions.  That would mean that an object
        /// flagged with _function spec_fireball would match functions spec_fireball AND
        /// spec_fire if one existed.
        /// </summary>
        public static ObjSpecial[] ObjectSpecialTable = new[]   
        {
            new ObjSpecial( "spec_giggle",            SpecGiggle             ),
            new ObjSpecial( "spec_soul_moan",         SpecSoulMoan          ),
            new ObjSpecial( "spec_hum",               SpecHum                ),
            new ObjSpecial( "spec_haste",             SpecHaste              ),
            new ObjSpecial( "spec_stoneskin",         SpecStoneskin          ),
            new ObjSpecial( "spec_sneak",             SpecSneak              ),
            new ObjSpecial( "spec_hide",              SpecHide               ),
            new ObjSpecial( "spec_invisibility",      SpecInvisibility       ),
            new ObjSpecial( "spec_wp_lightning",      SpecWpLightning       ),
            new ObjSpecial( "spec_wp_chill",          SpecWpChill           ),
            new ObjSpecial( "spec_wp_destroy_undead", SpecWpDestroyUndead  ),
            new ObjSpecial( "spec_wp_burn",           SpecWpBurn            ),
            new ObjSpecial( "spec_wp_fireball",       SpecWpFireball        ),
            new ObjSpecial( "spec_wp_harm",           SpecWpHarm            ),
            new ObjSpecial( "spec_wp_magic_missile",  SpecWpMagicMissile   ),
            new ObjSpecial( "spec_wp_cutting_breeze", SpecWpCuttingBreeze  ),
            new ObjSpecial( "spec_wp_wither",         SpecWpWither          ),
            new ObjSpecial( "spec_sundial",           SpecSundial            ),
            new ObjSpecial( "spec_ski",               SpecSki                ),
            new ObjSpecial( "spec_inferno",           SpecInferno            ),
            new ObjSpecial( "spec_autumndecay",       SpecAutumndecay        ),
            new ObjSpecial( "spec_celestial",         SpecCelestial          ),
            new ObjSpecial( "spec_windsabre",         SpecWindsabre          ),
            new ObjSpecial( "spec_hornofplenty",      SpecHornofplenty       ),
            new ObjSpecial( "spec_ghoulbane",         SpecGhoulbane          ),
            new ObjSpecial( "spec_wp_bonethunder",    SpecWpBonethunder     ),
            new ObjSpecial( "spec_lightanddark",      SpecLightanddark       ),
            new ObjSpecial( "spec_hammer",            SpecHammer             )
        };
    }
}