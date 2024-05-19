using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
/*
 
EfProductDal sınıfı EfEntityRepository sınıfını miras alarak EfEntityRepository sınıfındaki metotlara sahip olur

EfEntityRepository IProductDal interfacesindeki implemente ederse IProductDal interfacesindeki metotların yapacakları işlevler EfProductDal sınıfında belirlenir.

**ÇOK ÖNEMLİ:**

namespace DataAccess.Abstract
{
    public interface IProductDal:IEntityRepository<Product>
    {
        List<Product> GetAll(Expression<Func<Product, bool>>? filter = null);
        Product Get(Expression<Func<Product, bool>> filter);
        void Add(Product entity);
        void Update(Product entity);
        void Delete(Product entity);
       
    }
}
İçeriği böyle olsa idi;


IProductDal : IEntityRepository<Product> IProductDal IEntitiyRepository interfacesini Product sınıfına uygun implemente eder generic tipten dolayı 

public class EfEntityRepositoryBase<TEntity,TContex>  : IEntityRepository<TEntity> EfEntityRepositoryBase sınıfı IEntityRepository TEntity generic tipine uygun implemente eder

public class EfProductDal : EfEntityRepositoryBase<Product, NorthwindContex>, IProductDal implementasyonunda IProductDal da derleyici hata vermezdi çünkü  
IProductDal IEntityRepository imlemente eder EfEntityRepositoryBase IEntityRepository implemente ettiği için

EfProductDal : EfEntityRepositoryBase<Product, NorthwindContex>(miras alırken) ,IProductDal(implemente edilirken) zaten ilgili sınıf ile interfacede aynı imzaya sahip 
metotlar var bu yüzden derleyici hata vermez lakin IProductDal interfacesine başka bir metot imzası olasaydı o zaman derleyici bu metodun işlevini belirlememiz için hata veriridi
 */
namespace DataAccess.Concrete.EntitiyFramework
{
    public class EfProductDal : EfEntityRepositoryBase<Product, NorthwindContex>, IProductDal
    {
        public List<ProductDetailDto> GetProductDetails()
        {
            using (NorthwindContex contex = new NorthwindContex())
            {
              
                var result = from p in contex.Products //p'yi Product tablosu yap 
                             join c in contex.Categories//c'yi Categories tablosu yap ve bu p(Product Tbl) ve c(Categories Tbl) join yap  
                              on p.CategoryId equals c.CategoryId//join yapılma şartı p'deki CategoryId == c'deki CategoryId 
                             //Join işlemi sonucundaki veriler ProductDetailDto sınıfından bir nesnenin ilgili propertiylerine ilgili veriler atandı   
                             select new ProductDetailDto 
                             { 
                                 ProductId = p.ProductId,ProductName = p.ProductName,
                                 CategoryName=c.CategoryName,UnitsStock=p.UnitsInStock
                             };
                
                return result.ToList();
            }
             
        }
    }
}
