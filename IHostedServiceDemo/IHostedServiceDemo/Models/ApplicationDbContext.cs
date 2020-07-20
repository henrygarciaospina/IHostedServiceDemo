using IHostedServiceDemo.Models;
using Microsoft.EntityFrameworkCore;

namespace IHostedServiceDemo.Contexts
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {


        }

        public DbSet<HostedServiceLog> HostedServiceLogs { get; set; }
    }
}
