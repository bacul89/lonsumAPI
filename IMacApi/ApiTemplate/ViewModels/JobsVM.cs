using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiTemplate.ViewModels
{
    public class JobsVM
    {
        public Int64 IDTrans { get; set; }
        public string NamaKaryawan { get; set; }
        public DateTime? TanggalTransaksi { get; set; }
        public string SerialNumber { get; set; }
        public string TipeJob { get; set; }
        public string Kategori { get; set; }
        public string IDKategori { get; set; }
        public string NamaDevice { get; set; }
        public string TipeDevice { get; set; }
        public string RequestNo { get; set; }
        public Int64 IDJob { get; set; }
        public Int64 TaskDone { get; set; }
        public string SNBaru { get; set; }
    }
}