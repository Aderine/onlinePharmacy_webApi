using Microsoft.EntityFrameworkCore;
using onlinePharmacy_webApi.Models;

namespace onlinePharmacy_webApi.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
        {

        }

        public DbSet<Product> products { get; set; }
        

    }
}
