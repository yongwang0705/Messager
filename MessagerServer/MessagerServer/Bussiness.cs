using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Threading;
using UIOperator;

namespace MessagerServer
{
    public class Bussiness
    {
        private static object _lock = new object();
        private Socket _messagerExchangeSocket;
        private Socket _databaseExchangeSocket;
        private static int _reconfTime;
        public static int _threadCurNumber;
        public static int _threadMaxNumber;
        public static int ThreadMaxNumber
        {
            set
            {
                _threadMaxNumber = ThreadMaxNumber;
            }
            get
            {
                return _threadMaxNumber;
            }
        }
        public static int ThreadCurNumber
        {
            set
            {
                _threadCurNumber = ThreadCurNumber;
            }
            get
            {
                return _threadCurNumber;
            }
        }

        public Bussiness(int reconfTime,Socket messageMonitorSocket, Socket databaseExchangeSocket)
        {
            _reconfTime = reconfTime;
            this._messagerExchangeSocket = messageMonitorSocket;
            this._databaseExchangeSocket = databaseExchangeSocket;
            _threadCurNumber = 0;
            //_threadMaxNumber = max;
        }
        public void DatabaseOperationQuery()
        {
            int receiveNumber=0;
            Byte[] buffer = new Byte[1024];
            string strContent;
            Socket tmpSocket;
            while (true)
            {
                try
                {
                    while (this._databaseExchangeSocket.Poll(_reconfTime, SelectMode.SelectRead) == false)
                    {
                        Thread.Sleep(1000);
                    }
                }
                catch(Exception ex)
                {
                    UIOperatorClass.UpdateListbox(ServerForm.serverForm.messagelistBox, ex.Message, Operate.Add);
                }
                if (ServerForm.serverForm._running == false)
                {
                   break;
                }
                tmpSocket = _databaseExchangeSocket.Accept();
                //while (tmpSocket.Available > 0)
                {
                    try
                    {
                        receiveNumber = tmpSocket.Receive(buffer, 0, tmpSocket.Available, SocketFlags.None);
                    }
                    catch (Exception)
                    {

                        continue;

                    }
                }
                strContent = Encoding.Default.GetString(buffer, 0, receiveNumber);
                if (strContent.Length <= 0)
                    continue;
                string[] arr = strContent.Split('|');
                Client newClient = new Client();
                newClient._socket = tmpSocket;
                //registration new user return id 
                if (arr[2].ToString() == "signup")
                {
                    newClient.name = arr[3];
                    newClient.password = arr[4];

                    Thread clientRegistrationThread = CreateClientRegistrationThread(newClient);
                    clientRegistrationThread.IsBackground = true;
                    clientRegistrationThread.Start();
                }
            }
        }
        public void MessagerExchange()
        {
            Socket tmpSocket;
            while (true)
            {
                try
                {
                    while (this._messagerExchangeSocket.Poll(_reconfTime, SelectMode.SelectRead) == false)
                    {
                    }
                }
                catch (Exception ex)
                {
                    UIOperatorClass.UpdateListbox(ServerForm.serverForm.messagelistBox, ex.Message, Operate.Add);
                }
                if (ServerForm.serverForm._running == false)
                {
                    break;
                }
                tmpSocket = _messagerExchangeSocket.Accept();

                if (_threadCurNumber >= _threadMaxNumber)
                {
                    UIOperatorClass.UpdateListbox(ServerForm.serverForm.messagelistBox, "The number of threads arrived the limit", Operate.Add);
                    tmpSocket.Close();
                }
                else if (_threadCurNumber <_threadMaxNumber)
                {
                    Client newClient = new Client();
                    newClient._socket = tmpSocket;
                    Thread clientReadThread = CreateClientReadThread(newClient);
                    Thread clientWriteThread = CreateClientWriteThread(newClient);
                    clientWriteThread.IsBackground = true;
                    clientReadThread.IsBackground = true;
                    lock (_lock) 
                    {
                        _threadCurNumber++;
                        UIOperatorClass.UpdateLabel(ServerForm.serverForm.labelMessage,_threadCurNumber.ToString());
                    }
                    clientReadThread.Start(newClient._socket);
                    clientWriteThread.Start(newClient._socket);
                }
            }
        }
        Thread CreateClientReadThread(Client newClient)
        {
            ParameterizedThreadStart parameterizedThreadStart = new ParameterizedThreadStart(newClient.Read);
            Thread clientThread = new Thread(parameterizedThreadStart);
            return clientThread;
        }
        Thread CreateClientWriteThread(Client newClient)
        {
            ParameterizedThreadStart parameterizedThreadStart = new ParameterizedThreadStart(newClient.Write);
            Thread clientThread = new Thread(parameterizedThreadStart);
            return clientThread;
        }

        Thread CreateClientRegistrationThread(Client newClient)
        {
            ParameterizedThreadStart parameterizedThreadStart = new ParameterizedThreadStart(newClient.Register);
            Thread clientThread = new Thread(parameterizedThreadStart);
            return clientThread;
        }
    }
}
