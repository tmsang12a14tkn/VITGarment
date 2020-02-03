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
    public class ProduceHistoryRepository : IProduceHistoryRepository
    {
        private readonly GarmentContext _context;

        public ProduceHistoryRepository(GarmentContext context)
        {
            _context = context;
        }

        public ProduceHistoryRepository()
        {
            _context = new GarmentContext();
        }

        public IQueryable<ProduceHistory> All
        {
            get { return _context.ProduceHistories; }
        }

        public IQueryable<ProduceHistory> AllIncluding(params Expression<Func<ProduceHistory, object>>[] includeProperties)
        {
            return includeProperties.Aggregate<Expression<Func<ProduceHistory, object>>, IQueryable<ProduceHistory>>(_context.ProduceHistories, (current, includeProperty) => current.Include(includeProperty));
        }

        public ProduceHistory Find(int id)
        {
            return _context.ProduceHistories.Find(id);
        }

        public void Load<TElement>(ProduceHistory produceHistory, Expression<Func<ProduceHistory, ICollection<TElement>>> includeProperty) where TElement : class
        {
            _context.ProduceHistories.Attach(produceHistory);
            _context.Entry(produceHistory).Collection(includeProperty).Load();
        }

        public IEnumerable<ProduceHistory> GetMany(Expression<Func<ProduceHistory, bool>> where)
        {
            return _context.ProduceHistories.Where(where).ToList();
        }

        public void InsertOrUpdate(ProduceHistory produceHistory)
        {
            if (produceHistory.Id == default(int))
            {
                // New entity
                _context.ProduceHistories.Add(produceHistory);
            }
            else
            {
                // Existing entity
                _context.Entry(produceHistory).State = EntityState.Modified;
            }
        }

        public void Delete(int id)
        {
            var produceHistory = _context.ProduceHistories.Find(id);
            _context.ProduceHistories.Remove(produceHistory);
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

    public interface IProduceHistoryRepository : IDisposable
    {
        IQueryable<ProduceHistory> All { get; }
        IQueryable<ProduceHistory> AllIncluding(params Expression<Func<ProduceHistory, object>>[] includeProperties);
        ProduceHistory Find(int id);
        IEnumerable<ProduceHistory> GetMany(Expression<Func<ProduceHistory, bool>> where);
        void Load<TElement>(ProduceHistory produceHistory, Expression<Func<ProduceHistory, ICollection<TElement>>> includeProperty) where TElement : class;
        void InsertOrUpdate(ProduceHistory produceHistory);
        void Delete(int id);
        void Save();
    }
}