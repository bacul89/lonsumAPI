using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiTemplate.ViewModels
{
    public class SoftwareHardwareVM
    {
        public string Name { get; set; }
        public string Actual { get; set; }
        public string Version { get; set; }
        public DateTime? DATECREATED { get; set; }
    }
}