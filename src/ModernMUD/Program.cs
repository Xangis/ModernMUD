using System;
using MUDEngine;
using ModernMUD;

namespace ModernMUD
{
    /// <summary>
    /// Application runner for the ModernMUD code.
    /// </summary>
    public class Program
    {
        static void Main(string[] args)
        {
            // Get the port number.
            ushort port = 4502;
            if ( args != null )
            {
                foreach( string arg in args )
                {
                    bool success = ushort.TryParse( arg, out port );
                    if( success )
                        break;
                }
            }

            // Set up logging.
            Log.ExceptionDetailsEnabled = true;
            Log.TraceEnabled = true;

            // Create the socket engine.
            SocketConnection listenSocket = new SocketConnection();

            // Display our version information.
            string assemblyName = listenSocket.GetType().Assembly.GetName().Name;
            string assemblyVersion = listenSocket.GetType().Assembly.GetName().Version.ToString();
            System.IO.FileInfo assemblyInfo = new System.IO.FileInfo( listenSocket.GetType().Assembly.Location );
            DateTime assemblyDate = assemblyInfo.LastWriteTime;
            Log.Trace( "Running " + assemblyName + " version " + assemblyVersion + " located at " +
                       assemblyInfo + " and created on " + assemblyDate.ToShortDateString() + ".");

            // Initialize sockets, load the database, and run the game.
            System.Net.Sockets.Socket control = listenSocket.InitializeSocket( port );
            Database database = new Database();
            database.LoadDatabase( );

            Log.Trace(String.Format("{0} MUD is now running on port {1}.", Database.SystemData.MudName, port));
            try
            {
                listenSocket.MainGameLoop(control);
            }
            catch (Exception ex)
            {
                Log.Error("Unhandled exception running main game loop: " + ex.ToString());
                control.Close();
                return;
            }
            control.Close();

            // End of game.
            Log.Trace( "Normal shutdown of game." );
            return;
        }
    }
}