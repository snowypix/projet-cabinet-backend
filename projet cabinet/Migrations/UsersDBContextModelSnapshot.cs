﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using projet_cabinet.Data;

#nullable disable

namespace projet_cabinet.Migrations
{
    [DbContext(typeof(UsersDBContext))]
    partial class UsersDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.23")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("projet_cabinet.Models.Dossier", b =>
                {
                    b.Property<int>("DossierID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DossierID"), 1L, 1);

                    b.Property<int>("MedecinID")
                        .HasColumnType("int");

                    b.Property<int>("PatientID")
                        .HasColumnType("int");

                    b.HasKey("DossierID");

                    b.HasIndex("MedecinID");

                    b.HasIndex("PatientID")
                        .IsUnique();

                    b.ToTable("Dossiers");
                });

            modelBuilder.Entity("projet_cabinet.Models.Examen", b =>
                {
                    b.Property<int>("ExamenID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ExamenID"), 1L, 1);

                    b.Property<int>("DossierID")
                        .HasColumnType("int");

                    b.Property<string>("ExamenDate")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ExamenNom")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Resultat")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ExamenID");

                    b.HasIndex("DossierID");

                    b.ToTable("Examens");
                });

            modelBuilder.Entity("projet_cabinet.Models.Prescription", b =>
                {
                    b.Property<int>("PrescriptionID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PrescriptionID"), 1L, 1);

                    b.Property<int>("DossierID")
                        .HasColumnType("int");

                    b.Property<string>("Medicaments")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PrescriptionDate")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PrescriptionID");

                    b.HasIndex("DossierID");

                    b.ToTable("Prescriptions");
                });

            modelBuilder.Entity("projet_cabinet.Models.RDV", b =>
                {
                    b.Property<int>("RDVId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RDVId"), 1L, 1);

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<TimeSpan>("Heure")
                        .HasColumnType("time");

                    b.Property<int>("MedecinID")
                        .HasColumnType("int");

                    b.Property<int>("PatientID")
                        .HasColumnType("int");

                    b.HasKey("RDVId");

                    b.HasIndex("MedecinID");

                    b.HasIndex("PatientID");

                    b.ToTable("RDVs");
                });

            modelBuilder.Entity("projet_cabinet.Models.User", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"), 1L, 1);

                    b.Property<int>("Age")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Genre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Users");

                    b.HasDiscriminator<string>("UserType").HasValue("User");
                });

            modelBuilder.Entity("projet_cabinet.Models.Infirmier", b =>
                {
                    b.HasBaseType("projet_cabinet.Models.User");

                    b.ToTable("Users");

                    b.HasDiscriminator().HasValue("Infirmier");
                });

            modelBuilder.Entity("projet_cabinet.Models.Medecin", b =>
                {
                    b.HasBaseType("projet_cabinet.Models.User");

                    b.Property<TimeSpan>("HoraireDebut")
                        .HasColumnType("time");

                    b.Property<TimeSpan>("HoraireFin")
                        .HasColumnType("time");

                    b.ToTable("Users");

                    b.HasDiscriminator().HasValue("Medecin");
                });

            modelBuilder.Entity("projet_cabinet.Models.Patient", b =>
                {
                    b.HasBaseType("projet_cabinet.Models.User");

                    b.Property<int>("DossierID")
                        .HasColumnType("int");

                    b.ToTable("Users");

                    b.HasDiscriminator().HasValue("Patient");
                });

            modelBuilder.Entity("projet_cabinet.Models.Dossier", b =>
                {
                    b.HasOne("projet_cabinet.Models.Medecin", "Medecin")
                        .WithMany("Dossiers")
                        .HasForeignKey("MedecinID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("projet_cabinet.Models.Patient", "Patient")
                        .WithOne("Dossier")
                        .HasForeignKey("projet_cabinet.Models.Dossier", "PatientID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Medecin");

                    b.Navigation("Patient");
                });

            modelBuilder.Entity("projet_cabinet.Models.Examen", b =>
                {
                    b.HasOne("projet_cabinet.Models.Dossier", "Dossier")
                        .WithMany("Examens")
                        .HasForeignKey("DossierID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Dossier");
                });

            modelBuilder.Entity("projet_cabinet.Models.Prescription", b =>
                {
                    b.HasOne("projet_cabinet.Models.Dossier", "Dossier")
                        .WithMany("Prescriptions")
                        .HasForeignKey("DossierID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Dossier");
                });

            modelBuilder.Entity("projet_cabinet.Models.RDV", b =>
                {
                    b.HasOne("projet_cabinet.Models.Medecin", "Medecin")
                        .WithMany("RDVs")
                        .HasForeignKey("MedecinID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("projet_cabinet.Models.Patient", "Patient")
                        .WithMany("RDVs")
                        .HasForeignKey("PatientID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Medecin");

                    b.Navigation("Patient");
                });

            modelBuilder.Entity("projet_cabinet.Models.Dossier", b =>
                {
                    b.Navigation("Examens");

                    b.Navigation("Prescriptions");
                });

            modelBuilder.Entity("projet_cabinet.Models.Medecin", b =>
                {
                    b.Navigation("Dossiers");

                    b.Navigation("RDVs");
                });

            modelBuilder.Entity("projet_cabinet.Models.Patient", b =>
                {
                    b.Navigation("Dossier")
                        .IsRequired();

                    b.Navigation("RDVs");
                });
#pragma warning restore 612, 618
        }
    }
}