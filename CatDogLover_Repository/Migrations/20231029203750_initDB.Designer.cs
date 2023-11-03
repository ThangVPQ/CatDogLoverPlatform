﻿// <auto-generated />
using System;
using CatDogLover_Repository.DAO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CatDogLoverRepository.Migrations
{
    [DbContext(typeof(CatDogLoverDBContext))]
    [Migration("20231029203750_initDB")]
    partial class initDB
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("CatDogLover_Repository.DAO.Comment", b =>
                {
                    b.Property<Guid>("CommentID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Content")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("InsertDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("NewsFeedID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int?>("Status")
                        .HasColumnType("int");

                    b.Property<Guid?>("UpdateBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("UpdateDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("UserID")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("CommentID");

                    b.HasIndex("NewsFeedID");

                    b.HasIndex("UserID");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("CatDogLover_Repository.DAO.Image", b =>
                {
                    b.Property<Guid>("ImageID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("InsertDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("NewsFeedID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("UpdateDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("UrlImage")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ImageID");

                    b.HasIndex("NewsFeedID");

                    b.ToTable("Images");
                });

            modelBuilder.Entity("CatDogLover_Repository.DAO.NewsFeed", b =>
                {
                    b.Property<Guid>("NewsFeedID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Content")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("InsertDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal?>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int?>("Quantity")
                        .HasColumnType("int");

                    b.Property<int?>("Status")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("TypeGoodsID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("TypeNewsFeedID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Unit")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("UpdateBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("UpdateDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("UserID")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("NewsFeedID");

                    b.HasIndex("TypeGoodsID");

                    b.HasIndex("TypeNewsFeedID");

                    b.HasIndex("UserID");

                    b.ToTable("NewsFeeds");
                });

            modelBuilder.Entity("CatDogLover_Repository.DAO.NumberOfInteraction", b =>
                {
                    b.Property<Guid>("NumberOfInteractionID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("InsertDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("NewsFeedID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UserID")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("NumberOfInteractionID");

                    b.HasIndex("NewsFeedID");

                    b.HasIndex("UserID");

                    b.ToTable("NumberOfInteractions");
                });

            modelBuilder.Entity("CatDogLover_Repository.DAO.Role", b =>
                {
                    b.Property<Guid>("RoleID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("RoleName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("RoleID");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("CatDogLover_Repository.DAO.TypeGoods", b =>
                {
                    b.Property<Guid>("TypeGoodsID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("TypeGoodsName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("TypeGoodsID");

                    b.ToTable("TypeGoods");
                });

            modelBuilder.Entity("CatDogLover_Repository.DAO.TypeNewsFeed", b =>
                {
                    b.Property<Guid>("TypesNewFeedID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("TypesNewFeedName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("TypesNewFeedID");

                    b.ToTable("TypeNewsFeeds");
                });

            modelBuilder.Entity("CatDogLover_Repository.DAO.User", b =>
                {
                    b.Property<Guid>("UserID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("BirthDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FullName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("InsertDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("LogOutDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Otp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("RoleID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int?>("Status")
                        .HasColumnType("int");

                    b.Property<DateTime?>("UpdateBy")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("UpdateDate")
                        .HasColumnType("datetime2");

                    b.HasKey("UserID");

                    b.HasIndex("RoleID");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("CatDogLover_Repository.DAO.Comment", b =>
                {
                    b.HasOne("CatDogLover_Repository.DAO.NewsFeed", "NewsFeed")
                        .WithMany("Comments")
                        .HasForeignKey("NewsFeedID")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("CatDogLover_Repository.DAO.User", "User")
                        .WithMany("Comments")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("NewsFeed");

                    b.Navigation("User");
                });

            modelBuilder.Entity("CatDogLover_Repository.DAO.Image", b =>
                {
                    b.HasOne("CatDogLover_Repository.DAO.NewsFeed", "NewsFeed")
                        .WithMany("Images")
                        .HasForeignKey("NewsFeedID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("NewsFeed");
                });

            modelBuilder.Entity("CatDogLover_Repository.DAO.NewsFeed", b =>
                {
                    b.HasOne("CatDogLover_Repository.DAO.TypeGoods", "TypeGoods")
                        .WithMany("NewsFeeds")
                        .HasForeignKey("TypeGoodsID");

                    b.HasOne("CatDogLover_Repository.DAO.TypeNewsFeed", "TypeNewsFeed")
                        .WithMany("NewsFeeds")
                        .HasForeignKey("TypeNewsFeedID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CatDogLover_Repository.DAO.User", "User")
                        .WithMany("NewsFeeds")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TypeGoods");

                    b.Navigation("TypeNewsFeed");

                    b.Navigation("User");
                });

            modelBuilder.Entity("CatDogLover_Repository.DAO.NumberOfInteraction", b =>
                {
                    b.HasOne("CatDogLover_Repository.DAO.NewsFeed", "NewsFeed")
                        .WithMany("NumberOfInteractions")
                        .HasForeignKey("NewsFeedID")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("CatDogLover_Repository.DAO.User", "User")
                        .WithMany("NumberOfInteractions")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("NewsFeed");

                    b.Navigation("User");
                });

            modelBuilder.Entity("CatDogLover_Repository.DAO.User", b =>
                {
                    b.HasOne("CatDogLover_Repository.DAO.Role", "Role")
                        .WithMany("Users")
                        .HasForeignKey("RoleID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");
                });

            modelBuilder.Entity("CatDogLover_Repository.DAO.NewsFeed", b =>
                {
                    b.Navigation("Comments");

                    b.Navigation("Images");

                    b.Navigation("NumberOfInteractions");
                });

            modelBuilder.Entity("CatDogLover_Repository.DAO.Role", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("CatDogLover_Repository.DAO.TypeGoods", b =>
                {
                    b.Navigation("NewsFeeds");
                });

            modelBuilder.Entity("CatDogLover_Repository.DAO.TypeNewsFeed", b =>
                {
                    b.Navigation("NewsFeeds");
                });

            modelBuilder.Entity("CatDogLover_Repository.DAO.User", b =>
                {
                    b.Navigation("Comments");

                    b.Navigation("NewsFeeds");

                    b.Navigation("NumberOfInteractions");
                });
#pragma warning restore 612, 618
        }
    }
}