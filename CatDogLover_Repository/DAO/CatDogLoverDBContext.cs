﻿

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace CatDogLover_Repository.DAO
{
	public class CatDogLoverDBContext : DbContext
    {
        public CatDogLoverDBContext()
        {
        }

        public CatDogLoverDBContext(DbContextOptions<CatDogLoverDBContext> options)
            : base(options)
        {
        }
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<NewsFeed> NewsFeeds { get; set; }
        public DbSet<TypeGoods> TypeGoods { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<NumberOfInteraction> NumberOfInteractions { get; set; }
        public DbSet<Image> Images { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //if (!optionsBuilder.IsConfigured)
            //{
            //    IConfiguration config = new ConfigurationBuilder()
            //                            .SetBasePath(Directory.GetCurrentDirectory())
            //                            .AddJsonFile("appsettings.json").Build();

            //    string connectionString = config["ConnectionStrings:DefaultConnection"];
            //    optionsBuilder.UseSqlServer(connectionString);
            //}
            //base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("workstation id=CatDogLoverDB.mssql.somee.com;packet size=4096;user id=Thangvpq_SQLLogin_1;pwd=qwgw5z5vr3;data source=CatDogLoverDB.mssql.somee.com;persist security info=False;initial catalog=CatDogLoverDB; TrustServerCertificate=true;");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasOne(pp => pp.Role)
                .WithMany(cp => cp.Users)
                .HasForeignKey(pp => pp.RoleID);

            modelBuilder.Entity<NewsFeed>()
                .HasOne(pp => pp.User)
                .WithMany(e => e.NewsFeeds)
                .HasForeignKey(pp => pp.UserID);

            modelBuilder.Entity<NewsFeed>()
               .HasOne(e => e.TypeGoods)
               .WithMany(d => d.NewsFeeds)
               .HasForeignKey(e => e.TypeGoodsID);
            modelBuilder.Entity<Comment>()
             .HasOne(e => e.NewsFeed)
             .WithMany(d => d.Comments)
             .HasForeignKey(e => e.NewsFeedID).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Comment>()
            .HasOne(e => e.User)
            .WithMany(d => d.Comments)
            .HasForeignKey(e => e.UserID).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<NumberOfInteraction>()
           .HasOne(e => e.User)
           .WithMany(d => d.NumberOfInteractions)
           .HasForeignKey(e => e.UserID).OnDelete(DeleteBehavior.NoAction); ;
            modelBuilder.Entity<NumberOfInteraction>()
           .HasOne(e => e.NewsFeed)
           .WithMany(d => d.NumberOfInteractions)
           .HasForeignKey(e => e.NewsFeedID).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Image>()
           .HasOne(e => e.NewsFeed)
           .WithMany(d => d.Images)
           .HasForeignKey(e => e.NewsFeedID);

            base.OnModelCreating(modelBuilder);
        }
    }
}

