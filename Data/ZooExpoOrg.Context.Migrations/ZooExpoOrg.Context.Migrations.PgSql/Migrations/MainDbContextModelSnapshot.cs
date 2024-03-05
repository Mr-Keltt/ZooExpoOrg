﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using ZooExpoOrg.Context;

#nullable disable

namespace ZooExpoOrg.Context.Migrations.PgSql.Migrations
{
    [DbContext(typeof(MainDbContext))]
    partial class MainDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.16")
                .HasAnnotation("Proxies:ChangeTracking", false)
                .HasAnnotation("Proxies:CheckEquality", false)
                .HasAnnotation("Proxies:LazyLoading", true)
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("ExpositionUser", b =>
                {
                    b.Property<int>("SubscribersId")
                        .HasColumnType("integer");

                    b.Property<int>("SubscriptionsId")
                        .HasColumnType("integer");

                    b.HasKey("SubscribersId", "SubscriptionsId");

                    b.HasIndex("SubscriptionsId");

                    b.ToTable("exposition_subscribers", (string)null);
                });

            modelBuilder.Entity("ZooExpoOrg.Context.Entities.Achievement", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("AnimalId")
                        .HasColumnType("integer");

                    b.Property<int>("ConfirmationAchievementId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("DateAward")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("uid")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("AnimalId");

                    b.HasIndex("uid")
                        .IsUnique();

                    b.ToTable("achievements", (string)null);
                });

