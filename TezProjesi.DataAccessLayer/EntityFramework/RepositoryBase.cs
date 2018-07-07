using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TezProjesi.DataAccessLayer.EntityFramework
{
    public class RepositoryBase
    {
        protected static DatabaseContext context;
        private static object mutex_lock = new object();
        protected RepositoryBase()
        {
            CreateContext();
        }
        public static void CreateContext()
        {
            if (context == null)
            {
                lock (mutex_lock)
                {
                    if (context == null)
                    {
                        context = new DatabaseContext();
                    }
                }
            }
        }
    
    }
}
