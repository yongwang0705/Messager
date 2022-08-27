using System.Net.Sockets;
using System.Net;
using NATUPNPLib;
namespace TcpSocketOperator
{
    public class TcpSocketCreator
    {
        public Socket _socket;
        public IPEndPoint _ipEndPoint;
        IPAddress _ipv4;
        private int _port { set; get; }
        public static bool UPnp(IPAddress ipv4, int internalPort, int externalPort, string description)
        {
            var upnpnat = new UPnPNAT();
            var mappings = upnpnat.StaticPortMappingCollection;
            if (mappings == null)
            {
                return false;
            }
            else
            {
                mappings.Add(externalPort, "TCP", internalPort, ipv4.ToString(), true, description);
                return true;
            }
        }

        public TcpSocketCreator(int port, IPAddress ipv4)
        {
            //var name = Dns.GetHostName();
            // var ipv4s = Dns.GetHostEntry(name).AddressList;

            //this._ipv4 = IPAddress.Parse("127.0.0.1");
            /* foreach (var i in ipv4s)
             {
                 if (i.AddressFamily == AddressFamily.InterNetwork)
                 {
                     _ipv4 = i;
                     break;
                 }
             }*/

            this._port = port;
            this._ipv4 = ipv4;
            this._ipEndPoint = new IPEndPoint(this._ipv4, this._port);
        }
        public Socket CreateTcpSocket()
        {
            this._socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            return this._socket;
        }
    }
    public class TcpSocketDestructor
    {
        public bool DestructTcpSocket(Socket _mySocket)
        {
            _mySocket.Close();
            if (_mySocket.Connected == false)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
