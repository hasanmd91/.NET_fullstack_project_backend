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
        public DbSet<Category> Category { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<Review> Review { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<Image> Images { get; set; }


        static DataBaseContext()
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

        }


        public DataBaseContext(DbContextOptions options, IConfiguration config) : base(options)
        {
            _config = config;
        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            var dataSourceBuilder = new NpgsqlDataSourceBuilder(_config.GetConnectionString("LocalDb"));
            dataSourceBuilder.MapEnum<Role>();
            var dataSource = dataSourceBuilder.Build();
            optionsBuilder.UseNpgsql(dataSource).UseSnakeCaseNamingConvention().AddInterceptors(new TimeStampInterceptor());
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasPostgresEnum<Role>();
            modelBuilder.Entity<User>(entity => entity.Property(e => e.Role).HasColumnType("role"));
            modelBuilder.Entity<User>().HasIndex(u => u.Email).IsUnique();
            modelBuilder.Entity<Category>().HasIndex(c => c.Name).IsUnique();
            modelBuilder.Entity<Review>().HasOne<User>().WithMany(r => r.Reviews);
            modelBuilder.Entity<Order>().HasOne<User>().WithMany(r => r.Orders);

            base.OnModelCreating(modelBuilder);
        }


    }
}