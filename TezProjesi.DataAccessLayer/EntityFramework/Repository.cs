using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TezProjesi.Core.DatabaseContext;

namespace TezProjesi.DataAccessLayer.EntityFramework
{
    public class Repository<T>:RepositoryBase,IDatabaseContext<T> where T :class
    {
        
        private DbSet<T> _objSet;
        public Repository()
        {
            _objSet = context.Set<T>();
        }
        public int Save()
        {
          return  context.SaveChanges();
        }

        public int Insert(T obj)
        {
            _objSet.Add(obj);
            return Save();
        }

        public int Update(T obj)
        {
            return Save();
        }
        public int Delete(T obj)
        {
            _objSet.Remove(obj);
             return Save();
        }

        public List<T> List()
        {
            return _objSet.ToList();
        }

        public List<T> List(System.Linq.Expressions.Expression<Func<T, bool>> where)
        {
            return _objSet.Where(where).ToList();
        }

        public T Find(System.Linq.Expressions.Expression<Func<T, bool>> where)
        {
            return _objSet.Where(where).FirstOrDefault();
        }


        public IQueryable<T> IqueryableList()
        {
            return _objSet.AsQueryable<T>();
        }
    }
}
