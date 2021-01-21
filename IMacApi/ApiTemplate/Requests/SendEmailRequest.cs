using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiTemplate.Requests
{
    public class SendEmailRequest
    {
        public string userlogin { get; set; }
        public string recipient { get; set; }
        public string subject { get; set; }
        public string content { get; set; }
    }
}