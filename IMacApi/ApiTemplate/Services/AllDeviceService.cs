using ApiTemplate.Repositories.AllDeviceHistory;
using ApiTemplate.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace ApiTemplate.Services
{
    public interface IAllDeviceService
    {
        Task<IEnumerable<CategoryVM>> getAllCategory();
        Task<IEnumerable<DeviceListVM>> getListDeviceByCategory(string KategorID);
        Task<IEnumerable<DetailDeviceVM>> getDetailDeviceBySN(string SerialNumber);
        Task<int> getCountPending(string Username, string Role);
        EmailUserParameter getEmail(Int64 IDTRX);
        IEnumerable<EmailUserParameter> getOverdue();
        Task<IEnumerable<SoftwareHardwareVM>> getSoftHardw(string SerialNumber, string HS);
    }
    public class AllDeviceService : IAllDeviceService
    {
        private readonly IAllDeviceRepository _allDeviceRepository;
        public AllDeviceService(IAllDeviceRepository AllDeviceRepository)
        {
            _allDeviceRepository = AllDeviceRepository;
        }

        public async Task<IEnumerable<CategoryVM>> getAllCategory()
        {
            return await _allDeviceRepository.getAllCategory();
        }

        public async Task<int> getCountPending(string Username, string Role)
        {
            return await _allDeviceRepository.getCountPending(Username, Role);
        }

        public async Task<IEnumerable<DetailDeviceVM>> getDetailDeviceBySN(string SerialNumber)
        {
            return await _allDeviceRepository.getDetailDeviceBySN(SerialNumber);
        }

        public async Task<IEnumerable<DeviceListVM>> getListDeviceByCategory(string KategorID)
        {
            return await _allDeviceRepository.getListDeviceByCategory(KategorID);
        }
        public EmailUserParameter getEmail(long IDTRX)
        {
            return _allDeviceRepository.getEmail(IDTRX);
        }

        public IEnumerable<EmailUserParameter> getOverdue()
        {
            return _allDeviceRepository.getOverdue();
        }

        public async Task<IEnumerable<SoftwareHardwareVM>> getSoftHardw(string SerialNumber, string HS)
        {
            return await _allDeviceRepository.getSoftHardw(SerialNumber, HS);
        }
    }
}