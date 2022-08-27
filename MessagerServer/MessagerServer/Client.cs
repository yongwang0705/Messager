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
    enum Gender
    {
        male,
        female,
    }
    public class Client
    {
        public Socket _socket;
        public EventWaitHandle _eventWaitHandle;
        public Queue<MyMessage> _myMessageQueue;
        static object _lock = new object();
        public int id { set; get; }
        public string name { set; get; }
        public string password { set; get; }
        public string ipaddress { set; get; }

        public Client()
        {
            this._myMessageQueue = new Queue<MyMessage>();
            this._eventWaitHandle = new EventWaitHandle(false, EventResetMode.AutoReset);
        }
        private void setValue(string strValue, int i)
        {
            switch (i)
            {
                case 1://userlistBox
                    break;
                case 2://idlistBox
                    break;
                case 3://messagelistBox
                    break;
            }


        }
        // Client_handler consist of 2 threads read and send
        public void Write(object tmpSocket)
        {
            Socket clientSocket;
            clientSocket = (Socket)tmpSocket;
            //this._myMessageQueue = ServerForm.serverForm.dicMyMessages[this.id];
            while (true)
            {
                this._eventWaitHandle.WaitOne();
                if(ServerForm.serverForm._running==false)
                {
                    break;
                }
                try
                {
                    if (this._myMessageQueue.Count > 0)
                    {
                        lock (_lock)
                        {
                            for (int i = 0; i <= this._myMessageQueue.Count; i++)
                            {
                                MyMessage myMessage = this._myMessageQueue.Peek();

                                if (myMessage._content != null && myMessage._toClientId == this.id) //if this message is send to me
                                {
                                    string sendContent = myMessage._fromClientId + "|" + myMessage._toClientId + "|" + myMessage._content + "|" + myMessage._time + "$";
                                    clientSocket.Send(Encoding.Default.GetBytes(sendContent));
                                    this._myMessageQueue.Dequeue();
                                }
                            }
                        }

                    }
                }
                catch (Exception ex)
                {
                    UIOperatorClass.UpdateListbox(ServerForm.serverForm.messagelistBox, "WriteThread:" + ex.Message, Operate.Add);
                    break;
                }
            }
        }
        public void Register(object tmpSocket)
        {
            string sendContent;
            Socket clientSocket;
            clientSocket = (Socket)tmpSocket;
            clientSocket = _socket;
            DataBusiness dataBusiness = new DataBusiness();
            this.id = dataBusiness.CreateUser(this.name, this.password);

            sendContent = "0|" + this.id.ToString() + "|Response|" + this.id + "|" + DateTime.Now.ToString();
            try
            {
                clientSocket.Send(Encoding.Default.GetBytes(sendContent));
            }
            catch(Exception)
            {
                UIOperatorClass.UpdateListbox(ServerForm.serverForm.messagelistBox, "The registration is failed", Operate.Add);
                return;
            }
            this._myMessageQueue = null;
            clientSocket.Close();  
            if(this.id!=0)
                UIOperatorClass.UpdateListbox(ServerForm.serverForm.messagelistBox, "The registration is succeed", Operate.Add);
            else
                UIOperatorClass.UpdateListbox(ServerForm.serverForm.messagelistBox, "The registration is failed", Operate.Add);
        }

        public void Read(object tmpSocket)
        {
            int receiveNumber;
            Byte[] buffer = new Byte[100000];
            string strContent;
            Socket clientSocket;
            clientSocket = (Socket)tmpSocket;
            Queue<MyMessage> destQueue;

            while (true)
            {
                if (ServerForm.serverForm._running == false)
                {
                    break;
                }
                try
                {
                    // .. analyse the message
                    receiveNumber = clientSocket.Receive(buffer, 0, clientSocket.Available, SocketFlags.None);
                }
                catch (Exception ex)
                {
                    
                    if(ServerForm.serverForm._running==true)
                    {
                        UIOperatorClass.UpdateListbox(ServerForm.serverForm.messagelistBox, "ReadThread:"+ex.Message, Operate.Add);
                        clientSocket.Shutdown(SocketShutdown.Both);
                        clientSocket.Close();
                    }

                    
                    UIOperatorClass.UpdateListbox(ServerForm.serverForm.idlistBox, this.id.ToString(), Operate.Delete);
                    UIOperatorClass.UpdateListbox(ServerForm.serverForm.userlistBox, this.name, Operate.Delete);
                    Bussiness._threadCurNumber--;
                    UIOperatorClass.UpdateLabel(ServerForm.serverForm.labelMessage, Bussiness._threadCurNumber.ToString());
                    destQueue = null;
                    UpdateUserlist(destQueue);
                    this._myMessageQueue = null;
                    if (ServerForm.serverForm._running == true)
                    {
                        if(ServerForm.serverForm.dicWaitHandle.ContainsKey(this.id))
                            ServerForm.serverForm.dicWaitHandle[this.id].Set();
                    }
                    if (ServerForm.serverForm.dicMyMessages.ContainsKey(this.id))
                        ServerForm.serverForm.dicMyMessages.Remove(this.id);

                    if (ServerForm.serverForm.dictionaryClient.ContainsKey(this.id))
                        ServerForm.serverForm.dictionaryClient.Remove(this.id);

                    if (ServerForm.serverForm.dicWaitHandle.ContainsKey(this.id))
                        ServerForm.serverForm.dicWaitHandle.Remove(this.id);
                    
                    break;
                }
                strContent = Encoding.Default.GetString(buffer, 0, receiveNumber);
                if (strContent.Length <= 0)
                    continue;
                string[] arrM = strContent.Split('$');
                for (int j = 0; j < arrM.Length; j++)
                {
                    string[] arr = arrM[j].Split('|');
                    if (arr.Length < 4)
                        continue;
                    if (this.id <= 0) //new user
                    {
                        lock (_lock)
                        {
                            DataBusiness dataBusiness = new DataBusiness();
                            string sendContent;
                            //verifier the validity of user name and password
                            //to do if valid
                            if (arr[2].ToString() == "signin")
                            {
                                string returnValues = null;// this return the name
                                if (dataBusiness.VerifierUser(arr[0], arr[4], ref returnValues))
                                {
                                    this.id = int.Parse(arr[0]);
                                    this.password = arr[4];
                                    this.name = returnValues;
                                    //|Response|name successful  |Response|0 failed
                                    sendContent = "0|" + this.id.ToString() + "|ResponseUserName|" + this.name + "| " + DateTime.Now.ToString()+"$";
                                    clientSocket.Send(Encoding.Default.GetBytes(sendContent));
                                    if (ServerForm.serverForm.dicMyMessages.ContainsKey(this.id))
                                    {
                                        sendContent = "0|" + this.id.ToString() + "|ResponseUserName|1|" + DateTime.Now.ToString() + "$";
                                        clientSocket.Send(Encoding.Default.GetBytes(sendContent));
                                        clientSocket.Shutdown(SocketShutdown.Both);
                                        continue;
                                    }
                                    else
                                    {
                                        ServerForm.serverForm.dicMyMessages.Add(this.id, this._myMessageQueue);
                                        ServerForm.serverForm.dictionaryClient.Add(this.id, this);
                                        ServerForm.serverForm.dicWaitHandle.Add(this.id, this._eventWaitHandle);
                                        UIOperatorClass.UpdateListbox(ServerForm.serverForm.messagelistBox, "New client " + this.name, Operate.Add);
                                        UIOperatorClass.UpdateListbox(ServerForm.serverForm.userlistBox, this.name, Operate.Add);
                                        UIOperatorClass.UpdateListbox(ServerForm.serverForm.idlistBox, this.id.ToString(), Operate.Add);
                                        destQueue = null;
                                        Thread.Sleep(500);
                                        UpdateUserlist(destQueue);
                                    }
                                }
                                else
                                {
                                    sendContent = "0|" + this.id.ToString() + "|ResponseUserName|0|" + DateTime.Now.ToString()+"$";
                                    clientSocket.Send(Encoding.Default.GetBytes(sendContent));
                                    clientSocket.Shutdown(SocketShutdown.Both);
                                }                               
                            }
                        }
                    }
                    else//existed user, enqueue&dequeue from myMessageQueue //.. transfer to another client
                    {
                        //creat message instance from/to/content/time
                        int tmpFromId, tmpToId;
                        if (int.TryParse(arr[0], out tmpFromId) && int.TryParse(arr[1], out tmpToId))
                        {
                            if (ServerForm.serverForm.dictionaryClient.ContainsKey(tmpToId)) //if the target is existed, then put the message in message queue
                            {
                                destQueue = ServerForm.serverForm.dicMyMessages[tmpToId];
                                MyMessage myMessage = new MyMessage(tmpFromId, tmpToId, arr[2], arr[3]);
                                lock (_lock)// put message in
                                {
                                    destQueue.Enqueue(myMessage);
                                    ServerForm.serverForm.dicWaitHandle[tmpToId].Set();
                                }
                            }
                            else
                            {
                                if(arr[2].Contains( "request userlist"))
                                {
                                    destQueue = null; 
                                    UpdateUserlist(destQueue);
                                    continue;
                                }
                                string sendContent = "0|" + this.id + "|Exception|" + tmpToId + "|" + DateTime.Now.ToString()+"$";
                                clientSocket.Send(Encoding.Default.GetBytes(sendContent));
                            }
                        }

                    }
                }
            }
            UIOperatorClass.UpdateListbox(ServerForm.serverForm.userlistBox, this.name, Operate.Delete);
            UIOperatorClass.UpdateListbox(ServerForm.serverForm.idlistBox, this.id.ToString(), Operate.Delete);
        }
        void UpdateUserlist(Queue<MyMessage> destQueue)
        {
            string _strContent = "userlist|";
            for (int i = 0; i < ServerForm.serverForm.userlistBox.Items.Count; i++)
            {
                _strContent += ServerForm.serverForm.userlistBox.Items[i].ToString() + "|";
                _strContent += ServerForm.serverForm.idlistBox.Items[i].ToString() + "|";
            }
            _strContent = _strContent.Substring(0, _strContent.Length - 1);
            int tmpFromId, tmpToId;
            tmpFromId = 0;

            for (int i = 0; i < ServerForm.serverForm.userlistBox.Items.Count; i++)
            {
                tmpToId = int.Parse(ServerForm.serverForm.idlistBox.Items[i].ToString());
                MyMessage myMessage = new MyMessage(tmpFromId, tmpToId, _strContent, DateTime.Now.ToString());
                destQueue = ServerForm.serverForm.dicMyMessages[tmpToId];
                destQueue.Enqueue(myMessage);
                ServerForm.serverForm.dicWaitHandle[tmpToId].Set();
            }
        }
    }
}
