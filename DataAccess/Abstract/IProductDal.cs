using Core.DataAccess;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
/*
 
IProductDal:IEntityRepository<Product> implementasyon işlemi ile  IEntityRepository<Product> interfacesindeki metotları Product tablosu için IProductDal interfacesine 

kazandırmış olduk. 

NOT:IProductDal interfacesinde productlar ile ilgili özel işlemleri yapacak metotlar bulunur 
 


 
 */
namespace DataAccess.Abstract
{
    public interface IProductDal:IEntityRepository<Product>
    {
        List<ProductDetailDto> GetProductDetails();
    }
}
