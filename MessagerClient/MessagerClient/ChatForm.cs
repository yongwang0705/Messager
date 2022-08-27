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
namespace Client
{
    public partial class ChatForm : Form
    {
        static object _lock = new object();
        Client _client;
        public Client client { set { _client = client; } get { return _client; } }
        int _id;
        string _name;
        Socket _socket;
        //string _userName { set; get; }
        //int _id { set; get; }
        public ChatForm(int id, string name, Socket socket)
        {
            this._name = name;
            this._id = id;
            this._socket = socket;
            this._client = FrmUserList.frmUserList._client;
            InitializeComponent();
            this.Text = this._name;
        }

        private void ChatForm_Load(object sender, EventArgs e)
        {
            
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            for(int i=0;i<=1000;i++)
            this._client.Send(this._id, this._socket, richTextBox2.Text);
            string strInput= FrmUserList.frmUserList.Text + " " + DateTime.Now + "\r\n" + richTextBox2.Text + "\r\n";
            display(richTextBox1, strInput, Color.Blue, Color.White);
            richTextBox2.Text = "";
            //richTextBox1.Text = this.;
        }
        public void display(RichTextBox rtBox, string strInput, Color fontColor, Color backColor)
        {
            rtBox.AppendText(strInput);
            int p1 = rtBox.Text.IndexOf(strInput);
            int p2 = strInput.Length;
            rtBox.ForeColor = fontColor;
            rtBox.Select(rtBox.Text.Length - p2 + 2, p2);
            rtBox.SelectionColor = fontColor;
            rtBox.SelectionBackColor = backColor;
        }

        private void ChatForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            FrmUserList.frmUserList.dicChatforms.Remove(this._id);
        }

        private void richTextBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void richTextBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)//如果输入的是回车键
            {
                this.btnSend_Click(sender, e);//触发button事件
                richTextBox2.Text = "";
            }
        }
    }
}
