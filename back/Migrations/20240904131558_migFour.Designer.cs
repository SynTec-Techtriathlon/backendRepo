﻿// <auto-generated />
using System;
using Back;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Back.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240904131558_migFour")]
    partial class migFour
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Back.Models.Applicant", b =>
                {
                    b.Property<string>("NIC")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Nationality")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("BirthPlace")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Height")
                        .HasColumnType("int");

                    b.Property<string>("Occupation")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OccupationAddress")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TelNo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("NIC", "Nationality");

                    b.ToTable("Applicants");
                });

            modelBuilder.Entity("Back.Models.Application", b =>
                {
                    b.Property<int>("No")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("No"));

                    b.Property<int>("AmountOfMoney")
                        .HasColumnType("int");

                    b.Property<string>("ApplicantNIC")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ApplicantNationality")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("ArrivalDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("MoneyType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Period")
                        .HasColumnType("int");

                    b.Property<string>("Purpose")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Route")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Status")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TravelMode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("No");

                    b.HasIndex("ApplicantNIC", "ApplicantNationality");

                    b.ToTable("Applications");
                });

            modelBuilder.Entity("Back.Models.History", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ApplicantNIC")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ApplicantNationality")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("DateLeaving")
                        .HasColumnType("datetime2");

                    b.Property<string>("LastLocation")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("VisaIssuedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("VisaType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("VisaValidityPeriod")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ApplicantNIC", "ApplicantNationality");

                    b.ToTable("Histories");
                });

            modelBuilder.Entity("Back.Models.Passport", b =>
                {
                    b.Property<string>("Country")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ApplicantNIC")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ApplicantNationality")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("DateOfExpire")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateOfIssue")
                        .HasColumnType("datetime2");

                    b.HasKey("Country", "Id");

                    b.HasIndex("ApplicantNIC", "ApplicantNationality")
                        .IsUnique();

                    b.ToTable("Passports");
                });

            modelBuilder.Entity("Back.Models.ReportData", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("ReportData");
                });

            modelBuilder.Entity("Back.Models.Spouse", b =>
                {
                    b.Property<string>("ApplicantNIC")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ApplicantNationality")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("SpouseNIC")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ApplicantNIC", "ApplicantNationality", "SpouseNIC");

                    b.HasIndex("ApplicantNIC", "ApplicantNationality")
                        .IsUnique();

                    b.ToTable("Spouses");
                });

            modelBuilder.Entity("Back.Models.Application", b =>
                {
                    b.HasOne("Back.Models.Applicant", "Applicant")
                        .WithMany("Applications")
                        .HasForeignKey("ApplicantNIC", "ApplicantNationality")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Applicant");
                });

            modelBuilder.Entity("Back.Models.History", b =>
                {
                    b.HasOne("Back.Models.Applicant", "Applicant")
                        .WithMany("Histories")
                        .HasForeignKey("ApplicantNIC", "ApplicantNationality")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Applicant");
                });

            modelBuilder.Entity("Back.Models.Passport", b =>
                {
                    b.HasOne("Back.Models.Applicant", "Applicant")
                        .WithOne("Passport")
                        .HasForeignKey("Back.Models.Passport", "ApplicantNIC", "ApplicantNationality")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Applicant");
                });

            modelBuilder.Entity("Back.Models.Spouse", b =>
                {
                    b.HasOne("Back.Models.Applicant", "Applicant")
                        .WithOne("Spouse")
                        .HasForeignKey("Back.Models.Spouse", "ApplicantNIC", "ApplicantNationality")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Applicant");
                });

            modelBuilder.Entity("Back.Models.Applicant", b =>
                {
                    b.Navigation("Applications");

                    b.Navigation("Histories");

                    b.Navigation("Passport")
                        .IsRequired();

                    b.Navigation("Spouse")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
