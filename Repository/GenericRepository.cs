using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Thrift_Us.Data;

namespace Thrift_Us.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly ThriftDbContext _context;
        private readonly DbSet<T> _dbSet;

        public GenericRepository(ThriftDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public IEnumerable<T> GetAll()
        {
            return _dbSet.AsNoTracking().ToList();
        }


        public T GetById(object id)
        {
            return _dbSet.Find(id);
        }

        public T Read(int id)
        {
            return _dbSet.Find(id)!;
   
        }

        public bool Add(T model)
        {
            try
            {
                _dbSet.Add(model);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
        
                return false;
            }
        }

        public bool Update(T model)
        {
            try
            {
                _context.Update(model);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                
                return false;
            }
        }

        public bool Delete(object id)
        {
            try
            {
                T model = _dbSet.Find(id);
                if (model == null)
                {
                    return false;
                }
                _dbSet.Remove(model);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
              
                return false;
            }
        }



    }
}
