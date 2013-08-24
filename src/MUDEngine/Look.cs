using System;
using System.Collections.Generic;
using ModernMUD;

namespace MUDEngine
{
    /// <summary>
    /// The Look class contains utility methods used for displaying room, object, and mob/player details
    /// to onlooking players.
    /// </summary>
    public class Look
    {
        /// <summary>
        /// Shows what a player or mob is affected by.
        /// </summary>
        /// <param name="ch"></param>
        /// <param name="target"></param>
        public static void ShowAffectLines(CharData ch, CharData target)
        {
            if (target.IsAffected(Affect.AFFECT_BREATHE_UNDERWATER))
            {
                SocketConnection.Act("&+c$E has little gills in $s neck.&n", ch, null, target, SocketConnection.MessageTarget.character);
            }
            if (target.IsAffected(Affect.AFFECT_SOULSHIELD))
            {
                if (target._alignment > 0)
                {
                    SocketConnection.Act("&+W$E is surrounded by a holy aura.&n", ch, null, target, SocketConnection.MessageTarget.character);
                }
                else
                {
                    SocketConnection.Act("&+r$E is surrounded by an unholy aura.&n", ch, null, target, SocketConnection.MessageTarget.character);
                }
            }
            if (target.IsAffected(Affect.AFFECT_SANCTUARY))
                SocketConnection.Act("&+W$E&+W is surrounded by an white aura.&n", ch, null, target, SocketConnection.MessageTarget.character);
            if (target.IsAffected(Affect.AFFECT_STONESKIN))
            {
                SocketConnection.Act("&+L$S skin appears to be made from stone.&n", ch, null, target, SocketConnection.MessageTarget.character);
            }
            if (target.IsAffected(Affect.AFFECT_BARKSKIN))
            {
                SocketConnection.Act("&+y$S skin resembles the bark of a tree.&n", ch, null, target, SocketConnection.MessageTarget.character);
            }
            if (target.IsAffected(Affect.AFFECT_MAJOR_GLOBE) ||
                    target.IsAffected(Affect.AFFECT_MINOR_GLOBE))
            {
                SocketConnection.Act("&+R$E is surrounded by a shimmering globe.&n", ch, null, target, SocketConnection.MessageTarget.character);
            }
            if (target.IsAffected(Affect.AFFECT_GREATER_SPIRIT_WARD))
            {
                SocketConnection.Act("&+W$E is surrounded by a diffuse globe of light!&n", ch, null, target, SocketConnection.MessageTarget.character);
            }
            if (target.IsAffected(Affect.AFFECT_COLDSHIELD))
            {
                SocketConnection.Act("&+B$E is surrounded by a killing frost!&n", ch, null, target, SocketConnection.MessageTarget.character);
            }
            if (target.IsAffected(Affect.AFFECT_FIRESHIELD))
            {
                SocketConnection.Act("&+r$E is surrounded by a flaming shield.&n", ch, null, target, SocketConnection.MessageTarget.character);
            }
            if (target.IsClass(CharClass.Names.monk) || target.IsClass(CharClass.Names.mystic))
            {
                string stance = null;
                if (ch.IsImmortal() ||
                        ch.IsClass(CharClass.Names.monk) || ch.IsClass(CharClass.Names.mystic))
                {
                    if (!target.IsNPC())
                    {
                        stance = ((PC)target).Stance;
                    }
                    string text;
                    if (!String.IsNullOrEmpty(stance))
                    {
                        text = String.Format("&+y$E has assumed the {0} stance.&n", Database.MonkSkillList[stance].Name);
                    }
                    else
                    {
                        text = String.Format("&+y$E stands at ease.&n");
                    }
                    SocketConnection.Act(text, ch, null, target, SocketConnection.MessageTarget.character);
                }
            }
            if (target.IsAffected(Affect.AFFECT_BOUND))
            {
                SocketConnection.Act("&+y$E is bound and cannot move!&n", ch, null, target, SocketConnection.MessageTarget.character);
            }
        }

