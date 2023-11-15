using Microsoft.EntityFrameworkCore;

namespace XComm.Api.DataModel
{
    public class XcommDbContext: DbContext
    {
        public XcommDbContext(DbContextOptions<XcommDbContext> options): base(options)
        {            
        }

        public DbSet<Category> Categories { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //base.OnConfiguring(optionsBuilder);
            IConfigurationRoot builder = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .Build();

            optionsBuilder.UseSqlServer(builder.GetConnectionString("Db_Conn"));
        }
    }
}
