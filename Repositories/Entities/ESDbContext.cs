using Microsoft.EntityFrameworkCore;

namespace Repositories.Entities
{
    public class ESDbContext : DbContext
    {
        public ESDbContext(DbContextOptions<ESDbContext> options) : base(options)
        {
        }

         public ESDbContext()
        {
    
        }
        public virtual DbSet<Expense> Expenses { get; set; }
        public virtual DbSet<Group> Groups { get; set; }
        public virtual DbSet<Item> Items { get; set; }
        public virtual DbSet<Person> Persons { get; set; }
        public virtual DbSet<PersonGroup> PersonGroups { get; set; }
        public virtual DbSet<Record> Records { get; set; }
        public virtual DbSet<Report> Reports { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.UseSqlServer("server =(local),1433;database=ExpenseSharingAPICF;uid=sa;pwd=12345;Trusted_Connection=True;Trust Server Certificate=True;Timeout=30;");
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            // PersonGroup
            modelBuilder.Entity<PersonGroup>().HasKey(pg => new { pg.PersonId, pg.GroupId });
            modelBuilder.Entity<PersonGroup>().HasOne(pg => pg.Person).WithMany(p => p.PersonGroups).HasForeignKey(pg => pg.PersonId).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<PersonGroup>().HasOne(pg => pg.Group).WithMany(g => g.PersonGroups).HasForeignKey(pg => pg.GroupId).OnDelete(DeleteBehavior.NoAction);

            // Expense
            modelBuilder.Entity<Expense>().HasOne(e => e.Person).WithMany(p => p.Expenses).HasForeignKey(e => e.PersonId).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Expense>().HasOne(e => e.Report).WithMany(r => r.Expenses).HasForeignKey(e => e.ReportId).OnDelete(DeleteBehavior.NoAction);

            // Group
            modelBuilder.Entity<Group>().HasMany(g => g.Reports).WithOne(r => r.Group).HasForeignKey(r => r.GroupId).OnDelete(DeleteBehavior.NoAction);

            // Item
            modelBuilder.Entity<Item>().HasOne(i => i.Person).WithMany(p => p.Items).HasForeignKey(i => i.PersonId).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Item>().HasOne(i => i.Report).WithMany(r => r.Items).HasForeignKey(i => i.ReportId).OnDelete(DeleteBehavior.NoAction);

            // Person
            modelBuilder.Entity<Person>().HasKey(r => r.Id);
            modelBuilder.Entity<Person>().HasMany(p => p.Expenses).WithOne(e => e.Person).HasForeignKey(e => e.PersonId).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Person>().HasMany(p => p.Items).WithOne(i => i.Person).HasForeignKey(i => i.PersonId).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Person>().HasMany(p => p.PersonGroups).WithOne(pg => pg.Person).HasForeignKey(pg => pg.PersonId).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Person>().HasMany(p => p.Records).WithOne(r => r.Person).HasForeignKey(r => r.PersonId).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Person>().HasMany(p => p.Reports).WithOne(r => r.Person).HasForeignKey(r => r.PersonId).OnDelete(DeleteBehavior.NoAction);

            // Record
            modelBuilder.Entity<Record>().HasKey(r => r.Id);
            modelBuilder.Entity<Record>().HasOne(r => r.Person).WithMany(p => p.Records).HasForeignKey(r => r.PersonId).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Record>().HasOne(r => r.Report).WithMany(r => r.Records).HasForeignKey(r => r.ReportId).OnDelete(DeleteBehavior.NoAction);

            // Report
            modelBuilder.Entity<Report>().HasKey(r => r.Id);
            modelBuilder.Entity<Report>().HasOne(r => r.Group).WithMany(g => g.Reports).HasForeignKey(r => r.GroupId).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Report>().HasOne(r => r.Person).WithMany(p => p.Reports).HasForeignKey(r => r.PersonId).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Report>().HasMany(r => r.Items).WithOne(i => i.Report).HasForeignKey(i => i.ReportId).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Report>().HasMany(r => r.Records).WithOne(rc => rc.Report).HasForeignKey(rc => rc.ReportId).OnDelete(DeleteBehavior.NoAction);
        }
    }
}
