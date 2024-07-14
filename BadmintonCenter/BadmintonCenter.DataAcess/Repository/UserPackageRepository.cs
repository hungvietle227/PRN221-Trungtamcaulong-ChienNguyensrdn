using BadmintonCenter.BusinessObject.Models;
using BadmintonCenter.DataAcess.DAO;
using BadmintonCenter.DataAcess.Repository.Interface;

namespace BadmintonCenter.DataAcess.Repository
{
    public class UserPackageRepository : IUserPackageRepository
    {
        private readonly IUserPackageDAO _userPackageDAO;

        public UserPackageRepository(IUserPackageDAO userPackageDAO)
        {
            _userPackageDAO = userPackageDAO;
        }

        public async Task AddUserPackageAsync(UserPackage userPackage)
        {
            await _userPackageDAO.AddUserPackageAsync(userPackage);
        }

        public Task DeleteUserPackageAsync(int userPackageId)
        {
            throw new NotImplementedException();
        }

        public async Task<List<UserPackage>> GetAllUserPackageAsync()
        {
            return await _userPackageDAO.GetAllUserPackageAsync();
        }

        public Task<UserPackage> GetUserPackageByIdAsync(int userPackageId)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<UserPackage>> GetUserPackageByUserIdAsync(int userId)
        {
            var packages = await _userPackageDAO.GetAllUserPackageAsync();
            return packages.Where(p => p.UserId == userId).AsEnumerable();
        }

        public async Task UpdateUserPackageAsync(UserPackage userPackage)
        {
            await _userPackageDAO.UpdateUserPackageAsync(userPackage);
        }
    }
}
