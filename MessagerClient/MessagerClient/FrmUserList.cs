using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using System.Configuration;
namespace Client
{
    public partial class FrmUserList : Form
    {
        public delegate void setShowChartFormInvoke(NoticeForm myform,int index,string name,string message,Socket clientSocket);

        public Dictionary<int, ChatForm> dicChatforms = new Dictionary<int, ChatForm>();
        public Client _client;
        Socket _clientSocket;
        public static FrmUserList frmUserList;
        public void setShowChartForm(NoticeForm noticeFrm, int index, string name, string message, Socket clientSocket)
        {
            if (this.InvokeRequired)
            {
                setShowChartFormInvoke _setShowChartFormInvoke = new setShowChartFormInvoke(setShowChartForm);
                this.Invoke(_setShowChartFormInvoke, new object[] { noticeFrm, index, name, message, clientSocket });
            }
            else
            {
                noticeFrm._index = index;
                noticeFrm._name = name;
                noticeFrm._clientSocket = clientSocket;
                noticeFrm._message = message;
                noticeFrm.Show();
            }
        }

        public FrmUserList(Socket clientSocket, Client client)
        {
            frmUserList = this;
            this._clientSocket = clientSocket;
            this._client = client;

            InitializeComponent();
            frmUserList.Text = this._client.name;
            MonitorThreadsCreator monitorThreadsCreator = new MonitorThreadsCreator(this._client);
            Thread readThread = monitorThreadsCreator.CreateReadThread(this._client);
            readThread.IsBackground = true;
            readThread.Start(this._clientSocket);

            /*Thread heatBeaThread = monitorThreadsCreator.CreateHeatBeatThread(this._client);
            heatBeaThread.IsBackground = true;
            heatBeaThread.Start(this._clientSocket);*/
        }

        private void FrmUserList_Load(object sender, EventArgs e)
        {

        }

        private void listBoxUser_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listBoxUser_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            //create chat form
            //Client newClient = new Client();
            int index;
            string strIndex=null;
            try
            {
                strIndex = listBoxId.Items[listBoxUser.SelectedIndex].ToString();
            }
            catch
            {
                
            }
            if (int.TryParse(strIndex, out index))
            {
                if(dicChatforms.ContainsKey(index))//if the form is opened yet, put it in front 
                {
                    FrmUserList.frmUserList.dicChatforms[index].Activate();
                   /* foreach (KeyValuePair<int, ChatForm> dic in FrmUserList.frmUserList.dicChatforms)
                    {
                        if (index == dic.Key)
                        {
                            dic.Value.Activate();
                        }
                    }*/
                }
                else //
                {
                    ChatForm chatForm = new ChatForm(index, listBoxUser.Items[listBoxUser.SelectedIndex].ToString(),this._clientSocket);
                    dicChatforms.Add(index, chatForm);
                    chatForm.Show();
                }

            }
        }

        private void FrmUserList_FormClosed(object sender, FormClosedEventArgs e)
        {
            
        }

        private void FrmUserList_FormClosing(object sender, FormClosingEventArgs e)
        {
            //this._clientSocket.Shutdown(SocketShutdown.Both);
            this._clientSocket.Close();
            Application.Exit();
        }

        private void listBoxId_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
