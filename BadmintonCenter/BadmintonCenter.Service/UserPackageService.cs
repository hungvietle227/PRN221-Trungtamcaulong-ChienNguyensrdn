using BadmintonCenter.BusinessObject.Models;
using BadmintonCenter.DataAcess.Repository.Interface;
using BadmintonCenter.Service.Interface;

namespace BadmintonCenter.Service
{
    public class UserPackageService : IUserPackageService
    {
        private readonly IUserPackageRepository _userPackageRepository;

        public UserPackageService(IUserPackageRepository userPackageRepository)
        {
            _userPackageRepository = userPackageRepository;
        }

        public async Task<IEnumerable<UserPackage>> GetAllUserPackagesAsync()
        {
            return await _userPackageRepository.GetAllUserPackageAsync();
        }
    }
}
