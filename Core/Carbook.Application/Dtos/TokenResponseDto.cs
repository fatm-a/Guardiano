using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carbook.Application.Dtos
{
   public class TokenResponseDto
    {
        // Kurucu metod: Bu sınıf new'lenirken token ve expireDate değerleri atanır.
        public TokenResponseDto(string token, DateTime expireDate)
        {
            Token = token; // JWT token değeri
            ExpireDate = expireDate;  // Token'ın geçerlilik süresinin bitiş tarihi
        }

        public string Token { get; set; }// JWT token değerini tutar. Bu token, kullanıcı doğrulandıktan sonra üretilir ve client'a gönderilir.
        public DateTime ExpireDate { get; set; } // Token'ın geçerlilik süresinin sona ereceği zamanı belirtir.
    }
} // Bu sınıf, JWT token oluşturulduktan sonra istemciye dönecek bilgileri temsil eder.