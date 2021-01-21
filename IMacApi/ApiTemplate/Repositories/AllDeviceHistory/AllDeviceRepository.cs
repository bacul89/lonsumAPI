using ApiTemplate.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace ApiTemplate.Repositories.AllDeviceHistory
{
    public class AllDeviceRepository : IAllDeviceRepository
    {
        public async Task<IEnumerable<CategoryVM>> getAllCategory()
        {
            using (var context = new BaseContext())
            {
                ((IObjectContextAdapter)context).ObjectContext.CommandTimeout = 360;
                string commandText = "[dbo].[GetALLCategory] ";
                var listModel = context.Database.SqlQuery<CategoryVM>(commandText).ToListAsync();

                return await listModel;

            }
        }

        public async Task<IEnumerable<DetailDeviceVM>> getDetailDeviceBySN(string SerialNumber)
        {
            using (var context = new BaseContext())
            {
                ((IObjectContextAdapter)context).ObjectContext.CommandTimeout = 360;
                string commandText = "[dbo].[GetDeviceDetailBySN] @SerialNumber";
                var SNPost = new SqlParameter("SerialNumber", SerialNumber);
                var listModel = context.Database.SqlQuery<DetailDeviceVM>(commandText, SNPost).ToListAsync();

                return await listModel;

            }
        }

        public async Task<IEnumerable<DeviceListVM>> getListDeviceByCategory(string KategorID)
        {
            using (var context = new BaseContext())
            {
                ((IObjectContextAdapter)context).ObjectContext.CommandTimeout = 360;
                string commandText = "[dbo].[GetDeviceListByKategoriId] @KategoriID";
                var KategoriPost = new SqlParameter("KategoriID", KategorID);
                var listModel = context.Database.SqlQuery<DeviceListVM>(commandText, KategoriPost).ToListAsync();

                return await listModel;

            }
        }

        public async Task<int> getCountPending(string Username, string Role)
        {
            using (var context = new BaseContext())
            {
                ((IObjectContextAdapter)context).ObjectContext.CommandTimeout = 360;
                string commandText = "[dbo].[GetCountPending] @Username, @Role";
                var UsernamePost = new SqlParameter("Username", Username);
                var RolePost = new SqlParameter("Role", Role);
                var model = context.Database.SqlQuery<int>(commandText, UsernamePost, RolePost).FirstOrDefaultAsync();

                return await model;

            }
        }
        public EmailUserParameter getEmail(long IDTRX)
        {
            using (var context = new BaseContext())
            {
                ((IObjectContextAdapter)context).ObjectContext.CommandTimeout = 360;
                string commandText = "[dbo].[GetEmail] @IDTRX";
                var IDTRXPost = new SqlParameter("IDTRX", IDTRX);
                var listModel = context.Database.SqlQuery<EmailUserParameter>(commandText, IDTRXPost).FirstOrDefault();

                return listModel;

            }
        }

        public IEnumerable<EmailUserParameter> getOverdue()
        {
            using (var context = new BaseContext())
            {
                ((IObjectContextAdapter)context).ObjectContext.CommandTimeout = 360;
                string commandText = "[dbo].[GetEngineOverdue] ";
                var listModel = context.Database.SqlQuery<EmailUserParameter>(commandText).ToList();
                return listModel;

            }
        }

        public async Task<IEnumerable<SoftwareHardwareVM>> getSoftHardw(string SerialNumber, string HS)
        {
            using (var context = new BaseContext())
            {
                ((IObjectContextAdapter)context).ObjectContext.CommandTimeout = 360;
                string commandText = "[dbo].[GetSoftwHardw] @SerialNumber, @HS";
                var SerialNumberPost = new SqlParameter("SerialNumber", SerialNumber);
                var HSPost = new SqlParameter("HS", HS);
                var listModel = context.Database.SqlQuery<SoftwareHardwareVM>(commandText, SerialNumberPost, HSPost).ToListAsync();
                return await listModel;

            }
        }
    }
}