            modelBuilder.Entity("ZooExpoOrg.Context.Entities.Animal", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    b.Property<int>("AnimalSpecieId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Breed")
                        .HasColumnType("text");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int?>("Height")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.Property<int?>("Weight")
                        .HasColumnType("integer");

                    b.Property<int>("uid")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("AnimalSpecieId");

                    b.HasIndex("UserId");

                    b.HasIndex("uid")
                        .IsUnique();

                    b.ToTable("animals", (string)null);
                });

            modelBuilder.Entity("ZooExpoOrg.Context.Entities.AnimalPhoto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("AnimalId")
                        .HasColumnType("integer");

                    b.Property<byte[]>("ImageData")
                        .IsRequired()
                        .HasColumnType("bytea");

                    b.Property<string>("ImageMimeType")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("uid")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("AnimalId");

                    b.HasIndex("uid")
                        .IsUnique();

                    b.ToTable("animal_photo", (string)null);
                });

            modelBuilder.Entity("ZooExpoOrg.Context.Entities.AnimalSpecie", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("uid")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("uid")
                        .IsUnique();

                    b.ToTable("animals_species", (string)null);
                });

            modelBuilder.Entity("ZooExpoOrg.Context.Entities.Common.Comment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("AuthorId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("DateWriting")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("uid")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.HasIndex("uid")
                        .IsUnique();

                    b.ToTable("expositions_comments", (string)null);

                    b.HasDiscriminator<string>("Discriminator").HasValue("Comment");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("ZooExpoOrg.Context.Entities.ConfirmationAchievement", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("AchievementId")
                        .HasColumnType("integer");

                    b.Property<byte[]>("FileContent")
                        .IsRequired()
                        .HasColumnType("bytea");

                    b.Property<string>("FileName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("uid")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("AchievementId")
                        .IsUnique();

                    b.HasIndex("uid")
                        .IsUnique();

                    b.ToTable("confirmations_achievements", (string)null);
                });

            modelBuilder.Entity("ZooExpoOrg.Context.Entities.Exposition", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("DateEnd")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("DateStart")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("HouseNumber")
                        .HasColumnType("text");

                    b.Property<int>("OrganizersId")
                        .HasColumnType("integer");

                    b.Property<string>("Street")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("uid")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("OrganizersId");

                    b.HasIndex("uid")
                        .IsUnique();

                    b.ToTable("expositions", (string)null);
                });

            modelBuilder.Entity("ZooExpoOrg.Context.Entities.ExpositionPhoto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("ExpositionId")
                        .HasColumnType("integer");

                    b.Property<byte[]>("ImageData")
                        .IsRequired()
                        .HasColumnType("bytea");

                    b.Property<string>("ImageMimeType")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("uid")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("ExpositionId");

                    b.HasIndex("uid")
                        .IsUnique();

                    b.ToTable("expositions_photos", (string)null);
                });

            modelBuilder.Entity("ZooExpoOrg.Context.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("BirthDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Patronymic")
                        .HasColumnType("text");

                    b.Property<int?>("PhotoId")
                        .HasColumnType("integer");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("uid")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("PhotoId")
                        .IsUnique();

                    b.HasIndex("uid")
                        .IsUnique();

                    b.ToTable("users", (string)null);
                });

            modelBuilder.Entity("ZooExpoOrg.Context.Entities.UserPhoto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<byte[]>("ImageData")
                        .IsRequired()
                        .HasColumnType("bytea");

                    b.Property<string>("ImageMimeType")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.Property<int>("uid")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("uid")
                        .IsUnique();

                    b.ToTable("users_photos", (string)null);
                });

            modelBuilder.Entity("ZooExpoOrg.Context.Entities.AnimalComment", b =>
                {
                    b.HasBaseType("ZooExpoOrg.Context.Entities.Common.Comment");

                    b.Property<int>("AnimalId")
                        .HasColumnType("integer");

                    b.HasIndex("AnimalId");

                    b.HasDiscriminator().HasValue("AnimalComment");
                });

            modelBuilder.Entity("ZooExpoOrg.Context.Entities.ExpositionComment", b =>
                {
                    b.HasBaseType("ZooExpoOrg.Context.Entities.Common.Comment");

                    b.Property<int>("ExpositionId")
                        .HasColumnType("integer");

                    b.HasIndex("ExpositionId");

                    b.HasDiscriminator().HasValue("ExpositionComment");
                });

            modelBuilder.Entity("ExpositionUser", b =>
                {
                    b.HasOne("ZooExpoOrg.Context.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("SubscribersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ZooExpoOrg.Context.Entities.Exposition", null)
                        .WithMany()
                        .HasForeignKey("SubscriptionsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ZooExpoOrg.Context.Entities.Achievement", b =>
                {
                    b.HasOne("ZooExpoOrg.Context.Entities.Animal", "Animal")
                        .WithMany("Achievements")
                        .HasForeignKey("AnimalId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Animal");
                });

            modelBuilder.Entity("ZooExpoOrg.Context.Entities.Animal", b =>
                {
                    b.HasOne("ZooExpoOrg.Context.Entities.AnimalSpecie", "AnimalSpecie")
                        .WithMany()
                        .HasForeignKey("AnimalSpecieId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ZooExpoOrg.Context.Entities.Exposition", null)
                        .WithMany("Participants")
                        .HasForeignKey("Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ZooExpoOrg.Context.Entities.User", "User")
                        .WithMany("Animals")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AnimalSpecie");

                    b.Navigation("User");
                });

            modelBuilder.Entity("ZooExpoOrg.Context.Entities.AnimalPhoto", b =>
                {
                    b.HasOne("ZooExpoOrg.Context.Entities.Animal", "Animal")
                        .WithMany("Photos")
                        .HasForeignKey("AnimalId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Animal");
                });

            modelBuilder.Entity("ZooExpoOrg.Context.Entities.AnimalSpecie", b =>
                {
                    b.HasOne("ZooExpoOrg.Context.Entities.Exposition", null)
                        .WithMany("AnimalsSpecies")
                        .HasForeignKey("Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ZooExpoOrg.Context.Entities.Common.Comment", b =>
                {
                    b.HasOne("ZooExpoOrg.Context.Entities.User", "User")
                        .WithMany("Comments")
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("ZooExpoOrg.Context.Entities.ConfirmationAchievement", b =>
                {
                    b.HasOne("ZooExpoOrg.Context.Entities.Achievement", "Achievement")
                        .WithOne("ConfirmationAchievement")
                        .HasForeignKey("ZooExpoOrg.Context.Entities.ConfirmationAchievement", "AchievementId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Achievement");
                });

            modelBuilder.Entity("ZooExpoOrg.Context.Entities.Exposition", b =>
                {
                    b.HasOne("ZooExpoOrg.Context.Entities.User", "User")
                        .WithMany("OrganizedExpositions")
                        .HasForeignKey("OrganizersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("ZooExpoOrg.Context.Entities.ExpositionPhoto", b =>
                {
                    b.HasOne("ZooExpoOrg.Context.Entities.Exposition", "Exposition")
                        .WithMany("Photos")
                        .HasForeignKey("ExpositionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Exposition");
                });

            modelBuilder.Entity("ZooExpoOrg.Context.Entities.User", b =>
                {
                    b.HasOne("ZooExpoOrg.Context.Entities.UserPhoto", "Photo")
                        .WithOne("User")
                        .HasForeignKey("ZooExpoOrg.Context.Entities.User", "PhotoId");

                    b.Navigation("Photo");
                });

            modelBuilder.Entity("ZooExpoOrg.Context.Entities.AnimalComment", b =>
                {
                    b.HasOne("ZooExpoOrg.Context.Entities.Animal", "Animal")
                        .WithMany("Comments")
                        .HasForeignKey("AnimalId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Animal");
                });

            modelBuilder.Entity("ZooExpoOrg.Context.Entities.ExpositionComment", b =>
                {
                    b.HasOne("ZooExpoOrg.Context.Entities.Exposition", "Exposition")
                        .WithMany("Comments")
                        .HasForeignKey("ExpositionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Exposition");
                });

            modelBuilder.Entity("ZooExpoOrg.Context.Entities.Achievement", b =>
                {
                    b.Navigation("ConfirmationAchievement")
                        .IsRequired();
                });

            modelBuilder.Entity("ZooExpoOrg.Context.Entities.Animal", b =>
                {
                    b.Navigation("Achievements");

                    b.Navigation("Comments");

                    b.Navigation("Photos");
                });

            modelBuilder.Entity("ZooExpoOrg.Context.Entities.Exposition", b =>
                {
                    b.Navigation("AnimalsSpecies");

                    b.Navigation("Comments");

                    b.Navigation("Participants");

                    b.Navigation("Photos");
                });

            modelBuilder.Entity("ZooExpoOrg.Context.Entities.User", b =>
                {
                    b.Navigation("Animals");

                    b.Navigation("Comments");

                    b.Navigation("OrganizedExpositions");
                });

            modelBuilder.Entity("ZooExpoOrg.Context.Entities.UserPhoto", b =>
                {
                    b.Navigation("User")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
