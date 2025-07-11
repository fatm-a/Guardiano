using Carbook.Application.Dtos;
using Carbook.Application.Features.Mediator.Results.AppUserResults;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Carbook.Application.Tools
{
    public class JwtTokenGenerator
    {
        public static TokenResponseDto GenerateToken(GetCheckAppUserQueryResult result) 
        {
            var claims = new List<Claim>();

            if (!string.IsNullOrWhiteSpace(result.Role))
                claims.Add(new Claim(ClaimTypes.Role, result.Role));

            claims.Add(new Claim(ClaimTypes.NameIdentifier, result.Id.ToString()));

            if (!string.IsNullOrWhiteSpace(result.Username))
                claims.Add(new Claim("Username", result.Username));

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtTokenDefaults.Key));

            var signinCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var expireDate = DateTime.UtcNow.AddDays(JwtTokenDefaults.Expire);

            JwtSecurityToken token = new JwtSecurityToken(issuer: JwtTokenDefaults.ValidIssuer, audience: JwtTokenDefaults.ValidAudience,
                claims: claims, notBefore: DateTime.UtcNow, expires: expireDate, signingCredentials: signinCredentials);

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();

            return new TokenResponseDto(tokenHandler.WriteToken(token), expireDate);
        }
    }
}
//Bu sınıf, kullanıcı giriş işlemi başarılı olduktan sonra o kullanıcı için bir JWT (JSON Web Token) oluşturmak amacıyla kullanılır.

//JWT oluşturulurken kullanıcıya ait bazı kimlik bilgileri (ID, kullanıcı adı, rol gibi) token içine “claim” olarak eklenir.

//Token içine eklenen claim’ler, sistemin daha sonra bu bilgileri JWT üzerinden okuyabilmesini sağlar (örneğin kullanıcının rolü veya ID’si gibi).

//Token'ın güvenliği için bir gizli anahtar (secret key) kullanılarak token dijital olarak imzalanır.

//Kullanılan imzalama algoritması HMAC SHA-256’dır. Bu algoritma, token'ın sahte olup olmadığını doğrulamak için gereklidir.

//Token’ın geçerlilik süresi belirlenir (örneğin 1 gün), yani bu süreden sonra token artık geçerli kabul edilmez.

//Token’ın oluşturulmasında “issuer” ve “audience” bilgileri de belirtilir. Issuer, token’ı yayınlayan sistemdir. Audience ise token’ı kullanacak olan hedef sistemdir.

//Tüm bu bilgiler bir JwtSecurityToken nesnesi içinde birleştirilir.

//Oluşturulan bu token, string formatına çevrilerek kullanıma hazır hale getirilir.

//Token string’i ve geçerlilik süresi, TokenResponseDto nesnesine yerleştirilip sistemde kullanılmak üzere dışarıya döndürülür.

