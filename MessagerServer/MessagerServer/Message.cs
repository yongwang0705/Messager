using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessagerServer
{
    public class MyMessage
    {
        public int _fromClientId { set; get; } 
        public int _toClientId { set; get; }
        public string _content { set; get; }
        public string _time { set; get; }

        public MyMessage(int fromClientId,int toClientId, string content,string time)
        {
            this._fromClientId = fromClientId;
            this._toClientId = toClientId;
            this._content = content;
            this._time = time;
        }
    }
}
