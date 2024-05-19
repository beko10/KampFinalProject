using Core.DataAccess.UnitOfWork;
using DataAccess.Concrete.EntitiyFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly NorthwindContex _NorthwindContex;

        public UnitOfWork(NorthwindContex northwindContex)
        {
            _NorthwindContex = northwindContex;
        }

        public void Commit()
        {
            _NorthwindContex.SaveChanges();
        }
    }
}