        /// <summary>
        /// Formats an object for display to a character.
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="ch"></param>
        /// <param name="shortDisplay"></param>
        /// <returns></returns>
        public static string FormatObjectToCharacter(Object obj, CharData ch, bool shortDisplay)
        {
            string text = String.Empty;

            if (obj == null || ch == null)
            {
                Log.Error("format_obj_to_char: null ch or obj.", 0);
                return null;
            }

            if (shortDisplay)
            {
                if (obj.ShortDescription.Length > 0)
                {
                    text += obj.ShortDescription;
                }
                text += " ";
            }

            if (obj.HasFlag(ObjTemplate.ITEM_NOSHOW) && ch.IsImmortal())
            {
                text += "&n(&+LNoshow&n) ";
            }
            else if (obj.HasFlag(ObjTemplate.ITEM_NOSHOW))
            {
                return text;
            }
            if (obj.HasFlag(ObjTemplate.ITEM_INVIS))
            {
                text += "&n(&+LInvis&n) ";
            }
            if ((ch.IsAffected( Affect.AFFECT_DETECT_EVIL) || ch.HasInnate(Race.RACE_DETECT_ALIGN)
                    || ch.IsClass(CharClass.Names.antipaladin) || ch.IsClass(CharClass.Names.paladin))
                    && obj.HasFlag(ObjTemplate.ITEM_EVIL))
            {
                text += "&n(&+LDark Aura&n) ";
            }
            if ((ch.IsAffected(Affect.AFFECT_DETECT_GOOD) || ch.HasInnate(Race.RACE_DETECT_ALIGN)
                    || ch.IsClass(CharClass.Names.antipaladin) || ch.IsClass(CharClass.Names.paladin))
                    && obj.HasFlag(ObjTemplate.ITEM_BLESS))
            {
                text += "&n(&+WLight Aura&n) ";
            }
            if (obj.HasFlag(ObjTemplate.ITEM_MAGIC) && (ch.IsAffected(Affect.AFFECT_DETECT_MAGIC) ||
                (!ch.IsNPC() && ch.HasActBit(PC.PLAYER_GODMODE))))
            {
                text += "&n(&+BMagic&n) ";
            }
            if (obj.HasFlag(ObjTemplate.ITEM_GLOW))
            {
                text += "&n(&+CGlowing&n) ";
            }
            if (obj.HasFlag(ObjTemplate.ITEM_LIT))
            {
                text += "&n(&+WIlluminating&n) ";
            }
            if (obj.HasFlag(ObjTemplate.ITEM_SECRET) && (ch.IsAffected(Affect.AFFECT_DETECT_HIDDEN) ||
                (!ch.IsNPC() && ch.HasActBit(PC.PLAYER_GODMODE))))
            {
                text += "&n(&+yHidden&n) ";
            }

            if (!shortDisplay)
            {
                if (obj.ItemType == ObjTemplate.ObjectType.pc_corpse && (ch.IsImmortal() || (int)ch.GetRacewarSide() == obj.Values[4]))
                {
                    text += "The " + obj.ShortDescription + " is lying here.";
                }
                else if (obj.FullDescription.Length > 0)
                {
                    text += obj.FullDescription;
                }
            }

            text += "&n";

            return text;
        }

