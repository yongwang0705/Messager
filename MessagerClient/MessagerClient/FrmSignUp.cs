using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Net.Sockets;
namespace Client
{
    public partial class FrmSignUp : Form
    {
        public FrmSignUp()
        {
            InitializeComponent();
        }

        private void BtnReset_Click(object sender, EventArgs e)
        {
            txtBoxUserName.Text = "";
            txtBoxPwd.Text = "";
        }

        private void BtnSignUp_Click(object sender, EventArgs e)
        {
            TcpSocketCreator tcpSocketCreator = new TcpSocketCreator(20051);
            Socket clientSocket = tcpSocketCreator.CreateTcpSocket();
            try
            {
                clientSocket.Connect(tcpSocketCreator.endPoint);
                if (clientSocket.Connected)
                {
                    Client client = new Client();
                    client.Send(0, clientSocket, "signup|" + txtBoxUserName.Text + "|" + txtBoxPwd.Text);
                    client.id = int.Parse(client.ReadOnce(clientSocket));
                    if (client.id == 0)
                    {
                        MessageBox.Show("Creating account failed");
                    }
                    else
                    {
                        MessageBox.Show("Your account is successfully created, the id is " + client.id);
                        this.Close();
                    }
                }
                else
                {
                    MessageBox.Show("Network failed");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
