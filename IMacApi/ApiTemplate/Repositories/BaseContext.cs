using System.Configuration;
using System.Data.Entity;
using ApiTemplate.Cores.Models;

namespace ApiTemplate.Repositories
{
    public sealed class BaseContext:DbContext
    {
        public BaseContext():base(GetConnectionString())
        {
            Configuration.LazyLoadingEnabled = false;
        }

        private static string GetConnectionString()
        {
            var conn = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            return conn;
        }

        public DbSet<Flag_Done_Trx> Flag_Done_Trx { get; set; }
        public DbSet<Mapping_TaskListDone_MasterTask> Mapping_TaskListDone_MasterTask { get; set; }
        public DbSet<Master_Config_Desc> Master_Config_Desc { get; set; }
        public DbSet<Master_Configuration> Master_Configuration { get; set; }
        public DbSet<Master_Hardware> Master_Hardware { get; set; }
        public DbSet<Master_Job_Type> Master_Job_Type { get; set; }
        public DbSet<Master_PIC> Master_PIC { get; set; }
        public DbSet<Master_Setting_Admin> Master_Setting_Admin { get; set; }
        public DbSet<Master_Setting_User> Master_Setting_User { get; set; }
        public DbSet<Master_Software> Master_Software { get; set; }
        public DbSet<Master_Status_Software> Master_Status_Software { get; set; }
        public DbSet<Master_Task> Master_Task { get; set; }
        public DbSet<Trx_Additional_Software> Trx_Additional_Software { get; set; }
        public DbSet<Trx_All_Remark> Trx_All_Remark { get; set; }
        public DbSet<Trx_Change_Hardware> Trx_Change_Hardware { get; set; }
        public DbSet<Trx_Hardware_Information> Trx_Hardware_Information { get; set; }
        public DbSet<Trx_History_User> Trx_History_User { get; set; }
        public DbSet<Trx_HistorySystem_StdAndAddSoftware> Trx_HistorySystem_StdAndAddSoftware { get; set; }
        public DbSet<Trx_HistorySystemDetailUnitAndOS> Trx_HistorySystemDetailUnitAndOS { get; set; }
        public DbSet<Trx_Install_Std_Software> Trx_Install_Std_Software { get; set; }
        public DbSet<Trx_InstallConfigPraDeployAndOS> Trx_InstallConfigPraDeployAndOS { get; set; }
        public DbSet<Trx_Setting_Admin> Trx_Setting_Admin { get; set; }
        public DbSet<Trx_Setting_User> Trx_Setting_User { get; set; }
        public DbSet<Trx_Standard_Software> Trx_Standard_Software { get; set; }
        public DbSet<Trx_User_Information> Trx_User_Information { get; set; }
        public DbSet<Master_Mapping_Task_JobType> Master_Mapping_Task_JobType { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }

        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<AD_Employee>()
        //        .Property(e => e.LoginUser)
        //        .IsUnicode(false);

        //    modelBuilder.Entity<AD_Employee>()
        //        .Property(e => e.EmplCode)
        //        .IsUnicode(false);

        //    modelBuilder.Entity<AD_Employee>()
        //        .Property(e => e.EmplName)
        //        .IsUnicode(false);

        //    modelBuilder.Entity<AD_Employee>()
        //        .Property(e => e.Title)
        //        .IsUnicode(false);

        //    modelBuilder.Entity<AD_Employee>()
        //        .Property(e => e.Department)
        //        .IsUnicode(false);

        //    modelBuilder.Entity<AD_Employee>()
        //        .Property(e => e.SuperiorID)
        //        .IsUnicode(false);

        //    modelBuilder.Entity<AD_Employee>()
        //        .Property(e => e.Email)
        //        .IsUnicode(false);

        //    modelBuilder.Entity<AD_Employee>()
        //        .Property(e => e.Location)
        //        .IsUnicode(false);

        //    modelBuilder.Entity<AD_Employee>()
        //        .Property(e => e.Manager)
        //        .IsUnicode(false);

        //    modelBuilder.Entity<AD_Employee>()
        //        .Property(e => e.EmpType)
        //        .IsUnicode(false);

        //    modelBuilder.Entity<AD_Employee>()
        //        .Property(e => e.DistinguishedName)
        //        .IsUnicode(false);

        //    modelBuilder.Entity<AD_Employee>()
        //        .Property(e => e.Company)
        //        .IsUnicode(false);

        //    modelBuilder.Entity<AD_Employee>()
        //        .Property(e => e.Gender)
        //        .IsUnicode(false);

        //    modelBuilder.Entity<AD_Employee>()
        //        .Property(e => e.flag_Active)
        //        .IsUnicode(false);
        //}
    }
}