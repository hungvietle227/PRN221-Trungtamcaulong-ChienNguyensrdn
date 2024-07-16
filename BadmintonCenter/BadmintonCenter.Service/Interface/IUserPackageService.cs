using BadmintonCenter.BusinessObject.Models;

namespace BadmintonCenter.Service.Interface
{
    public interface IUserPackageService
    {
        Task<IEnumerable<UserPackage>> GetAllUserPackagesAsync();
        Task AddUserPackageAsync(UserPackage userPackage);
        Task UpdateUserPackageAsync(UserPackage userPackage);
        Task<IEnumerable<UserPackage>> GetUserPackagesByUserIdAndPackageId(int userId, int packageId);
    }
}
