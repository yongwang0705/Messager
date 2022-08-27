using System;
using System.Net.Sockets;
using System.Net;
using System.Configuration;
namespace Client
{
    class TcpSocketCreator
    {
        public Socket socket;
        public IPEndPoint endPoint;
        private int _port { set; get; }
        public TcpSocketCreator(int port)
        {
            this._port = port;
            IPAddress ip = IPAddress.Parse(ConfigurationManager.AppSettings["ServerIp"]);
            this.endPoint = new IPEndPoint(ip, _port);
        }
        public Socket CreateTcpSocket()
        {
            this.socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            return this.socket;
        }
    }
}
