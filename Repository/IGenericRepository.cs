using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Thrift_Us.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        T GetById(object id);
        T Read(int id);
        bool Add(T model);
        bool Update(T model);
        bool Delete(object id);
    }
}
