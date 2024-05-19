using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/*

Result sınıfı IResult sınıfını implemente ederek IResult sınıfındaki propertryleri uygulamak zorunda kaldı ve consturoctor ile obje oluşturulurken
bu iki propertye başlangıç değerleri verildi
 
 */
namespace Core.Utilities.Results
{
    public class Result : IResult
    {
        public bool Success { get; }

        public string Message { get; }


        public Result(bool success)
        {
            Success = success;  
        }

        public Result(bool success,string message):this(success) //Yukarıdaki consturoctor çağırıldı 
        {
            Message = message;  
            
        }
    }
}
