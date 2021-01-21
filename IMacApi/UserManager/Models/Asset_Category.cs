using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ITAssetsManagement.Models
{
    

    public partial class Asset_Category
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PKID { get; set; }

        [Key]
        [StringLength(5)]
        public string Category_ID { get; set; }

        [StringLength(200)]
        public string Remarks { get; set; }
    }
}
