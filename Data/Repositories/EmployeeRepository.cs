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
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly GarmentContext _context;

        public EmployeeRepository(GarmentContext context)
        {
            _context = context;
        }

        public EmployeeRepository()
        {
            _context = new GarmentContext();
        }

        public IQueryable<Employee> All
        {
            get { return _context.Employees; }
        }

        public IQueryable<Employee> AllIncluding(params Expression<Func<Employee, object>>[] includeProperties)
        {
            return includeProperties.Aggregate<Expression<Func<Employee, object>>, IQueryable<Employee>>(_context.Employees, (current, includeProperty) => current.Include(includeProperty));
        }

        public Employee Find(int id)
        {
            return _context.Employees.Find(id);
        }

        public void Load<TElement>(Employee employee, Expression<Func<Employee, ICollection<TElement>>> includeProperty) where TElement : class
        {
            _context.Employees.Attach(employee);
            _context.Entry(employee).Collection(includeProperty).Load();
        }

        public IEnumerable<Employee> GetMany(Expression<Func<Employee, bool>> where)
        {
            return _context.Employees.Where(where).ToList();
        }

        public void InsertOrUpdate(Employee employee)
        {
            if (employee.Id == default(int))
            {
                // New entity
                _context.Employees.Add(employee);
            }
            else
            {
                // Existing entity
                _context.Entry(employee).State = EntityState.Modified;
            }
        }

        public void Delete(int id)
        {
            var employee = _context.Employees.Find(id);
            _context.Employees.Remove(employee);
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

    public interface IEmployeeRepository : IDisposable
    {
        IQueryable<Employee> All { get; }
        IQueryable<Employee> AllIncluding(params Expression<Func<Employee, object>>[] includeProperties);
        Employee Find(int id);
        IEnumerable<Employee> GetMany(Expression<Func<Employee, bool>> where);
        void Load<TElement>(Employee employee, Expression<Func<Employee, ICollection<TElement>>> includeProperty) where TElement : class;
        void InsertOrUpdate(Employee employee);
        void Delete(int id);
        void Save();
    }
}