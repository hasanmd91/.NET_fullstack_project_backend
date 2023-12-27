using Ecom.Core.src.Entity;
using Ecom.Core.src.Enum;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Npgsql;

namespace Ecom.WebAPI.src.Database
{
    public class DataBaseContext : DbContext
    {
        private readonly string? _connectionString;
        public DbSet<User> Users { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<Review> Review { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<OrderDetails> OrderDetails { get; set; }
        public DbSet<Image> Images { get; set; }

        static DataBaseContext()
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

        }

        public DataBaseContext(DbContextOptions options, IConfiguration config) : base(options)
        {
            _connectionString = config.GetConnectionString("LocalDb");

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            var dataSourceBuilder = new NpgsqlDataSourceBuilder(_connectionString);
            dataSourceBuilder.MapEnum<Role>();
            dataSourceBuilder.MapEnum<OrderStatus>();
            var dataSource = dataSourceBuilder.Build();
            optionsBuilder
            .UseNpgsql(dataSource, options => options.EnableRetryOnFailure(3))
            .UseSnakeCaseNamingConvention()
            .AddInterceptors(new TimeStampInterceptor())
            .EnableDetailedErrors()
            .ConfigureWarnings(warnings => { warnings.Ignore(CoreEventId.ManyServiceProvidersCreatedWarning); });

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasPostgresEnum<Role>();
            modelBuilder.HasPostgresEnum<OrderStatus>();
            modelBuilder.Entity<User>(entity => entity.Property(e => e.Role).HasColumnType("role"));
            modelBuilder.Entity<Order>(entity => entity.Property(e => e.OrderStatus).HasColumnType("order_status"));
            modelBuilder.Entity<User>().HasIndex(u => u.Email).IsUnique();
            modelBuilder.Entity<Category>().HasIndex(c => c.Name).IsUnique();


            base.OnModelCreating(modelBuilder);
        }

    }
}