using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CayCanh.Libraries
{
    public class MessageAlert
    {
        public string Type { get; set; }
        public string Msg { get; set; }
        public MessageAlert() { }
        public MessageAlert(string type, string msg)
        {
            this.Type = type;
            this.Msg = msg;
        }
    }
}