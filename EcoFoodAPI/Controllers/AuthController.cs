// Controllers/AuthController.cs
using EcoFoodAPI.DTOs.Auth;
using EcoFoodAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace EcoFoodAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        /// <summary>
        /// Inscription d'un nouvel utilisateur
        /// </summary>
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new
                {
                    success = false,
                    message = "Données invalides",
                    errors = ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage)
                });
            }

            var result = await _authService.RegisterAsync(registerDto);

            if (result == null)
            {
                return BadRequest(new
                {
                    success = false,
                    message = "Cet email est déjà utilisé"
                });
            }

            return Ok(new
            {
                success = true,
                message = "Inscription réussie",
                data = result
            });
        }

        /// <summary>
        /// Connexion d'un utilisateur
        /// </summary>
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new
                {
                    success = false,
                    message = "Données invalides",
                    errors = ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage)
                });
            }

            var result = await _authService.LoginAsync(loginDto);

            if (result == null)
            {
                return Unauthorized(new
                {
                    success = false,
                    message = "Email ou mot de passe incorrect"
                });
            }

            return Ok(new
            {
                success = true,
                message = "Connexion réussie",
                data = result
            });
        }
    }
}