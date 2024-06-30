using BadmintonCenter.BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BadmintonCenter.Service.Interface
{
    public interface IPackageService
    {
        Task<List<Package>> GetAllPackages();
        Task<Package?> GetPackageById(int id);
    }
}
