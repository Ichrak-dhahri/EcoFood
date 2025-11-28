using EcoFoodAPI.DTOs.Category;

namespace EcoFoodAPI.Services
{
    public interface ICategoryService
    {
        Task<List<CategoryResponseDto>> GetAllCategoriesAsync();
        Task<CategoryResponseDto?> GetCategoryByIdAsync(int id);
        Task<CategoryResponseDto?> CreateCategoryAsync(CreateCategoryDto createDto);
        Task<CategoryResponseDto?> UpdateCategoryAsync(int id, UpdateCategoryDto updateDto);
        Task<bool> DeleteCategoryAsync(int id);
    }
}