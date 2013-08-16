using System;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace WPFMudClient
{
    public class SocketConnection
    {
        private Socket _socket;
        byte[] _inBuffer;
        private IPHostEntry _endpoint;
        private IPAddress _ipAddress;
        private TextCallback _textCallback;
        public delegate void TextCallback(string text);

        /// <summary>
        /// Removes ANSI codes from a string.
        /// </summary>
        /// <param name="text"></param>
        public static string RemoveANSICodes(String text)
        {
	        text = text.Replace("\x1B[0m", "" );
	        text = text.Replace("\x1B[30m", "" );
	        text = text.Replace("\x1B[31m", "" );
	        text = text.Replace("\x1B[32m", "" );
	        text = text.Replace("\x1B[33m", "" );
            text = text.Replace("\x1B[34m", "");
	        text = text.Replace("\x1B[35m", "" );
	        text = text.Replace("\x1B[36m", "" );
	        text = text.Replace("\x1B[37m", "" );
	        text = text.Replace("\x1B[0;30m", "" );
	        text = text.Replace("\x1B[0;31m", "" );
	        text = text.Replace("\x1B[0;32m", "" );
	        text = text.Replace("\x1B[0;33m", "" );
	        text = text.Replace("\x1B[0;34m", "" );
	        text = text.Replace("\x1B[0;35m", "" );
	        text = text.Replace("\x1B[0;36m", "" );
	        text = text.Replace("\x1B[0;37m", "" );
	        text = text.Replace("\x1B[1;30m", "" );
	        text = text.Replace("\x1B[1;31m", "" );
	        text = text.Replace("\x1B[1;32m", "" );
	        text = text.Replace("\x1B[1;33m", "" );
	        text = text.Replace("\x1B[1;34m", "" );
	        text = text.Replace("\x1B[1;35m", "" );
	        text = text.Replace("\x1B[1;36m", "" );
	        text = text.Replace("\x1B[1;37m", "" );
            return text;
        }

        public SocketConnection(TextCallback textCallback)
        {
            _textCallback = textCallback;
        }

        public void Connect(String host, int port)
        {
            try
            {
                _endpoint = Dns.GetHostEntry(host);
            }
            catch (Exception)
            {
                _textCallback("Unable to get host entry for server.");
                return;
            }

            try
            {
                _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            }
            catch (Exception)
            {
                _textCallback("Unable to create socket.");
                return;
            }

            try
            {
                _socket.Blocking = false;
                AsyncCallback onConnect = new AsyncCallback(OnConnect);
                _socket.BeginConnect(host, port, onConnect, _socket);
            }
            catch (SocketException ex)
            {
                _textCallback("Socket error connecting to server: " + ex.SocketErrorCode);
                return;
            }
            catch( Exception )
            {
                _textCallback("Error connecting to server.");
                return;
            }
        }

        /// <summary>
        /// Terminates the socket connection.
        /// </summary>
        public void Close()
        {
            if (_socket != null && _socket.Connected)
            {
                _socket.Shutdown(SocketShutdown.Both);
                _socket.Close();
            }
        }
         
        /// <summary>
        /// Socket connection callback.
        /// </summary>
        /// <param name="ar"></param>
        public void OnConnect(IAsyncResult ar)
        {
            _socket = (Socket)ar.AsyncState;
            try
            {
                _socket.EndConnect(ar);
                if (_socket.Connected)
                {
                    _textCallback("Connected.\n");
                    SetupReceiveCallback(_socket);
                }
            }
            catch
            {
                _textCallback("Error connecting to server.");
                return;
            }
        }

        public void SetupReceiveCallback(Socket sock)
        {
            try
            {
                AsyncCallback receiveData = new AsyncCallback(OnReceiveData);
                _inBuffer = new Byte[1536];
                sock.BeginReceive(_inBuffer, 0, 1536, SocketFlags.None, receiveData, sock);
            }
            catch (Exception)
            {
                _textCallback("Error setting up receive buffer.");
                return;
            }
        }

        /// <summary>
        /// Processes incoming data from the socket.
        /// </summary>
        /// <param name="ar"></param>
        public void OnReceiveData(IAsyncResult ar)
        {
            _socket = (Socket)ar.AsyncState;

            try
            {
                int bytesReceived = _socket.EndReceive(ar);
                if (bytesReceived > 0)
                {
                    StringBuilder buffer = new StringBuilder();
                    for (int i = 0; i < bytesReceived; i++)
                    {
                        buffer.Append((Char)_inBuffer[i]);
                    }
                    _textCallback(buffer.ToString());
                    _inBuffer = null;
                    SetupReceiveCallback(_socket);
                }
            }
            catch
            {
                _textCallback("Error receiving data.");
                return;
            }
        }

        public void Send(String text)
        {
            if (_socket != null && _socket.Connected)
            {
                byte[] sendBytes = Encoding.ASCII.GetBytes(text + "\n");
                AsyncCallback onSend = new AsyncCallback(OnSend);
                _socket.BeginSend(sendBytes, 0, sendBytes.Length, SocketFlags.None, onSend, _socket);
            }
        }

        public void OnSend(IAsyncResult ar)
        {
            _socket = (Socket)ar.AsyncState;

            try
            {
                int bytesSent = _socket.EndSend(ar);
                if (bytesSent > 0)
                {
                }
            }
            catch (Exception)
            {
                _textCallback("Error sending data.");
                return;
            }
        }
    }
}
