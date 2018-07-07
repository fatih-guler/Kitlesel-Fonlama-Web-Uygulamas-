using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace TezProjesi.Core.DatabaseContext
{
    public interface IDatabaseContext<T> where T:class
    {
        int Save();
        int Insert(T obj);
        int Update(T obj);
        int Delete(T obj);
        List<T> List();
        List<T> List(Expression<Func<T,bool>>where);

        T Find(Expression<Func<T, bool>> where);
        IQueryable<T> IqueryableList(); 

    }
}
