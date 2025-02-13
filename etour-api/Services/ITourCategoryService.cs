
using etour_api.Models;

namespace etour_api.Services;

public interface ITourCategoryService
{
    Task<List<TourCategory>> GetAllTourCategories();
}