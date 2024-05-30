using BadmintonCenter.BusinessObject.Models;
using BadmintonCenter.DataAcess.DAO;
using BadmintonCenter.DataAcess.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BadmintonCenter.DataAcess.Repository
{
    public class RoleRepository : IRoleRepository
    {
        private readonly IRoleDAO _roleDao;

        public RoleRepository(IRoleDAO roleDao)
        {
            _roleDao = roleDao;
        }

        public async Task<Role> GetRoleByIdAsync(int RoleId)
        {
            return await _roleDao.GetRoleByIdAsync(RoleId);
        }

        public async Task<List<Role>> GetAllRolesAsync()
        {
            return await _roleDao.GetAllRolesAsync();
        }

        public async Task AddRoleAsync(Role Role)
        {
            await _roleDao.AddRoleAsync(Role);
        }

        public async Task UpdateRoleAsync(Role Role)
        {
            await _roleDao.UpdateRoleAsync(Role);
        }

        public async Task DeleteRoleAsync(int RoleId)
        {
            await _roleDao.DeleteRoleAsync(RoleId);
        }
    }
}
