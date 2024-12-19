﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace FormulaOne.Infrastructure.Migrations
{
    [DbContext(typeof(FormulaOneDbContext))]
    partial class FormulaOneDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("FormulaOne.Infrastructure.Data.DataAccessObjects.CircuitDao", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Circuits");
                });

            modelBuilder.Entity("FormulaOne.Infrastructure.Data.DataAccessObjects.DriverDao", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nationality")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Drivers");
                });

            modelBuilder.Entity("FormulaOne.Infrastructure.Data.DataAccessObjects.DriverStandingDao", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("DriverId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<float>("Points")
                        .HasColumnType("real");

                    b.Property<int>("Position")
                        .HasColumnType("int");

                    b.Property<Guid>("TeamId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Year")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("DriverId");

                    b.HasIndex("TeamId");

                    b.ToTable("DriverStandings");
                });

            modelBuilder.Entity("FormulaOne.Infrastructure.Data.DataAccessObjects.RaceResultDao", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CircuitId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateOnly>("Date")
                        .HasColumnType("date");

                    b.Property<Guid>("DriverId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Laps")
                        .HasColumnType("int");

                    b.Property<float>("Points")
                        .HasColumnType("real");

                    b.Property<int>("Position")
                        .HasColumnType("int");

                    b.Property<Guid>("TeamId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<TimeSpan>("Time")
                        .HasColumnType("time");

                    b.Property<int>("Year")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CircuitId");

                    b.HasIndex("DriverId");

                    b.HasIndex("TeamId");

                    b.ToTable("RaceResults");
                });

            modelBuilder.Entity("FormulaOne.Infrastructure.Data.DataAccessObjects.TeamDao", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Teams");
                });

            modelBuilder.Entity("FormulaOne.Infrastructure.Data.DataAccessObjects.TeamStandingDao", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<float>("Points")
                        .HasColumnType("real");

                    b.Property<int>("Position")
                        .HasColumnType("int");

                    b.Property<Guid>("TeamId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Year")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TeamId");

                    b.ToTable("TeamStandings");
                });

            modelBuilder.Entity("FormulaOne.Infrastructure.Data.DataAccessObjects.DriverStandingDao", b =>
                {
                    b.HasOne("FormulaOne.Infrastructure.Data.DataAccessObjects.DriverDao", "Driver")
                        .WithMany("DriverStandings")
                        .HasForeignKey("DriverId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FormulaOne.Infrastructure.Data.DataAccessObjects.TeamDao", "Team")
                        .WithMany()
                        .HasForeignKey("TeamId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Driver");

                    b.Navigation("Team");
                });

            modelBuilder.Entity("FormulaOne.Infrastructure.Data.DataAccessObjects.RaceResultDao", b =>
                {
                    b.HasOne("FormulaOne.Infrastructure.Data.DataAccessObjects.CircuitDao", "Circuit")
                        .WithMany("RaceResults")
                        .HasForeignKey("CircuitId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FormulaOne.Infrastructure.Data.DataAccessObjects.DriverDao", "Driver")
                        .WithMany("RaceResults")
                        .HasForeignKey("DriverId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FormulaOne.Infrastructure.Data.DataAccessObjects.TeamDao", "Team")
                        .WithMany("RaceResults")
                        .HasForeignKey("TeamId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Circuit");

                    b.Navigation("Driver");

                    b.Navigation("Team");
                });

            modelBuilder.Entity("FormulaOne.Infrastructure.Data.DataAccessObjects.TeamStandingDao", b =>
                {
                    b.HasOne("FormulaOne.Infrastructure.Data.DataAccessObjects.TeamDao", "Team")
                        .WithMany("TeamStandings")
                        .HasForeignKey("TeamId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Team");
                });

            modelBuilder.Entity("FormulaOne.Infrastructure.Data.DataAccessObjects.CircuitDao", b =>
                {
                    b.Navigation("RaceResults");
                });

            modelBuilder.Entity("FormulaOne.Infrastructure.Data.DataAccessObjects.DriverDao", b =>
                {
                    b.Navigation("DriverStandings");

                    b.Navigation("RaceResults");
                });

            modelBuilder.Entity("FormulaOne.Infrastructure.Data.DataAccessObjects.TeamDao", b =>
                {
                    b.Navigation("RaceResults");

                    b.Navigation("TeamStandings");
                });
#pragma warning restore 612, 618
        }
    }
}
