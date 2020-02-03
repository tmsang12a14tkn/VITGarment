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
    public class GoalRepository : IGoalRepository
    {
        private readonly GarmentContext _context;

        public GoalRepository(GarmentContext context)
        {
            _context = context;
        }

        public GoalRepository()
        {
            _context = new GarmentContext();
        }

        public IQueryable<Goal> All
        {
            get { return _context.Goals; }
        }

        public IQueryable<Goal> AllIncluding(params Expression<Func<Goal, object>>[] includeProperties)
        {
            return includeProperties.Aggregate<Expression<Func<Goal, object>>, IQueryable<Goal>>(_context.Goals, (current, includeProperty) => current.Include(includeProperty));
        }

        public Goal Find(int id)
        {
            return _context.Goals.Find(id);
        }

        public void Load<TElement>(Goal goal, Expression<Func<Goal, ICollection<TElement>>> includeProperty) where TElement : class
        {
            _context.Goals.Attach(goal);
            _context.Entry(goal).Collection(includeProperty).Load();
        }

        public IEnumerable<Goal> GetMany(Expression<Func<Goal, bool>> where)
        {
            return _context.Goals.Where(where).ToList();
        }

        public void InsertOrUpdate(Goal goal)
        {
            if (goal.Id == default(int))
            {
                // New entity
                _context.Goals.Add(goal);
            }
            else
            {
                // Existing entity
                _context.Entry(goal).State = EntityState.Modified;
            }
        }

        public void Delete(int id)
        {
            var goal = _context.Goals.Find(id);
            _context.Goals.Remove(goal);
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

    public interface IGoalRepository : IDisposable
    {
        IQueryable<Goal> All { get; }
        IQueryable<Goal> AllIncluding(params Expression<Func<Goal, object>>[] includeProperties);
        Goal Find(int id);
        IEnumerable<Goal> GetMany(Expression<Func<Goal, bool>> where);
        void Load<TElement>(Goal goal, Expression<Func<Goal, ICollection<TElement>>> includeProperty) where TElement : class;
        void InsertOrUpdate(Goal goal);
        void Delete(int id);
        void Save();
    }
}