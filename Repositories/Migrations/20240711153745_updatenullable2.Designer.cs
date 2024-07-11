﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Repositories.Entities;

#nullable disable

namespace Repositories.Migrations
{
    [DbContext(typeof(ExpenseSharingContext))]
    [Migration("20240711153745_updatenullable2")]
    partial class updatenullable2
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Repositories.Entities.Expense", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<double?>("Amount")
                        .HasColumnType("float")
                        .HasColumnName("amount");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("DeletedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("DeletedTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("InvoiceImage")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("invoiceImage");

                    b.Property<string>("LastUpdatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("LastUpdatedTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("expenseName");

                    b.Property<string>("ReportId")
                        .HasColumnType("nvarchar(450)")
                        .HasColumnName("reportID");

                    b.Property<string>("Type")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("expenseType");

                    b.HasKey("Id");

                    b.HasIndex("ReportId");

                    b.ToTable("Expense", (string)null);
                });

            modelBuilder.Entity("Repositories.Entities.Group", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)")
                        .HasColumnName("ID");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("DeletedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("DeletedTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("LastUpdatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("LastUpdatedTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("name");

                    b.Property<int>("Size")
                        .HasColumnType("int")
                        .HasColumnName("size");

                    b.Property<int?>("Type")
                        .HasColumnType("int")
                        .HasColumnName("type");

                    b.HasKey("Id")
                        .HasName("PK_Room");

                    b.ToTable("Group", (string)null);
                });

