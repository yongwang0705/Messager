using System;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Net.Sockets;
namespace Client
{
    public partial class NoticeForm : Form
    {
        private int currentX;//横坐标
        private int currentY;//纵坐标
        private int screenHeight;//屏幕高度
        private int screenWidth;//屏幕宽度
        int AW_ACTIVE = 0x20000; //激活窗口，在使用了AW_HIDE标志后不要使用这个标志
        int AW_HIDE = 0x10000;//隐藏窗口
        int AW_BLEND = 0x80000;// 使用淡入淡出效果
        int AW_SLIDE = 0x40000;//使用滑动类型动画效果，默认为滚动动画类型，当使用AW_CENTER标志时，这个标志就被忽略
        int AW_CENTER = 0x0010;//若使用了AW_HIDE标志，则使窗口向内重叠；否则向外扩展
        int AW_HOR_POSITIVE = 0x0001;//自左向右显示窗口，该标志可以在滚动动画和滑动动画中使用。使用AW_CENTER标志时忽略该标志
        int AW_HOR_NEGATIVE = 0x0002;//自右向左显示窗口，该标志可以在滚动动画和滑动动画中使用。使用AW_CENTER标志时忽略该标志
        int AW_VER_POSITIVE = 0x0004;//自顶向下显示窗口，该标志可以在滚动动画和滑动动画中使用。使用AW_CENTER标志时忽略该标志
        int AW_VER_NEGATIVE = 0x0008;//自下向上显示窗口，该标志可以在滚动动画和滑动动画中使用。使用AW_CENTER标志时忽略该标志

        public int _index;
        public string _name;
        public Socket _clientSocket;
        public string _message;
        [System.Runtime.InteropServices.DllImportAttribute("user32.dll")]
        private static extern bool AnimateWindow(IntPtr hwnd, int dateTime, int dwFlags);
        public NoticeForm()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            this.TopMost = true;
            this.ShowInTaskbar = false;
        }

        private void NoticeForm_Load(object sender, EventArgs e)
        {
            Rectangle rect = Screen.PrimaryScreen.WorkingArea;
            screenHeight = rect.Height;
            screenWidth = rect.Width;
            currentX = screenWidth - this.Width;
            currentY = screenHeight - this.Height;
            this.Location = new System.Drawing.Point(currentX, currentY);
            this.label1.Text = "A message from " + this._name;
            AnimateWindow(this.Handle, 1000, AW_SLIDE | AW_VER_NEGATIVE);
        }

        private void NoticeForm_Click(object sender, EventArgs e)
        {
            if (FrmUserList.frmUserList.dicChatforms.ContainsKey(_index))
            {
                FrmUserList.frmUserList.dicChatforms[_index].richTextBox1.Text += this._message;
            }
            else
            {
                ChatForm chatForm = new ChatForm(_index, _name, _clientSocket);
                FrmUserList.frmUserList.dicChatforms.Add(_index, chatForm);
                FrmUserList.frmUserList.dicChatforms[_index].richTextBox1.Text = this._message;
                chatForm.Show();
            }
            this.Close();
        }

        private void NoticeForm_Shown(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {
            if (FrmUserList.frmUserList.dicChatforms.ContainsKey(_index))
            {
                FrmUserList.frmUserList.dicChatforms[_index].richTextBox1.Text += this._message;
            }
            else
            {
                ChatForm chatForm = new ChatForm(_index, _name, _clientSocket);
                FrmUserList.frmUserList.dicChatforms.Add(_index, chatForm);
                FrmUserList.frmUserList.dicChatforms[_index].richTextBox1.Text = this._message;
                
                chatForm.Show();
            }
            this.Close();
        }
    }
}
