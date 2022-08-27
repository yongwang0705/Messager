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
namespace MessagerServer
{
    public partial class ChatForm : Form
    {
        static object _lock = new object();
        MyMessage _myMessage { set; get; }

        public ChatForm(MyMessage message)
        {
            InitializeComponent();
            _myMessage = message;
        }

        private void ChatForm_Load(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _myMessage._content = richTextBox2.Text;
            _myMessage._time = DateTime.Now.ToString();
            lock (_lock)
            {
                //ServerForm.serverForm.myMessageQueue.Add(_myMessage);
            }
            
        }
    }
}
