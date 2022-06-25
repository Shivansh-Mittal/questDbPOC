using Microsoft.EntityFrameworkCore;
using QuestDbPOC.Models;

namespace QuestDbPOC.Models
{
    public partial class AppDbContext : DbContext
    {
        //public virtual DbSet<Contact> Contacts { get; set; }
        //public virtual DbSet<EmailTemplate> EmailTemplates { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Department> Departments { get; set; }

        // NewDbSet.//

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public AppDbContext()
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(Config.ConnectionString);
        }
    }
}