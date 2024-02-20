using Instagram.Databases;
using Instagram.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Instagram.Repositories
{
    public static class NewPostRepository
    {
        public static void Create(int userId, Post post)
        {
            //using (var db = new InstagramDbContext("MainDb"))
            //{
            //    db.Users.Where(u => u.Id == userId).ToList().ForEach(u => u.Posts.Add(post));
            //    db.SaveChanges();
            //}
            MessageBox.Show("Post created!");
        }
    }
}
