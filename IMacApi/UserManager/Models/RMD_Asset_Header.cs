using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ITAssetsManagement.Models
{

    public partial class RMD_Asset_Header
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PKID { get; set; }

        [StringLength(50)]
        public string Device_Name { get; set; }

        [Key]
        [StringLength(100)]
        public string Serial_Number { get; set; }

        [StringLength(200)]
        public string User_Name { get; set; }

        [StringLength(20)]
        public string Alias { get; set; }

        [StringLength(60)]
        public string Job_Title { get; set; }

        public string Job_Function { get; set; }

        [StringLength(50)]
        public string Top_Console_User { get; set; }

        [StringLength(5)]
        public string Category_ID { get; set; }

        [StringLength(50)]
        public string Device_Type { get; set; }

        [StringLength(10)]
        public string EstateMill_ID { get; set; }

        [StringLength(10)]
        public string AMA_ID { get; set; }

        [StringLength(8)]
        public string ProvinceID { get; set; }

        public bool? Manage_Service { get; set; }

        public bool? SCCMclient { get; set; }

        public DateTime? Acquire_Date { get; set; }

        public DateTime? Deployment_Date { get; set; }

        [StringLength(15)]
        public string FA_Number { get; set; }

        [StringLength(5)]
        public string Status_ID { get; set; }

        [StringLength(5)]
        public string SubStatus_ID { get; set; }

        public string Keterangan { get; set; }

        [StringLength(50)]
        public string Supporting_Document { get; set; }

        [StringLength(50)]
        public string Class_ID { get; set; }

        public bool? RMD_Mark_As_Deleted { get; set; }

        public DateTime? Created_Date { get; set; }

        [StringLength(20)]
        public string Created_By { get; set; }

        public DateTime? Modified_Date { get; set; }

        [StringLength(20)]
        public string Modified_By { get; set; }
    }
}
