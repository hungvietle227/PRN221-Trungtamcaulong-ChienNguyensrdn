using BadmintonCenter.BusinessObject.Models;

namespace BadmintonCenter.Service.Interface
{
    public interface IUserPackageService
    {
        Task<IEnumerable<UserPackage>> GetAllUserPackagesAsync();
    }
}
