using BadmintonCenter.BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BadmintonCenter.DataAcess.Repository.Interface
{
    public interface ICourtRepository
    {
        Task<Court> GetCourtByIdAsync(int courtId);
        Task<List<Court>> GetAllCourtsAsync();
        Task AddCourtAsync(Court court);
        Task UpdateCourtAsync(Court court);
        Task DeleteCourtAsync(int courtId);

    }
}
