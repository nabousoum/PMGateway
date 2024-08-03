using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;
using TokenService;

namespace PMGateway.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        private readonly TokenService.TokenServiceClient tokenService = new TokenService.TokenServiceClient();
        private readonly ServiceMetier.Service1Client service = new ServiceMetier.Service1Client();

        // POST: api/Auth/login
        [AllowAnonymous]
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginModel loginModel)
        {
            Console.WriteLine("Login attempt: " + loginModel.Email);

            var isValidUser = AuthenticateUser(loginModel.Email, loginModel.Password);
            if (!isValidUser)
            {
                Console.WriteLine("Invalid credentials for user: " + loginModel.Email);
                return Unauthorized();
            }

            var claims = new[]
            {
        new Claim(ClaimTypes.Email, loginModel.Email),
        new Claim(ClaimTypes.Role, "Utilisateur")
    };

            var accessToken = tokenService.GenerateAccessToken(claims.Select(c => new ClaimData { Type = c.Type, Value = c.Value }).ToArray());
            var refreshToken = tokenService.GenerateRefreshToken();

            Console.WriteLine("Token generated for user: " + loginModel.Email);

            return Ok(new
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            });
        }

        // POST: api/Auth/refresh
        [AllowAnonymous]
        [HttpPost("refresh")]
        public IActionResult Refresh([FromBody] RefreshTokenModel refreshTokenModel)
        {
            var principal = tokenService.GetPrincipalFromExpiredToken(refreshTokenModel.AccessToken);
            var email = principal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;

            if (email == null)
            {
                return BadRequest("Invalid token");
            }

            var claims = principal.Claims.Select(c => new ClaimData { Type = c.Type, Value = c.Value }).ToArray();
            var newAccessToken = tokenService.GenerateAccessToken(claims);
            var newRefreshToken = tokenService.GenerateRefreshToken();

            // Mettre à jour le refresh token dans la base de données
            // Appeler une méthode de service pour mettre à jour le refresh token

            return Ok(new
            {
                AccessToken = newAccessToken,
                RefreshToken = newRefreshToken
            });
        }

        private bool AuthenticateUser(string email, string password)
        {
            // Implémentez votre logique d'authentification ici
            return true;
        }

        // POST: api/Auth/register
        [AllowAnonymous]
        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterModel registerModel)
        {
            if (service.UserExists(registerModel.Email))
            {
                return BadRequest("User already exists");
            }

            var newUser = new ServiceMetier.Utilisateur
            {
                Email = registerModel.Email,
                Password = registerModel.Password, // Assurez-vous de hacher le mot de passe avant de le stocker
                Prenom = registerModel.Prenom,
                Nom = registerModel.Nom,
                Telephone = registerModel.Telephone,
                Adresse = registerModel.Adresse
            };

            service.addUser(newUser);

            var claims = new[]
            {
                new Claim(ClaimTypes.Email, registerModel.Email),
                new Claim(ClaimTypes.Role, "Utilisateur")
            };

            var accessToken = tokenService.GenerateAccessToken(claims.Select(c => new ClaimData { Type = c.Type, Value = c.Value }).ToArray());
            var refreshToken = tokenService.GenerateRefreshToken();

            // Sauvegarder le refresh token dans la base de données avec la date d'expiration
            // Appeler une méthode de service pour sauvegarder le refresh token pour le nouvel utilisateur
            newUser.RefreshToken = refreshToken;
            newUser.RefreshTokenExpiryTime = DateTime.Now.AddDays(7);
            service.updateUser(newUser);

            return Ok(new
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            });
        }
    }

    public class LoginModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class RefreshTokenModel
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }

    public class RegisterModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Prenom { get; set; }
        public string Nom { get; set; }
        public string Telephone { get; set; }
        public string Adresse { get; set; }
    }
}
