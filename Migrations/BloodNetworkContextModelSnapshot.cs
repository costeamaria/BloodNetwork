﻿// <auto-generated />
using System;
using BloodNetwork.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BloodNetwork.Migrations
{
    [DbContext(typeof(BloodNetworkContext))]
    partial class BloodNetworkContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.15")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("BloodNetwork.Models.Adress", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"), 1L, 1);

                    b.Property<string>("AdressName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Adress");
                });

            modelBuilder.Entity("BloodNetwork.Models.Appointment", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"), 1L, 1);

                    b.Property<DateTime>("AppointmentDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("AppointmentTime")
                        .HasColumnType("datetime2");

                    b.Property<int?>("ClinicID")
                        .HasColumnType("int");

                    b.Property<int?>("MemberID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("ClinicID");

                    b.HasIndex("MemberID");

                    b.ToTable("Appointment");
                });

            modelBuilder.Entity("BloodNetwork.Models.Category", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"), 1L, 1);

                    b.Property<string>("CategoryName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Category");
                });

            modelBuilder.Entity("BloodNetwork.Models.Clinic", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"), 1L, 1);

                    b.Property<int?>("AdressID")
                        .HasColumnType("int");

                    b.Property<int?>("DoctorID")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Phone")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("Quantity")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("ID");

                    b.HasIndex("AdressID");

                    b.HasIndex("DoctorID");

                    b.ToTable("Clinic");
                });

            modelBuilder.Entity("BloodNetwork.Models.ClinicCategory", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"), 1L, 1);

                    b.Property<int>("CategoryID")
                        .HasColumnType("int");

                    b.Property<int>("ClinicID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("CategoryID");

                    b.HasIndex("ClinicID");

                    b.ToTable("ClinicCategory");
                });

            modelBuilder.Entity("BloodNetwork.Models.Doctor", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"), 1L, 1);

                    b.Property<string>("DoctorName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Doctor");
                });

            modelBuilder.Entity("BloodNetwork.Models.Member", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"), 1L, 1);

                    b.Property<string>("Adress")
                        .HasMaxLength(70)
                        .HasColumnType("nvarchar(70)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("LastName")
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("Phone")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Member");
                });

            modelBuilder.Entity("BloodNetwork.Models.Appointment", b =>
                {
                    b.HasOne("BloodNetwork.Models.Clinic", "Clinic")
                        .WithMany()
                        .HasForeignKey("ClinicID");

                    b.HasOne("BloodNetwork.Models.Member", "Member")
                        .WithMany("Appointments")
                        .HasForeignKey("MemberID");

                    b.Navigation("Clinic");

                    b.Navigation("Member");
                });

            modelBuilder.Entity("BloodNetwork.Models.Clinic", b =>
                {
                    b.HasOne("BloodNetwork.Models.Adress", "Adress")
                        .WithMany("Clinics")
                        .HasForeignKey("AdressID");

                    b.HasOne("BloodNetwork.Models.Doctor", "Doctor")
                        .WithMany("Clinics")
                        .HasForeignKey("DoctorID");

                    b.Navigation("Adress");

                    b.Navigation("Doctor");
                });

            modelBuilder.Entity("BloodNetwork.Models.ClinicCategory", b =>
                {
                    b.HasOne("BloodNetwork.Models.Category", "Category")
                        .WithMany("ClinicCategories")
                        .HasForeignKey("CategoryID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BloodNetwork.Models.Clinic", "Clinic")
                        .WithMany("ClinicCategories")
                        .HasForeignKey("ClinicID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("Clinic");
                });

            modelBuilder.Entity("BloodNetwork.Models.Adress", b =>
                {
                    b.Navigation("Clinics");
                });

            modelBuilder.Entity("BloodNetwork.Models.Category", b =>
                {
                    b.Navigation("ClinicCategories");
                });

            modelBuilder.Entity("BloodNetwork.Models.Clinic", b =>
                {
                    b.Navigation("ClinicCategories");
                });

            modelBuilder.Entity("BloodNetwork.Models.Doctor", b =>
                {
                    b.Navigation("Clinics");
                });

            modelBuilder.Entity("BloodNetwork.Models.Member", b =>
                {
                    b.Navigation("Appointments");
                });
#pragma warning restore 612, 618
        }
    }
}
