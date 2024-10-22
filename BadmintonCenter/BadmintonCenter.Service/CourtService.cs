﻿using BadmintonCenter.BusinessObject.Models;
using BadmintonCenter.DataAcess.Repository.Interface;
using BadmintonCenter.Service.Interface;

namespace BadmintonCenter.Service
{
    public class CourtService : ICourtService
    {
        private readonly ICourtRepository _courtRepository;

        public CourtService(ICourtRepository courtRepository)
        {
            _courtRepository = courtRepository;
        }

        public async Task AddCourtAsync(Court court)
        {
            await _courtRepository.AddCourtAsync(court);
        }

        public async Task DeleteCourtAsync(Court court)
        {
            await _courtRepository.DeleteCourtAsync(court);
        }

        public async Task<IEnumerable<Court>> GetAllCourts()
        {
            return await _courtRepository.GetAllCourtsAsync();
        }

        public async Task<Court?> GetCourtById(int id)
        {
            return await _courtRepository.GetCourtByIdAsync(id);
        }

        public async Task<List<Court>> GetCourtByName(string name)
        {
            return await _courtRepository.GetCourtByName(name);
        }

        public async Task UpdateCourtAsync(Court court)
        {
            await _courtRepository.UpdateCourtAsync(court);
        }
    }
}
