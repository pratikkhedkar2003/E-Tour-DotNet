
using etour_api.Models;

namespace etour_api.Services;

public interface ITourSubCategoryService
{
    Task<List<TourSubcategory>> GetAllTourSubcategories();
    Task<List<TourSubcategory>> GetTourSubCategoriesByTourCategoryId(ulong tourCategoryId);
}