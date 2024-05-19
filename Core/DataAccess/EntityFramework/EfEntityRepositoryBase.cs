using Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
//Herhangi bir veri tabanı ile çalışmamızı sağlayacak generic sınıf tasarlandı
/*

Sınıfın Çalışma Mantığı 
-------------------------
TEntity = herhangi bir veri tanına ait tablo
TContex = Veri tabanının çeşidi(Mssql,Oracle)
 
EfEntityRepositoryBase<TEntity,TContex> sınıfını  IEntityRepository<TEntity> ile implemente ettiğimizde IEntityRepository sınıfındaki metotları herhangi bir tabloya göre 
ve herhangi bir veri tabanı çeşidine uygun çalışır hale getirmiş olduk.
 
 */
namespace Core.DataAccess.EntityFramework
{
    public class EfEntityRepositoryBase<TEntity,TContex>  : IEntityRepository<TEntity>
    where TEntity:class,IEntitiy,new()  
    where TContex : DbContext,new()
    {
        public void Add(TEntity entity)
        {
            using (TContex contex = new TContex())
            {
                var addedEntity = contex.Entry(entity);
                addedEntity.State = EntityState.Added;
                contex.SaveChanges();
            }

        }

        public void Delete(TEntity entity)
        {
            using (TContex contex = new TContex())
            {
                var deletedEntity = contex.Entry(entity);
                deletedEntity.State = EntityState.Deleted;
                contex.SaveChanges();
            }
        }

        public TEntity Get(Expression<Func<TEntity, bool>> filter)
        {
            using (TContex contex = new())
            {
                return contex.Set<TEntity>().SingleOrDefault(filter);
            }
        }

        public List<TEntity> GetAll(Expression<Func<TEntity, bool>>? filter = null)
        {
            using (TContex contex = new())
            {
                return filter == null
                    ? contex.Set<TEntity>().ToList() //filter null ise
                    : contex.Set<TEntity>().Where(filter).ToList();  // filter null değil ise 
            }
        }

        public void Update(TEntity entity)
        {
            using (TContex contex = new TContex())
            {
                var updatedEntity = contex.Entry(entity);
                updatedEntity.State = EntityState.Modified;
                contex.SaveChanges();
            }
        }
    }
}
