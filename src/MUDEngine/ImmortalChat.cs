using System;
using ModernMUD;

namespace MUDEngine
{
    /// <summary>
    /// Immortal chat channel commands.
    /// </summary>
    public class ImmortalChat
    {
        public const int IMMTALK_ON = Bitvector.BV00; /* On|Off switch */
        public const int IMMTALK_TICKS = Bitvector.BV01; /* Tick's. */
        public const int IMMTALK_LOGINS = Bitvector.BV02; /* Login|Logoff. */
        public const int IMMTALK_DEATHS = Bitvector.BV03; /* Plr death's. */
        public const int IMMTALK_RESETS = Bitvector.BV04; /* Area Reset's. */
        public const int IMMTALK_CRIME = Bitvector.BV05; /* KILLER & THIEF flag. */
        // BV06 Unused
        public const int IMMTALK_LEVELS = Bitvector.BV07; /* Level's advancing. */
        public const int IMMTALK_SECURE = Bitvector.BV08; /* log . screen. */
        public const int IMMTALK_SWITCHES = Bitvector.BV09; /* Switch. */
        public const int IMMTALK_SNOOPS = Bitvector.BV10; /* Snoops. */
        public const int IMMTALK_RESTORE = Bitvector.BV11; /* Restores. */
        public const int IMMTALK_LOAD = Bitvector.BV12; /* oload, mload. */
        public const int IMMTALK_NEWBIE = Bitvector.BV13; /* Newbie's. */
        // BV14 Unused
        // BV14 Unused
        public const int IMMTALK_DEBUG = Bitvector.BV16; /* Debug info. */
        public const int IMMTALK_PETITION = Bitvector.BV17; /* Petitions */
        public const int IMMTALK_QUESTS = Bitvector.BV18; /* Quests. */
        public const int IMMTALK_HUNTING = Bitvector.BV19; /* Hunter actions. */
        public const int IMMTALK_SPAM = Bitvector.BV20; /* Spam channel - excessive info. */
        public const int IMMTALK_HATING = Bitvector.BV21; /* Hater actions. */

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

            foreach( SocketConnection d in Database.SocketList )
            {
                if( d._connectionState == SocketConnection.ConnectionState.playing && !d.Original && d.Character.IsImmortal()
                        && d.Character.GetTrust() >= level && Macros.IsSet( ( (PC)d.Character ).ImmortalData.ImmtalkFlags, chan )
                        && Macros.IsSet(((PC)d.Character).ImmortalData.ImmtalkFlags, IMMTALK_ON) && d.Character != ch
                        && ( ch ? CharData.CanSee( d.Character, ch ) : true ) )
                {
                    d.Character.SendText( text );
                }
            }
            return;
        }
    }
}