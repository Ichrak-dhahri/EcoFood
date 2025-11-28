
// Services/CategoryService.cs
using EcoFoodAPI.Data;
using EcoFoodAPI.DTOs.Category;
using EcoFoodAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace EcoFoodAPI.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly EcoFoodDbContext _context;

        public CategoryService(EcoFoodDbContext context)
        {
            _context = context;
        }

        public async Task<List<CategoryResponseDto>> GetAllCategoriesAsync()
        {
            var categories = await _context.Categories
                .Include(c => c.Produits)
                .Select(c => new CategoryResponseDto
                {
                    IdCategorie = c.IdCategorie,
                    NomCategorie = c.NomCategorie,
                    NombreProduits = c.Produits.Count
                })
                .OrderBy(c => c.NomCategorie)
                .ToListAsync();

            return categories;
        }

        public async Task<CategoryResponseDto?> GetCategoryByIdAsync(int id)
        {
            var category = await _context.Categories
                .Include(c => c.Produits)
                .FirstOrDefaultAsync(c => c.IdCategorie == id);

            if (category == null)
                return null;

            return new CategoryResponseDto
            {
                IdCategorie = category.IdCategorie,
                NomCategorie = category.NomCategorie,
                NombreProduits = category.Produits.Count
            };
        }

        public async Task<CategoryResponseDto?> CreateCategoryAsync(CreateCategoryDto createDto)
        {
            // Vérifier si la catégorie existe déjà
            var existingCategory = await _context.Categories
                .FirstOrDefaultAsync(c => c.NomCategorie.ToLower() == createDto.NomCategorie.ToLower());

            if (existingCategory != null)
                return null; // Catégorie déjà existante

            var newCategory = new Categorie
            {
                NomCategorie = createDto.NomCategorie
            };

            _context.Categories.Add(newCategory);
            await _context.SaveChangesAsync();

            return new CategoryResponseDto
            {
                IdCategorie = newCategory.IdCategorie,
                NomCategorie = newCategory.NomCategorie,
                NombreProduits = 0
            };
        }

        public async Task<CategoryResponseDto?> UpdateCategoryAsync(int id, UpdateCategoryDto updateDto)
        {
            var category = await _context.Categories
                .Include(c => c.Produits)
                .FirstOrDefaultAsync(c => c.IdCategorie == id);

            if (category == null)
                return null;

            // Vérifier si le nouveau nom existe déjà (sauf pour la catégorie actuelle)
            var nameExists = await _context.Categories
                .AnyAsync(c => c.NomCategorie.ToLower() == updateDto.NomCategorie.ToLower() && c.IdCategorie != id);

            if (nameExists)
                return null;

            category.NomCategorie = updateDto.NomCategorie;

            await _context.SaveChangesAsync();

            return new CategoryResponseDto
            {
                IdCategorie = category.IdCategorie,
                NomCategorie = category.NomCategorie,
                NombreProduits = category.Produits.Count
            };
        }

        public async Task<bool> DeleteCategoryAsync(int id)
        {
            var category = await _context.Categories
                .Include(c => c.Produits)
                .FirstOrDefaultAsync(c => c.IdCategorie == id);

            if (category == null)
                return false;

            // Vérifier si la catégorie contient des produits
            if (category.Produits.Any())
            {
                // Empêcher la suppression si la catégorie contient des produits
                return false;
            }

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}