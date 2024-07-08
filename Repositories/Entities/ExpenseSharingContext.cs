using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

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

    public virtual DbSet<Person> People { get; set; }

    public virtual DbSet<PersonExpense> PersonExpenses { get; set; }

    public virtual DbSet<PersonGroup> PersonGroups { get; set; }

    public virtual DbSet<Record> Records { get; set; }

    public virtual DbSet<Report> Reports { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=(local);Database=ExpenseSharingAPICF;UID=sa;PWD=12345;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Expense>(entity =>
        {
            entity.ToTable("Expense");

            entity.Property(e => e.Amount).HasColumnName("amount");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("expenseName");
            entity.Property(e => e.Type)
                .HasMaxLength(50)
                .HasColumnName("expenseType");
            entity.Property(e => e.InvoiceImage).HasColumnName("invoiceImage");
            entity.Property(e => e.PersonId).HasColumnName("personID");
            entity.Property(e => e.ReportId).HasColumnName("reportID");

            entity.HasOne(d => d.Person).WithMany(p => p.Expenses)
                .HasForeignKey(d => d.PersonId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Expense_Person");

            entity.HasOne(d => d.Report).WithMany(p => p.Expenses)
                .HasForeignKey(d => d.ReportId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Expense_Report");
        });

        modelBuilder.Entity<Group>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Room");

            entity.ToTable("Group");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
            entity.Property(e => e.Size).HasColumnName("size");
            entity.Property(e => e.Type).HasColumnName("type");
        });

        modelBuilder.Entity<Person>(entity =>
        {
            entity.ToTable("Person");

            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
            entity.Property(e => e.Password)
                .HasMaxLength(50)
                .HasColumnName("password");
            entity.Property(e => e.Phone)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("phone");
        });

        modelBuilder.Entity<PersonExpense>(entity =>
        {
            entity.HasKey(e => new { e.ExpenseId, e.PersonId, e.GroupId }).HasName("PK__PersonEx__35F3AA9E2F5ABE97");

            entity.ToTable("PersonExpense");

            entity.Property(e => e.ExpenseId).HasColumnName("ExpenseID");
            entity.Property(e => e.PersonId).HasColumnName("PersonID");
            entity.Property(e => e.GroupId).HasColumnName("GroupID");

            entity.HasOne(d => d.Expense).WithMany(p => p.PersonExpenses)
                .HasForeignKey(d => d.ExpenseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PersonExp__Expen__32AB8735");

            entity.HasOne(d => d.Group).WithMany(p => p.PersonExpenses)
                .HasForeignKey(d => d.GroupId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PersonExp__Group__3493CFA7");

            entity.HasOne(d => d.Person).WithMany(p => p.PersonExpenses)
                .HasForeignKey(d => d.PersonId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PersonExp__Perso__339FAB6E");
        });

        modelBuilder.Entity<PersonGroup>(entity =>
        {
            entity.HasKey(e => new { e.PersonId, e.GroupId }).HasName("PK__PersonRo__290798144CC3970E");

            entity.ToTable("PersonGroup");

            entity.Property(e => e.PersonId).HasColumnName("personID");
            entity.Property(e => e.GroupId).HasColumnName("groupID");
            entity.Property(e => e.IsAdmin).HasColumnName("isAdmin");

            entity.HasOne(d => d.Group).WithMany(p => p.PersonGroups)
                .HasForeignKey(d => d.GroupId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PersonRoo__RoomI__60A75C0F");

            entity.HasOne(d => d.Person).WithMany(p => p.PersonGroups)
                .HasForeignKey(d => d.PersonId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PersonRoo__Perso__5FB337D6");
        });

        modelBuilder.Entity<Record>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Record__D825197E756EB7D9");

            entity.ToTable("Record");

            entity.Property(e => e.Amount).HasColumnName("amount");
            entity.Property(e => e.ExpenseId).HasColumnName("expenseID");
            entity.Property(e => e.IsPaid).HasColumnName("isPaid");
            entity.Property(e => e.PersonId).HasColumnName("personID");
            entity.Property(e => e.ReportId).HasColumnName("reportID");

            entity.HasOne(d => d.Expense).WithMany(p => p.Records)
                .HasForeignKey(d => d.ExpenseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Record_Expense");

            entity.HasOne(d => d.Person).WithMany(p => p.Records)
                .HasForeignKey(d => d.PersonId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Record_Person");

            entity.HasOne(d => d.Report).WithMany(p => p.Records)
                .HasForeignKey(d => d.ReportId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Record_Report");
        });

        modelBuilder.Entity<Report>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Report__3214EC2761BA3C9B");

            entity.ToTable("Report");

            entity.Property(e => e.GroupId).HasColumnName("groupID");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
            entity.Property(e => e.PersonId).HasColumnName("personID");

            entity.HasOne(d => d.Group).WithMany(p => p.Reports)
                .HasForeignKey(d => d.GroupId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Report_Group");

            entity.HasOne(d => d.Person).WithMany(p => p.Reports)
                .HasForeignKey(d => d.PersonId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Report_Person");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
