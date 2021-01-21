using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiTemplate.ViewModels
{
    public class TaskVM
    {
        public Int64 IDTask { get; set; }
        public string NamaTask { get; set; }
        public DateTime? Ekspetasi { get; set; }
        public string RemarkJob { get; set; }
        public string NamaPIC { get; set; }
    }
}