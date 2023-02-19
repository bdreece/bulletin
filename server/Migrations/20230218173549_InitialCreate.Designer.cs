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
    [Migration("20230218173549_InitialCreate")]
    partial class InitialCreate
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

                    b.ToTable("Documents");
                });

            modelBuilder.Entity("Bulletin.Server.Models.User", b =>
                {
                    b.Property<string>("ID")
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

                    b.Property<string>("Salt")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("ID");

                    b.ToTable("Users");
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

            modelBuilder.Entity("Bulletin.Server.Models.Directory", b =>
                {
                    b.Navigation("Documents");

                    b.Navigation("SubDirectories");
                });

            modelBuilder.Entity("Bulletin.Server.Models.User", b =>
                {
                    b.Navigation("Directories");

                    b.Navigation("Documents");
                });
#pragma warning restore 612, 618
        }
    }
}
