using Core.Entities.Concrete;
using System.Runtime.Serialization;

namespace Business.Constants
{
    public static class Messages
    {
        public static string ProductAdded = "Ürün eklendi.";
        public static string ProductNameInvalid = "Ürün ismi geçersiz.";
        public static string MaintenanceTime = "Sistem bakımda.";
        public static string ProductListed = "Ürünler listelendi.";


        public static string CustomerAdded = "Müşteri başarıyla eklendi.";
        public static string CustomerDeleted = "Müşteri başarıyla silindi.";
        public static string CustomerListed = "Müşteriler listelendi.";
        public static string CustomerUpdated = "Müşteri güncellendi.";

        public static string UserAdded = "Kullanıcı başarıyla eklendi.";
        public static string UserListed = "Kullanıcı listelendi.";

        public static string RentalAddError = "Araç kiradan dönmedi.";
        public static string CarImageLimitExceeded="5 resimden fazla eklenemez.";
        public static string ImageAdded="Resim başarıyla eklendi.";
        public static string ImageUpdated="Resim başarıyla güncellendi.";
        public static string UserNotFound="Kullanıcı bulunamadı";
        public static string PasswordError="Hatalı parola";
        public static string SuccessfullLogin="Giriş Başarılı";
        public static string UserAlreadyExist="Kullanıcı zaten mevcut";
        public static string AccessTokenCreated="Access Token oluşturuldu";
        public static string AuthorizationDenied="Yetkiniz yok.";
    }
}