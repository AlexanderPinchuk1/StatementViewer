﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using StatementViewer.Repositories;

#nullable disable

namespace StatementViewer.Migrations
{
    [DbContext(typeof(StatementViewerDbContext))]
    partial class StatementViewerDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("StatementViewer.Models.AccountInfo", b =>
                {
                    b.Property<Guid?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasDefaultValueSql("newsequentialid()");

                    b.Property<Guid?>("AccountUnitId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("Asset")
                        .HasPrecision(30, 5)
                        .HasColumnType("decimal(30,5)");

                    b.Property<decimal>("Credit")
                        .HasPrecision(30, 5)
                        .HasColumnType("decimal(30,5)");

                    b.Property<decimal>("Debit")
                        .HasPrecision(30, 5)
                        .HasColumnType("decimal(30,5)");

                    b.Property<string>("Number")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<decimal>("Passive")
                        .HasPrecision(30, 5)
                        .HasColumnType("decimal(30,5)");

                    b.HasKey("Id");

                    b.HasIndex("AccountUnitId");

                    b.ToTable("Account", (string)null);
                });

            modelBuilder.Entity("StatementViewer.Models.AccountUnit", b =>
                {
                    b.Property<Guid?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasDefaultValueSql("newsequentialid()");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("Number")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<Guid>("StatementId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("StatementId");

                    b.ToTable("AccountUnit", (string)null);
                });

            modelBuilder.Entity("StatementViewer.Models.Statement", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasDefaultValueSql("newsequentialid()");

                    b.Property<string>("BankName")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime?>("EndOfPeriod")
                        .HasColumnType("datetime2");

                    b.Property<string>("FileName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime?>("GenerationDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("StartOfPeriod")
                        .HasColumnType("datetime2");

                    b.Property<string>("Сurrency")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Statement", (string)null);
                });

            modelBuilder.Entity("StatementViewer.Models.AccountInfo", b =>
                {
                    b.HasOne("StatementViewer.Models.AccountUnit", "AccountUnit")
                        .WithMany("AccountsInfo")
                        .HasForeignKey("AccountUnitId");

                    b.Navigation("AccountUnit");
                });

            modelBuilder.Entity("StatementViewer.Models.AccountUnit", b =>
                {
                    b.HasOne("StatementViewer.Models.Statement", "Statement")
                        .WithMany("AccountUnits")
                        .HasForeignKey("StatementId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Statement");
                });

            modelBuilder.Entity("StatementViewer.Models.AccountUnit", b =>
                {
                    b.Navigation("AccountsInfo");
                });

            modelBuilder.Entity("StatementViewer.Models.Statement", b =>
                {
                    b.Navigation("AccountUnits");
                });
#pragma warning restore 612, 618
        }
    }
}
