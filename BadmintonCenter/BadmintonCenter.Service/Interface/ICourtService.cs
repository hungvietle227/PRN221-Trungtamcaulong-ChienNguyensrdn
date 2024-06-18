using BadmintonCenter.BusinessObject.Models;

namespace BadmintonCenter.Service.Interface
{
    public interface ICourtService
    {
        Task<IEnumerable<Court>> GetAllCourts();
        Task<Court?> GetCourtById(int id);
    }
}
