using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiTemplate.Requests
{
    public class UsernameIDTRXRequest
    {
        public Int64 IDTRX { get; set; }
        public string Username { get; set; }
    }
}