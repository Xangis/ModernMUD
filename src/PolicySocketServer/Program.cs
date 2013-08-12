using System;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Threading;
using System.Reflection;

namespace PolicySocketServer
{
    class PolicySocketServer
    {
        TcpListener _listener;
        TcpClient _client;
        static ManualResetEvent _TcpClientConnected = new ManualResetEvent(false);
        const string PolicyRequestString = "<policy-file-request/>";
        int _receivedLength;
        byte[] _policy;
        byte[] _receiveBuffer;

        static void Main()
        {
            PolicySocketServer server = new PolicySocketServer();
            server.StartSocketServer();
        }

        private void InitializeData()
        {
            const string policyFile = "policy.txt";
            using (FileStream fs = new FileStream(policyFile, FileMode.Open))
            {
                _policy = new byte[fs.Length]; fs.Read(_policy, 0, _policy.Length);
            }
            _receiveBuffer = new byte[PolicyRequestString.Length];
        }

        public void StartSocketServer()
        {
            InitializeData();
            try
            {
                // Using TcpListener which is a wrapper around a Socket 
                // Allowed port is 943 for Silverlight sockets policy data
                _listener = new TcpListener(IPAddress.Any, 943); _listener.Start(); Console.WriteLine("Policy server listening..."); while (true)
                {
                    _TcpClientConnected.Reset(); Console.WriteLine("Waiting for client connection..."); _listener.BeginAcceptTcpClient(new AsyncCallback(OnBeginAccept), null); _TcpClientConnected.WaitOne();
                    // Block until client connects
                }
            }
            catch (Exception exp)
            { LogError(exp); }
        } private void OnBeginAccept(IAsyncResult ar) { _client = _listener.EndAcceptTcpClient(ar); _client.Client.BeginReceive(_receiveBuffer, 0, PolicyRequestString.Length, SocketFlags.None, new AsyncCallback(OnReceiveComplete), null); }
        private void OnReceiveComplete(IAsyncResult ar)
        {
            try
            {
                _receivedLength += _client.Client.EndReceive(ar);
                // See if there's more data that we need to grab 
                if (_receivedLength < PolicyRequestString.Length)
                {
                    // Need to grab more data so receive remaining data
                    _client.Client.BeginReceive(_receiveBuffer, _receivedLength, PolicyRequestString.Length - _receivedLength, SocketFlags.None, new AsyncCallback(OnReceiveComplete), null); return;
                }
                // Check that <policy-file-request/> was sent from client
                string request = System.Text.Encoding.UTF8.GetString(_receiveBuffer, 0, _receivedLength); if (StringComparer.InvariantCultureIgnoreCase.Compare(request, PolicyRequestString) != 0)
                {
                    // Data received isn't valid so close
                    _client.Client.Close(); return;
                }
                // Valid request received....send policy data 
                _client.Client.BeginSend(_policy, 0, _policy.Length, SocketFlags.None, new AsyncCallback(OnSendComplete), null);
            }
            catch (Exception exp) { _client.Client.Close(); LogError(exp); } _receivedLength = 0; _TcpClientConnected.Set();
            // Allow waiting thread to proceed
        }
        private void OnSendComplete(IAsyncResult ar)
        {
            try { _client.Client.EndSendFile(ar); }
            catch (Exception exp) { LogError(exp); }
            finally
            {
                // Close client socket
                _client.Client.Close();
            }
        }

        private void LogError(Exception exp)
        {
            string appFullPath = Assembly.GetCallingAssembly().Location;
            string logPath = appFullPath.Substring(0, appFullPath.LastIndexOf("\\")) + ".log";
            StreamWriter writer = new StreamWriter(logPath, true); 
            try 
            {
                writer.WriteLine(logPath, String.Format("Error in PolicySocketServer: " + "{0} \r\n StackTrace: {1}", exp.Message, exp.StackTrace)); 
            }
            catch
            { 
            }
            finally
            {
                writer.Close(); 
            }
        }
    }
}