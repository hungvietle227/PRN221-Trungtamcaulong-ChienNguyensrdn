using BadmintonCenter.BusinessObject.Models;
using BadmintonCenter.DataAcess.Repository.Interface;
using BadmintonCenter.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BadmintonCenter.Service
{
    public class PackageService : IPackageService
    {
        private readonly IPackageRepository _packageRepository;
        private readonly IUserPackageRepository _userPackageRepository;

        public PackageService(IPackageRepository packageRepository, IUserPackageRepository userPackageRepository)
        {
            _packageRepository = packageRepository;
            _userPackageRepository = userPackageRepository;
        }

        public async Task AddPackageAsync(Package package)
        {
            await _packageRepository.AddPackageAsync(package);
        }

        public async Task DeletePackageAsync(Package package)
        {
            await _packageRepository.DeletePackageAsync(package);
        }

        public async Task<List<Package>> GetAllPackages()
        {
            return await _packageRepository.GetAllPackagesAsync();
        }

        public async Task<Package?> GetPackageById(int id)
        {
            return await _packageRepository.GetPackageByIdAsync(id);
        }

        public async Task UpdatePackageAsync(Package package)
        {
            await _packageRepository.UpdatePackageAsync(package);
        }

        public async Task UpdateUserPackage(UserPackage package)
        {
            await _userPackageRepository.UpdateUserPackageAsync(package);
        }
    }
}
