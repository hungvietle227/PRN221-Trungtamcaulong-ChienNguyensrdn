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
        Task<Package> GetPackageByIdAsync(int PackageId);
        Task<List<Package>> GetAllPackagesAsync();
        Task AddPackageAsync(Package Package);
        Task UpdatePackageAsync(Package Package);
        Task DeletePackageAsync(Package PackageId);
    }
    public class PackageDAO : IPackageDAO
    {
        private readonly BadmintonDbContext _context;

        public PackageDAO(BadmintonDbContext context)
        {
            _context = context;
        }
        public async Task<Package> GetPackageByIdAsync(int PackageId)
        {
            return _context.Packages.FirstOrDefault(b => b.PackageId == PackageId);
        }
        public async Task<List<Package>> GetAllPackagesAsync()
        {
            return await _context.Packages.ToListAsync();
        }

        public async Task AddPackageAsync(Package Package)
        {
            var addedPackage = await _context.Packages.AddAsync(Package);
            await _context.SaveChangesAsync();
        }

        public async Task UpdatePackageAsync(Package Package)
        {
            _context.Update(Package);
            await _context.SaveChangesAsync();
        }

        public async Task DeletePackageAsync(Package PackageId)
        {
            _context.Remove(PackageId);
            await _context.SaveChangesAsync();
        }
    }
}
