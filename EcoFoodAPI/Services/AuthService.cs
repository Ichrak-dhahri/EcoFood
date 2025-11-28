
// Services/AuthService.cs
using EcoFoodAPI.DTOs.Auth;
using EcoFoodAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

using EcoFoodAPI.Data;
namespace EcoFoodAPI.Services
{
    public class AuthService : IAuthService
    {
        private readonly EcoFoodDbContext _context;
        private readonly IConfiguration _configuration;

        public AuthService(EcoFoodDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<AuthResponseDto?> RegisterAsync(RegisterDto registerDto)
        {
            // Vérifier si l'email existe déjà
            var existingUser = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == registerDto.Email);

            if (existingUser != null)
            {
                return null; // Email déjà utilisé
            }

            // Hasher le mot de passe
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(registerDto.MotDePasse);

            // Créer le nouvel utilisateur
            var newUser = new User
            {
                Nom = registerDto.Nom,
                Prenom = registerDto.Prenom,
                Email = registerDto.Email,
                MotDePasse = hashedPassword,
                Ville = registerDto.Ville,
                Role = "User", // Par défaut, rôle utilisateur normal
                MoyenneRating = 0,
                TotalRatingsRecus = 0,
                TotalRatingsDonnes = 0
            };

            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();

            // Générer le token JWT
            string token = GenerateJwtToken(newUser);

            return new AuthResponseDto
            {
                IdUser = newUser.IdUser,
                Nom = newUser.Nom,
                Prenom = newUser.Prenom,
                Email = newUser.Email,
                Ville = newUser.Ville,
                Role = newUser.Role!,
                Token = token
            };
        }

        public async Task<AuthResponseDto?> LoginAsync(LoginDto loginDto)
        {
            // Rechercher l'utilisateur par email
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == loginDto.Email);

            if (user == null)
            {
                return null; // Utilisateur non trouvé
            }

            // Vérifier le mot de passe
            bool isPasswordValid = BCrypt.Net.BCrypt.Verify(loginDto.MotDePasse, user.MotDePasse);

            if (!isPasswordValid)
            {
                return null; // Mot de passe incorrect
            }

            // Générer le token JWT
            string token = GenerateJwtToken(user);

            return new AuthResponseDto
            {
                IdUser = user.IdUser,
                Nom = user.Nom,
                Prenom = user.Prenom,
                Email = user.Email,
                Ville = user.Ville,
                Role = user.Role!,
                Token = token
            };
        }

        private string GenerateJwtToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));

            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.IdUser.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, $"{user.Prenom} {user.Nom}"),
                new Claim(ClaimTypes.Role, user.Role!)
            };

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddDays(7),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}