        /// <summary>
        /// Shows a list of objects to the character.  Can combine duplicate items.
        /// </summary>
        /// <param name="list"></param>
        /// <param name="ch"></param>
        /// <param name="shortDisplay"></param>
        /// <param name="showNothing"></param>
        public static void ShowListToCharacter(List<Object> list, CharData ch, bool shortDisplay, bool showNothing)
        {
            // This is called every time someone looks at the room.  If there are no mobs, objects, etc. we skip out.
            if (!ch || ch._socket == null)
            {
                return;
            }

            string text = String.Empty; // The output buffer.
            int iterator = 0;
            string[] showStrings = new string[list.Count];
            int[] showCounts = new int[list.Count];
            int numShown = 0;

            // Format the list of objects.
            foreach (Object obj in list)
            {
                if (!obj.CarriedBy && !obj.InObject)
                {
                    if (ch._flyLevel != obj.FlyLevel && obj.ItemType != ObjTemplate.ObjectType.wall)
                    {
                        continue;
                    }
                }
                // Skip no-show objects if not immortal.
                if (!obj.HasFlag(ObjTemplate.ITEM_NOSHOW) || (obj.HasFlag(ObjTemplate.ITEM_NOSHOW) && ch.IsImmortal()))
                {
                    if (obj.WearLocation == ObjTemplate.WearLocation.none && CharData.CanSeeObj(ch, obj))
                    {
                        string pstrShow = FormatObjectToCharacter(obj, ch, shortDisplay); // Temporary string containing current line.
                        bool fCombine = false;

                        if (ch.IsNPC() || ch.HasActBit(PC.PLAYER_COMBINE))
                        {
                            // Look for duplicates, case sensitive.
                            // Matches tend to be near end so run loop backwords.
                            for (iterator = numShown - 1; iterator >= 0; iterator--)
                            {
                                if (!MUDString.StringsNotEqual(showStrings[iterator], pstrShow))
                                {
                                    ++showCounts[iterator];
                                    fCombine = true;
                                    break;
                                }
                            }
                        }

                        // Couldn't combine, or didn't want to.
                        if (!fCombine)
                        {
                            showStrings[numShown] = pstrShow;
                            showCounts[numShown] = 1;
                            ++numShown;
                        }
                    }
                }
            }

            // Output the formatted list.
            for (iterator = 0; iterator < numShown; ++iterator)
            {
                if (ch.IsNPC() || ch.HasActBit(PC.PLAYER_COMBINE))
                {
                    if (showCounts[iterator] != 1)
                    {
                        text += "(" + showCounts[iterator] + ") ";
                    }
                }
                text += showStrings[iterator] + "\r\n";
            }

            if (showNothing && numShown == 0)
            {
                if (ch.IsNPC() || ch.HasActBit(PC.PLAYER_COMBINE))
                {
                    text += "     ";
                }
                text += "Nothing.\r\n";
            }

            ch.SendText(text);
            return;
        }

