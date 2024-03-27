using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Runtime.InteropServices;


namespace _52294_Socket_Master
{

    internal class AsynchronousSocketListener
    {
        internal delegate void WaitConnectCallback(int port);
        internal delegate void ConnectedCallback();
        internal delegate void StoppedCallback();
        internal delegate void ReceivedCallback(String data);

        private class ReceiveBuffer
        {
            // Size of receive outData.  
            internal const int BufferSize = 1024;

            // Receive outData.  
            internal byte[] data = new byte[BufferSize];

            // Used worker socket, other could be null or disposed
            internal Socket origWorker;
        }

        private int _assignedPort = 0;
        private Socket _listener;
        private Socket _worker;
        private WaitConnectCallback _waitConnectCallback;
        private ConnectedCallback _connectedCallback;
        private StoppedCallback _stoppedCallback;
        private ReceivedCallback _receivedCallback;

        private String _receivedData;

        internal WaitConnectCallback waitConnectCallback
        {
            set { _waitConnectCallback = value; }
        }

        internal ConnectedCallback connectedCallback
        {
            set { _connectedCallback = value; }
        }

        internal StoppedCallback stoppedCallback
        {
            set { _stoppedCallback = value; }
        }

        internal ReceivedCallback receivedCallback
        {
            set { _receivedCallback = value; }
        }

        internal AsynchronousSocketListener()
        {
            _listener = null;
            _worker = null;
        }

        internal void StartListening(int port = 0) // 0 = any
        {
            IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
            IPAddress ipAddress = IPAddress.Parse("127.0.0.1");                 // localhost
            IPEndPoint localEndPoint = new IPEndPoint(ipAddress, port);
            if (_listener == null)
                _listener = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

            _assignedPort = port;
            try
            {
                _listener.Bind(localEndPoint);
                _listener.Listen(1);
                _assignedPort = ((IPEndPoint)_listener.LocalEndPoint).Port;

                _listener.BeginAccept(new AsyncCallback(_AcceptCallback), _listener);

                if (_waitConnectCallback != null) _waitConnectCallback(_assignedPort);
            }
            catch
            {
            }
        }

        internal void StopListening()
        {
            try
            {
                if (_listener != null) _listener.Close();
                if (_worker != null) _worker.Close();
                _listener = null;
                _worker = null;
            }
            catch
            {
            }
            if (_stoppedCallback != null) _stoppedCallback();
        }

        internal void CloseConnection()
        {
            StopListening();
            StartListening(_assignedPort);
        }

        private void _AcceptCallback(IAsyncResult ar)
        {
            try
            {
                if (_listener != null)
                {
                    // Get the socket that handles the client request.  
                    _worker = _listener.EndAccept(ar);

                    _listener.Close();
                    _listener = null;
                    
                    if (_connectedCallback != null) _connectedCallback();

                    _receivedData = String.Empty;
                    _WaitForReceive();

                }
            }
            catch
            {
            }
        }

        private void _WaitForReceive()
        {
            if (_worker != null)
            {
                try
                {
                    ReceiveBuffer buffer = new ReceiveBuffer();
                    buffer.origWorker = _worker;
                    _worker.BeginReceive(buffer.data, 0, ReceiveBuffer.BufferSize, 0, new AsyncCallback(_ReveiveCallback), buffer);
                }
                catch
                {
                }
            }
        }

        private void _ReveiveCallback(IAsyncResult ar)
        {
            try
            {
                ReceiveBuffer buffer = (ReceiveBuffer)ar.AsyncState;

                // Read outData from the client socket.
                int bytesRead = buffer.origWorker.EndReceive(ar);

                if (bytesRead > 0)
                {
                    // Retrieve the state object

                    // append to buffer
                    _receivedData += Encoding.ASCII.GetString(buffer.data, 0, bytesRead);

                    // newline?
                    if (_receivedData.IndexOf("\n") > -1)
                    {
                        // All the outData has been read from the client 
                        if (_receivedCallback != null) _receivedCallback(_receivedData);
                        _receivedData = String.Empty;
                    }
                }

                _WaitForReceive();
            }
            catch
            {
            }
        }

        internal void Send(String data)
        {
            if (_worker != null)
            {
                try
                {
                    // Convert the string outData to byte outData using ASCII encoding.  
                    byte[] byteData = Encoding.ASCII.GetBytes(data);

                    // Begin sending the outData to the remote device.  
                    _worker.BeginSend(byteData, 0, byteData.Length, 0, new AsyncCallback(_SendDoneCallback), _worker);
                }
                catch
                {
                }
            }
        }

        private void _SendDoneCallback(IAsyncResult ar)
        {
            try
            {
                Socket origWorker = (Socket)ar.AsyncState;
                int bytesSent = origWorker.EndSend(ar);
            }
            catch
            {
            }
        }
        
    }

}