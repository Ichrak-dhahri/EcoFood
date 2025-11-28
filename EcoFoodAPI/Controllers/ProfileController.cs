// Controllers/ProfileController.cs
using EcoFoodAPI.DTOs.Profile;
using EcoFoodAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EcoFoodAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize] // Tous les endpoints nécessitent une authentification
    public class ProfileController : ControllerBase
    {
        private readonly IProfileService _profileService;

        public ProfileController(IProfileService profileService)
        {
            _profileService = profileService;
        }

        /// <summary>
        /// Récupérer le profil de l'utilisateur connecté
        /// </summary>
        [HttpGet("me")]
        public async Task<IActionResult> GetMyProfile()
        {
            var userId = GetCurrentUserId();

            var profile = await _profileService.GetProfileAsync(userId);

            if (profile == null)
            {
                return NotFound(new
                {
                    success = false,
                    message = "Utilisateur non trouvé"
                });
            }

            return Ok(new
            {
                success = true,
                message = "Profil récupéré avec succès",
                data = profile
            });
        }

        /// <summary>
        /// Récupérer le profil d'un utilisateur par son ID (public)
        /// </summary>
        [HttpGet("{id}")]
        [AllowAnonymous] // Permet l'accès sans authentification
        public async Task<IActionResult> GetUserProfile(int id)
        {
            var profile = await _profileService.GetProfileAsync(id);

            if (profile == null)
            {
                return NotFound(new
                {
                    success = false,
                    message = "Utilisateur non trouvé"
                });
            }

            return Ok(new
            {
                success = true,
                message = "Profil récupéré avec succès",
                data = profile
            });
        }

        /// <summary>
        /// Mettre à jour le profil de l'utilisateur connecté
        /// </summary>
        [HttpPut("me")]
        public async Task<IActionResult> UpdateMyProfile([FromBody] UpdateProfileDto updateDto)
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

            var userId = GetCurrentUserId();

            var updatedProfile = await _profileService.UpdateProfileAsync(userId, updateDto);

            if (updatedProfile == null)
            {
                return BadRequest(new
                {
                    success = false,
                    message = "Impossible de mettre à jour le profil. L'email est peut-être déjà utilisé."
                });
            }

            return Ok(new
            {
                success = true,
                message = "Profil mis à jour avec succès",
                data = updatedProfile
            });
        }

        /// <summary>
        /// Changer le mot de passe de l'utilisateur connecté
        /// </summary>
        [HttpPut("change-password")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDto changePasswordDto)
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

            var userId = GetCurrentUserId();

            var result = await _profileService.ChangePasswordAsync(userId, changePasswordDto);

            if (!result)
            {
                return BadRequest(new
                {
                    success = false,
                    message = "Ancien mot de passe incorrect"
                });
            }

            return Ok(new
            {
                success = true,
                message = "Mot de passe changé avec succès"
            });
        }

        /// <summary>
        /// Voir la réputation (avis) de l'utilisateur connecté
        /// </summary>
        [HttpGet("me/reputation")]
        public async Task<IActionResult> GetMyReputation()
        {
            var userId = GetCurrentUserId();

            var reputation = await _profileService.GetUserReputationAsync(userId);

            if (reputation == null)
            {
                return NotFound(new
                {
                    success = false,
                    message = "Utilisateur non trouvé"
                });
            }

            return Ok(new
            {
                success = true,
                message = "Réputation récupérée avec succès",
                data = reputation
            });
        }

        /// <summary>
        /// Voir la réputation d'un utilisateur par son ID (public)
        /// </summary>
        [HttpGet("{id}/reputation")]
        [AllowAnonymous]
        public async Task<IActionResult> GetUserReputation(int id)
        {
            var reputation = await _profileService.GetUserReputationAsync(id);

            if (reputation == null)
            {
                return NotFound(new
                {
                    success = false,
                    message = "Utilisateur non trouvé"
                });
            }

            return Ok(new
            {
                success = true,
                message = "Réputation récupérée avec succès",
                data = reputation
            });
        }

        // Méthode utilitaire pour récupérer l'ID de l'utilisateur connecté
        private int GetCurrentUserId()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return int.Parse(userIdClaim!);
        }
    }
}