        /// <summary>
        /// Show a character to another character. This is the abbreviated version used in "look room"
        /// </summary>
        /// <param name="victim"></param>
        /// <param name="ch"></param>
        public static void ShowCharacterToCharacterAbbreviated(CharData victim, CharData ch)
        {
            string text = String.Empty;

            if (!victim || !ch)
            {
                Log.Error("ShowCharacterToCharacter0(): null ch or victim.", 0);
                return;
            }

            if (victim._rider && victim._rider._inRoom == ch._inRoom)
            {
                return;
            }

            // If invis, show char invis symbol first.
            if (victim.IsAffected(Affect.AFFECT_INVISIBLE))
            {
                text += "&+L*&n ";
            }

            // Show the player's description.
            if (((!ch.IsNPC() && victim._position == Position.standing)
                    || (victim.IsNPC() && victim._mobTemplate != null
                        && victim._position == victim._mobTemplate.DefaultPosition))
                    && (!String.IsNullOrEmpty(victim._fullDescription)) && !victim._riding)
            {
                // Added long description does not have \r\n removed.  We may want to.
                text += victim._description + "&n";
            }
            else
            {
                // Show the player's name.
                text += victim.ShowNameTo(ch, true) + "&n";

                // Show the player's title.
                // Show the player's race, only if PC, and on the same side of the racewar or a god.
                if (!victim.IsNPC() && ((PC)victim).Title.Length > 0)
                {
                    if (MUDString.StringsNotEqual(((PC)victim).Title, " &n") && (ch.IsNPC()))
                    {
                        text += ((PC)victim).Title;
                    }
                    if (victim.IsGuild() && (ch.IsNPC()))
                    {
                        text += " " + ((PC)victim).GuildMembership.WhoName;
                    }

                    if (!ch.IsRacewar(victim) || victim.IsImmortal() || ch.IsImmortal())
                    {
                        text += " (" + Race.RaceList[victim.GetRace()].ColorName + ")";
                    }
                }

                // Show the player's condition.
                text += " is ";
                if (victim._position == Position.standing && victim.CanFly())
                {
                    text += "flying";
                }
                else
                {
                    text += Position.PositionString(victim._position);
                }
                text += " here";
                if (victim._fighting != null)
                {
                    text += "&n fighting ";
                    if (victim._fighting == ch)
                    {
                        text += "&nYOU!";
                    }
                    else if (victim._inRoom == victim._fighting._inRoom)
                    {
                        text += victim._fighting.ShowNameTo(ch, false);
                    }
                    else
                    {
                        text += "&nsomeone who left??";
                    }
                }

                if (victim._riding && victim._riding._inRoom == victim._inRoom)
                {
                    text += "&n, mounted on " + victim._riding.ShowNameTo(ch, false);
                }
                text += "&n.";
            }

            if (victim.IsAffected(Affect.AFFECT_CASTING))
            {
                text += "&n&+y (casting)&n";
            }

            if (victim.IsAffected(Affect.AFFECT_MINOR_PARA))
            {
                text += "&n (&+Yparalyzed)&n";
            }
            if (!victim.IsNPC() && victim.HasActBit(PC.PLAYER_WIZINVIS)
                    && victim.GetTrust() <= ch.GetTrust())
            {
                text += " &n&+g*&n";
            }
            if (victim.IsAffected(Affect.AFFECT_HIDE) && (ch.IsAffected(Affect.AFFECT_DETECT_HIDDEN) ||
                      ch.HasInnate(Race.RACE_DETECT_HIDDEN)))
            {
                text += " &n(&+LHiding&n)";
            }
            if (victim.IsAffected(Affect.AFFECT_CHARM) && ch.HasActBit(PC.PLAYER_GODMODE))
            {
                text += " &n(&n&+mCharmed&n)";
            }
            if ((victim.IsAffected(Affect.AFFECT_PASS_DOOR) || victim.HasInnate(Race.RACE_PASSDOOR))
                    && ch.HasActBit(PC.PLAYER_GODMODE))
            {
                text += " &n(&+WTranslucent&n)";
            }
            if ((victim.GetRace() == Race.RACE_UNDEAD || victim.GetRace() == Race.RACE_VAMPIRE)
                    && (ch.IsAffected( Affect.AFFECT_DETECT_UNDEAD) || ch.HasActBit(PC.PLAYER_GODMODE)))
            {
                text += " &n(&+WPale&n)";
            }
            if (victim.IsAffected(Affect.AFFECT_FAERIE_FIRE))
            {
                text += " &n(&n&+mFa&+Me&n&+mr&+Mie&+L Aura&n)";
            }
            if (victim.IsEvil() && (ch.IsAffected(Affect.AFFECT_DETECT_EVIL)
                         || ch.HasInnate(Race.RACE_DETECT_ALIGN)
                         || ch.IsClass(CharClass.Names.paladin)
                         || ch.IsClass(CharClass.Names.antipaladin)))
            {
                text += " &n(&+rBlood&+L Aura&n)";
            }
            if (victim.IsGood() && (ch.IsAffected(Affect.AFFECT_DETECT_GOOD)
                         || ch.HasInnate(Race.RACE_DETECT_ALIGN)
                         || ch.IsClass(CharClass.Names.paladin)
                         || ch.IsClass(CharClass.Names.antipaladin)))
            {
                text += " &n(&+CLight&+L Aura&n)";
            }
            if (victim.IsAffected(Affect.AFFECT_SANCTUARY))
            {
                text += " &n(&+WWhite&+L Aura&n)";
            }
            if (!victim.IsNPC() && victim.HasActBit(PC.PLAYER_AFK))
            {
                text += " &n&+b(&+RAFK&n&+b)&n";
            }
            if (!victim.IsNPC() && victim.HasActBit(PC.PLAYER_BOTTING))
            {
                text += " &n&+b(&+YBot&n&+b)&n";
            }
            text += "\r\n";
            ch.SendText(text);
            return;
        }

