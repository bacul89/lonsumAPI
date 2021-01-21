using ApiTemplate.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiTemplate.Repositories.AllDeviceHistory
{
    public interface IAllDeviceRepository
    {
        Task<IEnumerable<CategoryVM>> getAllCategory();
        Task<IEnumerable<DeviceListVM>> getListDeviceByCategory(string KategorID);
        Task<IEnumerable<DetailDeviceVM>> getDetailDeviceBySN(string SerialNumber);
        Task<int> getCountPending(string Username, string Role);
        EmailUserParameter getEmail(Int64 IDTRX);
        IEnumerable<EmailUserParameter> getOverdue();
        Task<IEnumerable<SoftwareHardwareVM>> getSoftHardw(string SerialNumber, string HS);
    }
}
