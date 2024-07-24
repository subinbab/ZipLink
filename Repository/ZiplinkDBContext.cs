using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.DTO;

namespace Repository
{
    public class ZiplinkDBContext : DbContext
    {
        public ZiplinkDBContext():base("name=defaultConnection")
        {
                
        }
        public DbSet<URLDTO> urlOperator { get; set; }
    }
}
