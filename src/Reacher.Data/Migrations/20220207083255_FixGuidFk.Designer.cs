﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Reacher.Data;

#nullable disable

namespace Reacher.Data.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20220207083255_FixGuidFk")]
    partial class FixGuidFk
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Reacher.Data.Models.DbEmail", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Body")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal?>("CostUsd")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("FromEmailAddress")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("FromEmailName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("InvoiceStatus")
                        .HasColumnType("int");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("OriginalEmailId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("PaidDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("ReachableId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("SentDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("StrikeInvoiceId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Subject")
                        .IsRequired()
                        .HasMaxLength(998)
                        .HasColumnType("nvarchar(998)");

                    b.Property<string>("ToEmailAddress")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ToEmailName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("FromEmailAddress");

                    b.HasIndex("InvoiceStatus");

                    b.HasIndex("OriginalEmailId");

                    b.HasIndex("ReachableId");

                    b.HasIndex("SentDate");

                    b.HasIndex("StrikeInvoiceId");

                    b.HasIndex("ToEmailAddress");

                    b.HasIndex("Type");

                    b.ToTable("Email");
                });

            modelBuilder.Entity("Reacher.Data.Models.DbReachable", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("CostUsdToReach")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.Property<string>("ReacherEmailAddress")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.Property<bool>("ReplyIsFree")
                        .HasColumnType("bit");

                    b.Property<string>("StrikeUsername")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("ToEmailAddress")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.HasKey("Id");

                    b.HasIndex("ReacherEmailAddress")
                        .IsUnique();

                    b.ToTable("Reachable");
                });

            modelBuilder.Entity("Reacher.Data.Models.EnumTable<Reacher.Data.Enums.EmailType>", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("EmailTypes");

                    b.HasData(
                        new
                        {
                            Id = 0,
                            Name = "New"
                        },
                        new
                        {
                            Id = 1,
                            Name = "Failed"
                        },
                        new
                        {
                            Id = 2,
                            Name = "InboundReach"
                        },
                        new
                        {
                            Id = 3,
                            Name = "InboundForward"
                        },
                        new
                        {
                            Id = 4,
                            Name = "PaymentRequest"
                        },
                        new
                        {
                            Id = 5,
                            Name = "OutboundReply"
                        },
                        new
                        {
                            Id = 6,
                            Name = "OutboundForward"
                        },
                        new
                        {
                            Id = 7,
                            Name = "TooSoon"
                        });
                });

            modelBuilder.Entity("Reacher.Data.Models.EnumTable<Reacher.Data.Enums.InvoiceStatus>", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("InvoiceStatuses");

                    b.HasData(
                        new
                        {
                            Id = 0,
                            Name = "Requested"
                        },
                        new
                        {
                            Id = 1,
                            Name = "Paid"
                        });
                });

            modelBuilder.Entity("Reacher.Data.Models.DbEmail", b =>
                {
                    b.HasOne("Reacher.Data.Models.EnumTable<Reacher.Data.Enums.InvoiceStatus>", "InvoiceStatusRef")
                        .WithMany()
                        .HasForeignKey("InvoiceStatus");

                    b.HasOne("Reacher.Data.Models.DbEmail", "OriginalEmail")
                        .WithMany()
                        .HasForeignKey("OriginalEmailId");

                    b.HasOne("Reacher.Data.Models.DbReachable", "Reachable")
                        .WithMany("Emails")
                        .HasForeignKey("ReachableId");

                    b.HasOne("Reacher.Data.Models.EnumTable<Reacher.Data.Enums.EmailType>", "TypeRef")
                        .WithMany()
                        .HasForeignKey("Type")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("InvoiceStatusRef");

                    b.Navigation("OriginalEmail");

                    b.Navigation("Reachable");

                    b.Navigation("TypeRef");
                });

            modelBuilder.Entity("Reacher.Data.Models.DbReachable", b =>
                {
                    b.Navigation("Emails");
                });
#pragma warning restore 612, 618
        }
    }
}
