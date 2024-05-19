using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/*
SuccessResult sınıfı Result sınıfını miras alarak result sınıfındaki propertylere(Message ve Success propertyleri) sahip oldu ve bu propertyler
Result sınıfında oluşturulurken başlangıç değeri almayı consturoctor ile sağladığından SuccessResult sınıfından da obje üretirken bu 
propertylere başlangıç değeri vermemiz lazım çünkü miras alan sınıftan obje üretmeye çalıştığımızda once miras veren sınıftan obje üretilir ve 
biliyoruz ki obje üretiminde ilk tetiklenen yapı consturoctor yapılarıdır bundan dolayı SuccessResult sınıfında consturocturunda ilgili propertylere
istendi ve bu istenen değerler base sınıfının consturoctoruna iletildi consturoctor metot içerisinde propertylere atamalar gerçekleştirildi 
Yukarıda Anlatılan OLay Çözümlemesi 
------------------------------------

Adım-1
-------
namespace Core.Utilities.Results
{
    public interface IResult
    {
        bool Success { get; }            Result işlemeleri ile ilgili readonly propertyler tanımlandı 
        string Message { get; }        
    }
} 
Adım-2
-------
namespace Core.Utilities.Results
{
    public class Result : IResult
    {
        public bool Success { get; }

        public string Message { get; }

                                                                   IResult interfacesi implemente edildi ve implementeasyon yolu ile 
        public Result(bool success)                                elde edilen propertyler yapıcı metotlarda değerleri belirlendi     
        {
            Success = success;  
        }
                                                          
        public Result(bool success,string message):this(success)    
        {
            Message = message;  
            
        }
    }
}
Adım-3
---------
namespace Core.Utilities.Results
{
    public class SuccessResult : Result
    {
        public SuccessResult(string message) : base(true, message)
        {

        }
                                                                           Result sınıfı miras alındı ve Results sınıfındaki propertylere 
        public SuccessResult():base(true)                                  değerler yapıcı metotlar aracılığı ile Result iletildi 
        {

        }

    }
}
 
 */
namespace Core.Utilities.Results
{
    public class SuccessResult : Result
    {
        public SuccessResult(string message) : base(true, message)
        {

        }

        public SuccessResult():base(true)
        {

        }

    }
}
