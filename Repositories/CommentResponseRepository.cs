using Instagram.Databases;
using Instagram.Interfaces;
using Instagram.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instagram.Repositories
{
    public class CommentResponseRepository : ICommentResponseRepository
    {
        private readonly InstagramDbContext _db;

        public CommentResponseRepository(InstagramDbContext db)
        {
            _db = db;
        }

        public async Task<bool> AddCommentResponseAsync(CommentResponse commentResponse)
        {
            await _db.CommentResponses.AddAsync(commentResponse);
            return await SaveChangesAsync();
        }

        public async Task<bool> DeleteCommentResponseAsync(int commentResponseId)
        {
            _db.CommentResponses.Remove(await _db.CommentResponses.FirstAsync(c => c.Id == commentResponseId));
            return await SaveChangesAsync();
        }

        public async Task<bool> SaveChangesAsync()
        {
            var save = await _db.SaveChangesAsync();
            return save > 0 ? true : false;
        }
    }
}
