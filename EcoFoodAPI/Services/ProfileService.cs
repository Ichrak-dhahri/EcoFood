// Services/ProfileService.cs
using EcoFoodAPI.Data;
using EcoFoodAPI.DTOs.Profile;
using EcoFoodAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace EcoFoodAPI.Services
{
    public class ProfileService : IProfileService
    {
        private readonly EcoFoodDbContext _context;

        public ProfileService(EcoFoodDbContext context)
        {
            _context = context;
        }

        public async Task<ProfileResponseDto?> GetProfileAsync(int userId)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.IdUser == userId);

            if (user == null)
                return null;

            return new ProfileResponseDto
            {
                IdUser = user.IdUser,
                Nom = user.Nom,
                Prenom = user.Prenom,
                Email = user.Email,
                Ville = user.Ville,
                Role = user.Role!,
                MoyenneRating = user.MoyenneRating,
                TotalRatingsRecus = user.TotalRatingsRecus,
                TotalRatingsDonnes = user.TotalRatingsDonnes
            };
        }

        public async Task<ProfileResponseDto?> UpdateProfileAsync(int userId, UpdateProfileDto updateDto)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.IdUser == userId);

            if (user == null)
                return null;

            // Vérifier si l'email est déjà utilisé par un autre utilisateur
            var emailExists = await _context.Users
                .AnyAsync(u => u.Email == updateDto.Email && u.IdUser != userId);

            if (emailExists)
                return null;

            // Mettre à jour les informations
            user.Nom = updateDto.Nom;
            user.Prenom = updateDto.Prenom;
            user.Email = updateDto.Email;
            user.Ville = updateDto.Ville;

            await _context.SaveChangesAsync();

            return new ProfileResponseDto
            {
                IdUser = user.IdUser,
                Nom = user.Nom,
                Prenom = user.Prenom,
                Email = user.Email,
                Ville = user.Ville,
                Role = user.Role!,
                MoyenneRating = user.MoyenneRating,
                TotalRatingsRecus = user.TotalRatingsRecus,
                TotalRatingsDonnes = user.TotalRatingsDonnes
            };
        }

        public async Task<bool> ChangePasswordAsync(int userId, ChangePasswordDto changePasswordDto)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.IdUser == userId);

            if (user == null)
                return false;

            // Vérifier l'ancien mot de passe
            bool isOldPasswordValid = BCrypt.Net.BCrypt.Verify(
                changePasswordDto.AncienMotDePasse,
                user.MotDePasse
            );

            if (!isOldPasswordValid)
                return false;

            // Hasher et enregistrer le nouveau mot de passe
            user.MotDePasse = BCrypt.Net.BCrypt.HashPassword(changePasswordDto.NouveauMotDePasse);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<UserReputationDto?> GetUserReputationAsync(int userId)
        {
            var user = await _context.Users
                .Include(u => u.RatingIdToUserNavigations)
                    .ThenInclude(r => r.IdFromUserNavigation)
                .FirstOrDefaultAsync(u => u.IdUser == userId);

            if (user == null)
                return null;

            var ratings = user.RatingIdToUserNavigations
                .OrderByDescending(r => r.DateRating)
                .Select(r => new RatingDetailDto
                {
                    IdRating = r.IdRating,
                    Note = r.Note,  // Changé de "Score" à "Note"
                    Commentaire = r.Commentaire,
                    DateRating = r.DateRating,
                    NomDonneur = r.IdFromUserNavigation.Nom,
                    PrenomDonneur = r.IdFromUserNavigation.Prenom
                })
                .ToList();

            return new UserReputationDto
            {
                IdUser = user.IdUser,
                NomComplet = $"{user.Prenom} {user.Nom}",
                MoyenneRating = user.MoyenneRating,
                TotalRatingsRecus = user.TotalRatingsRecus,
                Ratings = ratings
            };
        }
    }
}