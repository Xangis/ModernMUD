using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Net.Sockets;

namespace SilverlightApplication1
{
    public partial class Page : UserControl
    {
        private Socket _socket;
        private DnsEndPoint _endpoint;

        public Page()
        {
            InitializeComponent();
        }

        public void OnTextChanged(Object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtInput.Text))
            {
                return;
            }

            if (txtInput.Text.EndsWith("\r"))
            {
                string txt = txtInput.Text;
                Dispatcher.BeginInvoke(() =>
                {
                    txtOutput.Text += txtInput.Text;
                    txtInput.Text = String.Empty;
                }
                );
                Send(txt + "\n");
            }
        }

        private void OnReceive(object sender, SocketAsyncEventArgs e)
        {
            String a = System.Text.Encoding.UTF8.GetString(e.Buffer, e.Offset, e.BytesTransferred);
            Dispatcher.BeginInvoke(() =>
                {
                    txtOutput.Text += a;
                }
            );
            
            _socket.ReceiveAsync(e);
        }

        private void Send(string message)
        {
            Byte[] bytes = System.Text.Encoding.UTF8.GetBytes(message);

            SocketAsyncEventArgs args = new SocketAsyncEventArgs();
            args.SetBuffer(bytes, 0, bytes.Length);
            args.UserToken = _socket;
            args.RemoteEndPoint = _endpoint;
            args.Completed += new EventHandler<SocketAsyncEventArgs>(OnSend);

            _socket.SendAsync(args);
        }

        private void OnSend(object sender, SocketAsyncEventArgs e)
        {
            if (e.SocketError == SocketError.Success)
            {
                if (e.LastOperation == SocketAsyncOperation.Send)
                {
                    // Prepare receiving.
                    Socket s = e.UserToken as Socket;

                    byte[] response = new byte[255];
                    e.SetBuffer(response, 0, response.Length);
                    e.Completed += new EventHandler<SocketAsyncEventArgs>(OnReceive);
                    s.ReceiveAsync(e);
                }
            }
        }

        private void OnConnect(object sender, SocketAsyncEventArgs e)
        {
            if (txtInput.Text == "Type Here")
            {
                txtInput.Text = String.Empty;
            }
            e.Completed -= new EventHandler<SocketAsyncEventArgs>(OnConnect);
            e.Completed += new EventHandler<SocketAsyncEventArgs>(OnReceive);
            Byte[] transferBuffer = new byte[1536];
            e.SetBuffer(transferBuffer, 0, transferBuffer.Length);
            _socket.ReceiveAsync(e);
        }

        private void OnDisconnect(object sender, SocketAsyncEventArgs e)
        {
            if( _socket != null )
            {
                if( _socket.Connected )
                {
                    _socket.Close();
                }
                _socket.Dispose();
            }
        }

        private void btnConnect_Click(object sender, RoutedEventArgs e)
        {
            Connect();
        }

        private void Connect()
        {
            _endpoint = new DnsEndPoint("localhost", 4502);
            _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            SocketAsyncEventArgs args = new SocketAsyncEventArgs();

            args.UserToken = _socket;
            args.RemoteEndPoint = _endpoint;
            args.Completed += new EventHandler<SocketAsyncEventArgs>(OnConnect);

            _socket.ConnectAsync(args);
        }
    }
}
