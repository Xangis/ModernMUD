using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Collections;
using System.IO.Compression;
using System.IO;
using System.Text;
using ModernMUD;

namespace MUDEngine
{
    /// <summary>
    /// Represents a socket connection and is the main container for a player.
    /// </summary>
    [Serializable]
    public class SocketConnection
    {
        /// <summary>
        /// Telnet "go ahead"
        /// </summary>
        public const byte GA = 249;
        /// <summary>
        /// IAC "will" flag.
        /// </summary>
        public const byte WILL = 251;
        /// <summary>
        /// IAC "do" flag.
        /// </summary>
        public const byte DO = 252;
        /// <summary>
        /// IAC "won't" flag.
        /// </summary>
        public const byte WONT = 253;
        /// <summary>
        /// IAC "don't" flag.
        /// </summary>
        public const byte DONT = 254;
        /// <summary>
        /// IAC, for miscellaneous terminal commands.
        /// </summary>
        public const byte IAC = 255;
        /// <summary>
        /// MCCP 1 compression flag (not supported).
        /// </summary>
        public const byte COMPRESS = 85;
        /// <summary>
        /// MCCP 2 compression flag (not supported).
        /// </summary>
        public const byte COMPRESS2 = 86;
        /// <summary>
        /// Mud Sound Protocol (MSP) flag, not supported.
        /// </summary>
        public const byte MSP = 90;
        /// <summary>
        /// Telnet echo flag.
        /// </summary>
        public const byte TELOPT_ECHO = 1;
        // These don't play well with Mono/Linux, so we just skip them.
        // They screw up input -- things like passwords fail to match if we use echo strings.
        //public static byte[] ECHO_OFF_STRING = new [] { IAC, WILL, TELOPT_ECHO };
        //public static byte[] ECHO_ON_STRING = new [] { IAC, WONT, TELOPT_ECHO };
        public static byte[] GO_AHEAD_STRING = new [] { IAC, GA };
        public static byte[] MSP_ON_STRING = new [] { IAC, WILL, MSP };
        public static byte[] MSP_OFF_STRING = new [] { IAC, WONT, MSP };
        public static byte[] MSP_DO_STRING = new [] { IAC, DO, MSP };
        public static byte[] MSP_DONT_STRING = new [] { IAC, DONT, MSP };
        public static byte[] MCCP2_ON_STRING = new [] { IAC, WILL, COMPRESS2 };
        public static byte[] MCCP2_OFF_STRING = new [] { IAC, WONT, COMPRESS2 };
        public static byte[] MCCP2_DO_STRING = new [] { IAC, DO, COMPRESS2 };
        public static byte[] MCCP2_DONT_STRING = new[] { IAC, DONT, COMPRESS2 };

        private static int _count;
        private bool _closeThisSocket;
        public SocketConnection SnoopBy { get; set; }
        public CharData Character { get; set; }
        public CharData Original { get; set; }
        public string _host = String.Empty;
        private Socket _socket;
        public ConnectionState _connectionState;
        public bool Fcommand { get; set; }
        public bool MccpEnabled { get; set; }
        public string Inbuf { get; set; }
        public string _commandQueue = String.Empty;
        public string _incomm = String.Empty;
        public List<HistoryData> _history = new List<HistoryData>();
        public int Repeat { get; set; }
        public string _showstringHead = String.Empty;
        public int ShowstringPoint { get; set; }
        public string _commandQueuePointer = String.Empty;
        public string Outbuf { get; set; }
        public string _stringEditing = String.Empty;
        public TerminalType _terminalType;

        /// <summary>
        /// String conversion.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            if (_socket != null)
            {
                return _socket.ToString();
            }
            return "Null socket.";
        }

        /// <summary>
        /// Terminal types supported.
        /// </summary>
        public enum TerminalType
        {
            /// <summary>
            /// Black and white text.
            /// </summary>
            TERMINAL_ASCII = 0,
            /// <summary>
            /// Colored text.
            /// </summary>
            TERMINAL_ANSI = 1,
            /// <summary>
            /// Graphical client.
            /// </summary>
            TERMINAL_ENHANCED = 2
        }

        /// <summary>
        /// Edit types, used in string editing and related edit functions.
        /// 
        /// We may not end up using this for anything.
        /// </summary>
        public enum EditState
        {
            none = 0,
            description,
            guild_description,
            note,
            other
        }

        /// <summary>
        /// Represents the connection state of a socket.
        /// </summary>
        public enum ConnectionState
        {
            playing = 0,
            choose_name,
            get_existing_password,
            confirm_new_name,
            choose_new_password,
            confirm_new_password,
            choose_new_race,
            confirm_new_race,
            choose_new_sex,
            choose_new_class,
            confirm_new_class,
            choose_terminal_type,
            roll_stats,
            read_motd,
            menu,
            apply_first_bonus,
            apply_second_bonus,
            apply_third_bonus,
            choose_new_hometown,
            change_password_get_old,
            change_password_get_new,
            change_password_confirm_new,
            retire_character_get_password,
            retire_character_confirm
        }

        /// <summary>
        /// MessageTarget types for Act command.
        /// </summary>
        public enum MessageTarget
        {
            /// <summary>
            /// All in room at ch's fly level.
            /// </summary>
            room = 0,
            /// <summary>
            /// All in room at ch's fly level except victim.
            /// </summary>
            everyone_but_victim,
            /// <summary>
            /// To victim.
            /// </summary>
            victim,
            /// <summary>
            /// To character.
            /// </summary>
            character,
            /// <summary>
            /// To all in room at victim's fly level except ch.
            /// </summary>
            room_vict,
            /// <summary>
            /// All in room regardless of fly level.
            /// </summary>
            all,
            /// <summary>
            /// All in room with fly level above ch.
            /// </summary>
            room_above,
            /// <summary>
            /// All in room with fly level below ch.
            /// </summary>
            room_below
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        public SocketConnection()
        {
            ++_count;
            _connectionState = ConnectionState.choose_terminal_type;
            _showstringHead = String.Empty;
            ShowstringPoint = 0;
            _socket = null;
            Fcommand = false;
            MccpEnabled = false;
            _terminalType = TerminalType.TERMINAL_ASCII;
            Repeat = 0;
            Inbuf = String.Empty;
            Outbuf = String.Empty;
            _closeThisSocket = false;
        }

        /// <summary>
        /// Destructor, decrements the instance count.
        /// </summary>
        ~SocketConnection()
        {
            --_count;
        }

        public static int Count
        {
            get
            {
                return _count;
            }
        }

