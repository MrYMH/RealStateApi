using Microsoft.EntityFrameworkCore;
using RealStateApi.Models;

namespace RealStateApi.Data
{
    public class ApiDbContext :DbContext
    {
        public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=SQL8001.site4now.net;Initial Catalog=db_a94aed_realstatedb;User Id=db_a94aed_realstatedb_admin;Password=yoyo123456");
        }
        //DESKTOP-KB8IIH3\SQLEXPRESS
        public DbSet<Category> Categories { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<Property> Properties { get; set; }
    }
}
