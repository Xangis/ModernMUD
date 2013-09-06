using System;
using ModernMUD;

namespace MUDEngine
{
    /// <summary>
    /// Immortal chat channel commands.
    /// </summary>
    public class ImmortalChat
    {
        /// <summary>
        /// On|Off switch
        /// </summary>
        public const int IMMTALK_ON = Bitvector.BV00;
        /// <summary>
        /// Tick notices.
        /// </summary>
        public const int IMMTALK_TICKS = Bitvector.BV01;
        /// <summary>
        /// Logins and logouts.
        /// </summary>
        public const int IMMTALK_LOGINS = Bitvector.BV02;
        /// <summary>
        /// Player deaths.
        /// </summary>
        public const int IMMTALK_DEATHS = Bitvector.BV03;
        /// <summary>
        /// Zone resets.
        /// </summary>
        public const int IMMTALK_RESETS = Bitvector.BV04;
        /// <summary>
        /// Justice info.
        /// </summary>
        public const int IMMTALK_CRIME = Bitvector.BV05;
        // BV06 Unused
        /// <summary>
        /// Level advancement.
        /// </summary>
        public const int IMMTALK_LEVELS = Bitvector.BV07;
        public const int IMMTALK_SECURE = Bitvector.BV08; /* log . screen. */
        /// <summary>
        /// Immortal mob switching.
        /// </summary>
        public const int IMMTALK_SWITCHES = Bitvector.BV09;
        /// <summary>
        /// Snooping.
        /// </summary>
        public const int IMMTALK_SNOOPS = Bitvector.BV10;
        /// <summary>
        /// Use of the restore command.
        /// </summary>
        public const int IMMTALK_RESTORE = Bitvector.BV11;
        /// <summary>
        /// Loading mobs, objects, etc. by immortals.
        /// </summary>
        public const int IMMTALK_LOAD = Bitvector.BV12;
        /// <summary>
        /// New players.
        /// </summary>
        public const int IMMTALK_NEWBIE = Bitvector.BV13;
        // BV14 Unused
        // BV14 Unused
        /// <summary>
        /// Debugging spam.
        /// </summary>
        public const int IMMTALK_DEBUG = Bitvector.BV16;
        /// <summary>
        /// Player petitions.
        /// </summary>
        public const int IMMTALK_PETITION = Bitvector.BV17;
        /// <summary>
        /// Quest info / completion.
        /// </summary>
        public const int IMMTALK_QUESTS = Bitvector.BV18;
        /// <summary>
        /// Hunter actions.
        /// </summary>
        public const int IMMTALK_HUNTING = Bitvector.BV19;
        /// <summary>
        /// Spam channel - excessive info.
        /// </summary>
        public const int IMMTALK_SPAM = Bitvector.BV20;
        /// <summary>
        /// Hater (mob memory) actions.
        /// </summary>
        public const int IMMTALK_HATING = Bitvector.BV21;

        /// <summary>
        /// Send a message to the immortal chat channel.
        /// </summary>
        /// <param name="ch"></param>
        /// <param name="chan"></param>
        /// <param name="level"></param>
        /// <param name="message"></param>
        public static void SendImmortalChat( CharData ch, int chan, int level, string message )
        {
            if( ch == null )
            {
                return;
            }

            string text = "&+b[&+mImm&+b]&n";

            switch( chan )
            {
                default:
                    text += String.Empty;
                    break;
                case IMMTALK_TICKS:
                    text += "[TICK] ";
                    break;
                case IMMTALK_DEATHS:
                    text += "[DEATH] ";
                    break;
                case IMMTALK_RESETS:
                    text += "[RESET] ";
                    break;
                case IMMTALK_CRIME:
                    text += "[JUSTICE] ";
                    break;
                case IMMTALK_LEVELS:
                    text += "[LEVEL] ";
                    break;
                case IMMTALK_SECURE:
                    text += ">> ";
                    break;
                case IMMTALK_SWITCHES:
                    text += "[SWITCH] ";
                    break;
                case IMMTALK_SNOOPS:
                    text += "[SNOOP] ";
                    break;
                case IMMTALK_RESTORE:
                    text += "[RESTORE] ";
                    break;
                case IMMTALK_LOAD:
                    text += "[LOAD] ";
                    break;
                case IMMTALK_NEWBIE:
                    text += "[NEWBIE] ";
                    break;
                case IMMTALK_DEBUG:
                    text += "[DEBUG] ";
                    break;
                case IMMTALK_PETITION:
                    text += " ";
                    break;
                case IMMTALK_QUESTS:
                    text += "[QUESTS] ";
                    break;
                case IMMTALK_HUNTING:
                    text += "[HUNTING] ";
                    break;
                case IMMTALK_HATING:
                    text += "[HATING] ";
                    break;
                case IMMTALK_SPAM:
                    text += "[SPAM] ";
                    break;

            }
            text += message;
            text += "\r\n";

            foreach( SocketConnection socket in Database.SocketList )
            {
                if( socket.ConnectionStatus == SocketConnection.ConnectionState.playing && !socket.Original && socket.Character.IsImmortal()
                        && socket.Character.GetTrust() >= level && Macros.IsSet( ( (PC)socket.Character ).ImmortalData.ImmtalkFlags, chan )
                        && Macros.IsSet(((PC)socket.Character).ImmortalData.ImmtalkFlags, IMMTALK_ON) && socket.Character != ch
                        && ( ch ? CharData.CanSee( socket.Character, ch ) : true ) )
                {
                    socket.Character.SendText( text );
                }
            }
            return;
        }
    }
}