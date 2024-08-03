using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Claims;
using System.Security.Cryptography;
using System.ServiceModel;
using System.Text;

namespace MetierPM
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "TokenService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select TokenService.svc or TokenService.svc.cs at the Solution Explorer and start debugging.
    public class TokenService : ITokenService
    {
        private readonly string _secretKey = "your_secret_key_here"; // Mettez votre clé secrète ici
        private readonly string _issuer = "your_issuer_here"; // Mettez votre émetteur ici
        private readonly string _audience = "your_audience_here"; // Mettez votre audience ici

        public string GenerateAccessToken(ClaimData[] claims)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var jwtClaims = claims.Select(c => new Claim(c.Type, c.Value)).ToArray();

            var token = new JwtSecurityToken(
                issuer: _issuer,
                audience: _audience,
                claims: jwtClaims,
                expires: DateTime.UtcNow.AddMinutes(15),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }

        public ClaimsPrincipalData GetPrincipalFromExpiredToken(string token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = false, // ici nous disons que nous ne nous soucions pas de la date d'expiration du token
                ValidateIssuerSigningKey = true,
                ValidIssuer = _issuer,
                ValidAudience = _audience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey))
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken;
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);
            var jwtSecurityToken = securityToken as JwtSecurityToken;
            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Token invalide");

            var claims = principal.Claims.Select(c => new ClaimData { Type = c.Type, Value = c.Value }).ToArray();
            return new ClaimsPrincipalData { Claims = claims };
        }
    }
}
