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
    [Migration("20240804113605_init")]
    partial class init
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
                        .HasColumnType("float");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("CreatedTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("DeletedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("DeletedTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("InvoiceImage")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastUpdatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("LastUpdatedTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ReportId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Type")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ReportId");

                    b.ToTable("Expenses");
                });

            modelBuilder.Entity("Repositories.Entities.Friend", b =>
                {
                    b.Property<string>("PersonId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("FriendId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("CreatedTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("DeletedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("DeletedTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastUpdatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("LastUpdatedTime")
                        .HasColumnType("datetime2");

                    b.HasKey("PersonId", "FriendId");

                    b.HasIndex("FriendId");

                    b.ToTable("Friends");
                });

            modelBuilder.Entity("Repositories.Entities.FriendRequest", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("CreatedTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("DeletedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("DeletedTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("LastUpdatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("LastUpdatedTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("ReceiverId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("RequestDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("SenderId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ReceiverId");

                    b.HasIndex("SenderId");

                    b.ToTable("FriendRequests");
                });

            modelBuilder.Entity("Repositories.Entities.Group", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("CreatedTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("DeletedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("DeletedTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("LastUpdatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("LastUpdatedTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("Size")
                        .HasColumnType("int");

                    b.Property<int?>("Type")
                        .HasColumnType("int");

                    b.Property<string>("Wallpaper")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Groups");
                });

            modelBuilder.Entity("Repositories.Entities.GroupCode", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("accessCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("expiredTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("groupId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("groupId");

                    b.ToTable("GroupCodes");
                });

            modelBuilder.Entity("Repositories.Entities.Person", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("CreatedTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("DeletedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("DeletedTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("LastUpdatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("LastUpdatedTime")
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

                    b.HasKey("ExpenseId", "PersonId");

                    b.HasIndex("PersonId");

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

                    b.Property<DateTime?>("CreatedTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("DeletedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("DeletedTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("InvoiceImage")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool?>("IsPaid")
                        .HasColumnType("bit");

                    b.Property<string>("LastUpdatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("LastUpdatedTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("PersonPayId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("PersonReceiveId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ReportId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("PersonPayId");

                    b.HasIndex("PersonReceiveId");

                    b.HasIndex("ReportId");

                    b.ToTable("Records");
                });

            modelBuilder.Entity("Repositories.Entities.Report", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("CreatedTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("DeletedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("DeletedTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("FromDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("GroupId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LastUpdatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("LastUpdatedTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("ToDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("GroupId");

                    b.ToTable("Reports");
                });

            modelBuilder.Entity("Repositories.Entities.Expense", b =>
                {
                    b.HasOne("Repositories.Entities.Report", "Report")
                        .WithMany("Expenses")
                        .HasForeignKey("ReportId");

                    b.Navigation("Report");
                });

            modelBuilder.Entity("Repositories.Entities.Friend", b =>
                {
                    b.HasOne("Repositories.Entities.Person", "FriendPerson")
                        .WithMany("FriendOf")
                        .HasForeignKey("FriendId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Repositories.Entities.Person", "Person")
                        .WithMany("Friends")
                        .HasForeignKey("PersonId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("FriendPerson");

                    b.Navigation("Person");
                });

            modelBuilder.Entity("Repositories.Entities.FriendRequest", b =>
                {
                    b.HasOne("Repositories.Entities.Person", "Receiver")
                        .WithMany("FriendRequestsReceived")
                        .HasForeignKey("ReceiverId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Repositories.Entities.Person", "Sender")
                        .WithMany("FriendRequestsSent")
                        .HasForeignKey("SenderId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Receiver");

                    b.Navigation("Sender");
                });

            modelBuilder.Entity("Repositories.Entities.GroupCode", b =>
                {
                    b.HasOne("Repositories.Entities.Group", "Group")
                        .WithMany("GroupCodes")
                        .HasForeignKey("groupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Group");
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

                    b.Navigation("Expense");

                    b.Navigation("Person");
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
                    b.HasOne("Repositories.Entities.Person", "PersonPay")
                        .WithMany("RecordPays")
                        .HasForeignKey("PersonPayId");

                    b.HasOne("Repositories.Entities.Person", "PersonReceive")
                        .WithMany("RecordReceives")
                        .HasForeignKey("PersonReceiveId");

                    b.HasOne("Repositories.Entities.Report", "Report")
                        .WithMany("Records")
                        .HasForeignKey("ReportId");

                    b.Navigation("PersonPay");

                    b.Navigation("PersonReceive");

                    b.Navigation("Report");
                });

            modelBuilder.Entity("Repositories.Entities.Report", b =>
                {
                    b.HasOne("Repositories.Entities.Group", "Group")
                        .WithMany("Reports")
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Group");
                });

            modelBuilder.Entity("Repositories.Entities.Expense", b =>
                {
                    b.Navigation("PersonExpenses");
                });

            modelBuilder.Entity("Repositories.Entities.Group", b =>
                {
                    b.Navigation("GroupCodes");

                    b.Navigation("PersonGroups");

                    b.Navigation("Reports");
                });

            modelBuilder.Entity("Repositories.Entities.Person", b =>
                {
                    b.Navigation("FriendOf");

                    b.Navigation("FriendRequestsReceived");

                    b.Navigation("FriendRequestsSent");

                    b.Navigation("Friends");

                    b.Navigation("PersonExpenses");

                    b.Navigation("PersonGroups");

                    b.Navigation("RecordPays");

                    b.Navigation("RecordReceives");
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
