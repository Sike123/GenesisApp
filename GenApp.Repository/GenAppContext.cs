using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;


namespace GenApp.Repository
{
    public class GenAppContext:DbContext
    {
        public GenAppContext() : base("GenAppContext")
        {
            

        }

        public DbSet<Asset> Assets { get; set; }

        public DbSet<Book> Books { get; set; }

        public DbSet<User>Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
   
    }
}
