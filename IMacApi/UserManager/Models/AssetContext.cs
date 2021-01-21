using System.Configuration;

namespace ITAssetsManagement.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class AssetContext : DbContext
    {
        public AssetContext()
            : base(GetConnectionString())
        {
            Configuration.LazyLoadingEnabled = false;
        }

        public virtual DbSet<Asset_Category> Asset_Category { get; set; }
        public virtual DbSet<RMD_Asset_Header> RMD_Asset_Header { get; set; }

        private static string GetConnectionString()
        {
            var conn = ConfigurationManager.ConnectionStrings["AssetConnectionString"].ConnectionString;
            return conn;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Asset_Category>()
                .Property(e => e.Category_ID)
                .IsUnicode(false);

            modelBuilder.Entity<Asset_Category>()
                .Property(e => e.Remarks)
                .IsUnicode(false);

            modelBuilder.Entity<RMD_Asset_Header>()
                .Property(e => e.Device_Name)
                .IsUnicode(false);

            modelBuilder.Entity<RMD_Asset_Header>()
                .Property(e => e.Serial_Number)
                .IsUnicode(false);

            modelBuilder.Entity<RMD_Asset_Header>()
                .Property(e => e.User_Name)
                .IsUnicode(false);

            modelBuilder.Entity<RMD_Asset_Header>()
                .Property(e => e.Alias)
                .IsUnicode(false);

            modelBuilder.Entity<RMD_Asset_Header>()
                .Property(e => e.Job_Title)
                .IsUnicode(false);

            modelBuilder.Entity<RMD_Asset_Header>()
                .Property(e => e.Job_Function)
                .IsUnicode(false);

            modelBuilder.Entity<RMD_Asset_Header>()
                .Property(e => e.Top_Console_User)
                .IsUnicode(false);

            modelBuilder.Entity<RMD_Asset_Header>()
                .Property(e => e.Category_ID)
                .IsUnicode(false);

            modelBuilder.Entity<RMD_Asset_Header>()
                .Property(e => e.Device_Type)
                .IsUnicode(false);

            modelBuilder.Entity<RMD_Asset_Header>()
                .Property(e => e.EstateMill_ID)
                .IsUnicode(false);

            modelBuilder.Entity<RMD_Asset_Header>()
                .Property(e => e.AMA_ID)
                .IsUnicode(false);

            modelBuilder.Entity<RMD_Asset_Header>()
                .Property(e => e.ProvinceID)
                .IsUnicode(false);

            modelBuilder.Entity<RMD_Asset_Header>()
                .Property(e => e.FA_Number)
                .IsUnicode(false);

            modelBuilder.Entity<RMD_Asset_Header>()
                .Property(e => e.Status_ID)
                .IsUnicode(false);

            modelBuilder.Entity<RMD_Asset_Header>()
                .Property(e => e.SubStatus_ID)
                .IsUnicode(false);

            modelBuilder.Entity<RMD_Asset_Header>()
                .Property(e => e.Keterangan)
                .IsUnicode(false);

            modelBuilder.Entity<RMD_Asset_Header>()
                .Property(e => e.Supporting_Document)
                .IsUnicode(false);

            modelBuilder.Entity<RMD_Asset_Header>()
                .Property(e => e.Class_ID)
                .IsUnicode(false);

            modelBuilder.Entity<RMD_Asset_Header>()
                .Property(e => e.Created_By)
                .IsUnicode(false);

            modelBuilder.Entity<RMD_Asset_Header>()
                .Property(e => e.Modified_By)
                .IsUnicode(false);
        }
    }
}
