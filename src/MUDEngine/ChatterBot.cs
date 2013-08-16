using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.IO;
using ModernMUD;

namespace MUDEngine
{
    /// <summary>
    /// Exists for the purpose of attaching a "personality" to a mob.  Contains a set of keyphrases
    /// with responses and personality attributes/flags that govern how the mob responds to
    /// statements.
    /// </summary>
    [Serializable]
    public class ChatterBot
    {
        public List<ChatterResponse> _responses;
        public String _name; // Name of the chatterbot, i.e. "grumpy orc".
        public String _duplicateResponse;
        public String _dontUnderstandResponse;
        public bool _complainAboutDuplicates;
        public bool _complainAboutNotUnderstanding;
        [XmlIgnore]
        private String _lastMessageHeard;
        [XmlIgnore]
        private CharData _lastConversationWith;
        [XmlIgnore]
        private String _currentTopic;
        private aiml _aimlDatabase;
        private static int _numChatterBots;

        /// <summary>
        /// Constructor.
        /// </summary>
        public ChatterBot()
        {
            _aimlDatabase = new aiml();
            _responses = new List<ChatterResponse>();
            _complainAboutDuplicates = false;
            _complainAboutNotUnderstanding = false;
            _duplicateResponse = String.Empty;
            _dontUnderstandResponse = String.Empty;
            ++_numChatterBots;
        }

        ~ChatterBot()
        {
            --_numChatterBots;
        }

        public static int Count
        {
            get
            {
                return _numChatterBots;
            }
        }

        /// <summary>
        /// Loads all of the chatter bot databases.
        /// </summary>
        /// <returns></returns>
        public static bool Load()
        {
            string filename = FileLocation.SystemDirectory + FileLocation.ChatterbotFile;
            try
            {
                XmlSerializer serializer = new XmlSerializer( typeof( List<ChatterBot> ) );
                Stream stream = new FileStream( filename, FileMode.Open, FileAccess.Read, FileShare.None );
                Database.ChatterBotList = (List<ChatterBot>)serializer.Deserialize( stream );
                stream.Close();
                return true;
            }
            catch( Exception ex )
            {
                Log.Error( "Exception in ChatterBot.Load(): " + ex );
                Database.ChatterBotList = new List<ChatterBot>();
                return false;
            }
        }

        /// <summary>
        /// Saves all of the chatter bot databases.
        /// </summary>
        /// <returns></returns>
        public static bool Save()
        {
            string filename = FileLocation.SystemDirectory + FileLocation.ChatterbotFile;
            try
            {
                XmlSerializer serializer = new XmlSerializer( typeof( List<ChatterBot> ) );
                Stream stream = new FileStream( filename, FileMode.Create, FileAccess.Write, FileShare.None );
                serializer.Serialize( stream, Database.ChatterBotList );
                stream.Close();
                return true;
            }
            catch( Exception ex )
            {
                Log.Error( "Unable to save ChatterBots: " + ex );
                return false;
            }
        }

        /// <summary>
        /// Compares what the speaker has said to the chatterbot's response database and
        /// generates a reply if necessary.
        /// </summary>
        /// <param name="bot"></param>
        /// <param name="speaker"></param>
        /// <param name="argument"></param>
        /// <returns></returns>
        public bool CheckConversation( CharData bot, CharData speaker, string argument )
        {
            if( _complainAboutDuplicates && argument == _lastMessageHeard )
            {
                if( !string.IsNullOrEmpty( _duplicateResponse ) )
                {
                    CommandType.Interpret( bot, "say" + _duplicateResponse );
                    return true;
                }
                else
                {
                    CommandType.Interpret( bot, "say Don't repeat yourself.  It makes you sound like an idiot." ); 
                    return true;
                }
            }

            foreach( ChatterResponse chat in _responses )
            {
                if( argument.Contains( chat.Keyphrase ) )
                {
                    CommandType.Interpret( bot, "say" + chat.Response );
                    bot._rageFactor += chat.RageModifier;
                    return true;
                }
            }

            if( _complainAboutNotUnderstanding )
            {
                if( !string.IsNullOrEmpty( _dontUnderstandResponse ) )
                {
                    CommandType.Interpret( bot, "say" + _dontUnderstandResponse );
                    return true;
                }
                else
                {
                    CommandType.Interpret( bot, "say That doesn't mean anything to me." );
                    return true;
                }
            }
            return false;
        }
    }
}
