using BadmintonCenter.BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BadmintonCenter.DataAcess.Repository.Interface
{
    public interface IUserPackageRepository
    {
        Task<UserPackage> GetUserPackageByIdAsync(int userPackageId);
        Task<UserPackage> GetUserPackageByUserIdAsync(int userId);
        Task<List<UserPackage>> GetAllUserPackageAsync();
        Task AddUserPackageAsync(UserPackage userPackage);
        Task UpdateUserPackageAsync(UserPackage userPackage);
        Task DeleteUserPackageAsync(int userPackageId);
    }
}
