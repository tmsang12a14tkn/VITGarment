using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using Data.Models;
using Data.DataAccessLayer;

namespace Data.Repositories
{
    public class GoalDetailRepository : IGoalDetailRepository
    {
        private readonly GarmentContext _context;

        public GoalDetailRepository(GarmentContext context)
        {
            _context = context;
        }

        public GoalDetailRepository()
        {
            _context = new GarmentContext();
        }

        public IQueryable<GoalDetail> All
        {
            get { return _context.GoalDetails; }
        }

        public IQueryable<GoalDetail> AllIncluding(params Expression<Func<GoalDetail, object>>[] includeProperties)
        {
            return includeProperties.Aggregate<Expression<Func<GoalDetail, object>>, IQueryable<GoalDetail>>(_context.GoalDetails, (current, includeProperty) => current.Include(includeProperty));
        }

        public GoalDetail Find(int id)
        {
            return _context.GoalDetails.Find(id);
        }

        public void Load<TElement>(GoalDetail goalDetail, Expression<Func<GoalDetail, ICollection<TElement>>> includeProperty) where TElement : class
        {
            _context.GoalDetails.Attach(goalDetail);
            _context.Entry(goalDetail).Collection(includeProperty).Load();
        }

        public IEnumerable<GoalDetail> GetMany(Expression<Func<GoalDetail, bool>> where)
        {
            return _context.GoalDetails.Where(where).ToList();
        }

        public void InsertOrUpdate(GoalDetail goalDetail)
        {
            if (goalDetail.Id == default(int))
            {
                // New entity
                _context.GoalDetails.Add(goalDetail);
            }
            else
            {
                // Existing entity
                _context.Entry(goalDetail).State = EntityState.Modified;
            }
        }

        public void Delete(int id)
        {
            var goalDetail = _context.GoalDetails.Find(id);
            _context.GoalDetails.Remove(goalDetail);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }

    public interface IGoalDetailRepository : IDisposable
    {
        IQueryable<GoalDetail> All { get; }
        IQueryable<GoalDetail> AllIncluding(params Expression<Func<GoalDetail, object>>[] includeProperties);
        GoalDetail Find(int id);
        IEnumerable<GoalDetail> GetMany(Expression<Func<GoalDetail, bool>> where);
        void Load<TElement>(GoalDetail goalDetail, Expression<Func<GoalDetail, ICollection<TElement>>> includeProperty) where TElement : class;
        void InsertOrUpdate(GoalDetail goalDetail);
        void Delete(int id);
        void Save();
    }
}