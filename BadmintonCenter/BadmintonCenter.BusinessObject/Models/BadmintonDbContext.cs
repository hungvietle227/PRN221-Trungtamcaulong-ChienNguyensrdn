using Microsoft.EntityFrameworkCore;

namespace BadmintonCenter.BusinessObject.Models
{
    public class BadmintonDbContext : DbContext
    {
        public BadmintonDbContext(DbContextOptions<BadmintonDbContext> options) : base(options)
        {
            
        }
    }
}
