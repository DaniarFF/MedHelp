﻿// <auto-generated />
using System;
using MedHelp.DBase;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace MedHelp.DBase.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("MedHelp.Core.Entities.DiseaseDrugEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("DiseaseId")
                        .HasColumnType("integer");

                    b.Property<int>("DrugId")
                        .HasColumnType("integer");

                    b.Property<string>("Peculiarity")
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)");

                    b.HasKey("Id");

                    b.HasIndex("DiseaseId");

                    b.HasIndex("DrugId");

                    b.ToTable("DiseaseDrugs");
                });

            modelBuilder.Entity("MedHelp.Core.Entities.DiseaseEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("DistinctiveSigns")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Recomendations")
                        .HasMaxLength(500)
                        .HasColumnType("character varying(500)");

                    b.Property<string>("Symptoms")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("character varying(500)");

                    b.HasKey("Id");

                    b.ToTable("Diseases");
                });

            modelBuilder.Entity("MedHelp.Core.Entities.DrugEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int?>("GroupId")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<string>("Recipe")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("character varying(500)");

                    b.Property<string>("RlsLink")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("GroupId");

                    b.ToTable("Drugs");
                });

            modelBuilder.Entity("MedHelp.Core.Entities.DrugGroupEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("DrugsGroups");
                });

            modelBuilder.Entity("MedHelp.Core.Entities.TreatmentTemplateDrugEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<int>("DrugId")
                        .HasColumnType("integer");

                    b.Property<int>("TreatmentTemplateId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("DrugId");

                    b.HasIndex("TreatmentTemplateId");

                    b.ToTable("TreatmentsTemplatesDrugs");
                });

            modelBuilder.Entity("MedHelp.Core.Entities.TreatmentTemplateEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("DiseaseId")
                        .HasColumnType("integer");

                    b.Property<string>("TemplateName")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("DiseaseId");

                    b.HasIndex("UserId");

                    b.ToTable("TreatmentsTemplates");
                });

            modelBuilder.Entity("MedHelp.Core.Entities.UserEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<long>("TelegramId")
                        .HasColumnType("bigint");

                    b.Property<string>("TelegramUserName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("MedHelp.Core.Entities.DiseaseDrugEntity", b =>
                {
                    b.HasOne("MedHelp.Core.Entities.DiseaseEntity", "Disease")
                        .WithMany("DiseaseDrugs")
                        .HasForeignKey("DiseaseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MedHelp.Core.Entities.DrugEntity", "Drug")
                        .WithMany("DiseaseDrugs")
                        .HasForeignKey("DrugId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Disease");

                    b.Navigation("Drug");
                });

            modelBuilder.Entity("MedHelp.Core.Entities.DrugEntity", b =>
                {
                    b.HasOne("MedHelp.Core.Entities.DrugGroupEntity", "DrugGroup")
                        .WithMany("Drugs")
                        .HasForeignKey("GroupId");

                    b.Navigation("DrugGroup");
                });

            modelBuilder.Entity("MedHelp.Core.Entities.TreatmentTemplateDrugEntity", b =>
                {
                    b.HasOne("MedHelp.Core.Entities.DrugEntity", "Drug")
                        .WithMany()
                        .HasForeignKey("DrugId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MedHelp.Core.Entities.TreatmentTemplateEntity", "TreatmentTemplate")
                        .WithMany("TreatmentDrugs")
                        .HasForeignKey("TreatmentTemplateId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Drug");

                    b.Navigation("TreatmentTemplate");
                });

            modelBuilder.Entity("MedHelp.Core.Entities.TreatmentTemplateEntity", b =>
                {
                    b.HasOne("MedHelp.Core.Entities.DiseaseEntity", "Disease")
                        .WithMany("TreatmentTemplates")
                        .HasForeignKey("DiseaseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MedHelp.Core.Entities.UserEntity", "User")
                        .WithMany("TreatmentTemplates")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Disease");

                    b.Navigation("User");
                });

            modelBuilder.Entity("MedHelp.Core.Entities.DiseaseEntity", b =>
                {
                    b.Navigation("DiseaseDrugs");

                    b.Navigation("TreatmentTemplates");
                });

            modelBuilder.Entity("MedHelp.Core.Entities.DrugEntity", b =>
                {
                    b.Navigation("DiseaseDrugs");
                });

            modelBuilder.Entity("MedHelp.Core.Entities.DrugGroupEntity", b =>
                {
                    b.Navigation("Drugs");
                });

            modelBuilder.Entity("MedHelp.Core.Entities.TreatmentTemplateEntity", b =>
                {
                    b.Navigation("TreatmentDrugs");
                });

            modelBuilder.Entity("MedHelp.Core.Entities.UserEntity", b =>
                {
                    b.Navigation("TreatmentTemplates");
                });
#pragma warning restore 612, 618
        }
    }
}