        /// <summary>
        /// Show a character to another character.
        /// </summary>
        /// <param name="victim"></param>
        /// <param name="ch"></param>
        public static void ShowCharacterToCharacterFull(CharData victim, CharData ch)
        {
            Object obj;
            string text = String.Empty;
            int percent;

            if (CharData.CanSee(victim, ch))
            {
                SocketConnection.Act("$n&n looks at you.", ch, null, victim, SocketConnection.MessageTarget.victim);
                if (victim != ch)
                {
                    SocketConnection.Act("$n&n looks at $N&n.", ch, null, victim, SocketConnection.MessageTarget.everyone_but_victim);
                }
                else
                {
                    SocketConnection.Act("$n&n looks at $mself.", ch, null, victim, SocketConnection.MessageTarget.everyone_but_victim);
                }
            }

            if (victim._riding != null)
            {
                text += String.Format("&nMounted on {0}, ", victim._riding.ShowNameTo(ch, false));
            }
            else if (victim._rider != null)
            {
                text += String.Format("&nRidden by {0}, ", victim._rider.ShowNameTo(ch, false));
            }

            if (!victim.IsNPC() && victim.IsGuild())
            {
                text += String.Format("&n{0} of {1}.\r\n", ((PC)victim).GuildRank.ToString().ToUpper(),
                          ((PC)victim).GuildMembership.Name);
            }

            SocketConnection.Act(text, ch, null, victim, SocketConnection.MessageTarget.character);

            if (!String.IsNullOrEmpty(victim._description))
            {
                ch.SendText(victim._description);
            }
            else
            {
                SocketConnection.Act("&nYou see nothing special about $M.", ch, null, victim, SocketConnection.MessageTarget.character);
            }

            if (victim.GetMaxHit() > 0)
            {
                percent = (100 * victim._hitpoints) / victim.GetMaxHit();
            }
            else
            {
                percent = -1;
            }

            text = victim.ShowNameTo(ch, true);

            if (percent >= 100)
                text += " &nis in perfect &n&+ghealth&n.  ";
            else if (percent >= 90)
                text += " &nis slightly &n&+yscratched&n.  ";
            else if (percent >= 80)
                text += " &nhas a &+yfew bruises&n.  ";
            else if (percent >= 70)
                text += " &nhas &+Ysome cuts&n.  ";
            else if (percent >= 60)
                text += " &nhas &+Mseveral wounds&n.  ";
            else if (percent >= 50)
                text += " &nhas &+mmany nasty wounds&n.  ";
            else if (percent >= 40)
                text += " &nis &+Rbleeding freely&n.  ";
            else if (percent >= 30)
                text += " &nis &+Rcovered in blood&n.  ";
            else if (percent >= 20)
                text += " &nis &+rleaking guts&n.  ";
            else if (percent >= 10)
                text += " &nis &+ralmost dead&n.  ";
            else
                text += " &nis &+rDYING&n.  ";

            ch.SendText(text);

            // Show size on look at someone.
            text = MUDString.CapitalizeANSIString(String.Format("{0}&n is a {1} of {2} size.\r\n", victim.GetSexPronoun(),
                Race.RaceList[victim.GetRace()].ColorName, Race.SizeString(victim._size)));
            ch.SendText(text);

            ShowAffectLines(ch, victim);

            bool found = false;
            foreach (ObjTemplate.WearLocation location in ObjTemplate.TopDownEquipment)
            {
                obj = Object.GetEquipmentOnCharacter(victim, location);
                if (obj && CharData.CanSeeObj(ch, obj))
                {
                    if (!found)
                    {
                        ch.SendText("\r\n");
                        SocketConnection.Act("&n$E is using:", ch, null, victim, SocketConnection.MessageTarget.character);
                        found = true;
                    }
                    if (obj.ItemType == ObjTemplate.ObjectType.weapon
                            && (location == ObjTemplate.WearLocation.hand_one
                                 || location == ObjTemplate.WearLocation.hand_three
                                 || location == ObjTemplate.WearLocation.hand_four
                                 || location == ObjTemplate.WearLocation.hand_two)
                            && obj.HasWearFlag(ObjTemplate.WEARABLE_WIELD))
                    {
                        if (obj.HasFlag(ObjTemplate.ITEM_TWOHANDED)
                                && !ch.HasInnate(Race.RACE_EXTRA_STRONG_WIELD))
                        {
                            ch.SendText("&+y(wielding twohanded)  &n");
                        }
                        else
                        {
                            ch.SendText("&+y(wielding)            &n");
                        }
                    }
                    else
                    {
                        if (obj.ItemType == ObjTemplate.ObjectType.shield
                                && (location == ObjTemplate.WearLocation.hand_one
                                     || location == ObjTemplate.WearLocation.hand_three
                                     || location == ObjTemplate.WearLocation.hand_four
                                     || location == ObjTemplate.WearLocation.hand_two)
                                && obj.HasWearFlag(ObjTemplate.WEARABLE_SHIELD))
                        {
                            ch.SendText("&+y(worn as shield)      &n");
                        }
                        else
                        {
                            ch.SendText(StringConversion.EquipmentLocationDisplay[(int)location]);
                        }
                    }
                    ch.SendText(FormatObjectToCharacter(obj, ch, true));
                    ch.SendText("\r\n");
                }
            }

            // Keep in mind that players can spam looking at someone in order
            // to increase their skill in peek - this will need to be fixed.
            if ((victim != ch && !ch.IsNPC()
                    && ((((PC)ch).SkillAptitude.ContainsKey("peek") && MUDMath.NumberPercent() < ((PC)ch).SkillAptitude["peek"])
                         || ch._level >= Limits.LEVEL_AVATAR)) || ch._riding == victim || ch._rider == victim)
            {
                ch.SendText("\r\n&nYou peek at the inventory:\r\n");
                ch.PracticeSkill("peek");
                ShowListToCharacter(victim._carrying, ch, true, true);
            }

            return;
        }

