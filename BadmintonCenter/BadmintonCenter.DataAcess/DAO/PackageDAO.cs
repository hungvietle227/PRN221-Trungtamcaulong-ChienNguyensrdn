using BadmintonCenter.BusinessObject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BadmintonCenter.DataAcess.DAO
{
    public interface IPackageDAO
    {
        Task<Package?> GetPackageByIdAsync(int PackageId);
        Task<List<Package>> GetAllPackagesAsync();
        Task AddPackageAsync(Package Package);
        Task UpdatePackageAsync(Package Package);
        Task DeletePackageAsync(Package PackageId);
        Task<List<Package>> GetPackageByCondition(string value);
    }
    public class PackageDAO : IPackageDAO
    {
        private readonly BadmintonDbContext _context;

        public PackageDAO(BadmintonDbContext context)
        {
            _context = context;
        }
        public async Task<Package?> GetPackageByIdAsync(int packageId)
        {
            return await _context.Packages.FirstOrDefaultAsync(b => b.PackageId == packageId);
        }
        public async Task<List<Package>> GetAllPackagesAsync()
        {
            return await _context.Packages.ToListAsync();
        }

        public async Task AddPackageAsync(Package package)
        {
            var addedPackage = await _context.Packages.AddAsync(package);
            await _context.SaveChangesAsync();
        }

        public async Task UpdatePackageAsync(Package package)
        {
            _context.Update(package);
            await _context.SaveChangesAsync();
        }

        public async Task DeletePackageAsync(Package packageId)
        {
            _context.Remove(packageId);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Package>> GetPackageByCondition(string value)
        {
            var listPackage = await GetAllPackagesAsync();
            return listPackage.Where(a => a.PackageName.ToLower().Contains(value.ToLower()) || a.HourIncluded.ToString() == value).ToList();
        }
    }
}
