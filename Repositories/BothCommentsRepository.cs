using Instagram.Databases;
using Instagram.Interfaces;
using Instagram.Models;
using Instagram.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instagram.Repositories
{
    public class BothCommentsRepository<T> : IBothCommentsRepository<T> where T : ModelBase
    {
        private readonly InstagramDbContext _db;
        private readonly DbSet<T> _base;

        public BothCommentsRepository(InstagramDbContext db)
        {
            _db = db;
            _base = _db.Set<T>();
        }

        public async Task<bool> AddCommentAsync(T comment)
        {
            try
            {
                _base.Add(comment);
                return await SaveChanges.SaveAsync(_db);
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        public async Task<bool> DeleteCommentAsync(int commentId)
        {
            _base.Remove(await _base.FirstAsync(c => c.Id == commentId));
            return await SaveChanges.SaveAsync(_db);
        }

        public async Task<T> GetCommentAsync(int commentId)
        {
            return _base.First(c => c.Id == commentId);
        }

        public async Task<Comment> GetCommentWithResponsesAsync(int commentId)
        {
            if (typeof(T) == typeof(Comment))
            {
                return _db.Comments.Include(c => c.CommentResponses).First(c => c.Id == commentId);
            }
            return new Comment();
        }

        public async Task<bool> UpdateCommentAsync(T comment)
        {
            _base.Update(comment);
            return await SaveChanges.SaveAsync(_db);
        }
    }
}
