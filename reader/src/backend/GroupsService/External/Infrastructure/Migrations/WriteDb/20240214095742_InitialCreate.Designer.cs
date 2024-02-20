﻿// <auto-generated />
using System;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infrastructure.Migrations.WriteDb
{
    [DbContext(typeof(WriteDbContext))]
    [Migration("20240214095742_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("BookGroup", b =>
                {
                    b.Property<Guid>("AllowedBooksId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("AllowedInGroupsId")
                        .HasColumnType("uuid");

                    b.HasKey("AllowedBooksId", "AllowedInGroupsId");

                    b.HasIndex("AllowedInGroupsId");

                    b.ToTable("BookGroup");
                });

            modelBuilder.Entity("Domain.Abstractions.Entity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasMaxLength(21)
                        .HasColumnType("character varying(21)");

                    b.HasKey("Id");

                    b.ToTable("Entity");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Entity");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("GroupUser", b =>
                {
                    b.Property<Guid>("GroupsId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("MembersId")
                        .HasColumnType("uuid");

                    b.HasKey("GroupsId", "MembersId");

                    b.HasIndex("MembersId");

                    b.ToTable("GroupUser");
                });

            modelBuilder.Entity("Infrastructure.OutboxMessages.OutboxMessage", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("EntityId")
                        .HasColumnType("uuid");

                    b.Property<int>("EventType")
                        .HasColumnType("integer");

                    b.Property<bool>("IsProcessed")
                        .HasColumnType("boolean");

                    b.HasKey("Id");

                    b.HasIndex("EntityId");

                    b.ToTable("OutboxMessages");
                });

            modelBuilder.Entity("Domain.Models.Book", b =>
                {
                    b.HasBaseType("Domain.Abstractions.Entity");

                    b.Property<string>("AuthorFirstName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("AuthorLastName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("BookName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasDiscriminator().HasValue("Book");
                });

            modelBuilder.Entity("Domain.Models.Group", b =>
                {
                    b.HasBaseType("Domain.Abstractions.Entity");

                    b.Property<Guid>("AdminId")
                        .HasColumnType("uuid");

                    b.Property<string>("GroupName")
                        .HasColumnType("text");

                    b.HasIndex("AdminId")
                        .IsUnique();

                    b.HasDiscriminator().HasValue("Group");
                });

            modelBuilder.Entity("Domain.Models.Note", b =>
                {
                    b.HasBaseType("Domain.Abstractions.Entity");

                    b.Property<int>("NotePosition")
                        .HasColumnType("integer");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("UserBookProgressId")
                        .HasColumnType("uuid");

                    b.HasIndex("UserBookProgressId");

                    b.HasDiscriminator().HasValue("Note");
                });

            modelBuilder.Entity("Domain.Models.User", b =>
                {
                    b.HasBaseType("Domain.Abstractions.Entity");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasDiscriminator().HasValue("User");
                });

            modelBuilder.Entity("Domain.Models.UserBookProgress", b =>
                {
                    b.HasBaseType("Domain.Abstractions.Entity");

                    b.Property<Guid>("BookId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("GroupId")
                        .HasColumnType("uuid");

                    b.Property<int>("LastReadSymbol")
                        .HasColumnType("integer");

                    b.Property<int>("Progress")
                        .HasColumnType("integer");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasIndex("BookId");

                    b.HasIndex("GroupId");

                    b.HasIndex("UserId");

                    b.HasDiscriminator().HasValue("UserBookProgress");
                });

            modelBuilder.Entity("BookGroup", b =>
                {
                    b.HasOne("Domain.Models.Book", null)
                        .WithMany()
                        .HasForeignKey("AllowedBooksId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Models.Group", null)
                        .WithMany()
                        .HasForeignKey("AllowedInGroupsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("GroupUser", b =>
                {
                    b.HasOne("Domain.Models.Group", null)
                        .WithMany()
                        .HasForeignKey("GroupsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Models.User", null)
                        .WithMany()
                        .HasForeignKey("MembersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Infrastructure.OutboxMessages.OutboxMessage", b =>
                {
                    b.HasOne("Domain.Abstractions.Entity", "Entity")
                        .WithMany()
                        .HasForeignKey("EntityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Entity");
                });

            modelBuilder.Entity("Domain.Models.Group", b =>
                {
                    b.HasOne("Domain.Models.User", "Admin")
                        .WithOne("AdminGroup")
                        .HasForeignKey("Domain.Models.Group", "AdminId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Admin");
                });

            modelBuilder.Entity("Domain.Models.Note", b =>
                {
                    b.HasOne("Domain.Models.UserBookProgress", "UserBookProgress")
                        .WithMany("UserNotes")
                        .HasForeignKey("UserBookProgressId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("UserBookProgress");
                });

            modelBuilder.Entity("Domain.Models.UserBookProgress", b =>
                {
                    b.HasOne("Domain.Models.Book", "Book")
                        .WithMany("UserBookProgress")
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Models.Group", "Group")
                        .WithMany("GroupProgresses")
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Models.User", "User")
                        .WithMany("UserBookProgresses")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Book");

                    b.Navigation("Group");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Domain.Models.Book", b =>
                {
                    b.Navigation("UserBookProgress");
                });

            modelBuilder.Entity("Domain.Models.Group", b =>
                {
                    b.Navigation("GroupProgresses");
                });

            modelBuilder.Entity("Domain.Models.User", b =>
                {
                    b.Navigation("AdminGroup");

                    b.Navigation("UserBookProgresses");
                });

            modelBuilder.Entity("Domain.Models.UserBookProgress", b =>
                {
                    b.Navigation("UserNotes");
                });
#pragma warning restore 612, 618
        }
    }
}
