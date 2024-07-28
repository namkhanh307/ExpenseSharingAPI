using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Repositories.Entities;

namespace Repositories.Entities;

public partial class ExpenseSharingContext : DbContext
{
    public ExpenseSharingContext()
    {
    }

    public ExpenseSharingContext(DbContextOptions<ExpenseSharingContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Expense> Expenses { get; set; }

    public virtual DbSet<Group> Groups { get; set; }

    public virtual DbSet<Person> Persons { get; set; }

    public virtual DbSet<PersonExpense> PersonExpenses { get; set; }

    public virtual DbSet<PersonGroup> PersonGroups { get; set; }

    public virtual DbSet<Record> Records { get; set; }

    public virtual DbSet<Report> Reports { get; set; }
    public virtual DbSet<Friend> Friends { get; set; }
    public virtual DbSet<FriendRequest> FriendRequests { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.UseSqlServer("Server=ASUS\\SQLSERVER;Database=ExpenseSharing;UID=sa;PWD=12345;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Expense configuration
        modelBuilder.Entity<Expense>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.HasOne(e => e.Report)
                .WithMany(r => r.Expenses)
                .HasForeignKey(e => e.ReportId);

            entity.HasMany(e => e.PersonExpenses)
                .WithOne(pe => pe.Expense)
                .HasForeignKey(pe => pe.ExpenseId);

            entity.HasMany(e => e.Records)
                .WithOne(r => r.Expense)
                .HasForeignKey(r => r.ExpenseId);
        });

        // Group configuration
        modelBuilder.Entity<Group>(entity =>
        {
            entity.HasKey(g => g.Id);

            entity.HasMany(g => g.PersonGroups)
                .WithOne(pg => pg.Group)
                .HasForeignKey(pg => pg.GroupId);

            entity.HasMany(g => g.Reports)
                .WithOne(r => r.Group)
                .HasForeignKey(r => r.GroupId);
        });

        // Person configuration
        //modelBuilder.Entity<Person>(entity =>
        //{
        //    entity.HasKey(p => p.Id);

        //    entity.HasMany(p => p.PersonExpenses)
        //        .WithOne(pe => pe.Person)
        //        .HasForeignKey(pe => pe.PersonId);

        //    entity.HasMany(p => p.PersonGroups)
        //        .WithOne(pg => pg.Person)
        //        .HasForeignKey(pg => pg.PersonId);

        //    entity.HasMany(p => p.Records)
        //        .WithOne(r => r.Person)
        //        .HasForeignKey(r => r.PersonId);
        //});
        modelBuilder.Entity<Person>(entity =>
        {
            entity.HasKey(p => p.Id);

            entity.HasMany(p => p.PersonExpenses)
                .WithOne(pe => pe.Person)
                .HasForeignKey(pe => pe.PersonId);

            entity.HasMany(p => p.PersonGroups)
                .WithOne(pg => pg.Person)
                .HasForeignKey(pg => pg.PersonId);

            entity.HasMany(p => p.Records)
                .WithOne(r => r.Person)
                .HasForeignKey(r => r.PersonId);

            entity.HasMany(p => p.Friends)
                .WithOne(f => f.Person)
                .HasForeignKey(f => f.PersonId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasMany(p => p.FriendRequestsSent)
                .WithOne(fr => fr.Sender)
                .HasForeignKey(fr => fr.SenderId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasMany(p => p.FriendRequestsReceived)
                .WithOne(fr => fr.Receiver)
                .HasForeignKey(fr => fr.ReceiverId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        // PersonExpense configuration
        modelBuilder.Entity<PersonExpense>(entity =>
        {
            entity.HasKey(pe => new { pe.ExpenseId, pe.PersonId });

            entity.HasOne(pe => pe.Report)
                .WithMany(r => r.PersonExpenses)
                .HasForeignKey(pe => pe.ReportId);
        });

        // PersonGroup configuration
        modelBuilder.Entity<PersonGroup>(entity =>
        {
            entity.HasKey(pg => new { pg.PersonId, pg.GroupId });
        });

        // Record configuration
        modelBuilder.Entity<Record>(entity =>
        {
            entity.HasKey(r => r.Id);

            entity.HasOne(r => r.Person)
                .WithMany(p => p.Records)
                .HasForeignKey(r => r.PersonId);

            entity.HasOne(r => r.Expense)
                .WithMany(e => e.Records)
                .HasForeignKey(r => r.ExpenseId);

            entity.HasOne(r => r.Report)
                .WithMany(rp => rp.Records)
                .HasForeignKey(r => r.ReportId);
        });

        // Report configuration
        modelBuilder.Entity<Report>(entity =>
        {
            entity.HasKey(r => r.Id);

            entity.HasOne(r => r.Group)
                .WithMany(g => g.Reports)
                .HasForeignKey(r => r.GroupId);
        });
        //Friend 
        modelBuilder.Entity<Friend>(entity =>
            {
                entity.HasKey(f => new { f.PersonId, f.FriendId });

                entity.HasOne(f => f.FriendPerson)
                    .WithMany(p => p.FriendOf)
                    .HasForeignKey(f => f.FriendId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // FriendRequest configuration
            modelBuilder.Entity<FriendRequest>(entity =>
            {
                entity.HasKey(fr => fr.Id);

                entity.HasOne(fr => fr.Sender)
                    .WithMany(p => p.FriendRequestsSent)
                    .HasForeignKey(fr => fr.SenderId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(fr => fr.Receiver)
                    .WithMany(p => p.FriendRequestsReceived)
                    .HasForeignKey(fr => fr.ReceiverId)
                    .OnDelete(DeleteBehavior.Restrict);
            });
    }
}
