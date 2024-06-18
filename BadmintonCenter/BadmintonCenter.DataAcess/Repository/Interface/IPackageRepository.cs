using BadmintonCenter.BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BadmintonCenter.DataAcess.Repository.Interface
{
    public interface IPackageRepository
    {
        Task<Package> GetPackageByIdAsync(int packageId);
        Task<List<Package>> GetAllPackagesAsync();
        Task AddPackageAsync(Package package);
        Task UpdatePackageAsync(Package package);
        Task DeletePackageAsync(Package package);
    }
}
