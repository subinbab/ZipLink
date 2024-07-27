
using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using Models.DTO;
using Models.User;

namespace Repository
{
    public class ZiplinkDBContext : IdentityDbContext<ApplicationUser>
    {
        public ZiplinkDBContext():base("name=defaultConnection")
        {
                
        }
        public DbSet<URLDTO> urlOperator { get; set; }
        public DbSet<URLTracking> urlTracking { get; set; }

        public static ZiplinkDBContext Create()
        {
            return new ZiplinkDBContext();
        }
    }
}
