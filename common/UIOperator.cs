using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace MessagerServer
{
    public enum Operate
    {
        Add,
        Delete
    };
    class UIOperator
    {
        private delegate void SetListBoxCallBack(ListBox listbox, string strValue, Operate operate);
        private static SetListBoxCallBack setListBoxCallBack;

        private static void SetListboxValue(ListBox listbox, string str, Operate operate)
        {
            if (operate==Operate.Add)
            {
                listbox.Items.Add(str);
            }
            if (operate == Operate.Delete)
            {
                listbox.Items.Remove(str);
            }
        }
        public static void UpdateListbox(ListBox listbox, string str, Operate operate)
        {
            if (listbox.InvokeRequired) 
            { 
                setListBoxCallBack = new SetListBoxCallBack(SetListboxValue);
                listbox.Invoke(setListBoxCallBack, listbox, str, operate);
            }
        }
        /*
        private delegate void SetIdListBoxCallBack(ListBox listbox, string strValue, Operate operate);
        private static SetIdListBoxCallBack setIdListBoxCallBack;
        private delegate void SetUserListBoxCallBack(string strValue, Operate operate);
        private static SetUserListBoxCallBack setUserListBoxCallBack;
        private static void setUserListboxValue(string Id, Operate operate)
        {
            if (operate == Operate.Add)
            {
                ServerForm.serverForm.userlistBox.Items.Add(Id);
            }
            if (operate == Operate.Delete)
            {
                ServerForm.serverForm.userlistBox.Items.Remove(Id);
            }
        }
        private static void setMsgListboxValue(string Id, Operate operate)
        {
            if (operate == Operate.Add)
            {
                ServerForm.serverForm.messagelistBox.Items.Add(Id);
            }
            if (operate == Operate.Delete)
            {
                ServerForm.serverForm.messagelistBox.Items.Remove(Id);
            }
        }
        public static void UpdateMsgListbox(string Id, Operate operate)
        {
            setMessageListBoxCallBack = new SetMessageListBoxCallBack(setMsgListboxValue);
            ServerForm.serverForm.messagelistBox.Invoke(setMessageListBoxCallBack, Id, operate);
        }
        public static void UpdateUserListbox(string Id, Operate operate)
        {
            setUserListBoxCallBack = new SetUserListBoxCallBack(setUserListboxValue);
            ServerForm.serverForm.userlistBox.Invoke(setUserListBoxCallBack, Id, operate);
        }
        */
    }
}
