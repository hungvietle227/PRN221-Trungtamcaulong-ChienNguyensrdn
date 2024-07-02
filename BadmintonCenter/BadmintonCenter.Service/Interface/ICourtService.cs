using BadmintonCenter.BusinessObject.Models;

namespace BadmintonCenter.Service.Interface
{
    public interface ICourtService
    {
        Task<IEnumerable<Court>> GetAllCourts();
        Task<Court?> GetCourtById(int id);
        Task AddCourtAsync(Court court);
        Task UpdateCourtAsync(Court court);
        Task DeleteCourtAsync(Court court);
    }
}
