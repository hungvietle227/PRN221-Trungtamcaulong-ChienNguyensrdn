using BadmintonCenter.BusinessObject.Models;
using BadmintonCenter.DataAcess.Repository.Interface;
using BadmintonCenter.Service.Interface;
using System.Security.Claims;

namespace BadmintonCenter.Service
{
    public class UserPackageService : IUserPackageService
    {
        private readonly IUserPackageRepository _userPackageRepository;
        private readonly IPackageService _packageService;
        private readonly IUserService _userService;

        public UserPackageService(IUserPackageRepository userPackageRepository, IUserService userService, IPackageService packageService)
        {
            _userPackageRepository = userPackageRepository;
            _userService = userService;
            _packageService = packageService;
        }

        public async Task AddUserPackageAsync(UserPackage userPackage)
        {
            await _userPackageRepository.AddUserPackageAsync(userPackage);
        }

        public async Task<IEnumerable<UserPackage>> GetAllUserPackagesAsync()
        {
            return await _userPackageRepository.GetAllUserPackageAsync();
        }

        public async Task<UserPackage> GetUserPackagesByUserIdAndPackageId(int userId, int packageId)
        {
            var userpackage = await _userPackageRepository.GetAllUserPackageAsync();
            var thisuserpackage = userpackage.Where(t => t.UserId == userId && t.PackageId == packageId).FirstOrDefault();
            return thisuserpackage;
        }

        public async Task UpdateUserPackageAsync(UserPackage userPackage)
        {
            var package = await _packageService.GetPackageById(userPackage.PackageId);
            userPackage.HourRemaining += package.HourIncluded;
            userPackage.ValidInMonth = DateTime.Now.Month;
            await _userPackageRepository.UpdateUserPackageAsync(userPackage);
        }
    }
}
