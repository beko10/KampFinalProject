using Core.Entities.Concrete;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Constants
{
    public static class Messages
    {
        public static string ProductAdded = "Ürün Eklendi";
        public static string ProductNameInValid = "Ürün ismi geçersiz";
        public static string ProductListed="Ürünler listelendi";
        public static string MaintenanceTime="Sistem bakımda";
        public static string ProductNameAlreadyExists="Ürün ismi zaten var";
        public static string? AuthorizationDenied="Yetkiniz yok";
        public static string UserRegistered="Kayıt oldu";
        public static string UserNotFound="kullanıcı bulunamadı";
        public static string PasswordError="parola hatası";
        public static string SuccessfulLogin="giriş başarılı";
        public static string UserAlreadyExists="kullanıcı mevcut";
        public static string AccessTokenCreated="Token oluşturuldu";
        public static string ProductCountOfCategory = "Bir kategoride en fazla 10 ürün olabilir";
        public static string CategoryLimitExeded="kategori limiti aşıldı";
    }
}
