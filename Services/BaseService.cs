using SafeCon.Context;
using SafeCon.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace SafeCon.Services
{
    
    public class BaseService<T> : IService<T> where T : BaseEntity
    {
        private ProjectContext _context;

        public BaseService()
        {
            _context = new ProjectContext();
        }

        public void Add(T item)
        {
            _context.Set<T>().Add(item);
        }

        public void Delete(Guid id)
        {
            T item = GetById(id);
            _context.Set<T>().Remove(item);
        }

        public List<T> GetAll()
        {
            return _context.Set<T>().ToList();
        }
        
        public List<T> GetAll(Expression<Func<T, bool>> exp)
        {
            return _context.Set<T>().Where(exp).ToList();
        }

        public T GetByDefault(Expression<Func<T, bool>> exp)
        {
            return _context.Set<T>().FirstOrDefault(exp);
        }

        public T GetById(Guid id)
        {
            return _context.Set<T>().Find(id);
        }
        
        public int Save()
        {
            return _context.SaveChanges();
        }

        public void Update(T item)
        {
            _context.Entry(item).State = EntityState.Modified;
        }

    }
}
