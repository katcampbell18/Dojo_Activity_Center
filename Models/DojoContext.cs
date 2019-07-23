using Microsoft.EntityFrameworkCore;


namespace ActivityCenter.Models {
    public class DojoContext : DbContext {
        public DojoContext(DbContextOptions options) : base(options) { }
        public DbSet<User> Users {get;set;}
        public DbSet<DActivity> Activities {get; set;}
        public DbSet<Join> Joins {get;set;}
    }
}
