using System.Runtime.CompilerServices;
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
        public DbSet<RoleGroup> RoleGroups { get; set; }
        public DbSet<AuthorizationGroup> AuthorizationGroups { get; set; }

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
            modelBuilder.Seed();

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

            modelBuilder.Entity<Account>()
                .HasIndex(o => o.UserName)
                .IsUnique();

            modelBuilder.Entity<Account>()
               .Property(o => o.Email)
               .HasDefaultValue("winkawinoy@gmail.com");

            modelBuilder.Entity<Account>()
               .Property(o => o.RoleGroupId)
               .HasDefaultValue(1);

            modelBuilder.Entity<Account>()
                .Property(o => o.Otp)
                .HasColumnType("char(6)");

            modelBuilder.Entity<RoleGroup>()
                .HasIndex(o => o.GroupName)
                .IsUnique();
        }
    }

    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>()
                .HasData(
                new Account()
                {
                    Id = 1,
                    UserName = "admin",
                    Password = "ac9689e2272427085e35b9d3e3e8bed88cb3434828b43b86fc0596cad4c6e270", //admin1234
                    FirstName = "Super",
                    LastName = "Admin",
                    Email = "winkawinoy@gmail.com",
                    Active = true,
                    CreatedDate = DateTime.Now,
                    CreatedBy = "admin"
                }, new Account()
                {
                    Id = 2,
                    UserName = "user1",
                    Password = "0a041b9462caa4a31bac3567e0b6e6fd9100787db2ab433d96f6d178cabfce90", //user1
                    FirstName = "User",
                    LastName = "One",
                    Email = "winkawinoy@gmail.com",
                    Active = true,
                    CreatedDate = DateTime.Now,
                    CreatedBy = "admin"
                });

            modelBuilder.Entity<RoleGroup>()
               .HasData(
                new RoleGroup() { Id = 1, GroupName = "Customer", CreatedBy = "admin", CreatedDate = DateTime.Now },
                new RoleGroup() { Id = 2, GroupName = "Kasir", CreatedBy = "admin", CreatedDate = DateTime.Now }
                );

            modelBuilder.Entity<AuthorizationGroup>()
               .HasData(
                new AuthorizationGroup() { Id = 1, RoleGroupId= 1, Role = "Products", CreatedBy = "admin", CreatedDate = DateTime.Now },
                new AuthorizationGroup() { Id = 2, RoleGroupId = 1, Role = "Orders", CreatedBy = "admin", CreatedDate = DateTime.Now },
                new AuthorizationGroup() { Id = 3, RoleGroupId = 2, Role = "Categories", CreatedBy = "admin", CreatedDate = DateTime.Now },
                new AuthorizationGroup() { Id = 4, RoleGroupId = 2, Role = "Variants", CreatedBy = "admin", CreatedDate = DateTime.Now },
                new AuthorizationGroup() { Id = 5, RoleGroupId = 2, Role = "Products", CreatedBy = "admin", CreatedDate = DateTime.Now },
                new AuthorizationGroup() { Id = 6, RoleGroupId = 2, Role = "Orders", CreatedBy = "admin", CreatedDate = DateTime.Now }

                );
        }
    }
}
