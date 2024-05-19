using Business.Abstract;
using Business.BusinessAspect.Autofac;
using Business.CCS;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Validation;
using Core.DataAccess.UnitOfWork;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Concrete.InMemory;
using Entities.Concrete;
using Entities.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Http.Headers;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using ValidationException = FluentValidation.ValidationException;

namespace Business.Concrete
{
    public class ProductManager : IProductService
    {
        /* 
          
         Businnes Katmanında ProductManager sınıfı DataAccess Katmanındaki InMemoryProductDal sınıfına bağımlı 
        
         bu bağımlılığı soyutlamak için IProductDal interfacesinden yararlandık Deependecy Injection ile bu bağımlılığı ProductManager

         sınıfına enjekte ettik.
         
         */
        IProductDal _productDal;

        ICategoryService _categoryService;
        //IUnitOfWork _unitOfWork;



        public ProductManager(IProductDal productDal, ICategoryService categoryService)
        {
            _productDal = productDal;
            //_unitOfWork = unitOfWork;
            _categoryService = categoryService;
        }

        [SecuredOperation("admin,editor")]
        [ValidationAspect(typeof(ProductValidator))]
        [CacheRemoveAspect("IProductService.Get")]
        public IResult Add(Product product)
        {
            // 'Add' metodu, yeni bir ürün eklemek için kullanılır ve güvenlik ile doğrulama katmanlarını içerir.

            // İş kurallarını çalıştırarak ürün ismi ve kategori sayısı doğrulamaları yapılır.
            IResult result = BusinessRules.Run(CheckOfProductName(product.ProductName), CheckIfProductCountCategoryCorrect(product.CategoryId), CheckCategoryCountLimit());
            if (result != null)
            {
                // Eğer iş kurallarından herhangi biri başarısızsa, hata mesajı döndürülür.
                return result;
            }
            // İş kuralları başarılıysa, ürün veritabanına eklenir.
            _productDal.Add(product);
            // İşlem başarılı olduğunda başarılı sonuç mesajı döndürülür.
            return new SuccessResult(Messages.ProductAdded);
        }
        //Bir kategoride en fazla 10 ürün olabilir


        [CacheAspect]//key,value
        public IDataResult<List<Product>> GetAll()
        {
            //if (DateTime.Now.Hour == 1)
            //{
            //    return new ErrorDataResult<List<Product>>(Messages.MaintenanceTime);
            //}
            //İş Kodları 
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(), Messages.ProductListed);
        }

        public IDataResult<List<Product>> GetAllByCategoryId(int id)
        {
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(p => p.CategoryId == id));
        }

        [CacheAspect]
        public IDataResult<Product> GetById(int productId)
        {
            return new SuccessDataResult<Product>(_productDal.Get(p => p.ProductId == productId));
        }

        public IDataResult<List<Product>> GetByUnitPrice(decimal min, decimal max)
        {
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(p => p.UnitPrice >= min && p.UnitPrice <= max));
        }

        public IDataResult<List<ProductDetailDto>> GetProductDetails()
        {
            return new SuccessDataResult<List<ProductDetailDto>>(_productDal.GetProductDetails());
        }

        [ValidationAspect(typeof(ProductValidator))]
        [CacheRemoveAspect("IProductService.Get")]
        public IResult Update(Product product)
        {
            throw new NotImplementedException();
        }


        //Business Kuralları 
        private IResult CheckIfProductCountCategoryCorrect(int categoryId)
        {
            var result = _productDal.GetAll(p => p.CategoryId == categoryId).Count;
            if (result >= 10)
            {
                return new ErrorResult(Messages.ProductCountOfCategory);
            }
            return new SuccessResult();

        }
        private IResult CheckOfProductName(string productName)
        {
            var result = _productDal.GetAll(p => p.ProductName == productName).Any();
            if (result)
            {
                return new ErrorResult(Messages.ProductNameAlreadyExists);
            }
            return new SuccessResult();
        }
        private IResult CheckCategoryCountLimit()
        {
            var result = _categoryService.GetAll();

            if (result.Data.Count > 15)
            {
                return new ErrorResult(Messages.CategoryLimitExeded);
            }

            return new SuccessResult();
        }

        public IResult AddTransactionalTest(Product product)
        {
            throw new NotImplementedException();
        }
    }
}
