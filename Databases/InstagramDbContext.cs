using Instagram.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Windows;

namespace Instagram.Databases
{
    public class InstagramDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<UserIdSentModel> UserIdSentModels { get; set; }
        public DbSet<UserIdGotModel> UserIdGotModels { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Story> Stories { get; set; }
        public DbSet<PostImage> PostImages { get; set; }
        public DbSet<ProfileImage> ProfileImages { get; set; }
        public DbSet<StoryImage> StoryImages { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Friend> Friends { get; set; }
        public DbSet<UserLiked> UsersLiked { get; set; }
        public DbSet<CommentResponse> CommentResponses { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //string connectionString = ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString.ToString();
            string connectionString = "Server=DESKTOP-KKCA33K;Database=InstagramDb;Trusted_Connection=True;TrustServerCertificate=True;";
            optionsBuilder.UseSqlServer(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Comment>().Property(p => p.Likes).HasDefaultValue(0);
            modelBuilder.Entity<Post>().Property(p => p.Likes).HasDefaultValue(0);
            modelBuilder.Entity<User>().HasMany(u => u.Posts).WithOne(p => p.User).HasForeignKey(p => p.UserId);
            modelBuilder.Entity<User>().HasMany(u => u.Stories).WithOne(s => s.User).HasForeignKey(s => s.UserId);
            modelBuilder.Entity<User>().HasMany(u => u.Friends).WithOne(f => f.User).HasForeignKey(f => f.UserId);
            modelBuilder.Entity<Post>().HasMany(p => p.Comments).WithOne(c => c.Post).HasForeignKey(c => c.PostId);
            modelBuilder.Entity<Post>().HasMany(p => p.Tags).WithOne(t => t.Post).HasForeignKey(c => c.PostId);
            modelBuilder.Entity<Comment>().HasMany(c => c.CommentResponses).WithOne(cr => cr.Comment).HasForeignKey(cr => cr.CommentId);
            modelBuilder.Entity<Post>().HasOne(p => p.Image).WithOne(i => i.Post).HasForeignKey<PostImage>(i => i.PostId);
            modelBuilder.Entity<User>().HasOne(p => p.ProfilePhoto).WithOne(p => p.User).HasForeignKey<ProfileImage>(i => i.UserId);
            modelBuilder.Entity<Story>().HasOne(p => p.Image).WithOne(s => s.Story).HasForeignKey<StoryImage>(i => i.StoryId);
            modelBuilder.Entity<User>().HasMany(u => u.SentFriendRequests).WithOne(uid => uid.User).HasForeignKey(uid => uid.UserId);
            modelBuilder.Entity<User>().HasMany(u => u.GotFriendRequests).WithOne(gfr => gfr.User).HasForeignKey(gfr => gfr.UserId);
        }
    }
}
