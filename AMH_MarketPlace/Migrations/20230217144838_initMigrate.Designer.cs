﻿// <auto-generated />
using System;
using AMH_MarketPlace.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace AMH_MarketPlace.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20230217144838_initMigrate")]
    partial class initMigrate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("AMH_MarketPlace.Entities.User.Credential", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("id");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("Varchar(100)")
                        .HasColumnName("email");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(2147483647)
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("password");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("role_id");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("RoleId");

                    b.ToTable("m_credential");
                });

            modelBuilder.Entity("AMH_MarketPlace.Entities.User.Role", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("Varchar(10)")
                        .HasColumnName("name");

                    b.HasKey("Id");

                    b.ToTable("m_role");
                });

            modelBuilder.Entity("AMH_MarketPlace.Entities.User.SubUser.Address", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("id");

                    b.Property<string>("Address1")
                        .HasColumnType("Varchar(255)")
                        .HasColumnName("address1");

                    b.Property<string>("Address2")
                        .HasColumnType("Varchar(255)")
                        .HasColumnName("address2");

                    b.Property<string>("City")
                        .HasColumnType("Varchar(255)")
                        .HasColumnName("city");

                    b.Property<string>("PostCode")
                        .HasColumnType("Varchar(5)")
                        .HasColumnName("post_code");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("user_id");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("m_address");
                });

            modelBuilder.Entity("AMH_MarketPlace.Entities.User.SubUser.Notifications.CategoryNotification", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("Varchar(10)")
                        .HasColumnName("name");

                    b.HasKey("Id");

                    b.ToTable("m_category_notif");
                });

            modelBuilder.Entity("AMH_MarketPlace.Entities.User.SubUser.Notifications.NotifRead", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("id");

                    b.Property<bool>("IsRead")
                        .HasColumnType("bit")
                        .HasColumnName("is_read");

                    b.HasKey("Id");

                    b.ToTable("m_notif_read");
                });

            modelBuilder.Entity("AMH_MarketPlace.Entities.User.SubUser.Notifications.Notification", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("id");

                    b.Property<string>("Body")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("body");

                    b.Property<Guid>("CategoryNotificationId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("category_notification_id");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2")
                        .HasColumnName("created_at");

                    b.Property<Guid>("NotifReadId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("is_read_id");

                    b.Property<string>("Title")
                        .HasColumnType("Varchar(255)")
                        .HasColumnName("title");

                    b.Property<Guid?>("UserId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("user_id");

                    b.HasKey("Id");

                    b.HasIndex("CategoryNotificationId");

                    b.HasIndex("NotifReadId");

                    b.ToTable("m_notif");
                });

            modelBuilder.Entity("AMH_MarketPlace.Entities.User.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("id");

                    b.Property<Guid>("CredentialId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("credential_id");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("Varchar(50)")
                        .HasColumnName("first_name");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("Varchar(50)")
                        .HasColumnName("last_name");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("Varchar(13)")
                        .HasColumnName("phone_number");

                    b.HasKey("Id");

                    b.HasIndex("CredentialId");

                    b.HasIndex("PhoneNumber")
                        .IsUnique();

                    b.ToTable("m_user");
                });

            modelBuilder.Entity("AMH_MarketPlace.Entities.User.Credential", b =>
                {
                    b.HasOne("AMH_MarketPlace.Entities.User.Role", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");
                });

            modelBuilder.Entity("AMH_MarketPlace.Entities.User.SubUser.Address", b =>
                {
                    b.HasOne("AMH_MarketPlace.Entities.User.User", null)
                        .WithOne("Address")
                        .HasForeignKey("AMH_MarketPlace.Entities.User.SubUser.Address", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("AMH_MarketPlace.Entities.User.SubUser.Notifications.Notification", b =>
                {
                    b.HasOne("AMH_MarketPlace.Entities.User.SubUser.Notifications.CategoryNotification", "CategoryNotification")
                        .WithMany()
                        .HasForeignKey("CategoryNotificationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AMH_MarketPlace.Entities.User.SubUser.Notifications.NotifRead", "NotifRead")
                        .WithMany()
                        .HasForeignKey("NotifReadId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CategoryNotification");

                    b.Navigation("NotifRead");
                });

            modelBuilder.Entity("AMH_MarketPlace.Entities.User.User", b =>
                {
                    b.HasOne("AMH_MarketPlace.Entities.User.Credential", "Credential")
                        .WithMany()
                        .HasForeignKey("CredentialId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Credential");
                });

            modelBuilder.Entity("AMH_MarketPlace.Entities.User.User", b =>
                {
                    b.Navigation("Address");
                });
#pragma warning restore 612, 618
        }
    }
}