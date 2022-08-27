using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Threading;
namespace MessagerServer
{
    class ThreadsDestructor
    {
        public static bool DestructThread(Thread thread)
        {
            try
            {
                thread.Join();
            }
            catch(Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            if (thread.IsAlive == false)
            {
                return true;
            }
            else
            {
                return false;
            }
            
        }
    }
}
