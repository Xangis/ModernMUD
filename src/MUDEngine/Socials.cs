using System;
using System.Xml.Serialization;
using System.IO;
using System.Collections.Generic;
using ModernMUD;

namespace MUDEngine
{
    [Serializable]
    public class Socials
    {
        public List<Social> SocialData;

        /// <summary>
        /// Locate a social by name.
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        Social FindSocial(string command)
        {
            foreach (Social soc in SocialData)
            {
                if (soc.Name.StartsWith(command, StringComparison.CurrentCultureIgnoreCase))
                    return soc;
            }

            return null;
        }

        /// <summary>
        /// The main social action processing routine.  Sends the social strings and any associated sounds.
        /// </summary>
        /// <param name="ch">Character acting out the social.</param>
        /// <param name="command">Command entered by the character.</param>
        /// <param name="argument">Additional modifiers to the command entered.</param>
        /// <returns></returns>
        public bool CheckSocial(CharData ch, string command, string argument)
        {
            string arg = String.Empty;

            Social soc = FindSocial(command);
            if (soc == null)
            {
                return false;
            }

            if (!ch.IsNPC() && ch.HasActionBit(PC.PLAYER_NO_EMOTE))
            {
                ch.SendText("You are anti-social!\r\n");
                return true;
            }

            if (!ch.IsNPC() && ch.HasActionBit(PC.PLAYER_MEDITATING))
            {
                ch.RemoveActionBit(PC.PLAYER_MEDITATING);
                ch.SendText("You stop meditating.\r\n");
            }

            // Performing a social action removes hide and conceal.
            if (ch.IsAffected(Affect.AFFECT_MINOR_INVIS))
            {
                ch.SendText("You appear.\r\n");
            }
            ch.AffectStrip(Affect.AffectType.skill, "shadow form");
            ch.AffectStrip(Affect.AffectType.spell, "concealment");
            ch.RemoveAffect(Affect.AFFECT_MINOR_INVIS);
            ch.RemoveAffect(Affect.AFFECT_HIDE);

            switch (ch.CurrentPosition)
            {
                case Position.dead:
                    ch.SendText("Lie still; you are DEAD.\r\n");
                    return true;

                case Position.incapacitated:
                case Position.mortally_wounded:
                    ch.SendText("You are hurt far too badly for that.\r\n");
                    return true;

                case Position.stunned:
                    ch.SendText("You are too stunned to do that.\r\n");
                    return true;

                case Position.sleeping:
                    // Special exception - only social when you're using when asleep is "snore".
                    if (!"snore".StartsWith(soc.Name, StringComparison.CurrentCultureIgnoreCase))
                    {
                        break;
                    }
                    ch.SendText("In your dreams, or what?\r\n");
                    return true;

            }

            MUDString.OneArgument(argument, ref arg);
            CharData victim = null;
            if (arg.Length == 0)
            {
                SocketConnection.Act(soc.CharNoArgument, ch, null, victim, SocketConnection.MessageTarget.character);
                SocketConnection.Act(soc.OthersNoArgument, ch, null, victim, SocketConnection.MessageTarget.room);
                if (!String.IsNullOrEmpty(soc.AudioFile) && ch.InRoom != null)
                {
                    foreach (CharData cd in ch.InRoom.People)
                    {
                        cd.SendSound(soc.AudioFile);
                    }
                }
                return true;
            }
            victim = ch.GetCharWorld(arg);
            if (!victim || (ch.IsRacewar(victim) && ch.InRoom != victim.InRoom))
            {
                ch.SendText("They aren't here.\r\n");
            }
            else if (victim == ch)
            {
                SocketConnection.Act(soc.CharSelf, ch, null, victim, SocketConnection.MessageTarget.character);
                SocketConnection.Act(soc.OthersSelf, ch, null, victim, SocketConnection.MessageTarget.room);
                if (!String.IsNullOrEmpty(soc.AudioFile) && ch.InRoom != null)
                {
                    foreach (CharData cd in ch.InRoom.People)
                    {
                        cd.SendSound(soc.AudioFile);
                    }
                }
            }
            else if (!ch.GetCharRoom(arg) && CharData.CanSee(ch, victim)
                      && soc.CharFound.Length > 0 && soc.VictimFound.Length > 0)
            {
                if (!ch.IsImmortal())
                {
                    ch.SendText("You don't see them here.\r\n");
                    return true;
                }
                if (!victim.IsNPC())
                {
                    const string ldbase = "From far away, ";

                    if (victim.IsIgnoring(ch))
                    {
                        ch.SendText("They are ignoring you.\r\n");
                        return false;
                    }
                    Room original = ch.InRoom;
                    ch.RemoveFromRoom();
                    ch.AddToRoom(victim.InRoom);

                    string ldmsg = ldbase;
                    ldmsg += soc.CharFound;
                    SocketConnection.Act(ldmsg, ch, null, victim, SocketConnection.MessageTarget.character);

                    ldmsg = ldbase;
                    ldmsg += soc.VictimFound;
                    SocketConnection.Act(ldmsg, ch, null, victim, SocketConnection.MessageTarget.victim);

                    if (!String.IsNullOrEmpty(soc.AudioFile) && ch.InRoom != null)
                    {
                        foreach (CharData cd in ch.InRoom.People)
                        {
                            cd.SendSound(soc.AudioFile);
                        }
                    }

                    ch.RemoveFromRoom();
                    ch.AddToRoom(original);

                }
                else
                {
                    ch.SendText("They aren't here.\r\n");
                }
            }
            else
            {
                SocketConnection.Act(soc.CharFound, ch, null, victim, SocketConnection.MessageTarget.character);
                SocketConnection.Act(soc.VictimFound, ch, null, victim, SocketConnection.MessageTarget.victim);
                SocketConnection.Act(soc.OthersFound, ch, null, victim, SocketConnection.MessageTarget.everyone_but_victim);
                if (!String.IsNullOrEmpty(soc.AudioFile) && ch.InRoom != null)
                {
                    foreach (CharData cd in ch.InRoom.People)
                    {
                        cd.SendSound(soc.AudioFile);
                    }
                }

                // If mobs are to respond to socials, it should be inserted here.
                // This might be useful for some quests, mob functions, or other things.
            }

            return true;
        }

        /// <summary>
        /// Loads the socials from a file.
        /// </summary>
        /// <returns></returns>
        public static Socials Load()
        {
            string filename = FileLocation.SystemDirectory + FileLocation.SocialFile;
            string blankFilename = FileLocation.BlankSystemFileDirectory + FileLocation.SocialFile;
            XmlSerializer serializer = new XmlSerializer(typeof(Socials));
            Stream stream = null;
            try
            {
                try
                {
                    stream = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.None);
                }
                catch (FileNotFoundException)
                {
                    Log.Info("Socials file not found. Loading blank socials file.");
                    File.Copy(blankFilename, filename);
                    stream = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.None);
                }
                Socials socials = (Socials)serializer.Deserialize(stream);
                stream.Close();
                return socials;
            }
            catch (Exception ex)
            {
                Log.Error("Exception in Socials.Load(): " + ex);
                return new Socials();
            }
        }

        /// <summary>
        /// Saves the socials file.
        /// </summary>
        public void Save()
        {
            string filename = FileLocation.SystemDirectory + FileLocation.SocialFile;
            XmlSerializer serializer = new XmlSerializer(typeof(Socials));
            Stream stream = new FileStream(filename, FileMode.Create, FileAccess.Write, FileShare.None);
            serializer.Serialize(stream, this);
            stream.Close();
        }
    }
}
