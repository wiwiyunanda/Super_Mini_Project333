using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace XComm.Api.DataModel
{
    public class XcommDbContext: DbContext
    {
        public XcommDbContext(DbContextOptions<XcommDbContext> options) : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Variants> Variants { get; set; }
        public DbSet<Products> Products { get; set; }
        public DbSet<OrderHeader> OrderHeaders { get; set; }
        public DbSet<OrderDetail> OrderDetails{ get; set; }
        public DbSet<FileCollections> FileCollection { get; set; }
        public DbSet<Account> Account { get; set; }
        public DbSet<UserRoles> UserRole { get; set; }
        public DbSet<Gallery> Galleries { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //base.Onconfiguring(optionsBuilder)
            IConfigurationRoot builder = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .Build();
            optionsBuilder.UseSqlServer(builder.GetConnectionString("Db_Conn"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            //base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Category>()
                .HasIndex(o => o.Initial)
                .IsUnique();

            modelBuilder.Entity<Category>()
                .HasIndex(o => o.Name)
                .IsUnique();

            modelBuilder.Entity<Variants>()
                .HasIndex(o => o.Initial)
                .IsUnique();

            modelBuilder.Entity<Category>()
                .HasIndex(o => o.Name)
                .IsUnique();

            modelBuilder.Entity<Products>()
                .HasIndex(o => o.Initial)
                .IsUnique();

            modelBuilder.Entity<Products>()
                .HasIndex(o => o.Name)
                .IsUnique();
            modelBuilder.Entity<OrderHeader>()
               .HasIndex(o => o.Reference)
               .IsUnique();

            modelBuilder.Entity<FileCollections>()
                .HasIndex(o => o.Title)
                .IsUnique();

            modelBuilder.Entity<FileCollections>()
                .HasIndex(o => o.FileName)
                .IsUnique();

            modelBuilder.Entity<Account>()
                .HasIndex(o => o.UserName)
                .IsUnique();

            modelBuilder.Entity<Products>()
                .Property(o => o.Price)
                .HasColumnType("decimal(18,4)");

            modelBuilder.Entity<Products>()
                .Property(o => o.Stock)
                .HasColumnType("decimal(18,4)");

            modelBuilder.Entity<OrderHeader>()
                .Property(o => o.Amount)
                .HasColumnType("decimal(18,4)");

            modelBuilder.Entity<OrderDetail>()
                .Property(o => o.Price)
                .HasColumnType("decimal(18,4)");

            modelBuilder.Entity<OrderDetail>()
                .Property(o => o.Quantity)
                .HasColumnType("decimal(18,4)");

            modelBuilder.Entity<Gallery>()
                .Property(o => o.Base64Big)
                .HasColumnType("nvarchar(max)");

            modelBuilder.Entity<Gallery>()
                .Property(o => o.Base64Small)
                .HasColumnType("nvarchar(max)");
        }
    }
}
