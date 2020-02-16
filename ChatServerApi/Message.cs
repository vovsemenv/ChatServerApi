using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatServerApi
{
    public class Message
    {
        public string name { get; set; }
        public string message { get; set; }
        public Message(string message, string username)
        {
            this.message = message;
            this.name = username;
        }
        public Message()
        {
            this.message = "";
            this.name = "";
        }
    }
}
