using EcoFoodAPI.DTOs.Profile;

namespace EcoFoodAPI.Services
{
    public interface IProfileService
    {
        Task<ProfileResponseDto?> GetProfileAsync(int userId);
        Task<ProfileResponseDto?> UpdateProfileAsync(int userId, UpdateProfileDto updateDto);
        Task<bool> ChangePasswordAsync(int userId, ChangePasswordDto changePasswordDto);
        Task<UserReputationDto?> GetUserReputationAsync(int userId);
    }
}