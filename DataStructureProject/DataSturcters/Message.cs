using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures
{
    public class Message
    {
        public string MessageData { get; set; }
        public DateTime DateSent { get; set; }
        public string Name { get; set; }
        public Message(string message, string name)
        {
            MessageData = message;
            Name = name;
            DateSent = DateTime.Now;
        }
    }
}
