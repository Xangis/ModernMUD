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
        /// <summary>
        /// The bot's collection of conversational responses.
        /// </summary>
        public List<ChatterResponse> Responses { get; set; }
        /// <summary>
        /// Name of the chatterbot, i.e. "grumpy orc".
        /// </summary>
        public String Name; 
        /// <summary>
        /// What the bot says when someone repeats what they just said.
        /// </summary>
        public String DuplicateResponse { get; set; }
        /// <summary>
        /// What the bot says when it has no clue about what was said.
        /// </summary>
        public String DontUnderstandResponse { get; set; }
        /// <summary>
        /// Does the bot complain about repetition?
        /// </summary>
        public bool ComplainAboutDuplicates { get; set; }
        /// <summary>
        /// Does the bot complain about not understanding?
        /// </summary>
        public bool ComplainAboutNotUnderstanding { get; set; }
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
            Responses = new List<ChatterResponse>();
            ComplainAboutDuplicates = false;
            ComplainAboutNotUnderstanding = false;
            DuplicateResponse = String.Empty;
            DontUnderstandResponse = String.Empty;
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
            string blankFilename = FileLocation.BlankSystemFileDirectory + FileLocation.ChatterbotFile;
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<ChatterBot>));
                FileStream stream = null;
                try
                {
                    stream = new FileStream( filename, FileMode.Open, FileAccess.Read, FileShare.None );
                }
                catch (FileNotFoundException)
                {
                    Log.Info("Chatterbot file not found, using blank file.");
                    File.Copy(blankFilename, filename);
                    stream = new FileStream( filename, FileMode.Open, FileAccess.Read, FileShare.None );
                }
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
            if( ComplainAboutDuplicates && argument == _lastMessageHeard )
            {
                if( !string.IsNullOrEmpty( DuplicateResponse ) )
                {
                    CommandType.Interpret( bot, "say" + DuplicateResponse );
                    return true;
                }
                else
                {
                    CommandType.Interpret( bot, "say Don't repeat yourself.  It makes you sound like an idiot." ); 
                    return true;
                }
            }

            foreach( ChatterResponse chat in Responses )
            {
                if( argument.Contains( chat.Keyphrase ) )
                {
                    CommandType.Interpret( bot, "say" + chat.Response );
                    bot.RageFactor += chat.RageModifier;
                    return true;
                }
            }

            if( ComplainAboutNotUnderstanding )
            {
                if( !string.IsNullOrEmpty( DontUnderstandResponse ) )
                {
                    CommandType.Interpret( bot, "say" + DontUnderstandResponse );
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
