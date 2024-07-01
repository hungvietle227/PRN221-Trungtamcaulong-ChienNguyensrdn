using BadmintonCenter.BusinessObject.Models;

namespace BadmintonCenter.DataAcess.Repository.Interface
{
    public interface ICourtRepository
    {
        Task<Court?> GetCourtByIdAsync(int courtId);
        Task<List<Court>> GetAllCourtsAsync();
        Task AddCourtAsync(Court court);
        Task UpdateCourtAsync(Court court);
        Task DeleteCourtAsync(Court court);
    }
}
