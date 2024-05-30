using BadmintonCenter.DataAcess.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BadmintonCenter.DataAcess.DAO;
using BadmintonCenter.BusinessObject.Models;

namespace BadmintonCenter.DataAcess.Repository
{
    public class CourtRepo : ICourtRepository
    {
        private readonly ICourtDAO _courtDAO;
         
        public CourtRepo(ICourtDAO courtDAO)
        {
            _courtDAO = courtDAO;
        }

        public async Task<Court> GetCourtByIdAsync(int courtId)
        {
            return await _courtDAO.GetCourtByIdAsync(courtId);
        }

        public async Task<List<Court>> GetAllCourtsAsync()
        {
            return await _courtDAO.GetAllCourtsAsync();
        }

        public async Task AddCourtAsync(Court court)
        {
            await _courtDAO.AddCourtAsync(court);
        }

        public async Task UpdateCourtAsync(Court court)
        {
            await _courtDAO.UpdateCourtAsync(court);
        }

        public async Task DeleteCourtAsync(int courtId)
        {
            await _courtDAO.DeleteCourtAsync(courtId);
        }
    }
}
