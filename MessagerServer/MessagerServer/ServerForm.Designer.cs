namespace MessagerServer
{
    partial class ServerForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.messagelistBox = new System.Windows.Forms.ListBox();
            this.btn_Start = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txBoxPort = new System.Windows.Forms.TextBox();
            this.txBoxMax = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.userlistBox = new System.Windows.Forms.ListBox();
            this.idlistBox = new System.Windows.Forms.ListBox();
            this.labelMessage = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // messagelistBox
            // 
            this.messagelistBox.FormattingEnabled = true;
            this.messagelistBox.ItemHeight = 12;
            this.messagelistBox.Location = new System.Drawing.Point(12, 145);
            this.messagelistBox.Name = "messagelistBox";
            this.messagelistBox.Size = new System.Drawing.Size(377, 244);
            this.messagelistBox.TabIndex = 1;
            // 
            // btn_Start
            // 
            this.btn_Start.Location = new System.Drawing.Point(312, 22);
            this.btn_Start.Name = "btn_Start";
            this.btn_Start.Size = new System.Drawing.Size(77, 25);
            this.btn_Start.TabIndex = 8;
            this.btn_Start.Text = "Start";
            this.btn_Start.UseVisualStyleBackColor = true;
            this.btn_Start.Click += new System.EventHandler(this.btn_Start_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.textBox1);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txBoxPort);
            this.groupBox1.Controls.Add(this.txBoxMax);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(260, 127);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Configuration";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(41, 49);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(101, 12);
            this.label1.TabIndex = 15;
            this.label1.Text = "RegistrationPort";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(157, 49);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(57, 21);
            this.textBox1.TabIndex = 14;
            this.textBox1.Text = "20051";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(41, 22);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 12);
            this.label3.TabIndex = 13;
            this.label3.Text = "MessagerPort";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(41, 79);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(23, 12);
            this.label2.TabIndex = 12;
            this.label2.Text = "Max";
            // 
            // txBoxPort
            // 
            this.txBoxPort.Location = new System.Drawing.Point(157, 22);
            this.txBoxPort.Name = "txBoxPort";
            this.txBoxPort.Size = new System.Drawing.Size(57, 21);
            this.txBoxPort.TabIndex = 10;
            this.txBoxPort.Text = "20050";
            // 
            // txBoxMax
            // 
            this.txBoxMax.Location = new System.Drawing.Point(157, 78);
            this.txBoxMax.Name = "txBoxMax";
            this.txBoxMax.Size = new System.Drawing.Size(59, 21);
            this.txBoxMax.TabIndex = 9;
            this.txBoxMax.Text = "10";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.userlistBox);
            this.groupBox2.Location = new System.Drawing.Point(410, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(231, 383);
            this.groupBox2.TabIndex = 11;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "UserList";
            // 
            // userlistBox
            // 
            this.userlistBox.FormattingEnabled = true;
            this.userlistBox.ItemHeight = 12;
            this.userlistBox.Location = new System.Drawing.Point(6, 23);
            this.userlistBox.Name = "userlistBox";
            this.userlistBox.Size = new System.Drawing.Size(217, 352);
            this.userlistBox.TabIndex = 11;
            this.userlistBox.DoubleClick += new System.EventHandler(this.userlistBox_DoubleClick);
            // 
            // idlistBox
            // 
            this.idlistBox.FormattingEnabled = true;
            this.idlistBox.ItemHeight = 12;
            this.idlistBox.Location = new System.Drawing.Point(312, 90);
            this.idlistBox.Name = "idlistBox";
            this.idlistBox.Size = new System.Drawing.Size(67, 28);
            this.idlistBox.TabIndex = 12;
            // 
            // labelMessage
            // 
            this.labelMessage.AutoSize = true;
            this.labelMessage.Location = new System.Drawing.Point(310, 55);
            this.labelMessage.Name = "labelMessage";
            this.labelMessage.Size = new System.Drawing.Size(11, 12);
            this.labelMessage.TabIndex = 13;
            this.labelMessage.Text = "0";
            // 
            // ServerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(645, 399);
            this.Controls.Add(this.labelMessage);
            this.Controls.Add(this.idlistBox);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btn_Start);
            this.Controls.Add(this.messagelistBox);
            this.MaximumSize = new System.Drawing.Size(661, 437);
            this.MinimumSize = new System.Drawing.Size(661, 437);
            this.Name = "ServerForm";
            this.Text = "Server";
            this.Load += new System.EventHandler(this.ServerForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btn_Start;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txBoxPort;
        public System.Windows.Forms.ListBox messagelistBox;
        public System.Windows.Forms.TextBox txBoxMax;
        private System.Windows.Forms.GroupBox groupBox2;
        public System.Windows.Forms.ListBox userlistBox;
        public System.Windows.Forms.ListBox idlistBox;
        public System.Windows.Forms.Label labelMessage;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
    }
}