        /// <summary>
        /// Shows a list of characters to the looker.  This is used for displaying the mobs that are in a room.
        /// </summary>
        /// <param name="list"></param>
        /// <param name="ch"></param>
        public static void ShowCharacterToCharacter(List<CharData> list, CharData ch)
        {
            if (list.Count == 0)
            {
                return;
            }

            foreach (CharData listChar in list)
            {
                if (listChar == ch)
                    continue;
                if (listChar._flyLevel != ch._flyLevel)
                    continue;

                if (!listChar.IsNPC() && listChar.HasActBit(PC.PLAYER_WIZINVIS) && ch.GetTrust() < listChar.GetTrust())
                    continue;

                Visibility sight = HowSee(ch, listChar);

                if (sight == Visibility.visible)
                {
                    ShowCharacterToCharacterAbbreviated(listChar, ch);
                }
                else if (sight == Visibility.sense_infravision)
                {
                    ch.SendText(String.Format("&+rYou see the red shape of a {0} living being here.&n\r\n", Race.SizeString(listChar._size)));
                }
                else if (sight == Visibility.sense_hidden)
                {
                    ch.SendText("&+LYou sense a lifeform nearby.&n\r\n");
                }
                else if (sight == Visibility.invisible && (listChar._riding)
                    && HowSee(ch, listChar._riding) != Visibility.invisible)
                {
                    listChar._riding._rider = null;
                    ShowCharacterToCharacterAbbreviated(listChar._riding, ch);
                    listChar._riding._rider = listChar._riding;
                }

            }
        }

        /// <summary>
        /// Enhanced exit display.
        /// </summary>
        /// <param name="ch"></param>
        /// <param name="room"></param>
        public static void ShowRoomInfo(CharData ch, Room room)
        {
            if (ch == null || room == null || !ch.HasActBit(PC.PLAYER_GODMODE))
            {
                return;
            }

            String roomOpen = String.Empty;
            String roomClose = String.Empty;
            if( !ch.IsNPC() && ch._socket._terminalType == SocketConnection.TerminalType.TERMINAL_ENHANCED)
            {
                roomOpen = "<room>";
                roomClose = "</room>";
            }
            string text = String.Format("{1}[{0}] {2} ({4}){3}\r\n", room.IndexNumber, roomOpen,
                                       room.Title, roomClose, room.TerrainType );
            ch.SendText(text);

            return;
        }

