using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MessagingWebApplication.Models
{
    public class Message
    {
        public int SenderId { get; set; }

        public int RecieverId  { get; set; }

        public string MessageContent { get; set; }

        public DateTime? MessageDate { get; set; }
    }
}