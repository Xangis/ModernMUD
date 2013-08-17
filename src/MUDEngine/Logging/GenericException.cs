using System;
using System.Xml;
using System.Text.RegularExpressions;
using System.Net.Sockets;
using System.IO;
using System.Diagnostics;

namespace MUDEngine
{
    /// <summary>
    /// Generic exception handler.
    /// </summary>
    /// <remarks>
    /// Provides a generic type of exception for ease in filtering the type of exception to be 
    /// displayed in an upper level application.
    /// </remarks>
    public class GenericException : SystemException
    {
        string _name = null;
        protected string _errorCode = string.Empty;

        /// <summary>
        /// Default constructor.
        /// </summary>
        public GenericException()
        {
        }

        /// <summary>
        /// Constructs the exception with the _name
        /// </summary>
        /// <param name="name">The name.</param>
        public GenericException( string name )
            : base()
        {
            _name = name;
        }

        /// <summary>
        /// Constructs the exception with the message and _name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="message">The message.</param>
        public GenericException( string name, string message )
            : base( message )
        {
            _name = name;
        }

        /// <summary>
        /// Constructs
        /// </summary>
        /// <param name="name">The name</param>
        /// <param name="message">The message</param>
        /// <param name="errorCode">The error code</param>
        public GenericException( string name, string message, string errorCode )
            : base( message )
        {
            _name = name;
            _errorCode = errorCode;
        }


        /// <summary>
        /// Constructs the exception with the message, componentName, and inner exception.
        /// </summary>
        /// <param name="name">The component name.</param>
        /// <param name="message">The message.</param>
        /// <param name="innerException">The inner exception.</param>
        public GenericException( string name, string message, Exception innerException )
            : base( message, innerException )
        {
            _name = name;
        }

        /// <summary>
        /// Constructs the exception with the message, componentName, errorCode, and the inner exception.
        /// </summary>
        /// <param name="name">The component name.</param>
        /// <param name="message">The message.</param>
        /// <param name="innerException">The inner exception.</param>
        /// <param name="errorCode">The error code</param>
        public GenericException( string name, string message, string errorCode, Exception innerException )
            : base( message, innerException )
        {
            _name = name;
            _errorCode = errorCode;
        }

