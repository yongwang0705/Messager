using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace UIOperator
{
    public enum Operate
    {
        Add,
        Delete,
        Clean,
        Show,
        Hide,
        Update,
        Activate,
        Checkin,
        Checkout
    };
    public class UIOperatorClass
    {
        private delegate void SetListBoxCallBack(ListBox listbox, string strValue, Operate operate);
        private static SetListBoxCallBack setListBoxCallBack;

        private delegate void SetLabelCallBack(Label label, string strValue);
        private static SetLabelCallBack setLabelCallBack;

        private delegate void SetFormCallBack(Form form, string strValue, Operate operate);
        private static SetFormCallBack setFormCallBack;

        private delegate void SetTextBoxCallBack(RichTextBox textBox, string strValue, Operate operate);
        private static SetTextBoxCallBack setTextBoxCallBack;

        private delegate void SetCheckBoxCallBack(CheckBox checkBox, Operate operate);
       // private static SetCheckBoxCallBack setCheckBoxCallBack;
        private static void SetCheckBox(CheckBox checkBox,Operate operate)
        {
            if (operate == Operate.Checkin)
                checkBox.Checked = true;
            if (operate == Operate.Checkout)
                checkBox.Checked = false;
        }
        public static void UpdateCheckBox(CheckBox checkBox, Operate operate)
        {
            if(checkBox!=null)
            {
                if(checkBox.InvokeRequired)
                {
                    SetCheckBoxCallBack setCheckBoxCallBack = new SetCheckBoxCallBack(SetCheckBox);
                    checkBox.Invoke(setCheckBoxCallBack, checkBox, operate);
                }
            }
        }
        private static void SetListboxValue(ListBox listbox, string str, Operate operate)
        {
            switch (operate)
            {
                case Operate.Add:
                    {
                        listbox.Items.Add(str);
                        break;
                    }
                case Operate.Delete:
                    {
                        listbox.Items.Remove(str);
                        break;
                    }
                case Operate.Clean:
                    {
                        listbox.Items.Clear();
                        break;
                    }
            }
        }
        public static void UpdateListbox(ListBox listbox, string str, Operate operate)
        {
            if (listbox != null) 
            { 
                if (listbox.InvokeRequired)
                {
                    setListBoxCallBack = new SetListBoxCallBack(SetListboxValue);
                    listbox.Invoke(setListBoxCallBack, listbox, str, operate);
                }
            }
        }

        private static void SetLabelValue(Label label, string str)
        {
            label.Text = str;
        }
        public static void UpdateLabel(Label label, string str)
        {
            if (label.InvokeRequired)
            {
                setLabelCallBack = new SetLabelCallBack(SetLabelValue);
                label.Invoke(setLabelCallBack, label, str);
            }
        }

        private static void SetFormValue(Form form, string str, Operate operate)
        {
            switch(operate)
            {
                case Operate.Hide:
                    {
                        form.Hide();
                        break;
                    }
                case Operate.Show:
                    {
                        form.Show();
                        break;
                    }
                case Operate.Activate:
                    {
                        form.Activate();
                        break;
                    }
            }
        }
        public static void UpdateForm(Form form, string str, Operate operate)
        {
            if (form.InvokeRequired)
            {
                setFormCallBack = new SetFormCallBack(SetFormValue);
                form.Invoke(setFormCallBack, form, str, operate);
            }
        }

        private static void SetTextBoxValue(RichTextBox textBox, string str, Operate operate)
        {
            switch (operate)
            {
                case Operate.Update:
                    {
                        textBox.Text= textBox.Text+"\r\n"+str;
                        break;
                    }
            }
        }
        public static void UpdateTextBox(RichTextBox textBox, string str, Operate operate)
        {
            if (textBox.InvokeRequired)
            {
                setTextBoxCallBack = new SetTextBoxCallBack(SetTextBoxValue);
                textBox.Invoke(setTextBoxCallBack, textBox, str, operate);
            }
        }
    }
}