            modelBuilder.Entity("Repositories.Entities.Person", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("DeletedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("DeletedTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("LastUpdatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("LastUpdatedTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("name");

                    b.Property<string>("Password")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("password");

                    b.Property<string>("Phone")
                        .HasMaxLength(10)
                        .HasColumnType("nchar(10)")
                        .HasColumnName("phone")
                        .IsFixedLength();

                    b.HasKey("Id");

                    b.ToTable("Person", (string)null);
                });

            modelBuilder.Entity("Repositories.Entities.PersonExpense", b =>
                {
                    b.Property<string>("ExpenseId")
                        .HasColumnType("nvarchar(450)")
                        .HasColumnName("ExpenseID");

                    b.Property<string>("PersonId")
                        .HasColumnType("nvarchar(450)")
                        .HasColumnName("PersonID");

                    b.Property<string>("GroupId")
                        .HasColumnType("nvarchar(450)")
                        .HasColumnName("GroupID");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("DeletedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("DeletedTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastUpdatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("LastUpdatedTime")
                        .HasColumnType("datetime2");

                    b.HasKey("ExpenseId", "PersonId", "GroupId")
                        .HasName("PK__PersonEx__35F3AA9E2F5ABE97");

                    b.HasIndex("GroupId");

                    b.HasIndex("PersonId");

                    b.ToTable("PersonExpense", (string)null);
                });

            modelBuilder.Entity("Repositories.Entities.PersonGroup", b =>
                {
                    b.Property<string>("PersonId")
                        .HasColumnType("nvarchar(450)")
                        .HasColumnName("personID");

                    b.Property<string>("GroupId")
                        .HasColumnType("nvarchar(450)")
                        .HasColumnName("groupID");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("DeletedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("DeletedTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool?>("IsAdmin")
                        .HasColumnType("bit")
                        .HasColumnName("isAdmin");

                    b.Property<string>("LastUpdatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("LastUpdatedTime")
                        .HasColumnType("datetime2");

                    b.HasKey("PersonId", "GroupId")
                        .HasName("PK__PersonRo__290798144CC3970E");

                    b.HasIndex("GroupId");

                    b.ToTable("PersonGroup", (string)null);
                });

            modelBuilder.Entity("Repositories.Entities.Record", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<double?>("Amount")
                        .HasColumnType("float")
                        .HasColumnName("amount");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("DeletedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("DeletedTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("ExpenseId")
                        .HasColumnType("nvarchar(450)")
                        .HasColumnName("expenseID");

                    b.Property<bool?>("IsPaid")
                        .HasColumnType("bit")
                        .HasColumnName("isPaid");

                    b.Property<string>("LastUpdatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("LastUpdatedTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("PersonId")
                        .HasColumnType("nvarchar(450)")
                        .HasColumnName("personID");

                    b.Property<string>("ReportId")
                        .HasColumnType("nvarchar(450)")
                        .HasColumnName("reportID");

                    b.HasKey("Id")
                        .HasName("PK__Record__D825197E756EB7D9");

                    b.HasIndex("ExpenseId");

                    b.HasIndex("PersonId");

                    b.HasIndex("ReportId");

                    b.ToTable("Record", (string)null);
                });

            modelBuilder.Entity("Repositories.Entities.Report", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("DeletedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("DeletedTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("GroupId")
                        .HasColumnType("nvarchar(450)")
                        .HasColumnName("groupID");

                    b.Property<string>("LastUpdatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("LastUpdatedTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("name");

                    b.HasKey("Id")
                        .HasName("PK__Report__3214EC2761BA3C9B");

                    b.HasIndex("GroupId");

                    b.ToTable("Report", (string)null);
                });

            modelBuilder.Entity("Repositories.Entities.Expense", b =>
                {
                    b.HasOne("Repositories.Entities.Report", "Report")
                        .WithMany("Expenses")
                        .HasForeignKey("ReportId")
                        .HasConstraintName("FK_Expense_Report");

                    b.Navigation("Report");
                });

            modelBuilder.Entity("Repositories.Entities.PersonExpense", b =>
                {
                    b.HasOne("Repositories.Entities.Expense", "Expense")
                        .WithMany("PersonExpenses")
                        .HasForeignKey("ExpenseId")
                        .IsRequired()
                        .HasConstraintName("FK__PersonExp__Expen__32AB8735");

                    b.HasOne("Repositories.Entities.Group", "Group")
                        .WithMany("PersonExpenses")
                        .HasForeignKey("GroupId")
                        .IsRequired()
                        .HasConstraintName("FK__PersonExp__Group__3493CFA7");

                    b.HasOne("Repositories.Entities.Person", "Person")
                        .WithMany("PersonExpenses")
                        .HasForeignKey("PersonId")
                        .IsRequired()
                        .HasConstraintName("FK__PersonExp__Perso__339FAB6E");

                    b.Navigation("Expense");

                    b.Navigation("Group");

                    b.Navigation("Person");
                });

            modelBuilder.Entity("Repositories.Entities.PersonGroup", b =>
                {
                    b.HasOne("Repositories.Entities.Group", "Group")
                        .WithMany("PersonGroups")
                        .HasForeignKey("GroupId")
                        .IsRequired()
                        .HasConstraintName("FK__PersonRoo__RoomI__60A75C0F");

                    b.HasOne("Repositories.Entities.Person", "Person")
                        .WithMany("PersonGroups")
                        .HasForeignKey("PersonId")
                        .IsRequired()
                        .HasConstraintName("FK__PersonRoo__Perso__5FB337D6");

                    b.Navigation("Group");

                    b.Navigation("Person");
                });

            modelBuilder.Entity("Repositories.Entities.Record", b =>
                {
                    b.HasOne("Repositories.Entities.Expense", "Expense")
                        .WithMany("Records")
                        .HasForeignKey("ExpenseId")
                        .HasConstraintName("FK_Record_Expense");

                    b.HasOne("Repositories.Entities.Person", "Person")
                        .WithMany("Records")
                        .HasForeignKey("PersonId")
                        .HasConstraintName("FK_Record_Person");

                    b.HasOne("Repositories.Entities.Report", "Report")
                        .WithMany("Records")
                        .HasForeignKey("ReportId")
                        .HasConstraintName("FK_Record_Report");

                    b.Navigation("Expense");

                    b.Navigation("Person");

                    b.Navigation("Report");
                });

            modelBuilder.Entity("Repositories.Entities.Report", b =>
                {
                    b.HasOne("Repositories.Entities.Group", "Group")
                        .WithMany("Reports")
                        .HasForeignKey("GroupId")
                        .HasConstraintName("FK_Report_Group");

                    b.Navigation("Group");
                });

            modelBuilder.Entity("Repositories.Entities.Expense", b =>
                {
                    b.Navigation("PersonExpenses");

                    b.Navigation("Records");
                });

            modelBuilder.Entity("Repositories.Entities.Group", b =>
                {
                    b.Navigation("PersonExpenses");

                    b.Navigation("PersonGroups");

                    b.Navigation("Reports");
                });

            modelBuilder.Entity("Repositories.Entities.Person", b =>
                {
                    b.Navigation("PersonExpenses");

                    b.Navigation("PersonGroups");

                    b.Navigation("Records");
                });

            modelBuilder.Entity("Repositories.Entities.Report", b =>
                {
                    b.Navigation("Expenses");

                    b.Navigation("Records");
                });
#pragma warning restore 612, 618
        }
    }
}
