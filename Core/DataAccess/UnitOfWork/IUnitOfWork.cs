using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DataAccess.UnitOfWork
{
    public interface IUnitOfWork
    {
        void Commit();
    }
}
