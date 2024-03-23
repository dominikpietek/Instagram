using Instagram.Databases;
using Instagram.Interfaces;
using Instagram.Models;
using Instagram.Services;
using Microsoft.EntityFrameworkCore;
using NLog;
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
        private static Logger _logger = LogManager.GetCurrentClassLogger();

        public BothCommentsRepository(InstagramDbContext db)
        {
            _db = db;
            _base = _db.Set<T>();
        }

        public async Task<bool> AddCommentAsync(T comment)
        {
            try
            {
                await _base.AddAsync(comment);
                return await SaveChanges.SaveAsync(_db);
            }
            catch(Exception e)
            {
                _logger.Error($"{e.Message};BothCommentsRepository");
                throw;
            }
        }

        public async Task<bool> DeleteCommentAsync(int commentId)
        {
            try
            {
                _base.Remove(_base.First(c => c.Id == commentId));
                return await SaveChanges.SaveAsync(_db);
            }
            catch(Exception e)
            {
                _logger.Error($"{e.Message};BothCommentsRepository");
                throw;
            }
        }

        public async Task<T> GetCommentAsync(int commentId)
        {
            try
            {
                return _base.First(c => c.Id == commentId);
            }
            catch (Exception e)
            {
                _logger.Error($"{e.Message};BothCommentsRepository");
                throw;
            }
        }

        public async Task<Comment> GetCommentWithResponsesAsync(int commentId)
        {
            try
            {
                if (typeof(T) == typeof(Comment))
                {
                    return _db.Comments.Include(c => c.CommentResponses).First(c => c.Id == commentId);
                }
                return new Comment();
            }
            catch (Exception e)
            {
                _logger.Error($"{e.Message};BothCommentsRepository");
                throw;
            }
        }

        public async Task<bool> UpdateCommentAsync(T comment)
        {
            try
            {
                _base.Update(comment);
                return await SaveChanges.SaveAsync(_db);
            }
            catch (Exception e)
            {
                _logger.Error($"{e.Message};BothCommentsRepository");
                throw;
            }
        }
    }
}
