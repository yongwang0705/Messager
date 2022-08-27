namespace Client
{
    partial class FrmUserList
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
            this.listBoxUser = new System.Windows.Forms.ListBox();
            this.listBoxId = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // listBoxUser
            // 
            this.listBoxUser.FormattingEnabled = true;
            this.listBoxUser.ItemHeight = 12;
            this.listBoxUser.Location = new System.Drawing.Point(12, 12);
            this.listBoxUser.Name = "listBoxUser";
            this.listBoxUser.Size = new System.Drawing.Size(204, 376);
            this.listBoxUser.TabIndex = 0;
            this.listBoxUser.SelectedIndexChanged += new System.EventHandler(this.listBoxUser_SelectedIndexChanged);
            this.listBoxUser.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listBoxUser_MouseDoubleClick);
            // 
            // listBoxId
            // 
            this.listBoxId.FormattingEnabled = true;
            this.listBoxId.ItemHeight = 12;
            this.listBoxId.Location = new System.Drawing.Point(12, 145);
            this.listBoxId.Name = "listBoxId";
            this.listBoxId.Size = new System.Drawing.Size(204, 160);
            this.listBoxId.TabIndex = 1;
            this.listBoxId.Visible = false;
            this.listBoxId.SelectedIndexChanged += new System.EventHandler(this.listBoxId_SelectedIndexChanged);
            // 
            // FrmUserList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(228, 396);
            this.Controls.Add(this.listBoxId);
            this.Controls.Add(this.listBoxUser);
            this.MaximumSize = new System.Drawing.Size(244, 434);
            this.MinimumSize = new System.Drawing.Size(244, 434);
            this.Name = "FrmUserList";
            this.Text = "FrmUserList";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmUserList_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmUserList_FormClosed);
            this.Load += new System.EventHandler(this.FrmUserList_Load);
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.ListBox listBoxUser;
        public System.Windows.Forms.ListBox listBoxId;
    }
}