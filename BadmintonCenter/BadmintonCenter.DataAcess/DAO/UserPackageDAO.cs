using BadmintonCenter.BusinessObject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BadmintonCenter.DataAcess.DAO
{
    public interface IUserPackageDAO
    {
        Task<UserPackage?> GetUserPackageByIdAsync(int userPackageId);
        Task<IEnumerable<UserPackage>> GetAllUserPackageAsync();
        Task AddUserPackageAsync(UserPackage userPackage);
        Task UpdateUserPackageAsync(UserPackage userPackage);
        Task DeleteUserPackageAsync(int userPackageId);
    }

    public class UserPackageDAO : IUserPackageDAO
    {
        private readonly BadmintonDbContext _context;

        public UserPackageDAO(BadmintonDbContext context)
        {
            _context = context;
        }

        public async Task AddUserPackageAsync(UserPackage userPackage)
        {
            await _context.UserPackages.AddAsync(userPackage);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteUserPackageAsync(int packageId)
        {
            var userPackage = await _context.UserPackages.FirstOrDefaultAsync(p => p.PackageId == packageId);
            if(userPackage != null)
            {
                _context.UserPackages.Remove(userPackage);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<UserPackage>> GetAllUserPackageAsync()
        {
            return await _context.UserPackages.ToListAsync();
        }

        public async Task<UserPackage?> GetUserPackageByIdAsync(int packageId)
        {
            var userPackage = await _context.UserPackages.FirstOrDefaultAsync(a => a.PackageId == packageId);
            return userPackage;
        }

        public async Task UpdateUserPackageAsync(UserPackage userPackage)
        {
            _context.UserPackages.Update(userPackage);
            await _context.SaveChangesAsync();
        }
    }
}
