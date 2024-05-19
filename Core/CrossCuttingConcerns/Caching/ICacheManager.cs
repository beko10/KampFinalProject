using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.CrossCuttingConcerns.Caching
{
    public interface ICacheManager
    {
        //cache ekleme yapar
        void Add(string key, object value,int duration);

        T Get<T>(string key);
        object Get(string key);
        
        //cache var mı kontrol edilir
        bool IsAdd(string key);

        //cacheden siler
        void Remove(string key);    

        void RemoveByPattern(string pattern);   
    }
}
