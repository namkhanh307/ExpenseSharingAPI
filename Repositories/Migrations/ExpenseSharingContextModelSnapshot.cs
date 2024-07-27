﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Repositories.Entities;

#nullable disable

namespace Repositories.Migrations
{
    [DbContext(typeof(ExpenseSharingContext))]
    partial class ExpenseSharingContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
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
                        .HasColumnType("float");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("DeletedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("DeletedTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("InvoiceImage")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastUpdatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("LastUpdatedTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ReportId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Type")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ReportId");

                    b.ToTable("Expenses");
                });

            modelBuilder.Entity("Repositories.Entities.Group", b =>
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
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Size")
                        .HasColumnType("int");

                    b.Property<int?>("Type")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Groups");
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
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Persons");
                });

            modelBuilder.Entity("Repositories.Entities.PersonExpense", b =>
                {
                    b.Property<string>("ExpenseId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("PersonId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<double>("Amount")
                        .HasColumnType("float");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("DeletedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("DeletedTime")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsShared")
                        .HasColumnType("bit");

                    b.Property<string>("LastUpdatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("LastUpdatedTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("ReportId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("ExpenseId", "PersonId");

                    b.HasIndex("PersonId");

                    b.HasIndex("ReportId");

                    b.ToTable("PersonExpenses");
                });

            modelBuilder.Entity("Repositories.Entities.PersonGroup", b =>
                {
                    b.Property<string>("PersonId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("GroupId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("DeletedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("DeletedTime")
                        .HasColumnType("datetime2");

                    b.Property<bool?>("IsAdmin")
                        .HasColumnType("bit");

                    b.Property<string>("LastUpdatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("LastUpdatedTime")
                        .HasColumnType("datetime2");

                    b.HasKey("PersonId", "GroupId");

                    b.HasIndex("GroupId");

                    b.ToTable("PersonGroups");
                });

            modelBuilder.Entity("Repositories.Entities.Record", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<double?>("Amount")
                        .HasColumnType("float");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("DeletedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("DeletedTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("ExpenseId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("InvoiceImage")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool?>("IsPaid")
                        .HasColumnType("bit");

                    b.Property<string>("LastUpdatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("LastUpdatedTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("PersonId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ReportId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("ExpenseId");

                    b.HasIndex("PersonId");

                    b.HasIndex("ReportId");

                    b.ToTable("Records");
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
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LastUpdatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("LastUpdatedTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("GroupId");

                    b.ToTable("Reports");
                });

            modelBuilder.Entity("Repositories.Entities.Expense", b =>
                {
                    b.HasOne("Repositories.Entities.Report", "Report")
                        .WithMany("Expenses")
                        .HasForeignKey("ReportId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Report");
                });

            modelBuilder.Entity("Repositories.Entities.PersonExpense", b =>
                {
                    b.HasOne("Repositories.Entities.Expense", "Expense")
                        .WithMany("PersonExpenses")
                        .HasForeignKey("ExpenseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Repositories.Entities.Person", "Person")
                        .WithMany("PersonExpenses")
                        .HasForeignKey("PersonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Repositories.Entities.Report", "Report")
                        .WithMany("PersonExpenses")
                        .HasForeignKey("ReportId");

                    b.Navigation("Expense");

                    b.Navigation("Person");

                    b.Navigation("Report");
                });

            modelBuilder.Entity("Repositories.Entities.PersonGroup", b =>
                {
                    b.HasOne("Repositories.Entities.Group", "Group")
                        .WithMany("PersonGroups")
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Repositories.Entities.Person", "Person")
                        .WithMany("PersonGroups")
                        .HasForeignKey("PersonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Group");

                    b.Navigation("Person");
                });

            modelBuilder.Entity("Repositories.Entities.Record", b =>
                {
                    b.HasOne("Repositories.Entities.Expense", "Expense")
                        .WithMany("Records")
                        .HasForeignKey("ExpenseId");

                    b.HasOne("Repositories.Entities.Person", "Person")
                        .WithMany("Records")
                        .HasForeignKey("PersonId");

                    b.HasOne("Repositories.Entities.Report", "Report")
                        .WithMany("Records")
                        .HasForeignKey("ReportId");

                    b.Navigation("Expense");

                    b.Navigation("Person");

                    b.Navigation("Report");
                });

            modelBuilder.Entity("Repositories.Entities.Report", b =>
                {
                    b.HasOne("Repositories.Entities.Group", "Group")
                        .WithMany("Reports")
                        .HasForeignKey("GroupId");

                    b.Navigation("Group");
                });

            modelBuilder.Entity("Repositories.Entities.Expense", b =>
                {
                    b.Navigation("PersonExpenses");

                    b.Navigation("Records");
                });

            modelBuilder.Entity("Repositories.Entities.Group", b =>
                {
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

                    b.Navigation("PersonExpenses");

                    b.Navigation("Records");
                });
#pragma warning restore 612, 618
        }
    }
}