        /// <summary>
        /// Returns a visibility value based on how well the looker can see the target.
        /// </summary>
        /// <param name="ch"></param>
        /// <param name="victim"></param>
        /// <returns></returns>
        public static Visibility HowSee(CharData ch, CharData victim)
        {
            if (ch == null)
            {
                Log.Error("how_see called with null ch.", 0);
                return Visibility.invisible;
            }

            if (victim == null)
            {
                Log.Error("how_see called with null victim.", 0);
                return Visibility.invisible;
            }

            // Char almost dead, or asleep.
            if (ch._position <= Position.sleeping)
            {
                return Visibility.invisible;
            }

            // All mobiles cannot see wizinvised immortals.
            if (ch.IsNPC() && !victim.IsNPC() && victim.HasActBit(PC.PLAYER_WIZINVIS))
            {
                return Visibility.invisible;
            }

            // Handles Immortal Invis.
            if (!victim.IsNPC() && victim.HasActBit(PC.PLAYER_WIZINVIS)
                    && ch.GetTrust() < victim._level)
            {
                return Visibility.invisible;
            }

            // Handles Immmortal sight.
            if (!ch.IsNPC() && ch.HasActBit(PC.PLAYER_GODMODE))
            {
                return Visibility.visible;
            }

            // Handles blindness.
            if (ch.IsAffected(Affect.AFFECT_BLIND))
            {
                return Visibility.invisible;
            }

            // Handles regular invisibility.
            if ((victim.IsAffected(Affect.AFFECT_INVISIBLE) || victim.IsAffected(Affect.AFFECT_MINOR_INVIS)))
            {
                if (ch.HasInnate(Race.RACE_DETECT_INVIS)
                        || ch.IsAffected(Affect.AFFECT_DETECT_INVIS)
                        || (ch.IsAffected(Affect.AFFECT_ELEM_SIGHT) &&
                             (victim.GetRace() == Race.RACE_AIR_ELE || victim.GetRace() == Race.RACE_WATER_ELE
                               || victim.GetRace() == Race.RACE_FIRE_ELE || victim.GetRace() == Race.RACE_EARTH_ELE)))
                {
                    if (victim.IsAffected(Affect.AFFECT_HIDE))
                    {
                        if (ch.IsAffected(Affect.AFFECT_DETECT_HIDDEN))
                        {
                            return Visibility.visible;
                        }
                        if (ch.HasInnate(Race.RACE_DETECT_HIDDEN)
                                || ch.IsAffected(Affect.AFFECT_SENSE_LIFE))
                        {
                            return Visibility.sense_hidden;
                        }
                        return Visibility.invisible;
                    }
                    return Visibility.visible;
                }
            }

            // Handles dark rooms. Added ultracheck.
            if (victim._inRoom.IsDark())
            {
                if (ch.HasInnate(Race.RACE_ULTRAVISION) || ch.IsAffected(Affect.AFFECT_ULTRAVISION))
                {
                    return Visibility.visible;
                }
                if ((ch.HasInnate(Race.RACE_INFRAVISION) || ch.IsAffected(Affect.AFFECT_INFRAVISION)
                     ) && !victim._inRoom.HasFlag(RoomTemplate.ROOM_UNDERWATER))
                {
                    return Visibility.sense_infravision;
                }
                if (!(ch.HasInnate(Race.RACE_ULTRAVISION) || ch.IsAffected(Affect.AFFECT_ULTRAVISION)))
                {
                    return Visibility.too_dark;
                }
            }

            // Handles hidden people.
            if (victim.IsAffected(Affect.AFFECT_HIDE))
            {
                if (ch.IsAffected(Affect.AFFECT_DETECT_HIDDEN))
                {
                    return Visibility.visible;
                }
                if (ch.HasInnate(Race.RACE_DETECT_HIDDEN) || ch.IsAffected(Affect.AFFECT_SENSE_LIFE))
                {
                    return Visibility.sense_hidden;
                }
                return Visibility.invisible;
            }

            return Visibility.visible;
        }

        /// <summary>
        /// Shows text for affects on a room.
        /// </summary>
        /// <param name="ch"></param>
        /// <param name="room"></param>
        public static void ShowRoomAffects(CharData ch, Room room)
        {
            if (room.HasFlag(RoomTemplate.ROOM_EARTHEN_STARSHELL))
                ch.SendText("&+yEarth moves about this room.&n\r\n");
            if (room.HasFlag(RoomTemplate.ROOM_FIERY_STARSHELL))
                ch.SendText("&+rFire burns in the air here.&n\r\n");
            if (room.HasFlag(RoomTemplate.ROOM_AIRY_STARSHELL))
                ch.SendText("&+cAir blows around viciously here.&n\r\n");
            if (room.HasFlag(RoomTemplate.ROOM_WATERY_STARSHELL))
                ch.SendText("&+bWater floats about in this room.&n\r\n");
            if (room.HasFlag(RoomTemplate.ROOM_HYPNOTIC_PATTERN))
                ch.SendText("&+mA &+bbe&+mau&+bt&+mifu&+bl pa&+mtter&+bn  floa&+mts her&+be..&n\r\n");
            if (room.HasFlag(RoomTemplate.ROOM_MAGICDARK))
                ch.SendText("It is unnaturally &+Ldark&n here.\r\n");
            if (room.HasFlag(RoomTemplate.ROOM_MAGICLIGHT))
                ch.SendText("It is unnaturally &+Wbright&n here.\r\n");
        }
    }
}
