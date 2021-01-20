using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using EntityFrameworkRepository.Models;
using EntityFrameworkRepository.Interfaces;
using System.Linq.Expressions;
using EntityFrameworkRepository.Data;

namespace EntityFrameworkRepository
{
    public class EntityFrameworkRepository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly DbContext _context = new BestellingSysteemContext();

        public IEnumerable<T> List()
        {
            return _context.Set<T>().AsEnumerable();
        }

        public IEnumerable<T> List(Expression<Func<T, bool>> predicate)
        {
            return _context.Set<T>()
                .Where(predicate)
                .AsEnumerable();
        }
        public T GetById(long id)
        {
            return _context.Set<T>().Find(id);
        }

        public long Create(T entity)
        {
            _context.Set<T>().Add(entity);
            _context.SaveChanges(); 
            return entity.Id;
        }
        public void Update(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
            _context.SaveChanges();
        }
    }
}
