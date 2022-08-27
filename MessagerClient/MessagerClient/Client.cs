using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Threading;
using UIOperator;
using System.Windows.Forms;
namespace Client
{

    enum Gender
    {
        male,
        female,
    }
    public class Client
    {
        static object _lock = new object();
        public int id { set; get; }
        public string name { set; get; }
        public string password { set; get; }
        public string ipAddress { set; get; }
        public void Send(int targetid, Socket clientSocket, string message)
        {
            string strContent;
            strContent = this.id.ToString() + "|" + targetid + "|" + message + "|" + DateTime.Now+ "$";
            try
            {
                clientSocket.Send(Encoding.Default.GetBytes(strContent));
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message+"\n Network connection failed");
            }
        }
        public void Read(object clientObect)
        {
            int receiveNumber;
            Socket clientSocket = clientObect as Socket;
            Byte[] buffer = new Byte[100000];
            while (true)
            {
                try
                {
                    receiveNumber = clientSocket.Receive(buffer, 0, clientSocket.Available, SocketFlags.None);
                }
                catch (Exception ex)
                {
                    return;
                }
                string strContent = Encoding.Default.GetString(buffer, 0, receiveNumber);

                string[] arrM = strContent.Split('$');
                for (int j = 0; j < arrM.Length; j++)
                {
                    string[] arr = arrM[j].Split('|');
                    if (arr.Length < 4)
                        continue;
                    if (arr[0].Equals("0")) // from server
                    {
                        if (arr[2].Equals("Exception"))
                        {
                            // FrmUserList.
                        }
                        else if (arr[2].Contains("userlist"))
                        {
                            UIOperatorClass.UpdateListbox(FrmUserList.frmUserList.listBoxId, "", Operate.Clean);
                            UIOperatorClass.UpdateListbox(FrmUserList.frmUserList.listBoxUser, "", Operate.Clean);
                            for (int i = 3; i <= arr.Length - 2; i = i + 2)
                            {
                                UIOperatorClass.UpdateListbox(FrmUserList.frmUserList.listBoxUser, arr[i], Operate.Add);
                                UIOperatorClass.UpdateListbox(FrmUserList.frmUserList.listBoxId, arr[i + 1], Operate.Add);
                            }
                        }
                        else if(arr[2].Contains("ResponseUserName"))
                        {
                            this.name = arr[3].ToString();                        
                        }
                    }
                    else //from other users
                    {
                        int index;
                        if (int.TryParse(arr[0], out index) == false)
                            continue;
                        //examine whether the form is opened
                        //if (FrmUserList.frmUserList.dicChatforms[index] == null || FrmUserList.frmUserList.dicChatforms[index].IsDisposed)
                        if (FrmUserList.frmUserList.dicChatforms.ContainsKey(index))
                        {
                            UIOperatorClass.UpdateForm(FrmUserList.frmUserList.dicChatforms[index], "", Operate.Activate);
                            UIOperatorClass.UpdateTextBox(FrmUserList.frmUserList.dicChatforms[index].richTextBox1, FrmUserList.frmUserList.dicChatforms[index].Text + " " + arr[3].ToString() + "\r\n" + arr[2].ToString() + "\r\n\r\n", Operate.Update);
                            //FrmUserList.frmUserList.dicChatforms[index].Activate();
                        }
                        else
                        {
                            try
                            {
                                string name = FrmUserList.frmUserList.listBoxUser.Items[FrmUserList.frmUserList.listBoxId.Items.IndexOf(index.ToString())].ToString();
                            }
                            catch
                            {
                                continue;
                            }
                            string message =name+" "+ arr[3].ToString() + "\r\n" + arr[2].ToString()+"\r\n\r\n";
                            NoticeForm noticeFrm = new NoticeForm();
                            FrmUserList.frmUserList.setShowChartForm(noticeFrm,index,name,message,clientSocket);
                        }
                        //UIOperatorClass.UpdateTextBox(FrmUserList.frmUserList.dicChatforms[index].richTextBox1, FrmUserList.frmUserList.dicChatforms[index].Text + " " + arr[3].ToString() + "\r\n" + arr[2].ToString(), Operate.Update);
                        /* foreach (KeyValuePair<int, ChatForm> dic in FrmUserList.frmUserList.dicChatforms)
                         {
                             if (index == dic.Key)
                             {
                                 //display the message to the chat form
                                 dic.Value.Show();
                                 dic.Value.richTextBox1.Text = arr[3].ToString() + arr[4].ToString();
                             }
                         }*/
                    }
                }
            }
        }
        public string ReadOnce(Socket clientSocket)
        {
            int receiveNumber=0;
            //Socket clientSocket = clientObect as Socket;
            Byte[] buffer = new Byte[1024];
            try
            {
                while (receiveNumber == 0)
                {
                    receiveNumber = clientSocket.Receive(buffer, 0, clientSocket.Available, SocketFlags.None);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            string strContent = Encoding.Default.GetString(buffer, 0, receiveNumber);
            string[] arrM = strContent.Split('$');
            for (int j = 0; j < arrM.Length; j++)
            {
                string[] arr = arrM[j].Split('|');
                if (arr.Length < 5)
                    continue;
                if (arr[2].Equals("Response"))
                {
                    return arr[3];// return new id for registration
                }
                else if (arr[2].Equals("ResponseUserName"))
                {
                    this.name = arr[3];//return username 
                   /* if (this.name != "0")
                    {
                        FrmUserList frmUserList = new FrmUserList(clientSocket, this);
                        frmUserList.Show();
                    }
                    else
                    {
                        return "";
                    }*/
                }
                else if (arr[2].Contains("userlist"))
                {
                    MessageBox.Show(arr[3]+","+arr[4]);
                    UIOperatorClass.UpdateListbox(FrmUserList.frmUserList.listBoxId, "", Operate.Clean);
                    UIOperatorClass.UpdateListbox(FrmUserList.frmUserList.listBoxUser, "", Operate.Clean);
                    for (int i = 3; i <= arr.Length - 2; i = i + 2)
                    {
                        UIOperatorClass.UpdateListbox(FrmUserList.frmUserList.listBoxUser, arr[i], Operate.Add);
                        UIOperatorClass.UpdateListbox(FrmUserList.frmUserList.listBoxId, arr[i + 1], Operate.Add);
                    }
                }
            }
            return "";
        }
    }
}
