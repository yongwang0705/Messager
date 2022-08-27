using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Sockets;
namespace Client
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void btnLoginIn_Click(object sender, EventArgs e)
        {
            TcpSocketCreator tcpSocketCreator = new TcpSocketCreator(20050);
            Socket clientSocket = tcpSocketCreator.CreateTcpSocket();
            try
            {
                clientSocket.Connect(tcpSocketCreator.endPoint);
            }
            catch
            {
                MessageBox.Show("Network failed");
                return;
            }
            if (clientSocket.Connected)
            {
                try
                {
                    Client client = new Client();
                    client.id = int.Parse(textBox1.Text);
                    client.password = textBox2.Text;
                    client.Send(0, clientSocket, "signin|" + textBox1.Text + "|" + textBox2.Text);
                    client.ReadOnce(clientSocket);
                    if (client.name.Equals("0"))
                    {
                        MessageBox.Show("Please check the id and the password");
                        client = null;
                        clientSocket.Shutdown(SocketShutdown.Both);
                        clientSocket.Close();
                        clientSocket = null;
                        tcpSocketCreator = null;
                        return;
                    }
                    else if (client.name.Equals("1"))
                    {
                        MessageBox.Show("This account is already logged in another place");
                        client = null;
                        clientSocket.Shutdown(SocketShutdown.Both);
                        clientSocket.Close();
                        clientSocket = null;
                        tcpSocketCreator = null;
                        return;
                    }
                    else
                    {
                        FrmUserList frmUserList = new FrmUserList(clientSocket, client);
                        frmUserList.Show();
                        this.Hide();
                    }
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return;
                }
            }
            else
            {
                MessageBox.Show("Socket is closed");
                return;
            }
        }

        private void btnSignUp_Click(object sender, EventArgs e)
        {
            FrmSignUp frmSignUp = new FrmSignUp();
            frmSignUp.Show();
        }
    }
}
