using System;
using System.Reflection;
using System.Diagnostics;

namespace MUDEngine
{
    /// <summary>
    /// Summary description for LogEntry.
    /// </summary>
    public class LogEntry
    {
        readonly string _type = string.Empty;
        readonly string _subType = string.Empty;
        readonly string _name = "NoNameGiven";
        readonly DateTime _time;
        readonly string _message = string.Empty;
        readonly Exception _exception = null;
        LogType _eventType = LogType.Misc;

        public enum LogType
        {
            Misc,
            Trace,
            Info,
            Error
        };

        public LogType EventType
        {
            get
            {
                return _eventType;
            }
            set
            {
                _eventType = value;
            }
        }

        public string Type
        {
            get
            {
                return _type;
            }
        }

        public string SubType
        {
            get
            {
                return _subType;
            }
        }

        public string Name
        {
            get
            {
                return _name;
            }
        }

        public string Message
        {
            get
            {
                return _message;
            }
        }

        public Exception LogException
        {
            get
            {
                return _exception;
            }
        }

        public DateTime Time
        {
            get
            {
                return _time;
            }
        }

        public bool IsGenericException
        {
            get
            {
                if( _exception == null )
                {
                    return false;
                }
                else
                {
                    return _exception is GenericException;
                }
            }

        }

        public LogEntry( string type, string subType, string name, string message )
        {
            _type = type;
            _subType = subType;
            _name = name;
            // Generate a _name if one is not provided
            if (name == string.Empty || name == null)
            {
                _name = GenerateName();
            }
            _time = DateTime.Now;
            _message = CleanMessage( message );
            _exception = null;
        }

        public LogEntry( string type, string subType, string name, string message, Exception e )
        {
            _type = type;
            _subType = subType;
            _name = name;
            // Generate a _name if one is not provided
            if (name == string.Empty || name == null)
            {
                _name = GenerateName();
            }
            _time = new DateTime( DateTime.Now.Ticks );
            _message = CleanMessage( message );
            _exception = e;
        }

        public LogEntry( string type, string subType, string name, string message, GenericException e )
        {
            _type = type;
            _subType = subType;
            _name = name;
            // Generate a _name if one is not provided
            if (name == string.Empty || name == null)
            {
                _name = GenerateName();
            }
            _time = new DateTime( DateTime.Now.Ticks );
            _message = CleanMessage( message );
            _exception = e;
        }

        public LogEntry( string message, string name )
        {
            _type = "";
            _subType = "";
            _name = name;
            // Generate a _name if one is not provided
            if (name == string.Empty || name == null)
            {
                _name = GenerateName();
            }
            _time = DateTime.Now;
            _message = CleanMessage( message );
            _exception = null;
        }

        public LogEntry( string message, string name, Exception e )
        {
            _type = "";
            _subType = "";
            _name = name;
            // Generate a _name if one is not provided
            if (name == string.Empty || name == null)
            {
                _name = GenerateName();
            }
            _time = DateTime.Now;
            _message = CleanMessage( message );
            _exception = e;
        }

        public LogEntry( string message, string name, GenericException e )
        {
            _type = "";
            _subType = "";
            _name = name;
            // Generate a _name if one is not provided
            if (name == string.Empty || name == null)
            {
                _name = GenerateName();
            }
            _time = DateTime.Now;
            _message = CleanMessage( message );
            _exception = e;
        }

        /// <summary>
        /// Cleans up message text so that only valid XML characters will be contained in
        /// the string.
        /// </summary>
        /// <remarks>
        /// The following are the valid XML characters and character ranges (hex values)
        ///  as defined by the W3C XML language specifications 1.0:
        ///   #x9 | #xA | #xD | [#x20-#xD7FF] | [#xE000-#xFFFD] | [#x10000-#x10FFFF]
        /// </remarks>
        /// <param name="message"></param>
        /// <returns></returns>
        private string CleanMessage( string message )
        {
            string clean = string.Empty;
            int newLineSpot;
            newLineSpot = message.IndexOf( Environment.NewLine );

            if( newLineSpot > 0 )
            {
                string s1 = message.Substring( 0, newLineSpot );
                string s2 = message.Substring( newLineSpot + 1 );
                message = s1 + "?" + s2;
            }

            for( int i = 0; i < message.Length; i++ )
            {
                char c = message[ i ];
                if( c == 0x9 || /*c == 0xA || c == 0xD ||*/ ( c >= 0x20 && c <= 0x7F ) )
                {
                    clean += c;
                }
                else
                    clean += "?";
            }
            return clean;
        }

        /// <summary>
        /// Uses Reflection to get the Class name of the calling function.
        /// 
        /// We are comparing specific Class names below, if those names were to
        /// change, this function would not be accurate anymore because it would return the
        /// first call from the stack.  This would happen if we were to obfuscate the code too!
        /// </summary>
        /// <returns></returns>
        private string GenerateName()
        {
            StackTrace stackTrace = new StackTrace();
            StackFrame stackFrame;
            MethodBase stackFrameMethod;
            int frameCount = 0;
            string typeName;
            bool isServiceBase;
            do
            {
                frameCount++;
                stackFrame = stackTrace.GetFrame( frameCount );
                stackFrameMethod = stackFrame.GetMethod();
                typeName = stackFrameMethod.ReflectedType.FullName;

                // If the typename is any of the following, then we need to walk back up the stack
                // at least one more time.  If the calling method was called from ServiceBase, then
                // we check to see if it was any of these 3 methods, LogInfo, LogTrace, and LogException.
                // If it is, we need to walk back again because these functions just wrap the actual log calls.
            } while( typeName.StartsWith( "System" ) || typeName.EndsWith( "LogEntry" ) ||
                           typeName.EndsWith( "Log" ) ||
                           stackFrameMethod.Name.EndsWith( "LogTrace" ) ||
                           stackFrameMethod.Name.EndsWith( "LogException" ) );

            return typeName;
        }
    }
}
