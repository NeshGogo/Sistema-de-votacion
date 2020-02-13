using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sistema_de_votacion.Data
{
    
        public interface IRepository<T> where T : class
        {
            T GetById(object id);
            IQueryable<T> GetAll();
            T Insert(T entity);
            IEnumerable<T> Insert(IEnumerable<T> entities);
            T Update(T entity);
            IEnumerable<T> Update(IEnumerable<T> entities);
            T Delete(T entity);
            IEnumerable<T> Delete(IEnumerable<T> entities);
        }
    
}
