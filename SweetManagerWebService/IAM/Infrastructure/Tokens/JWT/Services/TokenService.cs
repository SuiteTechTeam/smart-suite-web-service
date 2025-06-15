using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SweetManagerIotWebService.API.IAM.Application.Internal.OutboundServices;
using SweetManagerIotWebService.API.IAM.Infrastructure.Tokens.JWT.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SweetManagerIotWebService.API.IAM.Infrastructure.Tokens.JWT.Services
{
    public class TokenService(IOptions<TokenSettings> tokenSettings) : ITokenService
    {
        private readonly TokenSettings _tokenSettings = tokenSettings.Value;

        public string GenerateToken(dynamic user)
        {
            SymmetricSecurityKey securityKey = new(Encoding.ASCII.GetBytes(_tokenSettings.SecretKey));

            SigningCredentials credentials = new(securityKey, SecurityAlgorithms.HmacSha256);

            // Obtener valores de forma segura usando conversiones explícitas
            string userId = user.Id.ToString();
            string userRole = user.Role?.ToString() ?? "UNKNOWN_ROLE";
            string userEmail = user.Email?.ToString() ?? userId;
            
            string validationHotel = "0";
            if (user.Hotel != null && !string.IsNullOrEmpty(user.Hotel.ToString()))
            {
                validationHotel = user.Hotel.ToString();
            }

            Claim[]? claims =
            [
                new Claim(ClaimTypes.Sid, userId),
                new Claim(ClaimTypes.Role, userRole),
                new Claim(ClaimTypes.Locality, validationHotel),
                new Claim(ClaimTypes.Email, userEmail),
                new Claim("Email", userEmail),
                new Claim("UserId", userId)
            ];

            JwtSecurityToken token = new(
                issuer: _tokenSettings.Issuer,
                audience: _tokenSettings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_tokenSettings.Expire),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }        public dynamic? ValidateToken(string? token)
        {
            if (string.IsNullOrEmpty(token))
                return null;

            try
            {
                var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_tokenSettings.SecretKey));

                JwtSecurityTokenHandler tokenHandler = new();

                TokenValidationParameters validationParameters = new()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = _tokenSettings.Issuer,
                    ValidAudience = _tokenSettings.Audience,
                    IssuerSigningKey = securityKey,
                    LifetimeValidator = LifetimeValidator,
                    ClockSkew = TimeSpan.Zero
                };

                var principal = tokenHandler.ValidateToken(token, validationParameters, out var securityToken);

                var result = (JwtSecurityToken)securityToken;
                
                // Obtener los claims de forma segura
                var sidClaim = result.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Sid);
                var id = sidClaim != null ? Convert.ToInt32(sidClaim.Value) : 0;
                
                // Obtener email desde los claims
                string email = id.ToString(); // Default: usar el ID como email si no hay email
                
                var emailClaim = result.Claims.FirstOrDefault(claim => claim.Type == "Email" || claim.Type == ClaimTypes.Email);
                if (emailClaim != null && !string.IsNullOrEmpty(emailClaim.Value))
                {
                    email = emailClaim.Value;
                }
                
                // Obtener rol
                var roleClaim = result.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Role);
                var role = roleClaim != null ? Convert.ToString(roleClaim.Value) : "UNKNOWN_ROLE";
                
                // Obtener hotel
                var hotelClaim = result.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Locality);
                var hotel = hotelClaim != null ? Convert.ToString(hotelClaim.Value) : "0";
                
                // Retornar objeto anónimo con la información del token
                return new { Id = id, Email = email, Role = role, Hotel = hotel };
            }
            catch (Exception)
            {
                return null;
            }
        }

        private static bool LifetimeValidator(DateTime? notBefore, DateTime? expires, SecurityToken securityToken,
            TokenValidationParameters validationParameters)
        {
            if (expires is null) return false;

            var now = DateTime.UtcNow;

            var valid = now < expires;

            if (!valid)
            {
                Console.WriteLine($"Token expired. Current time: {now}, Expiration time: {expires}");
            }

            //return DateTime.UtcNow < expires;

            return valid;
        }
    }
}
