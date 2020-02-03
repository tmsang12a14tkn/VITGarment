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
    public class LatestSummaryRepository : ILatestSummaryRepository
    {
        private readonly GarmentContext _context;

        public LatestSummaryRepository(GarmentContext context)
        {
            _context = context;
        }

        public LatestSummaryRepository()
        {
            _context = new GarmentContext();
        }

        public IQueryable<LatestSummary> All
        {
            get { return _context.LatestSummaries; }
        }

        public IQueryable<LatestSummary> AllIncluding(params Expression<Func<LatestSummary, object>>[] includeProperties)
        {
            return includeProperties.Aggregate<Expression<Func<LatestSummary, object>>, IQueryable<LatestSummary>>(_context.LatestSummaries, (current, includeProperty) => current.Include(includeProperty));
        }

        public LatestSummary Find(int id)
        {
            return _context.LatestSummaries.Find(id);
        }

        public void Load<TElement>(LatestSummary latestSummary, Expression<Func<LatestSummary, ICollection<TElement>>> includeProperty) where TElement : class
        {
            _context.LatestSummaries.Attach(latestSummary);
            _context.Entry(latestSummary).Collection(includeProperty).Load();
        }

        public IEnumerable<LatestSummary> GetMany(Expression<Func<LatestSummary, bool>> where)
        {
            return _context.LatestSummaries.Where(where).ToList();
        }

        public void InsertOrUpdate(LatestSummary latestSummary)
        {
            if (latestSummary.Id == default(int))
            {
                // New entity
                _context.LatestSummaries.Add(latestSummary);
            }
            else
            {
                // Existing entity
                _context.Entry(latestSummary).State = EntityState.Modified;
            }
        }

        public void Delete(int id)
        {
            var latestSummary = _context.LatestSummaries.Find(id);
            _context.LatestSummaries.Remove(latestSummary);
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

    public interface ILatestSummaryRepository : IDisposable
    {
        IQueryable<LatestSummary> All { get; }
        IQueryable<LatestSummary> AllIncluding(params Expression<Func<LatestSummary, object>>[] includeProperties);
        LatestSummary Find(int id);
        IEnumerable<LatestSummary> GetMany(Expression<Func<LatestSummary, bool>> where);
        void Load<TElement>(LatestSummary latestSummary, Expression<Func<LatestSummary, ICollection<TElement>>> includeProperty) where TElement : class;
        void InsertOrUpdate(LatestSummary latestSummary);
        void Delete(int id);
        void Save();
    }
}