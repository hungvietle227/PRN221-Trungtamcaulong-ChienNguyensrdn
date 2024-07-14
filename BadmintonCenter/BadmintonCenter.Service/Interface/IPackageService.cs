using BadmintonCenter.BusinessObject.Models;

namespace BadmintonCenter.Service.Interface
{
    public interface IPackageService
    {
        Task<List<Package>> GetAllPackages();
        Task<Package?> GetPackageById(int id);
        Task AddPackageAsync(Package package);
        Task UpdatePackageAsync(Package package);
        Task DeletePackageAsync(Package package);
        Task UpdateUserPackage(UserPackage package);
    }
}
