using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Threading;
namespace Client
{
    class MonitorThreadsCreator
    {
        private Client _client { set; get; }
        public MonitorThreadsCreator(Client client)
        {
            this._client = client;
        }
        //public Queue<MyMessage> myMessageQueue = new Queue<MyMessage>();
        static object _lock = new object();
        public Thread CreateHeatBeatThread(Client client)
        {
            Thread _heatBeatSocket = new Thread(HeatBeat);
            return _heatBeatSocket;
        }
        public Thread CreateReadThread(Client client)
        {
            //this._client.Read(this._monitorSocket);
            Thread monitorThread = new Thread(client.Read);
            return monitorThread;
        }

        public void HeatBeat(object clientObect) //reflesh userlist
        {
            Socket clientSocket = clientObect as Socket;
            string strContent;
            strContent = "request userlist";
            while (true)
            {
                this._client.Send(0,clientSocket, strContent);
                Thread.Sleep(10000);
            }

        }
    }
}