        /// <summary>
        /// Exists for C++ style compatibility.
        /// </summary>
        /// <param name="socket"></param>
        /// <returns></returns>
        public static bool operator !( SocketConnection socket )
        {
            if (socket == null)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Checks whether a socket has a connection that supports ANSI color.
        /// </summary>
        /// <returns></returns>
        public bool HasColor()
        {
            if (_terminalType == TerminalType.TERMINAL_ASCII)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Channel function - sends text to a specific output channel.
        /// </summary>
        /// <param name="ch"></param>
        /// <param name="argument"></param>
        /// <param name="channel"></param>
        /// <param name="verb"></param>
        public static void SendToChannel( CharData ch, string argument, TalkChannel channel, string verb )
        {
            if (ch == null || String.IsNullOrEmpty(argument))
            {
                return;
            }

            string text;
            int position;
            
            if( String.IsNullOrEmpty(argument) )
            {
                text = String.Format( "{0} what?\r\n", verb );
                ch.SendText( text );
                return;
            }

            if( !ch.IsNPC() && ch.HasActBit(PC.PLAYER_SILENCE ) )
            {
                text = String.Format( "You can't {0}.\r\n", verb );
                ch.SendText( text );
                return;
            }

            if( !ch.CanSpeak() )
            {
                ch.SendText( "Your lips move but no sound comes out.\r\n" );
                return;
            }

            ch.SetListening(channel, true);

            // Make the words look drunk if needed...
            argument = DrunkSpeech.MakeDrunk( argument, ch );

            switch( channel )
            {
                default:
                    text = String.Format( "You {0} '{1}'\r\n", verb, argument );
                    ch.SendText( text );
                    text = String.Format( "$n&n {0}s '$t'", verb );
                    break;

                case TalkChannel.shout:
                    text = String.Format( "You {0} '{1}'\r\n", verb, argument );
                    ch.SendText( text );
                    text = String.Format( "$n&n {0}s '$t'", verb );
                    break;

                case TalkChannel.yell:
                    text = String.Format( "You {0} '{1}'\r\n", verb, argument );
                    ch.SendText( text );
                    text = String.Format( "$n&n {0}s '$t'", verb );
                    break;

                case TalkChannel.guild:
                    text = String.Format( "&+L[&+C$n&+L]&n&+c $t&n" );
                    Act( text, ch, argument, null, MessageTarget.character );
                    break;

                case TalkChannel.immortal:
                    text = String.Format( "&+L[&+r$n&+L]&n $t" );
                    position = ch._position;
                    ch._position = Position.standing;
                    Act( text, ch, argument, null, MessageTarget.character );
                    ch._position = position;
                    break;
            }

            foreach( SocketConnection socket in Database.SocketList )
            {
                CharData originalChar = socket.Original ? socket.Original : socket.Character;
                CharData targetChar = socket.Character;

                if( socket._connectionState == ConnectionState.playing && targetChar != ch
                        && !originalChar.IsListening( channel ) && !originalChar._inRoom.HasFlag( RoomTemplate.ROOM_SILENT ) )
                {
                    if( channel == TalkChannel.immortal && !originalChar.IsImmortal() )
                        continue;
                    if( channel == TalkChannel.guild && ( !originalChar.IsGuild() || !ch.IsSameGuild( originalChar ) ) )
                        continue;
                    if( channel == TalkChannel.shout && targetChar._inRoom.Area != ch._inRoom.Area && !targetChar.IsImmortal() )
                        continue;

                    position = targetChar._position;
                    if( channel != TalkChannel.shout && channel != TalkChannel.yell )
                        targetChar._position = Position.standing;
                    if( channel == TalkChannel.shout || channel == TalkChannel.yell )
                    {
                        // TODO: BUG: FIXME: Get rid of this hard-coded mob index number crap!
                        if( ( ch.IsNPC() && ( ch._mobTemplate.IndexNumber == 9316
                                  || ch._mobTemplate.IndexNumber == 9748 ) ) || (!targetChar.IsNPC() && targetChar.HasActBit(PC.PLAYER_SHOUT)))
                        {
                            // Add foreign language filter
                            if( !ch.IsNPC() )
                            {
                                Act( text, ch, TranslateText( argument, ch, originalChar ), targetChar, MessageTarget.victim );
                            }
                            else
                            {
                                Act( text, ch, argument, targetChar, MessageTarget.victim );
                            }
                        }
                    }
                    else
                    {
                        Act( text, ch, argument, targetChar, MessageTarget.victim );
                    }
                    targetChar._position = position;
                }
            }

            return;
        }

        /// <summary>
        /// Initializes a new listening socket.
        /// </summary>
        /// <param name="port"></param>
        /// <returns></returns>
        public Socket InitializeSocket( ushort port )
        {
            Socket socket = new Socket( AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp );
            socket.SetSocketOption( SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true );
            socket.Bind( new IPEndPoint(IPAddress.Any, port) );
            socket.Listen( 3 );
            return socket;
        }

        /// <summary>
        /// Main game loop.
        /// 
        /// We have to be careful here about removing sockets from the listen, write,
        /// and error lists - we're iterating over collections, so removing items
        /// from them can be touchy.
        /// </summary>
        /// <param name="control">Control socket.  Probably unnecessary.</param>
        public void MainGameLoop(Socket control)
        {
            DateTime lastTime = DateTime.Now;
            Database.SystemData.CurrentTime = lastTime;

            while( !Database.SystemData.GameIsDown )
            {
                // Close any sockets that have been flagged to be closed.  Sockets are
                // typically flagged as to-be-closed in the Disconnect() or Terminate()
                // command functions (via calls to CloseSocket, which does not specifically
                // close a socket, but rather flags a socket as to be closed.
                for(int i = (Database.SocketList.Count - 1); i >= 0; i-- )
                {
                    if (Database.SocketList[i]._closeThisSocket)
                    {
                        Socket sock = Database.SocketList[i]._socket;
                        Database.SocketList.RemoveAt(i);
                        sock.Close();
                    }
                }

                ArrayList listenList = new ArrayList();
                ArrayList writeList = new ArrayList();
                ArrayList errorList = new ArrayList();
                // Add the control socket, which is in a listen state, so we can accept new
                // connections.
                listenList.Add(control);
                // Build a list of all active sockets.
                foreach( SocketConnection connection in Database.SocketList )
                {
                    if (connection._socket != null)
                    {
                        listenList.Add(connection._socket);
                        writeList.Add(connection._socket);
                        errorList.Add(connection._socket);
                    }
                }
                // Select to find out what to do with each socket.  Timeout value is in microseconds.
                try
                {
                    Socket.Select( listenList, writeList, errorList, 1000 );
                }
                catch( SocketException ex )
                {
                    Log.Error( "Socket error {0} in Select(): " + ex, ex.ErrorCode );
                }

                // New connection?
                if (listenList.Contains(control))
                {
                    AcceptSocket(control);
                }

                ProcessSocketWrites(writeList);
                ProcessSocketReads(listenList);
                ProcessSocketErrors(errorList);
                ProcessPlayerActions();

                // Autonomous game motion handled by event system.
                Event.EventUpdate();

                // Synchronize to a clock.
                // Sleep( last_time + 1/Event.TICK_PER_SECOND - now ).
                bool timesUp = false;
                TimeSpan span;

                while( !timesUp )
                {
                    DateTime elapsedTime = DateTime.Now;
                    span = elapsedTime - Database.SystemData.CurrentTime;
                    if (span.TotalMilliseconds > (1000 / Event.TICK_PER_SECOND))
                    {
                        timesUp = true;
                    }
                    else
                    {
                        int sleeptime = (int)((1000 / Event.TICK_PER_SECOND) - span.TotalMilliseconds);
                        System.Threading.Thread.Sleep(sleeptime);
                        timesUp = true;
                    }
                }
                Database.SystemData.CurrentTime = DateTime.Now;
            }

            return;
        }

        /// <summary>
        /// Accepts an incoming socket connection.
        /// </summary>
        /// <param name="control"></param>
        static void AcceptSocket(Socket control)
        {
            if (control == null)
            {
                Log.Error("SocketConnection.AcceptSocket() called with null control socket.");
                return;
            }

            string text = String.Empty;
            Socket socket;

            if( ( socket = control.Accept()) == null )
            {
                throw new Exception( "Exception: " + "AcceptSocket: accept returned null" );
            }

            SocketConnection newSocket = new SocketConnection();
            newSocket._socket = socket;
            newSocket._host = socket.RemoteEndPoint.ToString();
            String logStr = String.Format( "New connection: {0} ({1})", newSocket._host, text );
            ImmortalChat.SendImmortalChat( null, ImmortalChat.IMMTALK_LOGINS, Limits.LEVEL_OVERLORD, logStr );
            Log.Info(logStr);

            // Site ban list, for those bastards who just don't know how to behave.
            foreach( BanData ban in Database.BanList )
            {
                if( !MUDString.IsSuffixOf( ban.Name, newSocket._host ) )
                {
                    newSocket.WriteToSocket( "Your site has been banned from this Mud.\r\n",false);
                    socket.Close();
                    return;
                }
            }

            // Add to our socket list.
            Database.SocketList.Add( newSocket );

            // Hidden option #5 is "enhanced terminal". This is automatically sent by the wxMudClient and
            // WPFMUDClient.
            newSocket.WriteToBuffer( "Enter your terminal type (0 for ASCII, 1 for ANSI): " );

            // Keep track of number of players and maximum number of players.
            if( ++Database.SystemData.NumPlayers > Database.SystemData.MaxPlayers )
                Database.SystemData.MaxPlayers = Database.SystemData.NumPlayers;
            if( Database.SystemData.MaxPlayers > Database.SystemData.MaxPlayersEver )
            {
                Database.SystemData.MaxPlayersTime = DateTime.Now;
                Database.SystemData.MaxPlayersEver = Database.SystemData.MaxPlayers;
                string logbuf = "Broke all-time maximum player record: " + Database.SystemData.MaxPlayersEver;
                Log.Trace( logbuf );
                Sysdata.Save();
            }
            return;
        }

        /// <summary>
        /// Closes this socket connection.
        /// </summary>
        public void CloseSocket()
        {
            if (Outbuf.Length > 0)
            {
                ProcessOutput(false);
            }

            if( SnoopBy != null )
            {
                SnoopBy.WriteToBuffer( "Your prey has fled the realm.\r\n" );
            }

            foreach( SocketConnection it in Database.SocketList )
            {
                if (it.SnoopBy == this)
                {
                    it.SnoopBy = null;
                }
            }
            if (Character)
            {
                string logBuf = String.Format("Closing link to {0}.", Character._name);
                Log.Trace( logBuf );
                if( IsPlaying() )
                {
                    Act("$n has been lost to the &+Rab&n&+ry&+Rs&n&+rs&n.", Character, null, null, MessageTarget.room);
                    logBuf = Character._name + " has lost link.";
                    CharData.SavePlayer(Character);
                    // Remove them from game in 10 min.
                    Event.CreateEvent(Event.EventType.extract_character, 10 * 60 * Event.TICK_PER_SECOND, Character, null, null);
                    ImmortalChat.SendImmortalChat(Character, ImmortalChat.IMMTALK_LOGINS, 0, logBuf);
                    Character._socket = null;
                }
                else
                {
                    Character = null;
                }
            }

            _closeThisSocket = true;

            --Database.SystemData.NumPlayers;

            return;
        }

        /// <summary>
        /// Read incoming data from a socket connection.
        /// </summary>
        /// <returns>bool representing success or failure.</returns>
        bool ReadFromSocket()
        {
            if( Inbuf.Length >= 4096 )
            {
                string logBuf = String.Format( "{0} input overflow!", _host );
                Log.Trace( logBuf );
                WriteToSocket("\r\n*** PUT A LID ON IT!!! ***\r\n", MccpEnabled);
                return false;
            }

            for( ; ; )
            {
                int numRead;
                byte[] buffer = new byte[ 2048 ];
                try
                {
                    numRead = _socket.Receive( buffer );
                }
                catch( SocketException ex )
                {
                    if (ex.ErrorCode == 10054)
                    {
                        Log.Trace("Connection closed by remote host for socket " + _host);
                    }
                    else
                    {
                        Log.Info("Socket exception received: " + ex);
                    }
                    return false;
                }
                catch( Exception ex )
                {
                    Log.Error( "Exception reading from socket: " + ex );
                    return false;
                }
                if( numRead > 0 )
                {
                    if( MccpEnabled )
                    {
                        MemoryStream ms = new MemoryStream();
                        // Use the newly created memory stream for the compressed data.
                        DeflateStream compressedzipStream = new DeflateStream( ms, CompressionMode.Decompress, true );
                        compressedzipStream.Write( buffer, 0, buffer.Length );
                        // Close the stream.
                        compressedzipStream.Close();
                        buffer = ms.GetBuffer();
                    }
                    int count;
                    for( count = 0; count < numRead; count++ )
                    {                 
                        Inbuf += (char)buffer[count];
                    }
                    return true;
                }
                if( numRead == 0 )
                {
                    Log.Trace( "EOF found on read." );
                    return false;
                }
                Log.Error("Unknown error in SocketConnection.ReadFromSocket(): ");
            }
        }

        /// <summary>
        /// Transfer one line from input buffer to input line.
        /// </summary>
        void ReadFromBuffer()
        {
            // Hold horses if pending command already.
            if( _incomm.Length > 0 )
                return;

            // Do nothing if there's nothing to rea
            if (Inbuf.Length <= 0)
                return;

            // Look for at least one new line.
            if( !Inbuf.Contains( "\n" ) )
                return;

            int chars = 0;
            // Canonical input processing.
            foreach( char ch in Inbuf )
            {
                // Track each char we've processed
                ++chars;
                
                if (ch == '\n')
                {
                    break;
                }

                // Just skip these.
                if (ch == '\r')
                {
                    continue;
                }

                if( char.IsLetterOrDigit( ch ) || char.IsPunctuation(ch )
                      || char.IsSeparator( ch ) || char.IsWhiteSpace( ch ) 
                      || char.IsSymbol( ch ))
                    _incomm += ch;
            }
            // Trim off the characters we've processed.
            Inbuf = Inbuf.Substring( chars );

            // Empty string in the incoming buffer.
            if (String.IsNullOrEmpty(_incomm))
            {
                return;
            }

            // Do '!' substitution.
            if( _incomm[ 0 ] == '!' )
            {
                HistoryData command = null;
                if (_history.Count == 1 || _incomm.Length == 1)
                {
                    command = _history[(_history.Count - 1)];
                }
                else if (MUDString.IsNumber(_incomm.Substring(1)))
                {
                    int line;

                    Int32.TryParse(_incomm.Substring(1), out line);
                    --line; // Correct for 0-based index.
                    if (line < _history.Count)
                    {
                        command = _history[line];
                    }
                }
                if( command != null )
                {
                    _incomm = command.Command;
                    string buf = _incomm + "\r\n";
                    WriteToSocket(buf, MccpEnabled);
                }
            }

            if( _incomm[ 0 ] != '!' && IsPlaying() )
            {
                HistoryData command = new HistoryData();
                command.Command = _incomm;

                // Remove one if history is full.
                if (_history.Count > Limits.MAX_HISTORY)
                {
                    _history.RemoveAt(0);
                }
                _history.Add(command);
            }

            return;
        }

        /// <summary>
        /// Low level output function.
        /// </summary>
        /// <param name="fPrompt"></param>
        /// <returns></returns>
        bool ProcessOutput( bool showPrompt )
        {
            if (Database.SystemData.GameIsDown)
            {
                return false;
            }

            // Show prompt.
            if( showPrompt && IsPlaying() )
            {
                if( ShowstringPoint < _showstringHead.Length )
                {
                    int shownLines = 0;
                    int ptr;
                    for( ptr = 0; ptr != ShowstringPoint; ptr++ )
                    {
                        if( _showstringHead[ ptr ] == '\n' ) // causes error in valgrind
                        {
                            shownLines++;
                        }
                    }

                    int totalLines = shownLines;
                    for( ptr = ShowstringPoint; ptr < _showstringHead.Length; ptr++ ) // causes error in valgrind
                    {
                        if( _showstringHead[ ptr ] == '\n' ) // causes error in valgrind
                        {
                            totalLines++;
                        }
                    }

                    // TODO: Fix and re-enable the pager.
                    //string buf = String.Format( "\r\n{0}%%) Please type (c)ontinue, (q)uit, (b)ack, (r)efresh, or ENTER.\r\n",
                    //                            100 * shown_lines / total_lines );
                    //WriteToBuffer( buf );
                }
                else
                {
                    if( _stringEditing.Length != 0)
                    {
                        WriteToBuffer( "> " );
                    }
                    else
                    {
                        if( _connectionState == ConnectionState.playing && Outbuf.Length > 0)
                        {
                            CharData ch = Original ? Original : Character;
                            if( ch.HasActBit(PC.PLAYER_BLANK ) )
                                WriteToBuffer( "\r\n" );

                            if( ch.HasActBit(PC.PLAYER_PROMPT ) )
                                ShowPrompt();

                            if( ch.HasActBit(PC.PLAYER_TELNET_GA ) )
                                WriteToBuffer( GO_AHEAD_STRING );
                        }
                    }
                }
            }

            // Short-circuit if nothing to write.
            if( Outbuf.Length == 0 )
            {
                return true;
            }

            // Snoop-o-rama.
            if( SnoopBy )
            {
                SnoopBy.WriteToBuffer( "% " );
                SnoopBy.WriteToBuffer( Outbuf );
            }

            // OS-dependent output.
            if (!WriteToSocket(Outbuf, MccpEnabled)) 
            {
                Outbuf = String.Empty;
                return false;
            }
            Outbuf = String.Empty;
            return true;
        }

        /// <summary>
        /// Shows the graphical client-friendly version of the prompt.
        /// </summary>
        private void ShowEnhancedPrompt(CharData ch)
        {
            StringBuilder output = new StringBuilder();
            output.Append("<prompt>");
            // H = Hits.
            output.Append("H:");
            output.Append(ch._hitpoints);
            // I = Max Hits.
            output.Append(" I:");
            output.Append(ch.GetMaxHit());
            // M = Moves.
            output.Append(" M:");
            output.Append(ch._currentMoves);
            // N = Max Moves.
            output.Append(" N:");
            output.Append(ch._maxMoves);
            // A = Mana.
            output.Append(" A:");
            output.Append(ch._currentMana);
            // B = Max Mana
            output.Append(" B:");
            output.Append(ch._maxMana);
            // P = Player Name.
            output.Append(" P:");
            output.Append((ch.ShowNameTo(ch, true)).Replace(' ', '_'));
            // Q = Player Condition.
            output.Append(" Q:");
            output.Append((ch._hitpoints * 100) / ch._maxHitpoints);
            output.Append(" R:");
            output.Append(Position.PositionString(ch._position));
            // T = Tank Name.
            output.Append(" T:");
            if (ch._fighting && ch._fighting._fighting)
            {
                output.Append((ch._fighting._fighting.ShowNameTo(ch, true)).Replace(' ', '_'));
                // U = Enemy Condition.
                output.Append(" U:");
                output.Append((ch._fighting._fighting._hitpoints * 100) / ch._fighting._fighting.GetMaxHit());
                output.Append(" V:");
                output.Append(Position.PositionString(ch._fighting._fighting._position));
            }
            else
            {
                output.Append("0 U:0 V:0");
            }
            // E = Enemy Name.
            output.Append(" E:");
            if (ch._fighting)
            {
                output.Append((ch._fighting.ShowNameTo(ch, true)).Replace(' ', '_'));
                // F = Enemy Condition.
                output.Append(" F:");
                output.Append((ch._fighting._hitpoints * 100) / ch._fighting.GetMaxHit());
                output.Append(" G:");
                output.Append(Position.PositionString(ch._fighting._position));
                output.Append(" ");
            }
            else
            {
                output.Append("0 F:0 G:0");
            }
            output.Append("</prompt>");
            ch.SendText(output.ToString());
        }

        /// <summary>
        /// Parses tokens in a player's prompt string and builds the necessary prompt,
        /// which is then sent to that player.
        /// </summary>
        void ShowPrompt()
        {
            string output = String.Empty;
            string coloredoutput;

            /* Will always have a pc ch after this */
            CharData ch = ( Original ? Original : Character );
            if( ( (PC)ch ).Prompt.Length == 0 )
            {
                ch.SendText( "\r\n\r\n" );
                return;
            }

            if( !ch.IsNPC() && ch._socket._terminalType == TerminalType.TERMINAL_ENHANCED )
            {
                ShowEnhancedPrompt(ch);
                return;
            }

            int position = 0;
            string str = ( (PC)ch ).Prompt;

            while( position < str.Length )
            {
                if( str[position] != '%' )
                {
                    output += str[ position ];
                    ++position;
                    continue;
                }
                // We have an argument, check the next char.
                ++position;
                // Abort if we went past the end of the string.
                if( position >= str.Length )
                    break;
                switch( str[position] )
                {
                    default:
                        output += " ";
                        break;
                    case 'h':
                        //Set it so that the hitpoints of a character
                        // show up a different color dependent on level of injuries.

                        if( ch._hitpoints < ( ch.GetMaxHit() / 10 ) )
                            output += String.Format( "&+R{0}&+L", ch._hitpoints );
                        else if( ch._hitpoints < ( ch.GetMaxHit() / 3 ) )
                            output += String.Format( "&n&+r{0}&+L", ch._hitpoints );
                        else if( ch._hitpoints < ( ch.GetMaxHit() * 2 / 3 ) )
                            output += String.Format( "&n&+m{0}&+L", ch._hitpoints );
                        else
                            output += ch._hitpoints.ToString();
                        break;
                    case 'H':
                        output += ch.GetMaxHit().ToString();
                        break;
                    case 'n':
                        output += String.Format( "\r\n" );
                        break;
                    case 'l':
                        if( ch._position <= Position.sleeping )
                            output += String.Format("&n&+r{0}&n", System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(Position.PositionString(ch._position)));
                        else if( ch._position <= Position.kneeling )
                            output += String.Format("&n&+y{0}&n", System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(Position.PositionString(ch._position)));
                        else
                            output += String.Format("&n&+g{0}&n", System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(Position.PositionString(ch._position)));
                        break;
                    case 'm':
                        // Set it so that the mana of a character
                        // show up a different color dependent on how low it is.

                        if( ch._currentMana < ( ch._maxMana / 10 ) )
                            output += String.Format( "&+R{0}&+L", ch._currentMana );
                        else if( ch._currentMana < ( ch._maxMana / 3 ) )
                            output += String.Format( "&n&+r{0}&+L", ch._currentMana );
                        else if( ch._currentMana < ( ( ch._maxMana * 2 ) / 3 ) )
                            output += String.Format( "&n&+m{0}&+L", ch._currentMana );
                        else
                            output += ch._currentMana.ToString();
                        break;
                    case 'M':
                        output += ch._maxMana.ToString();
                        break;
                    case 'u':
                        output += Database.SystemData.NumPlayers.ToString();
                        break;
                    case 'U':
                        output += Database.SystemData.MaxPlayers.ToString();
                        break;
                    case 'v':
                        // Set it so that the moves of a character
                        // show up a different color dependent on how tired they are.
                        if( ch._currentMoves < ( ch._maxMoves / 10 ) )
                            output += String.Format( "&+R{0}&+L", ch._currentMoves );
                        else if( ch._currentMoves < ( ch._maxMoves / 3 ) )
                            output += String.Format( "&n&+r{0}&+L", ch._currentMoves );
                        else if( ch._currentMoves < ( ( ch._maxMoves * 2 ) / 3 ) )
                            output += String.Format( "&n&+y{0}&+L", ch._currentMoves );
                        else
                            output += ch._currentMoves.ToString();
                        break;
                    case 'V':
                        output += ch._maxMoves.ToString();
                        break;
                    //
                    // Altered Tank Status Crapola
                    //
                    case 'e':
                        // Display (E)nemy's status as Chain Meter in Combat
                        if( ch._fighting )
                        {
                            output += String.Format( "{0}: {1} ", ch._fighting.ShowNameTo( ch, true ),
                                    ConditionMeter( ch._fighting ) );
                        }
                        else
                        {
                            output += String.Format( "&n" );
                        }
                        break;
                    case 'E':
                        // Display (E)nemy's status as New Text in Combat
                        if( ch._fighting )
                        {
                            output += String.Format( "{0}: {1} ", ch._fighting.ShowNameTo(ch, true),
                                    ConditionString( ch._fighting ) );
                        }
                        else
                        {
                            output += String.Format( "&n" );
                        }
                        break;
                    case 'b':
                        // Added for Old Meters - (B)ad Guy condition Standard status bar here.
                        if( ch._fighting )
                        {
                            output += String.Format( "{0}: {1} ", ch._fighting.ShowNameTo(ch, true),
                                    ConditionMeter2( ch._fighting ) );
                        }
                        else
                        {
                            output += String.Format( "&n" );
                        }
                        break;
                    case 'B':
                        // Added for Old Text Colors
                        // add (B)ad Guy condition Standard Text bar here.
                        if( ch._fighting )
                        {
                            output += String.Format( "{0}: {1} ",
                                    ch._fighting.ShowNameTo(ch, true),
                                    ConditionString2(ch._fighting));
                        }
                        else
                        {
                            output += String.Format( "&n" );
                        }
                        break;
                    case 'd':
                        // Added for Old Meters
                        // add (D)efensive tank condition Standard status bar here.
                        if( ch._fighting && ch._fighting._fighting )
                        {
                            output += String.Format( "{0}: {1} ",
                                    ch._fighting._fighting.ShowNameTo(ch, true),
                                    ConditionMeter2( ch._fighting._fighting ) );
                        }
                        else
                        {
                            output += String.Format( "&n" );
                        }
                        break;
                    case 'D':
                        // Added for Old Meters
                        // add (D)efensive tank condition Old Text Colors here.
                        if( ch._fighting && ch._fighting._fighting )
                        {
                            output += String.Format( "{0}: {1} ",
                                    ch._fighting._fighting.ShowNameTo(ch, true),
                                    ConditionString2( ch._fighting._fighting ) );
                        }
                        else
                        {
                            output += String.Format( "&n" );
                        }
                        break;
                    case 't':
                        // New Meter:
                        // Add (T)ank condition Chain Status bar here.
                        if( ch._fighting && ch._fighting._fighting )
                        {
                            output += String.Format( "{0}: {1} ",
                                    ch._fighting._fighting.ShowNameTo(ch, true),
                                    ConditionMeter( ch._fighting._fighting ) );
                        }
                        else
                        {
                            output += String.Format( "&n" );
                        }
                        break;
                    case 'T':
                        // Add tank condition NEW Text Strings here.
                        if( ch._fighting && ch._fighting._fighting )
                        {
                            output += String.Format( "{0}: {1} ",
                                    ch._fighting._fighting.ShowNameTo(ch, true),
                                    ConditionString( ch._fighting._fighting ) );
                        }
                        else
                        {
                            output += String.Format( "&n" );
                        }
                        break;
                    case 'P':
                        // Add player (not Tank) status strings here.
                        // New Text Section
                        if( ch._fighting )
                        {
                            output += String.Format("{0}: {1} ", ch.ShowNameTo(ch, true), ConditionString(ch));
                        }
                        else
                        {
                            output += String.Format( "&n" );
                        }
                        break;
                    case 'p':
                        // Chain Meter for PC (Not Tank)
                        if( ch._fighting )
                        {
                            output += String.Format("{0}: {1} ", ch.ShowNameTo(ch, true), ConditionMeter(ch));
                        }
                        else
                        {
                            output += String.Format( "&n" );
                        }
                        break;
                    case 'Q':
                        // Add player (not Tank) status strings here.
                        // Old Text Section
                        if( ch._fighting )
                        {
                            output += String.Format("{0}: {1} ", ch.ShowNameTo(ch, true), ConditionString2(ch));
                        }
                        else
                        {
                            output += String.Format( "&n" );
                        }
                        break;
                    case 'q':
                        // Old Meter for PC (Not Tank)
                        if( ch._fighting )
                        {
                            output += String.Format("{0}: {1} ", ch.ShowNameTo(ch, true), ConditionMeter2(ch));
                        }
                        else
                        {
                            output += String.Format( "&n" );
                        }
                        break;
                    case 'a':
                        if( ch._level > 10 )
                            output += String.Format( "{0}", ch._alignment );
                        else
                            output += String.Format( "{0}", ch.IsGood() ? "good" : ch.IsEvil() ? "evil" : "neutral" );
                        break;
                    case 'r':
                        if (ch._inRoom)
                        {
                            output += String.Format("{0}",
                                    ((!ch.IsNPC() && ch.HasActBit(PC.PLAYER_GODMODE)) ||
                                        (!ch.IsAffected(Affect.AFFECT_BLIND) &&
                                        !ch._inRoom.IsDark()))
                                    ? ch._inRoom.Title : "darkness");
                        }
                        else
                        {
                            output += String.Format(" ");
                        }
                        break;
                    case 'R':
                        if( ch.IsImmortal() && ch._inRoom )
                            output += String.Format( "{0}", ch._inRoom.IndexNumber );
                        else
                            output += String.Format( " " );
                        break;
                    case 'z':
                        if( ch.IsImmortal() && ch._inRoom )
                            output += String.Format( "{0}", ch._inRoom.Area.Name );
                        else
                            output += String.Format( " " );
                        break;
                    case 'i':
                        output += String.Format( "{0}", ch.IsAffected( Affect.AFFECT_INVISIBLE ) ?
                                "invisible" : "visible" );
                        break;
                    case 'I':
                        if (ch.IsImmortal())
                        {
                            output += String.Format("(wizinv: {0})", ch.HasActBit(PC.PLAYER_WIZINVIS) ?
                                    "on" : "off");
                        }
                        else
                        {
                            output += String.Format(" ");
                        }
                        break;
                    case '%':
                        output += String.Format( "%%" );
                        break;
                }
                ++position;
            }

            ColorConvert( out coloredoutput, output, ch );
            WriteToBuffer( coloredoutput );
            return;
        }

        /// <summary>
        /// Builds a condition string of style #2 for a player.
        /// </summary>
        /// <param name="ch"></param>
        /// <returns></returns>
        static string ConditionString2( CharData ch )
        {
            int percent;

            if( ch.GetMaxHit() > 0 )
                percent = ( 100 * ch._hitpoints ) / ch.GetMaxHit();
            else
                percent = -1;

            if( percent >= 100 )
                return "&+CExcellent&n";
            if( percent >= 90 )
                return "&+LBarely &n&+cScratched&n";
            if( percent >= 79 )
                return "&+CBa&n&+ct&+Ct&n&+cer&+Ce&n&+cd&+L and &n&+mBr&+Mu&n&+mi&+Mse&n&+md&n";
            if( percent >= 69 )
                return "&+LLightly &+MWo&n&+mu&+Mn&n&+mde&+Md&n";
            if( percent >= 58 )
                return "&+LModerately &+MWo&+Ru&+Mn&+Rde&+Md&n";
            if( percent >= 48 )
                return "&+LSeverely &n&+rWo&n&+mu&+rn&+mde&n&+rd&n";
            if( percent >= 37 )
                return "&n&+rBl&+Re&n&+re&+Rdi&n&+rn&+Rg&+L Freely&n";
            if( percent >= 27 )
                return "&+LBathed in &n&+rBl&+Ro&n&+ro&+Rd&n";
            if( percent >= 16 )
                return "&+LLeaking&n&+r Guts&n";
            if( percent >= 6 )
                return "&+RWr&n&+ri&+Rt&n&+rhi&+Rn&n&+rg&+L in &n&+rA&+Rgo&n&+rn&+Ry&n";
            return "&+LAt &n&+wDeath&+L's Door&n";
        }

        /// <summary>
        /// Builds a condition string for a player.
        /// </summary>
        /// <param name="ch"></param>
        /// <returns></returns>
        public static string ConditionString( CharData ch )
        {
            int percent;

            if( ch.GetMaxHit() > 0 )
                percent = ( 100 * ch._hitpoints ) / ch.GetMaxHit();
            else
                percent = -1;

            if( percent >= 100 )
                return "&+gexcellent&n";
            if( percent >= 90 )
                return "&+ybarely &n&+gscratched&n";
            if( percent >= 79 )
                return "&+yslightly&+L &n&+gbruised&n";
            if( percent >= 69 )
                return "&+ylightly wounded&n";
            if( percent >= 58 )
                return "&+ymoderately &n&+mwou&+rnded&n";
            if( percent >= 48 )
                return "&+mseverely &n&+rwo&n&+yu&+rn&+yde&n&+rd&n";
            if( percent >= 37 )
                return "&n&+rbleeding&+y copiously&n";
            if( percent >= 27 )
                return "&+rbadly &n&+rwo&+Ru&n&+rn&+Rded&n";
            if( percent >= 16 )
                return "&+yin &n&+R awful shape&n";
            if( ch._hitpoints > -3 )
                return "&+Rnearly&+L &n&+rdead&n";
            if( ch._hitpoints > -5 )
                return "&+Lincapacitated, and &+Rbl&n&+re&+Re&n&+rdi&+Rn&n&+rg&+L to death&n";
            if( ch._hitpoints >= -10 )
                return "&+rmortally &+Rw&n&+rou&+Rnd&N&+re&+Rd&n";
            return "&+Wdead&n";
        }

        /// <summary>
        /// Builds condition meter of style #1 for a player.
        /// </summary>
        /// <param name="ch"></param>
        /// <returns></returns>
        static string ConditionMeter( CharData ch )
        {
            int percent;

            if( ch.GetMaxHit() > 0 )
                percent = ( 100 * ch._hitpoints ) / ch.GetMaxHit();
            else
                percent = -1;

            if( percent >= 100 )
                return "&+L(&n&+c=&+C-&n&+c=&+C-&n&+c=&+C-&n&+c=&+C-&n&+c=&+C-&+L)&n";
            if( percent >= 90 )
                return "&+L(&+C-&n&+c=&+C-&n&+c=&+C-&n&+c=&+C-&n&+c=&+C-&+R=&+L)&n";
            if( percent >= 79 )
                return "&+L(&n&+c=&+C-&n&+c=&+C-&n&+c=&+C-&n&+c=&+C-&+R=&n&+r-&+L)&n";
            if( percent >= 69 )
                return "&+L(&+C-&n&+c=&+C-&n&+c=&+C-&n&+c=&+C-&+R=&n&+r-&+R=&+L)&n";
            if( percent >= 58 )
                return "&+L(&n&+c=&+C-&n&+c=&+C-&n&+c=&+C-&+R=&n&+r-&+R=&n&+r-&+L)&n";
            if( percent >= 48 )
                return "&+L(&+C-&n&+c=&+C-&n&+c=&+C-&+R=&n&+r-&+R=&n&+r-&+R=&+L)&n";
            if( percent >= 37 )
                return "&+L(&n&+c=&+C-&n&+c=&+C-&+R=&n&+r-&+R=&n&+r-&+R=&n&+r-&+L)&n";
            if( percent >= 27 )
                return "&+L(&+C-&n&+c=&+C-&+R=&n&+r-&+R=&n&+r-&+R=&n&+r-&+R=&+L)&n";
            if( percent >= 16 )
                return "&+L(&n&+c=&+C-&+R=&n&+r-&+R=&n&+r-&+R=&n&+r-&+R=&n&+r-&+L)&n";
            if( percent >= 6 )
                return "&+L(&+C-&+R=&n&+r-&+R=&n&+r-&+R=&n&+r-&+R=&n&+r-&+R=&+L)&n";
            return "&+L(&+R=&n&+r-&+R=&n&+r-&+R=&n&+r-&+R=&n&+r-&+R=&n&+r-&+L)&n";
        }

        /// <summary>
        /// Builds a condition meter of style #2 for a player.
        /// </summary>
        /// <param name="ch"></param>
        /// <returns></returns>
        static string ConditionMeter2( CharData ch )
        {
            if (ch == null) return String.Empty;
            int percent;

            if( ch.GetMaxHit() > 0 )
                percent = ( 100 * ch._hitpoints ) / ch.GetMaxHit();
            else
                percent = -1;

            if( percent >= 100 )
                return "&+L(&+r+++&+y++&+Y++&n&+g+++&+L)&n";
            if( percent >= 90 )
                return "&+L(&+r+++&+y++&+Y++&n&+g++&+L-&+L)&n";
            if( percent >= 79 )
                return "&+L(&+r+++&+y++&+Y++&n&+g+&+L--&+L)&n";
            if( percent >= 69 )
                return "&+L(&+r+++&+y++&+Y++&+L---&+L)&n";
            if( percent >= 58 )
                return "&+L(&+r+++&+y++&+Y+&+L----&+L)&n";
            if( percent >= 48 )
                return "&+L(&+r+++&+y++&+L-----&+L)&n";
            if( percent >= 37 )
                return "&+L(&+r+++&+y+&+L------&+L)&n";
            if( percent >= 27 )
                return "&+L(&+r+++&+L-------&+L)&n";
            if( percent >= 16 )
                return "&+L(&+r++&+L--------&+L)&n";
            if( percent >= 6 )
                return "&+L(&+r+&+L---------&+L)&n";
            return "&+L(&+L----------&+L)&n";
        }

        /// <summary>
        /// Adds text to the output buffer, to be sent on our next socket write.
        /// </summary>
        /// <param name="txt">Text to append, as an array of bytes.</param>
        public void WriteToBuffer( byte[] txt )
        {
            if (txt == null || txt.Length < 1)
            {
                return;
            }

            string str = String.Empty;
            int count;
            for( count = 0; count < txt.Length; count++ )
            {
                str += (char)txt[ count ];
            }
            WriteToBuffer( str );
        }

        /// <summary>
        /// Shows a Screen to the player.
        /// </summary>
        /// <param name="screen"></param>
        public void ShowScreen( Screen screen )
        {
            if (screen == null || String.IsNullOrEmpty(screen.Contents))
            {
                return;
            }
            WriteToBuffer( screen.Contents );
        }

        /// <summary>
        /// Adds text to the output buffer, to be sent on our next socket write.
        /// </summary>
        /// <param name="txt">Text to append, as a string.</param>
        public void WriteToBuffer( string txt )
        {
            if (String.IsNullOrEmpty(txt))
            {
                return;
            }

            // Find length.
            int length = txt.Length;

            if (!Character || (length > 77 && !Character.IsNPC() && _terminalType != TerminalType.TERMINAL_ENHANCED &&
                Character.HasActBit(PC.PLAYER_AUTOWRAP)))
            {
                txt = MUDString.InsertLineBreaks(txt, 78);
            }

            // Initial \r\n if needed.
            if( Outbuf.Length == 0 && !Fcommand )
            {
                Outbuf += "\r\n";
            }

            // Expand the buffer as needed.
            if( Outbuf.Length + length >= 32000 && !( Original ? Original : Character ).IsImmortal() )
            {
                /* empty buffer */
                Outbuf = String.Empty;
                string bugbuf = "Buffer overflow. Closing (" + ( Character ? Character._name : "???" ) + "{0}).";
                Log.Error( bugbuf, 0 );
                WriteToSocket("\r\n*** BUFFER OVERFLOW!!! ***\r\n", false);
                CloseSocket();
                return;
            }

            Outbuf += txt;
            return;
        }

        /// <summary>
        /// Allows the checking whether a socket is null using the not operator.
        /// Exists for C++ style compatibility.
        /// </summary>
        /// <param name="socket"></param>
        /// <returns></returns>
        public static implicit operator bool( SocketConnection socket )
        {
            if (socket == null)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Writes a string of bytes to a socket connection, compressing if necessary.
        /// </summary>
        /// <param name="desc"></param>
        /// <param name="txt"></param>
        /// <param name="length"></param>
        /// <param name="compress"></param>
        /// <returns></returns>
        private bool WriteToSocket(byte[] txt, bool compress)
        {
            if (txt.Length == 0)
                return true;

            if( compress )
            {
                MemoryStream ms = new MemoryStream();
                // Use the newly created memory stream for the compressed data.
                DeflateStream uncompressedzipStream = new DeflateStream( ms, CompressionMode.Compress, true );
                uncompressedzipStream.Write( txt, 0, txt.Length );
                // Close the stream.
                uncompressedzipStream.Close();
                txt = ms.GetBuffer();
            }

            int nWrite = _socket.Send( txt, txt.Length, 0 );

            if( nWrite < 0 )
            {
                throw new Exception( "Exception: Descriptor.WriteToSocket failed to send." );
            }
            return true;
        }

        /// <summary>
        /// Writes a string of bytes to a socket connection, compressing if necessary.
        /// </summary>
        /// <param name="txt"></param>
        /// <param name="compress"></param>
        /// <returns></returns>
        private bool WriteToSocket( string txt, bool compress )
        {
            return WriteToSocket( GetBytesFromString(txt), compress );
        }

        /// <summary>
        /// Utility _function to convert bytes into a string.
        /// </summary>
        /// <param name="text">An array of bytes.</param>
        /// <returns>A string containing the converted bytes.</returns>
        public static string GetStringFromBytes(byte[] text)
        {
            string str = String.Empty;
            foreach (byte b in text)
            {
                str += (char)b;
            }
            return str;
        }

        /// <summary>
        /// Utility _function to convert a string into bytes.
        /// </summary>
        /// <param name="txt"></param>
        /// <returns></returns>
        public static byte[] GetBytesFromString(string txt)
        {
            byte[] bytes = new byte[txt.Length];
            for(int count = 0; count < txt.Length; count++ )
            {
                bytes[count] = (byte)txt[count];
            }
            return bytes;
        }

        /// <summary>
        /// Called when the game is shutting down. Forces a save and closes socket on all players.
        /// </summary>
        public static void EndOfGame()
        {
            foreach( SocketConnection socket in Database.SocketList )
            {
                if (socket._connectionState == ConnectionState.playing)
                {
                    if (socket.Character._position == Position.fighting)
                    {
                        CommandType.Interpret(socket.Character, "save");
                    }
                    else
                    {
                        CommandType.Interpret(socket.Character, "quit");
                    }
                }
                else
                {
                    socket.CloseSocket();
                }
            }
            if (Database.CorpseList != null)
            {
                Database.CorpseList.Save();
            }
            else
            {
                Log.Trace("No corpse list to save, not saving.");
            }
            return;
        }

        /// <summary>
        /// Processes terminal control codes.
        /// </summary>
        /// <param name="argument"></param>
        /// <returns></returns>
        string ProcessTerminalChars(string argument)
        {
            if (argument.Contains(String.Format("{0}", (char)255)))
            {
                argument = argument.Replace("��", "");
                argument = argument.Replace("��", "");
                argument = argument.Replace("��", "");
            }
            return argument;
        }

        /// <summary>
        /// Deal with sockets that haven't logged in yet, and then some more!
        /// </summary>
        /// <param name="argument"></param>
        private void ConnectionStateManager( string argument )
        {
            argument = argument.Trim();
            argument = ProcessTerminalChars(argument);

            switch( _connectionState )
            {

                default:
                    Log.Error( "ConnectionStateManager: bad connection state {0}.", _connectionState );
                    CloseSocket();
                    return;
                case ConnectionState.choose_name:
                    ProcessNameEntry(argument);
                    break;
                case ConnectionState.get_existing_password:
                    ProcessPasswordEntry(argument);
                    break;
                case ConnectionState.confirm_new_name:
                    ConfirmCreateCharacter(argument);
                    break;
                case ConnectionState.choose_new_password:
                    ProcessSetPassword(argument);
                    break;
                case ConnectionState.confirm_new_password:
                    ProcessRetypePassword(argument);
                    break;
                case ConnectionState.choose_new_race:
                    ProcessRaceSelection(argument);
                    break;
                case ConnectionState.choose_new_sex:
                    ProcessSexSelection(argument);
                    break;
                case ConnectionState.choose_new_class:
                    ProcessClassSelection(argument);
                    break;
                case ConnectionState.confirm_new_class:
                    ProcessClassConfirmation(argument);
                    break;
                case ConnectionState.confirm_new_race:
                    ProcessRaceConfirmation(argument);
                    break;
                case ConnectionState.roll_stats:
                    ProcessKeepStats(argument);
                    break;
                case ConnectionState.apply_first_bonus:
                    ProcessFirstBonus(argument);
                    break;
                case ConnectionState.apply_second_bonus:
                    ProcessSecondBonus(argument);
                    break;
                case ConnectionState.apply_third_bonus:
                    ProcessThirdBonus(argument);
                    break;
                case ConnectionState.choose_new_hometown:
                    ProcessChooseHometown(argument);
                    break;
                case ConnectionState.choose_terminal_type:
                    ProcessTerminalType(argument);
                    break;
                case ConnectionState.menu:
                    ProcessMenuSelection(argument);
                    break;
                case ConnectionState.change_password_get_old:
                    ConfirmOldPassword(argument);
                    break;
                case ConnectionState.change_password_get_new:
                    ProcessPasswordChange(argument);
                    break;
                case ConnectionState.change_password_confirm_new:
                    ConfirmPasswordChange(argument);
                    break;
                case ConnectionState.retire_character_get_password:
                    ProcessRetireCharacter(argument);
                    break;
                case ConnectionState.retire_character_confirm:
                    ConfirmRetireCharacter(argument); 
                    break;
            }

            return;
        }

        /// <summary>
        /// Processes 'choose a password' reply.
        /// </summary>
        /// <param name="argument"></param>
        private void ProcessSetPassword(string argument)
        {
            if (argument.Length < 5)
            {
                WriteToBuffer("Password must be at least five characters long.\r\nPassword: ");
                return;
            }

            if (argument.Contains("~"))
            {
                WriteToBuffer("New password not acceptable, try again.\r\nPassword: ");
                return;
            }

            ((PC)Character).Password = argument;
            WriteToBuffer("\r\nPlease retype your password: ");
            _connectionState = ConnectionState.confirm_new_password;
        }

        /// <summary>
        /// Processes password entry for the character retirement process.
        /// </summary>
        /// <param name="argument"></param>
        private void ProcessRetireCharacter(string argument)
        {
            if (String.IsNullOrEmpty(argument))
            {
                WriteToBuffer("Retire aborted.\r\n");
                ShowScreen(Screen.MainMenuScreen);
                _connectionState = ConnectionState.menu;
                return;
            }

            if (MUDString.StringsNotEqual(argument, ((PC)Character).Password))
            {
                WriteToBuffer("Wrong password.\r\n");
                ShowScreen(Screen.MainMenuScreen);
                _connectionState = ConnectionState.menu;
                return;
            }

            WriteToBuffer("Are you sure (Y/N)? ");
            _connectionState = ConnectionState.retire_character_confirm;
        }

        /// <summary>
        /// Processes the reply to 'do you want to keep these stats' prompt.
        /// </summary>
        /// <param name="argument"></param>
        private void ProcessKeepStats(string argument)
        {
            if (String.IsNullOrEmpty(argument))
            {
                WriteToBuffer("Which stat do you want to put your first of three bonuses into? ");
                _connectionState = ConnectionState.apply_first_bonus;
                return;
            }

            switch (argument[0])
            {
                case 'y':
                case 'Y':
                    break;
                default:
                    RerollStats(Character);
                    SendAbilityScores(Character);
                    WriteToBuffer("Do you want to keep these stats? ");
                    _connectionState = ConnectionState.roll_stats;
                    return;
            }
            WriteToBuffer("Which stat do you want to put your first of three bonuses into? ");
            _connectionState = ConnectionState.apply_first_bonus;
        }

        /// <summary>
        /// Processes a character's first ability score bonus roll.
        /// </summary>
        /// <param name="argument"></param>
        private void ProcessFirstBonus(string argument)
        {
            if (!ApplyAbilityBonus(Character, argument))
            {
                WriteToBuffer("I didn't quite understand you.  Which stat do you want your first bonus in? ");
                _connectionState = ConnectionState.apply_first_bonus;
                return;
            }
            SendAbilityScores(Character);
            WriteToBuffer("Which stat do you want to put your second of three bonuses into? ");
            _connectionState = ConnectionState.apply_second_bonus;
        }

        /// <summary>
        /// Processes a character's second ability score bonus roll.
        /// </summary>
        /// <param name="argument"></param>
        private void ProcessSecondBonus(string argument)
        {
            if (!ApplyAbilityBonus(Character, argument))
            {
                WriteToBuffer("I didn't quite understand you.  Which stat do you want your second bonus in? ");
                _connectionState = ConnectionState.apply_second_bonus;
                return;
            }
            SendAbilityScores(Character);
            WriteToBuffer("Which stat do you want to put your third of three bonuses into? ");
            _connectionState = ConnectionState.apply_third_bonus;
        }

        /// <summary>
        /// Processes a character's third ability score bonus roll and then displays hometown choices.
        /// </summary>
        /// <param name="argument"></param>
        private void ProcessThirdBonus(string argument)
        {
            if (!ApplyAbilityBonus(Character, argument))
            {
                WriteToBuffer("I didn't quite understand you.  Which stat do you want your third bonus in? ");
                _connectionState = ConnectionState.apply_third_bonus;
                return;
            }
            SendAbilityScores(Character);

            Character.InitializeLanguages();
            List<RepopulationPoint> repoplist = Character.GetAvailableRepops();
            if (repoplist.Count < 1)
            {
                WriteToBuffer("\r\nYour race does not have a hometown.  Placing you at generic start point. <Press Enter>\r\n");
                ((PC)Character).CurrentHome = StaticRooms.GetRoomNumber("ROOM_NUMBER_START");
                ((PC)Character).OriginalHome = StaticRooms.GetRoomNumber("ROOM_NUMBER_START");
                ((PC)Character).LastRentLocation = StaticRooms.GetRoomNumber("ROOM_NUMBER_START");
                string logbuf = "Assigned new player a start room of " + StaticRooms.GetRoomNumber("ROOM_NUMBER_START") + " because there was no available home.";
                Log.Trace(logbuf);
                ShowScreen(Screen.MainMenuScreen);
                _connectionState = ConnectionState.menu;
            }
            else if (repoplist.Count == 1)
            {
                int place = repoplist[0].RoomIndexNumber;
                Room room = Room.GetRoom(place);
                if (room != null)
                {
                    WriteToBuffer("\r\nThe hometown for your race is ");
                    WriteToBuffer(room.Area.Name);
                    WriteToBuffer(". <Press Enter>\r\n");
                }
                ((PC)Character).CurrentHome = place;
                ((PC)Character).OriginalHome = place;
                ((PC)Character).LastRentLocation = place;
                string logbuf = "Assigned new player a start room of " + place + ".";
                Log.Trace(logbuf);
                ShowScreen(Screen.MainMenuScreen);
                _connectionState = ConnectionState.menu;
            }
            else
            {
                WriteToBuffer("\r\nPlease choose a hometown:\r\n");
                int count = 0;
                foreach (RepopulationPoint repop in repoplist)
                {
                    Room room = Room.GetRoom(repop.Room);
                    if (room != null)
                    {
                        string outbuf = count + ") " + room.Title + " in " + room.Area.Name + ".\r\n";
                        WriteToBuffer(outbuf);
                    }
                    ++count;
                }
            }
            _connectionState = ConnectionState.choose_new_hometown;
        }

        /// <summary>
        /// Processes entries selected from the login menu.
        /// </summary>
        /// <param name="argument"></param>
        private void ProcessMenuSelection(string argument)
        {
            if (String.IsNullOrEmpty(argument))
            {
                return;
            }
            if ((argument[0] == '0'))
            {
                Character.SendText("Goodbye.\r\n");
                ImmortalChat.SendImmortalChat(Character, ImmortalChat.IMMTALK_LOGINS, Character.GetTrust(), String.Format("{0} has left the game.", Character._name));
                CharData.SavePlayer(Character);
                CharData.ExtractChar(Character, true);

                CloseSocket();
                return;
            }
            if ((argument[0] == '1'))
            {
                Character.ResetStats();
                Log.Trace(String.Format("Character {0} ({1} {2}) entering the realm from the menu.",
                    Character._name,  SocketConnection.RemoveANSICodes(Character.RaceName()), 
                    SocketConnection.RemoveANSICodes(Character._charClass.Name)));
            }
            else if ((argument[0] == '2'))
            {
                Character._socket.WriteToBuffer("Enter Old Password: ");
                Character._socket._connectionState = ConnectionState.change_password_get_old;
                return;
            }
            else if ((argument[0] == '6'))
            {
                Character._socket.WriteToBuffer("Enter Password to Retire: ");
                Character._socket._connectionState = ConnectionState.retire_character_get_password;
                return;
            }
            else
            {
                Character.SendText("Invalid selection.\r\n");
                Character.SendText("\r\n");
                Character._socket.ShowScreen(Screen.MainMenuScreen);
                return;
            }

            // After this point we only have people who selected "enter the game".

            // Initialize newbie chars.
            if (Character._level == 0)
            {
                SetupNewCharacter();
            }

            // Get them in the game.
            PlaceCharacterInGame();
            Database.CharList.Add(Character);
            _connectionState = ConnectionState.playing;

            Character.SendText("\r\n&nWelcome to " + Database.SystemData.MudAnsiName + ".  May your visit here be... Eventful.\r\n");

            if (!Character.HasActBit(PC.PLAYER_WIZINVIS) && !Character.IsAffected(Affect.AFFECT_INVISIBLE))
            {
                Act("$n&n has entered the realm.", Character, null, null, MessageTarget.room);
            }

            ImmortalChat.SendImmortalChat(Character, ImmortalChat.IMMTALK_LOGINS, Character.GetTrust(),
                String.Format("{0} has entered the realm.", Character._name));
            // Check the character's position on the fraglist.  This ensures proper
            // updates.  No immortals allowed on the fraglist.
            if (!Character.IsImmortal())
            {
                if (((PC)Character).Frags > 0)
                {
                    FraglistData.SortFraglist(Character, FraglistData.Fraglist.TopFrags);
                    FraglistData.SortFraglist(Character, FraglistData.Fraglist.TopRaceFrags[Character.GetOrigRace()]);
                    FraglistData.SortFraglist(Character, FraglistData.Fraglist.TopClassFrags[(int)Character._charClass.ClassNumber]);
                    FraglistData.Fraglist.Save();
                }
                else if (((PC)Character).Frags < 0)
                {
                    FraglistData.SortFraglist(Character, FraglistData.Fraglist.BottomFrags);
                    FraglistData.SortFraglist(Character, FraglistData.Fraglist.BottomRaceFrags[Character.GetOrigRace()]);
                    FraglistData.SortFraglist(Character, FraglistData.Fraglist.BottomClassFrags[(int)Character._charClass.ClassNumber]);
                    FraglistData.Fraglist.Save();
                }
            }

            if (Character.HasActBit(PC.PLAYER_JUST_DIED))
            {
                Character.RemoveActBit(PC.PLAYER_JUST_DIED);
            }

            if (Character._hitpoints < 1)
            {
                Character._hitpoints = Character.GetMaxHit() / 3;
            }
            Character.ResetStats();
            CommandType.Interpret(Character, "look auto");
            // TODO: Check for new player-to-player notes and tell the player about them.
        }

        /// <summary>
        /// Processes password confirmation entry.
        /// </summary>
        /// <param name="argument"></param>
        private void ProcessRetypePassword(string argument)
        {
            if (String.IsNullOrEmpty(argument))
            {
                WriteToBuffer("\r\nBlank passwords are not allowed.\r\nRetype your password: ");
                _connectionState = ConnectionState.choose_new_password;
                return;
            }

            if (MUDString.StringsNotEqual(argument, ((PC)Character).Password))
            {
                WriteToBuffer("\r\nThe passwords don't match.\r\nRetype your password: ");
                _connectionState = ConnectionState.choose_new_password;
                return;
            }

            // Eliminated the extra Return
            if (HasColor() && Screen.RaceSelectionScreenColor != null)
            {
                ShowScreen(Screen.RaceSelectionScreenColor);
            }
            else if (Screen.RaceSelectionScreenMonochrome != null)
            {
                ShowScreen(Screen.RaceSelectionScreenMonochrome);
            }
            else
            {
                string races = "Please choose a race: ";
                foreach (Race race in Race.RaceList)
                {
                    if (race.HasInnate(Race.RACE_PC_AVAIL))
                    {
                        races += race.Name + " ";
                    }
                }
                WriteToBuffer(races);
            }
            _connectionState = ConnectionState.choose_new_race;
        }

        /// <summary>
        /// Processes 'do you want to create a new character' reply.
        /// </summary>
        /// <param name="argument"></param>
        private void ConfirmCreateCharacter(string argument)
        {
            switch (argument[0])
            {
                case 'y':
                case 'Y':
                    WriteToBuffer(String.Format("New character.\r\nGive me a password for {0}: ", Character._name));
                    _connectionState = ConnectionState.choose_new_password;
                    break;
                case 'n':
                case 'N':
                    WriteToBuffer("Ok, what IS it, then? ");
                    Character = null;
                    _connectionState = ConnectionState.choose_name;
                    break;
                default:
                    WriteToBuffer("Please type Yes or No? ");
                    break;
            }
        }

        /// <summary>
        /// Processes character retirement confirmation reply.
        /// </summary>
        /// <param name="argument"></param>
        /// <returns></returns>
        private void ConfirmRetireCharacter(string argument)
        {
            switch (argument[0])
            {
                case 'y':
                case 'Y':
                    break;
                default:
                    WriteToBuffer("Retire aborted.\r\n");
                    ShowScreen(Screen.MainMenuScreen);
                    _connectionState = ConnectionState.menu;
                    return;
            }

            WriteToBuffer("Hope you return soon brave adventurer!\r\n");

            Act("$n&n has retired from the realm.", Character, null, null, MessageTarget.room);
            Log.Trace(String.Format("{0} has retired from the realm.", Character._name));
            ImmortalChat.SendImmortalChat(Character, ImmortalChat.IMMTALK_LOGINS, Character.GetTrust(),
                String.Format("{0} has retired from the realm.", Character._name));

            CharData.SavePlayer(Character);
            CharData.DeletePlayer(Character);
            CharData.ExtractChar(Character, true);
            Character._socket._closeThisSocket = true;
        }

        /// <summary>
        /// Processes sencond entry for password change process.
        /// </summary>
        /// <param name="argument"></param>
        private void ConfirmPasswordChange(string argument)
        {
            if (MUDString.StringsNotEqual(argument, ((PC)Character).NewPassword))
            {
                WriteToBuffer("\r\nPasswords don't match.\r\nRetype password: ");
                _connectionState = ConnectionState.change_password_get_new;
                return;
            }

            ((PC)Character).NewPassword = String.Empty;
            ((PC)Character).Password = argument;
            Character._socket._connectionState = ConnectionState.menu;
            WriteToBuffer("Password successfully changed.\r\n");
            ShowScreen(Screen.MainMenuScreen);
        }

        /// <summary>
        /// Processes entry of old password during the password change process.
        /// </summary>
        private void ConfirmOldPassword(string argument)
        {
            if( String.IsNullOrEmpty(argument) )
            {
                WriteToBuffer( "Password change aborted.\r\n" );
                //WriteToBuffer( ECHO_ON_STRING );
                _connectionState = ConnectionState.menu;
                return;
            }

            if (MUDString.StringsNotEqual(argument, ((PC)Character).Password))
            {
                WriteToBuffer( "Wrong password.\r\n");
                ShowScreen(Screen.MainMenuScreen);
                _connectionState = ConnectionState.menu;
                return;
            }

            WriteToBuffer( "New password: " );
            _connectionState = ConnectionState.change_password_get_new;
        }

        /// <summary>
        /// Processes replies to the 'enter your _name' prompt.
        /// </summary>
        private void ProcessNameEntry(string argument)
        {
            if (String.IsNullOrEmpty(argument))
            {
                CloseSocket();
                return;
            }

            // Capitalize the name.
            argument = System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(argument);

            bool oldPlayer = CharData.LoadPlayer(this, argument.ToUpper());

            if (!oldPlayer && !CheckPlayerName(argument))
            {
                WriteToBuffer("That name can't be used here, please try another.\r\nName: ");
                return;
            }
            else if (!oldPlayer)
            {
                Character = new PC();
                Character._socket = this;
                Character._name = argument;
            }

            if (Character.HasActBit(PC.PLAYER_DENY))
            {
                string logbuf = String.Format("Denying access to {0}@{1}.", argument, _host);
                Log.Trace(logbuf);
                WriteToBuffer("You are denied access.\r\n");
                CloseSocket();
                return;
            }

            if (CheckForReconnect(argument, false))
            {
                oldPlayer = true;
            }
            else
            {
                /* Must be immortal with wizbit in order to get in */
                if (Database.SystemData.GameIsWizlocked && !Character.IsHero() && !Character.HasActBit(PC.PLAYER_WIZBIT))
                {
                    WriteToBuffer("The game is wizlocked.\r\n");
                    CloseSocket();
                    return;
                }
                if (Character._level == 0 && Macros.IsSet((int)Database.SystemData.ActFlags, (int)Sysdata.MudFlags.newlock))
                {
                    WriteToBuffer("\r\n\r\nThis is a private port.  If you want to play ");
                    WriteToBuffer(Database.SystemData.MudName + ", please contact the administrators for access.\r\n");
                    CloseSocket();
                    return;
                }
                if (Character._level < Database.SystemData.NumlockLevel && !Character.HasActBit(PC.PLAYER_WIZBIT))
                {
                    WriteToBuffer("The game is locked to your level character.\r\nTry contacting the admins for assitance.\r\n\r\n");
                    CloseSocket();
                    return;
                }
            }

            if (oldPlayer)
            {
                /* Old player */
                if (HasColor())
                {
                    WriteToBuffer("[0;31mPassword: [0m");
                }
                else
                {
                    WriteToBuffer("Password: ");
                }
                _connectionState = ConnectionState.get_existing_password;
            }
            else
            {
                /* New player */
                WriteToBuffer(String.Format("Do ye wish to be called {0} (Y/N)? ", argument));
                _connectionState = ConnectionState.confirm_new_name;
                return;
            }
        }

        /// <summary>
        /// Processes reply to 'select a sex' prompt.
        /// </summary>
        /// <param name="argument"></param>
        private void ProcessSexSelection(string argument)
        {
            switch (argument[0])
            {
                case 'm':
                case 'M':
                    Character._sex = MobTemplate.Sex.male;
                    break;
                case 'f':
                case 'F':
                    Character._sex = MobTemplate.Sex.female;
                    break;
                default:
                    WriteToBuffer("\r\nThat's not a sex.\r\nWhat IS your sex? ");
                    return;
            }

            // Eliminated the extra return during character creation.
            WriteToBuffer("Select a class [" + Race.RaceList[Character.GetOrigRace()].ClassesAvailable + "]:");
            _connectionState = ConnectionState.choose_new_class;
        }

        /// <summary>
        /// Processes terminal type selection reply.
        /// </summary>
        /// <param name="argument"></param>
        private void ProcessTerminalType(string argument)
        {
            if (String.IsNullOrEmpty(argument))
            {
                WriteToBuffer("Invalid terminal type. 0 for ASCII, 1 for ANSI. Enter your terminal type: ");
                return;
            }

            if (String.IsNullOrEmpty(argument))
            {
                argument = "1";
            }

            switch (argument[0])
            {
                case '1':
                    _terminalType = TerminalType.TERMINAL_ANSI;
                    break;
                case '0':
                    _terminalType = TerminalType.TERMINAL_ASCII;
                    break;
                case '5':
                    // Hidden option #5 automatically entered by the Basternae client.
                    _terminalType = TerminalType.TERMINAL_ENHANCED;
                    break;
                case '9':
                    _terminalType = TerminalType.TERMINAL_ANSI;
                    WriteToBuffer("Enter character name: ");
                    _connectionState = ConnectionState.choose_name;
                    return;
                default:
                    WriteToBuffer("Invalid terminal type.  0 for ASCII, 1 for ANSI.  Enter your terminal type: ");
                    return;
            }

            // Send the greeting.
            //
            // We check them for color because we don't have a ch to compare
            // against for the ansi/ascii greetings
            if (HasColor())
            {
                ShowScreen(Screen.IntroScreenColor);
            }
            else
            {
                ShowScreen(Screen.IntroScreenMonochrome);
            }
            _connectionState = ConnectionState.choose_name;
            return;
        }

        /// <summary>
        /// Processes a reply to 'choose your hometown' prompt.
        /// </summary>
        /// <param name="argument"></param>
        private void ProcessChooseHometown(string argument)
        {
            {
                List<RepopulationPoint> repoplist = Character.GetAvailableRepops();
                int size = repoplist.Count;
                if (size > 1)
                {
                    // This is the only case where they would have chosen a number.
                    int value;
                    Int32.TryParse(argument, out value);
                    if (value >= size)
                    {
                        WriteToBuffer("\r\nInvalid selection.  Try again.\r\n");
                        return;
                    }
                    RepopulationPoint iter = repoplist[value];
                    ((PC)Character).CurrentHome = (iter).RoomIndexNumber;
                    ((PC)Character).OriginalHome = (iter).RoomIndexNumber;
                    ((PC)Character).LastRentLocation = (iter).RoomIndexNumber;
                    string logbuf = "New player selected a start room of " + (iter) + ".";
                    Log.Trace(logbuf);
                    string outbuf = "You selected " + ((Room.GetRoom(iter.Room)).Area.Name) + " as your hometown.\r\n";
                    WriteToBuffer(outbuf);
                }
            }
            Log.Trace(String.Format("{0}@{1} new player.", Character._name, _host));
            ImmortalChat.SendImmortalChat(Character, ImmortalChat.IMMTALK_NEWBIE, 0, String.Format("{0}@{1} new player.", Character._name, _host));
            WriteToBuffer("\r\n");

            if (HasColor())
            {
                Character.SetActBit(PC.PLAYER_COLOR);
            }
            else
            {
                Character.RemoveActBit(PC.PLAYER_COLOR);
            }

            int lines = ((PC)Character).PageLength;
            ((PC)Character).PageLength = 22;
            WriteToBuffer("\r\n");
            ((PC)Character).PageLength = lines;
            ShowScreen(Screen.MainMenuScreen);
            _connectionState = ConnectionState.menu;
        }

        /// <summary>
        /// Processes a reply to the 'choose a class' prompt.
        /// </summary>
        /// <param name="argument"></param>
        private void ProcessClassSelection(string argument)
        {
            int classnum = CheckClassSelection(argument);
            if (classnum == 0)
            {
                WriteToBuffer("\r\nThat's not a valid class name.\r\nWhat IS your class? ");
                return;
            }
            Character._charClass = CharClass.ClassList[classnum];

            WriteToBuffer("\r\nAre you sure you want to be a(n) " + Character._charClass.Name + " (Y/N)? ");
            _connectionState = ConnectionState.confirm_new_class;
        }

        /// <summary>
        /// Processes a reply to the 'are you sure you want this class' prompt.
        /// </summary>
        /// <param name="argument"></param>
        private void ProcessClassConfirmation(string argument)
        {
            if (String.IsNullOrEmpty(argument))
            {
                // Bad input, try again.
                WriteToBuffer("Do you want to keep these stats? ");
                _connectionState = ConnectionState.roll_stats;
                return;
            }

            switch (argument[0])
            {
                case 'y':
                case 'Y':
                    break;
                default:
                    // Eliminated the extra return - Xangis
                    WriteToBuffer("\r\nPress Return to continue:\r\n");
                    WriteToBuffer("Select a class [" + Race.RaceList[Character.GetOrigRace()].ClassesAvailable + "]: ");
                    _connectionState = ConnectionState.choose_new_class;
                    return;
            }
            RerollStats(Character);
            SendAbilityScores(Character);
            WriteToBuffer("Do you want to keep these stats? ");
            _connectionState = ConnectionState.roll_stats;
        }

        /// <summary>
        /// Processes a reply to 'choose your race' prompt.
        /// </summary>
        /// <param name="argument"></param>
        private void ProcessRaceSelection(string argument)
        {
            // An empty string matches everything, so short-circuit that here.
            if (String.IsNullOrEmpty(argument))
            {
                WriteToBuffer( "That is not a race.\r\nWhat IS your race? " );
                return;
            }

            int iRace;
            for (iRace = 0; iRace < Race.RaceList.Length; iRace++)
            {
                if (Race.RaceList[iRace].Name.StartsWith(argument, StringComparison.CurrentCultureIgnoreCase))
                {
                    Character.SetPermRace(Race.RaceLookup(Race.RaceList[iRace].Name));
                    break;
                }
            }
            if (iRace == Race.RaceList.Length)
            {
                WriteToBuffer( "That is not a race.\r\nWhat IS your race? " );
                return;
            }

            WriteToBuffer( "\r\n" );
            Character.RemoveActBit(PC.PLAYER_PAGER);
            CommandType.Interpret(Character, "help " + Race.RaceList[Character.GetOrigRace()].Name);
            WriteToBuffer( "Are you sure you want to be a(n) " + Race.RaceList[Character.GetOrigRace()].Name + " (Y/N)? ");
            _connectionState = ConnectionState.confirm_new_race;
        }

        /// <summary>
        /// Processes a reply to 'are you sure you want this race' prompt.
        /// </summary>
        /// <param name="argument"></param>
        private void ProcessRaceConfirmation(string argument)
        {
            if (argument[0] == 'y' || argument[0] == 'Y')
            {
                WriteToBuffer("\r\nWhat is your sex (M/F)? ");
                _connectionState = ConnectionState.choose_new_sex;
            }
            else
            {
                if (HasColor())
                {
                    ShowScreen(Screen.RaceSelectionScreenColor);
                }
                else
                {
                    ShowScreen(Screen.RaceSelectionScreenMonochrome);
                }
                _connectionState = ConnectionState.choose_new_race;
            }
        }

        /// <summary>
        /// Processes password entry reply.
        /// </summary>
        /// <param name="argument"></param>
        private void ProcessPasswordEntry(string argument)
        {
            if (String.IsNullOrEmpty(argument) || MUDString.StringsNotEqual(argument, ((PC)Character).Password))
            {
                WriteToBuffer("Incorrect password.\r\n");
                Log.Info ("Expected password '" + ((PC)Character).Password + "', got '" + argument + "'");
                CloseSocket();
                return;
            }

            if (CheckForReconnect(Character._name, true))
            {
                return;
            }

            if (CheckForReconnect(Character._name))
            {
                Log.Trace(String.Format("A connection has just been overridden for {0}.", Character._name));
                return;
            }

            Log.Trace(String.Format("{0}@{1} has connected.", Character._name, _host));

            WriteToBuffer("\r\n");

            if (HasColor())
            {
                Character.SetActBit(PC.PLAYER_COLOR);
            }
            else
            {
                Character.RemoveActBit(PC.PLAYER_COLOR);
            }

            int lines = ((PC)Character).PageLength;
            ((PC)Character).PageLength = 20;

            WriteToBuffer("\r\n");
            ((PC)Character).PageLength = lines;
            Log.Trace("Showing login menu");
            ShowScreen(Screen.MainMenuScreen);
            _connectionState = ConnectionState.menu;
        }

        /// <summary>
        /// Processes new password entry during the password change process.
        /// </summary>
        /// <param name="argument"></param>
        private void ProcessPasswordChange(string argument)
        {
            if (String.IsNullOrEmpty(argument))
            {
                WriteToBuffer("Password change aborted.\r\n");
                _connectionState = ConnectionState.menu;
                return;
            }

            if (argument.Length < 5)
            {
                WriteToBuffer("\r\nPassword must be at least five characters long.\r\nNew password: ");
                return;
            }

            // This may not matter, but since player files are stored as XML, we take precautions.
            if (argument.Contains("<") || argument.Contains(">") || argument.Contains("&"))
            {
                WriteToBuffer("\r\nNew password not acceptable, try again.\r\nNew password: ");
                return;
            }

            ((PC)Character).NewPassword = argument;
            WriteToBuffer("\r\nPlease retype password: ");
            _connectionState = ConnectionState.change_password_confirm_new;
        }

        /// <summary>
        /// Parse a name for acceptability.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static bool CheckPlayerName( string name )
        {
            // Null or empty names.
            if (String.IsNullOrEmpty(name))
            {
                return false;
            }

            // Reserved keywords.
            if( MUDString.NameContainedIn( name, "all auto imm immortal self someone none . .. ./ ../ /" ) )
                return false;

            // Banned names, including obscenities and previously-banned names.
            if( MUDString.NameContainedIn( name, Database.SystemData.BannedNames ) )
                return false;

            // Name has to be at least 3 characters.
            if( name.Length < 3 )
                return false;

            // Block naming yourself after a mob in the game.
            foreach( Area area in Database.AreaList )
            {
                foreach( MobTemplate mobTemplate in area.Mobs )
                {
                    if (MUDString.NameContainedIn(name, mobTemplate.PlayerName))
                    {
                        return false;
                    }
                }
            }

            // Deny the idiots that want to name storage characters stuff like "bobstore".
            if( MUDString.NameContainedIn( "stor", name ) )
                return false;

            // Obscenities within name strings are a big nono.
            if( MUDString.NameContainedIn( "rape", name ) )
                return false;
            if( MUDString.NameContainedIn( "shit", name ) )
                return false;
            if( MUDString.NameContainedIn( "piss", name ) )
                return false;
            if( MUDString.NameContainedIn( "bitch", name ) )
                return false;
            if( MUDString.NameContainedIn( "damn", name ) )
                return false;
            if( MUDString.NameContainedIn( "cock", name ) )
                return false;
            if( MUDString.NameContainedIn( "cunt", name ) )
                return false;
            if( MUDString.NameContainedIn( "pussy", name ) )
                return false;
            if( MUDString.NameContainedIn( "tits", name ) )
                return false;
            if( MUDString.NameContainedIn( "penis", name ) )
                return false;
            if( MUDString.NameContainedIn( "fuck", name ) )
                return false;
            if( MUDString.NameContainedIn( "hell", name ) )
                return false;
            if (MUDString.NameContainedIn("jesus", name))
                return false;
            if (MUDString.NameContainedIn("christ", name))
                return false;

            return true;
        }

        /// <summary>
        /// Checks whether a player is reconnecting to an already-logged-on character.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="conn"></param>
        /// <returns></returns>
        private bool CheckForReconnect( string name, bool conn )
        {
            CharData ch;

            try
            {
                for (int j = Database.CharList.Count - 1; j >= 0; j--)
                {
                    ch = Database.CharList[j];

                    if (!ch.IsNPC() && (!conn || !ch._socket)
                            && !MUDString.StringsNotEqual(Character._name, ch._name))
                    {
                        if (conn == false)
                        {
                            ((PC)Character).Password = ((PC)ch).Password;
                        }
                        else
                        {
                            Character = ch;
                            ch._socket = this;
                            ch._timer = 0;
                            _connectionState = ConnectionState.playing;
                            ch.SendText("Reconnecting.\r\n");
                            Act("$n&+L has returned from the &+RA&n&+rb&+Rys&n&+rs&+L.&n", ch, null, null, MessageTarget.room);
                            string logBuf = String.Format("{0}@{1} reconnected.", ch._name, _host);
                            Log.Trace(logBuf);
                            /* Strip void events from char to prevent crashes. */
                            Event eventdata;
                            for (int i = (Database.EventList.Count - 1); i >= 0; i--)
                            {
                                eventdata = Database.EventList[i];
                                if (eventdata.Type == Event.EventType.extract_character)
                                {
                                    if ((CharData)eventdata.Target1 == ch)
                                    {
                                        Log.Trace("Killing void event.");
                                        Database.EventList.RemoveAt(i);
                                    }
                                }
                            }
                        }
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                // Maybe the char list or event list is unstable.  In that case we assume it's *not* a reconnect.
                Log.Error("Exception in CheckForReconnect(string, bool): " + ex); 
                return false;
            }

            return false;
        }

        /// <summary>
        /// Checks whether a player is reconnecting to an already-logged-on character.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        private bool CheckForReconnect( string name )
        {
            SocketConnection oldSocket;
            CharData ch;
            CharData newCh;

			for( int i = Database.SocketList.Count - 1; i >= 0; i-- )
            {
                oldSocket = Database.SocketList[i];
                Room room = null;
                if(   /* There's a different desc. */
                    oldSocket != this
                    /* That desc. has a char. */
                    && oldSocket.Character
                    /* The desc. is not in the process of getting a _name */
                    && oldSocket._connectionState != ConnectionState.choose_name
                    /* The desc.'s _name matches the char we're loading. */
                    && !MUDString.StringsNotEqual( name, oldSocket.Original
                                ? oldSocket.Original._name : oldSocket.Character._name ) )
                {
                    /* Tell the new char that we're loading up the old player. */
                    WriteToBuffer( "Navigating the Abyss...\r\n" );
                    /* Handle switched chars. */
                    ch = ( oldSocket.Original ? oldSocket.Original : oldSocket.Character );
                    /* Verify that the old character has a room. */
                    if( !ch._inRoom )
                    {
                        Log.Trace( "Character has no ch.in_room in CheckForReconnect" );
                        ch._inRoom = Room.GetRoom( StaticRooms.GetRoomNumber("ROOM_NUMBER_LIMBO") );
                    }

                    string logBuf = String.Format( "Overriding connection for {0}.", ch._name );
                    ImmortalChat.SendImmortalChat( ch, ImmortalChat.IMMTALK_LOGINS, ch.GetTrust(), logBuf );

                    /* Swap characters and destroy dold. */
                    /* Preserve d.character. */
                    newCh = Character;
                    /* Set d's characters to dold's. */
                    Original = oldSocket.Original;
                    Character = oldSocket.Character;
                    ch._socket = this;
                    /* Set dold's characters. (loading chars can't be switched. ) */
                    oldSocket.Original = null;
                    oldSocket.Character = newCh;
                    newCh._socket = oldSocket;
                    /* Set the connection status. */
                    if( oldSocket._connectionState == ConnectionState.playing )
                    {
                        _connectionState = ConnectionState.playing;
                    }
                    else
                    {
                        _connectionState = ConnectionState.menu;
                        ShowScreen(Screen.MainMenuScreen);
                    }
                    /* Get rid of dold. */
                    Log.Trace( "Closing old socket" );
                    if( _connectionState == ConnectionState.playing )
                    {
                        room = ch._inRoom;
                        ch.RemoveFromRoom();
                    }
                    oldSocket.CloseSocket();
                    if( _connectionState == ConnectionState.playing )
                    {
                        ch.AddToRoom( room );
                        Act( "$n&+L has returned from the &+RA&n&+rb&+Rys&n&+rs&+L.&n", ch, null, null, MessageTarget.room );
                    }
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Used with AFK folks -- resets a player's idle timer and displays a string
        /// saying that they are back at keys.
        /// </summary>
        /// <param name="ch"></param>
        private static void StopIdling( CharData ch )
        {
            if (!ch || !ch._socket || ch._socket._connectionState != ConnectionState.playing
                || !ch._wasInRoom || ch._inRoom != Room.GetRoom(StaticRooms.GetRoomNumber("ROOM_NUMBER_LIMBO")))
            {
                return;
            }

            ch._timer = 0;
            ch.RemoveFromRoom();
            ch.AddToRoom( ch._wasInRoom );
            
            ch._wasInRoom = null;
            Act( "$n&+L has returned from the &+RA&n&+rb&+Rys&n&+rs&+L.&n", ch, null, null, MessageTarget.room );
            return;
        }

        /// <summary>
        /// Sends a string of text to everyone in the specified room.
        /// </summary>
        /// <param name="txt"></param>
        /// <param name="room"></param>
        private void SendToRoom( string txt, Room room )
        {
            foreach( SocketConnection socket in Database.SocketList )
            {
                if (socket.Character != null)
                {
                    if (socket.Character._inRoom == room)
                    {
                        Act(txt, socket.Character, null, null, MessageTarget.character);
                    }
                }
            }
        }

        /// <summary>
        /// Send a string of text to all currently playing characters.  Those logged in but not
        /// playing (at menu, etc.) will not receive it.
        /// </summary>
        /// <param name="text"></param>
        public static void SendToAllChar( string text )
        {
            if( text.Length == 0 )
            {
                return;
            }
            foreach( SocketConnection socket in Database.SocketList )
            {
                if (socket.IsPlaying())
                {
                    socket.Character.SendText(text);
                }
            }

            return;
        }

        /// <summary>
        /// Send black and white text to a single character.
        /// </summary>
        /// <param name="txt"></param>
        /// <param name="ch"></param>
        static public void SendToCharBW( string txt, CharData ch )
        {
            if (ch == null)
            {
                return;
            }
            if( txt == null || ch._socket == null )
            {
                return;
            }

            // Bypass the paging procedure if the text output is small
            if( txt.Length < 600 || !ch.HasActBit(PC.PLAYER_PAGER ) )
            {
                ch._socket.WriteToBuffer( txt );
            }
            else
            {
                ch._socket._showstringHead = txt;
                ch._socket.ShowstringPoint = 0;
                ch._socket.ShowPagedString( String.Empty );
            }
            return;
        }

        /// <summary>
        /// Parses &+ based color codes and returns the string representing the ANSI control sequence
        /// used to produce that color.
        /// </summary>
        /// <param name="type">String to generate color codes from.</param>
        /// <returns>String containing ANSI color codes, or null if type is not valid.</returns>
        public static string GetColorCode( string type )
        {
            if (String.IsNullOrEmpty(type))
            {
                return String.Empty;
            }

            string code = String.Empty;

            if (type[0] == 'n' || type[0] == 'N')
            {
                code += "[0m";
            }
            else if (type[0] == '+' && type.Length >= 2)
            {
                switch (type[1])
                {
                    default:
                        break;
                    case 'l':
                        code += Color.FG_B_BLACK;
                        break;
                    case 'b':
                        code += Color.FG_BLUE;
                        break;
                    case 'r':
                        code += Color.FG_RED;
                        break;
                    case 'y':
                        code += Color.FG_YELLOW;
                        break;
                    case 'm':
                        code += Color.FG_MAGENTA;
                        break;
                    case 'c':
                        code += Color.FG_CYAN;
                        break;
                    case 'w':
                        code += Color.FG_WHITE;
                        break;
                    case 'g':
                        code += Color.FG_GREEN;
                        break;
                    case 'L':
                        code += Color.FG_B_BLACK;
                        break;
                    case 'B':
                        code += Color.FG_B_BLUE;
                        break;
                    case 'R':
                        code += Color.FG_B_RED;
                        break;
                    case 'Y':
                        code += Color.FG_B_YELLOW;
                        break;
                    case 'M':
                        code += Color.FG_B_MAGENTA;
                        break;
                    case 'C':
                        code += Color.FG_B_CYAN;
                        break;
                    case 'W':
                        code += Color.FG_B_WHITE;
                        break;
                    case 'G':
                        code += Color.FG_B_GREEN;
                        break;
                }
            }
            else if (type[0] == '-' && type.Length >= 2)
            {
                switch (type[1])
                {
                    default:
                        break;
                    case 'l':
                    case 'L':
                        code += Color.BG_BLACK;
                        break;
                    case 'g':
                    case 'G':
                        code += Color.BG_GREEN;
                        break;
                    case 'm':
                    case 'M':
                        code += Color.BG_MAGENTA;
                        break;
                    case 'r':
                    case 'R':
                        code += Color.BG_RED;
                        break;
                    case 'w':
                    case 'W':
                        code += Color.BG_WHITE;
                        break;
                    case 'y':
                    case 'Y':
                        code += Color.BG_YELLOW;
                        break;
                    case 'b':
                    case 'B':
                        code += Color.BG_BLUE;
                        break;
                    case 'c':
                    case 'C':
                        code += Color.BG_CYAN;
                        break;
                }
            }

            return code;
        }

        /// <summary>
        /// Uses &+ color codes - Xangis
        /// </summary>
        /// <param name="outputBuffer">This should be an empty string.</param>
        /// <param name="txt">Text to convert.</param>
        /// <param name="ch"></param>
        static void ColorConvert( out string outputBuffer, string txt, CharData ch )
        {
            outputBuffer = String.Empty;
            int position;

            if (txt == null)
            {
                return;
            }

            if( ch._socket && ch.HasActBit(PC.PLAYER_COLOR ) )
            {
                for( position = 0; position < txt.Length; position++ )
                {
                    if( txt[position] == '&' )
                    {
                        position++;
                        // only colorize shit that needs to be colorized... otherwise
                        // don't worry about it
                        if (txt[position] != 'N' && txt[position] != 'n' && txt[position] != '+' && txt[position] != '-')
                        {
                            position--;
                        }
                        else
                        {
                            // Pass the next wo characers to the color _function.
                            String temp = txt.Substring( position, 2 );
                            outputBuffer += GetColorCode(temp);
                            if (txt[position] != 'n' && txt[position] != 'N')
                            {
                                position++;
                            }
                            continue;
                        }
                    }
                    outputBuffer += txt[position];
                }
            }
            else
            {
                // Convert to black and white by stripping out all color control codes.
                for( position = 0; position < txt.Length; position++ )
                {
                    if (txt[position] == '&')
                    {
                        position++;
                        if (txt[position] != 'n' && txt[position] != 'N')
                        {
                            position++;
                        }
                        continue;
                    }
                    outputBuffer += txt[position];
                }
            }
            return;
        }

        /// <summary>
        /// Sends a multi-paged batch of text to a character.
        /// </summary>
        /// <param name="input"></param>
        public void ShowPagedString( string input )
        {
            int line = 0;
            int toggle = 0;
            int pagelines ;

            if (String.IsNullOrEmpty(input))
            {
                return;
            }

            switch (Char.ToUpper(input[0]))
            {
                case '\0':
                case 'C': /* show next page of text */
                    break;
                case 'R': /* refresh current page of text */
                    toggle = 1;
                    break;
                case 'B': /* scroll back a page of text */
                    toggle = 2;
                    break;
                default: /*otherwise, stop the text viewing */
                    _showstringHead = String.Empty;
                    ShowstringPoint = 0;
                    return;
            }

            if( Original )
            {
                pagelines = ( (PC)Original ).PageLength;
            }
            else
            {
                pagelines = ( (PC)Character ).PageLength;
            }

            // Toggle tells us whether we're going forward or backward.
            if( toggle != 0 )
            {
                if( ShowstringPoint == 0 )
                {
                    return;
                }
                // We use this to refresh the current page.
                if( toggle == 1 )
                {
                    line = -1;
                }
                do
                {
                    if( _showstringHead[ ShowstringPoint ] == '\n' )
                    {
                        if( ( line++ ) == ( pagelines * toggle ) )
                            break;
                    }
                    --ShowstringPoint;
                }
                while( ShowstringPoint != 0 );
            }

            // Buffer the text we want to print.
            line = 0;
            string buffer = String.Empty;
            if( ShowstringPoint < _showstringHead.Length )
            {
                do
                {
                    buffer += _showstringHead[ ShowstringPoint ];
                    if( _showstringHead[ ShowstringPoint ] == '\n' )
                    {
                        if( ( line++ ) == pagelines )
                        {
                            break;
                        }
                    }
                    ++ShowstringPoint;
                }
                while( ShowstringPoint < _showstringHead.Length );
            }

            // Write it.
            WriteToBuffer( buffer );
            // Clear it.
            _showstringHead = String.Empty;
            ShowstringPoint = 0;

            return;
        }

        /// <summary>
        /// Takes a string and removes the '&+' ANSI codes, returning a cleaned string.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static String RemoveANSICodes(String input)
        {
            input = input.Replace("&+L", "");
            input = input.Replace("&+R", "");
            input = input.Replace("&+Y", "");
            input = input.Replace("&+G", "");
            input = input.Replace("&+M", "");
            input = input.Replace("&+L", "");
            input = input.Replace("&+B", "");
            input = input.Replace("&+W", "");
            input = input.Replace("&n", "");
            input = input.Replace("&N", "");
            input = input.Replace("&+l", "");
            input = input.Replace("&+r", "");
            input = input.Replace("&+y", "");
            input = input.Replace("&+g", "");
            input = input.Replace("&+m", "");
            input = input.Replace("&+l", "");
            input = input.Replace("&+b", "");
            input = input.Replace("&+w", "");
            return input;
        }

        /// <summary>
        /// Forwards to Act with default argument of false. We don't use a default argument version that
        /// takes 5 or 6 parameters because the spell compiler doesn't like default arguments.
        /// </summary>
        /// <param name="format"></param>
        /// <param name="ch"></param>
        /// <param name="arg1"></param>
        /// <param name="arg2"></param>
        /// <param name="type"></param>
        public static void Act(string format, CharData ch, Target arg1, Target arg2, MessageTarget type)
        {
            Act(format, ch, arg1, arg2, type, false);
        }

        /// <summary>
        /// The primary output interface for formatted output.
        /// </summary>
        /// <param name="format">Format string for output.</param>
        /// <param name="actor">The actor (or TargetType, depending on format string).</param>
        /// <param name="arg1">Argument Target, use varies based on format string.</param>
        /// <param name="arg2">Argument Target, use varies based on format string</param>
        /// <param name="type">Target of Act statement.</param>
        /// <param name="capitalize">If capitalize is set, the first word of the phrase will be capitalized (after variable substitution).</param>
        public static void Act( string format, CharData actor, Target arg1, Target arg2, MessageTarget type, bool capitalize )
        {
            Object obj1 = (Object)arg1;
            Object obj2 = (Object)arg2;
            CharData victimChar = (CharData)arg2;
            string[] himHer = new[] { "it", "him", "her" };
            string[] hisHer = new[] { "its", "his", "her" };
            string str;
            // Discard null and zero-length messages.
            if( format.Length == 0 )
            {
                return;
            }

            if( actor == null )
            {
                Log.Error( "Act: null actor!", 0 );
                return;
            }

            // To prevent crashes
            if( !actor._inRoom )
            {
                Log.Error( "Act: Actor CharData (" + actor._name + ") is not in a room!" );
                return;
            }

            List<CharData> to = actor._inRoom.People;
            if( type == MessageTarget.victim || type == MessageTarget.room_vict )
            {
                if( !victimChar )
                {
                    Log.Error( "Act: null victim with Descriptor.MessageTarget.victim.", 0 );
                    Log.Error(String.Format("Bad act string: {0}", format));
                    return;
                }
                to = victimChar._inRoom.People;
            }

            string outputBuffer;

            foreach (CharData roomChar in to)
            {
                if( !roomChar._socket && roomChar.IsNPC() || !roomChar.IsAwake() )
                    continue;
                if( type == MessageTarget.room_vict && victimChar._flyLevel != roomChar._flyLevel )
                    continue;
                if( ( type == MessageTarget.room_vict || type == MessageTarget.room || type == MessageTarget.everyone_but_victim ) && actor._flyLevel != roomChar._flyLevel )
                    continue;
                if( type == MessageTarget.room_vict && ( roomChar == actor || roomChar == victimChar ) )
                    continue;
                if( type == MessageTarget.character && roomChar != actor )
                    continue;
                if( type == MessageTarget.victim && ( roomChar != victimChar || roomChar == actor ) )
                    continue;
                if( type == MessageTarget.room && roomChar == actor )
                    continue;
                if( type == MessageTarget.everyone_but_victim && ( roomChar == victimChar ) )
                    continue;
                if( type == MessageTarget.room_above && ( roomChar._flyLevel <= actor._flyLevel ) )
                    continue;
                if( type == MessageTarget.room_below && ( roomChar._flyLevel >= actor._flyLevel ) )
                    continue;

                str = format;

                if (str.Contains("$t"))
                    str = str.Replace("$t", (String)arg1);
                if (str.Contains("$T"))
                    str = str.Replace("$T", (String)arg2);
                if (str.Contains("$n") && actor != null)
                    str = str.Replace("$n", actor.ShowNameTo(roomChar, false));
                if (str.Contains("$N") && victimChar != null)
                    str = str.Replace("$N", victimChar.ShowNameTo(roomChar, false));
                if (str.Contains("$e") && actor != null)
                    str = str.Replace("$e", actor.GetSexPronoun());
                if (str.Contains("$E") && victimChar != null)
                    str = str.Replace("$E", victimChar.GetSexPronoun());
                if (str.Contains("$m") && actor != null)
                    str = str.Replace("$m", himHer[Macros.Range(0, (int)actor._sex, 2)]);
                if (str.Contains("$M") && victimChar != null)
                    str = str.Replace("$M", himHer[Macros.Range(0, (int)victimChar._sex, 2)]);
                if (str.Contains("$s") && actor != null)
                    str = str.Replace("$s", hisHer[Macros.Range(0, (int)actor._sex, 2)]);
                if (str.Contains("$S") && victimChar != null)
                    str = str.Replace("$S", hisHer[Macros.Range(0, (int)victimChar._sex, 2)]);
                if (str.Contains("$p") && obj1 != null)
                    str = str.Replace("$p", CharData.CanSeeObj(roomChar, obj1) ? obj1.ShortDescription : "something");
                if (str.Contains("$P") && obj1 != null)
                    str = str.Replace("$P", CharData.CanSeeObj(roomChar, obj2) ? obj2.ShortDescription : "something");
                if (str.Contains("$d"))
                {
                    if (((string)arg2).Length == 0)
                    {
                        str = str.Replace("$d", "door");
                    }
                    else
                    {
                        str = str.Replace("$d", (string)arg2);
                    }
                }

                str += "\r\n";

                ColorConvert( out outputBuffer, str, roomChar );

                if (capitalize)
                {
                    outputBuffer = MUDString.CapitalizeANSIString(outputBuffer);
                }

                if (roomChar._socket)
                {
                    roomChar._socket.WriteToBuffer(outputBuffer);
                }
            }
            return;
        }

        /// <summary>
        /// Sends ability scores when rolling stats or applying bonuses.  Used to avoid repetitive code.
        /// </summary>
        /// <param name="ch"></param>
        private void SendAbilityScores(CharData ch)
        {
            string buf = String.Format( "\r\n\r\nStr: {0}    Int: {1}\r\nDex: {2}    Wis: {3}\r\nAgi: {4}    Cha: {5}\r\nCon: {6}\r\nPow: {7}\r\n\r\n",
                    MUDString.PadStr( StringConversion.AbilityScoreString( ch.GetCurrStr() ), 17 ),
                    MUDString.PadStr( StringConversion.AbilityScoreString( ch.GetCurrInt() ), 17 ),
                    MUDString.PadStr( StringConversion.AbilityScoreString( ch.GetCurrDex() ), 17 ),
                    MUDString.PadStr( StringConversion.AbilityScoreString( ch.GetCurrWis() ), 17 ),
                    MUDString.PadStr( StringConversion.AbilityScoreString( ch.GetCurrAgi() ), 17 ),
                    MUDString.PadStr( StringConversion.AbilityScoreString( ch.GetCurrCha() ), 17 ),
                    MUDString.PadStr( StringConversion.AbilityScoreString( ch.GetCurrCon() ), 17 ),
                    MUDString.PadStr( StringConversion.AbilityScoreString( ch.GetCurrPow() ), 17 ) );

            WriteToBuffer( buf );
        }

        /// <summary>
        /// Splits semicolon-separated strings into separate commands.
        /// </summary>
        private void MultipleCommands()
        {
            _commandQueue = _commandQueuePointer;

            int index = _commandQueue.IndexOf( ';' );

            if( index == -1 )
            {
                _incomm = _commandQueue;
            }
            else
            {
                _incomm = _commandQueue.Substring(0,index);
                _commandQueue = _commandQueue.Substring(index + 1);
                Log.Trace("Dequeueing command '" + _incomm + "', queue still contains '" + _commandQueue + "'.");
            }
            return;
        }

        /// <summary>
        /// Mangles text based on languages used by speaker and listener.
        /// </summary>
        /// <param name="sstring"></param>
        /// <param name="ch"></param>
        /// <param name="victim"></param>
        /// <returns></returns>
        public static string TranslateText( string sstring, CharData ch, CharData victim )
        {
            if (String.IsNullOrEmpty(sstring) || ch == null || victim == null)
            {
                return String.Empty;
            }
            const string alphabet = "ecdfighjaklmnpyqrstvowxzub ";
            const string numbers = "1234567890";
            int i;
            Race.Language lang = ch.IsNPC() ? Race.RaceList[ ch.GetOrigRace() ].PrimaryLanguage : ( (PC)ch ).Speaking;

            // Immortals have perfect speech & NPCs have perfect ears & nobody misunderstands themselves.
            if (victim.IsImmortal() || ch.IsImmortal() || victim.IsNPC() || (ch == victim) || ch.IsNPC())
                return sstring;
            
            /* Step 1: Copy string. */
            char[] buffer = sstring.ToCharArray();

            /* Step 3: Check to see if ch says everything right.
            * NPC's never say anything wrong.
            */
            if( !ch.IsNPC() )
            {
                for( i = 0; i < buffer.Length; i++ )
                {
                    if( MUDMath.NumberPercent() > ( (PC)ch ).LanguageAptitude[ (int)lang ]
                            && MUDMath.NumberPercent() < 50 )
                    {
                        if( Char.IsLetter( buffer[ i ] ) )
                        {
                            if( Char.IsLower( buffer[ i ] ) )
                                buffer[ i ] = alphabet[ buffer[ i ] - 'a' ];
                            else
                                buffer[ i ] = Char.ToUpper( alphabet[ buffer[ i ] - 'A' ] );
                        }
                        else if( Char.IsDigit( buffer[ i ] ) )
                            buffer[ i ] = numbers[ buffer[ i ] - '0' ];
                        else
                            buffer[ i ] = buffer[ i ];
                    }
                }
            }

            // Step 4: Check for comprehend languages.  If so, victim understands perfectly.
            if (victim.IsAffected(Affect.AFFECT_COMP_LANG) || (lang == Race.Language.unknown))
            {
                return new String(buffer);
            }

            victim.PracticeLanguage( lang );

            // Step 5: Check to see if victim hears everything right.
            for( i = 0; i < buffer.Length; i++ )
            {
                if( MUDMath.NumberPercent() > ( (PC)victim ).LanguageAptitude[ (int)lang ]
                        && MUDMath.NumberPercent() < 50 )
                {
                    if( Char.IsLetter( buffer[ i ] ) )
                    {
                        if (Char.IsLower(buffer[i]))
                        {
                            buffer[i] = alphabet[buffer[i] - 'a'];
                        }
                        else
                        {
                            buffer[i] = Char.ToUpper(alphabet[buffer[i] - 'A']);
                        }
                    }
                    else if (Char.IsDigit(buffer[i]))
                    {
                        buffer[i] = numbers[buffer[i] - '0'];
                    }
                    else
                    {
                        buffer[i] = buffer[i];
                    }
                }
            }

            /* Step 6: return the (probably messed up if it got this far) string. */
            return new String(buffer);
        }

        /// <summary>
        /// Checks whether a connection is in a playing state.
        /// </summary>
        /// <returns>true if playing, false if not in a playing state (at menu or creating character).</returns>
        public bool IsPlaying()
        {
            return ( _connectionState <= ConnectionState.playing );
        }

        /// <summary>
        /// Called when a player quits or when camping preparations are complete.
        /// </summary>
        public static void Quit(CharData ch)
        {
            if (ch == null)
            {
                Log.Error("Quit: Called with null character.");
                return;
            }

            try
            {
                if (ch.HasActBit(PC.PLAYER_CAMPING))
                {
                    ch.RemoveActBit(PC.PLAYER_CAMPING);
                    Act("You climb into your bedroll and leave the realm.", ch, null, null, MessageTarget.character);
                    if (ch._sex == MobTemplate.Sex.male)
                        Act("$n&n climbs into his bedroll and leaves the realm.", ch, null, null, MessageTarget.room);
                    else if (ch._sex == MobTemplate.Sex.female)
                        Act("$n&n climbs into her bedroll and leaves the realm.", ch, null, null, MessageTarget.room);
                    else
                        Act("$n&n climbs into its bedroll and leaves the realm.", ch, null, null, MessageTarget.room);

                    string text = String.Format("{0} has camped out.", ch._name);
                    Log.Trace(text);
                    ImmortalChat.SendImmortalChat(ch, ImmortalChat.IMMTALK_LOGINS, ch.GetTrust(), text);
                }
                else
                {
                    ch.SendText("You leave the realm.\r\n\r\n");
                    Act("$n&n has left the realm.", ch, null, null, MessageTarget.room);
                    Log.Trace(String.Format("{0} has camped out.", ch._name));
                    ImmortalChat.SendImmortalChat(ch, ImmortalChat.IMMTALK_LOGINS, ch.GetTrust(), String.Format("{0} has camped out.", ch._name));
                }

                // I know we checked for position fighting, but I'm paranoid...
                if (ch._fighting)
                {
                    Combat.StopFighting(ch, true);
                }

                ch.DieFollower(ch._name);

                Room room = null;
                if (ch._inRoom)
                {
                    room = ch._inRoom;
                }

                ch.RemoveFromRoom();
                if (room != null)
                {
                    ch._inRoom = room;
                    ((PC)ch).LastRentLocation = ch._inRoom.IndexNumber;
                }

                // Put them in the correct body
                if (ch && ch._socket && ch._socket.Original)
                {
                    CommandType.Interpret(ch, "return");
                }

                CharData.SavePlayer(ch);

                Database.CharList.Remove(ch);

                if (ch && ch._socket)
                {
                    ch._socket.ShowScreen(Screen.MainMenuScreen); 
                    ch._socket._connectionState = ConnectionState.menu;
                }
            }
            catch (Exception ex)
            {
                Log.Error("Error in SocketConnection.Quit: " + ex.ToString());
            }

            return;
        }

        /// <summary>
        /// Applies a bonus roll to a player's ability scores.
        /// </summary>
        /// <param name="ch">The player.</param>
        /// <param name="argument">The score _name.</param>
        /// <returns>True if the bonus was applied successfully, false if it wasn't.</returns>
        private static bool ApplyAbilityBonus( CharData ch, string argument )
        {
            if (ch == null) return false;
            if( "strength".StartsWith( argument, StringComparison.CurrentCultureIgnoreCase ) )
            {
                ch._permStrength += MUDMath.Dice( 2, 8 );
                if (ch._permStrength > 100)
                {
                    ch._permStrength = 100;
                }
                return true;
            }
            if( "intelligence".StartsWith( argument, StringComparison.CurrentCultureIgnoreCase ) )
            {
                ch._permIntelligence += MUDMath.Dice( 2, 8 );
                if (ch._permIntelligence > 100)
                {
                    ch._permIntelligence = 100;
                }
                return true;
            }
            if( "wisdom".StartsWith( argument, StringComparison.CurrentCultureIgnoreCase ) )
            {
                ch._permWisdom += MUDMath.Dice( 2, 8 );
                if (ch._permWisdom > 100)
                {
                    ch._permWisdom = 100;
                }
                return true;
            }
            if( "dexterity".StartsWith( argument, StringComparison.CurrentCultureIgnoreCase ) )
            {
                ch._permDexterity += MUDMath.Dice( 2, 8 );
                if (ch._permDexterity > 100)
                {
                    ch._permStrength = 100;
                }
                return true;
            }
            if( "constitution".StartsWith( argument, StringComparison.CurrentCultureIgnoreCase ) )
            {
                ch._permConstitution += MUDMath.Dice( 2, 8 );
                if (ch._permConstitution > 100)
                {
                    ch._permConstitution = 100;
                }
                return true;
            }
            if( "agility".StartsWith( argument, StringComparison.CurrentCultureIgnoreCase ) )
            {
                ch._permAgility += MUDMath.Dice( 2, 8 );
                if (ch._permAgility > 100)
                {
                    ch._permAgility = 100;
                }
                return true;
            }
            if( "charisma".StartsWith( argument, StringComparison.CurrentCultureIgnoreCase ) )
            {
                ch._permCharisma += MUDMath.Dice( 2, 8 );
                if (ch._permCharisma > 100)
                {
                    ch._permCharisma = 100;
                }
                return true;
            }
            if( "power".StartsWith( argument, StringComparison.CurrentCultureIgnoreCase ) )
            {
                ch._permPower += MUDMath.Dice( 2, 8 );
                if (ch._permPower > 100)
                {
                    ch._permPower = 100;
                }
                return true;
            }
            return false;
        }

        /// <summary>
        /// Check whether the character picked a valid class for their race.
        /// </summary>
        /// <param name="ch">The player.</param>
        /// <param name="argument">Text that was entered.</param>
        /// <returns>The class number, or 0 for an invalid selection.</returns>
        private int CheckClassSelection( string argument )
        {
            bool[] classAvail = new bool[ Limits.MAX_CLASS ];
            for (int i = 1; i < CharClass.ClassList.Length; i++)
            {
                classAvail[ i ] = ( (int)Race.RaceList[ Character.GetOrigRace() ].ClassesAvailable & (int)Math.Pow( 2, ( i - 1 ) ) ) != 0;
                if (classAvail[i] && (((CharClass.Names)i).ToString()).StartsWith(argument, StringComparison.CurrentCultureIgnoreCase))
                {
                    return i;
                }
            }
            return 0;
        }

        /// <summary>
        /// Sets the default values for a newly-created player character.  Called when they enter the
        /// the game for the first time.
        /// </summary>
        /// <param name="ch"></param>
        private void SetupNewCharacter()
        {
            ((PC)Character).CreationTime = Database.SystemData.CurrentTime;
            ((PC)Character).Birthdate = Database.SystemData.CurrentTime;
            Character._level = 1;
            Character._experiencePoints = 1;
            Character.SetCoins( 0, 0, 0, 0 );
            // Con mod to newbie hitpoints for greater variation
            // keep in mind we're *still* using _character.GetMaxHit() on top of this
            Character._maxHitpoints += ( Character.GetCurrCon() / 30 );
            Character._hitpoints = Character.GetMaxHit();
            if (Character._charClass.GainsMana)
            {
                // Mana bonuses for newbies.
                Character._maxMana += (Character.GetCurrInt() / 10);
                Character._maxMana += (Character.GetCurrWis() / 14);
                Character._maxMana += (Character.GetCurrPow() / 7);
            }
            else
            {
                Character._maxMana = 0;
            }
            Character._currentMana = Character._maxMana;
            if (Character.GetOrigRace() == Race.RACE_CENTAUR)
            {
                Character._maxMoves += 80;
            }
            Character._currentMoves = Character._maxMoves;
            Character._size = Race.RaceList[ Character.GetOrigRace() ].DefaultSize;
            Character._alignment = Race.RaceList[ Character.GetOrigRace() ].BaseAlignment;
            ((PC)Character).GuildRank = Guild.Rank.normal;
            if( Character.IsClass( CharClass.Names.paladin ) )
            {
                Character._alignment = 1000;
            }
            else if( Character.IsClass( CharClass.Names.antipaladin ) )
            {
                Character._alignment = -1000;
            }
            else if( Character.IsClass( CharClass.Names.ranger ) )
            {
                Character._alignment += 250;
            }
            else if( Character.IsClass( CharClass.Names.assassin ) )
            {
                Character._alignment -= 100;
            }
            else if( Character.IsClass( CharClass.Names.cleric ) )
            {
                if (Character._alignment < 0)
                {
                    Character._alignment -= 250;
                }
                else if (Character._alignment > 0)
                {
                    Character._alignment += 250;
                }
            }

            if (Character._alignment < -1000)
            {
                Character._alignment = -1000;
            }
            else if (Character._alignment > 1000)
            {
                Character._alignment = 1000;
            }

            if( Character.IsClass( CharClass.Names.psionicist ) )
            {
                Character._currentMana = 45 + ( Character.GetCurrPow() / 10 );
            }

            // Set character's height and weight based on their race
            // using a generic 87.5-112.5% / 80-120% of race standard value
            ((PC)Character).Height = MUDMath.NumberRange(((Race.RaceList[Character.GetOrigRace()].Height * 7) / 8), ((Race.RaceList[Character.GetOrigRace()].Height * 9) / 8));
            ((PC)Character).Weight = MUDMath.NumberRange(((Race.RaceList[Character.GetOrigRace()].Weight * 8) / 10), ((Race.RaceList[Character.GetOrigRace()].Weight * 12) / 10));

            Command.SetTitle(Character, "&n");
            Character.InitializeSkills(); // Sets skills that player has to minimum levels.
            Character.InitializeSpells(); // Sets newbie spells to minimum levels
            // Set default prompt to "prompt meter" text.
            ((PC)Character).Prompt = "&n&+g<%h&n&+g/%H&n&+ghp %mm/%MM %v&n&+g/%V&n&+gmv>\r\n&n&+g<&n%d&n %b&+g>&n ";

            Character.SetActBit( PC.PLAYER_PAGER );

            Character.ReceiveNewbieEquipment();

            // Set default faction to global race faction values.
            for (int count = 0; count < Race.RaceList.Length; ++count)
            {
                ((PC)Character).RaceFaction[count] = Race.RaceList[Character.GetOrigRace()].RaceFaction[count];
            }

            // Give them the universal language
            ((PC)Character).LanguageAptitude[(int)Race.Language.unknown] = 100;

            // Give them their own language
            if (Race.RaceList[Character.GetOrigRace()].PrimaryLanguage > 0 && (int)Race.RaceList[Character.GetOrigRace()].PrimaryLanguage < Race.MAX_LANG)
            {
                ((PC)Character).LanguageAptitude[(int)Race.RaceList[Character.GetOrigRace()].PrimaryLanguage] = 100;
            }

            CharData.SavePlayer(Character);
            Character.SendText( "\r\nWelcome to the world of " + Database.SystemData.MudAnsiName + "!\r\n" +
                        "\r\nNew players should check out HELP INDEX and HELP NEWBIE" +
                        "\r\nAnd use the TOGGLE command to change various options.\r\n\r\n" );
                        
        }

        /// <summary>
        /// Figure out where there player should go and put them there.  Used when entering the game from the menu.
        /// 
        /// Starts with last rent location, then tries current home, then tries original home, then
        /// tries ROOM_NUMBER_START, then gives up.
        /// </summary>
        private void PlaceCharacterInGame()
        {
            Room room = Room.GetRoom(((PC)Character).LastRentLocation);
            if (room != null)
            {
                Character.AddToRoom(room);
            }
            else
            {
                Log.Error("Last rent location of " + ((PC)Character).LastRentLocation +
                    " not found. Sending them to repop room.");
                room = Room.GetRoom(((PC)Character).CurrentHome);
                {
                    if( room != null )
                    {
                        Character.AddToRoom(room);
                        ((PC)Character).LastRentLocation = room.IndexNumber;
                    }
                    else
                    {
                        Log.Error("Current home location of " + ((PC)Character).CurrentHome +
                            " not found.  Sending them to start room.");
                        room = Room.GetRoom( ((PC)Character).OriginalHome );
                        {
                            if( room != null )
                            {
                                Character.AddToRoom(room);
                                ((PC)Character).LastRentLocation = room.IndexNumber;
                            }
                            else
                            {
                                Log.Error("Original home location of " + ((PC)Character).CurrentHome +
                                    " not found.  Sending them to start room.");
                                room = Room.GetRoom( StaticRooms.GetRoomNumber("ROOM_NUMBER_START") );
                                {
                                    if( room != null )
                                    {
                                        Character.AddToRoom(room);
                                        ((PC)Character).LastRentLocation = room.IndexNumber;
                                    }
                                    else
                                    {
                                        Log.Error( "Start room " + StaticRooms.GetRoomNumber("ROOM_NUMBER_START") +
                                            " not found.  I give up.  MUD is broken." );
                                    }
                             
                                }
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Rerolls the player's statistics.
        /// </summary>
        /// <param name="ch"></param>
        private static void RerollStats( CharData ch )
        {
            ch._permStrength = MUDMath.Dice( 3, 31 ) + 7;
            ch._permIntelligence = MUDMath.Dice( 3, 31 ) + 7;
            ch._permWisdom = MUDMath.Dice( 3, 31 ) + 7;
            ch._permDexterity = MUDMath.Dice( 3, 31 ) + 7;
            ch._permConstitution = MUDMath.Dice( 3, 31 ) + 7;
            ch._permAgility = MUDMath.Dice( 3, 31 ) + 7;
            ch._permCharisma = MUDMath.Dice( 3, 31 ) + 7;
            ch._permPower = MUDMath.Dice( 3, 31 ) + 7;
            ch._permLuck = MUDMath.Dice( 3, 31 ) + 7;

            switch( ch._charClass.PrimeAttribute )
            {
                case Affect.Apply.strength:
                    ch._permStrength = MUDMath.NumberRange( 70, 100 );
                    break;
                case Affect.Apply.intelligence:
                    ch._permIntelligence = MUDMath.NumberRange( 70, 100 );
                    break;
                case Affect.Apply.wisdom:
                    ch._permWisdom = MUDMath.NumberRange( 70, 100 );
                    break;
                case Affect.Apply.dexterity:
                    ch._permDexterity = MUDMath.NumberRange( 70, 100 );
                    break;
                case Affect.Apply.constitution:
                    ch._permConstitution = MUDMath.NumberRange( 70, 100 );
                    break;
                case Affect.Apply.agility:
                    ch._permAgility = MUDMath.NumberRange( 70, 100 );
                    break;
                case Affect.Apply.charisma:
                    ch._permPower = MUDMath.NumberRange( 70, 100 );
                    break;
                case Affect.Apply.power:
                    ch._permCharisma = MUDMath.NumberRange( 70, 100 );
                    break;
                case Affect.Apply.luck:
                    ch._permLuck = MUDMath.NumberRange( 70, 100 );
                    break;
            }
        }

        /// <summary>
        /// Processes character actions -- updates wait timers and processes commands in the character's
        /// incoming buffer.
        /// </summary>
        private void ProcessPlayerActions()
        {
            SocketConnection sock = null;
            for (int i = (Database.SocketList.Count - 1); i >= 0; i--)
            {
                sock = Database.SocketList[i]; // Just to save space and make this more readable.
                if (sock.Character && sock.Character._wait > 0)
                {
                    --sock.Character._wait;
                    continue;
                }

                if (sock._commandQueuePointer.Length > 0)
                {
                    sock.MultipleCommands();
                }
                else
                {
                    sock.ReadFromBuffer();
                }

                if (sock._incomm.Length > 0 || sock._commandQueue.Length > 0)
                {
                    sock.Fcommand = true;
                    StopIdling(sock.Character);

                    string intcomm;
                    if (sock._commandQueuePointer.Length > 0)
                    {
                        intcomm = sock._commandQueue;
                    }
                    else
                    {
                        intcomm = sock._incomm;
                    }

                    if (sock.ShowstringPoint < sock._showstringHead.Length)
                    {
                        sock.ShowPagedString(sock._incomm);
                    }
                    else if (sock._stringEditing.Length > 0)
                    {
                        MUDString.StringAdd(sock.Character, sock._incomm);
                    }
                    else
                    {
                        switch (sock._connectionState)
                        {
                            case ConnectionState.playing:
                                if (sock.Character != null)
                                {
                                    if (sock.Character._inRoom != null)
                                    {
                                        Log.Trace("Interpreting command: '" + intcomm + "'" + " by " +
                                           sock.Character._name + " in room " + sock.Character._inRoom.IndexNumber);
                                    }
                                    else
                                    {
                                        Log.Trace("Interpreting command: '" + intcomm + "'" + " by " +
                                           sock.Character._name + " not in any valid room.");
                                    }
                                }
                                else
                                {
                                    Log.Trace("Command: '" + intcomm + "'" + " by (null).  Not sending to interpreter.");
                                    return;
                                }
                                CommandType.Interpret(sock.Character, intcomm);
                                break;
                            default:
                                //Log.Trace("Sending command '" + intcomm + "' to connection state manager.");
                                sock.ConnectionStateManager(intcomm);
                                break;
                        }
                    }
                    sock._incomm = String.Empty;
                }
            }
        }

        /// <summary>
        /// Processes sockets with errors as reported by the Select() statement.  Called by main game loop.
        /// </summary>
        /// <param name="errorList"></param>
        private void ProcessSocketErrors(ArrayList errorList)
        {
            // Close sockets that have errors.
            for (int i = (errorList.Count - 1); i >= 0; i--)
            {
                Socket sock = (Socket)errorList[i];
                // Save characters for error sockets so we don't lose data.
                for (int j = (Database.SocketList.Count - 1); j >= 0; j--)
                {
                    if (Database.SocketList[j]._socket == sock && Database.SocketList[j].Character)
                    {
                        CharData.SavePlayer(Database.SocketList[j].Character);
                    }
                    if (Database.SocketList[j]._socket == sock)
                    {
                        Database.SocketList.RemoveAt(j);
                    }
                }
                sock.Close();
            }
        }

        private void ProcessSocketReads(ArrayList listenList)
        {
            if (listenList == null || listenList.Count < 1)
            {
                return;
            }

            // Process input.
            //
            // Since we're processing a list at the socket level, we have to go through and
            // find the descriptor that matches the socket in order to process the input
            // and output.
            for (int i = (listenList.Count - 1); i >= 0; i--)
            {
                for (int counter = (Database.SocketList.Count - 1); counter >= 0; counter--)
                {
                    Database.SocketList[counter].Fcommand = false;

                    if (listenList[i] != Database.SocketList[counter]._socket)
                        continue;

                    if (Database.SocketList[counter].Character)
                        Database.SocketList[counter].Character._timer = 0;
                    // If we can't read from the socket, we save the character and close the socket.
                    if (!Database.SocketList[counter].ReadFromSocket())
                    {
                        if (Database.SocketList[counter].Character)
                        {
                            CharData.SavePlayer(Database.SocketList[counter].Character);
                        }
                        Database.SocketList[counter].Outbuf = String.Empty;
                        // Remove before closing to avoid errors.
                        SocketConnection sock = Database.SocketList[counter];
                        Database.SocketList.RemoveAt(counter);
                        sock.CloseSocket();
                        continue;
                    }
                }
            }
        }

        /// <summary>
        /// Process sockets with data needing to be written as reported by the Select() statement.
        /// Called by the main game loop.
        /// </summary>
        /// <param name="writeList"></param>
        private void ProcessSocketWrites(ArrayList writeList)
        {
            if (writeList == null || writeList.Count < 1)
            {
                return;
            }

            // Output.
            for (int i = (writeList.Count - 1); i >= 0; i--)
            {
                for (int d = (Database.SocketList.Count - 1); d >= 0; d--)
                {
                    if (writeList[i] == Database.SocketList[d]._socket)
                    {
                        if ((Database.SocketList[d].Fcommand || Database.SocketList[d].Outbuf.Length > 0))
                        {
                            // If we can't send to the socket, save the character and close the socket.
                            if (!Database.SocketList[d].ProcessOutput(true))
                            {
                                if (Database.SocketList[d].Character)
                                    CharData.SavePlayer(Database.SocketList[d].Character);
                                Database.SocketList[d].Outbuf = String.Empty;
                                // Remove before closing to avoid errors.
                                SocketConnection sock = Database.SocketList[d];
                                Database.SocketList.RemoveAt(d);
                                sock.CloseSocket();
                            }
                        }
                    }
                }
            }
        }
    }
}