        /// <summary>
        /// Gets or sets the _name.
        /// </summary>
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
            }
        }

        /// <summary>
        /// Gets or sets the error code.
        /// </summary>
        public string ErrorCode
        {
            get
            {
                return _errorCode;
            }
            set
            {
                _errorCode = value;
            }
        }

        protected static void AddStackTrace( XmlTextWriter w, string stackTrace )
        {
            // add stack trace if available 
            if( stackTrace != null )
            {

                bool startWritten = false;
                try
                {
                    // parse the stack trace into individual lines
                    string sTerm = Environment.NewLine;

                    Regex r = new Regex( sTerm );
                    string[] parts = r.Split( stackTrace );

                    for( int i = 0; i < parts.Length; i++ )
                    {
                        string stackString = parts[ i ];
                        string[] stackLines = ParseStackLine( stackString );
                        if( stackLines[ 0 ].Trim().Length > 0 )
                        {
                            if( !startWritten )
                            {
                                w.WriteStartElement( "StackTrace" );
                                startWritten = true;
                            }
                            w.WriteStartElement( "Data" );
                            w.WriteElementString( "Method", stackLines[ 0 ] );
                            w.WriteElementString( "Source", stackLines[ 1 ] );
                            w.WriteEndElement();
                        }
                    }
                    if( startWritten )
                    {
                        w.WriteEndElement();
                        startWritten = false;
                    }
                }
                catch( Exception ex )
                {
                    // log in event log...
                    string sEvent = string.Format( "Error when trying to log stacktrace. Error: {0}", ex.Message );
                    const string sSource = "GenericException";
                    const string sLog = "Application";
                    if( !EventLog.SourceExists( sSource ) )
                        EventLog.CreateEventSource( sSource, sLog );
                    EventLog.WriteEntry( sSource, sEvent, EventLogEntryType.Warning, 3 );
                }
                finally
                {
                    if( startWritten )
                        w.WriteEndElement();
                }
            }
        }

        /// <summary>
        /// Return an Xml formatted string of its own class properties 
        /// and their current values.
        /// </summary>
        /// <returns></returns>
        public virtual void ToXml( XmlTextWriter w )
        {
            w.WriteStartElement( "GenericException" );
            WriteBaseXml( w );
            FinalizeXml( w, this );
        }

        protected void WriteBaseXml( XmlTextWriter w )
        {
            w.WriteElementString( "Message", CleanMessage( Message ) );
            if( _name != string.Empty )
            {
                w.WriteElementString( "Name", _name );
            }
            if( _errorCode != string.Empty )
            {
                w.WriteElementString( "ErrorCode", _errorCode );
            }
        }

        public static void NonGenericToXml( Exception e, XmlTextWriter w )
        {
            w.WriteStartElement( "NonGenericException" );
            w.WriteElementString( "Message", string.Format( "(Exception Type: {0}) {1}", e.GetType(), e.Message ) );

            // Write specialized properties of specific exceptions of interest
            if( e is FileNotFoundException )
            {
                w.WriteElementString( "FileName", ( e as FileNotFoundException ).FileName );
            }
            else if( e is SocketException )
            {
                w.WriteElementString( "ErrorCode", ( e as SocketException ).ErrorCode.ToString() );
                w.WriteElementString( "NativeErrorCode", ( e as SocketException ).NativeErrorCode.ToString() );
            }
            else if( e is FileLoadException )
            {
                w.WriteElementString( "FileName", ( e as FileLoadException ).FileName );
            }
            else if( e is TypeLoadException )
            {
                w.WriteElementString( "TypeName", ( e as TypeLoadException ).TypeName );
            }
            FinalizeXml( w, e );

        }

        public static void FinalizeXml( XmlTextWriter w, Exception e )
        {
            // add the inner exception.
            if( e != null && e.InnerException != null )
            {
                if( e.InnerException is GenericException )
                {
                    ( e.InnerException as GenericException ).ToXml( w );
                }
                else
                {
                    NonGenericToXml( e.InnerException, w );
                }
            }


            // Add stack trace if available 
            if (e != null) AddStackTrace( w, e.StackTrace );

            w.WriteEndElement();
            w.Flush();
        }

        /// <summary>
        /// Parses a line of a stack trace.
        /// </summary>
        /// <remarks>
        /// The Method call and the source line are separated into two strings.
        /// </remarks>
        /// <param name="stackString">The line of a stack trace. It will be of the 
        /// form "  at xxx() in c:\xyz\program.cs line 999" </param>
        /// <returns></returns>
        static public string[] ParseStackLine( String stackString )
        {
            string method = string.Empty;
            string source = string.Empty;
            Regex theReg = new Regex(
                @"(?<Prefix>\s+at\s+)(?<Method>.*)(?<Prefix2>\s+in\s+)(?<Source>.*)",
                RegexOptions.IgnoreCase
                | RegexOptions.CultureInvariant
                | RegexOptions.IgnorePatternWhitespace
                | RegexOptions.Compiled
                );

            // Try to find a string that matches our reg expression. If
            // there is no match, then it is a line of additional message
            // text which should be added to the message.
            MatchCollection theMatches = theReg.Matches( stackString );

            if( theMatches.Count > 0 )
            {

                foreach( Match theMatch in theMatches )
                {
                    if( theMatch.Length != 0 )
                    {
                        // Break the string into it's components
                        method = theMatch.Groups[ "Method" ].ToString();
                        source = theMatch.Groups[ "Source" ].ToString();
                    }
                }
            }
            else
            {
                Regex theReg2 = new Regex(
                    @"(?<Prefix>\s+at\s+)(?<Method>.*)",
                    RegexOptions.IgnoreCase
                    | RegexOptions.CultureInvariant
                    | RegexOptions.IgnorePatternWhitespace
                    | RegexOptions.Compiled
                    );

                // Try to find a string that matches our reg expression. If
                // there is no match, then it is a line of additional message
                // text which should be added to the message.
                MatchCollection theMatches2 = theReg2.Matches( stackString );

                if( theMatches2.Count > 0 )
                {
                    foreach( Match theMatch in theMatches2 )
                    {
                        if( theMatch.Length != 0 )
                        {
                            // break the string into it's components

                            // get the time and date first...
                            method = theMatch.Groups[ "Method" ].ToString();
                            source = string.Empty;
                        }
                    }
                }
            }

            return new string[] { method, source };
        }

        /// <summary>
        /// Cleans up message text so that only valid XML characters will be contained in
        /// the string.
        /// </summary>
        /// <remarks>
        /// The following are the valid XML characters and character ranges (hex values)
        ///  as defined by the W3C XML language specifications 1.0:
        ///   #x9 | #xA | #xD | [#x20-#xD7FF] | [#xE000-#xFFFD] | [#x10000-#x10FFFF]
        ///   
        ///  Since the message is concerned with only printable characters the range of 
        ///  valid data is limited to ASCII which only includes byte values between 0x20
        ///  and 0x7f.
        ///  
        /// </remarks>
        /// <param name="message"></param>
        /// <returns></returns>
        static public string CleanMessage( string message )
        {
            string clean = string.Empty;
            for( int i = 0; i < message.Length; i++ )
            {
                char c = message[ i ];
                if( c == 0x9 || c == 0xA || c == 0xD || ( c >= 0x20 && c <= 0x7F ) )
                {
                    clean += c;
                }
                else
                    clean += "?";
            }
            return clean;
        }
    }
}
