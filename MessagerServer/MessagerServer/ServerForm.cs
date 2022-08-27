using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using System.Configuration;
using TcpSocketOperator;
namespace MessagerServer
{
    /**
     * <summary>
     * The major UI for controling the server and display its status
     * </summary>
     */
    public partial class ServerForm : Form
    {

        public Dictionary<int, Client> dictionaryClient;
        public static ServerForm serverForm;
        public static Thread messageMonitorThread;
        public static Thread databaseMonitorThread;
        public Dictionary<int, Queue<MyMessage>> dicMyMessages;
        public Dictionary<int, EventWaitHandle> dicWaitHandle;
        //public int threadMaxNumber { set; get; }
        //public int threadCurNumber { set; get; }
        public bool _running { set; get; }
        private static Socket messageMonitorSocket { set; get; }
        private static Socket databaseMonitorSocket { set; get; }
        //private MySqlConnection _conn { set; get; }

        public ServerForm()
        {
            InitializeComponent();
            serverForm = this;
            dictionaryClient = new Dictionary<int, Client>();
            dicMyMessages = new Dictionary<int, Queue<MyMessage>>();
            dicWaitHandle = new Dictionary<int, EventWaitHandle>();
        }
        /**
         * <summary>
         * The only button which make server run or stop
         * </summary>
         */
        private void btn_Start_Click(object sender, EventArgs e)
        {
            if (btn_Start.Text.Equals("Start"))
            {
                CreateMonitorThread(int.Parse(txBoxPort.Text), int.Parse(textBox1.Text));
                btn_Start.Text = "Stop";
                //to do connect database
                if (ConnectDatabase())
                {
                    messagelistBox.Items.Add("Connect to database is succeed");
                    messagelistBox.Items.Add("Server is running");
                    return;
                }
                else
                {
                    MessageBox.Show("Connect to database is failed");
                    return;
                }
                
            }
            else
            {
                _running = false;
                DisconnectDatabase();
                DestructThreads();
                userlistBox.Items.Clear();
                idlistBox.Items.Clear();
                messagelistBox.Items.Clear();
                
            }
        }
        /**
         * <summary>
         * This function creates the connection between server and database. 
         * The created connection will be passed to client read method as second parameter
         * </summary>
         */
        bool ConnectDatabase()
        {
            //string connetStr = "Data Source = " + ConfigurationManager.AppSettings["DatabaseServerIp"] + "; Initial Catalog = " + ConfigurationManager.AppSettings["DatebaseName"] + "; Persist Security Info = True; User ID = sa; Password=123;";
            //string connetStr = "server=" + ConfigurationManager.AppSettings["DatabaseServerIp"] + "; port=3306;user=root; database=" + ConfigurationManager.AppSettings["ServerIp"] + ";";
            string connetStr = ConfigurationManager.AppSettings["DbConnString"];
            try
            {
                MDatabaseOperator.ConnectDatabase(connetStr);
                return true;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return false;
            }
        }
        bool DisconnectDatabase()
        {
            if(MDatabaseOperator.DisconnectDatabase())
            {
                return true;
            }
            else
            {
                return false;
            }
            
        }
        /**
         * <summary>
         * The function will 
         * server, destroy all the threads and close sockets(including monitor purpose)
         * </summary>
         */
        void DestructThreads()
        {
            TcpSocketDestructor tcpSocketDestructor = new TcpSocketDestructor();
            if (tcpSocketDestructor.DestructTcpSocket(messageMonitorSocket) == true && tcpSocketDestructor.DestructTcpSocket(databaseMonitorSocket) == true)
            {
                btn_Start.Text = "Start";
                messagelistBox.Items.Add("messageMonitorSocket and databaseMonitorSocket are closed");
            }
            else
            {
                MessageBox.Show("Error occurs when messageMonitorSocket or databaseMonitorSocket is closing");
            }

            if (ThreadsDestructor.DestructThread(messageMonitorThread) == true) 
            {
                messagelistBox.Items.Add("monitorThread is closed");
            }
            else
            {
                MessageBox.Show("Error occurs when monitorThread is stopping");
            }
            if(ThreadsDestructor.DestructThread(databaseMonitorThread) == true)
            {
                messagelistBox.Items.Add("monitorThread is closed");
            }
            else
            {
                MessageBox.Show("Error occurs when monitorThread is stopping");
            }


            foreach (KeyValuePair<int,Client> client in dictionaryClient)
            {
                client.Value._socket.Shutdown(SocketShutdown.Both);
                client.Value._socket.Close();
                client.Value._eventWaitHandle.Set();
                client.Value._eventWaitHandle.Close();
                client.Value._myMessageQueue.Clear();
                messagelistBox.Items.Add("Every client socket are closed");
            }
            dicMyMessages.Clear();
            dictionaryClient.Clear();
            dicWaitHandle.Clear();

            //System.Environment.Exit(0);
            //Destory ClientThread
            //foreach()
            //Destory ClientSocket
            //
        }
         /**
         * <summary>
         * The function will start server, create all the threads and open sockets(including monitor purpose)
         * </summary>
         * <param name="max of connection and listening port"></param>
         */
        void CreateMonitorThread(int port,int port1)
        {
            
            IPAddress ipv4 = IPAddress.Parse(ConfigurationManager.AppSettings["ServerIp"]);
            try
            {
                if (TcpSocketCreator.UPnp(ipv4, port, port, "Messager") && TcpSocketCreator.UPnp(ipv4, port1, port1, "Messager"))
                {
                    MessageBox.Show("The UPnp function is not supported in route");
                }
                else
                {
                    messagelistBox.Items.Add("The UPnp function is activated");
                }
            }
            catch(Exception e)
            {
                messagelistBox.Items.Add("Error UPnp: " + e.Message);
            }
            

            
            TcpSocketCreator tcpSocketCreator = new TcpSocketCreator(port, ipv4);
            messageMonitorSocket = tcpSocketCreator.CreateTcpSocket();
            messageMonitorSocket.Bind(tcpSocketCreator._ipEndPoint);
            messageMonitorSocket.Listen(10);

            TcpSocketCreator tcpSocketCreator1 = new TcpSocketCreator(port1, ipv4);
            databaseMonitorSocket = tcpSocketCreator1.CreateTcpSocket();
            databaseMonitorSocket.Bind(tcpSocketCreator1._ipEndPoint);
            databaseMonitorSocket.Listen(10);

            int.TryParse(this.txBoxMax.Text, out Bussiness._threadMaxNumber);
            Bussiness bussiness = new Bussiness(100000, messageMonitorSocket, databaseMonitorSocket);

            messageMonitorThread = new Thread(bussiness.MessagerExchange);
            messageMonitorThread.IsBackground = true;
            messageMonitorThread.Start();

            databaseMonitorThread = new Thread(bussiness.DatabaseOperationQuery);
            databaseMonitorThread.IsBackground = true;
            databaseMonitorThread.Start();
            /* messageMonitorThreadsCreator = new MonitorThreadsCreator(max, messageMonitorSocket);
             messageMonitorThreadsCreator._callBackEventHandler = messageMonitorThreadsCreator.MessagerExchange;
             messageMonitorThread = messageMonitorThreadsCreator.CreateMonitorThread();
             messageMonitorThread.IsBackground = true;
             messageMonitorThread.Start();

             databaseMonitorThreadsCreator = new MonitorThreadsCreator(max, databaseMonitorSocket);
             messageMonitorThreadsCreator._callBackEventHandler = messageMonitorThreadsCreator.DatabaseOperationQuery;
             databaseMonitorThread = databaseMonitorThreadsCreator.CreateMonitorThread();
             databaseMonitorThread.IsBackground = true;
             databaseMonitorThread.Start();*/
            
            if (messageMonitorThread.IsAlive == true && databaseMonitorThread.IsAlive == true)
            {
                _running = true;
            }
            else
            {
                _running = false;
            }
        }

        private void userlistBox_DoubleClick(object sender, EventArgs e)
        {
            int index;
            if(int.TryParse(idlistBox.Items[userlistBox.SelectedIndex].ToString(), out index)) 
            {
                MyMessage myMessage = new MyMessage(0, index,"","");
                ChatForm chatForm = new ChatForm(myMessage);
                chatForm.Show();
            }  
        }

        private void ServerForm_Load(object sender, EventArgs e)
        {

        }
    }
}
