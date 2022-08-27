namespace Client
{
    partial class ChatForm
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
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.btnSend = new System.Windows.Forms.Button();
            this.richTextBox2 = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(3, 2);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(449, 218);
            this.richTextBox1.TabIndex = 0;
            this.richTextBox1.Text = "";
            // 
            // btnSend
            // 
            this.btnSend.Location = new System.Drawing.Point(390, 321);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(59, 24);
            this.btnSend.TabIndex = 1;
            this.btnSend.Text = "Send";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // richTextBox2
            // 
            this.richTextBox2.Location = new System.Drawing.Point(3, 226);
            this.richTextBox2.Name = "richTextBox2";
            this.richTextBox2.Size = new System.Drawing.Size(446, 89);
            this.richTextBox2.TabIndex = 2;
            this.richTextBox2.Text = "";
            this.richTextBox2.TextChanged += new System.EventHandler(this.richTextBox2_TextChanged);
            this.richTextBox2.KeyDown += new System.Windows.Forms.KeyEventHandler(this.richTextBox2_KeyDown);
            // 
            // ChatForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(458, 357);
            this.Controls.Add(this.richTextBox2);
            this.Controls.Add(this.btnSend);
            this.Controls.Add(this.richTextBox1);
            this.MaximumSize = new System.Drawing.Size(474, 395);
            this.MinimumSize = new System.Drawing.Size(474, 395);
            this.Name = "ChatForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ChatForm_FormClosing);
            this.Load += new System.EventHandler(this.ChatForm_Load);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.RichTextBox richTextBox2;
        public System.Windows.Forms.RichTextBox richTextBox1;
    }
}