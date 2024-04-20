using ComplaintsApp.Models;
using Microsoft.EntityFrameworkCore;

namespace ComplaintsApp.Data
{
    public class dbContext : DbContext
    {
        public dbContext(DbContextOptions<dbContext> options) : base(options)
        {
        }

        // DbSet for each entity in your database
        public DbSet<Complaint> Complaints { get; set; }
    }
}
