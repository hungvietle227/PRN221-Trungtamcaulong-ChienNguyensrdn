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

        public PackageService(IPackageRepository packageRepository)
        {
            _packageRepository = packageRepository;
        }
        public async Task<List<Package>> GetAllPackages()
        {
            return await _packageRepository.GetAllPackagesAsync();
        }

        public async Task<Package?> GetPackageById(int id)
        {
            return await _packageRepository.GetPackageByIdAsync(id);
        }
    }
}
