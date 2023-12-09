using Microsoft.EntityFrameworkCore;
using pedagangpulsa.api.Domain.Entities;

namespace pedagangpulsa.api.Context
{
    public class dbPedagangPulsaContext : DbContext
    {
        private readonly IConfiguration config;

        public dbPedagangPulsaContext(IConfiguration _config)
        {
            config = _config;
        }

        public dbPedagangPulsaContext(DbContextOptions<dbPedagangPulsaContext> options, IConfiguration _config)
            : base(options)
        {
            config = _config;
        }
        public DbSet<konter> Konter { get; set; }
        public DbSet<konter> Tasks { get; set; }


    }
}
