using Microsoft.EntityFrameworkCore;

namespace Repositories.Entities;

public class ESDbContext : DbContext
{
    public virtual DbSet<Expense> Expenses => Set<Expense>();
    public virtual DbSet<Group> Groups => Set<Group>();
    public virtual DbSet<Item> Items => Set<Item>();
    public virtual DbSet<Person> Persons => Set<Person>();
    public virtual DbSet<PersonGroup> PersonGroups => Set<PersonGroup>();
    public virtual DbSet<Record> Records => Set<Record>();
    public virtual DbSet<Report> Reports => Set<Report>();
    public ESDbContext(DbContextOptions<ESDbContext> options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        //PersonGroup
        modelBuilder.Entity<PersonGroup>().HasKey(pg => new {pg.PersonId, pg.GroupId});
        modelBuilder.Entity<PersonGroup>().HasOne(pg => pg.Person).WithMany(p => p.PersonGroups).HasForeignKey(pg => pg.PersonId);
        modelBuilder.Entity<PersonGroup>().HasOne(pg => pg.Group).WithMany(g => g.PersonGroups).HasForeignKey(g => g.GroupId);
        modelBuilder.Entity<PersonGroup>().Property(pg => pg.IsAdmin).IsRequired();
        //Expense
        modelBuilder.Entity<Expense>().HasKey(e => e.Id);
        //Group
        modelBuilder.Entity<Group>().HasKey(g => g.Id);
        modelBuilder.Entity<Group>().HasMany(g => g.Reports).WithOne(r => r.Group).HasForeignKey(r => r.GroupId);
        //Item
        modelBuilder.Entity<Item>().HasKey(i => i.Id);
        //Person
        modelBuilder.Entity<Person>().HasKey(p => p.Id);
        modelBuilder.Entity<Person>().HasMany(p => p.Expenses).WithOne(e => e.Person).HasForeignKey(e => e.PersonId);

        //Record
        modelBuilder.Entity<Record>().HasKey(r => r.Id);
        //Report
        modelBuilder.Entity<Report>().HasKey(r => r.Id);
        modelBuilder.Entity<Report>().HasMany(r => r.Items).WithOne(i => i.Report).HasForeignKey(i => i.ReportId);
        modelBuilder.Entity<Report>().HasMany(r => r.Records).WithOne(rc => rc.Report).HasForeignKey(i => i.ReportId);


    }
}