using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiTemplate.Models
{
    public class MailModel
    {
        public MailModel()
        {
            RecipientList = new List<string>();
        }
        //public int PIN { get; set; } = 0;
        public string Subject { get; set; }
        public string Body { get; set; }
        public string Recipient { get; set; }
        public List<string> RecipientList { get; set; }
    }
}