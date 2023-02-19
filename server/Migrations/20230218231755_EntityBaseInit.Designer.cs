﻿// <auto-generated />
using System;
using Bulletin.Server.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Bulletin.Server.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20230218231755_EntityBaseInit")]
    partial class EntityBaseInit
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.3");

            modelBuilder.Entity("Bulletin.Server.Models.Directory", b =>
                {
                    b.Property<string>("ID")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("DateLastUpdated")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("ParentDirectoryID")
                        .HasColumnType("TEXT");

                    b.Property<string>("UserID")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("ID");

                    b.HasIndex("ParentDirectoryID");

                    b.HasIndex("UserID");

                    b.ToTable("Directory");
                });

            modelBuilder.Entity("Bulletin.Server.Models.Document", b =>
                {
                    b.Property<string>("ID")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("DateLastUpdated")
                        .HasColumnType("TEXT");

                    b.Property<string>("DirectoryID")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Filename")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("UserID")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("ID");

                    b.HasIndex("DirectoryID");

                    b.HasIndex("UserID");

                    b.ToTable("Document");
                });

            modelBuilder.Entity("Bulletin.Server.Models.Role", b =>
                {
                    b.Property<string>("ID")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("DateLastUpdated")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("ID");

                    b.ToTable("Role");

                    b.HasData(
                        new
                        {
                            ID = "036a7855-3fc1-4ea2-8862-2a957ca54759",
                            DateCreated = new DateTime(2023, 2, 18, 23, 17, 54, 935, DateTimeKind.Utc).AddTicks(7196),
                            DateLastUpdated = new DateTime(2023, 2, 18, 23, 17, 54, 935, DateTimeKind.Utc).AddTicks(7196),
                            Name = "admin"
                        },
                        new
                        {
                            ID = "50ba0e85-8889-4ea9-86cd-a1ed5f99b99c",
                            DateCreated = new DateTime(2023, 2, 18, 23, 17, 54, 935, DateTimeKind.Utc).AddTicks(7219),
                            DateLastUpdated = new DateTime(2023, 2, 18, 23, 17, 54, 935, DateTimeKind.Utc).AddTicks(7219),
                            Name = "user"
                        });
                });

            modelBuilder.Entity("Bulletin.Server.Models.User", b =>
                {
                    b.Property<string>("ID")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("DateLastUpdated")
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Hash")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("SecurityToken")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("ID");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("User");
                });

            modelBuilder.Entity("Bulletin.Server.Models.UserRole", b =>
                {
                    b.Property<string>("ID")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("DateLastUpdated")
                        .HasColumnType("TEXT");

                    b.Property<string>("RoleID")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("UserID")
                        .IsRequired()
                        .HasColumnType("TEXT");

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
