using Microsoft.EntityFrameworkCore;

namespace XComm.Api.DataModel
{
    public class XcommDbContext: DbContext
    {
        public XcommDbContext(DbContextOptions<XcommDbContext> options): base(options)
        {            
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Variants> Variants { get; set; }
        public DbSet<Products> Products { get; set; }

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

            modelBuilder.Entity<Products>()
                .Property(o => o.Price)
                .HasColumnType("decimal(18,4)");

            modelBuilder.Entity<Products>()
                .Property(o => o.Stock)
                .HasColumnType("decimal(18,4)");
        }

    }


}
