using Ecom.Core.src.Entity;
using Ecom.Core.src.Enum;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace Ecom.WebAPI.src.Database
{
    public class DataBaseContext : DbContext
    {
        IConfiguration _config;

        public DbSet<User> Users { get; set; }

        public DataBaseContext(DbContextOptions options, IConfiguration config) : base(options)
        {
            _config = config;
        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            var dataSourceBuilder = new NpgsqlDataSourceBuilder(_config.GetConnectionString("LocalDb"));
            dataSourceBuilder.MapEnum<Role>();
            var dataSource = dataSourceBuilder.Build();
            optionsBuilder.UseNpgsql(dataSource).UseSnakeCaseNamingConvention();
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasPostgresEnum<Role>();
            modelBuilder.Entity<User>(entity => entity.Property(e => e.Role).HasColumnType("role"));

            base.OnModelCreating(modelBuilder);
        }


    }
}