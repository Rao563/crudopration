using Microsoft.EntityFrameworkCore;

namespace crudopration.Models
{
    public class UserContext : DbContext
    {
        public UserContext(DbContextOptions options) : base(options)
        { 

        }  
        
        public DbSet<Users> users { get; set; }
    }
}
