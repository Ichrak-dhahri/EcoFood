// Controllers/CategoryController.cs
using EcoFoodAPI.DTOs.Category;
using EcoFoodAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EcoFoodAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        /// <summary>
        /// Récupérer toutes les catégories (Public - accessible à tous)
        /// </summary>
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllCategories()
        {
            var categories = await _categoryService.GetAllCategoriesAsync();

            return Ok(new
            {
                success = true,
                message = "Liste des catégories récupérée avec succès",
                data = categories
            });
        }

        /// <summary>
        /// Récupérer une catégorie par ID (Public - accessible à tous)
        /// </summary>
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetCategoryById(int id)
        {
            var category = await _categoryService.GetCategoryByIdAsync(id);

            if (category == null)
            {
                return NotFound(new
                {
                    success = false,
                    message = "Catégorie non trouvée"
                });
            }

            return Ok(new
            {
                success = true,
                message = "Catégorie récupérée avec succès",
                data = category
            });
        }

        /// <summary>
        /// Ajouter une nouvelle catégorie (Admin uniquement) 🔒
        /// </summary>
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryDto createDto)
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

            var category = await _categoryService.CreateCategoryAsync(createDto);

            if (category == null)
            {
                return BadRequest(new
                {
                    success = false,
                    message = "Cette catégorie existe déjà"
                });
            }

            return CreatedAtAction(
                nameof(GetCategoryById),
                new { id = category.IdCategorie },
                new
                {
                    success = true,
                    message = "Catégorie créée avec succès",
                    data = category
                });
        }

        /// <summary>
        /// Modifier une catégorie existante (Admin uniquement) 🔒
        /// </summary>
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateCategory(int id, [FromBody] UpdateCategoryDto updateDto)
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

            var category = await _categoryService.UpdateCategoryAsync(id, updateDto);

            if (category == null)
            {
                return BadRequest(new
                {
                    success = false,
                    message = "Impossible de modifier la catégorie. Elle n'existe pas ou le nom est déjà utilisé."
                });
            }

            return Ok(new
            {
                success = true,
                message = "Catégorie modifiée avec succès",
                data = category
            });
        }

        /// <summary>
        /// Supprimer une catégorie (Admin uniquement) 🔒
        /// </summary>
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var result = await _categoryService.DeleteCategoryAsync(id);

            if (!result)
            {
                return BadRequest(new
                {
                    success = false,
                    message = "Impossible de supprimer la catégorie. Elle n'existe pas ou contient des produits."
                });
            }

            return Ok(new
            {
                success = true,
                message = "Catégorie supprimée avec succès"
            });
        }
    }
}