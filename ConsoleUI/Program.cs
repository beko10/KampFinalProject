
using Business.Concrete;
using DataAccess.Concrete.EntitiyFramework;
using DataAccess.Concrete.InMemory;

//ProductTest();

//CategoryTest();

Console.WriteLine();

//static void ProductTest()
//{
//    ProductManager productmanager = new ProductManager(new EfProductDal());

//    foreach (var product in productmanager.GetProductDetails().Data)
//    {
//        Console.WriteLine(product.ProductName + "/" + product.CategoryName);
//    }
//}

//static void CategoryTest()
//{
//    CategoryManager categoryManager = new CategoryManager(new EfCategoryDal());

//    foreach (var category in categoryManager.GetAll())
//    {
//        Console.WriteLine(category.CategoryName); ;
//    }
//}