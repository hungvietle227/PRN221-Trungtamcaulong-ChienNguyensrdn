using BadmintonCenter.BusinessObject.Models;
using BadmintonCenter.DataAcess.DAO;
using BadmintonCenter.DataAcess.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BadmintonCenter.DataAcess.Repository
{
    public class PackageRepository : IPackageRepository
    {
        private readonly IPackageDAO _packageDAO;

        public PackageRepository(IPackageDAO packageDAO)
        {
            _packageDAO = packageDAO;
        }

        public async Task<Package> GetPackageByIdAsync(int packageId)
        {
            return await _packageDAO.GetPackageByIdAsync(packageId);
        }

        public async Task<List<Package>> GetAllPackagesAsync()
        {
            return await _packageDAO.GetAllPackagesAsync();
        }

        public async Task AddPackageAsync(Package package)
        {
            await _packageDAO.AddPackageAsync(package);
        }

        public async Task UpdatePackageAsync(Package package)
        {
            await _packageDAO.UpdatePackageAsync(package);
        }

        public async Task DeletePackageAsync(Package packageId)
        {
            await _packageDAO.DeletePackageAsync(packageId);
        }
    }
}
