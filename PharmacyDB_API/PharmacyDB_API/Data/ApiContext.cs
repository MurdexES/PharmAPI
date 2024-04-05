using Microsoft.EntityFrameworkCore;
using PharmacyDB_API.Models;

namespace PharmacyDB_API.Data
{
    public class ApiContext : DbContext
    {
        public DbSet<Pharmaceutical> Pharms { get; set; }
        public  ApiContext(DbContextOptions<ApiContext> options) : base(options)
        {

        }
    }
}
