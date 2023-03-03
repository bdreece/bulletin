﻿// <auto-generated />
using System;
using Bulletin.Server.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Bulletin.Server.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20230302022357_PostgresMigration")]
    partial class PostgresMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Bulletin.Server.Models.Directory", b =>
                {
                    b.Property<string>("ID")
                        .HasColumnType("text");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("DateLastUpdated")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ParentDirectoryID")
                        .HasColumnType("text");

                    b.Property<string>("UserID")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("ID");

                    b.HasIndex("ParentDirectoryID");

                    b.HasIndex("UserID");

                    b.ToTable("Directory");
                });

            modelBuilder.Entity("Bulletin.Server.Models.Document", b =>
                {
                    b.Property<string>("ID")
                        .HasColumnType("text");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("DateLastUpdated")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("DirectoryID")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Filename")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("UserID")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("ID");

                    b.HasIndex("DirectoryID");

                    b.HasIndex("UserID");

                    b.ToTable("Document");
                });

            modelBuilder.Entity("Bulletin.Server.Models.Role", b =>
                {
                    b.Property<string>("ID")
                        .HasColumnType("text");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("DateLastUpdated")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("ID");

                    b.ToTable("Role");

                    b.HasData(
                        new
                        {
                            ID = "f3921644-8b54-41a8-ada7-5856b0eaecfc",
                            DateCreated = new DateTime(2023, 3, 2, 2, 23, 57, 270, DateTimeKind.Utc).AddTicks(1140),
                            DateLastUpdated = new DateTime(2023, 3, 2, 2, 23, 57, 270, DateTimeKind.Utc).AddTicks(1141),
                            Name = "admin"
                        },
                        new
                        {
                            ID = "9f62f8a2-f7f0-4b06-b68e-c12ec5e56e5d",
                            DateCreated = new DateTime(2023, 3, 2, 2, 23, 57, 270, DateTimeKind.Utc).AddTicks(1163),
                            DateLastUpdated = new DateTime(2023, 3, 2, 2, 23, 57, 270, DateTimeKind.Utc).AddTicks(1163),
                            Name = "user"
                        });
                });

            modelBuilder.Entity("Bulletin.Server.Models.User", b =>
                {
                    b.Property<string>("ID")
                        .HasColumnType("text");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("DateLastUpdated")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Hash")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("SecurityToken")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("ID");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("User");
                });

            modelBuilder.Entity("Bulletin.Server.Models.UserRole", b =>
                {
                    b.Property<string>("ID")
                        .HasColumnType("text");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("DateLastUpdated")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("RoleID")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("UserID")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("ID");

                    b.HasIndex("RoleID");

                    b.HasIndex("UserID");

                    b.ToTable("UserRole");
                });

            modelBuilder.Entity("Bulletin.Server.Models.Directory", b =>
                {
                    b.HasOne("Bulletin.Server.Models.Directory", "ParentDirectory")
                        .WithMany("SubDirectories")
                        .HasForeignKey("ParentDirectoryID");

                    b.HasOne("Bulletin.Server.Models.User", "User")
                        .WithMany("Directories")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ParentDirectory");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Bulletin.Server.Models.Document", b =>
                {
                    b.HasOne("Bulletin.Server.Models.Directory", "Directory")
                        .WithMany("Documents")
                        .HasForeignKey("DirectoryID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Bulletin.Server.Models.User", "User")
                        .WithMany("Documents")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Directory");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Bulletin.Server.Models.UserRole", b =>
                {
                    b.HasOne("Bulletin.Server.Models.Role", "Role")
                        .WithMany()
                        .HasForeignKey("RoleID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Bulletin.Server.Models.User", "User")
                        .WithMany("Roles")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Bulletin.Server.Models.Directory", b =>
                {
                    b.Navigation("Documents");

                    b.Navigation("SubDirectories");
                });

            modelBuilder.Entity("Bulletin.Server.Models.User", b =>
                {
                    b.Navigation("Directories");

                    b.Navigation("Documents");

                    b.Navigation("Roles");
                });
#pragma warning restore 612, 618
        }
    }
}