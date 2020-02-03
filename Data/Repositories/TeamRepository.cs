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
    public class TeamRepository : ITeamRepository
    {
        private readonly GarmentContext _context;

        public TeamRepository(GarmentContext context)
        {
            _context = context;
        }

        public TeamRepository()
        {
            _context = new GarmentContext();
        }

        public IQueryable<Team> All
        {
            get { return _context.Teams; }
        }

        public IQueryable<Team> AllIncluding(params Expression<Func<Team, object>>[] includeProperties)
        {
            return includeProperties.Aggregate<Expression<Func<Team, object>>, IQueryable<Team>>(_context.Teams, (current, includeProperty) => current.Include(includeProperty));
        }

        public Team Find(int id)
        {
            return _context.Teams.Find(id);
        }

        public void Load<TElement>(Team team, Expression<Func<Team, ICollection<TElement>>> includeProperty) where TElement : class
        {
            _context.Teams.Attach(team);
            _context.Entry(team).Collection(includeProperty).Load();
        }

        public IEnumerable<Team> GetMany(Expression<Func<Team, bool>> where)
        {
            return _context.Teams.Where(where).ToList();
        }

        public void InsertOrUpdate(Team team)
        {
            if (team.Id == default(int))
            {
                // New entity
                _context.Teams.Add(team);
            }
            else
            {
                // Existing entity
                _context.Entry(team).State = EntityState.Modified;
            }
        }

        public void Delete(int id)
        {
            var team = _context.Teams.Find(id);
            _context.Teams.Remove(team);
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

    public interface ITeamRepository : IDisposable
    {
        IQueryable<Team> All { get; }
        IQueryable<Team> AllIncluding(params Expression<Func<Team, object>>[] includeProperties);
        Team Find(int id);
        IEnumerable<Team> GetMany(Expression<Func<Team, bool>> where);
        void Load<TElement>(Team team, Expression<Func<Team, ICollection<TElement>>> includeProperty) where TElement : class;
        void InsertOrUpdate(Team team);
        void Delete(int id);
        void Save();
    }
}